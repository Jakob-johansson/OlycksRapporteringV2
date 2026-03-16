using OlycksRapporteringV2.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace OlycksRapporteringV2.Domain.Entities
{
    public class User
    {
        public string Id { get; set; }

        public string EmployeeId { get; set; }
        public string PasswordHash { get; set; }
        public string Email { get; set; }
       

        public UserRole Role { get; set; }
        public DateTime CreatedAt { get; set; }
        public string SocialSecurityNumber { get; set; }
        public InsuranceCompany Insurance { get; set; }
        public string PersonalBank { get; set; }
        public string PhoneNumber { get; set; }
        public string ContactPerson1 { get; set; }
        public string ContactPerson1PhoneNumber { get; set; }
        public string ContactPerson2 { get; set; }
        public string ContactPerson2PhoneNumber { get; set; }
        public string HomeAdress { get; set; }



    }
}
