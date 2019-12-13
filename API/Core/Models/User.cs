using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;

namespace API.Core.Models
{
    public class User
    {
        public int Id { get; set; }
        [Required]
        [StringLength(50)]
        public string Name { get; set; }
        [Required]
        [StringLength(50)]
        public string Email { get; set; }
        [StringLength(30)]
        public string ContactNo { get; set; }
        [StringLength(30)]
        public string OtherContactNo { get; set; }
        [Required]
        public byte[] PasswordHash { get; set; }
        [Required]
        public byte[] PasswordSalt { get; set; }
        public DateTime DateRegistered { get; set; }
        // public Photo Photo { get; set; }
        public Role Role { get; set; }
        public bool Enabled { get; set; }
        public bool EmailVerified { get; set; }
        public bool VerifiedAsMerchant { get; set; }
        public string Facebook { get; set; }
        public string Instagram { get; set; }
        public string Twitter { get; set; }
        public string Whatsapp { get; set; }
        public string LinkedIn { get; set; }
        public ICollection<Space> Spaces { get; set; }
        public ICollection<Booking> Bookings { get; set; }
        public ICollection<Enquiry> Enquiries { get; set; }
        public User()
        {
            this.Spaces = new Collection<Space>();
            this.Bookings = new Collection<Booking>();
            this.Enquiries = new Collection<Enquiry>();
        }

    }
}