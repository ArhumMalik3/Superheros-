using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SuperHero.Data;
using SuperHero.Models;

namespace SuperHero.Controllers
{
    public class SuperheroController : Controller
    {
        ApplicationDbContext _context;

        public SuperheroController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Superhero
        public ActionResult Index()
        {
            var superheroTable = _context.superheros;
            return View(superheroTable);
        }

        // GET: Superhero/Details/5
        public ActionResult Details(int id)
        {
            var superheroChosen = _context.superheros.Where(s => s.Id == id).Single();
            return View(superheroChosen);
        }

        // GET: Superhero/Create
        public ActionResult Create()
        {
            Superhero superhero = new Superhero();
            return View(superhero);
        }

        // POST: Superhero/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Superhero superhero)
        {
            try
            {
                _context.superheros.Add(superhero);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            catch(Exception e)
            {
                return View(e);
            }
        }

        // GET: Superhero/Edit/5
        public ActionResult Edit(int id)
        {
            Superhero superheroToEdit;
            superheroToEdit = _context.superheros.Where(s => s.Id == id).Single();
            return View(superheroToEdit);
        }

        // POST: Superhero/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Superhero superHero)
        {
            Superhero superHeroFromDb;
            superHeroFromDb = _context.superheros.Where(s => s.Id == superHero.Id).SingleOrDefault();
            superHeroFromDb.name = superHero.name;
            superHeroFromDb.primaryAbility = superHero.primaryAbility;
            superHeroFromDb.secondaryAbility = superHero.secondaryAbility;
            superHeroFromDb.catchPhrase = superHero.catchPhrase;
            superHeroFromDb.alterEgo = superHero.alterEgo;

            _context.Update(superHeroFromDb);
            _context.SaveChanges();
                
            
            
            return RedirectToAction("Details"); ;
        }

        // GET: Superhero/Delete/5
        public ActionResult Delete(int id)
        {
            Superhero superheroToDelete;
            superheroToDelete = _context.superheros.Where(s => s.Id == id).Single();
            return View(superheroToDelete);
        }

        // POST: Superhero/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(Superhero superhero)
        {
            try
            {
                _context.superheros.Remove(superhero);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
