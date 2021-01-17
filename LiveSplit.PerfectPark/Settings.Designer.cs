
namespace LiveSplit.PerfectPark
{
    partial class Settings
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.autoStartBox = new System.Windows.Forms.CheckBox();
            this.autoResetBox = new System.Windows.Forms.CheckBox();
            this.methodCombo = new System.Windows.Forms.ComboBox();
            this.methodLabel = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // autoStartBox
            // 
            this.autoStartBox.AutoSize = true;
            this.autoStartBox.Checked = true;
            this.autoStartBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.autoStartBox.Location = new System.Drawing.Point(4, 4);
            this.autoStartBox.Name = "autoStartBox";
            this.autoStartBox.Size = new System.Drawing.Size(141, 17);
            this.autoStartBox.TabIndex = 0;
            this.autoStartBox.Text = "Automatically Start Runs";
            this.autoStartBox.UseVisualStyleBackColor = true;
            // 
            // autoResetBox
            // 
            this.autoResetBox.AutoSize = true;
            this.autoResetBox.Checked = true;
            this.autoResetBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.autoResetBox.Location = new System.Drawing.Point(4, 28);
            this.autoResetBox.Name = "autoResetBox";
            this.autoResetBox.Size = new System.Drawing.Size(204, 17);
            this.autoResetBox.TabIndex = 1;
            this.autoResetBox.Text = "Reset Timer when Auto-Starting Runs";
            this.autoResetBox.UseVisualStyleBackColor = true;
            // 
            // methodCombo
            // 
            this.methodCombo.FormattingEnabled = true;
            this.methodCombo.Items.AddRange(new object[] {
            "Memory Watch",
            "Log File"});
            this.methodCombo.Location = new System.Drawing.Point(55, 51);
            this.methodCombo.Name = "methodCombo";
            this.methodCombo.Size = new System.Drawing.Size(121, 21);
            this.methodCombo.TabIndex = 2;
            this.methodCombo.SelectedIndex = 0;
            // 
            // methodLabel
            // 
            this.methodLabel.AutoSize = true;
            this.methodLabel.Location = new System.Drawing.Point(3, 54);
            this.methodLabel.Name = "methodLabel";
            this.methodLabel.Size = new System.Drawing.Size(46, 13);
            this.methodLabel.TabIndex = 3;
            this.methodLabel.Text = "Method:";
            // 
            // Settings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.methodLabel);
            this.Controls.Add(this.methodCombo);
            this.Controls.Add(this.autoResetBox);
            this.Controls.Add(this.autoStartBox);
            this.Name = "Settings";
            this.Size = new System.Drawing.Size(240, 150);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckBox autoStartBox;
        private System.Windows.Forms.CheckBox autoResetBox;
        private System.Windows.Forms.ComboBox methodCombo;
        private System.Windows.Forms.Label methodLabel;
    }
}
