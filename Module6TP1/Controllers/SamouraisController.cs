using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using BO;
using Module6TP1.Data;
using Module6TP1.Models;

namespace Module6TP1.Controllers
{
    public class SamouraisController : Controller
    {
        private Module6TP1Context db = new Module6TP1Context();

        // GET: Samourais
        public ActionResult Index()
        {
            return View(db.Samourais.ToList());
        }

        // GET: Samourais/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Samourai samourai = db.Samourais.Find(id);
            if (samourai == null)
            {
                return HttpNotFound();
            }
            return View(samourai);
        }

        // GET: Samourais/Create
        public ActionResult Create()
        {
            SamouraiVM vm = new SamouraiVM();

            List<int> armeIds = db.Samourais.Where(x => x.Arme != null).Select(x => x.Arme.Id).ToList();
            vm.allArme = db.Armes.Where(x => !armeIds.Contains(x.Id)).ToList()
                .Select(a => new SelectListItem { Text = a.Nom, Value = a.Id.ToString() });
            vm.ArtMartials.Add(new ArtMartial() { Nom = "Aucun" });
            vm.ArtMartials.AddRange(db.ArtMartials.ToList());

            initList(vm);
            return View(vm);
        }

        // POST: Samourais/Create
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(SamouraiVM samouraiVm)
        {
            if (ModelState.IsValid)
            {
                if (samouraiVm.IdArme !=null)
                {
                    samouraiVm.Samourai.Arme = db.Armes.FirstOrDefault(a => a.Id == samouraiVm.IdArme);
                }


                samouraiVm.Samourai.ArtMartials = db.ArtMartials.Where(a => samouraiVm.IdsArtMartiaux.Contains(a.Id))
                    .ToList();
                

                db.Samourais.Add(samouraiVm.Samourai);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(samouraiVm);
        }

        // GET: Samourais/Edit/50
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Samourai samourai = db.Samourais.Find(id);

            if (samourai == null)
            {
                return HttpNotFound();
            }

            SamouraiVM vm = new SamouraiVM();
            vm.Samourai = samourai;


            List<int> armeIds = db.Samourais.Where(x => x.Arme != null && x.Id != id).Select(x => x.Arme.Id).ToList();
            vm.allArme = db.Armes.Where(x => !armeIds.Contains(x.Id)).ToList()
                .Select(a => new SelectListItem { Text = a.Nom, Value = a.Id.ToString() });
            if (vm.Samourai.Arme != null)
            {
                vm.IdArme = vm.Samourai.Arme.Id;
            }

            vm.ArtMartials.Add(new ArtMartial() { Nom = "Aucun" });
            vm.ArtMartials.AddRange(db.ArtMartials.ToList());
            vm.IdsArtMartiaux = vm.Samourai.ArtMartials.Select(x => x.Id).ToList();

            initList(vm);
            return View(vm);
        }

        // POST: Samourais/Edit/5
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(SamouraiVM vm)
        {
            if (ModelState.IsValid)
            {
                Samourai samouraidb = db.Samourais.Find(vm.Samourai.Id);
                samouraidb.Nom = vm.Samourai.Nom;
                samouraidb.Force = vm.Samourai.Force;
                samouraidb.Arme = db.Armes.Find(vm.IdArme);

                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(vm);
        }

        // GET: Samourais/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Samourai samourai = db.Samourais.Find(id);
            if (samourai == null)
            {
                return HttpNotFound();
            }
            return View(samourai);
        }

        // POST: Samourais/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Samourai samourai = db.Samourais.Find(id);
            db.Samourais.Remove(samourai);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private void initList(SamouraiVM vm)
        {
            //vm.allArme = db.Armes.Select(a=> new SelectListItem { Text = a.Nom, Value = a.Id.ToString() }).ToList();
        }
    }
}
