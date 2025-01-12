using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;

namespace CreatePDF.API.Services
{
    public class PdfService
    {
        public byte[] CreateSamplePdf()
        {
            var document = Document.Create(container =>
            {
                container.Page(page =>
                {
                    page.Size(PageSizes.A2);
                    page.Margin(1, Unit.Centimetre);

                    // Header with a horizontal line
                    page.Header().Text("Information on Anti-Money Laundering Measures").FontSize(20).Bold().AlignLeft();

                    page.Content().Column(column =>
                    {
                        column.Spacing(5);

                        // Add a paragraph
                        //column.Item().Text("Information on Anti-Money Laundering Measures").Bold();
                        column.Item().Element(element =>
                        {
                            element.Width(450) // Set the desired width of the line (e.g., 300 units)
                                  .LineHorizontal(1)
                                  .LineColor(Colors.Grey.Darken1);
                        });
                        column.Item().Text("(Complete or indicate with X)"); 
                        column.Item().Text("Name of Authorised Dealer with limited authority (ADLA): NEC MONEY (PTY) LTD").FontSize(15).Bold();







                        // Add a table
                        column.Item().Table(table =>
                        {
                            table.ColumnsDefinition(columns =>
                            {
                                //columns.RelativeColumn(); // Adjusts column width automatically
                                columns.ConstantColumn(150);
                                columns.RelativeColumn();
                            });

                            table.Header(header =>
                            {
                                header.Cell().Text("Month").Bold();
                                header.Cell().Text("31st january").Bold();
                            });

                            for (int i = 1; i <= 5; i++)
                            {
                                table.Cell().Text($"Row {i}, Col 1");
                                table.Cell().Text($"Row {i}, Col 2");
                            }
                        });
                    });
                    page.Footer().Text(text =>
                    {
                        text.Span("Page ").Bold();
                        text.CurrentPageNumber();
                        text.Span(" of ");
                        text.TotalPages();
                    });
                });
            });

            // Generate PDF as byte array
            return document.GeneratePdf();
        }
    }
}
