using NUnit.Framework;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium;
using Microsoft.VisualStudio.TestPlatform.ObjectModel;

namespace ThirdFourthTask
{
    [TestFixture]
    public class Program
    {
        private IWebDriver driver;
        private WebDriverWait wait;
        private readonly string email = $"john.doe{Guid.NewGuid()}@kick.com";
        private readonly string password = "password123";

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            IWebDriver userDriver = new ChromeDriver();
            WebDriverWait userWait = new WebDriverWait(userDriver, TimeSpan.FromSeconds(10));

            userDriver.Navigate().GoToUrl("https://demowebshop.tricentis.com/");

            userDriver.FindElement(By.XPath("//a[@href='/login']")).Click();

            userDriver.FindElement(By.XPath("//input[@value='Register']")).Click();

            userDriver.FindElement(By.XPath("//input[@id='FirstName']")).SendKeys("John");

            userDriver.FindElement(By.XPath("//input[@id='LastName']")).SendKeys("Doe");

            userDriver.FindElement(By.XPath("//input[@id='Email']")).SendKeys(email);

            userDriver.FindElement(By.XPath("//div[@class='fieldset'][descendant::label[@for='Password']]//input[preceding-sibling::label[@for='Password']]")).SendKeys(password);

            userDriver.FindElement(By.XPath("//div[@class='fieldset'][descendant::label[@for='Password']]//input[preceding-sibling::label[@for='ConfirmPassword']]")).SendKeys(password);

            userDriver.FindElement(By.XPath("//div[@class='page-body']//input[@type='submit']")).Click();

            userDriver.FindElement(By.XPath("//div[@class='page-body']//input[@type='button' and @value='Continue']")).Click();

            userDriver.Quit();
        }

        [SetUp]
        public void SetUp()
        {
            driver = new ChromeDriver();
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            driver.Navigate().GoToUrl("https://demowebshop.tricentis.com/");
        }

        [TearDown]
        public void TearDown()
        {
            driver.Manage().Cookies.DeleteAllCookies();
            driver.Quit();
            driver.Dispose();
        }

        [Test]
        public void FirstScenario()
        {
            List<string> products = File.ReadLines("../../../Data/data1.txt")
                .Select(line => line.Trim())
                .Where(line => !string.IsNullOrEmpty(line))
                .ToList();

            driver.FindElement(By.XPath("//a[text()='Log in']")).Click();

            driver.FindElement(By.XPath("//input[@id='Email']")).SendKeys(email);

            driver.FindElement(By.XPath("//input[@id='Password']")).SendKeys(password);

            driver.FindElement(By.XPath("//input[@value='Log in']")).Click();

            driver.FindElement(By.XPath("//a[@href='/digital-downloads']")).Click();

            foreach (var product in products)
            {
                wait.Until(d =>
                {
                    var element = d.FindElement(By.XPath("//div[@class='ajax-loading-block-window']"));
                    return !element.Displayed ? d.FindElement(By.XPath($"//h2[descendant::a[text()='{product}']]//following-sibling::div[@class='add-info']//input[@value='Add to cart']")) : null;
                }).Click();
            }

            driver.FindElement(By.XPath("//a[@href='/cart']")).Click();

            driver.FindElement(By.XPath("//input[@id='termsofservice']")).Click();

            driver.FindElement(By.XPath("//button[@id='checkout']")).Click();

            driver.FindElement(By.XPath("//div[@class='edit-address']//select[preceding-sibling::label[@for='BillingNewAddress_CountryId']]//option[@value='86']")).Click();

            driver.FindElement(By.XPath("//input[@id='BillingNewAddress_City']")).SendKeys("Vilnius");

            driver.FindElement(By.XPath("//input[@id='BillingNewAddress_Address1']")).SendKeys("Zirmunu 23");

            driver.FindElement(By.XPath("//input[@id='BillingNewAddress_ZipPostalCode']")).SendKeys("12345");

            driver.FindElement(By.XPath("//input[@id='BillingNewAddress_PhoneNumber']")).SendKeys("123456789");

            driver.FindElement(By.XPath("//input[@title='Continue']")).Click();

            wait.Until(d =>
            {
                var element = d.FindElement(By.XPath("//span[@id='billing-please-wait']"));
                return !element.Displayed ? d.FindElement(By.XPath("//input[@onclick='PaymentMethod.save()']")) : null;
            }).Click();

            wait.Until(d =>
            {
                var element = d.FindElement(By.XPath("//span[@id='payment-method-please-wait']"));
                return !element.Displayed ? d.FindElement(By.XPath("//input[@onclick='PaymentInfo.save()']")) : null;
            }).Click();

            wait.Until(d =>
            {
                var element = d.FindElement(By.XPath("//span[@id='payment-info-please-wait']"));
                return !element.Displayed ? d.FindElement(By.XPath("//input[@onclick='ConfirmOrder.save()']")) : null;
            }).Click();

            string order = wait.Until(d =>
            {
                IWebElement? element = null;
                try
                {
                    element = d.FindElement(By.XPath("//span[@id='confirm-order-please-wait']"));
                }
                catch (NoSuchElementException)
                {
                    return d.FindElement(By.XPath("//ul[@class='details']/li[1]"));
                }
                return !element.Displayed ? d.FindElement(By.XPath("//ul[@class='details']/li[1]")) : null;
            }).Text.Trim();

            Assert.That(order.Length, Is.GreaterThan(0));
        }

