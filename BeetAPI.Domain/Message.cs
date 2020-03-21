using System;
using System.Collections.Generic;
using System.Text;

namespace BeetAPI.Domain
{
    public class Message
    {
        public int MessageId { get; set; }
        public int? DialogId { get; set; }
        public DateTime Date { get; set; }
        public int Author { get; set; }
        public int Reciver { get; set; }
        public string Text { get; set; }
        public string Name { get; set; }
        public string Avatar { get; set; }
    }
}
