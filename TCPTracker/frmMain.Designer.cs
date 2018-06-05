namespace TCPTracker
{
    partial class frmMain
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
            this.txtIPAddress = new System.Windows.Forms.TextBox();
            this.lblIPAddress = new System.Windows.Forms.Label();
            this.btnAddIP = new System.Windows.Forms.Button();
            this.listView1 = new System.Windows.Forms.ListView();
            this.SuspendLayout();
            // 
            // txtIPAddress
            // 
            this.txtIPAddress.Location = new System.Drawing.Point(12, 28);
            this.txtIPAddress.Name = "txtIPAddress";
            this.txtIPAddress.Size = new System.Drawing.Size(100, 20);
            this.txtIPAddress.TabIndex = 0;
            // 
            // lblIPAddress
            // 
            this.lblIPAddress.AutoSize = true;
            this.lblIPAddress.Location = new System.Drawing.Point(12, 9);
            this.lblIPAddress.Name = "lblIPAddress";
            this.lblIPAddress.Size = new System.Drawing.Size(58, 13);
            this.lblIPAddress.TabIndex = 1;
            this.lblIPAddress.Text = "IP Address";
            // 
            // btnAddIP
            // 
            this.btnAddIP.Location = new System.Drawing.Point(12, 54);
            this.btnAddIP.Name = "btnAddIP";
            this.btnAddIP.Size = new System.Drawing.Size(75, 23);
            this.btnAddIP.TabIndex = 2;
            this.btnAddIP.Text = "Add Address";
            this.btnAddIP.UseVisualStyleBackColor = true;
            this.btnAddIP.Click += new System.EventHandler(this.btnAddIP_Click);
            // 
            // listView1
            // 
            this.listView1.Location = new System.Drawing.Point(153, 9);
            this.listView1.Name = "listView1";
            this.listView1.Size = new System.Drawing.Size(240, 245);
            this.listView1.TabIndex = 3;
            this.listView1.UseCompatibleStateImageBehavior = false;
            this.listView1.View = System.Windows.Forms.View.List;
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(425, 299);
            this.Controls.Add(this.listView1);
            this.Controls.Add(this.btnAddIP);
            this.Controls.Add(this.lblIPAddress);
            this.Controls.Add(this.txtIPAddress);
            this.Name = "frmMain";
            this.Text = "Form1";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmMain_FormClosing);
            this.Load += new System.EventHandler(this.frmMain_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtIPAddress;
        private System.Windows.Forms.Label lblIPAddress;
        private System.Windows.Forms.Button btnAddIP;
        private System.Windows.Forms.ListView listView1;
    }
}

