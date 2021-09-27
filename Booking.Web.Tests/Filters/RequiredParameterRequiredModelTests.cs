using Booking.Web.Controllers;
using Booking.Web.Filters;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Routing;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking.Web.Tests.Filters
{
    [TestClass]
    public class RequiredParameterRequiredModelTests
    {
        [TestMethod]
        public void OnActionExcecuting_NullId_ShouldReturnBadRequest()
        {
            var actionExcecutionContext = GetContext("Id");

            var filter = new RequiredParameterRequiredModel("Id");
            filter.OnActionExecuting(actionExcecutionContext);

            var result = actionExcecutionContext.Result;

            result.ShouldBeOfType<BadRequestResult>();
           // Assert.IsInstanceOfType(result, typeof(BadRequestResult));
        }

        private ActionExecutingContext GetContext(string parameterName)
        {
            var routeValues = new RouteValueDictionary();
            routeValues.Add(parameterName, null);

            var routeData = new RouteData(routeValues);

            var actionContext = new ActionContext(
                Mock.Of<HttpContext>(),
                routeData,
                Mock.Of<ActionDescriptor>());

            var controller = new Mock<GymClassesController>();

            var actionExcecutingContext = new ActionExecutingContext(
                actionContext,
                new List<IFilterMetadata>(),
                routeValues,
                controller);
               

            return actionExcecutingContext;
        }
    }
}
