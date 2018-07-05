using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using todoList.Data;
using todoList.Models;

namespace todoList.Controllers
{
    public class CategoriesController : ApiController
    {
        // déclaration et instanciation : ouverture de la connexion à la base de données
        private TodoListDbContext db = new TodoListDbContext();

        [ResponseType(typeof(Category))]
        public IHttpActionResult PostCategory(Category category)
        {
            if ( ! ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Ok();
        }

        //réécriture de la méthode "dispose" pour libérer en mémoire le DbContext et donc la connexion
        protected override void Dispose(bool disposing)
        {
            this.db.Dispose();//libère le db context
            base.Dispose(disposing);
        }
    }
}
