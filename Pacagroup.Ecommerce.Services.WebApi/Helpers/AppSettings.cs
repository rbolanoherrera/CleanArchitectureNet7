namespace Pacagroup.Ecommerce.Services.WebApi.Helpers
{
    /// <summary>
    /// para leer la config del Token
    /// </summary>
    public class AppSettings
    {
        /// <summary>
        /// 
        /// </summary>
        public string Secret { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Issuer { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Audience { get; set; }
    }
}