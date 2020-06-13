using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using BookStore_API.Contracts;
using BookStore_API.Data;
using BookStore_API.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookStore_API.Controllers
{
    /// <summary>
    /// Endpoint used to interact with the Authors in the book store's database.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public class AuthorsController : ControllerBase
    {
        private readonly IAuthorRepository _authorRepository;
        private readonly ILoggerService _logger;
        private readonly IMapper _mapper;

        public AuthorsController(IAuthorRepository authorRepository,
            ILoggerService logger,
            IMapper mapper)
        {
            _authorRepository = authorRepository;
            _logger = logger;
            _mapper = mapper;
        }

        /// <summary>
        /// Get all authors.
        /// </summary>
        /// <returns>List of authors.</returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAuthors()
        {
            string location = GetControllerActionNames();

            try
            {
                _logger.LogInfo($"{location}: Attempted call.");
                IList<Author> authors = await _authorRepository.FindAll();
                IList<AuthorDTO> response = _mapper.Map<IList<AuthorDTO>>(authors);
                _logger.LogInfo($"{location}: Successful.");

                return Ok(response);    
            }
            catch (Exception e)
            {
                return InternalError($"{location}: {e.Message} - {e.InnerException}");
            }
        }

        /// <summary>
        /// Get an author by their Id.
        /// </summary>
        /// <param name="Id"></param>
        /// <returns>An author's record.</returns>
        [HttpGet("{Id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAuthor(int Id)
        {
            string location = GetControllerActionNames();

            try
            {
                _logger.LogInfo($"{location}: Attempted call for Id: {Id}.");
                Author author = await _authorRepository.FindById(Id);

                if (author == null)
                {
                    _logger.LogWarn($"{location}: Failed to retrieve record with Id: {Id}.");

                    return NotFound();
                }

                AuthorDTO response = _mapper.Map<AuthorDTO>(author);
                _logger.LogInfo($"{location}: Successful got record with Id: {Id}.");

                return Ok(response);
            }
            catch (Exception e)
            {
                return InternalError($"{location}: {e.Message} - {e.InnerException}");
            }
        }

        /// <summary>
        /// Creates an author.
        /// </summary>
        /// <param name="author"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Create([FromBody] AuthorCreateDTO authorDTO)
        {
            string location = GetControllerActionNames();

            try
            {
                _logger.LogInfo($"{location}: Create attempted.");
                
                if (authorDTO == null)
                {
                    _logger.LogWarn($"{location}: Empty request was submitted.");

                    return BadRequest(ModelState);
                }

                if (!ModelState.IsValid)
                {
                    _logger.LogWarn($"{location}: Data was incomplete.");

                    return BadRequest(ModelState);
                }
                
                Author author = _mapper.Map<Author>(authorDTO);
                bool isSuccess = await _authorRepository.Create(author);
                
                if (!isSuccess)
                {
                    return InternalError($"{location}: Creation failed.");
                }

                _logger.LogInfo($"{location}: Creation was successful.");
                _logger.LogInfo($"{location}: {author}");

                return Created("Create", new { author });
            }
            catch (Exception e)
            {
                return InternalError($"{location}: {e.Message} - {e.InnerException}");
            }
        }

        /// <summary>
        /// Updates an author's record.
        /// </summary>
        /// <param name="Id"></param>
        /// <param name="authorDTO"></param>
        /// <returns></returns>
        [HttpPut("{Id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Update(int Id, [FromBody] AuthorUpdateDTO authorDTO)
        {
            string location = GetControllerActionNames();

            try
            {
                _logger.LogInfo($"{location}: Update attempted on record with Id: {Id}.");

                if (Id < 1 || authorDTO == null || Id != authorDTO.Id)
                {
                    _logger.LogWarn($"{location}: Update failed with bad data - Id: {Id}.");

                    return BadRequest();
                }

                bool isExists = await _authorRepository.isExists(Id);

                if (!isExists)
                {
                    _logger.LogWarn($"{location}: Failed to retrieve record with Id: {Id}.");

                    return NotFound();
                }

                if (!ModelState.IsValid)
                {
                    _logger.LogWarn($"{location}: Data was incomplete.");

                    return BadRequest(ModelState);
                }

                Author author = _mapper.Map<Author>(authorDTO);
                bool isSuccess = await _authorRepository.Update(author);

                if (!isSuccess)
                {
                    return InternalError($"{location}: Update failed for record with Id: {Id}.");
                }

                _logger.LogInfo($"{location}: Record with Id: {Id} successfully updated.");

                return NoContent();
            }
            catch (Exception e)
            {
                return InternalError($"{location}: {e.Message} - {e.InnerException}");
            }
        }

        /// <summary>
        /// Removes an author by Id.
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [HttpDelete("{Id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Delete(int Id)
        {
            string location = GetControllerActionNames();

            try
            {
                _logger.LogInfo($"{location}: Delete attempted on record with Id: {Id}.");

                if (Id < 1)
                {
                    _logger.LogWarn($"{location}: Delete failed with bad data - Id: {Id}");

                    return BadRequest();
                }

                bool isExists = await _authorRepository.isExists(Id);

                if (!isExists)
                {
                    _logger.LogWarn($"{location}: Failed to retrieve record with Id: {Id}.");

                    return NotFound();
                }

                Author author = await _authorRepository.FindById(Id);
                bool isSuccess = await _authorRepository.Delete(author);

                if (!isSuccess)
                {
                    return InternalError($"Author delete failed");
                }

                _logger.LogInfo($"Author with Id: {Id} successfully deleted");

                return NoContent();
            }
            catch (Exception e)
            {
                return InternalError($"{location}: {e.Message} - {e.InnerException}");
            }
        }

        private string GetControllerActionNames()
        {
            string controller = ControllerContext.ActionDescriptor.ControllerName;
            string action = ControllerContext.ActionDescriptor.ActionName;
            
            return $"{controller} - {action}";
        }

        private ObjectResult InternalError(string message)
        {
            _logger.LogError(message);
            return StatusCode(500, "Something went wrong. Please contact the Administrator.");
        }
    }
}
