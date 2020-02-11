using System;
using System.Collections.Generic;
using System.Text;

namespace BeetAPI.Domain
{
    public class Photo
    {
        public int PhotoId { get; set; }
        public string Name { get; set; }
        public string Path { get; set; }
        public  SocialUser PhotoUsers { get; set; }
        public  Post PostsWithPhoto { get; set; }
        public  Message Message { get; set; }
    }
}
