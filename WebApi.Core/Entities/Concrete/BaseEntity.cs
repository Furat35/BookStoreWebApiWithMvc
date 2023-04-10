using WebApi.Core.Entities.Abstract;

namespace WebApi.Core.Entities.Concrete
{
    public class BaseEntity : IBaseEntity
    {
        public Guid Id { get; set; }
        public DateTime CreateDate { get; set; } = DateTime.Now;
        public DateTime? UpdateDate { get; set; }
        public DateTime? DeleteData { get; set; }
        public bool IsDeleted { get; set; } = false;
    }
}
