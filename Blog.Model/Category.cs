using Blog.Service;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Model
{
   public class Category:BaseEntity
    {
        public Category()
        {
            Posts = new HashSet<Post>();
        }

        [Display(Name="Kategori Adı")]
        public string Name { get; set; }
        [Display(Name="Kategori Açıklama")]
        [DataType(DataType.MultilineText)]
        public string Description{ get; set; }
        public string Photo { get; set; }

        public virtual ICollection<Post> Posts { get; set; }

    }
}
