
namespace SysProg
{
    partial class ResourceInputForm
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
            this.resourceAddressBox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.resourceModeBox = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.resourceDatePicker = new System.Windows.Forms.DateTimePicker();
            this.submitButton = new System.Windows.Forms.Button();
            this.errorLabel = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // resourceAddressBox
            // 
            this.resourceAddressBox.Location = new System.Drawing.Point(12, 24);
            this.resourceAddressBox.Name = "resourceAddressBox";
            this.resourceAddressBox.Size = new System.Drawing.Size(360, 20);
            this.resourceAddressBox.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 8);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(82, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Адрес ресурса";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 47);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(85, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Режим доступа";
            // 
            // resourceModeBox
            // 
            this.resourceModeBox.Location = new System.Drawing.Point(12, 63);
            this.resourceModeBox.Name = "resourceModeBox";
            this.resourceModeBox.Size = new System.Drawing.Size(360, 20);
            this.resourceModeBox.TabIndex = 2;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 86);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(76, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "Дата доступа";
            // 
            // resourceDatePicker
            // 
            this.resourceDatePicker.Location = new System.Drawing.Point(12, 102);
            this.resourceDatePicker.Name = "resourceDatePicker";
            this.resourceDatePicker.Size = new System.Drawing.Size(200, 20);
            this.resourceDatePicker.TabIndex = 5;
            // 
            // submitButton
            // 
            this.submitButton.Location = new System.Drawing.Point(12, 128);
            this.submitButton.Name = "submitButton";
            this.submitButton.Size = new System.Drawing.Size(360, 23);
            this.submitButton.TabIndex = 6;
            this.submitButton.Text = "Добавить";
            this.submitButton.UseVisualStyleBackColor = true;
            // 
            // errorLabel
            // 
            this.errorLabel.Location = new System.Drawing.Point(12, 154);
            this.errorLabel.Name = "errorLabel";
            this.errorLabel.Size = new System.Drawing.Size(360, 24);
            this.errorLabel.TabIndex = 7;
            this.errorLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // ResourceInputForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(384, 187);
            this.Controls.Add(this.errorLabel);
            this.Controls.Add(this.submitButton);
            this.Controls.Add(this.resourceDatePicker);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.resourceModeBox);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.resourceAddressBox);
            this.Name = "ResourceInputForm";
            this.Text = "Добавить файл";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox resourceAddressBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox resourceModeBox;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.DateTimePicker resourceDatePicker;
        private System.Windows.Forms.Button submitButton;
        private System.Windows.Forms.Label errorLabel;
    }
}