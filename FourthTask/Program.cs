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
        private readonly string uniqueEmail = $"john.doe{Guid.NewGuid()}@kick.com";
        private readonly string password = "test22";
        private readonly string reviewTitle = "YOoooooooo!";
        private readonly string reviewText = "This is a crazy deal!!!";

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
            LogIn(email, password);

            driver.FindElement(By.XPath("//a[@href='/apparel-shoes']")).Click();
            driver.FindElement(By.XPath("//a[@href='/green-and-blue-sneaker']")).Click();
            driver.FindElement(By.XPath("//a[@href='/productreviews/68']")).Click();
            driver.FindElement(By.XPath("//input[@id='AddProductReview_Title']")).SendKeys(reviewTitle);
            driver.FindElement(By.XPath("//textarea[@id='AddProductReview_ReviewText']")).SendKeys(reviewText);
            driver.FindElement(By.XPath("//input[@value='Submit review']")).Click();

            TearDown();
            SetUp();
            RegisterUser("John", "Doe", uniqueEmail, password);

            driver.FindElement(By.XPath("//a[@href='/apparel-shoes']")).Click();
            driver.FindElement(By.XPath("//a[@href='/green-and-blue-sneaker']")).Click();
            driver.FindElement(By.XPath("//a[@href='/productreviews/68']")).Click();
            driver.FindElement(By.XPath($"//div[@class='product-review-list']//div[@class='product-review-item']//div[@class='review-text'][contains(text(), '{reviewText}')]/following-sibling::div[@class='product-review-helpfulness']//span[starts-with(@id, 'vote-yes-')]")).Click();
        }

        public void UserCreation()
        {
            driver.Navigate().GoToUrl("https://demowebshop.tricentis.com/");

            driver.FindElement(By.XPath("//a[@href='/login']")).Click();

            driver.FindElement(By.XPath("//input[@value='Register']")).Click();

            RegisterUser("John", "Doe", email, password);
        }

        private void LogIn(string email, string password)
        {
            driver.FindElement(By.XPath("//a[text()='Log in']")).Click();

            driver.FindElement(By.XPath("//input[@id='Email']")).SendKeys(email);

            driver.FindElement(By.XPath("//input[@id='Password']")).SendKeys(password);

            driver.FindElement(By.XPath("//input[@value='Log in']")).Click();
        }

        private void RegisterUser(string firstName, string lastName, string email, string password)
        {
            driver.FindElement(By.XPath("//a[@href='/login']")).Click();

            driver.FindElement(By.XPath("//input[@value='Register']")).Click();

            driver.FindElement(By.XPath("//input[@id='FirstName']")).SendKeys(firstName);

            driver.FindElement(By.XPath("//input[@id='LastName']")).SendKeys(lastName);

            driver.FindElement(By.XPath("//input[@id='Email']")).SendKeys(email);

            driver.FindElement(By.XPath("//div[@class='fieldset'][descendant::label[@for='Password']]//input[preceding-sibling::label[@for='Password']]")).SendKeys(password);

            driver.FindElement(By.XPath("//div[@class='fieldset'][descendant::label[@for='Password']]//input[preceding-sibling::label[@for='ConfirmPassword']]")).SendKeys(password);

            driver.FindElement(By.XPath("//div[@class='page-body']//input[@type='submit']")).Click();

            driver.FindElement(By.XPath("//div[@class='page-body']//input[@type='button' and @value='Continue']")).Click();
        }
    }
}
