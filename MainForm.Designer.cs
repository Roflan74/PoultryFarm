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
            this.btnDictionaryChickens = new System.Windows.Forms.Button();
            this.btnDictionaryBreeds = new System.Windows.Forms.Button();
            this.btnDictionaryCages = new System.Windows.Forms.Button();
            this.btnDictionaryWorkers = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label1.Location = new System.Drawing.Point(230, 60);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(400, 46);
            this.label1.TabIndex = 4;
            this.label1.Text = "АСУ Птицефабрика";
            // 
            // btnDictionaryChickens
            // 
            this.btnDictionaryChickens.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btnDictionaryChickens.Location = new System.Drawing.Point(40, 200);
            this.btnDictionaryChickens.Name = "btnDictionaryChickens";
            this.btnDictionaryChickens.Size = new System.Drawing.Size(150, 100);
            this.btnDictionaryChickens.TabIndex = 0;
            this.btnDictionaryChickens.Text = "Куры";
            this.btnDictionaryChickens.UseVisualStyleBackColor = true;
            this.btnDictionaryChickens.Click += new System.EventHandler(this.btnDictionaryChickens_Click);
            // 
            // btnDictionaryBreeds
            // 
            this.btnDictionaryBreeds.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btnDictionaryBreeds.Location = new System.Drawing.Point(230, 200);
            this.btnDictionaryBreeds.Name = "btnDictionaryBreeds";
            this.btnDictionaryBreeds.Size = new System.Drawing.Size(150, 100);
            this.btnDictionaryBreeds.TabIndex = 1;
            this.btnDictionaryBreeds.Text = "Породы";
            this.btnDictionaryBreeds.UseVisualStyleBackColor = true;
            this.btnDictionaryBreeds.Click += new System.EventHandler(this.btnDictionaryBreeds_Click);
            // 
            // btnDictionaryCages
            // 
            this.btnDictionaryCages.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btnDictionaryCages.Location = new System.Drawing.Point(420, 200);
            this.btnDictionaryCages.Name = "btnDictionaryCages";
            this.btnDictionaryCages.Size = new System.Drawing.Size(150, 100);
            this.btnDictionaryCages.TabIndex = 2;
            this.btnDictionaryCages.Text = "Клетки";
            this.btnDictionaryCages.UseVisualStyleBackColor = true;
            this.btnDictionaryCages.Click += new System.EventHandler(this.btnDictionaryCages_Click);
            // 
            // btnDictionaryWorkers
            // 
            this.btnDictionaryWorkers.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btnDictionaryWorkers.Location = new System.Drawing.Point(610, 200);
            this.btnDictionaryWorkers.Name = "btnDictionaryWorkers";
            this.btnDictionaryWorkers.Size = new System.Drawing.Size(150, 100);
            this.btnDictionaryWorkers.TabIndex = 3;
            this.btnDictionaryWorkers.Text = "Работники";
            this.btnDictionaryWorkers.UseVisualStyleBackColor = true;
            this.btnDictionaryWorkers.Click += new System.EventHandler(this.btnDictionaryWorkers_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnDictionaryWorkers);
            this.Controls.Add(this.btnDictionaryCages);
            this.Controls.Add(this.btnDictionaryBreeds);
            this.Controls.Add(this.btnDictionaryChickens);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Главное меню";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnDictionaryChickens;
        private System.Windows.Forms.Button btnDictionaryBreeds;
        private System.Windows.Forms.Button btnDictionaryCages;
        private System.Windows.Forms.Button btnDictionaryWorkers;
        private System.Windows.Forms.Label label1;
    }
}