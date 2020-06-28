namespace BookStore_UI.Models
{
    public class ResponseModel
    {
        /// <summary>
        /// Gets or sets the success status of the call.
        /// </summary>
        public bool Success { get; set; }

        /// <summary>
        /// Gets or sets the error status of the call should it fail.
        /// </summary>
        public string Error { get; set; }

        /// <summary>
        /// Gets or sets the content of the response.
        /// </summary>
        /// <value></value>
        public string Content { get; set; }
    }
}
