using WebApi.Core.Entities.Concrete;

namespace WebApi.Entity.Entities
{
    public class Author : BaseEntity
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string? Phone { get; set; }
        public string? Mail { get; set; }
        public DateTime? BirthDate { get; set; }
        public ICollection<Book>? Books { get; set; }
    }
}
