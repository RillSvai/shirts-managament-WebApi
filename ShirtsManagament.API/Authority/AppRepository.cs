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
                Scopes = "read,write,delete"
            }
        };
        public static Application? GetById(string clientId) 
        {
            return _applications.FirstOrDefault(a => a.ClientId == clientId);
        }
    }
}
