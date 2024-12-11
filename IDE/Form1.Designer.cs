namespace IDE
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            menuStrip1 = new MenuStrip();
            файлToolStripMenuItem = new ToolStripMenuItem();
            запускToolStripMenuItem = new ToolStripMenuItem();
            оПрограммеToolStripMenuItem = new ToolStripMenuItem();
            richTextBox1 = new RichTextBox();
            запускToolStripMenuItem1 = new ToolStripMenuItem();
            компиляцияToolStripMenuItem = new ToolStripMenuItem();
            menuStrip1.SuspendLayout();
            SuspendLayout();
            // 
            // menuStrip1
            // 
            menuStrip1.ImageScalingSize = new Size(20, 20);
            menuStrip1.Items.AddRange(new ToolStripItem[] { файлToolStripMenuItem, запускToolStripMenuItem, оПрограммеToolStripMenuItem });
            menuStrip1.Location = new Point(0, 0);
            menuStrip1.Name = "menuStrip1";
            menuStrip1.Size = new Size(1154, 28);
            menuStrip1.TabIndex = 0;
            menuStrip1.Text = "menuStrip1";
            // 
            // файлToolStripMenuItem
            // 
            файлToolStripMenuItem.Name = "файлToolStripMenuItem";
            файлToolStripMenuItem.Size = new Size(60, 24);
            файлToolStripMenuItem.Text = "Файл";
            // 
            // запускToolStripMenuItem
            // 
            запускToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { запускToolStripMenuItem1, компиляцияToolStripMenuItem });
            запускToolStripMenuItem.Name = "запускToolStripMenuItem";
            запускToolStripMenuItem.Size = new Size(71, 24);
            запускToolStripMenuItem.Text = "Запуск";
            запускToolStripMenuItem.Click += запускToolStripMenuItem_Click;
            // 
            // оПрограммеToolStripMenuItem
            // 
            оПрограммеToolStripMenuItem.Name = "оПрограммеToolStripMenuItem";
            оПрограммеToolStripMenuItem.Size = new Size(115, 24);
            оПрограммеToolStripMenuItem.Text = "О программе";
            оПрограммеToolStripMenuItem.Click += оПрограммеToolStripMenuItem_Click;
            // 
            // richTextBox1
            // 
            richTextBox1.Dock = DockStyle.Fill;
            richTextBox1.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold | FontStyle.Italic, GraphicsUnit.Point, 204);
            richTextBox1.Location = new Point(0, 28);
            richTextBox1.Name = "richTextBox1";
            richTextBox1.Size = new Size(1154, 622);
            richTextBox1.TabIndex = 1;
            richTextBox1.Text = "";
            // 
            // запускToolStripMenuItem1
            // 
            запускToolStripMenuItem1.Name = "запускToolStripMenuItem1";
            запускToolStripMenuItem1.Size = new Size(224, 26);
            запускToolStripMenuItem1.Text = "Запуск";
            // 
            // компиляцияToolStripMenuItem
            // 
            компиляцияToolStripMenuItem.Name = "компиляцияToolStripMenuItem";
            компиляцияToolStripMenuItem.Size = new Size(224, 26);
            компиляцияToolStripMenuItem.Text = "Компиляция";
            компиляцияToolStripMenuItem.Click += компиляцияToolStripMenuItem_Click;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(9F, 18F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1154, 650);
            Controls.Add(richTextBox1);
            Controls.Add(menuStrip1);
            MainMenuStrip = menuStrip1;
            Name = "Form1";
            Text = "Form1";
            menuStrip1.ResumeLayout(false);
            menuStrip1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private MenuStrip menuStrip1;
        private ToolStripMenuItem файлToolStripMenuItem;
        private ToolStripMenuItem запускToolStripMenuItem;
        private ToolStripMenuItem оПрограммеToolStripMenuItem;
        private RichTextBox richTextBox1;
        private ToolStripMenuItem запускToolStripMenuItem1;
        private ToolStripMenuItem компиляцияToolStripMenuItem;
    }
}
