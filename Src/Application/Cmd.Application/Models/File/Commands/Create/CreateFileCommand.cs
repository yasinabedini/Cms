using Cmd.Application.Common.Commands;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cmd.Application.Models.File.Commands.Create
{
    public class CreateFileCommand : ICommand
    {
        public string Name { get;  set; }
        public string DisplayName { get; set; }
        public long GalleryId { get;  set; }
        public int Length { get;  set; }
        public string Extension { get;  set; }
        public long TypeId { get; set; }
        public CreateFileCommand()
        {
            
        }

        public CreateFileCommand(string name, string displayName, long galleryId, int length, string extension, long typeId)
        {
            Name = name;
            DisplayName = displayName;
            GalleryId = galleryId;
            Length = length;
            Extension = extension;
            TypeId = typeId;
        }
    }
}
