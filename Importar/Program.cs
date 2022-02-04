
using iTextSharp.text.pdf;
using iTextSharp.text.pdf.parser;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Web;
using System.Web.UI.WebControls;

namespace Importar
{
    public partial class Program 
    {
       
        public class GUIAS_IMPORTAR
        {
            public string OrdemServico { get; set; }
            public string Data { get; set; }
            public decimal Valor { get; set; }
            
        }

        static void Importar(string arquivo)// inicio
        {


            try
            {
                //if (System.IO.Path.GetExtension(arquivo).ToLower() != ".pdf")
              
                //grdListaImporta.Visible = false;
                //grdListaImporta.DataSource = null;
                //grdListaImporta.DataBind();

                String caminho = Server.MapPath("~/uploads"); // Caminho do servidor

                if (!Directory.Exists(caminho))
                    Directory.CreateDirectory(caminho); // Se não existir precisa criar

                // Salvo o arquivo no diretório base, nesse caso "caminho" por causa da permissão
                String caminho_completo = caminho + "\\" + System.IO.Path.GetFileName(arquivo);
                fupArquivo.SaveAs(caminho_completo);

                // Libera o arquivo
                fupArquivo.Dispose();

                List<GUIAS_IMPORTAR> lista = new List<GUIAS_IMPORTAR>();

                string ordemServivo; string data ; decimal todal = 0; 

                servicos_importar.Clear(); // limpar lista

                using (PdfReader leitor = new PdfReader(caminho_completo))
                {
                    StringBuilder texto = new StringBuilder();

                    int linha = 0;
                    int cont = 0;
                    int quem = 0; // 1=Nome e Guia  2=Pago e Glosa
                   

                    GUIAS_IMPORTAR guia = new GUIAS_IMPORTAR();
                    //ATENDIMENTO atend = null;

                    for (int i = 1; i <= leitor.NumberOfPages; i++)
                    {
                        string thePage = PdfTextExtractor.GetTextFromPage(leitor, i); // carrega cada pagina do pdf
                        string[] theLines = thePage.Split('\n'); // carrega cada linha do pdf

                        linha = 0;
                        cont = 0;

                        foreach (var theLine in theLines) // corre as linhas
                        {
                            texto.AppendLine(theLine);

                            // Encontra a linha do número da carteira
                            if (theLine.ToString().ToLower().Contains("11 - número da carteira"))
                            {
                                linha = cont + 2; quem = 1;
                            }

                            // Importar serviços
                            else if (theLine.ToString().ToLower().Contains("15 - código do procedimento"))
                            {
                                linha = cont + 1; quem = 4;
                            }

                            // A próxima linha após pegar o primeiro serviço sempre deve ser a 27 de obs, se não for é porque temos outros serviços
                            else if (quem == 5 && !theLine.ToLower().Contains("27 - observação / justificativa") && theLine != "")
                            {
                                linha = cont; quem = 4;
                            }
                        }
                        linha++;
                    }
                    cont++;
                }
               
            }
            
        }
    }
}

