
namespace Laba1
{
    partial class Statistics
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.MetricsListView = new System.Windows.Forms.ListView();
            this.jCountColumnHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.OperatorColumnHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.f1jColumnHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.iCountColumnHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.OperandColumnHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.f2iColumnHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.ηLabel = new System.Windows.Forms.Label();
            this.NLabel = new System.Windows.Forms.Label();
            this.VLabel = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // MetricsListView
            // 
            this.MetricsListView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.MetricsListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.jCountColumnHeader,
            this.OperatorColumnHeader,
            this.f1jColumnHeader,
            this.iCountColumnHeader,
            this.OperandColumnHeader,
            this.f2iColumnHeader});
            this.MetricsListView.Font = new System.Drawing.Font("Consolas", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.MetricsListView.GridLines = true;
            this.MetricsListView.HideSelection = false;
            this.MetricsListView.Location = new System.Drawing.Point(2, 37);
            this.MetricsListView.Name = "MetricsListView";
            this.MetricsListView.Size = new System.Drawing.Size(853, 483);
            this.MetricsListView.TabIndex = 0;
            this.MetricsListView.UseCompatibleStateImageBehavior = false;
            this.MetricsListView.View = System.Windows.Forms.View.Details;
            // 
            // jCountColumnHeader
            // 
            this.jCountColumnHeader.Text = "j";
            // 
            // OperatorColumnHeader
            // 
            this.OperatorColumnHeader.Text = "Operator";
            this.OperatorColumnHeader.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // f1jColumnHeader
            // 
            this.f1jColumnHeader.Text = "f1j";
            this.f1jColumnHeader.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // iCountColumnHeader
            // 
            this.iCountColumnHeader.Text = "i";
            this.iCountColumnHeader.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // OperandColumnHeader
            // 
            this.OperandColumnHeader.Text = "Operand";
            this.OperandColumnHeader.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // f2iColumnHeader
            // 
            this.f2iColumnHeader.Text = "f2i";
            this.f2iColumnHeader.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // ηLabel
            // 
            this.ηLabel.AutoSize = true;
            this.ηLabel.Font = new System.Drawing.Font("Consolas", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.ηLabel.Location = new System.Drawing.Point(2, 10);
            this.ηLabel.Name = "ηLabel";
            this.ηLabel.Size = new System.Drawing.Size(63, 20);
            this.ηLabel.TabIndex = 1;
            this.ηLabel.Text = "ηLabel";
            // 
            // NLabel
            // 
            this.NLabel.AutoSize = true;
            this.NLabel.Font = new System.Drawing.Font("Consolas", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.NLabel.Location = new System.Drawing.Point(143, 10);
            this.NLabel.Name = "NLabel";
            this.NLabel.Size = new System.Drawing.Size(63, 20);
            this.NLabel.TabIndex = 2;
            this.NLabel.Text = "NLabel";
            // 
            // VLabel
            // 
            this.VLabel.AutoSize = true;
            this.VLabel.Font = new System.Drawing.Font("Consolas", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.VLabel.Location = new System.Drawing.Point(304, 10);
            this.VLabel.Name = "VLabel";
            this.VLabel.Size = new System.Drawing.Size(63, 20);
            this.VLabel.TabIndex = 3;
            this.VLabel.Text = "VLabel";
            // 
            // Statistics
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(856, 521);
            this.Controls.Add(this.VLabel);
            this.Controls.Add(this.NLabel);
            this.Controls.Add(this.ηLabel);
            this.Controls.Add(this.MetricsListView);
            this.Name = "Statistics";
            this.Text = "Statistics";
            this.Resize += new System.EventHandler(this.StatisticsResize);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListView MetricsListView;
        private System.Windows.Forms.ColumnHeader jCountColumnHeader;
        private System.Windows.Forms.ColumnHeader OperatorColumnHeader;
        private System.Windows.Forms.ColumnHeader f1jColumnHeader;
        private System.Windows.Forms.ColumnHeader iCountColumnHeader;
        private System.Windows.Forms.ColumnHeader OperandColumnHeader;
        private System.Windows.Forms.ColumnHeader f2iColumnHeader;
        private System.Windows.Forms.Label ηLabel;
        private System.Windows.Forms.Label NLabel;
        private System.Windows.Forms.Label VLabel;
    }
}