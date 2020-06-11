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
    /// Interacts with the books table.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly IBookRepository _bookRepository;
        private readonly ILoggerService _logger;
        private readonly IMapper _mapper;

        public BooksController(IBookRepository bookRepository,
            ILoggerService logger,
            IMapper mapper)
        {
            _bookRepository = bookRepository;
            _logger = logger;
            _mapper = mapper;
        }

        /// <summary>
        /// Get all books.
        /// </summary>
        /// <returns>A list of books.</returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetBooks()
        {
            string location = GetControllerActionNames();

            try
            {
                _logger.LogInfo($"{location}: Attempted call.");
                IList<Book> books = await _bookRepository.FindAll();
                IList<BookDTO> response = _mapper.Map<IList<BookDTO>>(books);
                _logger.LogInfo($"{location}: Successful.");

                return Ok(response);
            }
            catch (Exception e)
            {
                return InternalError($"{location}: {e.Message} - {e.InnerException}");
            }
        }

        /// <summary>
        /// Gets a book by Id.
        /// </summary>
        /// <param name="Id"></param>
        /// <returns>A book record.</returns>
        [HttpGet("{Id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetBook(int Id)
        {
            string location = GetControllerActionNames();

            try
            {
                _logger.LogInfo($"{location}: Attempted call for Id: {Id}.");
                Book book = await _bookRepository.FindById(Id);

                if (book == null)
                {
                    _logger.LogWarn($"{location}: Failed to retrieve record with Id: {Id}.");
                    
                    return NotFound();
                }

                BookDTO response = _mapper.Map<BookDTO>(book);
                _logger.LogInfo($"{location}: Successful got record with Id: {Id}.");

                return Ok(response);
            }
            catch (Exception e)
            {
                return InternalError($"{location}: {e.Message} - {e.InnerException}");
            }
        }

        /// <summary>
        /// Creates a new book.
        /// </summary>
        /// <param name="bookDTO"></param>
        /// <returns>Book object.</returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Create([FromBody] BookCreateDTO bookDTO)
        {
            string location = GetControllerActionNames();

            try
            {
                _logger.LogInfo($"{location}: Create attempted.");

                if (bookDTO == null)
                {
                    _logger.LogWarn($"{location}: Empty request was submitted.");

                    return BadRequest(ModelState);
                }

                if (!ModelState.IsValid)
                {
                    _logger.LogWarn($"{location}: Data was incomplete.");

                    return BadRequest(ModelState);
                }

                Book book = _mapper.Map<Book>(bookDTO);
                bool isSuccess = await _bookRepository.Create(book);

                if (!isSuccess)
                {
                    return InternalError($"{location}: Creation failed.");
                }

                _logger.LogInfo($"{location}: Creation was successful.");
                _logger.LogInfo($"{location}: {book}");

                return Created("Create", new { book } );
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

            return StatusCode(500, "Something went wrong. Please contact the administrator.");
        }
    }
}
