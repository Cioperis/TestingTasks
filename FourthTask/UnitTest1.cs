using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;

namespace FourthTask
{
    [TestFixture]
    public class Program
    {
        private IWebDriver driver;
        private WebDriverWait wait;
        private readonly string email = "test22aaa@email.com";
        private readonly string password = "test22";

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
            driver.Dispose();
            driver.Quit();
        }

        [Test]
        public void ReviewAProduct()
        {
            driver.FindElement(By.XPath("//a[text()='Log in']")).Click();

            driver.FindElement(By.XPath("//input[@id='Email']")).SendKeys(email);

            driver.FindElement(By.XPath("//input[@id='Password']")).SendKeys(password);

            driver.FindElement(By.XPath("//input[@value='Log in']")).Click();

            driver.FindElement(By.XPath("//a[@href='/apparel-shoes']")).Click();

            driver.FindElement(By.XPath("//a[@href='/green-and-blue-sneaker']")).Click();
        }
    }
}
