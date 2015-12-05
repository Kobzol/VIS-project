using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using DomainLayer;
using DomainLayer.Repository;

namespace DataLayer.DataMapper.XmlMapper
{
    public class XmlGradeMapper : AbstractXmlMapper<IGrade>, IGradeRepository
    {
        protected override string FileName
        {
            get { return "grade.xml"; }
        }

        public XmlGradeMapper(string directory) : base(directory)
        {
            
        }

        protected override void UpdateObject(IGrade t)
        {
            XmlNode node = this.GetNode(t);
            node.Attributes["weight"].Value = t.Weight.ToString();
            node.Attributes["value"].Value = t.Value.ToString();

            this.Save();
        }

        protected override void Create(IGrade t)
        {
            XmlNode node = this.Document.CreateElement(this.ObjectTag);
            this.AddAttribute(node, this.ObjectIdAttribute, t.Id);
            this.AddAttribute(node, "weight", t.Weight);
            this.AddAttribute(node, "value", t.Value);

            this.AddObject(node);

            this.Save();
        }
        public override void Delete(IGrade t)
        {
            XmlNode node = this.GetNode(t);
            node.ParentNode.RemoveChild(node);
            this.Save();
        }

        protected override IGrade LoadObject(XmlNode node)
        {
            long id = Int64.Parse(node.Attributes[this.ObjectIdAttribute].Value);

            if (this.identityMap.HasObject(id))
            {
                return this.identityMap.GetObject(id);
            }

            IGrade grade = new Grade(
                Double.Parse(node.Attributes["weight"].Value),
                Double.Parse(node.Attributes["value"].Value)
            );

            grade.Id = id;
            this.identityMap.PutObject(grade);

            return grade;
        }
        public Dictionary<long, IGrade> FindByTestId(long testId)
        {
            Dictionary<long, IGrade> grades = new Dictionary<long, IGrade>();

            foreach (XmlNode node in this.Document.GetElementsByTagName(this.ObjectTag))
            {
                if (node.Attributes["testId"].Value == testId.ToString())
                {
                    long studentId = Int64.Parse(node.Attributes["studentId"].Value);
                    grades[studentId] = this.LoadObject(node);
                }
            }

            return grades;
        }
    }
}
