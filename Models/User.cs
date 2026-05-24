namespace AWCDigitalCommerce.Web.Models
{
    public class User
    {
        public int UserID { get; set; }

        public DateTime UserDTCreation { get; set; } = DateTime.Now;

        public string UserPIN { get; set; } = string.Empty;

        public string UserPW { get; set; } = string.Empty;

        public string UserName { get; set; } = string.Empty;

        public string UserAccessLevel { get; set; } = string.Empty;

        public bool UserActive { get; set; } = true;

        public string UserSecurityProfile { get; set; } = string.Empty;

        public bool UserPowerAdmin { get; set; } = false;

        public string UserFingerprint { get; set; } = string.Empty;
    }
}
