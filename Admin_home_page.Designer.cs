namespace Shop_management_system
{
    partial class Admin_home_page
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Admin_home_page));
            label1 = new Label();
            pictureBox4 = new PictureBox();
            toolStrip1 = new ToolStrip();
            toolStripLabel1 = new ToolStripLabel();
            toolStripButton1 = new ToolStripButton();
            toolStripLabel2 = new ToolStripLabel();
            toolStripButton2 = new ToolStripButton();
            toolStripLabel3 = new ToolStripLabel();
            toolStripButton3 = new ToolStripButton();
            ((System.ComponentModel.ISupportInitialize)pictureBox4).BeginInit();
            toolStrip1.SuspendLayout();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 19.8000011F, FontStyle.Bold | FontStyle.Italic, GraphicsUnit.Point, 0);
            label1.Location = new Point(37, 36);
            label1.Name = "label1";
            label1.Size = new Size(115, 46);
            label1.TabIndex = 1;
            label1.Text = "Home";
            // 
            // pictureBox4
            // 
            pictureBox4.Image = (Image)resources.GetObject("pictureBox4.Image");
            pictureBox4.Location = new Point(1866, 5);
            pictureBox4.Name = "pictureBox4";
            pictureBox4.Size = new Size(53, 50);
            pictureBox4.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox4.TabIndex = 3;
            pictureBox4.TabStop = false;
            pictureBox4.Click += pictureBox4_Click;
            // 
            // toolStrip1
            // 
            toolStrip1.AutoSize = false;
            toolStrip1.BackColor = SystemColors.ActiveCaptionText;
            toolStrip1.Dock = DockStyle.None;
            toolStrip1.ImageScalingSize = new Size(20, 20);
            toolStrip1.Items.AddRange(new ToolStripItem[] { toolStripLabel1, toolStripButton1, toolStripLabel2, toolStripButton2, toolStripLabel3, toolStripButton3 });
            toolStrip1.LayoutStyle = ToolStripLayoutStyle.Flow;
            toolStrip1.Location = new Point(9, 218);
            toolStrip1.MaximumSize = new Size(300, 570);
            toolStrip1.Name = "toolStrip1";
            toolStrip1.Size = new Size(175, 561);
            toolStrip1.TabIndex = 4;
            toolStrip1.Text = "toolStrip1";
            // 
            // toolStripLabel1
            // 
            toolStripLabel1.AutoSize = false;
            toolStripLabel1.BackgroundImage = (Image)resources.GetObject("toolStripLabel1.BackgroundImage");
            toolStripLabel1.BackgroundImageLayout = ImageLayout.Stretch;
            toolStripLabel1.Margin = new Padding(20, 10, 0, 2);
            toolStripLabel1.Name = "toolStripLabel1";
            toolStripLabel1.Size = new Size(130, 130);
            toolStripLabel1.Click += toolStripLabel1_Click;
            // 
            // toolStripButton1
            // 
            toolStripButton1.AutoSize = false;
            toolStripButton1.BackColor = Color.LightCyan;
            toolStripButton1.BackgroundImageLayout = ImageLayout.Stretch;
            toolStripButton1.Font = new Font("Segoe UI Semibold", 10.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            toolStripButton1.ImageTransparentColor = Color.Magenta;
            toolStripButton1.Margin = new Padding(10, 10, 0, 10);
            toolStripButton1.Name = "toolStripButton1";
            toolStripButton1.Size = new Size(150, 30);
            toolStripButton1.Text = "Staffs";
            toolStripButton1.Click += toolStripButton1_Click;
            // 
            // toolStripLabel2
            // 
            toolStripLabel2.AutoSize = false;
            toolStripLabel2.BackgroundImage = (Image)resources.GetObject("toolStripLabel2.BackgroundImage");
            toolStripLabel2.BackgroundImageLayout = ImageLayout.Stretch;
            toolStripLabel2.Margin = new Padding(20, 1, 0, 2);
            toolStripLabel2.Name = "toolStripLabel2";
            toolStripLabel2.Size = new Size(130, 130);
            toolStripLabel2.Click += toolStripLabel2_Click;
            // 
            // toolStripButton2
            // 
            toolStripButton2.AutoSize = false;
            toolStripButton2.BackColor = Color.LightCyan;
            toolStripButton2.Font = new Font("Segoe UI Semibold", 10.8F, FontStyle.Bold | FontStyle.Italic, GraphicsUnit.Point, 0);
            toolStripButton2.ImageTransparentColor = Color.Magenta;
            toolStripButton2.Margin = new Padding(10, 10, 0, 10);
            toolStripButton2.Name = "toolStripButton2";
            toolStripButton2.Size = new Size(150, 30);
            toolStripButton2.Text = "Products";
            toolStripButton2.Click += toolStripButton2_Click_1;
            // 
            // toolStripLabel3
            // 
            toolStripLabel3.AutoSize = false;
            toolStripLabel3.BackgroundImage = (Image)resources.GetObject("toolStripLabel3.BackgroundImage");
            toolStripLabel3.BackgroundImageLayout = ImageLayout.Stretch;
            toolStripLabel3.Margin = new Padding(20, 1, 0, 2);
            toolStripLabel3.Name = "toolStripLabel3";
            toolStripLabel3.Size = new Size(130, 130);
            toolStripLabel3.Click += toolStripLabel3_Click;
            // 
            // toolStripButton3
            // 
            toolStripButton3.AutoSize = false;
            toolStripButton3.BackColor = Color.LightCyan;
            toolStripButton3.Font = new Font("Segoe UI Semibold", 10.8F, FontStyle.Bold | FontStyle.Italic, GraphicsUnit.Point, 0);
            toolStripButton3.ImageTransparentColor = Color.Magenta;
            toolStripButton3.Margin = new Padding(10, 10, 0, 10);
            toolStripButton3.Name = "toolStripButton3";
            toolStripButton3.Size = new Size(150, 30);
            toolStripButton3.Text = "Sales";
            toolStripButton3.Click += toolStripButton3_Click;
            // 
            // Admin_home_page
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.LightCyan;
            ClientSize = new Size(1922, 805);
            Controls.Add(toolStrip1);
            Controls.Add(pictureBox4);
            Controls.Add(label1);
            FormBorderStyle = FormBorderStyle.None;
            Name = "Admin_home_page";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Admin_home_page";
            WindowState = FormWindowState.Maximized;
            ((System.ComponentModel.ISupportInitialize)pictureBox4).EndInit();
            toolStrip1.ResumeLayout(false);
            toolStrip1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private Label label1;
        private PictureBox pictureBox4;
        private ToolStrip toolStrip1;
        private ToolStripLabel toolStripLabel1;
        private ToolStripButton toolStripButton1;
        private ToolStripLabel toolStripLabel2;
        private ToolStripButton toolStripButton2;
        private ToolStripLabel toolStripLabel3;
        private ToolStripButton toolStripButton3;
    }
}