namespace EShop.Email.Models
{
    public class EmailLog
    {
        public int Id { get; set; }
        required public string Email { get; set; }
        public string Log { get; set; }
        public DateTime EmailSent { get; set; }
    }
}
