using System;
using System.IO;
using Newtonsoft.Json;

namespace blogpost
{
    class Program
    {
        static void Main(string[] args)
        {
            DateTime now = DateTime.Now;
            string documentTemp = String.Empty;
            string middle = String.Empty;
            string linkTitle = String.Empty;
            string directory = "/var/www/www.cryptospace.com";
            string newPageName = now.ToString("u").ToString().Substring(0, 10).Replace("\\", "-") + ".html";
            string firstHalf = "<!DOCTYPE html><html lang=\"en-US\"><head><meta charset=\"UTF-8\" /><meta http-equiv=\"X-UA-Compatible\" content=\"IE=edge\" /><!-- SEO --><title>Aaron K. Clark - CryptoJones</title><meta name=\"description\" content=\"Aaron K. Clark, CryptoJones, CV, Resume, Email, Cybersecurity, Software Engineering, Information Technology\" /><meta name=\"robots\" content=\"index, follow\" /><meta name=\"referrer\" content=\"always\" /><!-- Favicon --><link rel=\"icon\" type=\"image/png\" href=\"images/favicon.png\" sizes=\"32x32\"><!-- Styles --><link rel='stylesheet' href='css/split.css' type='text/css' media='screen' /><meta name=\"viewport\" content=\"width=device-width,initial-scale=1\" /></head><body id=\"fullsingle\" class=\"page-template-page-fullsingle-split\"><div class=\"fs-split\"><!-- Image Side --><div class=\"split-image\"></div><!-- Content Side --><div class=\"split-content\"><div class=\"split-content-vertically-center\"><div class=\"split-intro\"><span class=\"tagline\">" + newPageName.Substring(0, 10) + "</span></div><div class=\"split-bio\">";
            string secondHalf = "</div><div class=\"split-lists\"><div class=\"split-list\"><ul><li><a href=\"index.html\">Go Back</a></li></div></div><div class=\"split-credit\"><p>&copy;2017 - Present <a href=\"#\">Aaron K. Clark</a></div></div></div></div></body></html>";

            //read blogpost.config.json


            try
            {
                FileStream fileStream = new FileStream(directory + "/blogpost.config.json", FileMode.Open);

                using (StreamReader reader = new StreamReader(fileStream))
                {
                    documentTemp = reader.ReadToEnd();
                }


                // deserialze to object from json
                BlogPost bp = JsonConvert.DeserializeObject<BlogPost>(documentTemp);

                // populate variables with object data
                linkTitle = bp.title;
                middle = bp.data;

            }
            catch (Exception ex)
            {

                Console.WriteLine("Error occured. See Log.");
                DateTime eventTime = DateTime.Now;
                LogMessage(eventTime + " - Exception caught: " + ex);
                Environment.Exit(-1);
            }


            // create new page
            try
            {

                using (var newfileStream = System.IO.File.Create(directory + "/" + newPageName))
                {

                    using (var fileWriter = new System.IO.StreamWriter(newfileStream))
                    {

                        fileWriter.WriteLine(firstHalf + middle + secondHalf);

                    }

                }

            }
            catch (Exception ex)
            {

                Console.WriteLine("Error occured. See Log.");
                DateTime eventTime = DateTime.Now;
                LogMessage(eventTime + " - Exception caught: " + ex);
                Environment.Exit(-1);
            }

            // create new link
            string newLink = "<h3>" + newPageName.Substring(0, 10) + "</h3><ul><li><a href=\"" + newPageName + "\">" + linkTitle + "</a></li>";


            // read & replace HTML file

            try
            {

                FileStream exfileStream = new FileStream(directory + "/" + "index.html", FileMode.Open);

                using (StreamReader reader = new StreamReader(exfileStream))
                {
                    documentTemp = reader.ReadToEnd();
                }

                // replace <!-- NEW LINKS HERE--> with link data
                string newDocument = documentTemp.Replace("<!-- NEW LINKS HERE-->", "<!-- NEW LINKS HERE-->" + newLink);



                using (var newfileStream = System.IO.File.Create(directory + "/" + "index.html"))
                {

                    using (var fileWriter = new System.IO.StreamWriter(newfileStream))
                    {

                        fileWriter.WriteLine(newDocument);

                    }

                }


            }
            catch (Exception ex)
            {

                Console.WriteLine("Error occured. See Log.");
                DateTime eventTime = DateTime.Now;
                LogMessage(eventTime + " - Exception caught: " + ex);
                Environment.Exit(-1);
            }


            Console.WriteLine("Update Complete!");
        }

        private static void LogMessage(string message)
        {

            DateTime now = DateTime.Now;
            string newFileName = now.ToString("u").ToString().Substring(0, 10).Replace("\\", "-");

            try
            {

                var logPath = newFileName + ".log";
                using (var writer = File.AppendText(logPath))
                {
                    writer.WriteLine(message);
                }

            }
            catch (Exception ex)
            {

                Console.WriteLine("Something went wrong:" + ex);
                Environment.Exit(-1);

            }
        }
    }
}
