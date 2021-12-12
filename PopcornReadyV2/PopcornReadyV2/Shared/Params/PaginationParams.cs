namespace PopcornReadyV2.Shared.Params
{
    public class PaginationParams
    {
        private int _itemsPerPage = 5;
        private const int _maxItemsPerPage = 20;

        public int PageNumber { get; set; } = 1;

        public int ItemsPerPage
        {
            get => _itemsPerPage;
            set => _itemsPerPage = value > _maxItemsPerPage ? _maxItemsPerPage : value;
        }
    }
}
