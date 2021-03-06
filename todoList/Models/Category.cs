﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace todoList.Models
{
    public class Category : BaseModel
    {
        //public int ID { get; set; }



        [Required(ErrorMessage ="Le champ nom est obligatoire")]
        [MinLength(5,ErrorMessage ="Nom trop court")]
        [RegularExpression("^[a-z]+$")]

        public string Name { get; set; }

        //public ICollection<Todo> Todos { get; set; }
    }
}