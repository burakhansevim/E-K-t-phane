using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LibraryManagementSystem.Models.Entity;

namespace LibraryManagementSystem.Controllers
{
	public class KategoriController : Controller
	{
		// model üzerinden db isminde bir nesne türetme işlemi 
		DBKUTUPHANEEntities db = new DBKUTUPHANEEntities();

		public ActionResult Index()
		{
			//var degerler = db.TBLKATEGORIs.Where(a => a.DURUM == true).ToList();  // kullanılabilir(emanet edilebilir) kitapları listeler
			var degerler = db.TBLKATEGORIs.ToList();
			return View(degerler);
		}

		// kitap kategorilerini yeni ekleme yapmadan listelemesi için
		[HttpGet]
		public ActionResult YeniKategoriEkle()
		{
			return View();
		}

		// yeni kitap kategorisi eklemek için 
		[HttpPost]
		public ActionResult YeniKategoriEkle(TBLKATEGORI parametre)
		{
			db.TBLKATEGORIs.Add(parametre);
			db.SaveChanges();
			return RedirectToAction("Index");
		}

		// kitap kategorisini listeden silmek için
		public ActionResult KategoriSil(int id)
		{
			var kategori = db.TBLKATEGORIs.Find(id);
			//db.TBLKATEGORIs.Remove(kategori);
			kategori.DURUM = false; // ilgili kategoriyi silmek yerine durumu pasif hale getirildi
			db.SaveChanges();
			return RedirectToAction("Index");
		}

		// ilgili kitap bilgilerini id değerine göre detay sayfasına taşıma işlemi
		public ActionResult KategoriGetir(int id)
		{
			var kategori = db.TBLKATEGORIs.Find(id);
			return View("KategoriGetir", kategori);
		}

		// güncelleme işlemi için
		public ActionResult KategoriGuncelle(TBLKATEGORI parametre)
		{
			var kategori = db.TBLKATEGORIs.Find(parametre.ID);  // id değerine göre güncelleme yapmak için hafızaya alır
			kategori.AD = parametre.AD;                         // kategori ad değerini kullanıcı parametresinden gelen değere göre denkler
			db.SaveChanges();
			return RedirectToAction("Index");
		}
	}
}