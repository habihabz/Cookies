
namespace ReportDesigning
{
    partial class PrintPreview
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
            this.PrintReport = new System.Windows.Forms.Button();
            this.Lbl_InvoiceNumber = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.WB_View = new System.Windows.Forms.WebBrowser();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // PrintReport
            // 
            this.PrintReport.Location = new System.Drawing.Point(714, 21);
            this.PrintReport.Name = "PrintReport";
            this.PrintReport.Size = new System.Drawing.Size(75, 23);
            this.PrintReport.TabIndex = 0;
            this.PrintReport.Text = "Reprint";
            this.PrintReport.UseVisualStyleBackColor = true;
            this.PrintReport.Click += new System.EventHandler(this.PrintReport_Click);
            // 
            // Lbl_InvoiceNumber
            // 
            this.Lbl_InvoiceNumber.AutoSize = true;
            this.Lbl_InvoiceNumber.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.Lbl_InvoiceNumber.Location = new System.Drawing.Point(13, 21);
            this.Lbl_InvoiceNumber.Name = "Lbl_InvoiceNumber";
            this.Lbl_InvoiceNumber.Size = new System.Drawing.Size(82, 13);
            this.Lbl_InvoiceNumber.TabIndex = 2;
            this.Lbl_InvoiceNumber.Text = "Invoice Number";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(116, 21);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(31, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "0000";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.WB_View);
            this.panel1.Location = new System.Drawing.Point(-1, 95);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(801, 355);
            this.panel1.TabIndex = 3;
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.Teal;
            this.panel2.Controls.Add(this.label1);
            this.panel2.Controls.Add(this.Lbl_InvoiceNumber);
            this.panel2.Controls.Add(this.PrintReport);
            this.panel2.Location = new System.Drawing.Point(-1, 1);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(801, 94);
            this.panel2.TabIndex = 4;
            // 
            // WB_View
            // 
            this.WB_View.Dock = System.Windows.Forms.DockStyle.Fill;
            this.WB_View.Location = new System.Drawing.Point(0, 0);
            this.WB_View.MinimumSize = new System.Drawing.Size(20, 20);
            this.WB_View.Name = "WB_View";
            this.WB_View.Size = new System.Drawing.Size(801, 355);
            this.WB_View.TabIndex = 0;
            // 
            // PrintPreview
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Name = "PrintPreview";
            this.Text = "Print Preview";
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button PrintReport;
        private System.Windows.Forms.Label Lbl_InvoiceNumber;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.WebBrowser WB_View;
        private System.Windows.Forms.Panel panel2;
    }
}

