using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BookStore_API.DTOs
{
    /// <summary>
    /// Author data transfer object.
    /// </summary>
    public class AuthorDTO
    {
        /// <summary>
        /// Gets or sets the unique identifier for the author.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets this author's first name.
        /// </summary>
        public string Firstname { get; set; }

        /// <summary>
        /// Gets or sets this author's surname.
        /// </summary>
        public string Lastname { get; set; }

        /// <summary>
        /// Gets or sets this author's bio.
        /// </summary>
        public string Bio { get; set; }

        /// <summary>
        /// Gets or sets a list of all the book objects associated with this
        /// author.
        /// </summary>
        public virtual IList<BookDTO> Books { get; set; }
    }

    /// <summary>
    /// Author create data transfer object.
    /// </summary>
    public class AuthorCreateDTO
    {
        /// <summary>
        /// Gets or sets this author's first name.
        /// </summary>
        [Required]
        public string Firstname { get; set; }

        /// <summary>
        /// Gets or sets this author's surname.
        /// </summary>
        [Required]
        public string Lastname { get; set; }

        /// <summary>
        /// Gets or sets this author's bio.
        /// </summary>
        public string Bio { get; set; }
    }

    /// <summary>
    /// Author update data transfer object.
    /// </summary>
    public class AuthorUpdateDTO
    {
        /// <summary>
        /// Gets or sets the unique identifier for the author.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets this author's first name.
        /// </summary>
        [Required]
        public string Firstname { get; set; }

        /// <summary>
        /// Gets or sets this author's surname.
        /// </summary>
        [Required]
        public string Lastname { get; set; }

        /// <summary>
        /// Gets or sets this author's bio.
        /// </summary>
        public string Bio { get; set; }
    }
}
