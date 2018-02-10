using System;
using System.IO;

namespace blogpost
{
    class Program
    {
        static void Main(string[] args)
        {
            string documentTemp;
            bool testing = false;
            



            //read blogpost.config.json
                // TODO

            //populate variables from json
            string directory = "www.cryptospace.com";
            string linkTitle = "New Page";
            DateTime now = DateTime.Now;
            string newPageName = now.ToString("u").ToString().Substring(0,10).Replace("\\", "-") + ".html";
            
            // string pageData = "<p>I'm a bannana!</p>";
            
            try {
            
            // create new page

            // create new link
            string newLink = "<h3>"+ newPageName.Substring(0,10)+ "</h3><ul><li><a href=\""+newPageName+"\">"+ linkTitle+ "</a></li>";

            // read HTML file
                FileStream fileStream = new FileStream(directory + "/" + "index.html", FileMode.Open);
                    using (StreamReader reader = new StreamReader(fileStream))
                    {
                         documentTemp = reader.ReadToEnd();
                    }        

            // replace <!-- NEW LINKS HERE--> with link
             string newDocument =  documentTemp.Replace("<!-- NEW LINKS HERE-->", "<!-- NEW LINKS HERE-->" + newLink);
            
            // replace HTML file
            if (!testing) {

                using (var newfileStream = System.IO.File.Create(directory + "/"+ "index.html")) {

                    using (var fileWriter = new System.IO.StreamWriter(newfileStream)) { 

                        fileWriter.WriteLine(newDocument);

                    }

                }

            }

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
