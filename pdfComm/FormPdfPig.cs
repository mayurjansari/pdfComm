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
using UglyToad.PdfPig;
using UglyToad.PdfPig.Content;
//using UglyToad.PdfPig.DocumentLayoutAnalysis;

namespace pdfComm
{
    public partial class FormPdfPig : Form
    {
        public FormPdfPig()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog
            {
                InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop),
                Title = "Browse Text Files",

                CheckFileExists = true,
                CheckPathExists = true,

                DefaultExt = "pdf",
                Filter = "pdf files (*.pdf)|*.pdf",
                FilterIndex = 2,
                RestoreDirectory = true,

                ReadOnlyChecked = true,
                ShowReadOnly = true
            };

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                txtFilePath.Text = openFileDialog1.FileName;
                string fileName = Path.GetFileNameWithoutExtension(txtFilePath.Text);
                string FilenameExt = openFileDialog1.SafeFileName;
                if (!string.IsNullOrEmpty(fileName))
                {
                    txtPassword.Text = getPassword(fileName);
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                string abc = pdfbox.PDFText(txtFilePath.Text);
                textBox3.Text = "text from pdfbox \r\n" + abc + "\r\n";
            }
            catch { }

            using (PdfDocument doc = PdfDocument.Open(txtFilePath.Text, new ParsingOptions
            {
                Password = txtPassword.Text
                //, Text = new TextOptions { IncludeNewlines = true, Newline=" " }
            }))
            {
                Page page = doc.GetPage(1);
                string text = page.Text;
                var words = page.GetWords();
                //string xyCutText = string.Empty;
                //var cuts = RecursiveXYCut.GetBlocks(words, page.Width / 5m, k => Math.Round(k.Mode(), 3), k => Math.Round(k.Mode() * 1.5m, 3));
                //var leefs = cuts.GetLeefs();
                //foreach (var leef in leefs)
                //{
                //    var lines = leef.GetLines();
                //    foreach (var line in lines)
                //        xyCutText += line.Text + "    ";
                //}
                textBox3.Text += "text from pdfpig \r\n" + text + "\r\n text using getwords \r\n" + string.Join(" ", words.Select(x => x.Text)); //+ "\r\n\r\n" + xyCutText;
                //var images = page.ExperimentalAccess.GetRawImages();
            }
        }

        string getPassword(string filename)
        {
            if (filename.Length >= 6)
            {
                string pwd = filename.Split(' ', '-', '_').LastOrDefault();
                return pwd.Length < 10 ? pwd : "";
                //txtPassword.Text = pwd;
            }
            else return "";
        }
    }
}
