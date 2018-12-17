namespace Domain.Entities
{
    public class TallyAuthority
    {
        public int TallyAuthorityId { get; set; }
        public int G { get; set; }
        public int Q { get; set; }
        private string privateKey { get; set; }
        public string PublicKey { get; set; }


    }
}
