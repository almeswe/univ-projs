using FTPLayer.Entity;
using System.Collections.Generic;

namespace FTPLayer
{
    public sealed class WindowsFTPLayer : IFTPLayer
    {
        public string GetFile(string path) =>
            System.IO.File.ReadAllText(path);

        public void AppendToFile(string text, string path)
        {
            using (var writer = System.IO.File.AppendText(path))
            {
                writer.WriteLine(text);
            }
        }

        public void CopyFile(string sourcePath, string destinationPath) =>
            System.IO.File.Copy(sourcePath, destinationPath);
        
        public void DeleteFile(string path) =>
            System.IO.File.Delete(path);

        public IEnumerable<FileSystemEntity> GetDirectory(string path) =>
            new Directory(path).GetEntities();

        public void MoveFile(string sourcePath, string destinationPath) =>
            System.IO.File.Move(sourcePath, destinationPath);
    
        public void PutToFile(string text, string path) =>
            System.IO.File.WriteAllText(path, text);
    }
}