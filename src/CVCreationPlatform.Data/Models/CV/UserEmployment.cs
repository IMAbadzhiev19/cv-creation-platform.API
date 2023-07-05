using System.ComponentModel.DataAnnotations.Schema;

namespace Data.Models.CV
{
    public class UserEmployment
    {
        public int UserEmploymentId { get; set; }
        public string Position { get; set; }
        public DateOnly StartDate { get; set; }
        public DateOnly EndDate { get; set; }
        public string City { get; set; }
        public string Description { get; set; }

        public int UserDataId { get; set; }
        public UserData UserData { get; set; }
    }
}
