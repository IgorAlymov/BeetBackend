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
    public class MusicController : ControllerBase
    {
        SocialUserRepository _dbUser;
        MusicRepository _dbMusic;
        IHostingEnvironment _appEnvironment;

        public MusicController(SocialUserRepository rep,
            MusicRepository musicRepository,
            IHostingEnvironment appEnvironment)
        {
            _dbUser = rep;
            _dbMusic = musicRepository;
            _appEnvironment = appEnvironment;
        }

        [HttpPost("AddMusic")]
        public async Task<IActionResult> AddMusic([FromForm]UploadFileModel model)
        {
            if (!ModelState.IsValid) return BadRequest();
            string emailUser = User.Identity.Name;
            var lastUser = _dbUser.Get(emailUser);
            try
            {
                string path = Path.Combine("Music", model.File.FileName);
                using (var fileStream = new FileStream(Path.Combine(_appEnvironment.WebRootPath, path), FileMode.Create))
                {
                    await model.File.CopyToAsync(fileStream);
                    fileStream.Close();
                }

                Music music = new Music
                {
                    Name = model.File.FileName,
                    Path = "http://localhost:5001/" + path,
                    MusicUser = lastUser
                };
                _dbMusic.Create(music);
                _dbMusic.Save();

                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest();
            }
        }

        [HttpGet("GetAllMusic/{id}")]
        public IEnumerable<Music> GetAllMusic(int id)
        {
            return _dbMusic.GetMusicUser(id);
        }

        [HttpDelete("DeleteMusic/{id}")]
        public IActionResult DeleteMusic(int id)
        {
            _dbMusic.Delete(id);
            _dbMusic.Save();
            return Ok();
        }
    }
}
