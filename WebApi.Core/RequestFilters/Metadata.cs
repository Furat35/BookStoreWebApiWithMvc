namespace WebApi.Core.RequestFilters
{
    public class Metadata
    {
        public Metadata()
        {

        }

        protected int _maxPageSize = 50;
        protected int _pageSize = 5;
        protected int _totalPages = 1;
        protected int _currentPage = 1;
        protected int _totalEntitites = 1;

        public int CurrentPage
        {
            get => _currentPage;
            set => _currentPage = value < 1 ? _currentPage : value;
        }

        public int PageSize
        {
            get => _pageSize;
            set
            {
                if (value > 50)
                {
                    _pageSize = _maxPageSize;
                }
                else if (value < 1)
                {
                    _pageSize = 1;
                }
                else
                {
                    _pageSize = value;
                }
            }
        }

        public int TotalPages
        {
            get
            {
                return _totalPages;
            }
            set
            {
                _totalPages = value == 0
                    ? 1
                    : value;
            }
        }

        public int TotalEntities
        {
            get
            {
                return _totalEntitites;
            }
            set
            {
                _totalEntitites = value;
            }
        }

        public bool HasNext => CurrentPage < TotalPages;

        public bool HasPrevious => CurrentPage > 1;
    }
}
