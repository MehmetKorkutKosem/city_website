using System.IO;
using System.Web.Mvc;
using System.Web;
using WebApplication8.Models;
using System.Linq;
using System.Globalization; // LINQ namespace'ini ekledik

public class HomeController : Controller
{
    private BitlisEntities2 db = new BitlisEntities2(); // Veritabanı bağlamı

    // Ana Sayfa (herkese açık)
    public ActionResult Index()
    {
        // Slider verisi
        var slider = db.sliders.ToList(); // ToList() metodunu kullanıyoruz
        ViewBag.Slider = slider;

        // Diğer veriler
        var turizm = db.turizms.ToList(); // ToList() metodunu kullanıyoruz
        ViewBag.Turizm = turizm;

        var nufus = db.nufus.ToList(); // ToList() metodunu kullanıyoruz
        ViewBag.Nufus = nufus;

        var ilce = db.Ilces.ToList(); // ToList() metodunu kullanıyoruz
        ViewBag.Ilce = ilce;

        return View();
    }

    // Slider ekleme sayfası (sadece admin erişebilir)
    [Authorize(Roles = "Admin")]
    public ActionResult Create()
    {
        return View();
    }

    // Slider ekleme işlemi (sadece admin erişebilir)
    [Authorize(Roles = "Admin")]
    [HttpPost]
    [ValidateAntiForgeryToken]  // AntiForgery doğrulaması
    public ActionResult Create(slider slider, HttpPostedFileBase imgUrl)
    {
        if (ModelState.IsValid)
        {
            // Fotoğraf yükleme işlemi
            if (imgUrl != null && imgUrl.ContentLength > 0)
            {
                var fileName = Path.GetFileName(imgUrl.FileName);
                var filePath = Path.Combine(Server.MapPath("~/Content/SliderImages"), fileName);
                imgUrl.SaveAs(filePath);

                // Fotoğrafın URL'sini veritabanına ekle
                slider.imgUrl = "~/Content/SliderImages/" + fileName;
            }

            db.sliders.Add(slider);
            db.SaveChanges();

            // Ana sayfaya yönlendirme
            return RedirectToAction("Index");
        }

        return View(slider);
    }

    // İlçeler sayfası (sadece giriş yapmış üyeler erişebilir)
    [Authorize]
    public ActionResult Ilce()
    {
        var ilce = db.Ilces.ToList(); // ToList() metodunu kullanıyoruz
        ViewBag.Ilce = ilce;
        return View(ilce);
    }

    // Turizm sayfası (sadece giriş yapmış üyeler erişebilir)
    [Authorize]
    public ActionResult Turizm()
    {
        var turizm = db.turizms.ToList(); // ToList() metodunu kullanıyoruz
        ViewBag.Turizm = turizm;
        return View(turizm);
    }

    // Nüfus sayfası (sadece giriş yapmış üyeler erişebilir)
    [Authorize]
    public ActionResult Nufus()
    {
        var nufus = db.nufus.ToList(); // ToList() metodunu kullanıyoruz
        ViewBag.Nufus = nufus;
        return View(nufus);
    }

    // Hakkında sayfası (herkese açık)
    public ActionResult About()
    {
        ViewBag.Message = "Your application description page.";
        return View();
    }

    // İletişim sayfası (herkese açık)
    public ActionResult Contact()
    {
        ViewBag.Message = "Your contact page.";
        return View();
    }
    public ActionResult dilDegistir(string lang, string returnUrl)
    {
        Session["Dil"] = new CultureInfo(lang);
        return Redirect(returnUrl);
    }
}
