using FamilyBudget.Entities;
namespace FamilyBudget.Entities.Dto
{
    public class UserDto : User
    {
        public Profile Profile { get; set; }
    }
}