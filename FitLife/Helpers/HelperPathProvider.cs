namespace MvcCoreUtilidades.Helpers
{
    public enum Folder { Images = 0, Uploads= 1, Facturas = 2, Temporal = 3}

    public class HelperPathProvider
    {
        public IWebHostEnvironment hostEnvironment;

        public HelperPathProvider(IWebHostEnvironment hostEnvironment)
        {
            this.hostEnvironment = hostEnvironment;
        }

        public string MapFolder(string filename, Folder folder)
        {
            string carpeta = "";
            if(folder == Folder.Images)
            {
                carpeta = "images";
            }
            else if (folder == Folder.Uploads)
            {
                carpeta = "uploads";
            }
            else if (folder == Folder.Facturas)
            {
                carpeta = "facturas";
            }
            else if (folder == Folder.Temporal)
            {
                carpeta = "temp";
            }
            string rootPath = this.hostEnvironment.WebRootPath;
            string path = Path.Combine(rootPath, carpeta, filename);
            return path;
        }
    }
}
