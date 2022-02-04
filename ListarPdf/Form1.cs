
using PdfiumViewer;
using System;
using System.Data;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace ListarPdf
{


    public partial class Form1 : Form
    {
        PdfiumViewer.PdfViewer pdf;

        string rota = @"C:\Users\welli\Desktop\mecanica\PDF";

        public Form1()
        {
            InitializeComponent();
            pdf = new PdfViewer();
            pdf.ZoomMode = PdfViewerZoomMode.FitWidth;

            //pdf.Width = this.Width + 10;
            //pdf.Height = this.Height + 20;
            //this.Controls.Add(pdf);

            //ABRE O VISUALIZADOR
            pnlVisualizador.Controls.Add(pdf);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //using (var openDlg = new FolderBrowserDialog())
            //{
            //    openDlg.SelectedPath = rota;

            //    if (openDlg.ShowDialog() == DialogResult.OK)
            //       //caminho da pasta 
            //        rota = openDlg.SelectedPath;

            //}
            //limpando o listbox
            listBox1.Items.Clear();
            //chamando o metodo
            GetDir(rota);
            GetFiles(rota);
        }

        private void GetDir(string rota)
        {
            var diretorios = Directory.GetDirectories(rota);

            foreach (string dir in diretorios)
                listBox1.Items.Add(dir + " <dir>");
        }

        private void GetFiles(string rota)
        {
            var arquivos = Directory.GetFiles(rota);

            //var arquivo = new FileInfo(listBox1.SelectedItem.ToString());
            //var nomeArquivo = arquivo;

            //PEGA OS ARQUIVOS JA ENCONTRADOS NA LISTA ARQUIVO E FAZ A BUSCA POR PARTE DESSE NOME NO FORECH ABAIXO
            var arquivosFiltrados = arquivos.Where((x) => x.ToUpper().Contains(textBox2.Text.ToUpper()));


            foreach (string arq in arquivosFiltrados)
                listBox1.Items.Add(arq);
        }


        private void listBox1_DoubleClick(object sender, EventArgs e)
        {
            Console.WriteLine(listBox1.SelectedItem);

            openfile(listBox1.SelectedItem.ToString());
        }

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
            //pdfDocument.CreatePrintDocument()
        }

    }
}
