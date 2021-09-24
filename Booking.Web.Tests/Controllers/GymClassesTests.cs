using AutoMapper;
using Booking.Core.Models.Entities;
using Booking.Core.Models.ViewModels.GymClasses;
using Booking.Core.Repositories;
using Booking.Data.Data;
using Booking.Web.Controllers;
using Booking.Web.Tests.Helpers;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;

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
                var profile = new MapperProfile();
                cfg.AddProfile(profile);
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
            var actual = (controller.Index(vm).Result as ViewResult).Model as IndexViewModel;

            //Assert
            Assert.IsInstanceOfType(actual, typeof(IndexViewModel));

        }

        private IEnumerable<GymClass> GetGymClasses()
        {
            return null;
        }
    }
}
