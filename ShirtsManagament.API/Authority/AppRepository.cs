namespace ShirtsManagament.API.Authority
{
    public static class AppRepository
    {
        private static List<Application> _applications = new List<Application>()
        {
            new Application()
            {
                Id = 1,
                Name = "MyMvc",
                ClientId = "Test_CI",
                Secret = "Test_S",
                Scopes = "read,write"
            }
        };
        public static bool Authenticate(string clientId,  string secret) 
        {
            return _applications.Any(a => a.ClientId == clientId && a.Secret == secret);
        }
        public static Application? GetById(string clientId) 
        {
            return _applications.FirstOrDefault(a => a.ClientId == clientId);
        }
    }
}
