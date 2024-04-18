using System.Collections.ObjectModel;
using System.Threading;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;

namespace BolcsessegBarkaSeleniumTests;

[TestClass]
public class BolcsessegBarkaTests
{
    private WebDriver? _webDriver;
    private const string _sut = "http://localhost:5173";

    [TestInitialize]
    public void InitializeWebDriver()
    {
        var firefoxOptions = new FirefoxOptions();
        _webDriver = new FirefoxDriver(firefoxOptions);
    }

    [TestCleanup]
    public void TeardownWebDriver()
    {
        _webDriver!.Quit();
    }

    private IWebElement? Navigate(string linkName)
    {
        ReadOnlyCollection<IWebElement> navBarElements = _webDriver!.FindElements(By.CssSelector("ul.navbar-nav .nav-item .nav-link"));

        foreach (IWebElement element in navBarElements)
        {
            if (linkName == element.Text)
            {
                return element;
            }
        }

        return null;
    }

    private void LoginAsAdmin()
    {
        _webDriver!.Url = _sut + "/login";

        var email = _webDriver.FindElement(By.Name("email"));
        email.SendKeys("admin@admin.com");

        var password = _webDriver.FindElement(By.Name("password"));
        password.SendKeys("admin123");

        var submitButton = _webDriver.FindElement(By.CssSelector("button[type='submit']"));
        submitButton.Click();

        WebDriverWait wait = new WebDriverWait(_webDriver, TimeSpan.FromSeconds(10));
        wait.Until(ExpectedConditions.TitleIs("Főoldal"));
    }

    private void SelectElement(string data, string option, bool click = false)
    {
        if (option == "CssSelector" && click)
        {
            _webDriver!.FindElement(By.CssSelector($"{data}")).Click();
        }
        else if (option == "Name" && click)
        {
            _webDriver!.FindElement(By.Name($"{data}")).Click();
        }
        else if (option == "XPath" && click)
        {
            _webDriver!.FindElement(By.XPath($"{data}")).Click();
        }
        else if (option == "Id" && click)
        {
            _webDriver!.FindElement(By.Id($"{data}")).Click();
        }
        else if (option == "ClassName" && click)
        {
            _webDriver!.FindElement(By.ClassName($"{data}")).Click();
        }

        if (option == "CssSelector" && !click)
        {
            _webDriver!.FindElement(By.CssSelector($"{data}"));
        }
        else if (option == "Name" && !click)
        {
            _webDriver!.FindElement(By.Name($"{data}"));
        }
        else if (option == "XPath" && !click)
        {
            _webDriver!.FindElement(By.XPath($"{data}"));
        }
        else if (option == "Id" && !click)
        {
            _webDriver!.FindElement(By.Id($"{data}"));
        }
        else if (option == "ClassName" && !click)
        {
            _webDriver!.FindElement(By.ClassName($"{data}"));
        }
    }

    private void Wait(Func<IWebDriver, IWebElement> condition, TimeSpan timeout)
    {
        WebDriverWait wait = new WebDriverWait(_webDriver, timeout);

        wait.Until(condition);
    }

    private void Wait(Func<IWebDriver, bool> condition, TimeSpan timeout)
    {
        WebDriverWait wait = new WebDriverWait(_webDriver, timeout);

        wait.Until(condition);
    }

    [TestMethod]
    public void Test00_LoginPageTitleVerificationTest()
    {
        _webDriver!.Url = _sut;
        Assert.AreEqual("Bejelentkezés", _webDriver.Title);
    }

    [TestMethod]
    public void Test01_TestOfSuccessLogin()
    {
        LoginAsAdmin();

        Assert.AreEqual("Főoldal", _webDriver!.Title);
    }

