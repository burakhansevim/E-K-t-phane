using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LibraryManagementSystem.Models.Entity;

namespace LibraryManagementSystem.Controllers
{
    public class DuyuruController : Controller
    {
        // model üzerinden db isminde bir nesne türetme işlemi 
        DBKUTUPHANEEntities db = new DBKUTUPHANEEntities();

        // duyuruları listeleme işlemi
        public ActionResult Index()
        {
            var duyurular = db.TBLDUYURULARs.ToList();
            return View(duyurular);
        }

        // veritabanına yeni kayıt ekleme işlemi yapmadan listelemesi için
        [HttpGet]
        public ActionResult YeniDuyuruEkle()
        {
            return View();
        }

        // kullanıcı yeni duyuru ekleme işlemi
        [HttpPost]
        public ActionResult YeniDuyuruEkle(TBLDUYURULAR parametre)
        {
            db.TBLDUYURULARs.Add(parametre);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        // duyuru silme işlemi
        public ActionResult DuyuruSil(int id)
		{
            var duyuru = db.TBLDUYURULARs.Find(id);
            db.TBLDUYURULARs.Remove(duyuru);
            db.SaveChanges();
            return RedirectToAction("Index");
		}

        // duyuru sayfasından detay sayfasına erişim işlemi
        public ActionResult DuyuruDetay(TBLDUYURULAR parametre)
		{
            var duyuru = db.TBLDUYURULARs.Find(parametre.ID);
            return View("DuyuruDetay",duyuru);
		}

        // duyuru verileri güncelleme işlemi
        public ActionResult DuyuruGuncelle(TBLDUYURULAR parametre)
		{
            var duyuru = db.TBLDUYURULARs.Find(parametre.ID);   // id değerine göre güncellenecek duyuru verisini duyuru değişkeninde tutar
            duyuru.KATEGORI = parametre.KATEGORI;               // ilgili alanları kullanıcı parametresinden gelen değerle eşler
            duyuru.ICERIK = parametre.ICERIK;
            duyuru.TARIH = parametre.TARIH;
            db.SaveChanges();                                   // değişiklikleri kaydeder
            return RedirectToAction("Index");                   // kullanıcıyı index sayfasına tekrar yönlendirir
		}
    }
}