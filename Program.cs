using System;
using System.IO;

namespace blogpost
{
    class Program
    {
        static void Main(string[] args)
        {
            string documentTemp;
                       
            
            //read blogpost.config.json
                // TODO

            //populate variables from json
            string directory = "www.cryptospace.com";
            string linkTitle = "New Page";
            string middle = String.Empty;

            DateTime now = DateTime.Now;
            string newPageName = now.ToString("u").ToString().Substring(0,10).Replace("\\", "-") + ".html";
            string firstHalf = "<!DOCTYPE html><html lang=\"en-US\"><head><meta charset=\"UTF-8\" /><meta http-equiv=\"X-UA-Compatible\" content=\"IE=edge\" /><!-- SEO --><title>Aaron K. Clark - CryptoJones</title><meta name=\"description\" content=\"Aaron K. Clark, CryptoJones, CV, Resume, Email, Cybersecurity, Software Engineering, Information Technology\" /><meta name=\"robots\" content=\"index, follow\" /><meta name=\"referrer\" content=\"always\" /><!-- Favicon --><link rel=\"icon\" type=\"image/png\" href=\"images/favicon.png\" sizes=\"32x32\"><!-- Styles --><link rel='stylesheet' href='css/split.css' type='text/css' media='screen' /><meta name=\"viewport\" content=\"width=device-width,initial-scale=1\" /></head><body id=\"fullsingle\" class=\"page-template-page-fullsingle-split\"><div class=\"fs-split\"><!-- Image Side --><div class=\"split-image\"></div><!-- Content Side --><div class=\"split-content\"><div class=\"split-content-vertically-center\"><div class=\"split-intro\"><span class=\"tagline\">"+newPageName.Substring(0,10)+"</span></div><div class=\"split-bio\">";
            string secondHalf = "</div><div class=\"split-lists\"><div class=\"split-list\"><ul><li><a href=\"blog.html\">Go Back</a></li></div></div><div class=\"split-credit\"><p>&copy;2017 - Present <a href=\"#\">Aaron K. Clark</a></div></div></div></div></body></html>";          
            
            try {
            
            // create new page

                            using (var newfileStream = System.IO.File.Create(directory + "/"+ newPageName)) {

                    using (var fileWriter = new System.IO.StreamWriter(newfileStream)) { 

                        fileWriter.WriteLine(firstHalf + middle +secondHalf);

                    }

                }

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
           

                using (var newfileStream = System.IO.File.Create(directory + "/"+ "index.html")) {

                    using (var fileWriter = new System.IO.StreamWriter(newfileStream)) { 

                        fileWriter.WriteLine(newDocument);

                    }

                }

            

            } catch (Exception exception) {

                Console.WriteLine("Error occured. See Log.");
                LogMessage("Exception caught: " + exception);
            }
    
            Console.WriteLine("Update Complete!");
        }

        private static void LogMessage(string message){
            
        DateTime now = DateTime.Now;
        string newFileName = now.ToString("u").ToString().Substring(0,10).Replace("\\", "-");
        
        var logPath = newFileName + System.IO.Path.GetTempFileName() + ".log";
            using (var writer = File.CreateText(logPath))
            {
                writer.WriteLine(message); //or .Write(), if you wish
            }
        }
    }
}
