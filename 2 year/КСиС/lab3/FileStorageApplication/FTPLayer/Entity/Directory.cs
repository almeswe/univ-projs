using System;
using System.Collections.Generic;

namespace FTPLayer.Entity
{
    [Serializable]
    public sealed class Directory : FileSystemEntity
    {
        public Directory(string path) : base(path)
        {
            if (!System.IO.Directory.Exists(path))
                throw new System.IO.DirectoryNotFoundException();
            //this.Entities = this.GetEntities();
            this.Type = FileSystemEntityType.Directory;
        }

        public IEnumerable<FileSystemEntity> GetEntities()
        {
            foreach (var info in System.IO.Directory.GetFileSystemEntries(this.AbsolutePath))
                yield return FileSystemEntity.GetEntity(info);
        }
    }
}