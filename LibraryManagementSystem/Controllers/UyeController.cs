using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PagedList;
using PagedList.Mvc;
using System.Web.Mvc;
using LibraryManagementSystem.Models.Entity;

namespace LibraryManagementSystem.Controllers
{
	public class UyeController : Controller
	{
		// model üzerinden db isminde bir nesne türetme işlemi 
		DBKUTUPHANEEntities db = new DBKUTUPHANEEntities();
		public ActionResult Index(int page = 1)   // PagedList nuGet Package import ederek sayfa değerini 1'den başlatır
		{
			//var degerler = db.TBLUYELERs.ToList();
			var degerler = db.TBLUYELERs.ToList().ToPagedList(page, 3);  // ilgili sayfada 3 kayıt gösterir
			return View(degerler);
		}

		[HttpGet]   
		public ActionResult YeniUyeEkle()
		{
			return View();
		}

		// ekleme işlemi

		[HttpPost]
		public ActionResult YeniUyeEkle(TBLUYELER parametre)
		{
			if (!ModelState.IsValid)
			{
				return View("YeniUyeEkle");
			}
			db.TBLUYELERs.Add(parametre);
			db.SaveChanges();
			return RedirectToAction("Index");
		}

		// silme işlemi
		public ActionResult UyeSil(int id)
		{
			var member = db.TBLUYELERs.Find(id);
			db.TBLUYELERs.Remove(member);
			db.SaveChanges();
			return RedirectToAction("Index");
		}

		// veri taşıma işlemi
		public ActionResult UyeGetir(int id)
		{
			var member = db.TBLUYELERs.Find(id);
			return View("UyeGetir", member);
		}

		// güncelleme işlemi 
		public ActionResult UyeGuncelle(TBLUYELER parametre)
		{
			var member = db.TBLUYELERs.Find(parametre.ID);
			member.AD = parametre.AD;                         
			member.SOYAD = parametre.SOYAD;                         
			member.MAIL = parametre.MAIL;                         
			member.KULLANICIADI = parametre.KULLANICIADI;                         
			member.SIFRE = parametre.SIFRE;                         
			member.OKUL = parametre.OKUL;                         
			member.TELEFON = parametre.TELEFON;                         
			member.FOTOGRAF = parametre.FOTOGRAF;                         
			db.SaveChanges();
			return RedirectToAction("Index");
		}
		public ActionResult UyeAlınanKitap(int id)
		{
			var kitap = db.TBLHAREKETs.Where(a => a.UYE == id).ToList();
			var uye = db.TBLUYELERs.Where(b => b.ID == id).Select(c => c.AD + " " + c.SOYAD).FirstOrDefault();
			ViewBag.uyeAdSoyad = uye;
			return View(kitap);
		}
	}
}