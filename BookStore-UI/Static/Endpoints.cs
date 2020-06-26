namespace BookStore_UI.Static
{
    public static class Endpoints
    {
        /// <summary>
        /// Stores the API url.
        /// </summary>
        public static string BaseUrl = "https://localhost:44372/";

        /// <summary>
        /// Stores the authors endpoint API url.
        /// </summary>
        /// <value></value>
        public static string AuthorsEndpoint = $"{BaseUrl}api/authors/";

        /// <summary>
        /// Stores the books endpoint API url.
        /// </summary>
        /// <value></value>
        public static string BooksEndpoint = $"{BaseUrl}api/books/";
    }
}