        [Test]
        public void SecondScenario()
        {
            List<string> products = File.ReadLines("../../../Data/data2.txt")
                .Select(line => line.Trim())
                .Where(line => !string.IsNullOrEmpty(line))
                .ToList();

            driver.FindElement(By.XPath("//a[text()='Log in']")).Click();

            driver.FindElement(By.XPath("//input[@id='Email']")).SendKeys(email);

            driver.FindElement(By.XPath("//input[@id='Password']")).SendKeys(password);

            driver.FindElement(By.XPath("//input[@value='Log in']")).Click();

            driver.FindElement(By.XPath("//a[@href='/digital-downloads']")).Click();

            foreach (var product in products)
            {
                wait.Until(d =>
                {
                    var element = d.FindElement(By.XPath("//div[@class='ajax-loading-block-window']"));
                    return !element.Displayed ? d.FindElement(By.XPath($"//h2[descendant::a[text()='{product}']]//following-sibling::div[@class='add-info']//input[@value='Add to cart']")) : null;
                }).Click();
            }

            driver.FindElement(By.XPath("//a[@href='/cart']")).Click();

            driver.FindElement(By.XPath("//input[@id='termsofservice']")).Click();

            driver.FindElement(By.XPath("//button[@id='checkout']")).Click();

            //driver.FindElement(By.XPath("//div[@class='edit-address']//select[preceding-sibling::label[@for='BillingNewAddress_CountryId']]//option[@value='86']")).Click();

            //driver.FindElement(By.XPath("//input[@id='BillingNewAddress_City']")).SendKeys("Vilnius");

            //driver.FindElement(By.XPath("//input[@id='BillingNewAddress_Address1']")).SendKeys("Zirmunu 23");

            //driver.FindElement(By.XPath("//input[@id='BillingNewAddress_ZipPostalCode']")).SendKeys("12345");

            //driver.FindElement(By.XPath("//input[@id='BillingNewAddress_PhoneNumber']")).SendKeys("123456789");

            driver.FindElement(By.XPath("//input[@title='Continue']")).Click();

            wait.Until(d =>
            {
                var element = d.FindElement(By.XPath("//span[@id='billing-please-wait']"));
                return !element.Displayed ? d.FindElement(By.XPath("//input[@onclick='PaymentMethod.save()']")) : null;
            }).Click();

            wait.Until(d =>
            {
                var element = d.FindElement(By.XPath("//span[@id='payment-method-please-wait']"));
                return !element.Displayed ? d.FindElement(By.XPath("//input[@onclick='PaymentInfo.save()']")) : null;
            }).Click();

            wait.Until(d =>
            {
                var element = d.FindElement(By.XPath("//span[@id='payment-info-please-wait']"));
                return !element.Displayed ? d.FindElement(By.XPath("//input[@onclick='ConfirmOrder.save()']")) : null;
            }).Click();

            string order = wait.Until(d =>
            {
                IWebElement? element = null;
                try
                {
                    element = d.FindElement(By.XPath("//span[@id='confirm-order-please-wait']"));
                }
                catch (NoSuchElementException)
                {
                    return d.FindElement(By.XPath("//ul[@class='details']/li[1]"));
                }
                return !element.Displayed ? d.FindElement(By.XPath("//ul[@class='details']/li[1]")) : null;
            }).Text.Trim();

            Assert.That(order.Length, Is.GreaterThan(0));
        }
    }
}
