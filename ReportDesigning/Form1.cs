using System;
using System.Diagnostics;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ReportDesigning
{
    public partial class PrintPreview : Form
    {
        private string invId;
        private WebBrowser webBrowser;

        public PrintPreview(string name)
        {
            InitializeComponent();

            invId = name;
            label1.Text = name;

            // Initialize the WebBrowser control
            InitializeWebBrowser();

            // Subscribe to Load event to ensure that the control is fully initialized
            this.Load += Form1_Load;
            this.Hide();
        }

        private void InitializeWebBrowser()
        {
            webBrowser = new WebBrowser();
            webBrowser.DocumentCompleted += WebBrowser_DocumentCompleted;
            webBrowser.Visible = false; // Hide the WebBrowser control
            this.Controls.Add(webBrowser);

            // Ensure the handle is created
            if (!webBrowser.IsHandleCreated)
            {
                Trace.WriteLine($"[{DateTime.Now}] Forcing handle creation for WebBrowser.");
                webBrowser.HandleCreated += WebBrowser_HandleCreated;
                var handle = webBrowser.Handle; // Accessing the Handle property forces handle creation
            }
        }

        private async void Form1_Load(object sender, EventArgs e)
        {
            await LoadHtmlAndPrint(this.invId);
        }

        private async Task LoadHtmlAndPrint(string inv)
        {
            string serverUrl = $"http://localhost:1024/Invoice/getPrintViewOfInvoice?inv_id={inv}";
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    client.Timeout = TimeSpan.FromSeconds(60);
                    HttpResponseMessage response = await client.GetAsync(serverUrl).ConfigureAwait(false);
                    if (response.IsSuccessStatusCode)
                    {
                        string htmlContent = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                        WB_View.DocumentText = htmlContent;
                        // Interact with the WebBrowser control
                        if (webBrowser != null && webBrowser.IsHandleCreated)
                        {
                            this.Invoke(new Action(() => PrintHtmlContent(htmlContent)));
                        }
                        else
                        {
                            this.HandleCreated += (s, e) =>
                            {
                                this.Invoke(new Action(() => PrintHtmlContent(htmlContent)));
                            };
                        }
                    }
                    else
                    {
                        Trace.WriteLine($"[{DateTime.Now}] Failed to retrieve HTML content from the server. Status code: {response.StatusCode}");
                    }
                }
            }
            catch (TaskCanceledException ex)
            {
                Trace.WriteLine($"[{DateTime.Now}] Request timed out: {ex.Message}");
            }
            catch (HttpRequestException ex)
            {
                Trace.WriteLine($"[{DateTime.Now}] Request error: {ex.Message}");
            }
            catch (Exception ex)
            {
                Trace.WriteLine($"[{DateTime.Now}] An error occurred: {ex.Message}");
            }
        }

        private void WebBrowser_HandleCreated(object sender, EventArgs e)
        {
            Trace.WriteLine($"[{DateTime.Now}] WebBrowser handle created event fired.");
        }

        private void PrintHtmlContent(string htmlContent)
        {
            Trace.WriteLine($"[{DateTime.Now}] PrintHtmlContent executed.");
            webBrowser.DocumentText = htmlContent;
        }

        private void WebBrowser_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            if (webBrowser.ReadyState == WebBrowserReadyState.Complete)
            {
                Trace.WriteLine($"[{DateTime.Now}] Document loaded, starting print.");
                webBrowser.Print();
                Trace.WriteLine($"[{DateTime.Now}] Print command issued.");

                // Clean up
                this.Controls.Remove(webBrowser);
                webBrowser.Dispose();
                Trace.WriteLine($"[{DateTime.Now}] WebBrowser control disposed. Exiting application.");

                // Wait for a moment before closing to ensure the print job is sent
               // Task.Delay(5000).ContinueWith(_ => form1.Hide());
            }
        }

        private async void PrintReport_Click(object sender, EventArgs e)
        {
            await LoadHtmlAndPrint(this.invId);
        }
    }
}
