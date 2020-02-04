using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using WebServerSocialNet.Domains;

namespace WebServerSocialNet.Helpers
{
    /// <summary>
    /// Класс для наполнения
    /// Базы тестовыми данными.
    /// </summary>
    public class DbSeed
    {
        private ModelSocialNetContainer _db;
        private Random _rnd;

        public DbSeed()
        {
            _db = new ModelSocialNetContainer();
            _rnd = new Random();
        }

        public string RandomText(int wordCountMin, 
                                 int wordCountMax)
        {
            int count = _rnd.Next(wordCountMin, wordCountMax);
            string text = 
                "привет пока дела работа отдых учеба друзья сегодня завтра";

            string[] words = text.Split();
            var sb = new StringBuilder();
            for (int i = 0; i < count; i++)
            {
                int index = _rnd.Next(0, words.Length);
                string word = words[index];
                sb.Append(" " + word);
            }
            return sb.ToString();
        }

        public void Initialize()
        {
            string[] images =
            {
                "carlson.png",
                "leopold.png",
                "piggy.png",
                "pooh.png",
            };

            string[] namesCities =
            {
                "Челябинск",
                "Екатеринбург",
                "Москва"
            };
            
            _db.SaveChanges();

            string[] massPosts =
            Enumerable
                .Range(1, images.Length)
                .Select(x => RandomText(4, 8))
                .ToArray();

            var quePosts = new Queue<string>(massPosts);
            

            // пользователи, фотки, посты
            foreach (var name in images)
            {
                string userName = name.Replace(".png", "")
                                      .Replace(".jpg", "");

                var photo = new Photo()
                {
                    Description = "картинка",
                    Filename = name,
                    Name = userName
                };
                _db.Photos.Add(photo);
                
                var user = new SocialUser()
                {
                    Birthday = DateTime.Now.AddYears(-30),
                    Email = userName+"@"+"ya.ru",
                    Firstname = userName,
                    Lastname = "Союзмультфильм"
                };

                _db.SocialUsers.Add(user);
                //user.UserPhotos.Add(photo);

                var post = new Post()
                {
                    Author = user,
                    Date = DateTime.Now.AddDays(-10),
                    Text = quePosts.Dequeue()
                };
                post.AttachedPhotos.Add(photo);
                _db.Posts.Add(post);


                _db.SaveChanges();
                user.AvatarPhotoId = photo.PhotoId;
            }

            // комменты
            var posts = _db.Posts.ToList();
            var users = _db.SocialUsers.ToList();

            foreach (var p in posts)
            {
                for (int i = 0; i < 2; i++)
                {
                    var comment = new Comment()
                    {
                        Post = p,
                        Text = RandomText(2,6),
                        Date = DateTime.Now.AddHours(-10),
                        Author = users[_rnd.Next(0,users.Count)]
                    };
                    _db.Comments.Add(comment);
                }
            }
            _db.SaveChanges();

            #region Создание сообщений
            for (int i = 0; i < users.Count; i++)
            {
                for (int j = 0; j < users.Count; j++)
                {
                    if (i != j)
                    {
                        int n = _rnd.Next(1, 4);
                        for (int k = 0; k < n; k++)
                        {
                            string msgText = RandomText(4,8);
                            var msg = new Message()
                            {
                                Text = msgText,
                                Author = users[i],
                                Receiver = users[j]
                            };
                            _db.Messages.Add(msg);
                        }
                    }
                }
            }
            #endregion

            _db.SaveChanges();

        }
    }
}