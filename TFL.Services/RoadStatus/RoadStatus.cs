using System.Collections.Generic;
using TFL.Services.Interfaces.RoadStatus;

namespace TFL.Services.RoadStatus
{
    public class RoadStatus : IRoadStatus
    {
        public string RequestedRoad { get; set; }

        public string DisplayName { get; set; }

        public string Status { get; set; }

        public string StatusDescription { get; set; }

        public bool ValidRoad { get; set; } = true;

        public int ErrorCode { get; set; }

        public string ErrorDescription { get; set; }

        public override string ToString()
        {
            if (!this.ValidRoad)
            {
                return $"{this.RequestedRoad} is not a valid road\r";
            }

            var output = new List<string>
            {
                $"The Status of the {this.DisplayName} is as follows",
                $"Road Status is {this.Status}",
                $"Road Status Description is {this.StatusDescription}"
            };

            return string.Join($"{(char)10}{(char)9}", output); // 10 = carriage return, 9 = tab
        }
    }
}
