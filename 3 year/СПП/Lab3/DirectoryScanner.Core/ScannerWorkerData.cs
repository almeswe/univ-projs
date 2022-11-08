namespace DirectoryScanner.Core
{
    public struct ScannerWorkerData
    {
        public string Path { get; set; }
        public ScanResult ScanResult { get; set; }

        public ScannerWorkerData(string path, ScanResult scanResult) 
        {
            this.Path = path;
            this.ScanResult = scanResult;
        }
    }
}
