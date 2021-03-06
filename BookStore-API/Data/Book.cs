﻿using System.ComponentModel.DataAnnotations.Schema;

namespace BookStore_API.Data
{
    /// <summary>
    /// Represents a single book.
    /// </summary>
    [Table("Books")]
    public partial class Book
    {
        /// <summary>
        /// Gets or sets the unique identifier for this book.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the title of this book.
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Gets or sets the year this book was published.
        /// </summary>
        public int? Year { get; set; }

        /// <summary>
        /// Gets or sets the ISBN for this book.
        /// </summary>
        public string Isbn { get; set; }

        /// <summary>
        /// Gets or sets the summary about this book.
        /// </summary>
        public string Summary { get; set; }

        /// <summary>
        /// Gets or sets the url pointing to the location of this book's image.
        /// </summary>
        public string Image { get; set; }

        /// <summary>
        /// Gets or sets the price of this book.
        /// </summary>
        public decimal? Price { get; set; }

        /// <summary>
        /// Gets or sets the unique identifier of the author of this book.
        /// </summary>
        public int? AuthorId { get; set; }

        /// <summary>
        /// Gets or sets the author object of this book.
        /// </summary>
        public virtual Author Author { get; set; }    
    }
}