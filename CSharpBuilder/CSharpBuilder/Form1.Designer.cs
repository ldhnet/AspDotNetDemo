namespace CSharpBuilder
{
    partial class Form1
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
            this.databaseurl = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.username = new System.Windows.Forms.TextBox();
            this.userpwd = new System.Windows.Forms.TextBox();
            this.Databaselist = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.tablelist = new System.Windows.Forms.ListBox();
            this.label5 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.button2 = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.createEntity = new System.Windows.Forms.Button();
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.button3 = new System.Windows.Forms.Button();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // databaseurl
            // 
            this.databaseurl.Location = new System.Drawing.Point(94, 30);
            this.databaseurl.Name = "databaseurl";
            this.databaseurl.Size = new System.Drawing.Size(163, 23);
            this.databaseurl.TabIndex = 19;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(46, 95);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(44, 17);
            this.label2.TabIndex = 3;
            this.label2.Text = "用户名";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(47, 134);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(44, 17);
            this.label3.TabIndex = 4;
            this.label3.Text = "密   码";
            // 
            // username
            // 
            this.username.Location = new System.Drawing.Point(120, 95);
            this.username.Name = "username";
            this.username.Size = new System.Drawing.Size(114, 23);
            this.username.TabIndex = 5;
            // 
            // userpwd
            // 
            this.userpwd.Location = new System.Drawing.Point(120, 130);
            this.userpwd.Name = "userpwd";
            this.userpwd.Size = new System.Drawing.Size(114, 23);
            this.userpwd.TabIndex = 6;
            // 
            // Databaselist
            // 
            this.Databaselist.FormattingEnabled = true;
            this.Databaselist.Location = new System.Drawing.Point(26, 234);
            this.Databaselist.Name = "Databaselist";
            this.Databaselist.Size = new System.Drawing.Size(194, 25);
            this.Databaselist.TabIndex = 8;
            this.Databaselist.SelectedIndexChanged += new System.EventHandler(this.Databaselist_SelectedIndexChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(26, 213);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(68, 17);
            this.label4.TabIndex = 9;
            this.label4.Text = "选择数据库";
            // 
            // tablelist
            // 
            this.tablelist.FormattingEnabled = true;
            this.tablelist.ItemHeight = 17;
            this.tablelist.Location = new System.Drawing.Point(26, 302);
            this.tablelist.Name = "tablelist";
            this.tablelist.Size = new System.Drawing.Size(194, 259);
            this.tablelist.TabIndex = 10;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(26, 271);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(44, 17);
            this.label5.TabIndex = 11;
            this.label5.Text = "选择表";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(197, 144);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 29);
            this.button1.TabIndex = 0;
            this.button1.Text = "登录";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.button2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.databaseurl);
            this.groupBox1.Location = new System.Drawing.Point(26, 26);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(293, 175);
            this.groupBox1.TabIndex = 13;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "登录信息";
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(173, 138);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 15;
            this.button2.Text = "登录";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(18, 33);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(68, 17);
            this.label1.TabIndex = 14;
            this.label1.Text = "数据库地址";
            // 
            // createEntity
            // 
            this.createEntity.Location = new System.Drawing.Point(226, 385);
            this.createEntity.Name = "createEntity";
            this.createEntity.Size = new System.Drawing.Size(103, 30);
            this.createEntity.TabIndex = 16;
            this.createEntity.Text = "生成实体类";
            this.createEntity.UseVisualStyleBackColor = true;
            this.createEntity.Click += new System.EventHandler(this.createEntity_Click);
            // 
            // richTextBox1
            // 
            this.richTextBox1.Location = new System.Drawing.Point(364, 26);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.Size = new System.Drawing.Size(426, 535);
            this.richTextBox1.TabIndex = 17;
            this.richTextBox1.Text = "";
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(226, 438);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(103, 30);
            this.button3.TabIndex = 18;
            this.button3.Text = "生成类文件";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(229, 341);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(129, 23);
            this.textBox1.TabIndex = 21;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(226, 315);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(56, 17);
            this.label6.TabIndex = 20;
            this.label6.Text = "命名空间";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(815, 580);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.richTextBox1);
            this.Controls.Add(this.createEntity);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.tablelist);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.Databaselist);
            this.Controls.Add(this.userpwd);
            this.Controls.Add(this.username);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.groupBox1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private TextBox databaseurl;
        private Label label2;
        private Label label3;
        private TextBox username;
        private TextBox userpwd;
        private Button button1;
        private ComboBox Databaselist;
        private Label label4;
        private ListBox tablelist;
        private Label label5;
        private GroupBox groupBox1;
        private Label label1;
        private Button button2;
        private Button createEntity;
        private RichTextBox richTextBox1;
        private Button button3;
        private TextBox textBox1;
        private Label label6;
    }
}