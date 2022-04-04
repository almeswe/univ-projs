namespace FTPServer.Models
{
    public sealed class TwoWayPathModel
    {
        public string SourcePath { get; set; }
        public string DestinationPath { get; set; }

        public TwoWayPathModel() =>
            this.SourcePath = this.DestinationPath 
                = string.Empty;

        public TwoWayPathModel(string sourcePath, string destintaionPath)
        {
            this.SourcePath = sourcePath;
            this.DestinationPath = destintaionPath;
        }
    }
}
