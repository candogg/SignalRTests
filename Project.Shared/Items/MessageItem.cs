namespace Project.Shared.Items
{
    public class MessageItem
    {
        public string From { get; set; } = null!;
        public string ConnectionId { get; set; } = null!;
        public string Message { get; set; } = null!;

        public override string ToString()
        {
            return $"{From}: {Message}";
        }
    }
}
