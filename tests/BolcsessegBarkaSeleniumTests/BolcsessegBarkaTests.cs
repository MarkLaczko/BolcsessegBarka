using System.Collections.ObjectModel;
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
    private const string _sut = "http://localhost:5174";

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

    [TestMethod]
    public void LoginPageTitleVerificationTest()
    {
        _webDriver!.Url = _sut;
        Assert.AreEqual("Bejelentkezés", _webDriver.Title);
    }
    
    [TestMethod]
    public void TestOfSuccessLogin()
    {
        LoginAsAdmin();
        
        Assert.AreEqual("Főoldal", _webDriver!.Title);
    }

    [TestMethod]
    public void TestIfAdminCanCreateUserAccount() 
    {
        LoginAsAdmin();

        Wait(ExpectedConditions.ElementIsVisible(By.LinkText("Adminisztráció")), TimeSpan.FromSeconds(10));

        Navigate("Adminisztráció")!.Click();
        SelectElement("/html/body/div[1]/div/header/nav/div/div/ul/li[3]/ul/li[1]/a", "XPath",true);

        Wait(ExpectedConditions.ElementIsVisible(By.Id("newUser")), TimeSpan.FromSeconds(10));

        SelectElement("newUser","Id",true);

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
    public void TestIfAdminCanModifyUserAccount()
    {
        LoginAsAdmin();

        Wait(ExpectedConditions.ElementIsVisible(By.LinkText("Adminisztráció")), TimeSpan.FromSeconds(10));

        Navigate("Adminisztráció")!.Click();
        SelectElement("/html/body/div[1]/div/header/nav/div/div/ul/li[3]/ul/li[1]/a", "XPath", true);
         
        Wait(ExpectedConditions.ElementIsVisible(By.CssSelector("tr:nth-child(3)>td:nth-child(6)>button")), TimeSpan.FromSeconds(10));

        SelectElement("tr:nth-child(3)>td:nth-child(6)>button", "CssSelector", true);

        var name = _webDriver!.FindElement(By.Name("name"));
        name.Clear();
        name.SendKeys("teszt");

        var password = _webDriver.FindElement(By.Name("password"));
        password.SendKeys("teszt1234");

        var passwordConfirmation = _webDriver.FindElement(By.Name("password_confirm"));
        passwordConfirmation.SendKeys("teszt1234");

        SelectElement("button[type='submit'].btn-success", "CssSelector", true);


        IWebElement text = _webDriver.FindElement(By.CssSelector("tr[tabindex='-1']:nth-child(3)>td:nth-child(3)"));
        WebDriverWait wait = new WebDriverWait(_webDriver, TimeSpan.FromSeconds(10));
        wait.Until(ExpectedConditions.TextToBePresentInElementLocated(By.CssSelector("tr[tabindex='-1']:nth-child(3)>td:nth-child(3)"), "teszt"));
        var modifiedUserName = _webDriver.FindElement(By.CssSelector("tr[tabindex='-1']:nth-child(3)>td:nth-child(3)"));
        Assert.AreEqual("teszt", modifiedUserName.Text);
    }
}