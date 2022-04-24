using Microsoft.AspNetCore.Mvc;
using OfficeOpenXml;
using System.Collections.Generic;
using System.Linq;
namespace FileUpload.Controllers
{
    public class HomeController : Controller
    {
        // This action renders the form
        public ActionResult Upload()
        {
            return View();
        }

        // This action handles the form POST and the upload
       
        public ActionResult UploadFile()
        {
            var files = HttpContext.Request.Form.Files;

            if(files.Count > 0)
            {
                ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
                using ExcelPackage package = new ExcelPackage(files[0].OpenReadStream());
                ExcelWorksheet worksheet = package.Workbook.Worksheets.FirstOrDefault();


                //receber linhas e colunas
                int rowsOfFile = worksheet.Dimension.Rows;
                int columnsOfFile = worksheet.Dimension.Columns;

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
                        var conteudo = worksheet.Cells[rows, columns].Value == null ? "" : worksheet.Cells[rows, columns].Value;
                        if (conteudo == null && columns == columnsOfFile - 1)
                            break;

                        if (conteudo.ToString().ToUpper() == "CEP")
                            columnCep = columns;
                        list.Add(conteudo.ToString());
                        //Console.WriteLine($"{conteudo}");
                    }
                    if (worksheet.Cells[rows, columnCep].Value == null || worksheet.Cells[rows, columnCep].Value.ToString() == "")
                        break;

                    routes.Add(worksheet.Cells[rows, columnCep].Value.ToString(), list);


                }
                var value = routes.OrderBy(x => x.Key).ToDictionary(x => x.Key, x => x.Value);

            }

            return RedirectToAction(nameof(Upload));
        }

    }
}