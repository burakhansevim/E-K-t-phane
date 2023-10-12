using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LibraryManagementSystem.Models.Entity;

namespace LibraryManagementSystem.Controllers
{
	public class YazarController : Controller
	{
		// model üzerinden db isminde bir nesne türetme işlemi 
		DBKUTUPHANEEntities db = new DBKUTUPHANEEntities();

		public ActionResult Index()
		{
			var degerler = db.TBLYAZARs.ToList();
			return View(degerler);
		}

		// yazar kategorisinde yeni ekleme yapmadan listelemesi için
		[HttpGet]
		public ActionResult YeniYazarEkle()
		{
			return View();
		}

		// yeni yazar ekleme işlemi için
		[HttpPost]
		public ActionResult YeniYazarEkle(TBLYAZAR parametre)
		{
			if (!ModelState.IsValid)  // eğer modeldeki data annotation şartına uymuyorsa kullanıcıyı tekrar ekleme sayfasına yönlendirir
			{
				return View("YeniYazarEkle");
			}
			else
			{
				db.TBLYAZARs.Add(parametre);
				db.SaveChanges();
				return RedirectToAction("Index");
			}
		}

		// yazar silme işlemi için
		public ActionResult YazarSil(int id)
		{
			var yazar = db.TBLYAZARs.Find(id);
			db.TBLYAZARs.Remove(yazar);
			db.SaveChanges();
			return RedirectToAction("Index");
		}

		// ilgili yazar bilgilerini id değerine göre detay sayfasına taşıma işlemi
		public ActionResult YazarGetir(int id)
		{
			var yazar = db.TBLYAZARs.Find(id);
			return View("YazarGetir", yazar);
		}


		// yazar güncelle işlemi
		public ActionResult YazarGuncelle(TBLYAZAR parametre)  // YazarGuncelle methodu dışardan tablo bazlı parametre alır
		{
			var yazar = db.TBLYAZARs.Find(parametre.ID);
			/*yazar bilgilerindeki değerleri kullanıcı parametrelerinden gelen bilgilerle denkler*/
			yazar.AD = parametre.AD;
			yazar.SOYAD = parametre.SOYAD;
			yazar.DETAY = parametre.DETAY;
			db.SaveChanges();
			return RedirectToAction("Index");
		}

		public ActionResult YazaraAitKitaplar(int id)
		{
			var yazar = db.TBLKITAPs.Where(c => c.YAZAR == id).ToList(); // modeldeki TBLKITAP tablosundaki yazara ait kitapları ilgili yazar için listeler
			var yazarAd = db.TBLYAZARs.Where(d => d.ID == id).Select(e => e.AD + " " + e.SOYAD).FirstOrDefault();
			ViewBag.yazar1 = yazarAd;
			return View(yazar);		// dönüş değeri olarak oluşturulan yazar değişkenindeki değeri verir
		}
	}
}