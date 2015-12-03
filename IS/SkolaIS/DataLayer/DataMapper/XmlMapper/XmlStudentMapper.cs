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
    public class XmlTeachingHourMapper : AbstractXmlMapper<ITeachingHour>, ITeachingHourRepository
    {
        protected override string FileName
        {
            get { return "teachingHour.xml"; }
        }

        public XmlTeachingHourMapper(string directory)
            : base(directory)
        {

        }

        protected override ITeachingHour LoadObject(XmlNode node)
        {
            return new TeachingHour(
                Int32.Parse(node.Attributes["day"].Value),
                Int32.Parse(node.Attributes["order"].Value)
            );
        }

        public IEnumerable<ITeachingHour> FindByScheduleId(long scheduleId)
        {
            throw new NotImplementedException();
        }
    }
}
