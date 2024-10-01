using File_Convertor.Data;
using File_Convertor.Models;
using Microsoft.AspNetCore.Mvc;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Formats.Webp;
using SixLabors.ImageSharp.Processing;
using System.Diagnostics;

namespace File_Convertor.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly MovieCoreDBContext _context;

        public HomeController(ILogger<HomeController> logger, MovieCoreDBContext context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }


        [HttpGet]
        public IActionResult FileUpload()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> FileUpload(FileUploadViewModel model)
        {
            if (model.Files == null || model.Files.Count == 0)
            {
                ModelState.AddModelError("Files", "Please upload at least one file.");
                return View(model);
            }

            var outputDirectory = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads");

            if (!Directory.Exists(outputDirectory))
            {
                Directory.CreateDirectory(outputDirectory);
            }

            foreach (var file in model.Files)
            {
                if (file.Length > 0 && Path.GetExtension(file.FileName).ToLower() == ".png")
                {
                    // Generate new file name with .webp extension
                    var webpFileName = Path.ChangeExtension(file.FileName, ".webp");
                    var webpFilePath = Path.Combine(outputDirectory, webpFileName);

                    // Load the uploaded image
                    using (var image = await Image.LoadAsync(file.OpenReadStream()))
                    {
                        // Optionally, you can resize the image
                        image.Mutate(x => x.Resize(800, 600));  // Resize example, remove if not needed

                        // Save the image in .webp format
                        await image.SaveAsWebpAsync(webpFilePath, new WebpEncoder
                        {
                            Quality = 75 // Adjust quality if needed
                        });
                    }
                    // Save file details to the database
                    var uploadedFile = new UploadedFile
                    {
                        FileName = webpFileName,
                        CreatedDate = DateTime.Now
                    };
                    _context.UploadedFiles.Add(uploadedFile);
                    await _context.SaveChangesAsync();
                }

            }

            ViewBag.Message = "Files uploaded and converted successfully!";
            return View();
        }


    }
}
