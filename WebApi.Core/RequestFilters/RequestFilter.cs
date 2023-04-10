namespace WebApi.Core.RequestFilters
{
    public class RequestFilter
    {
        protected int _maxPageSize = 50;
        protected int _pageSize = 5;
        protected int _page = 1;
        public int Page
        {
            get => _page;
            set => _page = value < 1 ? _page : value;
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
    }
}
