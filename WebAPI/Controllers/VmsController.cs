using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAPI.Models;
using NetTopologySuite.Geometries;
// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VmsController : ControllerBase
    {
        private readonly VmsDbContext _context;

        public VmsController(VmsDbContext context)
        {
            _context = context;
        }

        // GET: api/Vms
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Vehicle>>> GetVehicles()
        {
            return await _context.Vehicles.Where(x => x.isDeleted == false).OrderByDescending(x=>x.UpdatedAt).ToListAsync();
        }

        // GET api/Vms/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Vehicle>> GetVehicle(int id)
        {
            return await _context.Vehicles.FirstOrDefaultAsync(x => x.Id == id && x.isDeleted == false);
        }

        // POST api/Vms
        [HttpPost]
        public async Task<ActionResult<Vehicle>> PostVehicle(Vehicle vehicle)
        {
            _context.Vehicles.Add(vehicle);
            await _context.SaveChangesAsync();

            return CreatedAtAction("PostVehicle", new { id = vehicle.Id }, vehicle);
        }

        // PUT api/Vms/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutVehicle(int id, Vehicle vehicle)
        {
            vehicle.Id = id;
            _context.Entry(vehicle).State = EntityState.Modified;
            _context.Entry(vehicle).Property(x => x.CreatedAt).IsModified = false;

            try {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException) {
                if (!VehicleExists(id))
                {
                    return NotFound();
                }
                else {
                    throw;
                }
            }
            return NoContent();
        }

        // DELETE api/Vms/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Vehicle>> DeleteVehicle(int id)
        {
            var vehicle = await _context.Vehicles.FindAsync(id);

            if (vehicle == null) {
                return NotFound();
            }

            //Hard delete
            //_context.Vehicles.Remove(vehicle);

            //instead doing soft delete
            vehicle.isDeleted = true;
            _context.Entry(vehicle).Property(x => x.isDeleted).IsModified = true;
            await _context.SaveChangesAsync();

            return vehicle;
        }

        private bool VehicleExists(int id)
        {
            return _context.Vehicles.Any(x => x.Id == id);
        }
    }
}
