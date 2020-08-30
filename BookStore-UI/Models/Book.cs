using System.ComponentModel.DataAnnotations;

namespace BookStore_UI.Models
{
    /// <summary>
    /// Book view model.
    /// </summary>
    public class Book
    {
        /// <summary>
        /// Gets or sets the unique identifier for the book.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the title of the book.
        /// </summary>
        [Required]
        public string Title { get; set; }

        /// <summary>
        /// Gets or sets the publish year for the book.
        /// </summary>
        /// <value></value>
        public int? Year { get; set; }

        /// <summary>
        /// Gets or sets the ISBN of the book.
        /// </summary>
        [Required]
        public string Isbn { get; set; }

        /// <summary>
        /// Gets or sets the summary of the book.
        /// </summary>
        [StringLength(150)]
        public string Summary { get; set; }

        /// <summary>
        /// Gets or sets the image url for the book.
        /// </summary>
        public string Image { get; set; }

        /// <summary>
        /// Gets or sets the price of the book.
        /// </summary>
        public decimal? Price { get; set; }
        
        /// <summary>
        /// Gets or sets the unique identifier of the Author of
        /// the book.
        /// </summary>
        [Required]
        public int? AuthorId { get; set; }

        /// <summary>
        /// Gets or sets the author of the book.
        /// </summary>
        /// <value></value>
        public virtual Author Author { get; set; }
    }
}