namespace Admin
{
    partial class FrmUsedInstrument
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
            this.rbtnNew = new System.Windows.Forms.RadioButton();
            this.rbtnAverage = new System.Windows.Forms.RadioButton();
            this.rbtnPoor = new System.Windows.Forms.RadioButton();
            this.label5 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // rbtnNew
            // 
            this.rbtnNew.AutoSize = true;
            this.rbtnNew.Checked = true;
            this.rbtnNew.Location = new System.Drawing.Point(245, 257);
            this.rbtnNew.Margin = new System.Windows.Forms.Padding(6);
            this.rbtnNew.Name = "rbtnNew";
            this.rbtnNew.Size = new System.Drawing.Size(116, 29);
            this.rbtnNew.TabIndex = 11;
            this.rbtnNew.TabStop = true;
            this.rbtnNew.Text = "As New";
            this.rbtnNew.UseVisualStyleBackColor = true;
            // 
            // rbtnAverage
            // 
            this.rbtnAverage.AutoSize = true;
            this.rbtnAverage.Location = new System.Drawing.Point(245, 301);
            this.rbtnAverage.Margin = new System.Windows.Forms.Padding(6);
            this.rbtnAverage.Name = "rbtnAverage";
            this.rbtnAverage.Size = new System.Drawing.Size(129, 29);
            this.rbtnAverage.TabIndex = 12;
            this.rbtnAverage.Text = "Average ";
            this.rbtnAverage.UseVisualStyleBackColor = true;
            // 
            // rbtnPoor
            // 
            this.rbtnPoor.AutoSize = true;
            this.rbtnPoor.Location = new System.Drawing.Point(245, 345);
            this.rbtnPoor.Margin = new System.Windows.Forms.Padding(6);
            this.rbtnPoor.Name = "rbtnPoor";
            this.rbtnPoor.Size = new System.Drawing.Size(88, 29);
            this.rbtnPoor.TabIndex = 13;
            this.rbtnPoor.Text = "Poor";
            this.rbtnPoor.UseVisualStyleBackColor = true;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(55, 257);
            this.label5.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(103, 25);
            this.label5.TabIndex = 14;
            this.label5.Text = "Condition";
            // 
            // FrmUsedInstrument
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.ClientSize = new System.Drawing.Size(714, 395);
            this.ControlBox = false;
            this.Controls.Add(this.label5);
            this.Controls.Add(this.rbtnPoor);
            this.Controls.Add(this.rbtnAverage);
            this.Controls.Add(this.rbtnNew);
            this.Margin = new System.Windows.Forms.Padding(12);
            this.Name = "FrmUsedInstrument";
            this.Controls.SetChildIndex(this.rbtnNew, 0);
            this.Controls.SetChildIndex(this.rbtnAverage, 0);
            this.Controls.SetChildIndex(this.rbtnPoor, 0);
            this.Controls.SetChildIndex(this.label5, 0);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.RadioButton rbtnNew;
        private System.Windows.Forms.RadioButton rbtnAverage;
        private System.Windows.Forms.RadioButton rbtnPoor;
        private System.Windows.Forms.Label label5;
    }
}
