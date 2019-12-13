using System;

namespace API.Controllers.DTOs
{
    public class UserToReturnDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string ContactNo { get; set; }
        public string OtherContactNo { get; set; }
        public string Whatsapp { get; set; }
        public string LinkedIn { get; set; }
        public DateTime DateRegistered { get; set; }
        public bool Enabled { get; set; }
        public bool EmailVerified { get; set; }
        public bool VerifiedAsMerchant { get; set; }

    }
}