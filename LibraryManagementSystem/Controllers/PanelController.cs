using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using LibraryManagementSystem.Models.Entity;

namespace LibraryManagementSystem.Controllers
{
	//[Authorize]  // controller bazlı authentication işlemi
	public class PanelController : Controller
	{
		// model üzerinden db isminde bir nesne türetme işlemi 
		DBKUTUPHANEEntities db = new DBKUTUPHANEEntities();

		[HttpGet]
		public ActionResult Index()
		{
			var memberMail = (string)Session["Mail"];
			//var bilgiler = db.TBLUYELERs.FirstOrDefault(veri => veri.MAIL == memberMail);
			var bilgiler = db.TBLDUYURULARs.ToList();
			var value1 = db.TBLUYELERs.Where(member => member.MAIL == memberMail).Select(name => name.AD).FirstOrDefault();
			ViewBag.viewValue1 = value1;
			var value2 = db.TBLUYELERs.Where(member => member.MAIL == memberMail).Select(surname => surname.SOYAD).FirstOrDefault();
			ViewBag.viewValue2 = value2;
			var value3 = db.TBLUYELERs.Where(member => member.MAIL == memberMail).Select(photo => photo.FOTOGRAF).FirstOrDefault();
			ViewBag.viewValue3 = value3;
			var value4 = db.TBLUYELERs.Where(member => member.MAIL == memberMail).Select(username => username.KULLANICIADI).FirstOrDefault();
			ViewBag.viewValue4 = value4;
			var value5 = db.TBLUYELERs.Where(member => member.MAIL == memberMail).Select(school => school.OKUL).FirstOrDefault();
			ViewBag.viewValue5 = value5;
			var value6 = db.TBLUYELERs.Where(member => member.MAIL == memberMail).Select(phone => phone.TELEFON).FirstOrDefault();
			ViewBag.viewValue6 = value6;
			var value7 = db.TBLUYELERs.Where(member => member.MAIL == memberMail).Select(phone => phone.MAIL).FirstOrDefault();
			ViewBag.viewValue7 = value7;

			var memberID = db.TBLUYELERs.Where(random1 => random1.MAIL == memberMail).Select(member => member.ID).FirstOrDefault();
			var value8 = db.TBLHAREKETs.Where(random2 => random2.UYE == memberID).Count();
			ViewBag.viewValue8 = value8;

			var value9 = db.TBLMESAJLARs.Where(random3 => random3.ALICI == memberMail).Count(); // sisteme kayıt olan üyelerde memberMail ifadesini mesajlar tablosundan sayar
			ViewBag.viewValue9 = value9;

			var value10 = db.TBLDUYURULARs.Count();
			ViewBag.viewValue10 = value10;


			return View(bilgiler);
		}

		[HttpPost]
		public ActionResult Index2(TBLUYELER parametre)
		{
			var user = (string)Session["Mail"];
			var member = db.TBLUYELERs.FirstOrDefault(a => a.MAIL == user);
			member.AD = parametre.AD;
			member.SOYAD = parametre.SOYAD;
			member.KULLANICIADI = parametre.KULLANICIADI;
			member.SIFRE = parametre.SIFRE;
			member.FOTOGRAF = parametre.FOTOGRAF;
			member.OKUL = parametre.OKUL;
			db.SaveChanges();
			return RedirectToAction("Index");
		}
		public ActionResult Kitaplarim()
		{
			var user = (string)Session["Mail"];
			var id = db.TBLUYELERs.Where(a => a.MAIL == user.ToString()).Select(b => b.ID).FirstOrDefault();
			var islemler = db.TBLHAREKETs.Where(islem => islem.UYE == id).ToList();
			return View(islemler);
		}

		public ActionResult Duyurular()
		{
			var duyurulistesi = db.TBLDUYURULARs.ToList();
			return View(duyurulistesi);
		}
		public ActionResult LogOut()	// oturumdan çıkış işlemi
		{
			FormsAuthentication.SignOut();
			return RedirectToAction("GirisYap", "Login");
		}
		public PartialViewResult PartialDuyurular()
		{
			return PartialView();
		}
		public PartialViewResult PartialAyarlar()
		{
			var user = (string)Session["Mail"];
			var id = db.TBLUYELERs.Where(a => a.MAIL == user).Select(b => b.ID).FirstOrDefault();
			var findMember = db.TBLUYELERs.Find(id);
			return PartialView("PartialAyarlar", findMember);
		}
	}
}