using System;
using System.Collections.Generic;

namespace Domain.Entities
{
    public class BulletinBoard
    {
        public int BulletinBoardId { get; set; }
        public DateTime StartCampaign { get; set; }
        public DateTime StopCampaign { get; set; }
        public List<Ballot> Ballots { get; set; }
        public List<CandidatesBulletinBoards> CandidatesBulletinBoards { get; set; }


        public BulletinBoard()
        {
            CandidatesBulletinBoards = new List<CandidatesBulletinBoards>();
            Ballots = new List<Ballot>();
        }
    }
}