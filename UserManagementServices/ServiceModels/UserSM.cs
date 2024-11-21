using UserManagementServices.ServiceModels.Shared;
using UserManagementDBModel.EF.Models;
using Core.Repository.Infrastructure;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;


namespace UserManagementServices.ServiceModels
{
    public class UserSM : BaseServiceModel<User, UserSM>, IObjectState
    {
        public int Id { get; set; }

        public string Email { get; set; } = null!;

        public string? FirstName { get; set; }

        public string? LastName { get; set; }
        public string PhoneNumber { get; set; }

        public DateTime? CreatedAt { get; set; }

        public DateTime? UpdatedAt { get; set; }
        public DateOnly DateOfBirth { get; set; }

        public ObjectState ObjectState { get; set; }

        public int Age => CalculateAge();

        private int CalculateAge()
        {
            var today = DateOnly.FromDateTime(DateTime.Today); // Convert to DateOnly
            int age = today.Year - DateOfBirth.Year;
            if (DateOfBirth > today.AddYears(-age)) age--;
            return age;
        }
    }
}
