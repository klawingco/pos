﻿namespace coop_main
{
    partial class stocksExiit
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.btn_yes = new System.Windows.Forms.Button();
            this.btn_no = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.White;
            this.panel1.Controls.Add(this.panel2);
            this.panel1.Controls.Add(this.btn_yes);
            this.panel1.Controls.Add(this.btn_no);
            this.panel1.Location = new System.Drawing.Point(9, 10);
            this.panel1.Margin = new System.Windows.Forms.Padding(2);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(357, 185);
            this.panel1.TabIndex = 4;
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(155)))), ((int)(((byte)(209)))), ((int)(((byte)(229)))));
            this.panel2.Controls.Add(this.label1);
            this.panel2.Location = new System.Drawing.Point(0, 16);
            this.panel2.Margin = new System.Windows.Forms.Padding(2);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(357, 95);
            this.panel2.TabIndex = 26;
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Mobile Sans", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(2, 11);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(353, 74);
            this.label1.TabIndex = 0;
            this.label1.Text = "There\'s still an ongoing transaction\r\nWould you like to continue to exit?\r\n";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btn_yes
            // 
            this.btn_yes.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(47)))), ((int)(((byte)(137)))), ((int)(((byte)(170)))));
            this.btn_yes.DialogResult = System.Windows.Forms.DialogResult.Yes;
            this.btn_yes.FlatAppearance.BorderSize = 0;
            this.btn_yes.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_yes.Font = new System.Drawing.Font("Mobile Sans", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_yes.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.btn_yes.Location = new System.Drawing.Point(27, 130);
            this.btn_yes.Margin = new System.Windows.Forms.Padding(2);
            this.btn_yes.Name = "btn_yes";
            this.btn_yes.Size = new System.Drawing.Size(123, 34);
            this.btn_yes.TabIndex = 25;
            this.btn_yes.Text = "Yes";
            this.btn_yes.UseVisualStyleBackColor = false;
            // 
            // btn_no
            // 
            this.btn_no.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(47)))), ((int)(((byte)(137)))), ((int)(((byte)(170)))));
            this.btn_no.DialogResult = System.Windows.Forms.DialogResult.No;
            this.btn_no.FlatAppearance.BorderSize = 0;
            this.btn_no.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_no.Font = new System.Drawing.Font("Mobile Sans", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_no.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.btn_no.Location = new System.Drawing.Point(205, 130);
            this.btn_no.Margin = new System.Windows.Forms.Padding(2);
            this.btn_no.Name = "btn_no";
            this.btn_no.Size = new System.Drawing.Size(123, 34);
            this.btn_no.TabIndex = 24;
            this.btn_no.Text = "No";
            this.btn_no.UseVisualStyleBackColor = false;
            this.btn_no.Click += new System.EventHandler(this.btn_no_Click);
            // 
            // stocksExiit
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(47)))), ((int)(((byte)(137)))), ((int)(((byte)(170)))));
            this.ClientSize = new System.Drawing.Size(375, 205);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "stocksExiit";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "inv_pdf";
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btn_yes;
        private System.Windows.Forms.Button btn_no;
    }
}