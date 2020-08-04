using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore_UI.Models
{
    public class Author
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
            [Required]
            [Display(Name = "First Name")]
            public string Firstname { get; set; }

            /// <summary>
            /// Gets or sets this author's surname.
            /// </summary>
            [Required]
            [Display(Name = "Last Name")]
            public string Lastname { get; set; }

            /// <summary>
            /// Gets or sets this author's bio.
            /// </summary>
            [Required]
            [Display(Name = "Biography")]
            [StringLength(250)]
            public string Bio { get; set; }

            /// <summary>
            /// Gets or sets a list of all the book objects associated with this
            /// author.
            /// </summary>
            public virtual IList<Book> Books { get; set; }
        }
    }
}
