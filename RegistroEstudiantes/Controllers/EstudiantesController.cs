using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using RegistroEstudiantes.Models;

namespace RegistroEstudiantes.Controllers
{
    [Authorize]
    public class EstudiantesController : Controller
    {
        private readonly RegistroEstudiantesContext _context;

        public EstudiantesController(RegistroEstudiantesContext context)
        {
            _context = context;
        }

        // GET: Estudiantes
        public async Task<IActionResult> Index()
        {
            var registroEstudiantesContext = _context.Estudiantes.Include(e => e.IdMateriaNavigation);
            return View(await registroEstudiantesContext.ToListAsync());
        }

        // GET: Estudiantes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var estudiantes = await _context.Estudiantes
                .Include(e => e.IdMateriaNavigation)
                .FirstOrDefaultAsync(m => m.IdEstudiante == id);
            if (estudiantes == null)
            {
                return NotFound();
            }

            return View(estudiantes);
        }

        // GET: Estudiantes/Create
        public IActionResult Create()
        {
            ViewData["IdMateria"] = new SelectList(_context.Materias, "IdMateria", "Nombre");
            return View();
        }

        // POST: Estudiantes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdEstudiante,Nombre,Apellido,Email,IdMateria")] Estudiantes estudiantes)
        {
            if (ModelState.IsValid)
            {
                _context.Add(estudiantes);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdMateria"] = new SelectList(_context.Materias, "IdMateria", "IdMateria", estudiantes.IdMateria);
            return View(estudiantes);
        }

        // GET: Estudiantes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var estudiantes = await _context.Estudiantes.FindAsync(id);
            if (estudiantes == null)
            {
                return NotFound();
            }
            ViewData["IdMateria"] = new SelectList(_context.Materias, "IdMateria", "Nombre", estudiantes.IdMateria);
            return View(estudiantes);
        }

        // POST: Estudiantes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdEstudiante,Nombre,Apellido,Email,IdMateria")] Estudiantes estudiantes)
        {
            if (id != estudiantes.IdEstudiante)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(estudiantes);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EstudiantesExists(estudiantes.IdEstudiante))
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
            ViewData["IdMateria"] = new SelectList(_context.Materias, "IdMateria", "IdMateria", estudiantes.IdMateria);
            return View(estudiantes);
        }

        // GET: Estudiantes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var estudiantes = await _context.Estudiantes
                .Include(e => e.IdMateriaNavigation)
                .FirstOrDefaultAsync(m => m.IdEstudiante == id);
            if (estudiantes == null)
            {
                return NotFound();
            }

            return View(estudiantes);
        }

        // POST: Estudiantes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var estudiantes = await _context.Estudiantes.FindAsync(id);
            _context.Estudiantes.Remove(estudiantes);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EstudiantesExists(int id)
        {
            return _context.Estudiantes.Any(e => e.IdEstudiante == id);
        }
    }
}
