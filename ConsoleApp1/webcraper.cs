using OpenQA.Selenium.Chrome;
using OpenQA.Selenium;
using System.Collections.ObjectModel;
using System.Text.Json;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace SeleniumWebscraper { 

    class webcraper
    {
        public List<Dictionary<string, string>> ScrapeTradingView()
        {
            // creates new instance of chrome driver
            var driver = new ChromeDriver();

            // navigates to the url in browser
            driver.Navigate().GoToUrl("https://www.tradingview.com/markets/world-stocks/worlds-largest-companies/");

            // finds all the html elements that use the "tr.row-RdUXZpkv.listRow" css selector and puts them in stock_list
            By elem_stock_list = By.CssSelector("tr.row-RdUXZpkv.listRow");
            ReadOnlyCollection<IWebElement> stock_list = driver.FindElements(elem_stock_list);

            // creates a new list of dictionaries
            List<Dictionary<string, string>> stocksDict = new List<Dictionary<string, string>>();
            // loop through a maximum of 10 stock_list elements or the count of stock_list if it's less than 10
            for (int i = 0; i < Math.Min(10, stock_list.Count); i++)
            {
                // saves the individual stock elements from the list
                IWebElement stock = stock_list[i];

                // find the company name element within the stock and extract its text
                IWebElement nameElement = stock.FindElement(By.CssSelector("sup"));
                string companyName = nameElement.Text;
                Console.WriteLine($"Company: {companyName}");

                // find all elements matching the price selector within the stock
                ReadOnlyCollection<IWebElement> priceElements = stock.FindElements(By.CssSelector("td.cell-RLhfr_y4.right-RLhfr_y4"));

                if (priceElements.Count > 1)
                {
                    // extract and print the stock price
                    string price = priceElements[1].Text;
                    Console.WriteLine($"Price: {price}");

                }
                if (priceElements.Count > 2)
                {
                    // extract and print the stock price change for today
                    IWebElement changeElement = priceElements[2].FindElement(By.CssSelector("span"));
                    string change = changeElement.Text;
                    Console.WriteLine($"Price change today: {change}");

                }

                // find all elements matching the sector selector within the stock
                ReadOnlyCollection<IWebElement> sectorElement = stock.FindElements(By.CssSelector("td.cell-RLhfr_y4.left-RLhfr_y4 a"));

                if (sectorElement.Count > 1)
                {
                    // Extract and print the sector of the stock
                    string sector = sectorElement[1].Text;
                    Console.WriteLine($"Sector: {sector}");

                }

                Console.WriteLine("------------------------");

                // create a dictionary to store stock details for this iteration
                IWebElement changeElement2 = priceElements[2].FindElement(By.CssSelector("span"));
                Dictionary<string, string> stockDict = new Dictionary<string, string>
                    {
                        { "Company", companyName },
                        { "Price", priceElements[1].Text },
                        { "Change Today", changeElement2.Text },
                        { "Sector", sectorElement[1].Text }
                    };
                // add the stock details to the list of dictionaries (stocksDict)
                stocksDict.Add(stockDict);
            }

            // closes web browser session
            driver.Quit();

            // returns dictionary of all stock details
            return stocksDict;
        }
        public List<Dictionary<string, string>> scrapeYouTube(string searchTerm)
        {
            // creates new instance of chrome driver
            var driver = new ChromeDriver();

            //nNavigate to the YouTube search results page based on the searchTerm
            driver.Navigate().GoToUrl($"https://www.youtube.com/results?search_query={searchTerm}");

            // find all elements that match the video list selector
            By elem_video_list = By.CssSelector("ytd-video-renderer");
            ReadOnlyCollection<IWebElement> video_list = driver.FindElements(elem_video_list);

            // create a list to store dictionaries representing video details
            List<Dictionary<string, string>> videosDict = new List<Dictionary<string, string>>();

            // loop through a maximum of 5 video_list elements or the count of video_list if it's less than 10
            for (int i = 0; i < Math.Min(5, video_list.Count); i++)
            {
                // saves the individual video elements from the list
                IWebElement video = video_list[i];

                // extract title, channel, channel link, and views information from the video element
                IWebElement titleElement = video.FindElement(By.CssSelector("a#video-title"));
                string title = titleElement.GetAttribute("title");

                IWebElement channelElement = video.FindElement(By.CssSelector("div.ytd-channel-name a"));
                string channelText = channelElement.GetAttribute("outerHTML").Split('>')[1].Split('<')[0].Trim();
                string channelHref = channelElement.GetAttribute("href");

                IWebElement viewsElements = video.FindElement(By.CssSelector("span.inline-metadata-item.style-scope.ytd-video-meta-block"));

                // output the extracted details to the console
                Console.WriteLine($"title: {title}");
                Console.WriteLine($"Channel: {channelText}");
                Console.WriteLine($"Channel link: {channelHref}");
                Console.WriteLine($"View Count: {viewsElements.Text}");
                Console.WriteLine("-------------------------");

                // creates a dictionary for each video's details and add it to the videosDict list
                Dictionary<string, string> videoDict = new Dictionary<string, string>
                {
                    { "Title", title },
                    { "Channel", channelText },
                    { "Channel-Link", channelHref },
                    { "Views", viewsElements.Text }
                };

                videosDict.Add(videoDict);
            }

            // close the driver and return the list of video dictionaries
            driver.Quit();
            return videosDict;
        }


        public List<Dictionary<string, string>> scrapeJobs(string searchTerm)
        {
            // creates new instance of chrome driver
            var driver = new ChromeDriver();

            // navigate to the job site 
            driver.Navigate().GoToUrl($"https://www.ictjob.be/en/search-it-jobs?keywords={searchTerm}");
            // wait 5s
            Thread.Sleep(5000);

            try
            {
                // handling the cookie consent if present
                IWebElement cookieConsent = driver.FindElement(By.CssSelector("a.button.cookie-layer-button"));
                cookieConsent.Click();
                Thread.Sleep(5000);

            }
            catch (NoSuchElementException)
            {
                // if the cookie consent isn't found press the data button
                IWebElement button1 = driver.FindElement(By.CssSelector("span.sort-by-date-container"));
                button1.Click();
            }
            // wait 5s
            Thread.Sleep(5000);

            IWebElement button = driver.FindElement(By.CssSelector("span.sort-by-date-container"));
            button.Click();
            // wait 5s
            Thread.Sleep(5000);

            // finding all job list elements on the page and puts them in job_list
            By elem_job_list = By.CssSelector("li.search-item.clearfix");
            ReadOnlyCollection<IWebElement> job_list = driver.FindElements(elem_job_list);

            // creates a list to store job details in dictionaries
            List<Dictionary<string, string>> jobDetails = new List<Dictionary<string, string>>();

            // loop through a maximum of 5 job_list elements or the count of job_list if it's less than 5
            for (int i = 0; i < Math.Min(5, job_list.Count); i++)
            {
                IWebElement job = job_list[i];
                // extracting job details and priniting to console
                IWebElement title = job.FindElement(By.CssSelector("h2.job-title"));
                Console.WriteLine($"Title: {title.Text}");

                IWebElement company = job.FindElement(By.CssSelector("span.job-company"));
                Console.WriteLine($"Company: {company.Text}");

                IWebElement location = job.FindElement(By.CssSelector("span.job-location span span"));
                Console.WriteLine($"Location: {location.Text}");

                IWebElement key_words = job.FindElement(By.CssSelector("span.job-keywords"));
                Console.WriteLine($"Key words: {key_words.Text}");

                IWebElement details = job.FindElement(By.CssSelector("a.job-title"));
                string joblink = details.GetAttribute("href");
                Console.WriteLine($"Details: {joblink}");
                Console.WriteLine("----------------");
                // creates a dictionary for each job and adding it to the list
                var jobInfo = new Dictionary<string, string>
                {
                    { "Title", title.Text },
                    { "Company", company.Text },
                    { "Location", location.Text },
                    { "KeyWords", key_words.Text },
                    { "Details", joblink }
                };

                jobDetails.Add(jobInfo);
            }

            // close the driver and return the list of job dictionaries
            driver.Quit();
            return jobDetails;
        }
        public void CSV (List<Dictionary<string, string>> data, string filePath, string fileName)
        {
            // specifies path of csv file
            string path = $"{filePath}\\{fileName}.csv";
            // open a stream writer to write to the CSV file
            using (StreamWriter writer = new StreamWriter(path))
            {
                writer.WriteLine(data);
            }
        }

        public void JSON(List<Dictionary<string, string>> data, string filePath, string fileName)
        {
            // specifies path of json file
            string path = $"{filePath}\\{fileName}.json";
            // serialize the data to JSON format and write it to the file
            string json = JsonConvert.SerializeObject(data, Formatting.Indented);
            File.WriteAllText(path, json);
        }
    }
}