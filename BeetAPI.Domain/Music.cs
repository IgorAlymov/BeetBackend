using System;
using System.Collections.Generic;
using System.Text;

namespace BeetAPI.Domain
{
    public class Music
    {
        public int MusicId { get; set; }
        public string Name { get; set; }
        public string Path { get; set; }
        public SocialUser MusicUser { get; set; }
    }
}
