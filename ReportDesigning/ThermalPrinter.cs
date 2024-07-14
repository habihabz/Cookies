using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ReportDesigning
{
    class ThermalPrinter
    {
        private WebBrowser webBrowser;

        public ThermalPrinter()
        {
            // Initialize the WebBrowser control
            webBrowser = new WebBrowser();
            webBrowser.DocumentCompleted += new WebBrowserDocumentCompletedEventHandler(WebBrowser_DocumentCompleted);
        }

        public void PrintHtmlContent(string htmlContent)
        {
            // Load HTML content into the WebBrowser control
            webBrowser.DocumentText = htmlContent;
        }

        private void WebBrowser_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            // Print the content of the WebBrowser control
            webBrowser.Print();
        }

        // Example method to initiate printing
        public void Print(string htmlContent)
        {
            try
            {
                
                PrintHtmlContent(htmlContent);
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred while printing: " + ex.Message);
            }
        }
    }
}
