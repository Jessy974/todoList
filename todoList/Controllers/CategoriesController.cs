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

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            //ajoute notre catégorie dans notre db par le db context.
            db.Categories.Add(category);

            //sauvegarde dans la bd
            db.SaveChanges();


            return CreatedAtRoute("DefaultApi", new { id = category.ID }, category);


        }
        //méthode GET
        /*
        [ResponseType(typeof(Category))]
        public List<Category> GetCategories()
        {
            return db.Categories.ToList();

        }*/
        //2ème méthode 
        public IQueryable<Category> GetCategories()
        {
            return db.Categories;
        }
        //Méthode GET par ID
        /*
        [ResponseType(typeof(Category))]
        public IHttpActionResult GetCategories(int id)
        {
            var idcat = db.Categories.SingleOrDefault(x => x.ID == id);
            if (idcat == null)
            {
                return NotFound();
            }
            return Ok(idcat);
        }*/
        //2ème méthode
        [ResponseType(typeof(Category))]
        public IHttpActionResult GetCategories(int id)
        {
            //find : trouve un élément par son identifiant
            var category = db.Categories.Find(id);
            if (category == null)
            {
                return NotFound();
            }


            return Ok(category);
        }

        //Méthode PUT

        [ResponseType(typeof(Category))]
        public IHttpActionResult PutCategories(int id, Category category)
        {
            if (id != category.ID)
            {
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Entry(category).State = System.Data.Entity.EntityState.Modified;


            //gérer une exception si l'objet n'existe pas en base.
            try
            {
                db.SaveChanges();
            }
            catch (Exception)
            {
                return NotFound();
            }

            return StatusCode(HttpStatusCode.NoContent);

            /*var nameAMod = db.Categories.SingleOrDefault(x => x.ID == id);
            if (nameAMod == null)
            {
                return NotFound();
            }*/

           
           

        }
        [ResponseType(typeof(Category))]
        public IHttpActionResult DeleteCategories(int id)
        {
            var category = db.Categories.Find(id);
            if (category == null)
            {
                return NotFound();
            }
            db.Categories.Remove(category);

            db.SaveChanges();
            return Ok(category);
        }
        //réécriture de la méthode "dispose" pour libérer en mémoire le DbContext et donc la connexion
        //methode Dispose appelée lorsque que IIS n'utilise plus le controller
        protected override void Dispose(bool disposing)
        {
            this.db.Dispose();//libère le db context
            base.Dispose(disposing);
        }
    }
}
