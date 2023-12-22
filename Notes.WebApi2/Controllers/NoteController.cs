using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Notes.Application.Notes.Commands.CreateNode;
using Notes.Application.Notes.Commands.DeleteCommand;
using Notes.Application.Notes.Commands.UpdateNote;
using Notes.Application.Notes.Queries.GetNoteDetails;
using Notes.Application.Notes.Queries.GetNoteList;
using Notes.WebApi2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Notes.WebApi2.Controllers
{
    [Produces ("application/json")]
    [Route("api/[controller]")]
    public class NoteController:BaseController
    {

        private readonly IMapper _mapper;
        public NoteController(IMapper mapper) => _mapper = mapper;


        /// <summary>
        /// Получить список всех заметок
        /// </summary>
        /// <remarks>
        /// GET /note
        /// </remarks>
        /// <returns> Returns NoteListVm </returns>
        /// <responce code="200">Success</responce>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<NoteListVm>> GetAll()
        {
            var query = new GetNoteListQuery
            {
                UserId = UserId
            };
            var vm = await Mediator.Send(query);
            return Ok(vm);
        }
        /// <summary>
        /// Получить заметку по id
        /// </summary>
        /// <remarks>
        /// GET /note/"id"
        /// </remarks>
        /// <param name="id">Id записи</param>
        /// <returns> Returns NoteDetailsVm </returns>
        /// <responce code="200">Success</responce>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<NoteListVm>> Get(Guid id)
        {
            var query = new GetNoteDetailsQuery
            {
                UserId = UserId,
                Id = id
            };
            var vm = await Mediator.Send(query);
            return Ok(vm);
        }
        /// <summary>
        /// Создать заметку
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// POST /note
        /// {
        ///     title:"note title",
        ///     details: "note details"
        /// }
        /// </remarks>
        /// <param name="createNoteDto">CreateNoteDto object</param>
        /// <returns>Returns id(guid)</returns>
        /// <responce code="200">Success</responce>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<Guid>> Create([FromBody] CreateNoteDto createNoteDto)
        {
            var command = _mapper.Map<CreateNoteCommand>(createNoteDto);
            command.UserId = UserId;
            var noteId = await Mediator.Send(command);
            return Ok(noteId);
        }


        /// <summary>
        /// Обновляет данные заметки
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// PUT /note
        /// {
        ///     title: "updated note title"
        /// }
        /// </remarks>
        /// <param name="updateNoteDto">UpdateNoteDto object</param>
        /// <returns>Returns NoContent</returns>
        ///  <responce code="204">Success</responce>
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> Update([FromBody] UpdateNoteDto updateNoteDto)
        {
            var command = _mapper.Map<UpdateNoteCommand>(updateNoteDto);
            command.UserId = UserId;
            await Mediator.Send(command);
            return NoContent();
        }

        /// <summary>
        /// Удаляет заметку по id
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// DELETE /note/"id"
        /// </remarks>
        /// <param name="id">Id заметки</param>
        /// <returns>Returns NoContent</returns>
        /// <responce code="204">Success</responce>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> Delete(Guid id)
        {
            var command = new DeleteNoteCommand
            {
                Id = id,
                UserId = UserId
            };
            await Mediator.Send(command);
            return NoContent();
        }
    }
}
