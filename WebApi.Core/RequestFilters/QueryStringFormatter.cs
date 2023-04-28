using System.Text;

namespace WebApi.Core.RequestFilters
{
    public class QueryStringFormatter
    {
        public QueryStringFormatter()
        {

        }
        public string FormatQuery(object queryString)
        {
            var properties = queryString.GetType().GetProperties();
            StringBuilder builder = new StringBuilder();
            foreach (var property in properties)
            {
                var value = property.GetValue(queryString)?.ToString();
                builder.Append(property.Name + "=" + value + "&");
            }
            builder.Replace("&", "", builder.Length - 1, 1);
            return builder.ToString();
        }
    }
}
