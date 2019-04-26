using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RenamingTool
{
    public partial class Form1 : Form
    {
        private List<string> files;
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            files = new List<string>();
        }

        private void Browse_Click(object sender, EventArgs e)
        {
            if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
            {
                path.Text = folderBrowserDialog1.SelectedPath;
            }
        }

        private void Apply_Click(object sender, EventArgs e)
        {
            var files = Directory.GetFiles(path.Text);
            foreach (DataGridViewRow row in grid.Rows)
            {
                string toReplace = (string)row.Cells[0].Value;
                string replaceWith = (string)row.Cells[1].Value;

                if (string.IsNullOrEmpty(toReplace)) continue;
                if (string.IsNullOrEmpty(replaceWith)) replaceWith = string.Empty;


                for (int i = 0; i < files.Length; i++)
                {
                    FileInfo info = new FileInfo(files[i]);
                    var oldPath = info.FullName;
                    var newPath = Path.Combine(info.DirectoryName, info.Name.Replace(toReplace, replaceWith));
                    File.Move(oldPath, newPath);
                    files[i] = newPath;
                }
            }
        }
    }
}
