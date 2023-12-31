﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using Final_Project.Models;

namespace Final_Project.Controllers
{
    public class TruckDataController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: api/TruckData/listTrucks
        [HttpGet]
        public IEnumerable<TruckDto> ListTrucks()
        {
            List<Truck> Trucks = db.Trucks.ToList();
            List<TruckDto> TruckDtos = new List<TruckDto>();

            Trucks.ForEach(a => TruckDtos.Add(new TruckDto()
            {
                TruckID = a.TruckID,
                TruckNumber = a.TruckNumber
            }));
            return TruckDtos;
        }

        // GET: api/TruckData/FindTrucks/5
        [HttpGet]
        [ResponseType(typeof(Truck))]
        public IHttpActionResult FindTruck(int id)
        {
            Truck Truck = db.Trucks.Find(id);
            TruckDto TruckDto = new TruckDto()
            {
                TruckID = Truck.TruckID,
                TruckNumber = Truck.TruckNumber 
            };
            
            if (Truck == null)
            {
                return NotFound();
            }

            return Ok(TruckDto);
        }

        // POST: api/TruckData/UpdateTruck/5
        [ResponseType(typeof(void))]
        [HttpPost]
        public IHttpActionResult UpdateTruck(int id, Truck truck)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != truck.TruckID)
            {
                return BadRequest();
            }

            db.Entry(truck).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TruckExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/TruckData/AddTruck
        [ResponseType(typeof(Truck))]
        [HttpPost]
        public IHttpActionResult AddTruck(Truck truck)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Trucks.Add(truck);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = truck.TruckID }, truck);
        }

        // POST: api/TruckData/DeleteTruck/5
        [ResponseType(typeof(Truck))]
        [HttpPost]
        public IHttpActionResult DeleteTruck(int id)
        {
            Truck truck = db.Trucks.Find(id);
            if (truck == null)
            {
                return NotFound();
            }

            db.Trucks.Remove(truck);
            db.SaveChanges();

            return Ok();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool TruckExists(int id)
        {
            return db.Trucks.Count(e => e.TruckID == id) > 0;
        }
    }
}