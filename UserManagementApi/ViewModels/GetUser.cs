using System.ComponentModel.DataAnnotations;
using UserManagementApi.ViewModels.Shared;
using UserManagementServices.ServiceModels;

namespace UserManagementApi.ViewModels
{
    public class GetUser : BaseAutoViewModel<UserSM, UserVM>
    {
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        [EmailAddress]
        public string Email { get; set; } = null!;

        [Required, MaxLength(128)]
        public string FirstName { get; set; }

        [MaxLength(128)]
        public string LastName { get; set; }
        [Required]
        public DateOnly DateOfBirth { get; set; }

        [Required, RegularExpression(@"^\d{10}$", ErrorMessage = "Phone number must be 10 digits.")]
        public string PhoneNumber { get; set; }

        public int Age { get; set; }
    }
}
