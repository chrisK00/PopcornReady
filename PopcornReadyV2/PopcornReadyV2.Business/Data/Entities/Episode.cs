using System;

namespace PopcornReadyV2.Business.Data.Entities
{
    public class Episode
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Season { get; set; }
        public int Number { get; set; }
        public DateTime AirDate { get; set; }
    }
}