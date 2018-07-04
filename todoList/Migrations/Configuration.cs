using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Web;
using todoList.Data;

namespace todoList.Migrations
{
    public class Configuration : DbMigrationsConfiguration<TodoListDbContext>
    {
        //création d'un constructeur
    public Configuration()
        {
            //migration non automatique
            AutomaticMigrationsEnabled = false;
        }
    }
}