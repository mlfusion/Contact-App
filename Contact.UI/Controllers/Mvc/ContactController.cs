using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Contact.UI.Controllers.Mvc
{
    public class ContactController : Controller
    {
        private readonly Contact.Business.Objects.Contact contactObject;
        private readonly Contacts.Model.ContactModel contactModel;

        public ContactController()
        {
            contactObject = new Business.Objects.Contact();
            contactModel = new Contacts.Model.ContactModel();
        }

        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult GetContacts()
        {
            try
            {
                var contactsViewModel =
                        AutoMapper.Mapper.Map<IEnumerable<Contacts.Model.ContactModel>, IEnumerable<ViewModels.ContactViewModel>>(contactObject.SelectContacts());



                return Json(contactsViewModel, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(ex.Message, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpGet]
        public ActionResult GetContact(int id)
        {
            try
            {
                var contactViewModel =
                        AutoMapper.Mapper.Map<Contacts.Model.ContactModel, ViewModels.ContactViewModel>(contactObject.SelectContact(id));

                return Json(contactViewModel, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(ex.Message);
            }
        }

        [HttpPut]
        public ActionResult AddContact(Contact.UI.ViewModels.ContactViewModel contactViewModel)
        {
            try
            {
                if (!ModelState.IsValid)
                    return Json("InValid field was found", JsonRequestBehavior.AllowGet);

                var contactMapper =
                        AutoMapper.Mapper.Map<ViewModels.ContactViewModel, Contacts.Model.ContactModel>(contactViewModel);

                var result = contactObject.AddContact(contactMapper);

                return Json(result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                // log error in error table

                return Json(ex.Message, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public ActionResult UpdateContact(Contact.UI.ViewModels.ContactViewModel contactViewModel)
        {
            try
            {
                if (!ModelState.IsValid)
                    return View();

                var contactMapper =
                        AutoMapper.Mapper.Map<ViewModels.ContactViewModel, Contacts.Model.ContactModel>(contactViewModel);

                return Json(contactObject.UpdateContact(contactMapper));
            }
            catch (Exception ex)
            {
                // log error in error table

                return Json(ex.Message);
            }
        }

        [HttpDelete]
        public ActionResult DeleteContact(int id)
        {
            try
            {
                contactObject.DeleteContact(id);

                return Json(id);

            }
            catch (Exception ex)
            {
                return Json(ex.Message);
            }
        }

    }
}
