using StructureMap; 
using TFL.Services.Interfaces.RoadStatus;
using TFL.Services.RoadStatus;

namespace TFL.Configuration.StructureMap
{
    public class RoadStatusRegistry : Registry
    {
        public RoadStatusRegistry()
        {
            For<IRoadStatusService>().Use<RoadStatusService>();
            For<IRoadStatus>().Use<RoadStatus>();
        }
    }
}
