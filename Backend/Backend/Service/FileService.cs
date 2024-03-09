using Backend.Models;

namespace Backend.Service
{
    public class FileService
    {
        DbContextClasse _context = new DbContextClasse();

        public async Task<int> SaveFileName(string fileName)
        {
            var uploadedFile = new UploadedFile();
            uploadedFile.FileName = fileName;
            _context.fileInfo.Add(uploadedFile);
            await _context.SaveChangesAsync();
            return uploadedFile.id;
        }
    }

}
