namespace LoanAPI.Models
{
    public class ErrorLog
    {
        public int Id { get; set; }
        public string ErrorMessage { get; set; }
        public string UserId { get; set; }
        public string UserRole { get; set; }
        public string ErrorStatus { get; set; }
        public string MethodName { get; set; } 
        public DateTime InsertDate { get; set; }
    }

}
