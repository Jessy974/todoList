﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using todoList.Data;
using todoList.Models;

namespace todoList.Controllers
{
    [RoutePrefix("api/todos")]
    public class TodosController : ApiController
    {
        private TodoListDbContext db = new TodoListDbContext();

        // GET: api/Todos
        /// <summary>
        /// Retourne la liste des Todos
        /// </summary>
        /// <remarks>
        /// Il fait chaud !
        /// </remarks>
        /// <returns></returns>
        public IQueryable<Todo> GetTodos()
        {
            return db.Todos.Include(x => x.Category);
        }

        // GET: api/Todos/5
        [Route("id:int")]
        [ResponseType(typeof(Todo))]
        public IHttpActionResult GetTodo(int id)
        {
            Todo todo = db.Todos.Find(id);
            if (todo == null)
            {
                return NotFound();
            }

            return Ok(todo);
        }

        //GET : api/todos/search
        [Route("search")]
        public IQueryable<Todo> GetTodo(string name = "", int? categoryId = null, bool? done = null, DateTime? deadline = null)
        {
            var query = db.Todos.Where(x => !x.Deleted);

            if (!string.IsNullOrWhiteSpace(name))
                query = query.Where(x => x.Name.Contains(name));
               

            if (categoryId != null)
                query = query.Where(x => x.CategoryID == categoryId);

            if (done != null)
                query = query.Where(x => x.Done == done);

            if (deadline != null)
                query = query.Where(x => x.DeadLineDate == deadline);

            return query;




        }

        // PUT: api/Todos/5
        [Route("id:int")]
        [ResponseType(typeof(void))]
        public IHttpActionResult PutTodo(int id, Todo todo)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != todo.ID)
            {
                return BadRequest();
            }
            if (db.Todos.Find(id).Deleted)
            {
                return BadRequest();
            }

            db.Entry(todo).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TodoExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Todos
        [ResponseType(typeof(Todo))]
        public IHttpActionResult PostTodo(Todo todo)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Todos.Add(todo);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = todo.ID }, todo);
        }

        // DELETE: api/Todos/5
        [ResponseType(typeof(Todo))]
        public IHttpActionResult DeleteTodo(int id)
        {
            Todo todo = db.Todos.Find(id);
            if (todo == null)
            {
                return NotFound();
            }

            //db.Todos.Remove(todo);
            todo.Deleted = true;
            todo.DeletedAt = DateTime.Now;
            //enregistrement de la suppression en base
            db.Entry(todo).State = System.Data.Entity.EntityState.Modified;

            db.SaveChanges();

            return Ok(todo);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool TodoExists(int id)
        {
            return db.Todos.Count(e => e.ID == id) > 0;
        }
    }
}