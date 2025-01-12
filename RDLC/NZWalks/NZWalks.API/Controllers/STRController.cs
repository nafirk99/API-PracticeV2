using AspNetCore.Reporting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NZWalks.API.Data;
using NZWalks.API.Models.Domain;
using NZWalks.API.Models.DTO;

namespace NZWalks.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class STRController : ControllerBase
    {
        private readonly NZWalksDbContext _dbContext;

        public STRController(NZWalksDbContext dbContext)
        {
            this._dbContext = dbContext;
        }

        [HttpPost("InputReportData")]
        public async Task<IActionResult> Create([FromBody] AddSTRDTO addSTRDTO)
        {
            var STRDomain = new STR
            {
                OneA = addSTRDTO.OneA,
                OneB = addSTRDTO.OneB,
                TwoA = addSTRDTO.TwoA,
                TwoB = addSTRDTO.TwoB,
                Three = addSTRDTO.Three,
                Four = addSTRDTO.Four,
                Five = addSTRDTO.Five,
                SixA = addSTRDTO.SixA,
                SixB = addSTRDTO.SixB,
                SixC = addSTRDTO.SixC,
                Seven = addSTRDTO.Seven,
                Eight = addSTRDTO.Eight,
                Nine = addSTRDTO.Nine
            };

            await _dbContext.STRs.AddAsync(STRDomain);
            await _dbContext.SaveChangesAsync();
            return Ok(STRDomain);
        }

        [HttpGet("GenerateSTRPDF")]
        public async Task<IActionResult> GenerateSTRPDF()
        {
            // Fetch data from the database
            var data = await _dbContext.STRs.FirstOrDefaultAsync(x => x.OneA == "1");

            // Convert the selected STR to a format suitable for RDLC
            var reportData = new List<object>
            {
                new
                {
                    data.OneA,
                    data.OneB,
                    data.TwoA,
                    data.TwoB,
                    data.Three,
                    data.Four,
                    data.Five,
                    data.SixA,
                    data.SixB,
                    data.SixC,
                    data.Seven,
                    data.Eight,
                    data.Nine
                }
            };

            // Load the RDLC report
            var reportPath = Path.Combine(Directory.GetCurrentDirectory(), "Reports", "STRReport.rdlc");
            var localReport = new LocalReport(reportPath);

            // Add the data source
            localReport.AddDataSource("STRDataSet", reportData);

            // Render the report to a PDF
            var result = localReport.Execute(RenderType.Pdf);

            // Return the PDF as a file
            return File(result.MainStream, "application/pdf", "STRReport.pdf");
        }
    }
}
