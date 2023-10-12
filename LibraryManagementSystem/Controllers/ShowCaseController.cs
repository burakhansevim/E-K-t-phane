using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LibraryManagementSystem.Models.Entity;
using LibraryManagementSystem.Models.myClasses;

namespace LibraryManagementSystem.Controllers
{
	[AllowAnonymous]  //  ilgili controller'ı authentication işleminden muaf tutar

	public class ShowCaseController : Controller
	{
		// model üzerinden db isminde bir nesne türetme işlemi 
		DBKUTUPHANEEntities db = new DBKUTUPHANEEntities();

		[HttpGet]
		public ActionResult Index()
		{
			Class1 class1 = new Class1();       // IEnumerable özelliğini kullandığımız sınıftan class1 isminde nesne türetildi
			class1.value1 = db.TBLKITAPs.ToList();
			class1.value2 = db.TBLHAKKIMIZDAs.ToList();

			//var veriler = db.TBLKITAPs.ToList();
			return View(class1);
		}

		[HttpPost]
		public ActionResult Index(TBLILETISIM parametre)
		{
			db.TBLILETISIMs.Add(parametre);
			db.SaveChanges();
			return RedirectToAction("Index");
		}
	}
}