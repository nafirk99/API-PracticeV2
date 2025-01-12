using CreatePDF.API.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CreatePDF.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PdfController : ControllerBase
    {
        private readonly PdfService pdfService;

        public PdfController(PdfService pdfService)
        {
            this.pdfService = pdfService;
        }

        [HttpGet("")]
        public IActionResult GeneratePdf()
        {
            var pdfBytes = pdfService.CreateSamplePdf();

            // Return PDF As Downlaodable file
            return File(pdfBytes, "application/pdf", "sample.pdf");
        }
    }
}
