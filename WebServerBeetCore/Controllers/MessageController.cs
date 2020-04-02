using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BeetAPI.DataAccessLayer;
using BeetAPI.Domain;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebServerBeetCore.Controllers
{
    [Route("api")]
    [ApiController]
    public class MessageController : ControllerBase
    {
        MessageRepository _dbMessage;
        DialogRepository _dbDialog;
        SocialUserRepository _dbUser;
        PhotoRepository _dbPhoto;

        public MessageController(MessageRepository messageRepository,
                                 SocialUserRepository repUser,
                                 DialogRepository dialogRepository,
                                 PhotoRepository photoRepository)
        {
            _dbMessage = messageRepository;
            _dbDialog = dialogRepository;
            _dbUser = repUser;
            _dbPhoto = photoRepository;
        }
        
        [HttpPost("SendMessage")]
        public IActionResult SendMessage([FromBody]Message message)
        {
            var author = _dbUser.Get(message.Author);
            var avatarAuthor = new Photo();
            if (author.AvatarPhotoId == null)
            {
                avatarAuthor.Path = "Photos/noAvatar.png";
            }
            else
            {
                avatarAuthor = _dbPhoto.GetAvatar((int)author.AvatarPhotoId);
                if (avatarAuthor == null)
                {
                    avatarAuthor = new Photo();
                    avatarAuthor.Path = "Photos/noAvatar.png";
                }
            }
            var dialogActive = _dbDialog.GetActiveDialog(message.Author, message.Reciver);
            if (dialogActive == null)
            {
                message.Name = author.Firstname + " " + author.Lastname;
                message.Avatar = "http://localhost:5001/" + avatarAuthor.Path;
                message.Date = DateTime.Now;
                Dialog dialog = new Dialog()
                {
                    Author = message.Author,
                    Date=DateTime.Now,
                    LastMessage=message.Text,
                    Reciver=message.Reciver,
                    ReadAuthor=true,
                    ReadReciver=false
                };
                dialog.Messages.Add(message);
                _dbDialog.Create(dialog);
                message.DialogId = dialog.DialogId;
                _dbMessage.Create(message);
                _dbMessage.Save();
            }
            else
            {
                message.Name = author.Firstname + " " + author.Lastname;
                message.Avatar = "http://localhost:5001/" + avatarAuthor.Path;
                message.Date = DateTime.Now;
                Dialog dialog = _dbDialog.Get(dialogActive.DialogId);
                dialog.Date = DateTime.Now;
                dialog.LastMessage = message.Text;

                if (dialog.Author == message.Author)
                {
                    dialog.ReadReciver = false;
                    dialog.ReadAuthor = true;
                }
                else
                {
                    dialog.ReadAuthor = false;
                    dialog.ReadReciver = true;
                }
                    
                dialog.Messages.Add(message);
                _dbDialog.Update(dialog);
                _dbMessage.Create(message);
                _dbMessage.Save();
            }
            return Ok();
        }

        [HttpGet("GetMyDialogs")]
        public IEnumerable<Dialog> GetMyDialogs ()
        {
            string emailUser = User.Identity.Name;
            var user = _dbUser.Get(emailUser);
            List<Dialog> dialogs=null;
            if (user!=null)
                 dialogs= _dbDialog.GetMyDialogs(user.SocialUserId).ToList();
            return dialogs;
        }

        [HttpGet("DeleteDialog/{id}")]
        public IActionResult DeleteDialog(int id)
        {
            var dialog = _dbDialog.Get(id);
            var messages = dialog.Messages;
            foreach (var message in messages)
            {
                _dbMessage.Delete(message);
            }
            _dbDialog.Delete(dialog);
            _dbDialog.Save();
            return Ok();
        }

        [HttpGet("GetDialog/{id}")]
        public Dialog GetDialog(int id)
        {
            string emailUser = User.Identity.Name;
            var user = _dbUser.Get(emailUser);
            var dialog = _dbDialog.GetActiveDialog(id,user.SocialUserId);
            return dialog;
        }

        [HttpGet("GetMessages/{id}")]
        public IEnumerable<Message> GetMessages (int id)
        {
            string emailUser = User.Identity.Name;
            var user = _dbUser.Get(emailUser);
            var dialog = _dbDialog.Get(id, user.SocialUserId);

            if (dialog != null && dialog.Author == user.SocialUserId)
            {
                dialog.ReadAuthor = true;
                _dbDialog.Update(dialog);
                _dbDialog.Save();
            }
            else if(dialog != null)
            {
                dialog.ReadReciver = true;
                _dbDialog.Update(dialog);
                _dbDialog.Save();
            }
            
            if (dialog!=null)
            {
                var messages = dialog.Messages;
                return messages;
            }
            else
                return null;
        }
    }
}
