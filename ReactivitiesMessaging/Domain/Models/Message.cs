namespace Domain.Models
{
    public partial class Message
    {
        public int Id { get; set; }
        public int SenderId { get; set; }
        public int ReceiverId { get; set; }
        public string? Content { get; set; }
        public DateTime DateSent { get; set; }

        public virtual Profile Receiver { get; set; } = null!;
        public virtual Profile Sender { get; set; } = null!;
    }
}
