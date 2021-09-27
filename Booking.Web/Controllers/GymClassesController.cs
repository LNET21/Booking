using System;
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
using Booking.Core.Models.ViewModels.GymClasses;
using AutoMapper;
using Booking.Web.Filters;

namespace Booking.Web.Controllers
{
    public class GymClassesController : Controller
    {
        private readonly IMapper mapper;
        private readonly IUnitOfWork uow;
        private readonly UserManager<ApplicationUser> userManager;

        public GymClassesController(UserManager<ApplicationUser> userManager, IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.mapper = mapper;
            uow = unitOfWork;
            this.userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
        }

        // GET: GymClasses
        [AllowAnonymous]
        public async Task<IActionResult> Index(IndexViewModel vm)
        {
            if (!User.Identity.IsAuthenticated)
                return View(mapper.Map<IndexViewModel>(await uow.GymClassRepository.GetAsync()));

            var data = vm.ShowHistory ?
                       await uow.GymClassRepository.GetHistoryAsync() :
                       await uow.GymClassRepository.GetWithAttendingAsync();

            return View(mapper.Map<IndexViewModel>(data));
        }

       

       // [Authorize]
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

                uow.AppUserGymClassRepository.Add(booking);
            }
            else
            {
                uow.AppUserGymClassRepository.Remove(attending);
            }

            await uow.CompleteAsync();


            return RedirectToAction(nameof(Index));
        }



        // GET: GymClasses/Details/5
        [RequiredParameterRequiredModel("Id)")]
        public async Task<IActionResult> Details(int? id)
        {
            GymClass gymClass = await uow.GymClassRepository.GetAsync((int)id);
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
        [Authorize(Roles ="Admin")]
        public IActionResult Create()
        {
            return Request.IsAjax() ? PartialView("CreatePartial") : View();
        }

        // POST: GymClasses/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create([Bind("Id,Name,StartDate,Duration,Description")] GymClass gymClass)
        {
            if (ModelState.IsValid)
            {
                uow.GymClassRepository.Add(gymClass);
                await uow.CompleteAsync();

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
        [Authorize(Roles = "Admin")]
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
        [Authorize(Roles = "Admin")]
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
                    uow.GymClassRepository.Update(gymClass);
                    await uow.CompleteAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!await GymClassExists(gymClass.Id))
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
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> NewEdit(int? id)
        {
            if (id is null)
                return BadRequest();


            var gymClass = await uow.GymClassRepository.FindAsync(id);

            if (await TryUpdateModelAsync(gymClass, "", g => g.Name, g => g.Duration))
            {
                try
                {
                    // _context.Update(gymClass);
                    await uow.CompleteAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!await GymClassExists(gymClass.Id))
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
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var gymClass = await uow.GymClassRepository.GetAsync((int)id);

            if (gymClass == null)
            {
                return NotFound();
            }

            return View(gymClass);
        }

        // POST: GymClasses/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var gymClass = await uow.GymClassRepository.FindAsync((int)id);
            uow.GymClassRepository.Remove(gymClass);
            await uow.CompleteAsync();
            return RedirectToAction(nameof(Index));
        }

        private async Task<bool> GymClassExists(int id)
        {
            return await uow.GymClassRepository.AnyAsync(id);
        }
    }
}
