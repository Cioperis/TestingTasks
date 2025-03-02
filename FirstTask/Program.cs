using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Globalization;

internal class Program
{
    static void Main()
    {
        IWebDriver driver = new ChromeDriver();

        try
        {
            // 1. Atidaryti tinklalapi
            driver.Navigate().GoToUrl("https://demowebshop.tricentis.com/");

            // 2. Spausti gift cards kairiam meniu
            IWebElement giftCardsLink = driver.FindElement(By.XPath("//a[@href='/gift-cards']"));
            giftCardsLink.Click();

            // 3. Pasirinkti prekę, kuri kainuoja daugiau nei 99
            IWebElement priceElement = driver.FindElement(By.XPath("//div[@class='product-grid']//div[@class='item-box'][.//span[@class='price actual-price'][number(normalize-space()) > 99]]//a"));
            priceElement.Click();

            // 4. Įvesti recipient name
            IWebElement recipientNameField = driver.FindElement(By.XPath("//div[@class='giftcard']//input[@class='recipient-name']"));
            recipientNameField.SendKeys("John");

            // 4. Ivesti sender name
            IWebElement senderNameField = driver.FindElement(By.XPath("//div[@class='giftcard']//input[@class='sender-name']"));
            senderNameField.SendKeys("Mock");

            // 5. Įvesti i qty 5000
            IWebElement quantityField = driver.FindElement(By.XPath("//div[@class='add-to-cart']//input[contains(@class, 'qty-input')]"));
            quantityField.Clear();
            quantityField.SendKeys("5000");

            // 6. Spausti add to cart
            IWebElement addToCart = driver.FindElement(By.XPath("//div[@class='add-to-cart']//input[@value='Add to cart']"));
            addToCart.Click();
            System.Threading.Thread.Sleep(1000);

            // 7. Spausti add to wishlist
            IWebElement addToWishlist = driver.FindElement(By.XPath("//div[@class='add-to-cart']//input[@value='Add to wishlist']"));
            addToWishlist.Click();

            // 8. Spausti Jewelry kairiam meniu
            IWebElement jewelryLink = driver.FindElement(By.XPath("//a[@href='/jewelry']"));
            jewelryLink.Click();

            // 9. Spausti create your own jewelry
            IWebElement createYourOwnJewelry = driver.FindElement(By.XPath("//div[@class='product-grid']//div[@class='item-box']//a[contains(@href, 'create-it-yourself-jewelry')]"));
            createYourOwnJewelry.Click();

            // 10. Pasirinkti reiksmes: 'Material' - 'Silver 1mm', 'Length in cm' - '80', 'Pendant' - 'Star'
            IWebElement materialDropdown = driver.FindElement(By.XPath("//div[@class='attributes']//dl//dd[1]//select"));
            IWebElement silverOption = driver.FindElement(By.XPath("//div[@class='attributes']//dl//dd[1]//select//option[contains(text(), 'Silver')]"));
            materialDropdown.Click();
            silverOption.Click();
            materialDropdown.Click();

            IWebElement lengthInCm = driver.FindElement(By.XPath("//div[@class='attributes']//dl//dd[2]//input"));
            lengthInCm.SendKeys("80");

            IWebElement starOption = driver.FindElement(By.XPath("//li[label[contains(text(), 'Star')]]//input"));
            starOption.Click();

            // 11. Ivesti i qty 26
            IWebElement quantityField1 = driver.FindElement(By.XPath("//div[@class='add-to-cart']//input[contains(@class, 'qty-input')]"));
            quantityField1.Clear();
            quantityField1.SendKeys("26");

            // 12. Spausti add to cart
            IWebElement addToCart1 = driver.FindElement(By.XPath("//div[@class='add-to-cart']//input[@value='Add to cart']"));
            addToCart1.Click();
            System.Threading.Thread.Sleep(1000);

            // 13. Spausti add to wishlist
            IWebElement addToWishlist1 = driver.FindElement(By.XPath("//div[@class='add-to-cart']//input[@value='Add to wishlist']"));
            addToWishlist1.Click();

            // 14. Spausti wishlist virsuje
            IWebElement wishlistLink = driver.FindElement(By.XPath("//a[@href='/wishlist']"));
            wishlistLink.Click();

            // 15. Abiejoms prekems paspausti add to cart checkboxus
            IWebElement cartTable = driver.FindElement(By.XPath("//div[@class='wishlist-content']//table[@class='cart']"));

            var rows = cartTable.FindElements(By.XPath(".//tr[@class='cart-item-row']"));
            foreach (var row in rows)
            {
                IWebElement checkbox = row.FindElement(By.XPath(".//td[@class='add-to-cart']//input[@type='checkbox']"));
                if (!checkbox.Selected)
                {
                    checkbox.Click();
                }
            }

            // 16. Spausti add to cart
            IWebElement addToCart2 = driver.FindElement(By.XPath("//div[@class='common-buttons']//input[@value='Add to cart']"));
            addToCart2.Click();

            System.Threading.Thread.Sleep(15000);
        }
        finally
        {
            driver.Quit();
        }
    }
}
