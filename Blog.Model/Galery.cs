using Blog.Service;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Model
{
   public class Galery:BaseEntity
    {
        [Display(Name ="Başlık")]
        public string Title { get; set; }
        [Display(Name = "Fotograf")]
        public string Photo { get; set; }
    }
}
