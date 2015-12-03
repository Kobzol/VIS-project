using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using DataLayer.Database;
using DataLayer.Helper;
using DomainLayer;

namespace DataLayer.DataMapper.SqlMapper
{
    public abstract class AbstractSqlMapper<T> where T : IIdentifiable
    {
        public DatabaseConnection Database { get; private set; }

        protected abstract string TableName { get; }
        protected virtual string FindByIdQuery { get { return "SELECT * FROM {0} WHERE {1} = @{1}".FormatWith(this.TableName, this.ColumnId); } }
        protected abstract string UpdateByIdQuery { get; }
        protected virtual string DeleteByIdQuery { get { return "DELETE FROM {0} WHERE {1} = @{1}".FormatWith(this.TableName, this.ColumnId); } }
        protected abstract string InsertQuery { get; }
        protected virtual string ColumnId
        {
            get
            {
                return "id";
            }
        }

        protected IdentityMap<T> identityMap = new IdentityMap<T>();

        public AbstractSqlMapper(DatabaseConnection connection)
        {
            this.Database = connection;
        }

        public virtual T Find(long id)
        {
            if (this.HasStoredObject(id))
            {
                return this.GetStoredObject(id);
            }

            SqlCommand command = this.Database.GetCommand(this.FindByIdQuery);
            command.Parameters.AddWithValue(this.ColumnId, id);

            T obj;

            using (SqlDataReader reader = command.ExecuteReader())
            {
                if (!reader.HasRows)
                {
                    throw new PersistenceException("Object with id {0} was not found".FormatWith(id));
                }

                reader.Read();

                obj = this.LoadObjectFromCache(reader);
            }

            return obj;
        }
        public virtual void Update(T t)
        {
            if (!t.IsPersisted)
            {
                this.Create(t);
            }
            else
            {
                SqlCommand command = this.Database.GetCommand(this.UpdateByIdQuery);

                foreach (KeyValuePair<string, object> obj in this.GetUpdateValues(t))
                {
                    command.Parameters.AddWithValue(obj.Key, obj.Value == null ? DBNull.Value : obj.Value);
                }

                command.Parameters.AddWithValue(this.ColumnId, t.Id);

                command.ExecuteNonQuery();
            }
        }
        public virtual void Delete(T t)
        {
            SqlCommand command = this.Database.GetCommand(this.DeleteByIdQuery);
            command.Parameters.AddWithValue(this.ColumnId, t.Id);

            command.ExecuteNonQuery();
        }

        protected virtual T LoadObjectFromCache(SqlDataReader reader)
        {
            long id = this.GetId(reader);

            if (this.HasStoredObject(id))
            {
                return this.GetStoredObject(id);
            }
            else return this.LoadObject(reader);
        }
        protected abstract T LoadObject(SqlDataReader reader);
        protected abstract Dictionary<string, object> GetUpdateValues(T t);
        protected abstract Dictionary<string, object> GetInsertValues(T t);
        protected virtual void Create(T t)
        {
            using (SqlTransaction transaction = this.Database.BeginTransaction())
            {
                SqlCommand command = this.Database.GetCommand(this.InsertQuery);

                foreach (KeyValuePair<string, object> obj in this.GetInsertValues(t))
                {
                    command.Parameters.AddWithValue(obj.Key, obj.Value == null ? DBNull.Value : obj.Value);
                }

                command.ExecuteNonQuery();

                this.AddId(t, this.Database.GetLastInsertedId());
                this.identityMap.PutObject(t);

                this.Database.Commit();
            }
        }
        protected virtual IEnumerable<T> FindMultiple(SqlCommand command)
        {
            List<T> objects = new List<T>();

            using (SqlDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    objects.Add(this.LoadObjectFromCache(reader));
                }
            }

            return objects;
        }

        protected virtual long GetId(SqlDataReader reader)
        {
            return reader.GetColumnValue<long>(this.ColumnId);
        }
        protected virtual bool HasStoredObject(long id)
        {
            return this.identityMap.HasObject(id);
        }
        protected virtual T GetStoredObject(long id)
        {
            return this.identityMap.GetObject(id);
        }
        protected void AddId(T t, long id)
        {
            t.Id = id;
        }
    }
}