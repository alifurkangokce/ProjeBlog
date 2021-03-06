﻿using Blog.Service;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Model
{
    public class Post:BaseEntity
    {
        public Post()
        {
            PostFiles = new HashSet<PostFile>();
        }

        [Display(Name = "Başlık")]
        public string Title{ get; set; }
        [Display(Name = "Açıklama")]
        public string Description { get; set; }
        public Guid? CategoryId { get; set; }

        public string Photo { get; set; }
        public virtual Category Category { get; set; }

        public virtual ICollection<PostFile> PostFiles { get; set; }

    }
}
