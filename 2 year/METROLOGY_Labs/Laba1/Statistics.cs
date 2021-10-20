using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace Laba1
{
    public partial class Statistics : Form
    {
        public Statistics(MetricsAnalyzer metrics)
        {
            InitializeComponent();
            this.StatisticsResize(null, null);
            this.ShowMetrics(metrics);
        }

        private void ShowMetrics(MetricsAnalyzer metrics)
        {
            this.MetricsListView.Items.Clear();

            var operands = new List<KeyValuePair<string, int>>();
            var operators = new List<KeyValuePair<string, int>>();

            foreach (var jsOperator in metrics.Operators)
                operators.Add(jsOperator);
            foreach (var jsOperand in metrics.Operands)
                operands.Add(jsOperand);

            for (int i = 0; i < operators.Count; i++)
            {
                var item = new ListViewItem($"{i+1}.");
                item.SubItems.Add(operators[i].Key);
                item.SubItems.Add($"{operators[i].Value}");
                this.MetricsListView.Items.Add(item);
            }

            for (int i = 0; i < Math.Min(operators.Count, operands.Count); i++)
            {
                this.MetricsListView.Items[i].SubItems.Add($"{i+1}.");
                this.MetricsListView.Items[i].SubItems.Add(operands[i].Key);
                this.MetricsListView.Items[i].SubItems.Add($"{operands[i].Value}");
            }

            if (operands.Count > operators.Count)
            {
                for (int i = operators.Count; i < operands.Count; i++)
                {
                    var item = new ListViewItem();
                    item.SubItems.Add("");
                    item.SubItems.Add("");
                    item.SubItems.Add($"{i+1}.");
                    item.SubItems.Add(operands[i].Key);
                    item.SubItems.Add($"{operands[i].Value}");
                    this.MetricsListView.Items.Add(item);
                }
            }
            this.CalculateResult(metrics);
        }
        private void CalculateResult(MetricsAnalyzer metrics)
        {

            var item = new ListViewItem($"η1 = {metrics.Operators.Keys.Count}");
            item.SubItems.Add("");
            item.SubItems.Add($"N1 = {metrics.Operators.Sum(x => x.Value)}");
            item.SubItems.Add($"η2 = {metrics.Operands.Keys.Count}");
            item.SubItems.Add("");
            item.SubItems.Add($"N2 = {metrics.Operands.Sum(x => x.Value)}");
            this.MetricsListView.Items.Add(item);

            long η, N, V;

            this.ηLabel.Text = $"η = {η = metrics.Operators.Keys.Count + metrics.Operands.Keys.Count}";
            this.NLabel.Text = $"N = {N = metrics.Operators.Sum(x => x.Value) + metrics.Operands.Sum(x => x.Value)}";
            this.VLabel.Text = $"V = N*log2(η) = {V = N * (long)Math.Log(η, 2)}";
        }

        private void StatisticsResize(object sender, EventArgs e)
        {
            var width = this.MetricsListView.Width;
            var itemWidth = width / this.MetricsListView.Columns.Count;
            for (int i = 0; i < this.MetricsListView.Columns.Count; i++)
                this.MetricsListView.Columns[i].Width = itemWidth;
        }
    }
}