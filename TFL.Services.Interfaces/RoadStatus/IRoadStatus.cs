namespace TFL.Services.Interfaces.RoadStatus
{
    public interface IRoadStatus
    {
        string RequestedRoad { get; set; }

        string DisplayName { get; set; }

        string Status { get; set; }

        string StatusDescription { get; set; }

        bool ValidRoad { get; set; }

        int ErrorCode { get; set; }

        string ErrorDescription { get; set; }

        string ToString();
    }
}
