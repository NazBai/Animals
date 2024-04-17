﻿using System.ComponentModel.DataAnnotations;

namespace Animals.Models
{
    public class Animal
    {
        [Key]
        public int IdAnimal { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public string category { get; set; }
        public string area { get; set; }
    }
}
