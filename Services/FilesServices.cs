namespace Api_ProjectManagement.Services
{
    public class FilesServices : IFilesServices
    {
        private readonly IWebHostEnvironment _env;
        private readonly IHttpContextAccessor _http;

        public FilesServices(IWebHostEnvironment env, IHttpContextAccessor http)
        {
            _env = env;
            _http = http;
        }

        public async Task<string> GuardarArchivo(byte[] contenido, string extension, string contenedor, string contentType)
        {
            var nombreArchivo = $"{Guid.NewGuid()}{extension}";
            var folder = Path.Combine(_env.WebRootPath, contenedor);
            if (!Directory.Exists(folder))
            {
                Directory.CreateDirectory(folder);
            }

            string ruta = Path.Combine(folder, nombreArchivo);
            await File.WriteAllBytesAsync(ruta, contenido);

            var UrlActual = $"{_http.HttpContext.Request.Scheme}://{_http.HttpContext.Request.Host}";
            var UrlParaDb = Path.Combine(UrlActual, contenedor, nombreArchivo).Replace("\\", "/");
            return UrlParaDb;
        }

        public Task BorrarArchivo(string ruta, string contenedor)
        {
            if (ruta != null)
            {
                var nombreArchivo = Path.GetFileName(ruta);
                string directorioArchivo = Path.Combine(_env.WebRootPath, contenedor, nombreArchivo);

                if (File.Exists(directorioArchivo))
                {
                    File.Delete(directorioArchivo);
                }
            }
            return Task.FromResult(0);
        }

        public async Task<string> EditarArchivo(byte[] contenido, string extension, string contenedor, 
            string ruta, string contentType)
        {
            await BorrarArchivo(ruta, contenedor);
            return await GuardarArchivo(contenido, extension, contenedor, contentType);
        }
    }

    public interface IFilesServices
    {
        Task<string> GuardarArchivo(byte[] contenido, string extension, string contenedor, string contentType);
        Task BorrarArchivo(string ruta, string contenedor);
        Task<string> EditarArchivo(byte[] contenido, string extension, string contenedor,
            string ruta, string contentType);
    }
}
