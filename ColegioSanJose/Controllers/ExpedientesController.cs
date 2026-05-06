using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ColegioSanJose.Data;
using ColegioSanJose.Models;

namespace ColegioSanJose.Controllers
{
    public class ExpedientesController : Controller
    {
        private readonly ColegioContext _context;

        public ExpedientesController(ColegioContext context)
        {
            _context = context;
        }

        // GET: Expedientes
        public async Task<IActionResult> Index()
        {
            var colegioContext = _context.Expedientes.Include(e => e.Alumno).Include(e => e.Materia);
            return View(await colegioContext.ToListAsync());
        }

        // GET: Expedientes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var expediente = await _context.Expedientes
                .Include(e => e.Alumno)
                .Include(e => e.Materia)
                .FirstOrDefaultAsync(m => m.ExpedienteId == id);
            if (expediente == null)
            {
                return NotFound();
            }

            return View(expediente);
        }

        // GET: Expedientes/Create
        public IActionResult Create()
        {
            ViewData["AlumnoId"] = new SelectList(_context.Alumnos, "AlumnoId", "Nombre");
            ViewData["MateriaId"] = new SelectList(_context.Materias, "MateriaId", "NombreMateria");
            return View();
        }

        // POST: Expedientes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ExpedienteId,AlumnoId,MateriaId,NotaFinal,Observaciones")] Expediente expediente)
        {
            if (ModelState.IsValid)
            {
                _context.Add(expediente);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            ViewData["AlumnoId"] = new SelectList(_context.Alumnos, "AlumnoId", "Nombre", expediente.AlumnoId);
            ViewData["MateriaId"] = new SelectList(_context.Materias, "MateriaId", "NombreMateria", expediente.MateriaId);
            return View(expediente);
        }


        // GET: Expedientes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var expediente = await _context.Expedientes.FindAsync(id);
            if (expediente == null)
            {
                return NotFound();
            }
            ViewData["AlumnoId"] = new SelectList(_context.Alumnos, "AlumnoId", "Nombre", expediente.AlumnoId);
            ViewData["MateriaId"] = new SelectList(_context.Materias, "MateriaId", "NombreMateria", expediente.MateriaId);
            return View(expediente);
        }

        // POST: Expedientes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ExpedienteId,AlumnoId,MateriaId,NotaFinal,Observaciones")] Expediente expediente)
        {
            if (id != expediente.ExpedienteId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(expediente);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ExpedienteExists(expediente.ExpedienteId))
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
            ViewData["AlumnoId"] = new SelectList(_context.Alumnos, "AlumnoId", "Nombre", expediente.AlumnoId);
            ViewData["MateriaId"] = new SelectList(_context.Materias, "MateriaId", "NombreMateria", expediente.MateriaId);
            return View(expediente);
        }

        // GET: Expedientes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var expediente = await _context.Expedientes
                .Include(e => e.Alumno)
                .Include(e => e.Materia)
                .FirstOrDefaultAsync(m => m.ExpedienteId == id);
            if (expediente == null)
            {
                return NotFound();
            }

            return View(expediente);
        }

        // POST: Expedientes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var expediente = await _context.Expedientes.FindAsync(id);
            if (expediente != null)
            {
                _context.Expedientes.Remove(expediente);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ExpedienteExists(int id)
        {
            return _context.Expedientes.Any(e => e.ExpedienteId == id);
        }

        public async Task<IActionResult> Promedios()
        {
            var promedios = await _context.Expedientes
                .Include(e => e.Alumno)
                .GroupBy(e => new { e.AlumnoId, e.Alumno.Nombre, e.Alumno.Apellido })
                .Select(g => new
                {
                    Alumno = g.Key.Nombre + " " + g.Key.Apellido,
                    Promedio = g.Average(e => e.NotaFinal)
                })
                .ToListAsync();

            return View(promedios);
        }
    }
}
