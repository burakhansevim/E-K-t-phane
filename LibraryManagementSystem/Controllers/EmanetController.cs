using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LibraryManagementSystem.Models.Entity;

namespace LibraryManagementSystem.Controllers
{
	public class EmanetController : Controller
	{
		// model üzerinden db isminde bir nesne türetme işlemi 
		DBKUTUPHANEEntities db = new DBKUTUPHANEEntities();

		//[Authorize(Roles = "A")]    // rol bazlı görüntüleme işlemi: yetki değeri "A" olmayan admin kullanıcısı ilgili indexi göremez
		public ActionResult Index()
		{
			var veriler = db.TBLHAREKETs.Where(x => x.ISLEMDURUM == false).ToList();   // method biçimli LINQ
			return View(veriler);
		}
		[HttpGet]
		public ActionResult EmanetVer()
		{
			List<SelectListItem> value1 = (from data in db.TBLUYELERs.ToList()      // query biçimli LINQ
										   select new SelectListItem
										   {
											   Text = data.AD + " " + data.SOYAD,
											   Value = data.ID.ToString()
										   }).ToList();
			List<SelectListItem> value2 = (from data in db.TBLKITAPs.Where(x => x.DURUM == true).ToList()
										   select new SelectListItem
										   {
											   Text = data.AD,
											   Value = data.ID.ToString()
										   }).ToList();
			List<SelectListItem> value3 = (from data in db.TBLPERSONELs.ToList()
										   select new SelectListItem
										   {
											   Text = data.PERSONEL,
											   Value = data.ID.ToString()
										   }).ToList();
			ViewBag.viewValue1 = value1;
			ViewBag.viewValue2 = value2;
			ViewBag.viewValue3 = value3;
			return View();
		}

		[HttpPost]
		public ActionResult EmanetVer(TBLHAREKET parametre)
		{
			var deger1 = db.TBLUYELERs.Where(x => x.ID == parametre.TBLUYELER.ID).FirstOrDefault();
			var deger2 = db.TBLKITAPs.Where(y => y.ID == parametre.TBLKITAP.ID).FirstOrDefault();
			var deger3 = db.TBLPERSONELs.Where(z => z.ID == parametre.TBLPERSONEL.ID).FirstOrDefault();
			parametre.TBLUYELER = deger1;
			parametre.TBLKITAP = deger2;
			parametre.TBLPERSONEL = deger3;
			db.TBLHAREKETs.Add(parametre);
			db.SaveChanges();
			return RedirectToAction("Index");
		}

		public ActionResult KitapTeslim(TBLHAREKET parametre)
		{
			var teslim = db.TBLHAREKETs.Find(parametre.ID);
			DateTime date1 = DateTime.Parse(teslim.IADETARIH.ToString());
			DateTime date2 = Convert.ToDateTime(DateTime.Now.ToShortDateString());
			TimeSpan date3 = date2 - date1;
			ViewBag.deger = date3.TotalDays;    // Viewbag ile controllerden view'e veri taşınır
			return View("KitapTeslim", teslim);
		}

		// teslim edilen kitabın durumunu güncelleme işlemi
		public ActionResult TeslimGuncelle(TBLHAREKET parametre)
		{
			var teslim = db.TBLHAREKETs.Find(parametre.ID);  // id değerine göre güncelleme yapmak için hafızaya alır
			teslim.UYEGETIRTARIH = parametre.UYEGETIRTARIH;  // üyenin teslim ettiği tarihle standart teslim tarihini denkler
			teslim.ISLEMDURUM = true;                        // kitabın emanet edilebilir olmasını mümkün kılar
			db.SaveChanges();
			return RedirectToAction("Index");
		}
	}
}