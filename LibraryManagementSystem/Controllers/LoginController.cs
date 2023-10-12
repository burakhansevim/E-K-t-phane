using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using LibraryManagementSystem.Models.Entity;

namespace LibraryManagementSystem.Controllers
{
	[AllowAnonymous]  //  ilgili controller'ı authentication işleminden muaf tutar
	public class LoginController : Controller
	{
		// model üzerinden db isminde bir nesne türetme işlemi 
		DBKUTUPHANEEntities db = new DBKUTUPHANEEntities();

		[HttpGet]
		public ActionResult GirisYap()
		{
			return View();
		}
		[HttpPost]
		public ActionResult GirisYap(TBLUYELER parametre)
		{
			var veriler = db.TBLUYELERs.FirstOrDefault(a => a.MAIL == parametre.MAIL && a.SIFRE == parametre.SIFRE);
			if (veriler != null)
			{
				FormsAuthentication.SetAuthCookie(veriler.MAIL, false);
				Session["Mail"] = veriler.MAIL.ToString();
				//TempData["Id"] = veriler.ID.ToString();
				//TempData["KullanıcıAdı"] = veriler.KULLANICIADI.ToString();
				//TempData["Ad"] = veriler.AD.ToString();
				//TempData["Soyad"] = veriler.SOYAD.ToString();
				//TempData["Sifre"] = veriler.SIFRE.ToString();
				//TempData["Okul"] = veriler.OKUL.ToString();

				return RedirectToAction("Index","Panel");
			}
			else
			{
				return View();
			}
		}
	}
}