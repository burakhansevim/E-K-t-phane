using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LibraryManagementSystem.Models;
using LibraryManagementSystem.Models.Entity;

namespace LibraryManagementSystem.Controllers
{
	public class GrafikController : Controller
	{
		// model üzerinden db isminde bir nesne türetme işlemi 
		DBKUTUPHANEEntities db = new DBKUTUPHANEEntities();
		public ActionResult Index()
		{
			return View();
		}
		public ActionResult VisualizeKitapResult()
		{
			return Json(liste());
		}
		public List<Charts> liste()
		{
			List<Charts> cs = new List<Charts>();
			cs.Add(new Charts()
			{
				yayinevi = "Yıldız",
				sayi = 7
			});
			cs.Add(new Charts()
			{
				yayinevi = "Uranüs",
				sayi = 10
			});
			cs.Add(new Charts()
			{
				yayinevi = "Satürn",
				sayi = 5
			});
			return cs;
		}
	}
}