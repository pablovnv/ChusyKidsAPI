using System.Collections.Generic;

namespace ChusyKidsAPI.Models
{
    public class SendModel
    {
        public List<SendMessage> FulfillmentMessagesList { get; set; }
    }

    public class SendMessage
    {
        public SendText Text { get; set; }
    }

    public class SendText
    {
        public List<string> TextList { get; set; }
    }
}
