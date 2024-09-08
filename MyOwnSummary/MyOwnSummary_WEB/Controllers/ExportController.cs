using Microsoft.AspNetCore.Mvc;
using MyOwnSummary_WEB.Models.Dtos.NoteDtos;
using Newtonsoft.Json;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.IO;

namespace MyOwnSummary_WEB.Controllers
{
    public class ExportController : Controller
    {
        [HttpPost]
        public IActionResult Export(string notesJson)
        {
            var notes = JsonConvert.DeserializeObject<List<NoteDto>>(notesJson);
            if(notes != null)
            {
                using (var memoryStream = new MemoryStream())
                {
                    var document = new Document();
                    PdfWriter.GetInstance(document, memoryStream);
                    document.Open();

                    // Create a table with two columns
                    var table = new PdfPTable(4);

                    // Add headers
                    table.AddCell("Source Text");
                    table.AddCell("Translate");
                    table.AddCell("Pronunciation");
                    table.AddCell("Description");

                    // Add notes to the table
                    foreach (var note in notes)
                    {
                        table.AddCell(note.SourceText);
                        table.AddCell(note.Translate);
                        table.AddCell(note.Pronunciation);
                        table.AddCell(note.Description);
                    }

                    // Add the table to the document
                    document.Add(table);
                    document.Close();

                    // Return the PDF document as a file download
                    return File(memoryStream.ToArray(), "application/pdf", "notes.pdf");
                }
            }
            return View("Error");
        }
    }
}
