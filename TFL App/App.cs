using System;
using TFL.Services.Interfaces.RoadStatus;

namespace TFL.App
{
    public class App
    {
        private readonly IRoadStatusService _roadStatusService;

        public App(IRoadStatusService roadStatusService)
        {
            this._roadStatusService = roadStatusService;
        }

        public void Run()
        {
            try
            {
                var road = Console.ReadLine();
                var roadStatus = this._roadStatusService.GetStatus(road);

                if (roadStatus != null)
                {
                    Console.WriteLine(roadStatus.ToString());
                    Environment.Exit(roadStatus.ErrorCode);
                }

                throw new Exception();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"There was an error processing this request: {ex.Message}");
                Environment.Exit(1);
            }
        }
    }
}