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
            try
            {
                _logger.LogInfo("Attempted to get all authors.");
                IList<Author> authors = await _authorRepository.FindAll();
                IList<AuthorDTO> response = _mapper.Map<IList<AuthorDTO>>(authors);
                _logger.LogInfo("Successfully got all authors.");
                return Ok(response);    
            }
            catch (Exception e)
            {
                return InternalError($"{e.Message} - {e.InnerException}");
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
            try
            {
                _logger.LogInfo("Attempted to get author with Id:{Id}");
                Author author = await _authorRepository.FindById(Id);
                if (author == null)
                {
                    _logger.LogWarn($"Author with Id:{Id} was not found.");
                    return NotFound();
                }
                AuthorDTO response = _mapper.Map<AuthorDTO>(author);
                _logger.LogInfo($"Successfully got Id:{Id}");
                return Ok(response);
            }
            catch (Exception e)
            {
                return InternalError($"{e.Message} - {e.InnerException}");
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
            try
            {
                _logger.LogInfo($"Author submission attempted.");
                
                if (authorDTO == null)
                {
                    _logger.LogWarn($"Empty request was submitted.");
                    return BadRequest(ModelState);
                }

                if (!ModelState.IsValid)
                {
                    _logger.LogWarn($"Author data incomplete.");
                    return BadRequest(ModelState);
                }
                
                Author author = _mapper.Map<Author>(authorDTO);
                bool isSuccess = await _authorRepository.Create(author);
                
                if (!isSuccess)
                {
                    return InternalError($"Author creation failed.");
                }

                _logger.LogInfo("Author Created");
                return Created("Create", new { author });
            }
            catch (Exception e)
            {
                return InternalError($"{e.Message} - {e.InnerException}");
            }
        }

        /// <summary>
        /// Updates and author's record.
        /// </summary>
        /// <param name="Id"></param>
        /// <param name="authorDTO"></param>
        /// <returns></returns>
        [HttpPut("{Id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Update(int Id, [FromBody] AuthorUpdateDTO authorDTO)
        {
            try
            {
                _logger.LogInfo($"Author with Id: {Id} update attempted");

                if (Id < 1 || authorDTO == null || Id != authorDTO.Id)
                {
                    _logger.LogWarn($"Author update failed with bad data");
                    return BadRequest();
                }

                bool isExists = await _authorRepository.isExists(Id);

                if (!isExists)
                {
                    _logger.LogWarn($"Author with Id: {Id} was not found");
                    return NotFound();
                }


                if (!ModelState.IsValid)
                {
                    _logger.LogWarn($"Author data was incomplete");
                    return BadRequest(ModelState);
                }

                Author author = _mapper.Map<Author>(authorDTO);
                bool isSuccess = await _authorRepository.Update(author);

                if (!isSuccess)
                {
                    return InternalError($"Update operation failed.");
                }

                _logger.LogInfo($"Author with Id: {Id} successfully updated");
                return NoContent();
            }
            catch (Exception e)
            {
                return InternalError($"{e.Message} - {e.InnerException}");
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
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Delete(int Id)
        {
            try
            {
                _logger.LogInfo($"Author with Id: {Id} delete attempted");

                if (Id < 1)
                {
                    _logger.LogWarn($"Author delete failed with bad data");
                    return BadRequest();
                }

                bool isExists = await _authorRepository.isExists(Id);

                if (!isExists)
                {
                    _logger.LogWarn($"Author with Id: {Id} was not found");
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
                return InternalError($"{e.Message} - {e.InnerException}");
            }
        }

        private ObjectResult InternalError(string message)
        {
            _logger.LogError(message);
            return StatusCode(500, "Something went wrong. Please contact the Administrator.");
        }
    }
}
