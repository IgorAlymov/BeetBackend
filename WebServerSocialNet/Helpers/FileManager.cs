using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace WebServerSocialNet.Helpers
{
    public class FileManager
    {
        private static string _imagesPath;

        public static string ImagesPath
        {
            get
            {
                if (_imagesPath == null)
                {
                    _imagesPath = 
                    HttpContext
                        .Current
                        .Server
                        .MapPath(@"~\Images");
                }
                return _imagesPath;
            }
        }

        public static byte[] GetPhoto(string fileName)
        {
            string filePath = Path.Combine(ImagesPath, fileName);
            byte[] bytes = null;

            try
            {
                // using - приказывает системе после работы сразу освободить ресурсы
                using (var fs = new FileStream(filePath, FileMode.Open))
                {
                    bytes = new byte[fs.Length];
                    fs.Read(bytes, 0, (int)fs.Length);
                }
            }
            catch(Exception ex)
            {
                // добавить логгер
                Console.WriteLine(ex.ToString());
            }

            return bytes;
        }

        public static string SavePhoto(HttpPostedFile f)
        {
            string fnameSource = Path.GetFileName(f.FileName);
            string fnameLocal =
                Path.Combine(ImagesPath, fnameSource);

            f.SaveAs(fnameLocal);

            return fnameSource;
        }
    }
}