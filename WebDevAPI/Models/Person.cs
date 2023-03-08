using System.ComponentModel.DataAnnotations;
using System;

namespace WebDevAPI.Models
{
    public class Person
    {
        [Key] public Guid Id { get; set; }
        [Required] public string Name { get { return "Ruben"; } }
        public string Description { get { return "ICTER"; } }
        public int Age { get; set; }

        public Person()
        {

        }
    }
}
