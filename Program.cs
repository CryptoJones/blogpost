using System;
using System.IO;

namespace blogpost
{
    class Program
    {
        static void Main(string[] args)
        {
            string document;
            string linktitle = "New Page";
            string newpagename;
            string pagedata;


            try {
            
            // create new page

            // create new link

            // read HTML file
                FileStream fileStream = new FileStream("index.html", FileMode.Open);
                    using (StreamReader reader = new StreamReader(fileStream))
                    {
                         document = reader.ReadToEnd();
                    }        

            // insert link

            // replace HTML file


            } catch (Exception exception) {

                Console.WriteLine("Error occured. See Log.");
                LogMessage("Exception caught: " + exception);
            }
    
            Console.WriteLine("Update Complete!");
        }

        private static void LogMessage(string message){
         var logPath = System.IO.Path.GetTempFileName();
            using (var writer = File.CreateText(logPath))
            {
                writer.WriteLine(message); //or .Write(), if you wish
            }
        }
    }
}
