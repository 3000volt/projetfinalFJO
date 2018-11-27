using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using projetfinalFJO.Appdata;

namespace projetfinalFJO.Controllers
{
    public class SessionsController : Controller
    {
        private readonly ActualisationContext _context;

        public SessionsController(ActualisationContext context)
        {
            _context = context;
        }

        // GET: Sessions
        public async Task<IActionResult> Index()
        {
            return View(await _context.Session.ToListAsync());
        }

        // GET: Sessions/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var session = await _context.Session
                .FirstOrDefaultAsync(m => m.NomSession == id);
            if (session == null)
            {
                return NotFound();
            }

            return View(session);
        }

        // GET: Sessions/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Sessions/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([FromBody][Bind("NomSession,NoProgramme")] Session session)
        {
            session.NoProgramme = this.HttpContext.Session.GetString("programme");
            if (ModelState.IsValid)
            {
                _context.Session.Add(session);
                await _context.SaveChangesAsync();
                return Ok("ajout reussi");
            }
            return BadRequest("groupe non ajouté");
        }

        // GET: Sessions/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var session = await _context.Session.FindAsync(id);
            if (session == null)
            {
                return NotFound();
            }
            return View(session);
        }

        // POST: Sessions/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("NomSession,NoProgramme")] Session session)
        {
            if (id != session.NomSession)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(session);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SessionExists(session.NomSession))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(session);
        }

        // GET: Sessions/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var session = await _context.Session
                .FirstOrDefaultAsync(m => m.NomSession == id);
            if (session == null)
            {
                return NotFound();
            }

            return View(session);
        }

        // POST: Sessions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var session = await _context.Session.FindAsync(id);
            _context.Session.Remove(session);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SessionExists(string id)
        {
            return _context.Session.Any(e => e.NomSession == id);
        }
    }
}
