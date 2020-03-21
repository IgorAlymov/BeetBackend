using System;
using System.Collections.Generic;
using System.Text;

namespace BeetAPI.Domain
{
    public class Dialog
    {
        public int DialogId { get; set; }
        public string Avatar { get; set; }
        public string Name { get; set; }
        public string LastMessage { get; set; }
        public DateTime Date { get; set; }
        public int Author { get; set; }
        public int Reciver { get; set; }
        public bool ReadAuthor { get; set; }
        public bool ReadReciver { get; set; }
        public ICollection<Message> Messages { get; set; }
        public Dialog()
        {
            Messages = new List<Message>();
        }
    }
}
