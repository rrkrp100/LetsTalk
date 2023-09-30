namespace LetsTalk.Models
{
    public interface IViewModel
    {
        public List<Error>? Errors { get; set; }
    }
    public class Error
    {
        public Error() 
        {
            Message = "Error";
            Code = 500; 
        }
        public Error(string message, int code)
        {
            Message = message;
            Code = code;
        }

        public string Message { get; set; }
        public int Code { get; set; }
    }
}
