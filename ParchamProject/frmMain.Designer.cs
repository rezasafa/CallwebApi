namespace ParchamProject
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
            this.components = new System.ComponentModel.Container();
            this.label1 = new System.Windows.Forms.Label();
            this.txtUserName = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtPassword = new System.Windows.Forms.TextBox();
            this.txtToken = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.btnGetToken = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnAuto = new System.Windows.Forms.Button();
            this.btnReadFactors = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.btnRegister = new System.Windows.Forms.Button();
            this.btnReadSite = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.txtAzdt = new System.Windows.Forms.TextBox();
            this.txtTadt = new System.Windows.Forms.TextBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.SepehrDGV = new System.Windows.Forms.DataGridView();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.WebDGV = new System.Windows.Forms.DataGridView();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.FactorDGV = new System.Windows.Forms.DataGridView();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.rb30 = new System.Windows.Forms.RadioButton();
            this.rb1 = new System.Windows.Forms.RadioButton();
            this.rb2 = new System.Windows.Forms.RadioButton();
            this.btnFix = new System.Windows.Forms.Button();
            this.btnStopAuto = new System.Windows.Forms.Button();
            this.lblLastUpdate = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.SepehrDGV)).BeginInit();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.WebDGV)).BeginInit();
            this.groupBox4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.FactorDGV)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(396, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(64, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "نام کاربری :";
            // 
            // txtUserName
            // 
            this.txtUserName.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txtUserName.Location = new System.Drawing.Point(299, 19);
            this.txtUserName.Name = "txtUserName";
            this.txtUserName.Size = new System.Drawing.Size(91, 20);
            this.txtUserName.TabIndex = 1;
            this.txtUserName.Text = "najafi";
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(396, 48);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(58, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "کلمه عبور :";
            // 
            // txtPassword
            // 
            this.txtPassword.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txtPassword.Location = new System.Drawing.Point(299, 45);
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.PasswordChar = '*';
            this.txtPassword.Size = new System.Drawing.Size(91, 20);
            this.txtPassword.TabIndex = 3;
            this.txtPassword.Text = "qaz123";
            // 
            // txtToken
            // 
            this.txtToken.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txtToken.Location = new System.Drawing.Point(299, 71);
            this.txtToken.Name = "txtToken";
            this.txtToken.Size = new System.Drawing.Size(91, 20);
            this.txtToken.TabIndex = 5;
            this.txtToken.Visible = false;
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(396, 74);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(44, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "Token :";
            this.label3.Visible = false;
            // 
            // btnGetToken
            // 
            this.btnGetToken.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnGetToken.Location = new System.Drawing.Point(299, 97);
            this.btnGetToken.Name = "btnGetToken";
            this.btnGetToken.Size = new System.Drawing.Size(158, 26);
            this.btnGetToken.TabIndex = 6;
            this.btnGetToken.Text = "دریافت Token";
            this.btnGetToken.UseVisualStyleBackColor = true;
            this.btnGetToken.Click += new System.EventHandler(this.btnGetToken_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.lblLastUpdate);
            this.groupBox1.Controls.Add(this.btnStopAuto);
            this.groupBox1.Controls.Add(this.btnFix);
            this.groupBox1.Controls.Add(this.rb2);
            this.groupBox1.Controls.Add(this.rb1);
            this.groupBox1.Controls.Add(this.rb30);
            this.groupBox1.Controls.Add(this.btnAuto);
            this.groupBox1.Controls.Add(this.btnReadFactors);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.btnRegister);
            this.groupBox1.Controls.Add(this.btnReadSite);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.txtUserName);
            this.groupBox1.Controls.Add(this.txtAzdt);
            this.groupBox1.Controls.Add(this.btnGetToken);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.txtTadt);
            this.groupBox1.Controls.Add(this.txtToken);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.txtPassword);
            this.groupBox1.Location = new System.Drawing.Point(460, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(471, 190);
            this.groupBox1.TabIndex = 7;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "اتصال به وب سایت";
            // 
            // btnAuto
            // 
            this.btnAuto.Location = new System.Drawing.Point(7, 49);
            this.btnAuto.Name = "btnAuto";
            this.btnAuto.Size = new System.Drawing.Size(155, 25);
            this.btnAuto.TabIndex = 15;
            this.btnAuto.Text = "بروز رسانی خودکار";
            this.btnAuto.UseVisualStyleBackColor = true;
            this.btnAuto.Click += new System.EventHandler(this.btnAuto_Click);
            // 
            // btnReadFactors
            // 
            this.btnReadFactors.Location = new System.Drawing.Point(7, 154);
            this.btnReadFactors.Name = "btnReadFactors";
            this.btnReadFactors.Size = new System.Drawing.Size(155, 25);
            this.btnReadFactors.TabIndex = 14;
            this.btnReadFactors.Text = "خواندن فاکتور سایت";
            this.btnReadFactors.UseVisualStyleBackColor = true;
            this.btnReadFactors.Click += new System.EventHandler(this.btnReadFactors_Click);
            // 
            // label5
            // 
            this.label5.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(396, 153);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(49, 13);
            this.label5.TabIndex = 6;
            this.label5.Text = "تا تاریخ :";
            // 
            // btnRegister
            // 
            this.btnRegister.Location = new System.Drawing.Point(7, 102);
            this.btnRegister.Name = "btnRegister";
            this.btnRegister.Size = new System.Drawing.Size(155, 25);
            this.btnRegister.TabIndex = 10;
            this.btnRegister.Text = "ثبت محصولات در سایت";
            this.btnRegister.UseVisualStyleBackColor = true;
            this.btnRegister.Click += new System.EventHandler(this.btnRegister_Click);
            // 
            // btnReadSite
            // 
            this.btnReadSite.Location = new System.Drawing.Point(7, 128);
            this.btnReadSite.Name = "btnReadSite";
            this.btnReadSite.Size = new System.Drawing.Size(155, 25);
            this.btnReadSite.TabIndex = 12;
            this.btnReadSite.Text = "خواندن محصولات سایت";
            this.btnReadSite.UseVisualStyleBackColor = true;
            this.btnReadSite.Click += new System.EventHandler(this.btnReadSite_Click);
            // 
            // label4
            // 
            this.label4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(396, 130);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(49, 13);
            this.label4.TabIndex = 4;
            this.label4.Text = "از تاریخ :";
            // 
            // txtAzdt
            // 
            this.txtAzdt.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txtAzdt.Location = new System.Drawing.Point(299, 127);
            this.txtAzdt.MaxLength = 10;
            this.txtAzdt.Name = "txtAzdt";
            this.txtAzdt.Size = new System.Drawing.Size(91, 20);
            this.txtAzdt.TabIndex = 5;
            this.txtAzdt.Text = "1398/01/01";
            this.txtAzdt.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtAzdt_KeyPress);
            this.txtAzdt.Validating += new System.ComponentModel.CancelEventHandler(this.txtAzdt_Validating);
            // 
            // txtTadt
            // 
            this.txtTadt.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txtTadt.Location = new System.Drawing.Point(299, 150);
            this.txtTadt.MaxLength = 10;
            this.txtTadt.Name = "txtTadt";
            this.txtTadt.Size = new System.Drawing.Size(91, 20);
            this.txtTadt.TabIndex = 7;
            this.txtTadt.Text = "1398/12/29";
            this.txtTadt.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtTadt_KeyPress);
            this.txtTadt.Validating += new System.ComponentModel.CancelEventHandler(this.txtTadt_Validating);
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.Controls.Add(this.SepehrDGV);
            this.groupBox2.Location = new System.Drawing.Point(12, 12);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(442, 190);
            this.groupBox2.TabIndex = 8;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "سپهر";
            // 
            // SepehrDGV
            // 
            this.SepehrDGV.AllowUserToAddRows = false;
            this.SepehrDGV.AllowUserToDeleteRows = false;
            this.SepehrDGV.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.SepehrDGV.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.SepehrDGV.BackgroundColor = System.Drawing.Color.White;
            this.SepehrDGV.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.SepehrDGV.Location = new System.Drawing.Point(6, 19);
            this.SepehrDGV.Name = "SepehrDGV";
            this.SepehrDGV.RowHeadersWidth = 30;
            this.SepehrDGV.Size = new System.Drawing.Size(430, 165);
            this.SepehrDGV.TabIndex = 0;
            // 
            // groupBox3
            // 
            this.groupBox3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.groupBox3.Controls.Add(this.WebDGV);
            this.groupBox3.Location = new System.Drawing.Point(12, 208);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(442, 201);
            this.groupBox3.TabIndex = 9;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "وب سایت";
            // 
            // WebDGV
            // 
            this.WebDGV.AllowUserToAddRows = false;
            this.WebDGV.AllowUserToDeleteRows = false;
            this.WebDGV.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.WebDGV.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.WebDGV.BackgroundColor = System.Drawing.Color.White;
            this.WebDGV.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.WebDGV.Location = new System.Drawing.Point(6, 19);
            this.WebDGV.Name = "WebDGV";
            this.WebDGV.Size = new System.Drawing.Size(430, 176);
            this.WebDGV.TabIndex = 0;
            // 
            // groupBox4
            // 
            this.groupBox4.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox4.Controls.Add(this.FactorDGV);
            this.groupBox4.Location = new System.Drawing.Point(460, 208);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(471, 201);
            this.groupBox4.TabIndex = 14;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "فاکتور های سایت";
            // 
            // FactorDGV
            // 
            this.FactorDGV.AllowUserToAddRows = false;
            this.FactorDGV.AllowUserToDeleteRows = false;
            this.FactorDGV.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.FactorDGV.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.FactorDGV.BackgroundColor = System.Drawing.Color.White;
            this.FactorDGV.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.FactorDGV.Location = new System.Drawing.Point(7, 19);
            this.FactorDGV.Name = "FactorDGV";
            this.FactorDGV.RowHeadersWidth = 30;
            this.FactorDGV.Size = new System.Drawing.Size(458, 176);
            this.FactorDGV.TabIndex = 1;
            // 
            // progressBar1
            // 
            this.progressBar1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.progressBar1.Location = new System.Drawing.Point(12, 415);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(919, 23);
            this.progressBar1.TabIndex = 15;
            // 
            // timer1
            // 
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // rb30
            // 
            this.rb30.AutoSize = true;
            this.rb30.Checked = true;
            this.rb30.Location = new System.Drawing.Point(193, 22);
            this.rb30.Name = "rb30";
            this.rb30.Size = new System.Drawing.Size(65, 17);
            this.rb30.TabIndex = 16;
            this.rb30.TabStop = true;
            this.rb30.Text = "30 دقیقه";
            this.rb30.UseVisualStyleBackColor = true;
            // 
            // rb1
            // 
            this.rb1.AutoSize = true;
            this.rb1.Location = new System.Drawing.Point(125, 22);
            this.rb1.Name = "rb1";
            this.rb1.Size = new System.Drawing.Size(62, 17);
            this.rb1.TabIndex = 17;
            this.rb1.Text = "1 ساعت";
            this.rb1.UseVisualStyleBackColor = true;
            // 
            // rb2
            // 
            this.rb2.AutoSize = true;
            this.rb2.Location = new System.Drawing.Point(57, 22);
            this.rb2.Name = "rb2";
            this.rb2.Size = new System.Drawing.Size(62, 17);
            this.rb2.TabIndex = 18;
            this.rb2.Text = "2 ساعت";
            this.rb2.UseVisualStyleBackColor = true;
            // 
            // btnFix
            // 
            this.btnFix.Location = new System.Drawing.Point(7, 75);
            this.btnFix.Name = "btnFix";
            this.btnFix.Size = new System.Drawing.Size(57, 25);
            this.btnFix.TabIndex = 19;
            this.btnFix.Text = "ترمیم";
            this.btnFix.UseVisualStyleBackColor = true;
            this.btnFix.Click += new System.EventHandler(this.btnFix_Click);
            // 
            // btnStopAuto
            // 
            this.btnStopAuto.Location = new System.Drawing.Point(70, 75);
            this.btnStopAuto.Name = "btnStopAuto";
            this.btnStopAuto.Size = new System.Drawing.Size(92, 25);
            this.btnStopAuto.TabIndex = 20;
            this.btnStopAuto.Text = "توقف خودکار";
            this.btnStopAuto.UseVisualStyleBackColor = true;
            this.btnStopAuto.Click += new System.EventHandler(this.btnStopAuto_Click);
            // 
            // lblLastUpdate
            // 
            this.lblLastUpdate.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblLastUpdate.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this.lblLastUpdate.ForeColor = System.Drawing.Color.Red;
            this.lblLastUpdate.Location = new System.Drawing.Point(168, 52);
            this.lblLastUpdate.Name = "lblLastUpdate";
            this.lblLastUpdate.Size = new System.Drawing.Size(125, 127);
            this.lblLastUpdate.TabIndex = 21;
            this.lblLastUpdate.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(943, 450);
            this.Controls.Add(this.progressBar1);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Name = "frmMain";
            this.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.Text = "انتقال اطلاعات";
            this.Load += new System.EventHandler(this.frmMain_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.SepehrDGV)).EndInit();
            this.groupBox3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.WebDGV)).EndInit();
            this.groupBox4.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.FactorDGV)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtUserName;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtPassword;
        private System.Windows.Forms.TextBox txtToken;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btnGetToken;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.DataGridView SepehrDGV;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.DataGridView WebDGV;
        private System.Windows.Forms.Button btnRegister;
        private System.Windows.Forms.Button btnReadSite;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.Button btnReadFactors;
        private System.Windows.Forms.TextBox txtAzdt;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtTadt;
        private System.Windows.Forms.DataGridView FactorDGV;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.Button btnAuto;
        private System.Windows.Forms.RadioButton rb2;
        private System.Windows.Forms.RadioButton rb1;
        private System.Windows.Forms.RadioButton rb30;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Button btnStopAuto;
        private System.Windows.Forms.Button btnFix;
        private System.Windows.Forms.Label lblLastUpdate;
    }
}

