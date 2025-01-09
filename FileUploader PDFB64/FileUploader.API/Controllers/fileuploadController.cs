using System.Text;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FileUploader.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class fileuploadController : ControllerBase
    {
        [HttpPost("UploadFile")]
        public async Task<IActionResult> UploadFile(IFormFile file)
        {
            if (file == null || file.Length == 0)
                return BadRequest("No File was found...");

            var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/uploads");
            var filePath = Path.Combine(uploadsFolder, file.FileName);

            // Ensure the uploads folder exists
            if (!Directory.Exists(uploadsFolder))
            {
                Directory.CreateDirectory(uploadsFolder);
            }

            // Save File To the Server
            using ( var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            return Ok(new
            {
                Filename = file.FileName,
                FileSize = file.Length,
                FilePath = $"/uploads/{file.FileName}"
            });
        }
        [HttpPost("UploadEncodedFile")]
        public async Task<IActionResult> UploadEncodedFile(IFormFile file)
        {
            if(file == null || file.Length == 0)
            return BadRequest("No File Was Found");

            //if (Path.GetExtension(file.FileName)?.ToLower() != ".pdf")
                //return BadRequest("Only Pdf File Is supported");

            var uploadFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/uploads");
            var encodedFolder = Path.Combine(uploadFolder, "encoded");
            var finalPath = Path.Combine(encodedFolder, file.FileName);

            // Ensure uploads Folder Exists
            if (!Directory.Exists(uploadFolder))
            {
                Directory.CreateDirectory(uploadFolder);
            }
            if (!Directory.Exists(encodedFolder))
            {
                Directory.CreateDirectory(encodedFolder);
            }

            // Save The PDF File
            using (var stream = new FileStream(finalPath, FileMode.Create)) 
            {
               await file.CopyToAsync(stream);
            }

            // Read The File Content And encode it to Base64
            byte[] fileBytes = await System.IO.File.ReadAllBytesAsync(finalPath);
            string base64EncodedString = Convert.ToBase64String(fileBytes);

            // Save the Base64 String to a File
            var encodedFilePath = Path.Combine(encodedFolder, $"{Path.GetFileNameWithoutExtension(file.FileName)}.txt");
            await System.IO.File.WriteAllTextAsync(encodedFilePath, base64EncodedString, Encoding.UTF8);

            return Ok();
        }
        [HttpPost("DecodeAndSaveFile")]
        public async Task<IActionResult> DecodeAndSaveFile(IFormFile file) 
        {
            if(file == null || file.Length == 0)
                return BadRequest("No File Was Found");

            if(Path.GetExtension(file.FileName) != ".txt")
                return BadRequest("Only Text File Is Supported");

            var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/uploads");
            var decodedFolder = Path.Combine(uploadsFolder, "decoded");

            // Ensure The Decoded Folder Exists
            if (!Directory.Exists(decodedFolder))
            {
                Directory.CreateDirectory(decodedFolder);
            }

            var endodedFilePath = Path.Combine(decodedFolder, file.FileName);
            // Save the uploaded encoded text file temporarily
            using (var stream = new FileStream(endodedFilePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            // Read The Base64 Content from the file
            string base64Content;
            try
            {
                base64Content = await System.IO.File.ReadAllTextAsync(endodedFilePath);
            }
            catch
            {
                return BadRequest("Failed to read the content of the file");
            }

            // Convert the bas64 text fiel to Original Pdf
            byte[] pdfBytes;
            try
            {
                pdfBytes =  Convert.FromBase64String(base64Content);
            }
            catch 
            {
                return BadRequest("The File Content is not base64 encoded String");
            }

            // Save The decoded PDF File
            var originalFileName = Path.GetFileNameWithoutExtension(file.FileName) + ".pdf";
            var decodedFilePath = Path.Combine(decodedFolder, originalFileName);

            await System.IO.File.WriteAllBytesAsync(decodedFilePath, pdfBytes);

            return Ok(new
            {
                EncodedFilename = file.FileName,
                DecodedFilename = originalFileName,
                DecodedFilePath = $"/uploads/decoded/{originalFileName}"
            });
        }
        [HttpPost("UploadAndProcessPdf")]
        public async Task<IActionResult> UploadAndProcessPdf(IFormFile file)
        {
            if (file == null || file.Length == 0)
                return BadRequest("No File Ws Found");

            if (Path.GetExtension(file.FileName)?.ToLower() != ".pdf")
                return BadRequest();

            var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/uploads");
            var processedFolder = Path.Combine(uploadsFolder, "procssed");
            var tempFolder = Path.Combine(uploadsFolder, "temp");

            // Ensure the processed folder exists
            if (!Directory.Exists(processedFolder))
            {
                Directory.CreateDirectory(processedFolder);
            }

            // Ensure the temp folder exists
            if (!Directory.Exists(tempFolder)) 
            {
                Directory.CreateDirectory(tempFolder);
            }

            // Save the original PDF temporarily
            var tempFilePath = Path.Combine(tempFolder, file.FileName);
            using (var stream = new FileStream(tempFilePath, FileMode.Create)) 
            {
                await file.CopyToAsync(stream);
            }

            // Read the PDF File to Byte Array
            byte[] pdfBytes;
            //try
            //{
                pdfBytes = await System.IO.File.ReadAllBytesAsync(tempFilePath);
           // }
            //catch
            //{
            //    BadRequest("Failed To read the Uploaded PDF File");
            //}

            // Encode the PDF to Base64
            string base64String;
            try
            {
                base64String = Convert.ToBase64String(pdfBytes);
            }
            catch 
            {
                return BadRequest("Failed to encode the PDF to Base64.");
            }

            // Decode the Base64 back to a byte array
            byte[] decodedPdfBytes;
            try
            {
                decodedPdfBytes = Convert.FromBase64String(base64String);
            }
            catch
            {
                return BadRequest("Failed to decode the base64 string back to PDF");
            }

            // Save the decoded PDF
            var decodedFileName = Path.GetFileNameWithoutExtension(file.FileName) + "-decoded.pdf";
            var decodedFilePath = Path.Combine(processedFolder, decodedFileName);

            await System.IO.File.WriteAllBytesAsync(decodedFilePath, decodedPdfBytes);

            return Ok(new
            {
                OriginalFilename = file.FileName,
                EncodedBase64Length = base64String.Length,
                DecodedFilename = decodedFileName,
                DecodedFilePath = $"/uploads/processed/{decodedFileName}"
            });
        }
    }
}
