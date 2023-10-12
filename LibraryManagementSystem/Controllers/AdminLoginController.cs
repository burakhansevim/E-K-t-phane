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

	public class AdminLoginController : Controller
	{
		// model üzerinden db isminde bir nesne türetme işlemi 
		DBKUTUPHANEEntities db = new DBKUTUPHANEEntities();
		[HttpGet]
		public ActionResult Login()
		{
			return View();
		}
		[HttpPost]
		public ActionResult Login(TBLADMIN parametre)
		{
			var veriler = db.TBLADMINs.FirstOrDefault(admin => admin.KULLANICIADI == parametre.KULLANICIADI &&
													admin.SIFRE == parametre.SIFRE);
			if (veriler != null)
			{
				FormsAuthentication.SetAuthCookie(veriler.KULLANICIADI, false);
				Session["Kullanici"] = veriler.KULLANICIADI.ToString();
				return RedirectToAction("Index", "Istatistik");
			}
			else
			{
				return View();
			}
		}

	}
}