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
