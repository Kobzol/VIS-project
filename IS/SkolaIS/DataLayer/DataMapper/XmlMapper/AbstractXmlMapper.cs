using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using DataLayer.Database;
using DataLayer.Helper;
using DomainLayer;

namespace DataLayer.DataMapper.XmlMapper
{
    public abstract class AbstractXmlMapper<T> where T : IIdentifiable
    {
        public string Directory { get; private set; }
        protected abstract string FileName { get; }
        protected XmlDocument Document { get; private set; }

        protected IdentityMap<T> identityMap = new IdentityMap<T>();

        protected virtual string ObjectTag { get { return "object"; } }
        protected virtual string ObjectIdAttribute { get { return "id"; } }

        public AbstractXmlMapper(string directory)
        {
            this.Directory = directory;
            this.Document = this.LoadMapperFile();
        }

        public virtual T Find(long id)
        {
            if (this.HasStoredObject(id))
            {
                return this.GetStoredObject(id);
            }

            foreach (XmlNode node in this.Document.GetElementsByTagName(this.ObjectTag))
            {
                if (node.Attributes[this.ObjectIdAttribute] != null && Int64.Parse(node.Attributes[this.ObjectIdAttribute].Value) == id)
                {
                    return this.LoadObject(node);
                }
            }

            throw new PersistenceException("Object with id {0} was not found".FormatWith(id));
        }
        public virtual void Update(T t)
        {
            throw new NotImplementedException();
        }
        public virtual void Delete(T t)
        {
            throw new NotImplementedException();
        }

        protected abstract T LoadObject(XmlNode node);

        protected virtual bool HasStoredObject(long id)
        {
            return this.identityMap.HasObject(id);
        }
        protected virtual T GetStoredObject(long id)
        {
            return this.identityMap.GetObject(id);
        }

        private XmlDocument LoadMapperFile()
        {
            XmlDocument doc = new XmlDocument();
            doc.Load(Path.Combine(this.Directory, this.FileName));

            return doc;
        }
    }
}
