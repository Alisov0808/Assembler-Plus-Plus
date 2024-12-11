using System.Diagnostics;

namespace IDE
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        string filename = "";
            private void îÏğîãğàììåToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void çàïóñêToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void êîìïèëÿöèÿToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Process process = new Process();
            process.StartInfo.FileName = "Lumin.exe";
            process.StartInfo.UseShellExecute = false;
            process.StartInfo.RedirectStandardOutput = true;
            process.StartInfo.RedirectStandardError = true;
            process.StartInfo.Arguments = ;

            process.Start();
            string errorOutput = process.StandardError.ReadToEnd();
            process.WaitForExit();
        }
    }
}
