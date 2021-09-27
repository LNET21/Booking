using AutoMapper;
using Booking.Core.Models.Entities;
using Booking.Core.Models.ViewModels.GymClasses;
using Booking.Core.Repositories;
using Booking.Data.Data;
using Booking.Web.Controllers;
using Booking.Web.Tests.Helpers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Booking.Web.Tests.Controllers
{
    [TestClass]
    public class GymClassesTests
    {
        private Mock<IGymClassRepository> mockRepo;
        private GymClassesController controller;

        [TestInitialize]
        public void TestSetUp()
        {
            mockRepo = new Mock<IGymClassRepository>();
            var mockUoW = new Mock<IUnitOfWork>();
            mockUoW.Setup(u => u.GymClassRepository).Returns(mockRepo.Object);

            var mapper = new Mapper(new MapperConfiguration(cfg =>
            {
                cfg.ConstructServicesUsing(t => new AttendingResolver(Mock.Of<HttpContextAccessor>()));
                cfg.AddProfile<MapperProfile>();
            }));

            var mockUserStore = new Mock<IUserStore<ApplicationUser>>();
            var userManager = new UserManager<ApplicationUser>(mockUserStore.Object, null, null, null, null, null, null, null, null);

            controller = new GymClassesController(userManager, mockUoW.Object, mapper);

        }

        [TestMethod]
        public void Index_NotAutenticated_ReturnsIndexViewModel()
        {
            //Arrange
            controller.SetUserIsAuthenticated(false);
            var vm = new IndexViewModel();
            //mockRepo.Setup(r => r.GetAsync()).ReturnsAsync(GetGymClasses());

            //Act
            var actual = (controller.Index(vm).Result as ViewResult)?.Model as IndexViewModel;

            //Assert
            Assert.IsInstanceOfType(actual, typeof(IndexViewModel));

        }

        [TestMethod]
        public void Index_AutenticatedWithGymClasses_ReturnsExpectedCount()
        {
            controller.SetUserIsAuthenticated(true);
            var gymClasses = GetGymClasses();
            var expected = gymClasses.Count();
            mockRepo.Setup(r => r.GetWithAttendingAsync()).ReturnsAsync(gymClasses);

            var vm = new IndexViewModel() { ShowHistory = false };

            var actual = (controller.Index(vm).Result as ViewResult)?.Model as IndexViewModel;

            Assert.AreEqual(expected, actual.GymClasses.Count());
            Assert.IsInstanceOfType(actual, typeof(IndexViewModel));
            
        }

        [TestMethod]
        public void Create_ReturnsDefaultView_ShouldReturnNull()
        {
            controller.SetAjaxRequest(false);

            var actual = controller.Create() as ViewResult;

            Assert.IsNull(actual?.ViewName);
        }    
        
        [TestMethod]
        public void Create_ReturnsPartaialView_ShouldNotBeNull()
        {
            controller.SetAjaxRequest(true);
            const string correctViewName = "CreatePartial";

            var actual = controller.Create() as PartialViewResult;

            //actual?.ViewName.ShouldBeNull();

            Assert.IsNotNull(actual?.ViewName);
            Assert.AreEqual(correctViewName, actual?.ViewName);
        }



        private IEnumerable<GymClass> GetGymClasses()
        {
            return new List<GymClass>
            {
                new GymClass
                {
                    Id = 1,
                    Name = "Spinning",
                    Description = "Easy",
                    StartDate = DateTime.Now.AddDays(3),
                    Duration = new TimeSpan(0,60,0)
                   
                },
                new GymClass
                {
                    Id = 2,
                    Name = "Body Pump",
                    Description = "Hard",
                    StartDate = DateTime.Now.AddDays(-3),
                    Duration = new TimeSpan(0,60,0)
                }
            };
        }
    }
}
