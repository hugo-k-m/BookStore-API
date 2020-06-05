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

        private ObjectResult InternalError(string message)
        {
            _logger.LogError(message);
            return StatusCode(500, "Something went wrong. Please contact the Administrator.");

        }
    }
}
