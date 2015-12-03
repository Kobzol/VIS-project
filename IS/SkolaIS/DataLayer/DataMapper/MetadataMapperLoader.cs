using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using DataLayer.Database;
using DataLayer.Helper;
using DomainLayer.Repository;

namespace DataLayer.DataMapper
{
    public class MetadataMapperLoader
    {
        public void LoadMappers(string xml, DatabaseConnection connection, RepositoryContainer repository)
        {
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(xml);

            XmlNode prefixes = doc.GetElementsByTagName("namespacePrefixes")[0];
            string dataLayerPrefix = prefixes.Attributes.GetNamedItem("dataLayer").Value;
            string domainPrefix = prefixes.Attributes.GetNamedItem("domain").Value;

            foreach (XmlNode mapping in doc.GetElementsByTagName("mapping"))
            {
                string mapperName = mapping.Attributes.GetNamedItem("mapper").Value;
                string domainClassName = mapping.Attributes.GetNamedItem("model").Value;

                object mapper = this.ConstructMapper(dataLayerPrefix, mapperName, connection);

                Type domainType = Type.GetType("{0}.{1}, {2}".FormatWith(domainPrefix, domainClassName, "DomainLayer"));

                this.AddMapper(mapper, domainType, repository);
            }
        }

        private void AddMapper(object mapper, Type domainType, RepositoryContainer repository)
        {
            /*MethodInfo method = typeof(MapperRegistry).GetMethod("RegisterMapper");
            method = method.MakeGenericMethod(domainType);
            method.Invoke(registry, new object[] { mapper });*/
        }

        private object ConstructMapper(string namespacePrefix, string mapperName, DatabaseConnection connection)
        {
            Type mapperType = Type.GetType("{0}.{1}".FormatWith(namespacePrefix, mapperName));
            ConstructorInfo info = mapperType.GetConstructor(new Type[] { typeof(DatabaseConnection) });
            return info.Invoke(new object[] { connection });
        }
    }
}
