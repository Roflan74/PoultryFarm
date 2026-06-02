namespace PoultryFarm
{
    partial class MainForm
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
            this.btnDirectorPanel = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.lblDate = new System.Windows.Forms.Label();
            this.lblGreeting = new System.Windows.Forms.Label();
            this.lblTime = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.panel2 = new System.Windows.Forms.Panel();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.pictureBox3 = new System.Windows.Forms.PictureBox();
            this.btnDictionaryAssignments = new System.Windows.Forms.Button();
            this.btnDictionaryShops = new System.Windows.Forms.Button();
            this.btnDictionaryDiets = new System.Windows.Forms.Button();
            this.btnDictionaryWorkers = new System.Windows.Forms.Button();
            this.btnDictionaryCages = new System.Windows.Forms.Button();
            this.btnDictionaryBreeds = new System.Windows.Forms.Button();
            this.btnDictionaryChickens = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label1.Location = new System.Drawing.Point(105, 20);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(325, 45);
            this.label1.TabIndex = 4;
            this.label1.Text = "АСУ Птицефабрика";
            // 
            // btnDirectorPanel
            // 
            this.btnDirectorPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)));
            this.btnDirectorPanel.BackColor = System.Drawing.Color.White;
            this.btnDirectorPanel.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnDirectorPanel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDirectorPanel.Font = new System.Drawing.Font("Segoe UI", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btnDirectorPanel.Location = new System.Drawing.Point(653, 19);
            this.btnDirectorPanel.Margin = new System.Windows.Forms.Padding(4);
            this.btnDirectorPanel.Name = "btnDirectorPanel";
            this.btnDirectorPanel.Size = new System.Drawing.Size(206, 62);
            this.btnDirectorPanel.TabIndex = 8;
            this.btnDirectorPanel.Text = "Открыть отчеты >";
            this.btnDirectorPanel.UseVisualStyleBackColor = false;
            this.btnDirectorPanel.Click += new System.EventHandler(this.btnDirectorPanel_Click);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.White;
            this.panel1.Controls.Add(this.pictureBox2);
            this.panel1.Controls.Add(this.lblDate);
            this.panel1.Controls.Add(this.lblGreeting);
            this.panel1.Controls.Add(this.lblTime);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.pictureBox1);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(984, 100);
            this.panel1.TabIndex = 9;
            // 
            // pictureBox2
            // 
            this.pictureBox2.Image = global::PoultryFarm.Properties.Resources.calendar_clock_42dp_6D6D6D_FILL0_wght400_GRAD0_opsz40;
            this.pictureBox2.Location = new System.Drawing.Point(677, 44);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(42, 42);
            this.pictureBox2.TabIndex = 13;
            this.pictureBox2.TabStop = false;
            // 
            // lblDate
            // 
            this.lblDate.AutoSize = true;
            this.lblDate.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lblDate.ForeColor = System.Drawing.SystemColors.GrayText;
            this.lblDate.Location = new System.Drawing.Point(725, 44);
            this.lblDate.Name = "lblDate";
            this.lblDate.Size = new System.Drawing.Size(88, 21);
            this.lblDate.TabIndex = 11;
            this.lblDate.Text = "01.01.1970";
            // 
            // lblGreeting
            // 
            this.lblGreeting.AutoSize = true;
            this.lblGreeting.Font = new System.Drawing.Font("Segoe UI", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lblGreeting.Location = new System.Drawing.Point(672, 9);
            this.lblGreeting.Name = "lblGreeting";
            this.lblGreeting.Size = new System.Drawing.Size(273, 30);
            this.lblGreeting.TabIndex = 12;
            this.lblGreeting.Text = "Добрый день, Директор!";
            // 
            // lblTime
            // 
            this.lblTime.AutoSize = true;
            this.lblTime.BackColor = System.Drawing.Color.White;
            this.lblTime.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lblTime.ForeColor = System.Drawing.SystemColors.GrayText;
            this.lblTime.Location = new System.Drawing.Point(725, 65);
            this.lblTime.Name = "lblTime";
            this.lblTime.Size = new System.Drawing.Size(70, 21);
            this.lblTime.TabIndex = 10;
            this.lblTime.Text = "12:00:00";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label2.ForeColor = System.Drawing.SystemColors.GrayText;
            this.label2.Location = new System.Drawing.Point(109, 63);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(267, 21);
            this.label2.TabIndex = 5;
            this.label2.Text = "Система управления предприятием";
            // 
            // pictureBox1
            // 
            this.pictureBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)));
            this.pictureBox1.Image = global::PoultryFarm.Properties.Resources.egg_64dp_F19E39_FILL0_wght400_GRAD0_opsz48;
            this.pictureBox1.Location = new System.Drawing.Point(36, 20);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(64, 64);
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Interval = 1000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.RoyalBlue;
            this.panel2.Controls.Add(this.label4);
            this.panel2.Controls.Add(this.label3);
            this.panel2.Controls.Add(this.btnDirectorPanel);
            this.panel2.Controls.Add(this.pictureBox3);
            this.panel2.Location = new System.Drawing.Point(36, 125);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(912, 100);
            this.panel2.TabIndex = 10;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label4.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.label4.Location = new System.Drawing.Point(47, 45);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(358, 42);
            this.label4.TabIndex = 11;
            this.label4.Text = "Аналитические запросы, статистика по цехам и\r\nформирование отчетов в форматы Word" +
    " и Excel.";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Segoe UI", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label3.ForeColor = System.Drawing.Color.White;
            this.label3.Location = new System.Drawing.Point(89, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(316, 45);
            this.label3.TabIndex = 10;
            this.label3.Text = "Панель Директора";
            // 
            // pictureBox3
            // 
            this.pictureBox3.Image = global::PoultryFarm.Properties.Resources.bar_chart_32dp_FFFFFF_FILL0_wght400_GRAD0_opsz40;
            this.pictureBox3.Location = new System.Drawing.Point(51, 10);
            this.pictureBox3.Name = "pictureBox3";
            this.pictureBox3.Size = new System.Drawing.Size(32, 32);
            this.pictureBox3.TabIndex = 9;
            this.pictureBox3.TabStop = false;
            // 
            // btnDictionaryAssignments
            // 
            this.btnDictionaryAssignments.BackColor = System.Drawing.Color.White;
            this.btnDictionaryAssignments.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnDictionaryAssignments.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDictionaryAssignments.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btnDictionaryAssignments.Image = global::PoultryFarm.Properties.Resources.link_48dp_4B77D1_FILL0_wght400_GRAD0_opsz48;
            this.btnDictionaryAssignments.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnDictionaryAssignments.Location = new System.Drawing.Point(220, 356);
            this.btnDictionaryAssignments.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.btnDictionaryAssignments.Name = "btnDictionaryAssignments";
            this.btnDictionaryAssignments.Size = new System.Drawing.Size(180, 100);
            this.btnDictionaryAssignments.TabIndex = 7;
            this.btnDictionaryAssignments.Text = "Закрепление\r\nклеток";
            this.btnDictionaryAssignments.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnDictionaryAssignments.UseVisualStyleBackColor = false;
            this.btnDictionaryAssignments.Click += new System.EventHandler(this.btnDictionaryAssignments_Click);
            // 
            // btnDictionaryShops
            // 
            this.btnDictionaryShops.BackColor = System.Drawing.Color.White;
            this.btnDictionaryShops.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnDictionaryShops.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDictionaryShops.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btnDictionaryShops.Image = global::PoultryFarm.Properties.Resources.factory_48dp_18333C_FILL0_wght400_GRAD0_opsz48;
            this.btnDictionaryShops.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnDictionaryShops.Location = new System.Drawing.Point(588, 250);
            this.btnDictionaryShops.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.btnDictionaryShops.Name = "btnDictionaryShops";
            this.btnDictionaryShops.Size = new System.Drawing.Size(180, 100);
            this.btnDictionaryShops.TabIndex = 6;
            this.btnDictionaryShops.Text = "Цеха";
            this.btnDictionaryShops.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnDictionaryShops.UseVisualStyleBackColor = false;
            this.btnDictionaryShops.Click += new System.EventHandler(this.btnDictionaryShops_Click);
            // 
            // btnDictionaryDiets
            // 
            this.btnDictionaryDiets.BackColor = System.Drawing.Color.White;
            this.btnDictionaryDiets.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnDictionaryDiets.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDictionaryDiets.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btnDictionaryDiets.Image = global::PoultryFarm.Properties.Resources.wheat_48dp_F19E39_FILL0_wght400_GRAD0_opsz48;
            this.btnDictionaryDiets.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnDictionaryDiets.Location = new System.Drawing.Point(220, 250);
            this.btnDictionaryDiets.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.btnDictionaryDiets.Name = "btnDictionaryDiets";
            this.btnDictionaryDiets.Size = new System.Drawing.Size(180, 100);
            this.btnDictionaryDiets.TabIndex = 5;
            this.btnDictionaryDiets.Text = "Диеты";
            this.btnDictionaryDiets.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnDictionaryDiets.UseVisualStyleBackColor = false;
            this.btnDictionaryDiets.Click += new System.EventHandler(this.btnDictionaryDiets_Click);
            // 
            // btnDictionaryWorkers
            // 
            this.btnDictionaryWorkers.BackColor = System.Drawing.Color.White;
            this.btnDictionaryWorkers.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnDictionaryWorkers.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDictionaryWorkers.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btnDictionaryWorkers.Image = global::PoultryFarm.Properties.Resources.patient_list_48dp_75FB4C_FILL0_wght400_GRAD0_opsz48;
            this.btnDictionaryWorkers.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnDictionaryWorkers.Location = new System.Drawing.Point(404, 250);
            this.btnDictionaryWorkers.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.btnDictionaryWorkers.Name = "btnDictionaryWorkers";
            this.btnDictionaryWorkers.Size = new System.Drawing.Size(180, 100);
            this.btnDictionaryWorkers.TabIndex = 3;
            this.btnDictionaryWorkers.Text = "Работники";
            this.btnDictionaryWorkers.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnDictionaryWorkers.UseVisualStyleBackColor = false;
            this.btnDictionaryWorkers.Click += new System.EventHandler(this.btnDictionaryWorkers_Click);
            // 
            // btnDictionaryCages
            // 
            this.btnDictionaryCages.BackColor = System.Drawing.Color.White;
            this.btnDictionaryCages.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnDictionaryCages.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDictionaryCages.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btnDictionaryCages.Image = global::PoultryFarm.Properties.Resources.grid_on_48dp_634FA2_FILL0_wght400_GRAD0_opsz48;
            this.btnDictionaryCages.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnDictionaryCages.Location = new System.Drawing.Point(772, 250);
            this.btnDictionaryCages.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.btnDictionaryCages.Name = "btnDictionaryCages";
            this.btnDictionaryCages.Size = new System.Drawing.Size(176, 100);
            this.btnDictionaryCages.TabIndex = 2;
            this.btnDictionaryCages.Text = "Клетки";
            this.btnDictionaryCages.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnDictionaryCages.UseVisualStyleBackColor = false;
            this.btnDictionaryCages.Click += new System.EventHandler(this.btnDictionaryCages_Click);
            // 
            // btnDictionaryBreeds
            // 
            this.btnDictionaryBreeds.BackColor = System.Drawing.Color.White;
            this.btnDictionaryBreeds.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnDictionaryBreeds.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDictionaryBreeds.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btnDictionaryBreeds.Image = global::PoultryFarm.Properties.Resources.book_48dp_5985E1_FILL0_wght400_GRAD0_opsz48;
            this.btnDictionaryBreeds.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnDictionaryBreeds.Location = new System.Drawing.Point(36, 250);
            this.btnDictionaryBreeds.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.btnDictionaryBreeds.Name = "btnDictionaryBreeds";
            this.btnDictionaryBreeds.Size = new System.Drawing.Size(180, 100);
            this.btnDictionaryBreeds.TabIndex = 1;
            this.btnDictionaryBreeds.Text = "Породы";
            this.btnDictionaryBreeds.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnDictionaryBreeds.UseVisualStyleBackColor = false;
            this.btnDictionaryBreeds.Click += new System.EventHandler(this.btnDictionaryBreeds_Click);
            // 
            // btnDictionaryChickens
            // 
            this.btnDictionaryChickens.BackColor = System.Drawing.Color.White;
            this.btnDictionaryChickens.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnDictionaryChickens.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDictionaryChickens.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btnDictionaryChickens.Image = global::PoultryFarm.Properties.Resources.egg_48dp_F19E39_FILL0_wght400_GRAD0_opsz48;
            this.btnDictionaryChickens.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnDictionaryChickens.Location = new System.Drawing.Point(36, 356);
            this.btnDictionaryChickens.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.btnDictionaryChickens.Name = "btnDictionaryChickens";
            this.btnDictionaryChickens.Size = new System.Drawing.Size(180, 100);
            this.btnDictionaryChickens.TabIndex = 0;
            this.btnDictionaryChickens.Text = "Куры";
            this.btnDictionaryChickens.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnDictionaryChickens.UseVisualStyleBackColor = false;
            this.btnDictionaryChickens.Click += new System.EventHandler(this.btnDictionaryChickens_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.WhiteSmoke;
            this.ClientSize = new System.Drawing.Size(984, 561);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.btnDictionaryAssignments);
            this.Controls.Add(this.btnDictionaryShops);
            this.Controls.Add(this.btnDictionaryDiets);
            this.Controls.Add(this.btnDictionaryWorkers);
            this.Controls.Add(this.btnDictionaryCages);
            this.Controls.Add(this.btnDictionaryBreeds);
            this.Controls.Add(this.btnDictionaryChickens);
            this.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.MaximizeBox = false;
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Главное меню";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnDictionaryChickens;
        private System.Windows.Forms.Button btnDictionaryBreeds;
        private System.Windows.Forms.Button btnDictionaryCages;
        private System.Windows.Forms.Button btnDictionaryWorkers;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnDictionaryDiets;
        private System.Windows.Forms.Button btnDictionaryShops;
        private System.Windows.Forms.Button btnDictionaryAssignments;
        private System.Windows.Forms.Button btnDirectorPanel;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Label lblTime;
        private System.Windows.Forms.Label lblDate;
        private System.Windows.Forms.Label lblGreeting;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.PictureBox pictureBox3;
        private System.Windows.Forms.Label label4;
    }
}