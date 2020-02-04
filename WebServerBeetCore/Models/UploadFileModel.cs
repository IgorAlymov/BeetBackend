using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebServerBeetCore.Models
{
    public class UploadFileModel
    { 
        public IFormFile File { get; set; }
    }
}
