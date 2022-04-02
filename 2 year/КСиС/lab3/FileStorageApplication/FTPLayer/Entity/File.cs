using System;

namespace FTPLayer.Entity
{
    [Serializable]
    public sealed class File : FileSystemEntity
    {
        public File(string path) : base(path)
        {
            if (!System.IO.File.Exists(path))
                throw new System.IO.FileNotFoundException();
            this.Type = FileSystemEntityType.File;
        }
    }
}