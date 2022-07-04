using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Linq;
using System.Text;
using System.Collections.Generic;
using NUnit.Framework;
using OpenQA.Selenium.Interactions;

namespace project.TestActions // rename project to project name
{
    internal class ServicesActions
    {
        /// <summary>
        /// Navigates to the Home Page a url.
        /// </summary>
        /// <param name="driver">The Webdriver instance</param>
        /// <param name="url">Contains Home Page URL</param>
        public static void NavigateToUrl(IWebDriver driver, string url)
        {
            var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(30));
            driver.Navigate().GoToUrl(url);
            wait.Until(e => e.FindElement(By.TagName("body")).Displayed);
        }

        /// <summary>
        /// Clicks "Accept Cookies" when prompted on the site.
        /// </summary>
        /// <param name="driver">The Webdriver instance</param>
        public static void AcceptCookies(IWebDriver driver)
        {
            var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(30));

            try
            {
                var cookies = wait.Until(e => e.FindElement(By.XPath("//*[@id='acceptAllCookieLevel']")));

                // If there is a prompt to accept cookies, clicks "Accept"
                cookies.Click();
                wait.Until(e => !cookies.Displayed);
            }
            catch (WebDriverTimeoutException)
            {
                // If no prompt is shown, continues the code.
            }
        }

        /// <summary>
        /// Navigates to a certain page from the home page.
        /// </summary>
        /// <param name="driver">The Webdriver instance</param>
        public static void ClickServices(IWebDriver driver)
        {
            var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(30));
            var ServicesNavi = wait.Until(e => e.FindElement(By.CssSelector("")));
            
            // Clicks "Services" on the Navi Header
            ServicesNavi.Click();

            // Wait until expected output appears
            wait.Until(e => e.FindElement(By.CssSelector("")));
            wait.Until(e => e.Url.Contains(""));
        }

        /// <summary>
        /// Clicks on a series of logos.
        /// </summary>
        /// <param name="driver">The Webdriver instance</param>
        /// <returns>List of Accessory Brand Links</returns>
        public static List<string> AccessoryBrands(IWebDriver driver)
        {
            var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(30));
            var httplinkReturned = new List<string>();
            var paths = wait.Until(e => e.FindElements(By.XPath("")));

            foreach (var path in paths)
            {
                // Clicks each brand/logo on the page and opens them on a new tab
                path.SendKeys(Keys.Control + Keys.Enter);

                // Switches to the last tab
                driver.SwitchTo().Window(driver.WindowHandles.Last());
                wait.Until(e => e.FindElement(By.CssSelector("")));
                httplinkReturned.Add(driver.Url);
                driver.Close();
                driver.SwitchTo().Window(driver.WindowHandles[0]);
            }
            // Returns a list of all the accessory brands manufacturing/product page.
            return httplinkReturned;
        }

        /// <summary>
        /// Clicks tabs that are present on a page.
        /// </summary>
        /// <param name="driver">The Webdriver instance</param>
        /// <returns>Header element of the Tab</returns>
        public static IWebElement ClickServicesTabs(IWebDriver driver, string tabHeader, string tabTitle)
        {
            var wait = new WebDriverWait (driver, TimeSpan.FromSeconds(30));
            var Header = wait.Until(e => e.FindElement(By.XPath($"//nav/a[contains(text(), '{tabHeader}')]")));

            Header.Click();
            var Title = wait.Until(e => e.FindElement(By.XPath($"//div[@class = 'c-tab-content tabContent-shown']//strong[contains(text(), '{tabTitle}')]")));

            return Title;
        }
        
         ///<summary>
        /// Verify if each hyperlinks are correct
        /// </summary>
        /// <param name="driver">The Webdriver instance</param>
        /// <param name="tabHeader">Name of the Tab Header</param>
        /// <returns>All emails, numbers, and hyperlink redirects</returns>
        public static List<List<string>> VerifyAllTabs(IWebDriver driver, string tabHeader)
        {
            var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(30));
            var Emails = new List<string>();            
            var Numbers = new List<string>();            
            var httplinkReturned = new List<string>();
            var Header = wait.Until(e => e.FindElement(By.XPath($"//li[@class = 'e-nav_item']/a[contains(text(), '{tabHeader}')]")));
            
            Header.SendKeys(Keys.Enter);

            // Locators for those highlighted in the tab
            var EmailAddresses = driver.FindElements(By.XPath($"//strong[contains(text(),'{tabHeader}')]/parent::div//a[contains(text(),'.com')]"));
            var PhoneNumbers = driver.FindElements(By.XPath($"//strong[contains(text(),'{tabHeader}')]/parent::div//a[starts-with(@href, 'tel:+')]"));
            var Hyperlinks = driver.FindElements(By.XPath($"//strong[contains(text(),'{tabHeader}')]/parent::div//a[starts-with(@href, '/en-us/')]"));

            EmailAddresses.ToList().ForEach(elem => Emails.Add(elem.GetAttribute("innerText")));
            PhoneNumbers.ToList().ForEach(elem => Numbers.Add(elem.GetAttribute("innerText")));

            foreach (var Hyperlink in Hyperlinks)
            {
                Hyperlink.SendKeys(Keys.Control + Keys.Enter);
                driver.SwitchTo().Window(driver.WindowHandles.Last());
                wait.Until(e => e.FindElement(By.CssSelector(".hd-header_brand")));
                httplinkReturned.Add(driver.Url);
                driver.Close();
                driver.SwitchTo().Window(driver.WindowHandles[0]);
            }
            return new List<List<string>> { Emails, Numbers, httplinkReturned };
        }
        
        ///<summary>
        /// Clicks all the hyperlinks a page
        /// </summary>
        /// <param name="driver">The Webdriver instance</param>
        /// <returns>URL of the hyperlinks under Supply Chain Solutions</returns>
        public static List<string> ClickHyperlinks(IWebDriver driver)
        {
            var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(30));
            var hyperlinkReturned = new List<string>();
            var paths = wait.Until(e => e.FindElements(By.XPath("")));

            foreach (var path in paths)
            {
                // Clicks each hyperlink on the page and opens them on a new tab
                path.SendKeys(Keys.Control + Keys.Enter);

                // Switches to the last tab.
                driver.SwitchTo().Window(driver.WindowHandles.Last());
                wait.Until(e => e.FindElement(By.CssSelector(".l-content")));
                var URL = wait.Until(e => e.FindElement(By.XPath("/html")));
                hyperlinkReturned.Add(driver.Url);
                driver.Close();
                driver.SwitchTo().Window(driver.WindowHandles[0]);
            }
            // Returns a list of URLs based from the hyperlinks
            return hyperlinkReturned;
        }
        
        /// <summary>
        /// Clicks all subnavi links under a Header Navi.
        /// </summary>
        /// <param name="driver">The Webdriver instance</param>
        /// <returns>Urls of subnavi links under Support</returns>
        public static List<string> SubnaviLinks(IWebDriver driver)
        {
            var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(30));

            var httplinkReturned = new List<string>();
            var paths = wait.Until(e => e.FindElements(By.XPath("")));

            foreach (var path in paths)
            {
                var logo = wait.Until(e => e.FindElement(By.CssSelector("")));
                var SupportNavi = wait.Until(e => e.FindElement(By.XPath("//li[@class = 'hd-nav_item ']/a[contains(text(), 'Support')]")));
                var actions = new Actions(driver);

                // Clicks each subnavi link on the Services dropdown menu and opens them on a new tab
                actions.MoveToElement(logo).Perform();
                actions.MoveToElement(SupportNavi).Perform();
                wait.Until(e => e.FindElement(By.CssSelector("")));
                path.SendKeys(Keys.Control + Keys.Enter);

                // Switches to the last tab.
                driver.SwitchTo().Window(driver.WindowHandles.Last());
                wait.Until(e => e.FindElement(By.XPath("/html/body/div[1]/main/div[1]/div|/html/body/div[1]/main/div[1]/div|/html/body/div[1]/div/div[5]/div/div/div/span/table/tbody/tr/td/div")));
                var URL = wait.Until(e => e.FindElement(By.XPath("")));
                httplinkReturned.Add(URL.GetAttribute("baseURI"));
                driver.Close();
                driver.SwitchTo().Window(driver.WindowHandles[0]);
            }
            // Returns a list of URLs under the Services Header Navi.
            return httplinkReturned;
        }
    }
}
