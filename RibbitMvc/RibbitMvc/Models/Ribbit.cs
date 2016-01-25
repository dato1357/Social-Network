using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace RibbitMvc.Models
{
    public class Ribbit
    {
        public int Id { get; set; }

        public int AuthorId { get; set; }

        [ForeignKey("AuthorId")]
        public virtual User Author { get; set; }


        public string Status { get; set; }
        public DateTime DateCreated { get; set; }
    }
}