using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LibraryManagementSystem.Models.Entity;

namespace LibraryManagementSystem.Controllers
{
	public class AyarlarController : Controller
	{
		// model üzerinden db isminde bir nesne türetme işlemi 
		DBKUTUPHANEEntities db = new DBKUTUPHANEEntities();

		public ActionResult Index()
		{
			var users = db.TBLADMINs.ToList();
			return View(users);
		}
		// admin listesini veritabanına boş kayıt oluşturmadan görüntüler
		[HttpGet]
		public ActionResult AdminEkle()
		{
			return View();
		}

		// popup penceresi üzerinden yeni admin ekleme işlemi
		[HttpPost]
		public ActionResult AdminEkle(TBLADMIN parametre)
		{
			db.TBLADMINs.Add(parametre);
			db.SaveChanges();
			return RedirectToAction("Index");
		}

		// admin silme işlemi
		public ActionResult AdminSil(int id)
		{
			var admin = db.TBLADMINs.Find(id);
			db.TBLADMINs.Remove(admin);
			db.SaveChanges();
			return RedirectToAction("Index");
		}

		// admin bilgileri güncelleme işlemi

		[HttpGet]
		public ActionResult AdminGuncelle(int id)
		{
			var admin = db.TBLADMINs.Find(id);
			return View("AdminGuncelle", admin);
		}
		[HttpPost]
		public ActionResult AdminGuncelle(TBLADMIN parametre)
		{
			var admin = db.TBLADMINs.Find(parametre.ID);
			admin.KULLANICIADI = parametre.KULLANICIADI;
			admin.SIFRE = parametre.SIFRE;
			admin.YETKI = parametre.YETKI;
			db.SaveChanges();
			return RedirectToAction("Index");
		}
	}
}