    [TestMethod]
    public void Test02_TestIfAdminCanCreateUserAccount()
    {
        LoginAsAdmin();

        Wait(ExpectedConditions.ElementIsVisible(By.LinkText("Adminisztráció")), TimeSpan.FromSeconds(10));

        Navigate("Adminisztráció")!.Click();
        SelectElement("a[href='/user-administration']", "CssSelector", true);

        Wait(ExpectedConditions.ElementIsVisible(By.Id("newUser")), TimeSpan.FromSeconds(10));

        SelectElement("newUser", "Id", true);

        var name = _webDriver!.FindElement(By.Name("name"));
        name.SendKeys("felhasznalo");

        var email = _webDriver.FindElement(By.Name("email"));
        email.SendKeys("tesztadat@gmail.com");

        var password = _webDriver.FindElement(By.Name("password"));
        password.SendKeys("teszt1234");

        var passwordConfirmation = _webDriver.FindElement(By.Name("password_confirm"));
        passwordConfirmation.SendKeys("teszt1234");

        SelectElement("addUserButton", "Id", true);

        Wait(ExpectedConditions.ElementExists(By.CssSelector("tr:nth-child(3)")), TimeSpan.FromSeconds(10));
        var usersCount = _webDriver.FindElements(By.CssSelector("tr[tabindex='-1']"));
        Assert.AreEqual(3, usersCount.Count);
    }

    [TestMethod]
    public void Test03_TestIfAdminCanModifyUserAccount()
    {
        LoginAsAdmin();

        Wait(ExpectedConditions.ElementIsVisible(By.LinkText("Adminisztráció")), TimeSpan.FromSeconds(10));

        Navigate("Adminisztráció")!.Click();
        SelectElement("a[href='/user-administration']", "CssSelector", true);

        Wait(ExpectedConditions.ElementIsVisible(By.CssSelector("#app > div > main > div > div > div > div:nth-child(2) > div:nth-child(1) > table > tbody > tr:nth-child(3) > td:nth-child(6) > button")), TimeSpan.FromSeconds(10));

        SelectElement("#app > div > main > div > div > div > div:nth-child(2) > div:nth-child(1) > table > tbody > tr:nth-child(3) > td:nth-child(6) > button", "CssSelector", true);

        var name = _webDriver!.FindElement(By.Name("name"));
        name.Clear();
        name.SendKeys("teszt");

        var password = _webDriver.FindElement(By.Name("password"));
        password.SendKeys("teszt1234");

        var passwordConfirmation = _webDriver.FindElement(By.Name("password_confirm"));
        passwordConfirmation.SendKeys("teszt1234");

        SelectElement("button[type='submit'].btn-success", "CssSelector", true);


        IWebElement text = _webDriver.FindElement(By.CssSelector("#app > div > main > div > div > div > div:nth-child(2) > div:nth-child(1) > table > tbody > tr:nth-child(3) > td:nth-child(3)"));
        WebDriverWait wait = new WebDriverWait(_webDriver, TimeSpan.FromSeconds(10));
        wait.Until(ExpectedConditions.TextToBePresentInElementLocated(By.CssSelector("#app > div > main > div > div > div > div:nth-child(2) > div:nth-child(1) > table > tbody > tr:nth-child(3) > td:nth-child(3)"), "teszt"));
        var modifiedUserName = _webDriver.FindElement(By.CssSelector("#app > div > main > div > div > div > div:nth-child(2) > div:nth-child(1) > table > tbody > tr:nth-child(3) > td:nth-child(3)"));
        
        Assert.AreEqual("teszt", modifiedUserName.Text);
    }

    [TestMethod]
    public void Test04_TestIfAdminCanDeleteUserAccount()
    {
        LoginAsAdmin();

        Wait(ExpectedConditions.ElementIsVisible(By.LinkText("Adminisztráció")), TimeSpan.FromSeconds(10));

        Navigate("Adminisztráció")!.Click();
        SelectElement("a[href='/user-administration']", "CssSelector", true);

        Wait(ExpectedConditions.ElementIsVisible(By.CssSelector("tbody tr")), TimeSpan.FromSeconds(10));

        SelectElement("#app > div > main > div > div > div > div:nth-child(2) > div:nth-child(1) > table > tbody > tr:nth-child(3) > td:nth-child(7) > button", "CssSelector", true);
        new WebDriverWait(_webDriver, TimeSpan.FromSeconds(10)).Until(drv => drv.FindElements(By.CssSelector("tr[tabindex='-1']")).Count == 2);

        var usersCount = _webDriver!.FindElements(By.CssSelector("tr[tabindex='-1']"));
        Assert.AreEqual(2, usersCount.Count);
    }

