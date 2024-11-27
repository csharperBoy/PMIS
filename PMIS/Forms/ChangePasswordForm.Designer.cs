namespace PMIS.Forms
{
    partial class ChangePasswordForm
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
            labelCurrentPassword = new Label();
            textBoxNewPassword = new TextBox();
            labelNewPassword = new Label();
            buttonSubmit = new Button();
            textBoxRepeatNewPassword = new TextBox();
            labelRepeatNewPassword = new Label();
            textBoxCurrentPassword = new TextBox();
            SuspendLayout();
            // 
            // labelCurrentPassword
            // 
            labelCurrentPassword.AutoSize = true;
            labelCurrentPassword.Location = new Point(15, 35);
            labelCurrentPassword.Name = "labelCurrentPassword";
            labelCurrentPassword.Size = new Size(72, 15);
            labelCurrentPassword.TabIndex = 0;
            labelCurrentPassword.Text = "گذرواژه فعلی";
            // 
            // textBoxNewPassword
            // 
            textBoxNewPassword.Location = new Point(110, 70);
            textBoxNewPassword.Name = "textBoxNewPassword";
            textBoxNewPassword.PasswordChar = '●';
            textBoxNewPassword.Size = new Size(100, 23);
            textBoxNewPassword.TabIndex = 2;
            // 
            // labelNewPassword
            // 
            labelNewPassword.AutoSize = true;
            labelNewPassword.Location = new Point(15, 75);
            labelNewPassword.Name = "labelNewPassword";
            labelNewPassword.Size = new Size(68, 15);
            labelNewPassword.TabIndex = 0;
            labelNewPassword.Text = "گذرواژه جدید";
            // 
            // buttonSubmit
            // 
            buttonSubmit.Location = new Point(120, 150);
            buttonSubmit.Name = "buttonSubmit";
            buttonSubmit.Size = new Size(75, 25);
            buttonSubmit.TabIndex = 4;
            buttonSubmit.Text = "اعمال";
            buttonSubmit.UseVisualStyleBackColor = true;
            buttonSubmit.Click += buttonSubmit_Click;
            // 
            // textBoxRepeatNewPassword
            // 
            textBoxRepeatNewPassword.Location = new Point(110, 105);
            textBoxRepeatNewPassword.Name = "textBoxRepeatNewPassword";
            textBoxRepeatNewPassword.PasswordChar = '●';
            textBoxRepeatNewPassword.Size = new Size(100, 23);
            textBoxRepeatNewPassword.TabIndex = 3;
            // 
            // labelRepeatNewPassword
            // 
            labelRepeatNewPassword.AutoSize = true;
            labelRepeatNewPassword.Location = new Point(15, 110);
            labelRepeatNewPassword.Name = "labelRepeatNewPassword";
            labelRepeatNewPassword.Size = new Size(93, 15);
            labelRepeatNewPassword.TabIndex = 0;
            labelRepeatNewPassword.Text = "تکرار گذرواژه جدید";
            // 
            // textBoxCurrentPassword
            // 
            textBoxCurrentPassword.Location = new Point(110, 30);
            textBoxCurrentPassword.Name = "textBoxCurrentPassword";
            textBoxCurrentPassword.PasswordChar = '●';
            textBoxCurrentPassword.Size = new Size(100, 23);
            textBoxCurrentPassword.TabIndex = 1;
            // 
            // ChangePasswordForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(270, 200);
            Controls.Add(textBoxCurrentPassword);
            Controls.Add(textBoxRepeatNewPassword);
            Controls.Add(labelRepeatNewPassword);
            Controls.Add(labelCurrentPassword);
            Controls.Add(textBoxNewPassword);
            Controls.Add(labelNewPassword);
            Controls.Add(buttonSubmit);
            Margin = new Padding(3, 2, 3, 2);
            Name = "ChangePasswordForm";
            RightToLeft = RightToLeft.Yes;
            RightToLeftLayout = true;
            Text = "تغییر گذرواژه";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label labelCurrentPassword;
        private TextBox textBoxCurrentPassword;
        private Label labelNewPassword;
        private TextBox textBoxNewPassword;
        private Label labelRepeatNewPassword;
        private TextBox textBoxRepeatNewPassword;
        private Button buttonSubmit;
    }
}