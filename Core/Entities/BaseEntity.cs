

namespace UserManagementTask.Core.Entities
{
    public class BaseEntity
    {
        public int Id { get; set; }
        public DateTime CreationDate { get; set; } = DateTime.Now;
        public bool IsDeleted { get; set; }
    }
}
