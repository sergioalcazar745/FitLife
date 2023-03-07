namespace MvcCoreUtilidades.Helpers
{
    public class HelperUploadFile
    {
        private HelperPathProvider helperPath;
        public HelperUploadFile(HelperPathProvider pathProvider)
        {
            this.helperPath = pathProvider;
        }

        public async Task<List<string>> UploadFilesAsync (List<IFormFile> files, Folder folder)
        {
            List<string> paths = new List<string>();
            foreach(FormFile file in files)
            {
                string filename = file.FileName;
                string path = this.helperPath.MapFolder(filename, folder);
                using (Stream stream = new FileStream(path, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }
                paths.Add(path);
            }
            return paths;
        }

        public async Task<string> UploadFileAsync(IFormFile file, Folder folder)
        {
            string filename = file.FileName;
            string path = this.helperPath.MapFolder(filename, folder);
            using (Stream stream = new FileStream(path, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }
            return path;
        }
    }
}
