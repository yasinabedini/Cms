using Cms.Domain.Common.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cms.Domain.Models.News.Entities
{
    public class Asnad : Entity
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string ImageName { get; set; }

        public Asnad() { }

        public Asnad(long id, string title, string description, string imageName)
        {
            Id = id;
            Title = title;
            Description = description;
            ImageName = imageName;
        }

        public static Asnad Create(long id, string title, string description, string imageName)
        {
            return new Asnad(id, title, description, imageName);
        }
    }
}
