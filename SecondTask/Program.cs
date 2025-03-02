using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System;

internal class Program
{
    static void Main()
    {
        IWebDriver driver = new ChromeDriver();

        try
        {
            // 1. Open https://demoqa.com/.
            driver.Navigate().GoToUrl("https://demoqa.com/");

            // 3. Select the "Widgets" tab.
            IWebElement widgetsCard = driver.FindElement(By.XPath("//div[@class='card mt-4 top-card' and .//h5[text()='Widgets']]"));
            widgetsCard.Click();

            // 4. Choose the "Progress Bar" menu item.
            IWebElement widgetsElement = driver.FindElement(By.XPath("//li[./span[text()='Progress Bar']]"));
            widgetsElement.Click();

            // 5. Click the "Start" button
            IWebElement startStopButton = driver.FindElement(By.XPath("//button[@id='startStopButton']"));
            startStopButton.Click();

            // 6. Wait until it reaches 100% and then click "Reset."
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(30)); // Max wait time of 30 seconds
            wait.Until(d => d.FindElement(By.XPath("//div[@role='progressbar']")).GetAttribute("aria-valuenow") == "100");

            // 6. Reset
            IWebElement resetButton = driver.FindElement(By.XPath("//button[@id='resetButton']"));
            resetButton.Click();

            // 2 dalis
            // 1. Open https://demoqa.com/.
            driver.Navigate().GoToUrl("https://demoqa.com/");

            // 3. Select the "Elements" tab.
            IWebElement widgetsElements = driver.FindElement(By.XPath("//div[@class='card mt-4 top-card' and .//h5[text()='Elements']]"));
            widgetsElements.Click();

            // 4. Choose the "Web Tables" menu item.
            IWebElement widgetsTables = driver.FindElement(By.XPath("//li[./span[text()='Web Tables']]"));
            widgetsTables.Click();

            // 5. Add enough elements to create a second page in the pagination.
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

            // 6. Navigate to the second page by clicking "Next."
            IWebElement nextButton = driver.FindElement(By.XPath("//button[text()='Next']"));
            nextButton.Click();

            // 7. Delete an element on the second page.
            IWebElement lastDeleteButton = driver.FindElement(By.XPath("//span[@title='Delete']"));
            lastDeleteButton.Click();

            System.Threading.Thread.Sleep(5000);
        }
        finally
        {
            driver.Quit();
        }
    }
}
