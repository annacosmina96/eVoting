using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace Domain.Entities
{
    public class User : IdentityUser
    { 
        public string CNP { get; set; }
        public string ElGamal_1 { get; set; }
        public string ElGamal_2 { get; set; }
        public List<Ballot> Votes { get; set; }

        public User()
        {
            Votes = new List<Ballot>();
        }


    }
}
