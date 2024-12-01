namespace PMIS.Forms
{
    partial class LoginForm
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
            textBoxUsername = new TextBox();
            textBoxPassword = new TextBox();
            labelUsername = new Label();
            labelPassword = new Label();
            buttonEntry = new Button();
            SuspendLayout();
            // 
            // textBoxUsername
            // 
            textBoxUsername.Location = new Point(110, 40);
            textBoxUsername.Name = "textBoxUsername";
            textBoxUsername.Size = new Size(100, 23);
            textBoxUsername.TabIndex = 1;
            textBoxUsername.KeyPress += textBoxEntry_KeyPress;
            // 
            // textBoxPassword
            // 
            textBoxPassword.Location = new Point(110, 95);
            textBoxPassword.Name = "textBoxPassword";
            textBoxPassword.PasswordChar = '●';
            textBoxPassword.Size = new Size(100, 23);
            textBoxPassword.TabIndex = 2;
            textBoxPassword.KeyPress += textBoxEntry_KeyPress;
            // 
            // labelUsername
            // 
            labelUsername.AutoSize = true;
            labelUsername.Location = new Point(50, 45);
            labelUsername.Name = "labelUsername";
            labelUsername.Size = new Size(55, 15);
            labelUsername.TabIndex = 0;
            labelUsername.Text = "نام کاربری";
            // 
            // labelPassword
            // 
            labelPassword.AutoSize = true;
            labelPassword.Location = new Point(50, 100);
            labelPassword.Name = "labelPassword";
            labelPassword.Size = new Size(42, 15);
            labelPassword.TabIndex = 0;
            labelPassword.Text = "گذرواژه";
            // 
            // buttonEntry
            // 
            buttonEntry.Location = new Point(120, 150);
            buttonEntry.Name = "buttonEntry";
            buttonEntry.Size = new Size(75, 25);
            buttonEntry.TabIndex = 3;
            buttonEntry.Text = "ورود";
            buttonEntry.UseVisualStyleBackColor = true;
            buttonEntry.Click += buttonEntry_Click;
            // 
            // LoginForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(270, 200);
            Controls.Add(labelUsername);
            Controls.Add(textBoxPassword);
            Controls.Add(labelPassword);
            Controls.Add(textBoxUsername);
            Controls.Add(buttonEntry);
            Margin = new Padding(1);
            Name = "LoginForm";
            RightToLeft = RightToLeft.Yes;
            RightToLeftLayout = true;
            Text = "ایستگاه ورودی";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label labelUsername;
        private TextBox textBoxUsername;
        private Label labelPassword;
        private TextBox textBoxPassword;
        private Button buttonEntry;
    }
}