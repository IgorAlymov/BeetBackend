using System;
using System.Collections.Generic;
using System.Text;

namespace BeetAPI.Domain
{
    public class Message
    {
        public int MessageId { get; set; }
        public string Text { get; set; }
        public bool WasReaded { get; set; }

        public  ICollection<Photo> AttachedPhoto { get; set; }
        public  SocialUser Author { get; set; }
        public  SocialUser Receiver { get; set; }
    }
}
