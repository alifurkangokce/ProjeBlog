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
        public Galery()
        {
            GaleryFiles = new HashSet<GaleryFile>();
        }
        [Display(Name ="Başlık")]
        public string Title { get; set; }
        [Display(Name = "Fotograf")]
        public string Photo { get; set; }
        public virtual ICollection<GaleryFile> GaleryFiles{ get; set; }
    }
}
