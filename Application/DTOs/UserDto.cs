namespace UserManagementTask.Application.DTOs
{
    public class UserDto
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string UserFullName { get; set; }
        public bool IsActive { get; set; } = true;
        public DateTime DateOfBirth { get; set; }
        public DateTime CreationDate { get; set; }
    }
}
