namespace UserManagementTask.Core.Entities
{
    public class User: BaseEntity
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string UserFullName { get; set; }
        public bool IsActive { get; set; } = true;
        public DateTime DateOfBirth { get; set; }
    }
}
