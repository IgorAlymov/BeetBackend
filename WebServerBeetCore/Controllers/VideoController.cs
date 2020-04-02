using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using BeetAPI.DataAccessLayer;
using BeetAPI.Domain;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebServerBeetCore.Models;

namespace WebServerBeetCore.Controllers
{
    [Route("api")]
    [ApiController]
    public class VideoController : ControllerBase
    {
        SocialUserRepository _dbUser;
        VideoRepository _dbVideo;
        IHostingEnvironment _appEnvironment;

        public VideoController(SocialUserRepository rep,
            VideoRepository videoRepository,
            IHostingEnvironment appEnvironment)
        {
            _dbUser = rep;
            _dbVideo = videoRepository;
            _appEnvironment = appEnvironment;

        }

        [HttpPost("AddVideo")]
        [RequestSizeLimit(400000000)]
        public async Task<IActionResult> AddVideo([FromForm]UploadFileModel model)
        {
            if (!ModelState.IsValid) return BadRequest();
            string emailUser = User.Identity.Name;
            var lastUser = _dbUser.Get(emailUser);
            try
            {
                string path = Path.Combine("Videos", model.File.FileName);
                using (var fileStream = new FileStream(Path.Combine(_appEnvironment.WebRootPath, path), FileMode.Create))
                {
                    await model.File.CopyToAsync(fileStream);
                    fileStream.Close();
                }

                Video video = new Video
                {
                    Name = model.File.FileName,
                    Path = "http://localhost:5001/" + path,
                    VideoUser = lastUser,
                    Date = DateTime.Now,
                    ViewCounter = 0
                };
                _dbVideo.Create(video);
                _dbVideo.Save();

                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest();
            }
        }

        [HttpGet("GetAllVideo/{id}")]
        public IEnumerable<Video> GetAllVideo(int id)
        {
            var video = _dbVideo.GetVideoUser(id);
            return video;
        }

        [HttpPut("AddViewVideo/{id}")]
        public IActionResult AddViewVideo(int id)
        {
            var video = _dbVideo.Get(id);
            video.ViewCounter++;
            _dbVideo.Update(video);
            _dbVideo.Save();
            return Ok();
        }
           
        [HttpDelete("DeleteVideo/{id}")]
        public IActionResult DeleteVideo(int id)
        {
            _dbVideo.Delete(id);
            _dbVideo.Save();
            return Ok();
        }
    }
}
