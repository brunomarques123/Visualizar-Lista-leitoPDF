using iText.Kernel.Pdf;
using iText.Kernel.Pdf.Canvas.Parser;
using iText.Kernel.Pdf.Canvas.Parser.Listener;
using System.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace LeitorPdf
{
    class Program
    {
        static void Main(string[] args)
        {
            var _lista = ExtrairTextoPdf();

            foreach (var ordem in _lista)
            {
                Console.WriteLine("ordem: " + ordem.ordem + "\n" + "data: " + ordem.data + "\n" + "valor: " + ordem.valor);
            }
        }


        static List<Ordemservico> ExtrairTextoPdf()
        {
            // buscar no banco de dados o base64/pdf de um cliente especifico
            // transformar em pdf
            // salvar na maquina

            // loop nos arquivos

            var _path = @"C:\Users\welli\Desktop\mecanica\PDF\";
            DirectoryInfo diretorio = new DirectoryInfo(_path);
            FileInfo[] Arquivos = diretorio.GetFiles("*.*");

            List<Ordemservico> _lista = new List<Ordemservico>();

            foreach (FileInfo fileinfo in Arquivos)
            {
                var _file = fileinfo.Name;

                string result = null;
                PdfReader pdfReader = new PdfReader($"{_path}{_file}");

                PdfDocument pdfDoc = new PdfDocument(pdfReader);

                for (int page = 1; page <= pdfDoc.GetNumberOfPages(); page++)
                {
                    ITextExtractionStrategy strategy = new SimpleTextExtractionStrategy();
                    string conteudo = PdfTextExtractor.GetTextFromPage(pdfDoc.GetPage(page), strategy);
                    result += conteudo;
                }

                var _index = result.IndexOf("ORDEM DE SERVIÇO:");

                Ordemservico ordemservico = new Ordemservico();

                if (_index > 0)
                    ordemservico.ordem = result.Substring(result.IndexOf("ORDEM DE SERVIÇO:") + 17, 5).Trim();
                else
                    ordemservico.ordem = result.Substring(result.IndexOf("PEDIDO:") + 8, 5).Trim();

                ordemservico.data = result.Substring(result.IndexOf("Data / Hora Entrada:") + 20, 11).Trim();
                ordemservico.valor = result.Substring(result.IndexOf("Valor Total =>") + 14, 9).Replace("V", "").Trim();

                _lista.Add(ordemservico);

                pdfDoc.Close();
                pdfReader.Close();
            }

            return _lista;
        }
    }

    public class Ordemservico
    { 
        public string ordem { get; set; }
        public string data { get; set; }
        public string valor { get; set; }
    }
}
