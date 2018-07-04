using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using todoList.Models;

namespace todoList.Controllers
{
    public class CategoriesController : ApiController
    {
        [ResponseType(typeof(Category))]
        public IHttpActionResult PostCategory(Category category)
        {
            if ( ! ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Ok();
        }
    }
}
