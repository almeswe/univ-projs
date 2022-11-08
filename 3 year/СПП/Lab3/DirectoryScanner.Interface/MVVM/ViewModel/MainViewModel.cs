using System;
using System.Windows;
using System.Windows.Controls;

using System.Threading;
using System.Collections.ObjectModel;

using DirectoryScanner.Core;
using DirectoryScanner.Interface.Core;
using DirectoryScanner.Interface.MVVM.Model;

namespace DirectoryScanner.Interface.MVVM.ViewModel
{
    internal sealed class MainViewModel : ObservableObject
    {
        private Scanner _scanner;
        private string _scannerStatus;
        private ObservableCollection<TreeViewItem> _items;

        public RelayCommand StopButtonClick { get; }
        public RelayCommand ExploreButtonClick { get; }
        public RelayCommand TreeViewItemExpanded { get; }

        public string ScannerStatus
        {
            get => this._scannerStatus;
            set 
            {
                this._scannerStatus = $"Directory Scanner: {value}";
                this.OnPropertyChanged(nameof(this.ScannerStatus));
            }
        }

        public ObservableCollection<TreeViewItem> Items
        {
            get => this._items;
            set
            {
                this._items = value;
                this.OnPropertyChanged(nameof(this.Items));
            }
        }

        public MainViewModel()
        {
            this._scanner = null;
            this.ScannerStatus = "Started~";
            this.StopButtonClick = new RelayCommand(
                this.StopButtonClickExecute, this.StopButtonClickCanExecute);
            this.ExploreButtonClick = new RelayCommand(
                this.ExploreButtonClickExecute, this.ExploreButtonClickCanExecute);
        }

        private void StartScanner(object obj)
        {
            this.ScannerStatus = $"Scanning...({(string)obj})";
            this._scanner = new Scanner((string)obj);
            this._scanner.Finished += this.ScannerFinished;
            this._scanner.Start();
        }

        private void ScannerFinished(ScanResult result)
        {
            var elapsedTime = this._scanner.Watch.Elapsed;
            this.ScannerStatus = $"Finished in ({elapsedTime})";
            Application.Current.Dispatcher.Invoke(() =>
                this.RenderTreeViewRoot(result));
        }

        private void RenderTreeViewRoot(ScanResult result)
        {
            if (this.Items == null) 
                this.Items = new ObservableCollection<TreeViewItem>();
            result.Nodes.TryTake(out result);
            var root = new TreeViewItem();
            root.Tag = new TreeViewItemTag(result, true);
            root.Header = this.RenderNodeName(result, result);
            foreach (var node in result.Nodes)
            {
                var child = new TreeViewItem();
                child.Tag = new TreeViewItemTag(node);
                child.Header = this.RenderNodeName(node, result);
                if (node is DirectoryScanResult)
                    child.Items.Add(new TreeViewItem());
                root.Items.Add(child);
            }
            this.Items?.Clear();
            this.Items?.Add(root);
        }

        private string RenderNodeName(ScanResult nodeResult, ScanResult parentResult)
        {
            var percent = (double)nodeResult.GetBytes() / parentResult.GetBytes();
            var icon = nodeResult is FileScanResult ? "📄" : "📁";
            return $"{icon} {nodeResult.Name} ({nodeResult.GetBytes()} bytes, {Math.Round(percent * 100, 2)}%)";
        }

        public bool ExploreButtonClickCanExecute(object parameter)
        {
            if (this._scanner == null)
                return true;
            return this._scanner.IsFinished;
        }

        public void ExploreButtonClickExecute(object parameter)
        {
            using (var dialog = new System.Windows.Forms.FolderBrowserDialog())
            {
                var result = dialog.ShowDialog();
                if (result == System.Windows.Forms.DialogResult.OK)
                    ThreadPool.QueueUserWorkItem(this.StartScanner, dialog.SelectedPath);
            }
        }

        public bool StopButtonClickCanExecute(object parameter)
        {
            if (this._scanner == null)
                return false;
            return !this._scanner.IsFinished;
        }

        public void StopButtonClickExecute(object parameter) => 
            this._scanner?.Stop();
    }
}
