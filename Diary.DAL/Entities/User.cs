using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Diary.DAL.Entities
{
    public class User : IdentityUser
    {
        [Required]
        [MaxLength(20)]
        public string Name { get; set; }
        [Required]
        public DateTime JoinDate { get; set; }
        public virtual ICollection<Note> Notes { get; set; }

        public User()
        {
            Notes = new List<Note>();
        }
    }
}
