namespace API.Controllers.DTOs
{
    public class UserToSignUpDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string ContactNo { get; set; }
        public string OtherContactNo { get; set; }
        public string Facebook { get; set; }
        public string Instagram { get; set; }
        public string Twitter { get; set; }
        public string Whatsapp { get; set; }
        public string LinkedIn { get; set; }
    }
}