using System.ComponentModel.DataAnnotations;

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
        [Required]
        [EmailAddress]
        public string EmailAddress { get; set; }

        /// <summary>
        /// Gets or sets the user's password.
        /// </summary>
        [Required]
        [DataType(DataType.Password)]
        [StringLength(15, ErrorMessage = "Your password is limited to {2} to {1} characters.", MinimumLength = 6)]
        public string Password { get; set; }
    }
}
