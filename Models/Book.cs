using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Globalization;
using System.Web.Security;

namespace MyStore.Models
{
    public class Book
    {
        [Key]
        public int BookId { get; set; }

        public string Name { get; set; }
        public string Author { get; set; }
        public int Price { get; set; }
        public string Description { get; set; }
        public string ImagePath { get; set; }
        public int CategorieId { get; set; }
        public DateTime Date { get; set; }
    }
}