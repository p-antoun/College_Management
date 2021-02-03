﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace College_Management.Models
{
    public class Teacher
    { 
        [Key]
        [ForeignKey("SubjectTeacher")]
        public int SubjectId { get; set; }

        public string Name { get; set; }

        [DataType(DataType.Date)]
        public DateTime Birthday { get; set; }
         
        public int Salary { get; set; }
        public virtual Subject SubjectTeacher { get; set; }
    }
}