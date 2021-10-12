﻿namespace PopcornReady.Core.Options
{
    public class ApiOptions
    {
        public const string SectionName = "Apis";
        public const string EpisodateClientName = nameof(Episodate);
        public string Episodate { get; init; }
    }
}
