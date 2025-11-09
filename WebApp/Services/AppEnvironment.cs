namespace WebApp.Services
{
    public class AppEnvironment(IWebHostEnvironment env) : Core.IAppEnvironment
    {

        private readonly IWebHostEnvironment _env = env;

        public string WebRootPath => _env.WebRootPath;

    }
}
