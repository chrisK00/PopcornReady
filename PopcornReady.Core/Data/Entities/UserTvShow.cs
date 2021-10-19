namespace PopcornReady.Core.Data.Entities
{
    /// <summary>
    /// Allows a User to Track a Tv Show
    /// </summary>
    public class UserTvShow
    {
        public int UserId { get; init; }
        public int TvShowId { get; init; }
    }
}
