using System.ComponentModel.DataAnnotations;

namespace BookStore_UI.Models
{
    public class LoginModel
    {
        /// <summary>
        /// Gets or sets the user's username.
        /// </summary>
        [Required]
        [EmailAddress]
        [Display(Name = "Email Address")]
        public string EmailAddress { get; set; }

        /// <summary>
        /// Gets or sets the user's password.
        /// </summary>
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }

    /// <summary>
    /// Registration view model.
    /// </summary>
    public class RegistrationModel
    {
        /// <summary>
        /// Gets or sets the user's username.
        /// </summary>
        [Required]
        [EmailAddress]
        [Display(Name = "Email Address")]
        public string EmailAddress { get; set; }

        /// <summary>
        /// Gets or sets the user's password.
        /// </summary>
        [Required]
        [DataType(DataType.Password)]
        [StringLength(15, ErrorMessage = "Your password is limited to {2} to {1} characters.", MinimumLength = 6)]
        public string Password { get; set; }

        /// <summary>
        /// Gets or sets the user's confirmation password.
        /// </summary>
        [DataType(DataType.Password)]
        [Display(Name = "Confirm Password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }
    }
}
