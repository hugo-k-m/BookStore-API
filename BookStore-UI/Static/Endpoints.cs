namespace BookStore_UI.Static
{
    public static class Endpoints
    {
        /// <summary>
        /// Stores the API url.
        /// </summary>
        /// <value></value>
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

        /// <summary>
        /// Stores the registration endpoint url.
        /// </summary>
        /// <value></value>
        public static string LoginEndpoint  = $"{BaseUrl}api/users/login/";

        /// <summary>
        /// Stores the registration endpoint url.
        /// </summary>
        /// <value></value>
        public static string RegisterEndpoint  = $"{BaseUrl}api/users/register/";
    }
}
