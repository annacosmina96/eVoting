

namespace Domain.Entities
{
    public class Ballot
    {
        public string VoterId { get; set; }
        public int BulletinBoardId { get; set; }

        public string Values { get; set; }
        public User Voter { get; set; }
        public BulletinBoard BulletinBoard { get; set; }

      
    }
}