    [TestMethod]
    public void Test05_TestIfAdminCanDeleteMultipleUserAccount()
    {
        LoginAsAdmin();

        Wait(ExpectedConditions.ElementIsVisible(By.LinkText("Adminisztráció")), TimeSpan.FromSeconds(10));

        Navigate("Adminisztráció")!.Click();
        SelectElement("a[href='/user-administration']", "CssSelector", true);

        Wait(ExpectedConditions.ElementIsVisible(By.CssSelector("tbody tr")), TimeSpan.FromSeconds(10));

        SelectElement("newUser", "Id", true);

        var name = _webDriver!.FindElement(By.Name("name"));
        name.SendKeys("felhasznalo");

        var email = _webDriver.FindElement(By.Name("email"));
        email.SendKeys("tesztadat@gmail.com");

        var password = _webDriver.FindElement(By.Name("password"));
        password.SendKeys("teszt1234");

        var passwordConfirmation = _webDriver.FindElement(By.Name("password_confirm"));
        passwordConfirmation.SendKeys("teszt1234");

        SelectElement("addUserButton", "Id", true);

        Wait(ExpectedConditions.ElementExists(By.CssSelector("tbody[role='rowgroup']>tr:nth-child(3)")), TimeSpan.FromSeconds(10));

        SelectElement("#app > div > main > div > div > div > div:nth-child(2) > div:nth-child(1) > table > tbody > tr:nth-child(2) > td:nth-child(1) > div > i", "CssSelector", true);
        SelectElement("#app > div > main > div > div > div > div:nth-child(2) > div:nth-child(1) > table > tbody > tr:nth-child(3) > td:nth-child(1) > div > i", "CssSelector", true);
        SelectElement("#app > div > main > div > div > div > div.row.mb-2 > div.col-sm-12.col-md-5.d-flex.justify-content-md-start.align-items-center.justify-content-center > button.btn.btn-danger.text-white.mt-2", "CssSelector", true);
        new WebDriverWait(_webDriver, TimeSpan.FromSeconds(10)).Until(drv => drv.FindElements(By.CssSelector("tr[tabindex='-1']")).Count == 1);

        var usersCount = _webDriver!.FindElements(By.CssSelector("tr[tabindex='-1']"));
        Assert.AreEqual(1, usersCount.Count);
    }

    [TestMethod]
    public void Test06_TestUserAdministrationPageFiltering()
    {
        LoginAsAdmin();

        Wait(ExpectedConditions.ElementIsVisible(By.LinkText("Adminisztráció")), TimeSpan.FromSeconds(10));

        Navigate("Adminisztráció")!.Click();
        SelectElement("a[href='/user-administration']", "CssSelector", true);

        Wait(ExpectedConditions.ElementIsVisible(By.CssSelector("tbody tr")), TimeSpan.FromSeconds(10));

        SelectElement("newUser", "Id", true);

        var name = _webDriver!.FindElement(By.Name("name"));
        name.SendKeys("felhasznalo");

        var email = _webDriver.FindElement(By.Name("email"));
        email.SendKeys("tesztadat@gmail.com");

        var password = _webDriver.FindElement(By.Name("password"));
        password.SendKeys("teszt1234");

        var passwordConfirmation = _webDriver.FindElement(By.Name("password_confirm"));
        passwordConfirmation.SendKeys("teszt1234");

        SelectElement("addUserButton", "Id", true);

        Wait(ExpectedConditions.ElementExists(By.CssSelector("tbody[role='rowgroup']>tr:nth-child(2)")), TimeSpan.FromSeconds(10));

        var nameFilter = _webDriver.FindElement(By.CssSelector("input[placeholder='Név...']"));
        nameFilter.SendKeys("felhasznalo");

        Assert.AreEqual(1, _webDriver!.FindElements(By.CssSelector("tr[tabindex='-1']")).Count);

        SelectElement("#app > div > main > div > div > div > div:nth-child(2) > div:nth-child(1) > table > thead > tr:nth-child(2) > th:nth-child(3) > div > button:nth-child(3)", "CssSelector", true);
        new WebDriverWait(_webDriver, TimeSpan.FromSeconds(10)).Until(drv => drv.FindElements(By.CssSelector("tr[tabindex='-1']")).Count == 2);

        Assert.AreEqual(2, _webDriver!.FindElements(By.CssSelector("tr[tabindex='-1']")).Count);

        var emailFilter = _webDriver.FindElement(By.CssSelector("input[placeholder='Email...']"));
        emailFilter.SendKeys("admin@admin.com");

        Assert.AreEqual(1, _webDriver!.FindElements(By.CssSelector("tr[tabindex='-1']")).Count);
    }

