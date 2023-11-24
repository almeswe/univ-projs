namespace Visual
{
	partial class SceneForm
	{
		/// <summary>
		///  Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		///  Clean up any resources being used.
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
		///  Required method for Designer support - do not modify
		///  the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.components = new System.ComponentModel.Container();
			this.Timer = new System.Windows.Forms.Timer(this.components);
			this.SuspendLayout();
			// 
			// Timer
			// 
			this.Timer.Enabled = true;
			this.Timer.Interval = 30;
			this.Timer.Tick += new System.EventHandler(this.SceneTimerTick);
			// 
			// SceneForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(584, 561);
			this.Name = "SceneForm";
			this.Text = "Scene";
			this.Paint += new System.Windows.Forms.PaintEventHandler(this.OnScenePaint);
			this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.OnSceneKeyDown);
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Timer Timer;
	}
}