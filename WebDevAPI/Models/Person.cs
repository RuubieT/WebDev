using System.ComponentModel.DataAnnotations;
using System;

namespace WebDevAPI.Models
{
    public class Person
    {
        [Key] public Guid Id { get; set; }
        [Required] public string Name { get; set; }
        public string Description { get; set; }
        public int Age { get; set; }

        public Person()
        {

        }
    }
}
