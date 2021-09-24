using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking.Web.Tests.Helpers
{
    public static class ControllerExtensions
    {
        public static void SetUserIsAuthenticated(this Controller controller, bool isAuthenticated)
        {
            var mockContext = new Mock<HttpContext>();
            mockContext.SetupGet(c => c.User.Identity.IsAuthenticated).Returns(isAuthenticated);

            controller.ControllerContext = new ControllerContext { HttpContext = mockContext.Object };

        }
    }
}
