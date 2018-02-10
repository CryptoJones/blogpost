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
            string firstHalf = string.Empty;
            string incomingFileName = string.Empty;
            string incomingData = string.Empty;
            string secondHalf = String.Empty;
            //read blogpost.config.json


            try
            {
                FileStream configFileStream = new FileStream("blogpost.config.json", FileMode.Open);

                using (StreamReader reader = new StreamReader(configFileStream))
                {
                    documentTemp = reader.ReadToEnd();
                }


                // deserialze to object from json
                BlogPost bp = JsonConvert.DeserializeObject<BlogPost>(documentTemp);

                // populate variables with object data
                linkTitle = bp.title;
                incomingFileName = bp.data;

                FileStream firstHalfFileStream = new FileStream("template_first_half", FileMode.Open);
                using (StreamReader reader = new StreamReader(firstHalfFileStream))
                {
                    firstHalf = reader.ReadToEnd();
                }

                FileStream incomingDataFileStream = new FileStream(incomingFileName, FileMode.Open);
                using (StreamReader reader = new StreamReader(incomingDataFileStream))
                {
                    incomingData = reader.ReadToEnd();
                }

                FileStream secondHalfFileStream = new FileStream(incomingFileName, FileMode.Open);
                using (StreamReader reader = new StreamReader(secondHalfFileStream))
                {
                    secondHalf = reader.ReadToEnd();
                }

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
