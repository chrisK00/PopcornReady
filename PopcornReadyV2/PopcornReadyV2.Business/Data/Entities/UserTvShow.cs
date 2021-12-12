namespace PopcornReadyV2.Business.Data.Entities
{
    /// <summary>
    /// Allows a User to Track a Tv Show
    /// </summary>
    public class UserTvShow
    {
        public string UserId { get; init; }
        public int TvShowId { get; init; }
    }
}
