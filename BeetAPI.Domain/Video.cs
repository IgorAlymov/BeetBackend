using System;
using System.Collections.Generic;
using System.Text;

namespace BeetAPI.Domain
{
    public class Video
    {
        public int VideoId { get; set; }
        public string Name { get; set; }
        public string Path { get; set; }
        public SocialUser VideoUser { get; set; }
        public int ViewCounter { get; set; }
        public DateTime Date { get; set; } 
    }
}
