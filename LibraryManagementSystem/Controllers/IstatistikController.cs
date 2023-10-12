using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LibraryManagementSystem.Models.Entity;

namespace LibraryManagementSystem.Controllers
{
	public class IstatistikController : Controller
	{
		// model üzerinden db isminde bir nesne türetme işlemi 
		DBKUTUPHANEEntities db = new DBKUTUPHANEEntities();

		public ActionResult Index()
		{
			var value1 = db.TBLUYELERs.Count();									// sistemde kayıtlı toplam üye sayısı
			var value2 = db.TBLKITAPs.Count();									// sistemde kayıtlı toplam kitap sayısı	
			var value3 = db.TBLKITAPs.Where(emanet => emanet.DURUM == false).Count();   // sistemde kayıtlı emanet olarak verilen kitap sayısı
			var value4 = db.TBLCEZALARs.Sum(ceza => ceza.PARA);
			ViewBag.viewValue1 = value1;
			ViewBag.viewValue2 = value2;
			ViewBag.viewValue3 = value3;
			ViewBag.viewValue4 = value4;
			return View();
		}

		public ActionResult HavaDurumu()
		{
			return View();
		}

		public ActionResult HavaKart()
		{
			return View();
		}

		public ActionResult Galeri()
		{
			return View();
		}

		[HttpPost]
		public ActionResult ResimYukle(HttpPostedFileBase file)
		{
			if (file.ContentLength > 0)
			{
				string filePath = Path.Combine(Server.MapPath("~/web2/resimler"), Path.GetFileName(file.FileName));
				file.SaveAs(filePath);
			}
			return RedirectToAction("Galeri");
		}

		public ActionResult LinqKart()		
		{	// method biçimli LINQ sorguları ile veritabanından veri çekme işlemi
			var value1 = db.TBLKITAPs.Count();
			var value2 = db.TBLUYELERs.Count();
			var value3 = db.TBLCEZALARs.Sum(kasa => kasa.PARA);
			var value4 = db.TBLKITAPs.Where(kitap => kitap.DURUM == false).Count();
			var value5 = db.TBLKATEGORIs.Count();
			var value8 = db.MOSTWRITER().FirstOrDefault();  // db tarafında oluşturulan stored procedure modelden projeye dahil edildi
			var value9 = db.TBLKITAPs.GroupBy(a => a.YAYINEVI).OrderByDescending(b => b.Count()).Select(c => c.Key).FirstOrDefault();

			var value11 = db.TBLILETISIMs.Count();
			ViewBag.viewValue1 = value1;		// toplam kitap sayısı
			ViewBag.viewValue2 = value2;		// toplam üye sayısı
			ViewBag.viewValue3 = value3;		// kasadaki toplam tutar
			ViewBag.viewValue4 = value4;		// emanet verilen kitaplar
			ViewBag.viewValue5 = value5;        // toplam kategori sayısı
			ViewBag.viewValue8 = value8;        // en fazla kitabı olan yazar
			ViewBag.viewValue9 = value9;        // en iyi yayınevi
			ViewBag.viewValue11 = value11;		// toplam mesaj sayısı
			return View();
		}
	}
}