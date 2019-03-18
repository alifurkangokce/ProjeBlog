using Blog.Service;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Model
{
   public class PostFile:BaseEntity
    {
        public string FileName { get; set; }
        public Guid PostId { get; set; }
        [ForeignKey("PostId")]
        public virtual Post Post{ get; set; }
    }
}
