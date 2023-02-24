using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TravelPlaces.Models;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc.Rendering;
using TravelPlaces.Filters;
namespace TravelPlaces.Controllers
{
    [SessionFilter]
    public class AdminController : Controller
    {
        private readonly Ace42023Context db;
        public AdminController(Ace42023Context _db)
        {
            db = _db;
        }

        private SelectList TouristPlacesList()
        {
            var touristplaces = db.TouristPlaces.ToList();
            return new SelectList(touristplaces, "PlaceId", "Name");
        }
        private SelectList ServiceProviderList()
        {
            var serviceproviders = db.ServiceProviders.ToList();
            return new SelectList(serviceproviders, "ProviderId", "Name");
        }

        public IActionResult Index()
        {
            return View(db.TouristPlaces.ToList());
        }
        public IActionResult Images()
        {
            return View(db.PlaceImages.ToList());
        }
        public IActionResult Providers()
        {
            return View(db.ServiceProviders.ToList());
        }
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(TouristPlace T)
        {
            db.TouristPlaces.Add(T);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public IActionResult AddImage()
        {
            ;
            ViewBag.TouristPlacesList = TouristPlacesList();
            return View();
        }

        [HttpPost]
        public IActionResult AddImage(PlaceImage I)
        {
            ViewBag.TouristPlacesList = TouristPlacesList();

            ModelState.Remove("Place");
            if (ModelState.IsValid)
            {
                db.PlaceImages.Add(I);
                db.SaveChanges();
                ViewBag.SuccessMessage = "Image added successfully.";
                return View(I);
            }
            ViewBag.ErrorMessage = "There are some errors. please try later";
            return View(I);
        }
        public IActionResult CreateProvider()
        {
            return View();
        }

        [HttpPost]
        public IActionResult CreateProvider(TravelPlaces.Models.ServiceProvider S)
        {

            if (ModelState.IsValid)
            {
                db.ServiceProviders.Add(S);
                db.SaveChanges();
                ViewBag.SuccessMessage = "Service Provider added successfully.";
                return View(S);
            }
            ViewBag.ErrorMessage = "There are some errors. please try later";
            return View(S);
        }

        public IActionResult AddPlaceServiceProvider()
        {
            ViewBag.TouristPlacesList = TouristPlacesList();
            ViewBag.ServiceProviderList = ServiceProviderList();
            return View();
        }

        [HttpPost]
        public IActionResult AddPlaceServiceProvider(PlaceServiceProvider P)
        {
            ViewBag.TouristPlacesList = TouristPlacesList();
            ViewBag.ServiceProviderList = ServiceProviderList();

            if (ModelState.IsValid)
            {
                var touristplace = db.TouristPlaces.Where(x => x.PlaceId == P.PlaceId).SingleOrDefault();
                var serviceprovider = db.ServiceProviders.Where(x => x.ProviderId == P.ProviderId).SingleOrDefault();
                if (touristplace != null && serviceprovider != null)
                {
                    try
                    {
                        touristplace.Providers.Add(serviceprovider);
                        db.SaveChanges();
                    }
                    catch (Exception e)
                    {
                        ViewBag.ErrorMessage = "Some error Occured.";
                        return View(P);
                    }
                    ViewBag.SuccessMessage = "Provider added to Place successfully.";
                    return View(P);
                }
                ViewBag.ErrorMessage = "Service Provider or Place not found in the database.";
                return View(P);
            }
            ViewBag.ErrorMessage = "There are some errors. please try later";
            return View(P);
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            TouristPlace T = db.TouristPlaces.Where(x => x.PlaceId == id).SingleOrDefault();
            return View(T);
        }

        [HttpPost]
        public IActionResult Edit(TouristPlace T)
        {
            db.TouristPlaces.Update(T);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        [HttpGet]
        public IActionResult EditImage(int id)
        {
            PlaceImage S = db.PlaceImages.Where(x => x.ImageId == id).SingleOrDefault();
            return View(S);
        }

        [HttpPost]
        public IActionResult EditImage(PlaceImage P)
        {
            db.PlaceImages.Update(P);
            db.SaveChanges();
            return RedirectToAction("Images");
        }
        [HttpGet]
        public IActionResult EditProvider(int id)
        {
            TravelPlaces.Models.ServiceProvider S = db.ServiceProviders.Where(x => x.ProviderId == id).SingleOrDefault();
            return View(S);
        }

        [HttpPost]
        public IActionResult EditProvider(TravelPlaces.Models.ServiceProvider P)
        {
            db.ServiceProviders.Update(P);
            db.SaveChanges();
            return RedirectToAction("Providers");
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            TouristPlace T = db.TouristPlaces.Where(x => x.PlaceId == id).SingleOrDefault();
            return View(T);
        }

        [HttpPost]
        [ActionName("Delete")]
        public IActionResult DeleteConfirmed(int id)
        {
            TouristPlace T = db.TouristPlaces.Where(x => x.PlaceId == id).SingleOrDefault();
            db.TouristPlaces.Remove(T);
            db.SaveChanges();
            Console.WriteLine("Deleting.............");
            return RedirectToAction("Index");
        }
        [HttpGet]
        public IActionResult DeleteImage(int id)
        {
            PlaceImage T = db.PlaceImages.Where(x => x.ImageId == id).SingleOrDefault();
            return View(T);
        }

        [HttpPost]
        [ActionName("DeleteImage")]
        public IActionResult DeleteImageConfirmed(int id)
        {
            PlaceImage T = db.PlaceImages.Where(x => x.ImageId == id).SingleOrDefault();
            db.PlaceImages.Remove(T);
            db.SaveChanges();
            Console.WriteLine("Deleting Image.............");
            return RedirectToAction("Images");
        }
        [HttpGet]
        public IActionResult DeleteProvider(int id)
        {
            TravelPlaces.Models.ServiceProvider T = db.ServiceProviders.Where(x => x.ProviderId == id).SingleOrDefault();
            return View(T);
        }

        [HttpPost]
        [ActionName("DeleteProvider")]
        public IActionResult DeleteProviderConfirmed(int id)
        {
            TravelPlaces.Models.ServiceProvider T = db.ServiceProviders.Where(x => x.ProviderId == id).SingleOrDefault();
            db.ServiceProviders.Remove(T);
            db.SaveChanges();
            Console.WriteLine("Deleting Provider.............");
            return RedirectToAction("Providers");
        }

        [HttpGet]
        public IActionResult Details(int id)
        {
            TouristPlace T = db.TouristPlaces.Include(p => p.Providers).FirstOrDefault(p => p.PlaceId == id);
            return View(T);
        }
        [HttpGet]
        public IActionResult DetailsImage(int id)
        {
            PlaceImage T = db.PlaceImages.FirstOrDefault(x => x.ImageId == id);
            return View(T);
        }
        [HttpGet]
        public IActionResult DetailsProvider(int id)
        {
            TravelPlaces.Models.ServiceProvider T = db.ServiceProviders.FirstOrDefault(p => p.ProviderId == id);
            return View(T);
        }


    }
}