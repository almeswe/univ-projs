using FTPLayer.Entity;
using System.Collections.Generic;

namespace FTPLayer
{
    public interface IFTPLayer
    {
        string GetFile(string path);
        IEnumerable<FileSystemEntity> GetDirectory(string path);

        void DeleteFile(string path);
        void PutToFile(string text, string path);
        void AppendToFile(string text, string path);

        void CopyFile(string sourcePath, string destinationPath);
        void MoveFile(string sourcePath, string destinationPath);
    }
}