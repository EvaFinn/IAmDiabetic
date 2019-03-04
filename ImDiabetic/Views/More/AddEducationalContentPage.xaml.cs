using System;
using System.Collections.Generic;
using Syncfusion.Pdf;
using Syncfusion.Pdf.Parsing;
using Syncfusion.Pdf.Graphics;
using Syncfusion.Pdf.Grid;
using Xamarin.Forms;
using System.IO;
using ImDiabetic.Utils;
using Syncfusion.Drawing;
using System.Diagnostics;

namespace ImDiabetic.Views.More
{
    public partial class AddEducationalContentPage : ContentPage
    {
        public AddEducationalContentPage()
        {
            InitializeComponent();
        }

        private static void CreatePDF(string text)
        {
            // Create a new PDF document
            PdfDocument document = new PdfDocument();

            //Add a page to the document
            PdfPage page = document.Pages.Add();

            //Create PDF graphics for the page
            PdfGraphics graphics = page.Graphics;

            //Set the standard font
            PdfFont font = new PdfStandardFont(PdfFontFamily.Helvetica, 20);

            //Draw the text
            graphics.DrawString(text, font, PdfBrushes.Black, new PointF(0, 0));

            //Save the document to the stream
            MemoryStream stream = new MemoryStream();
            document.Save(stream);

            //Close the document
            document.Close(true);

            //Save the stream as a file in the device and invoke it for viewing
            Xamarin.Forms.DependencyService.Get<ISave>().SaveAndView("Output.pdf", "application / pdf", stream);
            //var dependency = new DependencyService();
        }

        void Handle_Clicked(object sender, System.EventArgs e)
        {
            Debug.WriteLine("Tap - " + editor.Text);
            CreatePDF(editor.Text);
        }
    }
}
