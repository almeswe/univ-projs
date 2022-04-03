using System;

namespace FTPLayer.Entity
{
    [Serializable]
    public sealed class File : FileSystemEntity
    {
        public File(string path) : base(path) =>
            this.Type = FileSystemEntityType.File;
    }
}