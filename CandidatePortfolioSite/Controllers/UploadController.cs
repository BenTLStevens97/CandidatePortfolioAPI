using Microsoft.AspNetCore.Mvc;
using System.IO;
using UploadFile.Models;

namespace UploadFile.Controllers
{
    public class UploadController : Controller
    {
        public IActionResult uploadView()
        {
            SingleFileModel model = new SingleFileModel();
            return View(model);
        }

        [HttpPost]
        public IActionResult Upload(SingleFileModel model)
        {

            SingleFileModel obj = new SingleFileModel()
            {
                FileName = model.FileName
            };
            
            if (ModelState.IsValid)
            {
                model.IsResponse = true;

                string path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Files");

                //create folder if not exist
                if (!Directory.Exists(path))
                    Directory.CreateDirectory(path);

                //get file extension
                FileInfo fileInfo = new FileInfo(model.File.FileName);
                string fileName = model.id + fileInfo.Extension;

                if (fileInfo.Extension == ".jpg") {


                string fileNameWithPath = Path.Combine(path, fileName);
                System.IO.File.Delete(fileNameWithPath);

                using (var stream = new FileStream(fileNameWithPath, FileMode.Create))
                {

                    model.File.CopyTo(stream);
                }

                model.IsSuccess = true;
                model.Message = "File upload successfully";
                }
                if (model.IsSuccess == false) { 
                model.Message = "Incorrect File Type, .jpg required";
                return View("uploadView", model);
                }
                return View("../Shared/UploadSuccess");
            }
            return View("../Candidates/Candidates");
        }






    }
}