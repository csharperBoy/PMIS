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
            labelUsername.Location = new Point(15, 35);
            labelUsername.Name = "labelUsername";
            labelUsername.Size = new Size(72, 15);
            labelUsername.TabIndex = 0;
            labelUsername.Text = "گذرواژه فعلی";
            // 
            // textBoxNewPassword
            // 
            textBoxNewPassword.Location = new Point(110, 70);
            textBoxNewPassword.Name = "textBoxNewPassword";
            textBoxNewPassword.PasswordChar = '●';
            textBoxNewPassword.Size = new Size(100, 23);
            textBoxNewPassword.TabIndex = 2;
            // 
            // labelPassword
            // 
            labelPassword.AutoSize = true;
            labelPassword.Location = new Point(15, 75);
            labelPassword.Name = "labelPassword";
            labelPassword.Size = new Size(68, 15);
            labelPassword.TabIndex = 0;
            labelPassword.Text = "گذرواژه جدید";
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
            // textBoxReNewPassword
            // 
            textBoxReNewPassword.Location = new Point(110, 105);
            textBoxReNewPassword.Name = "textBoxReNewPassword";
            textBoxReNewPassword.PasswordChar = '●';
            textBoxReNewPassword.Size = new Size(100, 23);
            textBoxReNewPassword.TabIndex = 3;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(15, 110);
            label1.Name = "label1";
            label1.Size = new Size(93, 15);
            label1.TabIndex = 0;
            label1.Text = "تکرار گذرواژه جدید";
            // 
            // textBoxOldPassword
            // 
            textBoxOldPassword.Location = new Point(110, 30);
            textBoxOldPassword.Name = "textBoxOldPassword";
            textBoxOldPassword.PasswordChar = '●';
            textBoxOldPassword.Size = new Size(100, 23);
            textBoxOldPassword.TabIndex = 1;
            // 
            // ChangePasswordForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(270, 200);
            Controls.Add(textBoxOldPassword);
            Controls.Add(textBoxReNewPassword);
            Controls.Add(label1);
            Controls.Add(labelUsername);
            Controls.Add(textBoxNewPassword);
            Controls.Add(labelPassword);
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

        private Label labelUsername;
        private TextBox textBoxNewPassword;
        private Label labelPassword;
        private Button buttonSubmit;
        private TextBox textBoxReNewPassword;
        private Label label1;
        private TextBox textBoxOldPassword;
    }
}