using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LibraryManagementSystem.Models.Entity;

namespace LibraryManagementSystem.Controllers
{
	public class PersonelController : Controller
	{
		// model üzerinden db isminde bir nesne türetme işlemi 
		DBKUTUPHANEEntities db = new DBKUTUPHANEEntities();
		public ActionResult Index()
		{
			var degerler = db.TBLPERSONELs.ToList();
			return View(degerler);
		}
		[HttpGet]       // boş kayıt eklemeden listeleme yapar
		public ActionResult YeniPersonelEkle()
		{
			return View();
		}
		[HttpPost]      // kullanıcı yeni ekleme işlemi yaptığında listeleme yapar
		public ActionResult YeniPersonelEkle(TBLPERSONEL parametre)
		{
			if (!ModelState.IsValid)
			{
				return View("YeniPersonelEkle");
			}
			db.TBLPERSONELs.Add(parametre);
			db.SaveChanges();
			return RedirectToAction("Index");
		}

		// ilgili personeli listeden silmek için
		public ActionResult PersonelSil(int id)
		{
			var personel = db.TBLPERSONELs.Find(id);
			db.TBLPERSONELs.Remove(personel);
			db.SaveChanges();
			return RedirectToAction("Index");
		}

		// ilgili bilgileri id değerine göre detay sayfasına taşıma işlemi
		public ActionResult PersonelGetir(int id)
		{
			var personel = db.TBLPERSONELs.Find(id);
			return View("PersonelGetir", personel);
		}

		// güncelleme işlemi için
		public ActionResult PersonelGuncelle(TBLPERSONEL parametre)
		{
			var personel = db.TBLPERSONELs.Find(parametre.ID);  // id değerine göre güncelleme yapmak için hafızaya alır
			personel.PERSONEL = parametre.PERSONEL;            // personel değerini kullanıcı parametresinden gelen değere göre denkler
			db.SaveChanges();
			return RedirectToAction("Index");
		}
	}
}