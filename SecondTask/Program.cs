using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System;

internal class Program
{
    static void Main()
    {
        // Initialize Chrome WebDriver
        IWebDriver driver = new ChromeDriver();

        try
        {
            //// 1. Open the website
            //driver.Navigate().GoToUrl("https://demoqa.com/");

            //// 2. Click on "Widgets" card
            //IWebElement widgetsCard = driver.FindElement(By.XPath("//div[@class='card mt-4 top-card' and .//h5[text()='Widgets']]"));
            //widgetsCard.Click();

            //// 3. Click on "Progress Bar" in the menu
            //IWebElement widgetsElement = driver.FindElement(By.XPath("//li[./span[text()='Progress Bar']]"));
            //widgetsElement.Click();

            //// 4. Click the "Start" button
            //IWebElement startStopButton = driver.FindElement(By.XPath("//button[@id='startStopButton']"));
            //startStopButton.Click();

            //// 5. Wait until the progress bar reaches 100%
            //WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(30)); // Max wait time of 30 seconds
            //wait.Until(d => d.FindElement(By.XPath("//div[@role='progressbar']")).GetAttribute("aria-valuenow") == "100");

            //// 6. Click the "Stop" button only when progress is 100%
            //IWebElement resetButton = driver.FindElement(By.XPath("//button[@id='resetButton']"));
            //resetButton.Click();

            driver.Navigate().GoToUrl("https://demoqa.com/");

            IWebElement widgetsElements = driver.FindElement(By.XPath("//div[@class='card mt-4 top-card' and .//h5[text()='Elements']]"));
            widgetsElements.Click();

            // 3. Click on "Progress Bar" in the menu
            IWebElement widgetsTables = driver.FindElement(By.XPath("//li[./span[text()='Web Tables']]"));
            widgetsTables.Click();

            for (int i = 0; i < 8; i++)
            {
                IWebElement addButton = driver.FindElement(By.XPath("//button[@id='addNewRecordButton']"));
                addButton.Click();

                IWebElement firstNameField = driver.FindElement(By.XPath("//input[@id='firstName']"));
                firstNameField.SendKeys("Lukas");

                IWebElement lastNameField = driver.FindElement(By.XPath("//input[@id='lastName']"));
                lastNameField.SendKeys("Lukas");

                IWebElement emailField = driver.FindElement(By.XPath("//input[@id='userEmail']"));
                emailField.SendKeys("lu@lu.com");

                IWebElement ageField = driver.FindElement(By.XPath("//input[@id='age']"));
                ageField.SendKeys("12");

                IWebElement salaryField = driver.FindElement(By.XPath("//input[@id='salary']"));
                salaryField.SendKeys("123");

                IWebElement departmentField = driver.FindElement(By.XPath("//input[@id='department']"));
                departmentField.SendKeys("Vilnius");

                IWebElement submitButton = driver.FindElement(By.XPath("//button[@id='submit']"));
                submitButton.Click();
            }

            IWebElement nextButton = driver.FindElement(By.XPath("//button[text()='Next']"));
            nextButton.Click();

            IWebElement lastDeleteButton = driver.FindElement(By.XPath("(//div[@class='rt-tr-group'])[last()]//span[@title='Delete']"));
            lastDeleteButton.Click();

            System.Threading.Thread.Sleep(5000);
        }
        finally
        {
            // Close the browser
            driver.Quit();
        }
    }
}
