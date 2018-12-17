
namespace Domain.Entities
{
    public class CandidatesBulletinBoards
    {
        public int BulletinBoardId { set; get; }
        public BulletinBoard BulletinBoard { get; set; }

        public string CandidateId { get; set; }
        public Candidate Candidate { get; set; }
    }
}
