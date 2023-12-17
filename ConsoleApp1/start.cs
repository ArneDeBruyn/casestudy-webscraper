using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Intrinsics.X86;
using System.Text;
using System.Threading.Tasks;

namespace SeleniumWebscraper
{
   class Start
    {
        // start of program
        static void Main(string[] args)
        {
            // welcome message with information about project
            Console.WriteLine("DevOps Case 5: Web Scraping Tool based on Selenium and C# . Author - Arne De Bruyn\n-----------------------------------------");
            webcraper scraper = new webcraper();
            
            // while loop with options the user can use in this webscraper
            while(true)
            {
                Console.WriteLine("Choose one of the webscraping options:\n1 - Scrape data of the 5 most recent YT video's based on a searchterm.\n2 - Scrape data from the 5 most recent job offers based on a searchterm.\n3 - Scrape the stocks of the 10 biggest companies in the world from tradingview.com.\n4 - Close webscraper");
                string option = Console.ReadLine();

                if (option == "1")
                {
                    // user defines parameters for youtube webscraper in console
                    Console.WriteLine("Enter a search term for YouTube:");
                    string searchTerm = Console.ReadLine();
                    Console.WriteLine("Enter a filepath to save the file:");
                    string filePath = Console.ReadLine();
                    Console.WriteLine("Do you want to save the data in a csv or json format?");
                    string fileType = Console.ReadLine();
                    Console.WriteLine("Eneter a file name:");
                    string fileName = Console.ReadLine();

                    if (fileType == "csv")
                    {
                        // see CSV method in program.cs
                        scraper.CSV(scraper.scrapeYouTube(searchTerm), filePath, fileName);
                    } 
                    else if (fileType == "json")
                    {
                        // see JSON method in program.cs
                        scraper.JSON(scraper.scrapeYouTube(searchTerm), filePath, fileName);
                    }
                    
                }
                else if (option == "2")
                {
                    // user defines parameters for job webscraper in console
                    Console.WriteLine("Enter a search term for a joboffer:");
                    string searchTerm = Console.ReadLine();
                    Console.WriteLine("Enter a filepath to save the file:");
                    string filePath = Console.ReadLine();
                    Console.WriteLine("Do you want to save the data in a csv or json format?");
                    string fileType = Console.ReadLine();
                    Console.WriteLine("Enter a file name:");
                    string fileName = Console.ReadLine();

                    if (fileType == "csv")
                    {
                        // see CSV method in program.cs
                        scraper.CSV(scraper.scrapeJobs(searchTerm), filePath, fileName);
                    }
                    else if (fileType == "json")
                    {
                        // see JSON method in program.cs
                        scraper.JSON(scraper.scrapeJobs(searchTerm), filePath, fileName);
                    }
                }
                else if (option == "3")
                {
                    // user defines parameters for stocks webscraper in console
                    Console.WriteLine("Enter a filepath to save the file:");
                    string filePath = Console.ReadLine();
                    Console.WriteLine("Do you want to save the data in a csv or json format?");
                    string fileType = Console.ReadLine();
                    Console.WriteLine("Eneter a file name:");
                    string fileName = Console.ReadLine();

                    if (fileType == "csv")
                    {
                        // see CSV method in program.cs
                        scraper.CSV(scraper.ScrapeTradingView(), filePath, fileName);
                    }
                    else if (fileType == "json")
                    {
                        // see JSON method in program.cs
                        scraper.JSON(scraper.ScrapeTradingView(), filePath, fileName);
                    }
                }
                // program closes when option 4 is chosen
                else if (option == "4")
                {
                    break;
                }
            }
        }
    }
}
