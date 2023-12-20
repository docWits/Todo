using AutoMapper;
using Notes.Application.Common.Mapping;
using Notes.Application.Notes.Commands.CreateNode;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Notes.WebApi2.Models
{
    public class CreateNoteDto : IMapWith<CreateNoteCommand>
    {
        public string Details { get; set; }
        public string Title { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<CreateNoteDto, CreateNoteCommand>()
                .ForMember(noteVm => noteVm.Title, opt => opt.MapFrom(note => note.Title))
                .ForMember(noteVm => noteVm.Details, opt => opt.MapFrom(note => note.Details));
        }
    }
}