    [TestMethod]
    public void Test07_CoursePageTitleVerificationTest()
    {
        LoginAsAdmin();
        Navigate("Kurzusaim")!.Click();
        Assert.AreEqual("Kurzusok", _webDriver!.Title);
    }

    [TestMethod]
    public void Test08_TestHomePageWorkingPaginator() 
    {
        LoginAsAdmin();

        Wait(ExpectedConditions.ElementExists(By.CssSelector("#app > div > main > div > div:nth-child(2)")),TimeSpan.FromSeconds(10));

        SelectElement("a[aria-label='Next']", "CssSelector",true);
        WebDriverWait wait = new WebDriverWait(_webDriver, TimeSpan.FromSeconds(10));
        wait.Until(ExpectedConditions.TextToBePresentInElementLocated(By.CssSelector("#app > div > main > div > div:nth-child(2) > div > div:nth-child(2) > div"), "Feladat 5"));

        var expectedCardTitle = _webDriver!.FindElement(By.XPath("//*[@id=\"app\"]/div/main/div/div[1]/div/div[2]/div/div[1]/h3"));
        Assert.AreEqual("Feladat 5", expectedCardTitle.Text);
    }

    [TestMethod]
    public void Test09_TestAboutLogoutFunctionality() 
    {
        LoginAsAdmin();

        SelectElement("a.dropdown-toggle>div.user-icon","CssSelector",true);

        SelectElement("ul.dropdown-menu li:last-child>a", "CssSelector", true);

        Wait(ExpectedConditions.TitleIs("Bejelentkezés"),TimeSpan.FromSeconds(10));

        Assert.AreEqual("Bejelentkezés", _webDriver!.Title);
    }

    [TestMethod]
    public void Test10_TestLanguageSwitchFunctionality()
    {
        _webDriver!.Url = _sut + "/login";

        Wait(ExpectedConditions.ElementExists(By.CssSelector("a.dropdown-item input")),TimeSpan.FromSeconds(10));

        SelectElement("button.dropdown-toggle","CssSelector",true);

        Assert.AreEqual("Bejelentkezés",_webDriver.FindElement(By.TagName("h1")).Text);

        SelectElement("a.dropdown-item input","CssSelector",true);

        WebDriverWait wait = new WebDriverWait(_webDriver, TimeSpan.FromSeconds(10));
        wait.Until(ExpectedConditions.TextToBePresentInElementLocated(By.TagName("h1"), "Login"));

        Assert.AreEqual("Login", _webDriver.FindElement(By.CssSelector("h1")).Text);

        LoginAsAdmin();

        Wait(ExpectedConditions.ElementExists(By.TagName("h1")), TimeSpan.FromSeconds(10));

        Assert.AreEqual("Welcome admin!", _webDriver.FindElement(By.TagName("h1")).Text);
    }

    [TestMethod]
    public void Test11_TestDarkModeFunctionality()
    {
        _webDriver!.Url = _sut + "/login";

        Wait(ExpectedConditions.ElementExists(By.CssSelector("a.dropdown-item")), TimeSpan.FromSeconds(10));


        Assert.AreEqual("rgb(63, 174, 236)", _webDriver.FindElement(By.CssSelector("form.rounded-4")).GetCssValue("background-color"));

        SelectElement("button.dropdown-toggle", "CssSelector", true);
        Wait(ExpectedConditions.ElementIsVisible(By.CssSelector("i.fa-moon")), TimeSpan.FromSeconds(10));
        SelectElement("i.fa-moon", "CssSelector", true);

        Assert.AreEqual("rgb(27, 27, 27)", _webDriver.FindElement(By.CssSelector("form.rounded-4")).GetCssValue("background-color"));

        LoginAsAdmin();

        Assert.AreEqual("rgb(255, 255, 255)", _webDriver.FindElement(By.CssSelector("body")).GetCssValue("background-color"));
    }
}