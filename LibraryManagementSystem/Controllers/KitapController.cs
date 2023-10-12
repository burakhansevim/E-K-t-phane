using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LibraryManagementSystem.Models.Entity;

namespace LibraryManagementSystem.Controllers
{
	public class KitapController : Controller
	{
		DBKUTUPHANEEntities db = new DBKUTUPHANEEntities();
		public ActionResult Index(string parametre)
		{
			var kitaplar = from x in db.TBLKITAPs
						   select x;

			if (!string.IsNullOrEmpty(parametre))
			{
				kitaplar = kitaplar.Where(y => y.AD.Contains(parametre));
			}
			//var kitaplar = db.TBLKITAPs.ToList();
			return View(kitaplar.ToList());
		}
		[HttpGet]
		public ActionResult YeniKitapEkle()
		{
			List<SelectListItem> deger1 = (from x in db.TBLKATEGORIs.ToList()    //  sorgu biçimli LINQ 
										   select new SelectListItem
										   {
											   Text = x.AD,              // display member   
											   Value = x.ID.ToString()   // value member
										   }).ToList();
			ViewBag.valueFocused = deger1;

			List<SelectListItem> deger2 = (from y in db.TBLYAZARs.ToList()
										   select new SelectListItem
										   {
											   Text = y.AD + ' ' + y.SOYAD,
											   Value = y.ID.ToString()
										   }).ToList();
			ViewBag.valueFocused2 = deger2;

			return View();
		}

		// gelen parametreyle sisteme yeni kitap ekleme işlemi
		[HttpPost]
		public ActionResult YeniKitapEkle(TBLKITAP parametre)
		{
			var kitap1 = db.TBLKATEGORIs.Where(kitap => kitap.ID == parametre.TBLKATEGORI.ID).FirstOrDefault();  // method biçimli LINQ
			var yazar1 = db.TBLYAZARs.Where(yazar => yazar.ID == parametre.TBLYAZAR.ID).FirstOrDefault();
			parametre.TBLKATEGORI = kitap1;
			parametre.TBLYAZAR = yazar1;
			db.TBLKITAPs.Add(parametre);
			db.SaveChanges();
			return RedirectToAction("Index");
		}

		// kitap silme işlemi

		public ActionResult KitapSil(int id)
		{
			var kitap = db.TBLKITAPs.Find(id);  // id değerine göre silinecek kitabı bulur
			db.TBLKITAPs.Remove(kitap);			// ilgili kitabı sistemden siler
			db.SaveChanges();					// değişikliği model tarafına kaydeder
			return RedirectToAction("Index");	// Index sayfasına geri döner
		}

		// kitap bilgilerini sayfaya taşıma işlemi

		public ActionResult KitapGetir(int id)
		{
			var kitap = db.TBLKITAPs.Find(id);
			List<SelectListItem> deger3 = (from x in db.TBLKATEGORIs.ToList()    //  sorgu biçimli LINQ 
										   select new SelectListItem
										   {
											   Text = x.AD,              // display member   
											   Value = x.ID.ToString()   // value member
										   }).ToList();
			ViewBag.valueFocused3 = deger3;

			List<SelectListItem> deger4 = (from y in db.TBLYAZARs.ToList()
										   select new SelectListItem
										   {
											   Text = y.AD + ' ' + y.SOYAD,
											   Value = y.ID.ToString()
										   }).ToList();
			ViewBag.valueFocused4 = deger4;

			return View("KitapGetir", kitap);
		}


		// kitap güncelleme işlemi

		public ActionResult KitapGuncelle(TBLKITAP parametre)       // modeldeki TBLKITAP tablosundan parametre türetme işlemi
		{
			var kitap = db.TBLKITAPs.Find(parametre.ID);		// id değerine göre güncellenecek kitap bilgilerini tutar
			kitap.AD = parametre.AD;                            // mevcut kitap bilgileri ile kullanıcının değişikliklerini denkler
			kitap.BASIMYIL = parametre.BASIMYIL;
			kitap.SAYFA = parametre.SAYFA;
			kitap.YAYINEVI = parametre.YAYINEVI;
			kitap.KITAPRESIM = parametre.KITAPRESIM;
			kitap.DURUM = true;
			var kategori1 = db.TBLKATEGORIs.Where(kategori => kategori.ID == parametre.TBLKATEGORI.ID).FirstOrDefault(); // method biçimli LINQ
			var yazar1 = db.TBLYAZARs.Where(yazar => yazar.ID == parametre.TBLYAZAR.ID).FirstOrDefault();
			kitap.KATEGORI = kategori1.ID;
			kitap.YAZAR = yazar1.ID;
			db.SaveChanges();
			return RedirectToAction("Index");
		}
	}
}