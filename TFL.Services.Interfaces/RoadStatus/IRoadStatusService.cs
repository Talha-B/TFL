namespace TFL.Services.Interfaces.RoadStatus
{
    public interface IRoadStatusService
    {
        IRoadStatus GetStatus(string roadId);
    }
}