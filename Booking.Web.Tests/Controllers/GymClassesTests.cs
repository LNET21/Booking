using AutoMapper;
using Booking.Core.Models.Entities;
using Booking.Core.Repositories;
using Booking.Data.Data;
using Booking.Web.Controllers;
using Microsoft.AspNetCore.Identity;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace Booking.Web.Tests.Controllers
{
    [TestClass]
    public class GymClassesTests
    {
        private Mock<IGymClassRepository> mockRepo;

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

            var controller = new GymClassesController(userManager, mockUoW.Object, mapper);

        }

        [TestMethod]
        public void Index_NotAutenticated_ReturnsIndexViewModel()
        {
            //Controller
            //User
            //Dataset
            //Mapper
            //UserManager
            //Unit of work
            //Repository

        }
    }
}
