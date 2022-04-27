using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using MVC.Models;
using MVC.Services;
using OfficeOpenXml;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVC.Controllers
{
    [Authorize]
    public class UploadController : Controller
    {
        private static IDictionary<string, List<string>> allDocumment = new Dictionary<string, List<string>>();
        private static ICollection<string> services = new List<string>();  
        private static ICollection<string> firstLine = new List<string>();
        private static string serviceName;
        private static string cityId;
        private static int allColumnsExcel;
        private static IWebHostEnvironment _hostEnvironment;
        private static string download;
        public UploadController(IWebHostEnvironment hostEnvironment)
        {
            _hostEnvironment = hostEnvironment;
        }
        // This action renders the form.
        public ActionResult Upload()
        {
            return View();
        }

        // This action handles the form POST and the upload
       
        public ActionResult UploadFile()
        {
            allDocumment = new Dictionary<string, List<string>>();
            var files = HttpContext.Request.Form.Files;

            if(files.Count > 0)
            {
                firstLine = new List<string>();
                ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
                using ExcelPackage package = new ExcelPackage(files[0].OpenReadStream());
                ExcelWorksheet worksheet = package.Workbook.Worksheets.FirstOrDefault();
                //receber linhas e colunas
                int rowsOfFile = worksheet.Dimension.Rows;
                int columnsOfFile = worksheet.Dimension.Columns;

                int columnCep = 0, columnServices = 0, columnOs =0;

                for (var column = 1; column < columnsOfFile; column++)
                {
                    firstLine.Add(worksheet.Cells[1, column].Value.ToString());

                    if (worksheet.Cells[1, column].Value.ToString().ToUpper().Equals("CEP"))
                        columnCep = column - 1;

                    if (worksheet.Cells[1, column].Value.ToString().ToUpper().Equals("SERVIÇO"))
                        columnServices = column;

                    if (worksheet.Cells[1, column].Value.ToString().ToUpper().Equals("OS"))
                        columnOs = column;
                }

                worksheet.Cells[2, 1, rowsOfFile, columnsOfFile].Sort(columnCep, false);
                List<string> keysOfDictionary = new List<string>();
                //criando instancias de listas e dicionarios
                List<string> servicesRaw = new List<string>();
                
                for (int rows = 1; rows < rowsOfFile; rows++)
                {
                    
                    var content = new List<string>();
                    for (int columns = 1; columns < columnsOfFile; columns++)
                    {
                        if (worksheet.Cells[rows, columnServices].Value == null)
                            break;
                        servicesRaw.Add(worksheet.Cells[rows, columnServices].Value.ToString().ToUpper());
                        var conteudo = worksheet.Cells[rows, columns].Value?.ToString() ?? "";
                        content.Add(conteudo);
                    }
                    if (worksheet.Cells[rows, columnOs].Value == null)
                        break;
                    var result = worksheet.Cells[rows, columnOs].Value?.ToString() ?? "";

                    if (result != null)
                        allDocumment.Add(result, content);
                    keysOfDictionary.Add(result);
                }
                allDocumment.Add("KEYS", keysOfDictionary);
                var service = servicesRaw.Distinct().ToList();
                firstLine = allDocumment["OS"].ToList();
                service.ForEach(x => services.Add(x));
                var result2 = allDocumment["OS"].ToList();
                int value = result2.IndexOf("OS");

                return RedirectToAction(nameof(OperationAddress));
            }
            return RedirectToAction(nameof(Upload));
        
        }

        public async Task<IActionResult> OperationAddress()
        {
            IEnumerable<Address> address = await AddressServices.GetAll();
            ViewBag.Address = address;
            ViewBag.Services = services;
            return View();
        }

        public IActionResult GetTeamsByCity()
        {
            serviceName = Request.Form["serviceName"].ToString();
            cityId = Request.Form["AddressOperationTeams"].ToString();
            if (string.IsNullOrEmpty(serviceName) || string.IsNullOrEmpty(cityId))
                return RedirectToAction(nameof(OperationAddress));
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Index()
        {
            IEnumerable<Teams> teams = await TeamsServices.GetTeamsByCity(cityId);

            ViewBag.Teams = teams;
            ViewBag.FirstLine = firstLine;

            return View();
        }

        public async Task<IActionResult> Create()
        {
            var teamsForServices = Request.Form["checkTeamForService"].ToList();
            var optionsOfDocumment = Request.Form["checkColumn"].ToList();



            if(teamsForServices.Count == 0 || optionsOfDocumment.Count == 0)
            {
                return RedirectToAction(nameof(Index));
            }

            var teams = new List<string>();

            foreach (var team in teamsForServices)
            {
                var result = await TeamsServices.Details(team);
                teams.Add(result.Name);
            }
            var addressSelected = await AddressServices.Details(cityId);
            await MakeFileDoc.Wirte(allDocumment, teams, optionsOfDocumment, serviceName, addressSelected);
                return View();
        }

        public FileContentResult Download()
        {
            var fileName = download.Split("//").ToList();
            var file = System.IO.File.ReadAllBytes(download);
            return File(file, "application/octet-stream", fileName.Last().ToString());
        }
    }
}