using System.ComponentModel.DataAnnotations;

namespace Domain.Models
{
    public partial class Message
    {
        private Message()
        {
        }

        private Message(Profile sender, Profile receiver, string content, DateTime dateSent)
        {
            this.Sender = sender;
            this.Receiver = receiver;
            this.Content = content;
            this.DateSent = dateSent;
        }

        public int Id { get; set; }

        public int SenderId { get; private set; }

        public Profile Sender { get; private set; }

        public int ReceiverId { get; private set; }

        public Profile Receiver { get; private set; }

        public string Content { get; private set; }

        public DateTime DateSent { get; private set; }

        public static Message New(Profile sender, Profile receiver, string content, DateTime dateSent)
        {
            if (string.IsNullOrEmpty(content))
            {
                throw new ArgumentException(content);
            }

            GuardAgainstInvalidUsername(sender);

            GuardAgainstInvalidUsername(receiver);

            return new Message(sender, receiver, content, dateSent);
        }


        private static void GuardAgainstInvalidUsername(Profile sender)
        {
            if (sender == null)
            {
                throw new ValidationException($"Invalid sender profile!");
            }
        }
    }
}