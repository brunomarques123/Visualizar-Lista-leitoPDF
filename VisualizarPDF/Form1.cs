using PdfiumViewer;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace VisualizarPDF
{
    public partial class Form1 : Form
    {
        PdfiumViewer.PdfViewer pdf;

        public Form1()
        {
            InitializeComponent();
            pdf = new PdfViewer();
            pdf.Width = this.Width - 20;
            pdf.Height = this.Height -40;

            //colocar no formulario os controles do PDF
            this.Controls.Add(pdf);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // abre a caixa onde esta os pdf
            OpenFileDialog dialog = new OpenFileDialog();
            if (dialog.ShowDialog()== DialogResult.OK)
            {
                //abre o caminho passado(nome)
                openfile(dialog.FileName);
            }

        }

        //metodo criado
        private void openfile(string filepath)
        {
            //lendo  e colocando em um array
            byte[] bytes = System.IO.File.ReadAllBytes(filepath);
            //declara um objto que vai ler e carregar na memoria
            var stream = new System.IO.MemoryStream(bytes);
            //declara o documento da biblioteca pdfWier carregando
            PdfDocument pdfDocument = PdfDocument.Load(stream);
            //
            pdf.Document = pdfDocument;
        }
    }
}
