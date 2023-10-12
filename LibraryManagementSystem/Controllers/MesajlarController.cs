using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LibraryManagementSystem.Models.Entity;

namespace LibraryManagementSystem.Controllers
{
	public class MesajlarController : Controller
	{
		// model üzerinden db isminde bir nesne türetme işlemi 
		DBKUTUPHANEEntities db = new DBKUTUPHANEEntities();

		public ActionResult GelenMesaj()
		{
			var memberMail = (string)Session["Mail"].ToString();
			var mesajlar = db.TBLMESAJLARs.Where(x => x.ALICI == memberMail.ToString()).ToList();
			return View(mesajlar);
		}
		public ActionResult GidenMesaj()
		{
			var memberMail = (string)Session["Mail"].ToString();
			var mesajlar = db.TBLMESAJLARs.Where(x => x.GONDEREN == memberMail.ToString()).ToList();
			return View(mesajlar);
		}
		[HttpGet]
		public ActionResult YeniMesaj()
		{
			return View();
		}
		[HttpPost]
		public ActionResult YeniMesaj(TBLMESAJLAR parametre)
		{
			var memberMail = (string)Session["Mail"].ToString();
			parametre.GONDEREN = memberMail.ToString();
			parametre.TARIH = DateTime.Parse(DateTime.Now.ToShortDateString());
			db.TBLMESAJLARs.Add(parametre);
			db.SaveChanges();
			return RedirectToAction("GidenMesaj","Mesajlar");
		}
		public PartialViewResult PartialEylemler()
		{
			var memberMail = (string)Session["Mail"].ToString();
			var gelenMesajSayisi = db.TBLMESAJLARs.Where(mesaj => mesaj.ALICI == memberMail).Count();
			ViewBag.viewValueMesaj1 = gelenMesajSayisi;

			var gidenMesajSayisi = db.TBLMESAJLARs.Where(mesaj => mesaj.GONDEREN == memberMail).Count();
			ViewBag.viewValueMesaj2 = gidenMesajSayisi;
			return PartialView();
		}
	}
}