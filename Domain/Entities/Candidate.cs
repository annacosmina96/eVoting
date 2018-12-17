using System.Collections.Generic;

namespace Domain.Entities
{
    public class Candidate : User
    {
        public string LastNume { get; set; }
        public string FirstNume { get; set; }

        public List<CandidatesBulletinBoards> CandidatesBulletinBoards { get; set; }

        public Candidate()
        {
            CandidatesBulletinBoards = new List<CandidatesBulletinBoards>();
        }
    }
}

