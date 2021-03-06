using ModelShare.Entities;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.IO;
using System.Linq;

//C:\Users\LeonardoBorges\Downloads\Gerador de Rotas.xlsx

namespace ManipulationExcel
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Digite o caminho que se encontra o arquivo .xmls: ");
            string pathFile = Console.ReadLine().Trim();
            
            var value = Value(pathFile);
            
        }

        static object Value(string caminhoArquivo)
        {
            //abrir e receber excel na aplicacao
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial; 
            var arquivoExcel = new ExcelPackage(new FileInfo(caminhoArquivo));
            ExcelWorksheet rotasXLSX = arquivoExcel.Workbook.Worksheets.FirstOrDefault();

            //receber linhas e colunas
            int rowsOfFile = rotasXLSX.Dimension.Rows;
            int columnsOfFile = rotasXLSX.Dimension.Columns;

            //criando instancias de listas e dicionarios
            List<string> list = new List<string>();
            IDictionary<string, List<string>> routes = new Dictionary<string, List<string>>();


            //referencia cep
            int columnCep = 0;


            for (int rows = 1; rows < rowsOfFile; rows++)
            {
                list = new List<string>();
                for (int columns = 1; columns < columnsOfFile; columns++)
                {
                    //recebe conteudo da linhado excel
                    var conteudo = rotasXLSX.Cells[rows, columns].Value == null ? "" : rotasXLSX.Cells[rows, columns].Value;
                    if (conteudo == null && columns == columnsOfFile-1)
                        break;

                    if (conteudo.ToString().ToUpper() == "CEP")
                        columnCep = columns;
                    list.Add(conteudo.ToString());
                    //Console.WriteLine($"{conteudo}");
                }
                if (rotasXLSX.Cells[rows, columnCep].Value == null || rotasXLSX.Cells[rows, columnCep].Value.ToString() == "") 
                    break;

                routes.Add(rotasXLSX.Cells[rows, columnCep].Value.ToString(), list);


            }
            var value = routes.OrderBy(x => x.Key).ToDictionary(x => x.Key, x => x.Value);

            return value;
        }
    }
}

