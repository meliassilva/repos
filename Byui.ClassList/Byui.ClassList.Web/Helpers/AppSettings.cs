namespace Byui.Byui.ClassList.Web.Helpers
{
    public class AppSettings
    {
        public string ApiUrl { get; set; }
        public string HeadFileLocation { get; set; }
        public string HeaderFileLocation { get; set; }
        public string FooterFileLocation { get; set; }
        public string MetadataAddress { get; set; }
        public string ClientId { get; set; }
        public string ClientSecret { get; set; }
        public bool AllowAnonymous { get; set; }
        public bool Impersonate { get; set; }
        public User User { get; set; }
        public string DataProtectionKeyLocation { get; set; }

        /// <summary>
        /// Indicates whether or not we should be using open id connect (does it have a clientid/secret).
        /// </summary>
        /// <returns></returns>
        public bool IsUsingOpenIdConnect()
        {
            return !string.IsNullOrEmpty(ClientId) && !string.IsNullOrEmpty(ClientSecret);
        }
    }
}
