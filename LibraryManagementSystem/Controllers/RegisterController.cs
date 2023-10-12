using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LibraryManagementSystem.Models.Entity;

namespace LibraryManagementSystem.Controllers
{
	[AllowAnonymous]  //  ilgili controller'ı authentication işleminden muaf tutar

	public class RegisterController : Controller
	{
		// model üzerinden db isminde bir nesne türetme işlemi 
		DBKUTUPHANEEntities db = new DBKUTUPHANEEntities();

		[HttpGet]
		public ActionResult Kaydol()
		{
			return View();
		}
		[HttpPost]
		public ActionResult Kaydol(TBLUYELER parametre)
		{
			if (!ModelState.IsValid)
			{
				return View("Kaydol");
			}
			else
			{
				db.TBLUYELERs.Add(parametre);
				db.SaveChanges();
				return View();
			}
		}
	}
}