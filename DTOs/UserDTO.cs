namespace BookStore_API.DTOs
{
    /// <summary>
    /// User data transfer object.
    /// </summary>
    public class UserDTO
    {
        /// <summary>
        /// Gets or sets the user's username.
        /// </summary>
        public string Username { get; set; }

        /// <summary>
        /// Gets or sets the user's password.
        /// </summary>
        public string Password { get; set; }
    }
}
