using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System;

internal class Program
{
    static void Main()
    {
        IWebDriver driver = new ChromeDriver();
        WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));  

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

            //FIXME use custom wait expected condition instead of for/while loops
            // 5. Add enough elements to create a second page in the pagination.
            IWebElement addRecord = driver.FindElement(By.XPath("//button[@id='addNewRecordButton']"));
           

            wait.Until(d =>
            {
                IWebElement checkIfNewRow = d.FindElement(By.XPath("//div[@class='-pagination']/div[@class='-next']/button"));
                if (checkIfNewRow.Enabled)
                {
                    return true;
                }
                addRecord.Click();

                IWebElement firstNameField = d.FindElement(By.XPath("//input[@id='firstName']"));
                firstNameField.SendKeys("Lukas");

                IWebElement lastNameField = d.FindElement(By.XPath("//input[@id='lastName']"));
                lastNameField.SendKeys("Lukas");

                IWebElement emailField = d.FindElement(By.XPath("//input[@id='userEmail']"));
                emailField.SendKeys("lu@lu.com");

                IWebElement ageField = d.FindElement(By.XPath("//input[@id='age']"));
                ageField.SendKeys("12");

                IWebElement salaryField = d.FindElement(By.XPath("//input[@id='salary']"));
                salaryField.SendKeys("123");

                IWebElement departmentField = d.FindElement(By.XPath("//input[@id='department']"));
                departmentField.SendKeys("Vilnius");

                IWebElement submitButton = d.FindElement(By.XPath("//button[@id='submit']"));
                submitButton.Click();

                return false;
            });

            // 6. Navigate to the second page by clicking "Next."
            IWebElement next = driver.FindElement(By.XPath("//div[@class='-pagination']/div[@class='-next']/button"));
            IWebElement random = driver.FindElement(By.XPath("//div[@class='col-12 mt-4 col-md-6']"));
            random.Click();
            ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].scrollIntoView({block: 'center'});", next);
            next.Click();

            // 7. Delete an element on the second page.
            var totalPages = Convert.ToInt32(driver.FindElement(By.XPath("//div[@class='-pagination']//span[@class='-totalPages']")).Text.Trim());
            IWebElement delete = wait.Until(d =>
            {
                var element = d.FindElement(By.XPath("//div[@class='-pagination']/div[@class='-next']/button"));
                return !element.Enabled ? d.FindElement(By.XPath("//div[@class='rt-tbody']/div[@class='rt-tr-group'][1]//div[@class='action-buttons']/span[@title='Delete']")) : null;
            });
            ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].scrollIntoView(true);", delete);
            delete.Click();
        }
        finally
        {
            driver.Quit();
        }
    }
}
