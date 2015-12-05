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

            XmlNode node = this.GetNode(id);

            if (node != null)
            {
                return this.LoadObject(node);
            }

            throw new PersistenceException("Object with id {0} was not found".FormatWith(id));
        }
        public void Update(T t)
        {
            if (!t.IsPersisted)
            {
                this.Create(t);
            }
            else this.UpdateObject(t);
        }
        public abstract void Delete(T t);
        protected abstract void UpdateObject(T t);
        protected abstract void Create(T t);

        protected void Save()
        {
            this.Document.Save(this.GetTargetPath());
        }

        protected abstract T LoadObject(XmlNode node);

        protected virtual bool IsTargetNode(XmlNode node, T t)
        {
            return this.IsTargetNode(node, t.Id);
        }
        protected virtual bool IsTargetNode(XmlNode node, long id)
        {
            return node.Attributes[this.ObjectIdAttribute] != null && Int64.Parse(node.Attributes[this.ObjectIdAttribute].Value) == id;
        }
        protected virtual XmlNode GetNode(T t)
        {
            return this.GetNode(t.Id);
        }
        protected virtual XmlNode GetNode(long id)
        {
            foreach (XmlNode node in this.Document.GetElementsByTagName(this.ObjectTag))
            {
                if (this.IsTargetNode(node, id))
                {
                    return node;
                }
            }

            return null;
        }

        protected void AddAttribute<P>(XmlNode node, string key, P value)
        {
            XmlAttribute attrib = this.Document.CreateAttribute(key);
            attrib.Value = value.ToString();
            node.Attributes.Append(attrib);
        }
        protected void AddObject(XmlNode node)
        {
            this.Document.ChildNodes[0].AppendChild(node);
        }

        protected virtual bool HasStoredObject(long id)
        {
            return this.identityMap.HasObject(id);
        }
        protected virtual T GetStoredObject(long id)
        {
            return this.identityMap.GetObject(id);
        }

        protected string GetTargetPath()
        {
            return Path.Combine(this.Directory, this.FileName);
        }
        private XmlDocument LoadMapperFile()
        {
            string targetPath = this.GetTargetPath();

            if (!File.Exists(targetPath))
            {
                using (StreamWriter writer = new StreamWriter(File.Create(targetPath)))
                {
                    writer.Write("<?xml version=\"1.0\" encoding=\"utf-8\" standalone=\"no\" ?><objects></objects>");
                }
            }

            XmlDocument doc = new XmlDocument();
            doc.Load(targetPath);

            return doc;
        }
    }
}
