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
            this.modelL = new System.Windows.Forms.Label();
            this.idL = new System.Windows.Forms.Label();
            this.versionL = new System.Windows.Forms.Label();
            this.firmwareL = new System.Windows.Forms.Label();
            this.statsConnectL = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.LogTB = new System.Windows.Forms.TextBox();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // Connect
            // 
            this.Connect.Location = new System.Drawing.Point(267, 27);
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
            this.RateCB.Location = new System.Drawing.Point(140, 27);
            this.RateCB.Name = "RateCB";
            this.RateCB.Size = new System.Drawing.Size(121, 21);
            this.RateCB.TabIndex = 1;
            // 
            // PortCB
            // 
            this.PortCB.FormattingEnabled = true;
            this.PortCB.Location = new System.Drawing.Point(12, 27);
            this.PortCB.Name = "PortCB";
            this.PortCB.Size = new System.Drawing.Size(121, 21);
            this.PortCB.TabIndex = 2;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.modelL);
            this.groupBox1.Controls.Add(this.idL);
            this.groupBox1.Controls.Add(this.versionL);
            this.groupBox1.Controls.Add(this.firmwareL);
            this.groupBox1.Controls.Add(this.statsConnectL);
            this.groupBox1.Location = new System.Drawing.Point(13, 66);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(270, 537);
            this.groupBox1.TabIndex = 3;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Данные о ККТ";
            // 
            // modelL
            // 
            this.modelL.AutoSize = true;
            this.modelL.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.modelL.Location = new System.Drawing.Point(6, 130);
            this.modelL.Name = "modelL";
            this.modelL.Size = new System.Drawing.Size(61, 16);
            this.modelL.TabIndex = 4;
            this.modelL.Text = "Модель:";
            // 
            // idL
            // 
            this.idL.AutoSize = true;
            this.idL.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.idL.Location = new System.Drawing.Point(6, 101);
            this.idL.Name = "idL";
            this.idL.Size = new System.Drawing.Size(149, 16);
            this.idL.TabIndex = 3;
            this.idL.Text = "Серийный номер ККТ:";
            // 
            // versionL
            // 
            this.versionL.AutoSize = true;
            this.versionL.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.versionL.Location = new System.Drawing.Point(6, 72);
            this.versionL.Name = "versionL";
            this.versionL.Size = new System.Drawing.Size(125, 16);
            this.versionL.TabIndex = 2;
            this.versionL.Text = "Версия прошивки:";
            // 
            // firmwareL
            // 
            this.firmwareL.AutoSize = true;
            this.firmwareL.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.firmwareL.Location = new System.Drawing.Point(6, 43);
            this.firmwareL.Name = "firmwareL";
            this.firmwareL.Size = new System.Drawing.Size(88, 16);
            this.firmwareL.TabIndex = 1;
            this.firmwareL.Text = "Код firmware:";
            // 
            // statsConnectL
            // 
            this.statsConnectL.AutoSize = true;
            this.statsConnectL.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.statsConnectL.Location = new System.Drawing.Point(44, 16);
            this.statsConnectL.Name = "statsConnectL";
            this.statsConnectL.Size = new System.Drawing.Size(181, 20);
            this.statsConnectL.TabIndex = 0;
            this.statsConnectL.Text = "ККТ: не подключено";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(1020, 27);
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
            // LogTB
            // 
            this.LogTB.Location = new System.Drawing.Point(288, 66);
            this.LogTB.Multiline = true;
            this.LogTB.Name = "LogTB";
            this.LogTB.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.LogTB.Size = new System.Drawing.Size(892, 536);
            this.LogTB.TabIndex = 8;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1192, 615);
            this.Controls.Add(this.LogTB);
            this.Controls.Add(this.progressBar1);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.PortCB);
            this.Controls.Add(this.RateCB);
            this.Controls.Add(this.Connect);
            this.Name = "Form1";
            this.Text = "Form1";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button Connect;
        private System.Windows.Forms.ComboBox RateCB;
        private System.Windows.Forms.ComboBox PortCB;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label statsConnectL;
        private System.Windows.Forms.Label versionL;
        private System.Windows.Forms.Label firmwareL;
        private System.Windows.Forms.Label idL;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.TextBox LogTB;
        private System.Windows.Forms.Label modelL;
    }
}

