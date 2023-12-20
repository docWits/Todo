using AutoMapper;
using Notes.Application.Common.Mapping;
using Notes.Application.Notes.Commands.UpdateNote;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
namespace Notes.WebApi2.Models
{
    public class UpdateNoteDto : IMapWith<UpdateNoteCommand>
    {

        public Guid Id { get; set; }
        public string Details { get; set; }
        public string Title { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<UpdateNoteDto, UpdateNoteCommand>()
                .ForMember(noteVm => noteVm.Id, opt => opt.MapFrom(note => note.Id))
                .ForMember(noteVm => noteVm.Title, opt => opt.MapFrom(note => note.Title))
                .ForMember(noteVm => noteVm.Details, opt => opt.MapFrom(note => note.Details));
        }
    }
}
