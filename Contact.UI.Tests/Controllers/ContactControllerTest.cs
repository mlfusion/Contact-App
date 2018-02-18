using System;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Contact.UI.Controllers;
using Contact.UI.Controllers.Mvc;
using System.Collections.Generic;

namespace Contact.UI.Tests.Controllers
{
    [TestClass]
    public class ContactControllerTest
    {
        private ViewModels.ContactViewModel viewModel;

        public ContactControllerTest()
        {
            Contact.UI.Mapping.AutoMApperConfiguration.Configure();
        }

        /// <summary>
        /// Test Select Contacts
        /// </summary>
        [TestMethod]
        public void SelectContactsTestMethod()
        {

            ContactController controller = new ContactController();

            JsonResult result = controller.GetContacts() as JsonResult;

            Assert.IsNotNull(result);
        }

        /// <summary>
        /// Test Select Contact
        /// </summary>
        [TestMethod]
        public void SelectContactTestMethod()
        {

            ContactController controller = new ContactController();

            JsonResult result = controller.GetContact(1) as JsonResult;

            Assert.IsNotNull(result);
        }

        /// <summary>
        /// Test Add Contact with mock data
        /// </summary>
        /// <returns></returns>
        private ViewModels.ContactViewModel Contacts()
        {
            viewModel = new ViewModels.ContactViewModel();

            viewModel.FirstName = "Bonny";
            viewModel.LastName = "Black";
            viewModel.Email = "bonny@yahoo.com";
            viewModel.Birthdate = DateTime.Now;
            viewModel.Groups = new List<ViewModels.GroupViewModel>
            //viewModel.ContactGroups = new List<ViewModels.ContactGroupViewModel>
            {
                new ViewModels.GroupViewModel() { Id = 11}
            };
            viewModel.Comments = "This is a test at " + DateTime.Now;

            return viewModel;
        }

        [TestMethod]
        public void AddContactTestMethod()
        {

            ContactController controller = new ContactController();

            JsonResult result = controller.AddContact(Contacts()) as JsonResult;

            Assert.IsNotNull(result);
        }

        /// <summary>
        /// Test Update Contact with mock data
        /// </summary>
        /// <returns></returns>
        private ViewModels.ContactViewModel UpdateContact()
        {
            viewModel = new ViewModels.ContactViewModel();

            viewModel.Id = 9;
            viewModel.FirstName = "Billy";
            viewModel.LastName = "Man";
            viewModel.Email = "catdog2@yahoo.com";
            viewModel.Birthdate = DateTime.Now;
            viewModel.Groups = new List<ViewModels.GroupViewModel>
            //viewModel.ContactGroups = new List<ViewModels.ContactGroupViewModel>
            {
               // new ViewModels.GroupViewModel() { Id = 2},
                 new ViewModels.GroupViewModel() { Id = 1}
            };
            viewModel.Comments = "This is a test at " + DateTime.Now;

            return viewModel;
        }

        [TestMethod]
        public void UpdateContactTestMethod()
        {

            ContactController controller = new ContactController();

            JsonResult result = controller.UpdateContact(UpdateContact()) as JsonResult;

            Assert.IsNotNull(result);
        }

        /// <summary>
        /// Test Delete Contact with mock contact.Id
        /// </summary>
        [TestMethod]
        public void DeleteContactTestMethod()
        {

            ContactController controller = new ContactController();

            JsonResult result = controller.DeleteContact(2) as JsonResult;

            Assert.IsNotNull(result);
        }
    }
}
