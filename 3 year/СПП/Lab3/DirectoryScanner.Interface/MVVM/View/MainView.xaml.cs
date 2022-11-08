using System;
using System.Windows;
using System.Windows.Controls;

using DirectoryScanner.Core;
using DirectoryScanner.Interface.MVVM.Model;

namespace DirectoryScanner.Interface
{
    public partial class MainWindow : Window
    {
        public MainWindow() =>
            this.InitializeComponent();

        private void RenderTreeViewItem(TreeViewItem item)
        {
            if (item == null || item.Tag == null)
                return;
            var tag = item.Tag as TreeViewItemTag;
            if (tag.IsLoaded)
                return;
            item.Items.Clear();
            var result = tag.Result;
            foreach (var node in result.Nodes)
            {
                var child = new TreeViewItem();
                child.Tag = new TreeViewItemTag(node);
                child.Header = this.RenderNodeName(node, result);
                if (node is DirectoryScanResult)
                    child.Items.Add(new TreeViewItem());
                item.Items.Add(child);
            }
            tag.IsLoaded = true;
        }

        private string RenderNodeName(ScanResult nodeResult, ScanResult parentResult)
        {
            var percent = (double)nodeResult.GetBytes() / parentResult.GetBytes();
            var icon = nodeResult is FileScanResult ? "📄" : "📁";
            return $"{icon} {nodeResult.Name} ({nodeResult.GetBytes()} bytes, {Math.Round(percent * 100, 2)}%)";
        }

        private void MainTreeViewExpanded(object sender, RoutedEventArgs e) =>
            this.RenderTreeViewItem(e.OriginalSource as TreeViewItem);
    }
}