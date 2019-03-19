using Blog.Service;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Model
{
   public class GaleryFile:BaseEntity
    {
        public string FileName { get; set; }
        public Guid GaleryId { get; set; }
        [ForeignKey("GaleryId")]
        public virtual Galery Galery{ get; set; }
    }
}
