using System;
using System.IO;

namespace DirectoryScanner.Core
{
    public sealed class FileScanResult : ScanResult
    {
        public FileInfo FileInfo { get; private set; }

        public FileScanResult(FileInfo fileInfo) 
            : this(fileInfo.FullName) { }

        public FileScanResult(string filePath) : base()
        {
            if (!File.Exists(filePath))
                throw new ArgumentException($"File does not exists \"{filePath}\"");
            this.FileInfo = new FileInfo(filePath);
            this.Name = this.FileInfo.Name;
            this.Path = this.FileInfo.FullName;
        }

        public override long GetBytes(bool forceRecalculation = false)
        {
            if (!this._bytesRetrieved || forceRecalculation)
            {
                this._bytesRetrieved = true;
                this._bytes = this.FileInfo.Exists ? this.FileInfo.Length : 0L;
            }
            return this._bytes;
        }
    }
}
