namespace PopcornReady.Core.Options
{
    public class ApiOptions
    {
        public const string SectionName = "Api";
        public const string EpisodateClientName = nameof(Episodate);
        public string Episodate { get; init; }
    }
}
