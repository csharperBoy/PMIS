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
            labelUsername = new Label();
            textBoxNewPassword = new TextBox();
            labelPassword = new Label();
            buttonSubmit = new Button();
            textBoxReNewPassword = new TextBox();
            label1 = new Label();
            textBoxOldPassword = new TextBox();
            SuspendLayout();
            // 
            // labelUsername
            // 
            labelUsername.AutoSize = true;
            labelUsername.Location = new Point(300, 51);
            labelUsername.Name = "labelUsername";
            labelUsername.Size = new Size(110, 20);
            labelUsername.TabIndex = 7;
            labelUsername.Text = "کلمه عبور فعلی";
            labelUsername.Click += labelUsername_Click;
            // 
            // textBoxNewPassword
            // 
            textBoxNewPassword.Location = new Point(175, 98);
            textBoxNewPassword.Margin = new Padding(3, 4, 3, 4);
            textBoxNewPassword.Name = "textBoxNewPassword";
            textBoxNewPassword.PasswordChar = '●';
            textBoxNewPassword.Size = new Size(114, 27);
            textBoxNewPassword.TabIndex = 2;
            // 
            // labelPassword
            // 
            labelPassword.AutoSize = true;
            labelPassword.Location = new Point(300, 101);
            labelPassword.Name = "labelPassword";
            labelPassword.Size = new Size(105, 20);
            labelPassword.TabIndex = 8;
            labelPassword.Text = "کلمه عبور جدید";
            // 
            // buttonSubmit
            // 
            buttonSubmit.Location = new Point(190, 210);
            buttonSubmit.Margin = new Padding(3, 4, 3, 4);
            buttonSubmit.Name = "buttonSubmit";
            buttonSubmit.Size = new Size(86, 33);
            buttonSubmit.TabIndex = 9;
            buttonSubmit.Text = "اعمال";
            buttonSubmit.UseVisualStyleBackColor = true;
            buttonSubmit.Click += buttonSubmit_Click;
            // 
            // textBoxReNewPassword
            // 
            textBoxReNewPassword.Location = new Point(175, 151);
            textBoxReNewPassword.Margin = new Padding(3, 4, 3, 4);
            textBoxReNewPassword.Name = "textBoxReNewPassword";
            textBoxReNewPassword.PasswordChar = '●';
            textBoxReNewPassword.Size = new Size(114, 27);
            textBoxReNewPassword.TabIndex = 3;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(300, 154);
            label1.Name = "label1";
            label1.Size = new Size(137, 20);
            label1.TabIndex = 11;
            label1.Text = "تکرار کلمه عبور جدید";
            // 
            // textBoxOldPassword
            // 
            textBoxOldPassword.Location = new Point(175, 51);
            textBoxOldPassword.Margin = new Padding(3, 4, 3, 4);
            textBoxOldPassword.Name = "textBoxOldPassword";
            textBoxOldPassword.PasswordChar = '●';
            textBoxOldPassword.Size = new Size(114, 27);
            textBoxOldPassword.TabIndex = 1;
            // 
            // ChangePasswordForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(592, 323);
            Controls.Add(textBoxOldPassword);
            Controls.Add(textBoxReNewPassword);
            Controls.Add(label1);
            Controls.Add(labelUsername);
            Controls.Add(textBoxNewPassword);
            Controls.Add(labelPassword);
            Controls.Add(buttonSubmit);
            Name = "ChangePasswordForm";
            Text = "تغییر رمز عبور";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label labelUsername;
        private TextBox textBoxNewPassword;
        private Label labelPassword;
        private Button buttonSubmit;
        private TextBox textBoxReNewPassword;
        private Label label1;
        private TextBox textBoxOldPassword;
    }
}