﻿using System;

namespace PopcornReady.Razor.Entities
{
    public class TvShow
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int ApiId { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string Status { get; set; }
        public DateTime LastUpdateDate { get; } = DateTime.Now;
        public Episode NextEpisode { get; set; }
    }
}
