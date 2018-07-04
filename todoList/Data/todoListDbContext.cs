using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using todoList.Models;

namespace todoList.Data
{
    public class TodoListDbContext : DbContext
    {
        //constructeur : base appelle le constructeur de la classe mère dbContext
        public TodoListDbContext() : base("TodoList")
        {
        }

        //ajout d'entités qui se trouvent dans le DbSet
        //public DbSet<Category> Categories { get; set; }
    }
}