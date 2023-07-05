namespace Data.Models.CV
{
    public class UserEducation
    {
        public int UserEducationId { get; set; }
        public string InstituteName { get; set; }
        public DateOnly StartDate { get; set; }
        public DateOnly EndDate { get; set; }
        public string City { get; set; }

        public int UserDatId { get; set; }
        public UserData UserData { get; set; }
    }
}
