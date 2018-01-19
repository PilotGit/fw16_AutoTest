namespace FW16AutoTestProgram
{
    partial class Form1
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.Connect = new System.Windows.Forms.Button();
            this.RateCB = new System.Windows.Forms.ComboBox();
            this.PortCB = new System.Windows.Forms.ComboBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label_id = new System.Windows.Forms.Label();
            this.label_version = new System.Windows.Forms.Label();
            this.label_firmware = new System.Windows.Forms.Label();
            this.label_stats_connect = new System.Windows.Forms.Label();
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.button1 = new System.Windows.Forms.Button();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // Connect
            // 
            this.Connect.Location = new System.Drawing.Point(267, 12);
            this.Connect.Name = "Connect";
            this.Connect.Size = new System.Drawing.Size(108, 23);
            this.Connect.TabIndex = 0;
            this.Connect.Text = "Подключиться";
            this.Connect.UseVisualStyleBackColor = true;
            this.Connect.Click += new System.EventHandler(this.ConnectButtonClick);
            // 
            // RateCB
            // 
            this.RateCB.FormattingEnabled = true;
            this.RateCB.Location = new System.Drawing.Point(140, 12);
            this.RateCB.Name = "RateCB";
            this.RateCB.Size = new System.Drawing.Size(121, 21);
            this.RateCB.TabIndex = 1;
            // 
            // PortCB
            // 
            this.PortCB.FormattingEnabled = true;
            this.PortCB.Location = new System.Drawing.Point(13, 12);
            this.PortCB.Name = "PortCB";
            this.PortCB.Size = new System.Drawing.Size(121, 21);
            this.PortCB.TabIndex = 2;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label_id);
            this.groupBox1.Controls.Add(this.label_version);
            this.groupBox1.Controls.Add(this.label_firmware);
            this.groupBox1.Controls.Add(this.label_stats_connect);
            this.groupBox1.Location = new System.Drawing.Point(13, 41);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(270, 537);
            this.groupBox1.TabIndex = 3;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Данные о ККТ";
            // 
            // label_id
            // 
            this.label_id.AutoSize = true;
            this.label_id.Font = new System.Drawing.Font("Lucida Bright", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label_id.Location = new System.Drawing.Point(6, 96);
            this.label_id.Name = "label_id";
            this.label_id.Size = new System.Drawing.Size(0, 17);
            this.label_id.TabIndex = 3;
            // 
            // label_version
            // 
            this.label_version.AutoSize = true;
            this.label_version.Font = new System.Drawing.Font("Lucida Bright", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label_version.Location = new System.Drawing.Point(6, 73);
            this.label_version.Name = "label_version";
            this.label_version.Size = new System.Drawing.Size(0, 17);
            this.label_version.TabIndex = 2;
            // 
            // label_firmware
            // 
            this.label_firmware.AutoSize = true;
            this.label_firmware.Font = new System.Drawing.Font("Lucida Bright", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label_firmware.Location = new System.Drawing.Point(5, 46);
            this.label_firmware.Name = "label_firmware";
            this.label_firmware.Size = new System.Drawing.Size(0, 17);
            this.label_firmware.TabIndex = 1;
            // 
            // label_stats_connect
            // 
            this.label_stats_connect.AutoSize = true;
            this.label_stats_connect.Font = new System.Drawing.Font("Kristen ITC", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label_stats_connect.Location = new System.Drawing.Point(6, 19);
            this.label_stats_connect.Name = "label_stats_connect";
            this.label_stats_connect.Size = new System.Drawing.Size(221, 27);
            this.label_stats_connect.TabIndex = 0;
            this.label_stats_connect.Text = "ККТ: не подключено";
            // 
            // listBox1
            // 
            this.listBox1.FormattingEnabled = true;
            this.listBox1.Location = new System.Drawing.Point(289, 41);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new System.Drawing.Size(893, 537);
            this.listBox1.TabIndex = 5;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(1022, 12);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(160, 23);
            this.button1.TabIndex = 6;
            this.button1.Text = "Начать тестирование";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.BeginTest);
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(13, 584);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(1169, 23);
            this.progressBar1.TabIndex = 7;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1192, 615);
            this.Controls.Add(this.progressBar1);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.listBox1);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.PortCB);
            this.Controls.Add(this.RateCB);
            this.Controls.Add(this.Connect);
            this.Name = "Form1";
            this.Text = "Form1";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button Connect;
        private System.Windows.Forms.ComboBox RateCB;
        private System.Windows.Forms.ComboBox PortCB;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ListBox listBox1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label_stats_connect;
        private System.Windows.Forms.Label label_version;
        private System.Windows.Forms.Label label_firmware;
        private System.Windows.Forms.Label label_id;
        private System.Windows.Forms.ProgressBar progressBar1;
    }
}

