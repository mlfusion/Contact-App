using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Contact.UI.Controllers.Api
{
    [RoutePrefix("api/v2/group")]
    public class GroupController : ApiController
    {
        private readonly Contact.Business.Group groupObject;


        public GroupController()
        {
            groupObject = new Business.Group();
        }


        [HttpGet]
        [Route("~/api/v2/groups")]
        public IHttpActionResult Groups()
        {
            try
            {
                // get group object
                var groups = groupObject.SelectGroups();

                // usr automapper to convert from Models.Group => GroupViewModel
                var groupsViewModel = AutoMapper.Mapper.Map<IEnumerable<Contacts.Model.GroupModel>, IEnumerable<UI.ViewModels.GroupViewModel>>(groups);

               return Ok(groupsViewModel);
            }
            catch (Exception ex)
            {
                return ResponseMessage(Request.CreateResponse(HttpStatusCode.ExpectationFailed, ex.Message));
            }
        }

        [HttpGet]
        [Route("{id:int}")]
        public IHttpActionResult Group(int id)
        {
            try
            {
                // get group object
                var groups = groupObject.SelectGroup(id);

                // usr automapper to convert from Models.Group => GroupViewModel
                var groupsViewModel = AutoMapper.Mapper.Map<IEnumerable<Contacts.Model.ContactModel>, IEnumerable<UI.ViewModels.ContactViewModel>>(groups);

                return Ok(groupsViewModel);
            }
            catch (Exception ex)
            {
                return ResponseMessage(Request.CreateResponse(HttpStatusCode.ExpectationFailed, ex.Message));
            }
        }

        [HttpPut]
        [Route("add")]
        public IHttpActionResult AddGroup(UI.ViewModels.GroupViewModel groupViewModel)
        {
            try
            {
                // usr automapper to convert from GroupViewModel=> Models.Group
                var group = AutoMapper.Mapper.Map<UI.ViewModels.GroupViewModel, Contacts.Model.GroupModel>(groupViewModel);

                // add group object
                var result = groupObject.AddGroup(group);

                return Ok(result);
            }
            catch(Exception ex)
            {
                return ResponseMessage(Request.CreateResponse(HttpStatusCode.ExpectationFailed, ex.Message));
            }
        }
        [HttpPost]
        [Route("update")]
        public IHttpActionResult UpdateGroup(UI.ViewModels.GroupViewModel groupViewModel)
        {
            try
            {
                // usr automapper to convert from GroupViewModel=> Models.Group
                var group = AutoMapper.Mapper.Map<UI.ViewModels.GroupViewModel, Contacts.Model.GroupModel>(groupViewModel);

                // update group object
                var result = groupObject.UpdateGroup(group);

                return Ok(result);
            }
            catch (Exception ex)
            {
                return ResponseMessage(Request.CreateResponse(HttpStatusCode.ExpectationFailed, ex.Message));
            }
        }

        [HttpDelete]
        [Route("delete/{id:int}")]
        public IHttpActionResult DeleteGroup(int id)
        {
            try
            {
                // delete group object
                var status = groupObject.DeleteGroup(id);

                if (status.Contains("Can't"))
                {
                    return BadRequest(status); // Request.CreateResponse(HttpStatusCode.Forbidden, status);
                }

                return Ok(status);
            }
            catch (Exception ex)
            {
                return ResponseMessage(Request.CreateResponse(HttpStatusCode.ExpectationFailed, ex.Message));
            }
        }

        [HttpDelete]
        [Route("delete/{contactId:int}/{groupId:int}")]
        public IHttpActionResult DeleteGroupContact(int contactId, int groupId)
        {
            try
            {
                // delete group object
                var status = groupObject.DeleteGroupContact(contactId, groupId);

                if (status.Contains("No"))
                {
                    return BadRequest(status); // Request.CreateResponse(HttpStatusCode.Forbidden, status);
                }

                return Ok(status);
            }
            catch (Exception ex)
            {
                return ResponseMessage(Request.CreateResponse(HttpStatusCode.ExpectationFailed, ex.Message));
            }
        }
    }
}