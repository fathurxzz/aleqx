namespace iBank.DataAccess.Entities
{
    public class OtpSms
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public string Password { get; set; }
        public System.DateTime ExpireDate { get; set; }
        public int TokenId { get; set; }

        public virtual Token Token { get; set; }
    }
}
