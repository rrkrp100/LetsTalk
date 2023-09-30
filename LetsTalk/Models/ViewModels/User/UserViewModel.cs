namespace LetsTalk.Models
{
    public class UserViewModel : UserBase, IViewModel
    {
        public string? SessionToken { get; set; }
        public List<Error>? Errors { get; set; }
    }
    
}



