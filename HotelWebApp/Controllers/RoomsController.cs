using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using HotelWebApp.Data;
using HotelWebApp.Models;

namespace HotelWebApp.Controllers
{
    public class RoomsController : Controller
    {
        private readonly HotelWebAppContext _context;

        public RoomsController(HotelWebAppContext context)
        {
            _context = context;
        }

        // GET: Rooms
        public async Task<IActionResult> Index()
        {
            var hotelWebAppContext = _context.Room.Include(r => r.Hotel);
            return View(await hotelWebAppContext.ToListAsync());
        }

        // GET: Rooms/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Room == null)
            {
                return NotFound();
            }

            var room = await _context.Room
                .Include(r => r.Hotel)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (room == null)
            {
                return NotFound();
            }

            return View(room);
        }

        // GET: Rooms/Create
        public IActionResult Create()
        {

            // Recupera la lista de hoteles desde tu base de datos o fuente de datos
            var hotelList = _context.Hotel.ToList(); // 
           
            // Pasa la lista de hoteles a la vista usando ViewBag
            ViewBag.HotelList = new SelectList(hotelList, "Id", "Name");

            ViewData["HotelId"] = new SelectList(_context.Hotel, "Id", "Name");
            return View();

        }


        // POST: Rooms/Create
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Number,MaxGuests,HotelId")] Room room)
        {


            var hotelElegido = _context.Hotel
                .Find(room.HotelId);

            if (hotelElegido != null)
            {

                // Asigna el hotel a la habitación.
                room.Hotel = hotelElegido;

            }
            // hotelElegido.Rooms.Add(room);
            VerificarHabitacion(room);

            if (ModelState.IsValid)
            {
                _context.Add(room);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["HotelId"] = new SelectList(_context.Hotel, "Id", "Address", ViewBag.Hotel);
            return RedirectToAction(nameof(Index));
        }

        // GET: Rooms/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Room == null)
            {
                return NotFound();
            }

            var room = await _context.Room.FindAsync(id);
            if (room == null)
            {
                return NotFound();
            }
            ViewData["HotelId"] = new SelectList(_context.Hotel, "Id", "Address", room.HotelId);
            return View(room);
        }

        // POST: Rooms/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Number,MaxGuests,HotelId")] Room room)
        {
            if (id != room.Id)
            {
                return NotFound();
            }

          //  VerificarHabitacion(room);   Terminar de arreglar

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(room);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RoomExists(room.Id))
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
            ViewData["HotelId"] = new SelectList(_context.Hotel, "Id", "Address", room.HotelId);
            return View(room);
        }

        // GET: Rooms/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Room == null)
            {
                return NotFound();
            }

            var room = await _context.Room
                .Include(r => r.Hotel)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (room == null)
            {
                return NotFound();
            }

            return View(room);
        }

        // POST: Rooms/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Room == null)
            {
                return Problem("Entity set 'HotelWebAppContext.Room'  is null.");
            }
            var room = await _context.Room.FindAsync(id);
            if (room != null)
            {
                _context.Room.Remove(room);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RoomExists(int id)
        {
            return (_context.Room?.Any(e => e.Id == id)).GetValueOrDefault();
        }
        public bool ExistRoomNumber(Room room)
        {
            bool resultado = false;
            bool resultado2 = false;
            if (room.Number!=0)
            {
                if ( room.Id != 0)
                {
                    resultado = _context.Room.Any(p => p.Number == room.Number && p.Id != room.Id) ;
                   
                }
                else
                {
                    resultado = _context.Room.Any(p => p.Number == room.Number) ;
                    
                }
            }
            return resultado && resultado2;
        }


        private void VerificarHabitacion(Room room)
        {
           
            if (ExistRoomNumber(room))
            {
                ModelState.AddModelError("Titulo", "Ya existe una Habitacion con ese numero.");
            }
        }
    }
}
