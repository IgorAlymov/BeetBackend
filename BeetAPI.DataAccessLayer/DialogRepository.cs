using BeetAPI.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BeetAPI.DataAccessLayer
{
    public class DialogRepository
    {
        AppDbContext db;

        public DialogRepository(AppDbContext appDbContext)
        {
            db = appDbContext;
        }

        public void Create(Dialog dialog)
        {
            db.Dialogs.Add(dialog);
        }

        public IEnumerable<Dialog> Get()
        {
            return db.Dialogs;
        }

        public IEnumerable<Dialog> GetMyDialogs(int id)
        {
            return db.Dialogs.Where(m => m.Author == id || m.Reciver == id);
        }

        public Dialog Get(int id)
        {
            return db.Dialogs.Include(m=>m.Messages).SingleOrDefault(p=>p.DialogId == id);
        }

        public Dialog GetActiveDialog(int idR,int idA)
        {
            return db.Dialogs.Where(d => d.Author == idA && d.Reciver==idR || d.Author == idR && d.Reciver == idA).FirstOrDefault();
        }

        public Dialog Get(int idR, int idA)
        {
            return db.Dialogs.Where(d => d.Author == idA && d.Reciver == idR || d.Author == idR && d.Reciver == idA)
                .Include(m=>m.Messages)
                .FirstOrDefault();
        }

        public void Update(Dialog dialogs)
        {
            db.Entry(dialogs).State = EntityState.Modified;
        }

        public void Delete(int id)
        {
            Dialog dialog = db.Dialogs.Find(id);
            if (dialog != null)
                db.Dialogs.Remove(dialog);
        }

        public void Delete(Dialog dialog)
        {
            db.Dialogs.Remove(dialog);
        }

        public void Save()
        {
            db.SaveChanges();
        }
    }
}
