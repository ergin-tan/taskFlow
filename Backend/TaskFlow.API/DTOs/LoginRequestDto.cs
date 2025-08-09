namespace TaskFlow.API.DTOs
{
    public class LoginRequestDto
    {
        public string EmployeeID { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
    }
}
