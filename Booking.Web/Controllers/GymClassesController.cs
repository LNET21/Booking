﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Booking.Core.Models.Entities;
using Booking.Data.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using Booking.Web.Extensions;
using Booking.Data.Repositories;
using Booking.Core.Repositories;

namespace Booking.Web.Controllers
{
    public class GymClassesController : Controller
    {
        private readonly ApplicationDbContext db;
        private readonly IUnitOfWork uow;

        //private readonly GymClassRepository gymClassRepository;
        //private readonly ApplicationUserGymsRepository appUserGymClassRepository;
        private readonly UserManager<ApplicationUser> userManager;

        public GymClassesController(ApplicationDbContext context, UserManager<ApplicationUser> userManager, IUnitOfWork unitOfWork)
        {
            db = context ?? throw new ArgumentNullException(nameof(context));
            uow = unitOfWork;
            //uow = new UnitOfWork(db);
            //gymClassRepository = new GymClassRepository(context);
            //appUserGymClassRepository = new ApplicationUserGymsRepository(context);
            this.userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
        }

        // GET: GymClasses
        [AllowAnonymous]
        public async Task<IActionResult> Index()
        {
            return base.View(await uow.GymClassRepository.GetAsync());
        }

        

        [Authorize]
        public async Task<IActionResult> BookingToggle(int? id)
        {
            if (id is null) return BadRequest();

            var userId = userManager.GetUserId(User);

            var attending = await uow.AppUserGymClassRepository.FindAsync(id, userId);

            if (attending is null)
            {
                var booking = new ApplicationUserGymClass
                {
                    ApplicationUserId = userId,
                    GymClassId = (int)id
                };

                db.ApplicationUserGyms.Add(booking);
            }
            else
            {
                db.ApplicationUserGyms.Remove(attending);
            }

            await db.SaveChangesAsync();


            return RedirectToAction(nameof(Index));
        }

       

        // GET: GymClasses/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var gymClass = await db.GymClasses
                .FirstOrDefaultAsync(m => m.Id == id);
            if (gymClass == null)
            {
                return NotFound();
            }

            return View(gymClass);
        }

        public ActionResult Fetch()
        {
            return PartialView("CreatePartial");
        } 
        
        public ActionResult Ajax()
        {
            return PartialView("CreatePartial");
        }

        // GET: GymClasses/Create
        public IActionResult Create()
        {
            return Request.IsAjax() ? PartialView("CreatePartial") : View();
        }

        // POST: GymClasses/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,StartDate,Duration,Description")] GymClass gymClass)
        {
            if (ModelState.IsValid)
            {
                db.Add(gymClass);
                await db.SaveChangesAsync();

                if (Request.IsAjax())
                {
                   // return PartialView("GymClassesPartial", await db.GymClasses.ToListAsync());
                    return PartialView("GymClassPartial", gymClass);
                }

                return RedirectToAction(nameof(Index));
            }

            if (Request.IsAjax())
            {
                Response.StatusCode = 500;
                return PartialView("CreatePartial", gymClass);
            }

            return View(gymClass);
        }

        // GET: GymClasses/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var gymClass = await uow.GymClassRepository.FindAsync(id);
            if (gymClass == null)
            {
                return NotFound();
            }
            return View(gymClass);
        }

       

        // POST: GymClasses/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,StartDate,Duration,Description")] GymClass gymClass)
        {
            if (id != gymClass.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    db.Update(gymClass);
                    await db.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!GymClassExists(gymClass.Id))
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
            return View(gymClass);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> NewEdit(int? id)
        {
            if (id is null)
                return BadRequest();


            var gymClass = db.GymClasses.Find(id);

            if (await TryUpdateModelAsync(gymClass, "", g => g.Name, g => g.Duration))
            {
                try
                {
                    // _context.Update(gymClass);
                    await db.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!GymClassExists(gymClass.Id))
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

            return View(gymClass);
        }



        // GET: GymClasses/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var gymClass = await db.GymClasses
                .FirstOrDefaultAsync(m => m.Id == id);
            if (gymClass == null)
            {
                return NotFound();
            }

            return View(gymClass);
        }

        // POST: GymClasses/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var gymClass = await db.GymClasses.FindAsync(id);
            db.GymClasses.Remove(gymClass);
            await db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool GymClassExists(int id)
        {
            return db.GymClasses.Any(e => e.Id == id);
        }
    }
}
