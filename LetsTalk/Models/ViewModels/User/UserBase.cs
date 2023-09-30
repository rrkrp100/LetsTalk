namespace LetsTalk.Models
{
    public class UserBase
    {
        public string UserId { get; set; }
        public string UserName { get; set; }
        public string GivenName { get; set; }
        public string Email { get; set; }
        public string Role { get; set; }
        public string Password { get; set; }
        public int Age { get; set; }
        public List<Guid> ChatRooms { get; set; }

    }
}



