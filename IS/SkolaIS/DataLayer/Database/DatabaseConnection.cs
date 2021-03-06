﻿using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataLayer.Helper;

namespace DataLayer.Database
{
    public class DatabaseConnection
    {
        private readonly string connectionString;

        public SqlConnection Connection { get; private set; }

        private SqlTransaction activeTransaction;

        public DatabaseConnection(string connectionString)
        {
            this.connectionString = connectionString;
        }
        public DatabaseConnection(string server, string user, string password)
        {
            this.connectionString = "Data Source={0};Persist Security Info=True;User ID={1};Password={2}".FormatWith(server, user, password);
        }

        public void Connect()
        {
            this.Close();

            this.Connection = new SqlConnection(this.connectionString);
            this.Connection.Open();
        }
        public void Close()
        {
            if (this.IsConnected())
            {
                this.Connection.Close();
                this.Connection.Dispose();
                this.Connection = null;
            }
        }
        public bool IsConnected()
        {
            if (this.Connection != null)
            {
                return this.Connection.State == System.Data.ConnectionState.Open;
            }
            else return false;
        }

        public SqlCommand GetCommand(string commandText)
        {
            if (this.IsTransactionActive())
            {
                return new SqlCommand(commandText, this.Connection, this.activeTransaction);
            }
            else return new SqlCommand(commandText, this.Connection);
        }

        public SqlTransaction BeginTransaction()
        {
            if (this.IsTransactionActive())
            {
                throw new PersistenceException("A transaction is already active.");
            }

            return this.activeTransaction = this.Connection.BeginTransaction(System.Data.IsolationLevel.Serializable);
        }
        public void Commit()
        {
            if (this.IsTransactionActive())
            {
                this.activeTransaction.Commit();
                this.ClearTransaction();
            }
        }
        public void Rollback()
        {
            if (this.IsTransactionActive())
            {
                this.activeTransaction.Rollback();
                this.ClearTransaction();
            }
        }

        public long GetLastInsertedId()
        {
            SqlCommand command = this.GetCommand("SELECT @@IDENTITY");
            string data = command.ExecuteScalar().ToString();

            return Int64.Parse(data);
        }
        public long GetLastInsertedId(string tableName)
        {
            SqlCommand command = this.GetCommand("SELECT IDENT_CURRENT(@table)");
            command.Parameters.AddWithValue("table", tableName);
            string data = command.ExecuteScalar().ToString();

            return Int64.Parse(data);
        }

        public void Dispose()
        {
            if (this.IsConnected())
            {
                this.Close();
            }
        }

        private void ClearTransaction()
        {
            this.activeTransaction.Dispose();
            this.activeTransaction = null;
        }
        private bool IsTransactionActive()
        {
            return this.activeTransaction != null;
        }
    }
}
