using FTPLayer.Entity;

namespace FTPClient
{
    public sealed class TreeNodeWrapper
    {
        public bool IsLoaded { get; set; }
        public FileSystemEntity Entity { get; private set; }

        public TreeNodeWrapper(FileSystemEntity entity)
        {
            this.Entity = entity;
            this.IsLoaded = false;
        }
    }
}
