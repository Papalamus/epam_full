namespace WindowsFormsApplication1
{
    partial class Form1
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.articleRadio = new System.Windows.Forms.RadioButton();
            this.personRadio = new System.Windows.Forms.RadioButton();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.adoRadio = new System.Windows.Forms.RadioButton();
            this.myORMradio = new System.Windows.Forms.RadioButton();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.idBox = new System.Windows.Forms.TextBox();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.articleRadio);
            this.groupBox1.Controls.Add(this.personRadio);
            this.groupBox1.Location = new System.Drawing.Point(12, 90);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(146, 69);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Entity";
            // 
            // articleRadio
            // 
            this.articleRadio.AutoSize = true;
            this.articleRadio.Location = new System.Drawing.Point(6, 42);
            this.articleRadio.Name = "articleRadio";
            this.articleRadio.Size = new System.Drawing.Size(54, 17);
            this.articleRadio.TabIndex = 1;
            this.articleRadio.Text = "Article";
            this.articleRadio.UseVisualStyleBackColor = true;
            this.articleRadio.CheckedChanged += new System.EventHandler(this.entity_CheckedChanged);
            // 
            // personRadio
            // 
            this.personRadio.AutoSize = true;
            this.personRadio.Checked = true;
            this.personRadio.Location = new System.Drawing.Point(6, 19);
            this.personRadio.Name = "personRadio";
            this.personRadio.Size = new System.Drawing.Size(58, 17);
            this.personRadio.TabIndex = 0;
            this.personRadio.TabStop = true;
            this.personRadio.Text = "Person";
            this.personRadio.UseVisualStyleBackColor = true;
            this.personRadio.CheckedChanged += new System.EventHandler(this.entity_CheckedChanged);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.adoRadio);
            this.groupBox2.Controls.Add(this.myORMradio);
            this.groupBox2.Location = new System.Drawing.Point(12, 12);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(146, 72);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "IPersonConnecter";
            // 
            // adoRadio
            // 
            this.adoRadio.AutoSize = true;
            this.adoRadio.Location = new System.Drawing.Point(6, 42);
            this.adoRadio.Name = "adoRadio";
            this.adoRadio.Size = new System.Drawing.Size(93, 17);
            this.adoRadio.TabIndex = 3;
            this.adoRadio.TabStop = true;
            this.adoRadio.Text = "AdoConnecter";
            this.adoRadio.UseVisualStyleBackColor = true;
            this.adoRadio.CheckedChanged += new System.EventHandler(this.connector_CheckedChanged);
            // 
            // myORMradio
            // 
            this.myORMradio.AutoSize = true;
            this.myORMradio.Checked = true;
            this.myORMradio.Location = new System.Drawing.Point(6, 19);
            this.myORMradio.Name = "myORMradio";
            this.myORMradio.Size = new System.Drawing.Size(140, 17);
            this.myORMradio.TabIndex = 2;
            this.myORMradio.TabStop = true;
            this.myORMradio.Text = "MyORM ConnecterType";
            this.myORMradio.UseVisualStyleBackColor = true;
            this.myORMradio.CheckedChanged += new System.EventHandler(this.connector_CheckedChanged);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(12, 193);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(146, 23);
            this.button1.TabIndex = 2;
            this.button1.Text = "Insert";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(12, 222);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 3;
            this.button2.Text = "GetByID";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(12, 165);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(146, 22);
            this.button3.TabIndex = 4;
            this.button3.Text = "GetAll";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(12, 251);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(75, 23);
            this.button4.TabIndex = 5;
            this.button4.Text = "DeleteByID";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(164, 12);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(582, 261);
            this.dataGridView1.TabIndex = 6;
            // 
            // idBox
            // 
            this.idBox.Location = new System.Drawing.Point(93, 239);
            this.idBox.Name = "idBox";
            this.idBox.Size = new System.Drawing.Size(65, 20);
            this.idBox.TabIndex = 7;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(758, 287);
            this.Controls.Add(this.idBox);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton articleRadio;
        private System.Windows.Forms.RadioButton personRadio;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.RadioButton adoRadio;
        private System.Windows.Forms.RadioButton myORMradio;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.TextBox idBox;
    }
}

