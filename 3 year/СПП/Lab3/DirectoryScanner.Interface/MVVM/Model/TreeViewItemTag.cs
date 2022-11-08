using DirectoryScanner.Core;

namespace DirectoryScanner.Interface.MVVM.Model
{
    public sealed class TreeViewItemTag
    {
        public bool IsLoaded { get; set; }
        public ScanResult Result { get; private set; }

        public TreeViewItemTag(ScanResult result) 
        {
            this.Result = result;
            this.IsLoaded = false;
        }

        public TreeViewItemTag(ScanResult result, bool isLoaded)
        {
            this.Result = result;
            this.IsLoaded = isLoaded;
        }
    }
}
