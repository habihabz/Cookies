using System;
using System.IO;
using System.Net.Http;
using System.Windows.Forms;

class Program
{
    public string URLroot = "", printerAddress = "";
    static void Main(string[] args)
    {
         
        try
        {
            var FilePath = @"C:\PRINT\";
            if (!Directory.Exists(FilePath))
            {
                Directory.CreateDirectory(FilePath);
            }
            HttpClient client = new HttpClient();
            string[] lines1 = { "" };
            System.IO.File.WriteAllLines(@"C:\PRINT\print.txt", lines1);
           
            
            string[] format = "".Split();
            
            System.IO.File.AppendAllLines(@"C:\PRINT\print.txt", format);
            

            string printerAddress = System.IO.File.ReadAllText(@"C:\PRINT\printerAddress.txt");
            System.IO.File.Copy(@"c:\print\print.txt", printerAddress, true);
            

            MessageBox.Show("Success");
           

        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message, "Error");
        }
    }
}