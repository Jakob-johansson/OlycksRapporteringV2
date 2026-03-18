using OlycksRapporteringV2.Domain.Entities;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;

namespace OlycksRapporteringV2.MAUI.Services
{
    public class PdfService
    {
        public PdfService()
        {
            //Gratis licens
            QuestPDF.Settings.License = LicenseType.Community;
        }

        //GENERERA PDF AV EN RAPPORT\\
        public byte[] GenerateReportPdf(Report report)
        {
            return Document.Create(container =>
            {
                container.Page(page =>
                {
                    page.Size(PageSizes.A4);
                    page.Margin(2, Unit.Centimetre);
                    page.DefaultTextStyle(x => x.FontSize(11));

                    //HEADER\\
                    page.Header().Column(col =>
                    {
                        col.Item().Text("INCIDENTRAPPORT")
                            .Bold().FontSize(22).FontColor("#E83B3B");
                        col.Item().Text($"Skapad: {report.CreatedAt:yyyy-MM-dd HH:mm}")
                            .FontSize(10).FontColor("#888888");
                        col.Item().PaddingTop(4).LineHorizontal(1).LineColor("#E83B3B");
                    });

                    //INNEHÅLL\\
                    page.Content().PaddingTop(20).Column(col =>
                    {
                        col.Spacing(16);

                        //GRUNDINFO\\
                        col.Item().Text("Grundläggande information")
                            .Bold().FontSize(13).FontColor("#E83B3B");

                        col.Item().Table(table =>
                        {
                            table.ColumnsDefinition(c =>
                            {
                                c.RelativeColumn(1);
                                c.RelativeColumn(2);
                            });

                            void Row(string label, string value)
                            {
                                table.Cell().Padding(5).Background("#F5F5F5").Text(label).FontColor("#888888");
                                table.Cell().Padding(5).Text(value ?? "-").Bold();
                            }

                            Row("Titel", report.ReportTitle);
                            Row("Typ av olycka", report.AccidentType);
                            Row("Plats", report.Location);
                            Row("Tid vid olycka", report.TimeOfAccident);
                            Row("Inblandade", report.PeopleInvolved);
                            Row("Rapporten gäller", report.AffectedPersonType);
                            Row("Status", report.Status.ToString());
                        });

                        //BESKRIVNING\\
                        col.Item().Text("Beskrivning")
                            .Bold().FontSize(13).FontColor("#E83B3B");
                        col.Item().Background("#F5F5F5").Padding(10)
                            .Text(report.ReportDescription ?? "-");

                        //VÄDER\\
                        if (!string.IsNullOrEmpty(report.WeatherDescription))
                        {
                            col.Item().Text("Väder vid olyckan")
                                .Bold().FontSize(13).FontColor("#E83B3B");

                            col.Item().Table(table =>
                            {
                                table.ColumnsDefinition(c =>
                                {
                                    c.RelativeColumn(1);
                                    c.RelativeColumn(2);
                                });

                                void Row(string label, string value)
                                {
                                    table.Cell().Padding(5).Background("#F5F5F5").Text(label).FontColor("#888888");
                                    table.Cell().Padding(5).Text(value ?? "-").Bold();
                                }

                                Row("Plats", report.WeatherLocation);
                                Row("Väder", report.WeatherDescription);
                                Row("Temperatur", $"{report.Temperature:F1}°C");
                                Row("Vindhastighet", $"{report.WindSpeed:F1} km/h");
                            });
                        }

                        //SKADOR & ÅTGÄRDER\\
                        col.Item().Text("Skador & Åtgärder")
                            .Bold().FontSize(13).FontColor("#E83B3B");

                        col.Item().Table(table =>
                        {
                            table.ColumnsDefinition(c =>
                            {
                                c.RelativeColumn(1);
                                c.RelativeColumn(2);
                            });

                            void Row(string label, string value)
                            {
                                table.Cell().Padding(5).Background("#F5F5F5").Text(label).FontColor("#888888");
                                table.Cell().Padding(5).Text(value ?? "-").Bold();
                            }

                            Row("Materiell skada", report.MaterialDamage);
                            Row("Omedelbara åtgärder", report.ImmediateActions);
                            Row("Förebyggande åtgärder", report.PreventiveActions);
                        });
                    });

                    //FOOTER\\
                    page.Footer().AlignCenter().Text(x =>
                    {
                        x.Span("OlycksRapporteraren — Rapport ID: ").FontColor("#888888").FontSize(9);
                        x.Span(report.Id).FontColor("#888888").FontSize(9);
                    });
                });
            }).GeneratePdf();
        }

        //SPARA PDF PÅ ENHETEN\\
        public async Task<string> SavePdf(Report report)
        {
            var pdfBytes = GenerateReportPdf(report);
            var fileName = $"Rapport_{report.ReportTitle}_{report.CreatedAt:yyyyMMdd}.pdf"
                .Replace(" ", "_");
            var path = Path.Combine(FileSystem.CacheDirectory, fileName);
            await File.WriteAllBytesAsync(path, pdfBytes);
            return path;
        }
    }
}