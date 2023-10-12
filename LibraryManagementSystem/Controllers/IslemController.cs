using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LibraryManagementSystem.Models.Entity;

namespace LibraryManagementSystem.Controllers
{
	public class IslemController : Controller
	{
		// model üzerinden db isminde bir nesne türetme işlemi 
		DBKUTUPHANEEntities db = new DBKUTUPHANEEntities();

		public ActionResult Index()
		{
			var islemler = db.TBLHAREKETs.Where(islem => islem.ISLEMDURUM == true).ToList();
			return View(islemler);
		}
	}
}