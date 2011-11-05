namespace XnaToAnxConverter
{
	partial class ConverterForm
	{
		/// <summary>
		/// Erforderliche Designervariable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Verwendete Ressourcen bereinigen.
		/// </summary>
		/// <param name="disposing">True, wenn verwaltete Ressourcen gelöscht werden sollen; andernfalls False.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Vom Windows Form-Designer generierter Code

		/// <summary>
		/// Erforderliche Methode für die Designerunterstützung.
		/// Der Inhalt der Methode darf nicht mit dem Code-Editor geändert werden.
		/// </summary>
		private void InitializeComponent()
		{
			this.convertButton = new System.Windows.Forms.Button();
			this.sourcePath = new System.Windows.Forms.TextBox();
			this.destPath = new System.Windows.Forms.TextBox();
			this.browsePath1 = new System.Windows.Forms.Button();
			this.browsePath2 = new System.Windows.Forms.Button();
			this.checkBox1 = new System.Windows.Forms.CheckBox();
			this.label1 = new System.Windows.Forms.Label();
			this.SuspendLayout();
			// 
			// convertButton
			// 
			this.convertButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.convertButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.convertButton.Location = new System.Drawing.Point(161, 64);
			this.convertButton.Name = "convertButton";
			this.convertButton.Size = new System.Drawing.Size(275, 25);
			this.convertButton.TabIndex = 0;
			this.convertButton.Text = "Convert";
			this.convertButton.UseVisualStyleBackColor = true;
			this.convertButton.Click += new System.EventHandler(this.convertButton_Click);
			// 
			// sourcePath
			// 
			this.sourcePath.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.sourcePath.Location = new System.Drawing.Point(161, 13);
			this.sourcePath.Name = "sourcePath";
			this.sourcePath.Size = new System.Drawing.Size(203, 20);
			this.sourcePath.TabIndex = 1;
			// 
			// destPath
			// 
			this.destPath.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.destPath.Location = new System.Drawing.Point(161, 38);
			this.destPath.Name = "destPath";
			this.destPath.Size = new System.Drawing.Size(203, 20);
			this.destPath.TabIndex = 2;
			// 
			// browsePath1
			// 
			this.browsePath1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.browsePath1.Location = new System.Drawing.Point(370, 13);
			this.browsePath1.Name = "browsePath1";
			this.browsePath1.Size = new System.Drawing.Size(66, 20);
			this.browsePath1.TabIndex = 3;
			this.browsePath1.Text = "•••";
			this.browsePath1.UseVisualStyleBackColor = true;
			this.browsePath1.Click += new System.EventHandler(this.browsePath1_Click);
			// 
			// browsePath2
			// 
			this.browsePath2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.browsePath2.Location = new System.Drawing.Point(370, 38);
			this.browsePath2.Name = "browsePath2";
			this.browsePath2.Size = new System.Drawing.Size(66, 20);
			this.browsePath2.TabIndex = 4;
			this.browsePath2.Text = "•••";
			this.browsePath2.UseVisualStyleBackColor = true;
			this.browsePath2.Click += new System.EventHandler(this.browsePath2_Click);
			// 
			// checkBox1
			// 
			this.checkBox1.AutoSize = true;
			this.checkBox1.BackColor = System.Drawing.Color.Transparent;
			this.checkBox1.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.checkBox1.Location = new System.Drawing.Point(15, 39);
			this.checkBox1.Name = "checkBox1";
			this.checkBox1.Size = new System.Drawing.Size(140, 17);
			this.checkBox1.TabIndex = 5;
			this.checkBox1.Text = "Convert to different path";
			this.checkBox1.UseVisualStyleBackColor = false;
			this.checkBox1.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.BackColor = System.Drawing.Color.Transparent;
			this.label1.Location = new System.Drawing.Point(114, 15);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(41, 13);
			this.label1.TabIndex = 6;
			this.label1.Text = "Source";
			// 
			// ConverterForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(451, 101);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.checkBox1);
			this.Controls.Add(this.browsePath2);
			this.Controls.Add(this.browsePath1);
			this.Controls.Add(this.destPath);
			this.Controls.Add(this.sourcePath);
			this.Controls.Add(this.convertButton);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.MaximizeBox = false;
			this.Name = "ConverterForm";
			this.ShowIcon = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "XNA To ANX Converter";
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Button convertButton;
		private System.Windows.Forms.TextBox sourcePath;
		private System.Windows.Forms.TextBox destPath;
		private System.Windows.Forms.Button browsePath1;
		private System.Windows.Forms.Button browsePath2;
		private System.Windows.Forms.CheckBox checkBox1;
		private System.Windows.Forms.Label label1;
	}
}

