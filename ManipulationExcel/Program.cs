using ModelShare.Entities;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.IO;
using System.Linq;

namespace ManipulationExcel
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Digite o caminho que se encontra o arquivo .xmls: ");
            string pathFile = Console.ReadLine().Trim();
            
            var value = Value(pathFile);
            var list = value.Item1 as List<string>;
            var rotas = value.Item4 as string[,];
            if (list != null)
            {
                foreach (var dados in list)
                {
                    Console.Write("{0}  ", dados);
                }
                Console.WriteLine();
                Console.WriteLine();
                Console.WriteLine();

                for (int i = 1; i < value.Item2; i++)
                {
                    for (int j = 0; j < value.Item3; j++)
                    {
                        Console.WriteLine(rotas[i, j]);
                    }
                }
            }


            
        }

        static (List<string>, int, int, string[,]) Value(string caminhoArquivo)
        {
            string[,] rotas;
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial; 
            var arquivoExcel = new ExcelPackage(new FileInfo(caminhoArquivo));
            ExcelWorksheet rotasXLSX = arquivoExcel.Workbook.Worksheets.FirstOrDefault();

            int rowsOfFile = rotasXLSX.Dimension.Rows;
            int columnsOfFile = rotasXLSX.Dimension.Columns;
            List<string> list = new List<string>();
            rotas = new string[rowsOfFile, columnsOfFile];
            for (int rows = 1; rows < rowsOfFile; rows++)
            {
                for (int columns = 1; columns < columnsOfFile; columns++)
                {
                    
                    var conteudo = rotasXLSX.Cells[rows, columns].Value == null ? "" : rotasXLSX.Cells[rows, columns].Value;
                    if (conteudo == null && columns == columnsOfFile-1)
                        break;
                    
                    rotas[rows, columns] = conteudo.ToString();
                    list.Add(conteudo.ToString());
                    Console.WriteLine($"{conteudo}");
                }
            }
            return (list, rowsOfFile, columnsOfFile, rotas);
        }
    }
}



//C:\Users\LeonardoBorges\Downloads\Gerador de Rotas.xlsx