using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Contact.UI.Controllers;
using Contact.UI.Controllers.Mvc;
using System.Web.Http;

namespace Contact.UI.Tests.Controllers
{
    [TestClass]
    public class GroupControllerTest
    {
        public GroupControllerTest()
        {
            Contact.UI.Mapping.AutoMApperConfiguration.Configure();
        }

        [TestMethod]
        public void AddGroupMethodTest()
        {
            ViewModels.GroupViewModel viewModel = new ViewModels.GroupViewModel();
            viewModel.GroupName = "Cat";

            UI.Controllers.Api.GroupController groupController = new UI.Controllers.Api.GroupController();
            IHttpActionResult result = groupController.AddGroup(viewModel) as IHttpActionResult;

            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void UpdateGroupMethodTest()
        {
            ViewModels.GroupViewModel viewModel = new ViewModels.GroupViewModel();
            viewModel.GroupName = "Catwomen";
            viewModel.Id = 11;

            UI.Controllers.Api.GroupController groupController = new UI.Controllers.Api.GroupController();
            IHttpActionResult result = groupController.UpdateGroup(viewModel) as IHttpActionResult;

            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void DeleteGroupMethodTest()
        {
            ViewModels.GroupViewModel viewModel = new ViewModels.GroupViewModel();
            viewModel.Id = 2;

            UI.Controllers.Api.GroupController groupController = new UI.Controllers.Api.GroupController();
            IHttpActionResult result = groupController.DeleteGroup(viewModel.Id) as IHttpActionResult;

            Assert.IsNotNull(result);
        }
    }
}
