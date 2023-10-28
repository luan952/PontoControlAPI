using ClosedXML.Excel;
using DocumentFormat.OpenXml.Spreadsheet;
using PontoControl.Comunication.Requests;
using PontoControl.Comunication.Responses;
using System.Diagnostics;

namespace PontoControl.Application.UseCases.Marking.DowloadMarkings
{
    public class DowloadMarkings : IDowloadMarkings
    {

        public async Task<MemoryStream> Execute(DowloadMarkingRequest request)
        {
            return await Task.Run(() =>
            {
                var wb = new XLWorkbook();
                var ws = wb.Worksheets.Add("Dados");

                ws.Cell(1, 1).Value = "Dia";
                ws.Cell(1, 2).Value = "Entrada 1";
                ws.Cell(1, 3).Value = "Saída 1";
                ws.Cell(1, 4).Value = "Entrada 2";
                ws.Cell(1, 5).Value = "Saída 2";

                var count = 2;
                foreach (GetMarkingResponse item in request.ListMarkings)
                {
                    ws.Cell(count, 1).Value = item.Date.Date;
                    ws.Cell(count, 2).Value = item.Marking.Count > 0 ? GetFormattedHour(item.Marking[0].Hour) : "Sem marcação";
                    ws.Cell(count, 3).Value = item.Marking.Count > 1 ? GetFormattedHour(item.Marking[1].Hour) : "Sem marcação";
                    ws.Cell(count, 4).Value = item.Marking.Count > 2 ? GetFormattedHour(item.Marking[2].Hour) : "Sem marcação";
                    ws.Cell(count, 5).Value = item.Marking.Count > 3 ? GetFormattedHour(item.Marking[3].Hour) : "Sem marcação";

                    count++;
                }


                foreach (var cell in ws.Row(1).CellsUsed())
                {
                    cell.Style.Font.Bold = true;
                }

                foreach (var cell in ws.CellsUsed())
                {
                    cell.Style.Border.TopBorder = XLBorderStyleValues.Thin;
                    cell.Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                    cell.Style.Border.LeftBorder = XLBorderStyleValues.Thin;
                    cell.Style.Border.RightBorder = XLBorderStyleValues.Thin;

                    cell.WorksheetColumn().AdjustToContents();

                    if (cell.Value.Equals("Sem marcação"))
                        cell.Style.Fill.BackgroundColor = XLColor.Yellow;
                }

                var stream = new MemoryStream();
                wb.SaveAs(stream);
                stream.Seek(0, SeekOrigin.Begin);

                return stream;
            });
        }

        private string GetFormattedHour(DateTime? dateTime)
        {
            if (dateTime.HasValue)
            {
                return dateTime.Value.TimeOfDay.ToString("hh\\:mm");
            }

            return "Sem marcação";
        }
    }
}
