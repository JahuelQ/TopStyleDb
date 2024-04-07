using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace TopStyleDb.Models.Entities
{
    public class Customer
    {
        [Key]
        public int CustomerId { get; set; }

        [Required(ErrorMessage = "Please enter your first name.")]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "Please enter a name with at least 2 characters and no more than 50 characters.")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Please enter your last name.")]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "Please enter a name with at least 2 characters and no more than 50 characters.")]
        public string LastName { get; set; }

        [Required]
        [StringLength(100)]
        [EmailAddress]
        [RegularExpression(@"^[^@\s]+@[^@\s]+\.[^@\s]+$", ErrorMessage = "Please enter a valid email address.")]
        public string Email { get; set; }

        [Required]
        [StringLength(100, MinimumLength = 8)]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d).{8,}$", ErrorMessage = 
            "Password must be at least 8 characters long and include at least one uppercase letter, one lowercase letter, and one number.")]
        public string Password { get; set; }

        // List of orders from specific CustomerId
        public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
    }
}
