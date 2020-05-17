namespace DatingApp.Data.DTOs
{
	public class UserForUpdateDTO
	{
        public string City { get; set; }
        public string Country { get; set; }
        public UserDescriptionDTO UserDescription { get; set; }
    }
}
