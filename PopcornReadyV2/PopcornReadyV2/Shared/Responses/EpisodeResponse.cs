using System;

namespace PopcornReadyV2.Shared.Responses
{
    public class EpisodeResponse
    {
        public string Name { get; set; }
        public int Season { get; set; }
        public int Number { get; set; }
        public DateTime AirDate { get; set; }
    }
}