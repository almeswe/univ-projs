using System;

namespace FTPLayer.Entity
{
    public abstract class FileSystemEntity
    {
        public static string RootPath = "C:\\";

        public string AbsolutePath { get; protected set; }
        public FileSystemEntityType Type { get; protected set; }

        public FileSystemEntity(string path) =>
            this.AbsolutePath = path;

        public static FileSystemEntity GetEntity(string path)
        {
            if (System.IO.File.Exists(path))
                return new File(path);
            else if (System.IO.Directory.Exists(path))
                return new Directory(path);
            return null;
        }
    }
}