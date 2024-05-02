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
    private const string _sut = "http://localhost";

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

        Wait(ExpectedConditions.ElementIsVisible(By.CssSelector("#app > div > main > div > div > div > div:nth-child(2) > div:nth-child(1) > table > tbody > tr:nth-child(3) > td:nth-child(6) > button > i")), TimeSpan.FromSeconds(10));

        SelectElement("#app > div > main > div > div > div > div:nth-child(2) > div:nth-child(1) > table > tbody > tr:nth-child(3) > td:nth-child(6) > button > i", "CssSelector", true);

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
        
        Wait(ExpectedConditions.ElementIsVisible(By.CssSelector("button[data-pc-name='acceptbutton']")),TimeSpan.FromSeconds(10));
        SelectElement("button[data-pc-name='acceptbutton']","CssSelector",true);
        
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
        SelectElement("button span.pi-trash", "CssSelector", true);
        
        Wait(ExpectedConditions.ElementIsVisible(By.CssSelector("button[data-pc-name='acceptbutton']")),TimeSpan.FromSeconds(10));
        SelectElement("button[data-pc-name='acceptbutton']","CssSelector",true);
        
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

        SelectElement(".dropdown-menu-end > div:nth-child(1) > li:nth-child(4) > a:nth-child(1)", "CssSelector", true);

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

        Assert.AreEqual("rgb(20, 20, 20)", _webDriver.FindElement(By.CssSelector("body")).GetCssValue("background-color"));
    }
    
    [TestMethod]
    public void Test12_TestCreateGroup()
    {
        LoginAsAdmin();
    
        Wait(ExpectedConditions.ElementIsVisible(By.LinkText("Adminisztráció")), TimeSpan.FromSeconds(10));
    
        Navigate("Adminisztráció")!.Click();
        SelectElement("a[href='/group-administration']", "CssSelector", true);
    
        Wait(ExpectedConditions.ElementIsVisible(By.Id("newGroup")), TimeSpan.FromSeconds(10));
    
        SelectElement("newGroup", "Id", true);
    
        var name = _webDriver!.FindElement(By.Name("name"));
        name.SendKeys("Új csoport");
    
        SelectElement("addGroupButton", "Id", true);
    
        Wait(ExpectedConditions.ElementExists(By.CssSelector("#app > div > main > div > div > div > div:nth-child(2) > div:nth-child(1) > table > tbody > tr:nth-child(15) > td:nth-child(3)")), TimeSpan.FromSeconds(10));
        Assert.AreEqual("Új csoport", _webDriver.FindElement(By.CssSelector("tr:last-child>td:nth-child(3)")).Text);
    }
    
    [TestMethod]
    public void Test13_TestUpdateGroup()
    {
        LoginAsAdmin();
    
        Wait(ExpectedConditions.ElementIsVisible(By.LinkText("Adminisztráció")), TimeSpan.FromSeconds(10));
    
        Navigate("Adminisztráció")!.Click();
        SelectElement("a[href='/group-administration']", "CssSelector", true);
    
        Wait(ExpectedConditions.ElementIsVisible(By.Id("newGroup")), TimeSpan.FromSeconds(10));
    
        SelectElement("#app > div > main > div > div > div > div:last-child > div:nth-child(1) > table > tbody > tr:nth-child(3) > td:nth-child(6) > button", "CssSelector", true);
    
        var name = _webDriver!.FindElement(By.Name("name"));
        name.Clear();
        name.SendKeys("Módosított csoport");
        
        SelectElement("form > div:nth-child(2) > div:nth-child(1) > table > tbody > tr:nth-child(1) > td:nth-child(1) > div > button", "CssSelector", true);
    
        SelectElement("modifyGroupButton", "Id", true);
    
        IWebElement text = _webDriver.FindElement(By.CssSelector("#app > div > main > div > div > div > div:nth-child(2) > div:nth-child(1) > table > tbody > tr:nth-child(3) > td:nth-child(3)"));
        Wait(ExpectedConditions.TextToBePresentInElementLocated(By.CssSelector("body"), "Módosított csoport"), TimeSpan.FromSeconds(10));
        var groupName = _webDriver.FindElement(By.CssSelector("#app > div > main > div > div > div > div:nth-child(2) > div:nth-child(1) > table > tbody > tr:nth-child(3) > td:nth-child(3)"));
        var groupMemberCount = _webDriver.FindElement(By.CssSelector("#app > div > main > div > div > div > div:nth-child(2) > div:nth-child(1) > table > tbody > tr:nth-child(3) > td:nth-child(4)"));
        
        Assert.AreEqual("Módosított csoport", groupName.Text);
        Assert.AreEqual("1 tag", groupMemberCount.Text);
    }
    
    [TestMethod]
    public void Test14_TestDeleteGroup()
    {
        LoginAsAdmin();
    
        Wait(ExpectedConditions.ElementIsVisible(By.LinkText("Adminisztráció")), TimeSpan.FromSeconds(10));
    
        Navigate("Adminisztráció")!.Click();
        SelectElement("a[href='/group-administration']", "CssSelector", true);
    
        Wait(ExpectedConditions.ElementIsVisible(By.Id("newGroup")), TimeSpan.FromSeconds(10));
        
        Assert.AreEqual(15, _webDriver.FindElements(By.CssSelector("tr[tabindex='-1']")).Count);

        SelectElement("#app > div > main > div > div > div > div:nth-child(2) > div:nth-child(1) > table > tbody > tr:nth-child(2) > td:nth-child(7) > button > i","CssSelector",true);
        
        Wait(ExpectedConditions.ElementIsVisible(By.CssSelector("div[role='alertdialog']")), TimeSpan.FromSeconds(10));

        SelectElement("body > div:nth-child(6) > div > div.d-flex.justify-content-end.align-items-center.gap-1.mt-2 > button.btn.btn-success > span","CssSelector",true);
        
        new WebDriverWait(_webDriver, TimeSpan.FromSeconds(10)).Until(drv => drv.FindElements(By.CssSelector("tr[tabindex='-1']")).Count == 14);
        Assert.AreEqual(14, _webDriver.FindElements(By.CssSelector("tr[tabindex='-1']")).Count);
    }

    [TestMethod]
    public void Test15_TestIfAdminCanCreateCourse()
    {
        LoginAsAdmin();
        
        Wait(ExpectedConditions.ElementIsVisible(By.LinkText("Adminisztráció")), TimeSpan.FromSeconds(10));

        Navigate("Adminisztráció")!.Click();
        SelectElement("a[href='/course-administration']", "CssSelector", true);
        
        Wait(ExpectedConditions.ElementExists(By.XPath("/html/body/div[1]/div/main/div/div/div/div[1]/div[1]/button[1]")),TimeSpan.FromSeconds(10));
        
        SelectElement("/html/body/div[1]/div/main/div/div/div/div[1]/div[1]/button[1]","XPath",true);
        
        Wait(ExpectedConditions.ElementIsVisible(By.XPath("/html/body/div[5]/div")),TimeSpan.FromSeconds(10));

        var courseName = _webDriver.FindElement(By.Name("name"));
        courseName.SendKeys("Informatika");

        var uploadFile = _webDriver.FindElement(By.ClassName("file"));
        var imagePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Images", "logo.png");
        uploadFile.SendKeys(imagePath);
        
        Thread.Sleep(20);
        
        SelectElement("addCourse","ClassName", true);
        
        Wait(ExpectedConditions.TextToBePresentInElementLocated(By.CssSelector("body"), "Informatika"), TimeSpan.FromSeconds(10));

        Assert.AreEqual("Informatika", _webDriver.FindElement(By.CssSelector("#app > div > main > div > div > div > div:nth-child(2) > div:nth-child(1) > table > tbody > tr:nth-child(3) > td:nth-child(3)")).Text);
    }

    [TestMethod]
    public void Test16_TestIfAdminCanModifyCourse()
    {
        LoginAsAdmin();
        
        Wait(ExpectedConditions.ElementIsVisible(By.LinkText("Adminisztráció")), TimeSpan.FromSeconds(10));

        Navigate("Adminisztráció")!.Click();
        SelectElement("a[href='/course-administration']", "CssSelector", true);
        
        Wait(ExpectedConditions.ElementExists(By.CssSelector(".table > tbody:nth-child(2) > tr:nth-child(1) > td:nth-child(5) > button:nth-child(1)")),TimeSpan.FromSeconds(10));
        
        SelectElement(".table > tbody:nth-child(2) > tr:nth-child(1) > td:nth-child(5) > button:nth-child(1)","CssSelector",true);

        var newCourseName = _webDriver.FindElement(By.Name("name"));
        newCourseName.Clear();
        newCourseName.SendKeys("Fizika");
        
        SelectElement("modifyCourse","ClassName",true);
        
        Wait(ExpectedConditions.TextToBePresentInElementLocated(By.CssSelector("body"),"Fizika"),TimeSpan.FromSeconds(10));
        
        Assert.AreEqual("Fizika", _webDriver.FindElement(By.CssSelector(".table > tbody:nth-child(2) > tr:nth-child(1) > td:nth-child(3)")).Text);
    }

    [TestMethod]
    public void Test17_TestIfAdminCanAssignGroupToACourse()
    {
        LoginAsAdmin();
        
        Wait(ExpectedConditions.ElementIsVisible(By.LinkText("Adminisztráció")), TimeSpan.FromSeconds(10));

        Navigate("Adminisztráció")!.Click();
        SelectElement("a[href='/course-administration']", "CssSelector", true);
        
        Wait(ExpectedConditions.ElementExists(By.CssSelector("i.fa-pen-to-square")),TimeSpan.FromSeconds(10));
        
        SelectElement("i.fa-pen-to-square","CssSelector",true);

        SelectElement("svg[data-pc-section='dropdownicon']","CssSelector",true);
        
        Wait(ExpectedConditions.ElementIsVisible(By.CssSelector("div[data-pc-section='panel']")), TimeSpan.FromSeconds(10));
        
        SelectElement("div[data-pc-name='itemcheckbox'] > input:first-child","CssSelector",true);
        
        SelectElement("svg[data-pc-section='closeicon']","CssSelector",true);
        
        Wait(ExpectedConditions.ElementToBeClickable(By.ClassName("modifyCourse")),TimeSpan.FromSeconds(10));
        
        SelectElement("modifyCourse","ClassName",true);
        
        Wait(ExpectedConditions.ElementIsVisible(By.CssSelector("div[data-pc-name='toast'] div[data-pc-section='detail']")), TimeSpan.FromSeconds(10));
        
        Navigate("Adminisztráció")!.Click();
        SelectElement("a[href='/group-administration']", "CssSelector", true);
        
        Wait(ExpectedConditions.ElementIsVisible(By.TagName("table")), TimeSpan.FromSeconds(10));
        
        Assert.AreEqual("1 kurzus", _webDriver.FindElement(By.CssSelector(".table > tbody:nth-child(2) > tr:nth-child(1) > td:nth-child(5)")).Text);
    }

    [TestMethod]
    public void Test18_TestIfAdminCanDeleteMultipleCourses()
    {
        LoginAsAdmin();
        
        Wait(ExpectedConditions.ElementIsVisible(By.LinkText("Adminisztráció")), TimeSpan.FromSeconds(10));

        Navigate("Adminisztráció")!.Click();
        SelectElement("a[href='/course-administration']", "CssSelector", true);
        
        Wait(ExpectedConditions.ElementExists(By.CssSelector("tr[tabindex='-1']")),TimeSpan.FromSeconds(10));
        
        Assert.AreEqual(3, _webDriver.FindElements(By.CssSelector("tr[tabindex='-1']")).Count);

        SelectElement("#app > div > main > div > div > div > div:nth-child(2) > div:nth-child(1) > table > tbody > tr:nth-child(2) > td:nth-child(1) > div > i","CssSelector",true);
        SelectElement("#app > div > main > div > div > div > div:nth-child(2) > div:nth-child(1) > table > tbody > tr:nth-child(3) > td:nth-child(1) > div > i","CssSelector",true);
        SelectElement("div[data-pc-section='start'] button:nth-child(2)","CssSelector",true);
        
        Wait(ExpectedConditions.ElementIsVisible(By.CssSelector("button[data-pc-name='acceptbutton']")),TimeSpan.FromSeconds(10));
        SelectElement("button[data-pc-name='acceptbutton']","CssSelector",true);
        
        new WebDriverWait(_webDriver, TimeSpan.FromSeconds(10)).Until(drv => drv.FindElements(By.CssSelector("tr[tabindex='-1']")).Count == 1);
        
        Assert.AreEqual(1, _webDriver.FindElements(By.CssSelector("tr[tabindex='-1']")).Count);
    }

    [TestMethod]
    public void Test19_TesIfAdminCanDeleteACourse()
    {
        LoginAsAdmin();
        
        Wait(ExpectedConditions.ElementIsVisible(By.LinkText("Adminisztráció")), TimeSpan.FromSeconds(10));

        Navigate("Adminisztráció")!.Click();
        SelectElement("a[href='/course-administration']", "CssSelector", true);
        
        Wait(ExpectedConditions.ElementExists(By.XPath("/html/body/div[1]/div/main/div/div/div/div[1]/div[1]/button[1]")),TimeSpan.FromSeconds(10));
        
        SelectElement("/html/body/div[1]/div/main/div/div/div/div[1]/div[1]/button[1]","XPath",true);
        
        Wait(ExpectedConditions.ElementIsVisible(By.XPath("/html/body/div[5]/div")),TimeSpan.FromSeconds(10));

        var courseName = _webDriver.FindElement(By.Name("name"));
        courseName.SendKeys("Informatika");

        var uploadFile = _webDriver.FindElement(By.ClassName("file"));
        var imagePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Images", "logo.png");
        uploadFile.SendKeys(imagePath);
        
        Thread.Sleep(200);
        
        SelectElement("addCourse","ClassName", true);
        
        Wait(ExpectedConditions.TextToBePresentInElementLocated(By.CssSelector("body"), "Informatika"), TimeSpan.FromSeconds(10));
        
        Assert.AreEqual(3, _webDriver.FindElements(By.CssSelector("tr[tabindex='-1']")).Count);
        Thread.Sleep(500);
        
        IWebElement deleteBtn = _webDriver.FindElement(By.CssSelector(".table > tbody:nth-child(2) > tr:nth-child(3) > td:nth-child(6) > button:nth-child(1)"));
        ((IJavaScriptExecutor)_webDriver).ExecuteScript("arguments[0].scrollIntoView(true);", deleteBtn);
        Thread.Sleep(2000);
        
        SelectElement(".table > tbody:nth-child(2) > tr:nth-child(3) > td:nth-child(6) > button:nth-child(1)","CssSelector",true);
        
        Wait(ExpectedConditions.ElementIsVisible(By.CssSelector("button[data-pc-name='acceptbutton']")),TimeSpan.FromSeconds(10));
        SelectElement("button[data-pc-name='acceptbutton']","CssSelector",true);
        
        new WebDriverWait(_webDriver, TimeSpan.FromSeconds(10)).Until(drv => drv.FindElements(By.CssSelector("tr[tabindex='-1']")).Count == 2);
        
        Assert.AreEqual(2, _webDriver.FindElements(By.CssSelector("tr[tabindex='-1']")).Count);
    }

    [TestMethod]
    public void Test20_AdminCanAssignUserToAGroup()
    {
        LoginAsAdmin();
        
        Wait(ExpectedConditions.ElementIsVisible(By.LinkText("Adminisztráció")), TimeSpan.FromSeconds(10));
        
        Navigate("Adminisztráció")!.Click();
        SelectElement("a[href='/group-administration']", "CssSelector", true);
        
        Wait(ExpectedConditions.ElementIsVisible(By.CssSelector("#app > div > main > div > div > div > div:nth-child(2) > div:nth-child(1) > table > tbody > tr:nth-child(1) > td:nth-child(6) > button > i")), TimeSpan.FromSeconds(10));
        SelectElement("#app > div > main > div > div > div > div:nth-child(2) > div:nth-child(1) > table > tbody > tr:nth-child(1) > td:nth-child(6) > button > i", "CssSelector", true);
        
        Wait(ExpectedConditions.ElementIsVisible(By.CssSelector("div[name='dialog'] ")), TimeSpan.FromSeconds(10));
        SelectElement("tbody > tr:nth-child(1) > td:nth-child(1) > div > button > i", "CssSelector", true);
        SelectElement("#modifyGroupButton","CssSelector",true);
        
        new WebDriverWait(_webDriver, TimeSpan.FromSeconds(10)).Until(drv => drv.FindElement(By.CssSelector("#app > div > main > div > div > div > div:nth-child(2) > div:nth-child(1) > table > tbody > tr:nth-child(1) > td:nth-child(4)")).Text == "1 tag");
        
        Assert.AreEqual("1 tag", _webDriver.FindElement(By.CssSelector("#app > div > main > div > div > div > div:nth-child(2) > div:nth-child(1) > table > tbody > tr:nth-child(1) > td:nth-child(4)")).Text);
        
        Thread.Sleep(5000);
        Navigate("Kurzusaim")!.Click();
        
        Wait(ExpectedConditions.ElementExists(By.CssSelector(".card")),TimeSpan.FromSeconds(10));
        
        Assert.AreEqual("Történelem (9.a)", _webDriver.FindElement(By.CssSelector("#app > div > main > div > div > div > div > div.card-body.text-center > h5")).Text);
    }

    [TestMethod]
    public void Test21_AdminCanCreateTopicInsideACourse()
    {
        LoginAsAdmin();
        
        Wait(ExpectedConditions.ElementIsVisible(By.LinkText("Adminisztráció")), TimeSpan.FromSeconds(10));
        Navigate("Kurzusaim")!.Click();
        Thread.Sleep(5000);
        // Wait(ExpectedConditions.ElementIsVisible(By.CssSelector("a[href='/course/1?groupName=9.a']")), TimeSpan.FromSeconds(10));
        SelectElement("a[href='/course/1?groupName=9.a']","CssSelector",true);
        
        Wait(ExpectedConditions.ElementIsVisible(By.CssSelector("#newTopic")), TimeSpan.FromSeconds(10));
        SelectElement("#newTopic","CssSelector",true);
        
        Wait(ExpectedConditions.ElementIsVisible(By.CssSelector("body > div:nth-child(7) > div")), TimeSpan.FromSeconds(10));

        var topicName = _webDriver.FindElement(By.Name("name"));
        topicName.SendKeys("1. világháború");
        
        SelectElement("div[role='dialog'] button[type='submit']","CssSelector",true);
        Wait(ExpectedConditions.TextToBePresentInElementLocated(By.CssSelector("body"), "1. világháború"), TimeSpan.FromSeconds(10));
        
        Assert.AreEqual("1. világháború", _webDriver.FindElement(By.CssSelector("div.accordion:last-child .accordion-button h2")).Text);
    }

    [TestMethod]
    public void Test22_AdminCanEditTopicInsideACourse()
    {
        LoginAsAdmin();
        
        Wait(ExpectedConditions.ElementIsVisible(By.LinkText("Kurzusaim")), TimeSpan.FromSeconds(10));
        Navigate("Kurzusaim")!.Click();
        
        Wait(ExpectedConditions.ElementIsVisible(By.CssSelector("#app > div > main > div > div > div > div > div.card-body.text-center > a")), TimeSpan.FromSeconds(10));
        SelectElement("#app > div > main > div > div > div > div > div.card-body.text-center > a","CssSelector",true);
        
        Wait(ExpectedConditions.ElementIsVisible(By.CssSelector("div.accordion:last-child .accordion-button button")), TimeSpan.FromSeconds(10));
        SelectElement("div.accordion:last-child .accordion-button button","CssSelector",true);
        
        Wait(ExpectedConditions.ElementIsVisible(By.CssSelector("div.accordion:last-child div.dropdown li:nth-child(4)")), TimeSpan.FromSeconds(10));
        SelectElement("div.accordion:last-child div.dropdown li:nth-child(4)","CssSelector",true);
        
        Wait(ExpectedConditions.ElementIsVisible(By.CssSelector("div[role='dialog']")), TimeSpan.FromSeconds(10));

        var newTopicName = _webDriver.FindElement(By.Name("name"));
        newTopicName.SendKeys("Kádár korszak");
        
        SelectElement("div[role='dialog'] button[type='submit']","CssSelector",true);
        
        Wait(ExpectedConditions.TextToBePresentInElementLocated(By.CssSelector("body"), "Kádár korszak"), TimeSpan.FromSeconds(10));
        Assert.AreEqual("Kádár korszak", _webDriver.FindElement(By.CssSelector("div.accordion:last-child .accordion-button h2")).Text);
    }

    [TestMethod]
    public void Test23_AdminCanDeleteTopicInsideACourse()
    {
        LoginAsAdmin();
        
        Wait(ExpectedConditions.ElementIsVisible(By.LinkText("Kurzusaim")), TimeSpan.FromSeconds(10));
        Navigate("Kurzusaim")!.Click();
        
        Wait(ExpectedConditions.ElementIsVisible(By.CssSelector("#app > div > main > div > div > div > div > div.card-body.text-center > a")), TimeSpan.FromSeconds(10));
        SelectElement("#app > div > main > div > div > div > div > div.card-body.text-center > a","CssSelector",true);
        
        Wait(ExpectedConditions.ElementIsVisible(By.CssSelector("div.accordion:last-child .accordion-button button")), TimeSpan.FromSeconds(10));
        SelectElement("div.accordion:last-child .accordion-button button","CssSelector",true);
        
        Wait(ExpectedConditions.ElementIsVisible(By.CssSelector("div.accordion:last-child div.dropdown li:nth-child(5)")), TimeSpan.FromSeconds(10));
        SelectElement("div.accordion:last-child div.dropdown li:nth-child(5)","CssSelector",true);
        
        Wait(ExpectedConditions.ElementIsVisible(By.CssSelector("div[role='alertdialog'] button[data-pc-name='acceptbutton']")), TimeSpan.FromSeconds(10));
        SelectElement("div[role='alertdialog'] button[data-pc-name='acceptbutton']","CssSelector",true);
        
        new WebDriverWait(_webDriver, TimeSpan.FromSeconds(10)).Until(drv => drv.FindElements(By.CssSelector("div.accordion")).Count == 1);
        Assert.AreEqual(1, _webDriver.FindElements(By.CssSelector("div.accordion")).Count);
    }

    [TestMethod]
    public void Test24_TestIfAdminCanCreateNoteInsideATopic()
    {
        LoginAsAdmin();
        
        Wait(ExpectedConditions.ElementIsVisible(By.LinkText("Kurzusaim")), TimeSpan.FromSeconds(10));
        Navigate("Kurzusaim")!.Click();
        
        Wait(ExpectedConditions.ElementIsVisible(By.CssSelector("#app > div > main > div > div > div > div > div.card-body.text-center > a")), TimeSpan.FromSeconds(10));
        SelectElement("#app > div > main > div > div > div > div > div.card-body.text-center > a","CssSelector",true);
        
        Wait(ExpectedConditions.ElementIsVisible(By.CssSelector("div.accordion:last-child .accordion-button button")), TimeSpan.FromSeconds(10));
        SelectElement("div.accordion:last-child .accordion-button button","CssSelector",true);
        
        Wait(ExpectedConditions.ElementIsVisible(By.CssSelector("div.accordion:last-child div.dropdown li:nth-child(2)")), TimeSpan.FromSeconds(10));
        SelectElement("div.accordion:last-child div.dropdown li:nth-child(2)","CssSelector",true);
        
        Wait(ExpectedConditions.ElementIsVisible(By.CssSelector("div[role='dialog']")), TimeSpan.FromSeconds(10));

        var noteTitle = _webDriver.FindElement(By.Id("notetitle"));
        noteTitle.SendKeys("Teszt Jegyzet");

        var noteText = _webDriver.FindElement(By.CssSelector("div[data-pc-name='editor'] div.ql-editor p"));
        string script = "arguments[0].innerText = arguments[1]";
        ((IJavaScriptExecutor)_webDriver).ExecuteScript(script, noteText, "Teszt");   
        
        SelectElement("saveNoteButton","Id", true);
        
        Wait(ExpectedConditions.TextToBePresentInElementLocated(By.CssSelector("body"), "Teszt Jegyzet"), TimeSpan.FromSeconds(10));
        Assert.AreEqual("Jegyzet címe: Teszt Jegyzet", _webDriver.FindElement(By.CssSelector("div.card-body")).Text);  
    }

    [TestMethod]
    public void Test25_TestIfAdminOrTeacherCanAssignGroupsToACourse()
    {
        LoginAsAdmin();
        
        Wait(ExpectedConditions.ElementIsVisible(By.LinkText("Kurzusaim")), TimeSpan.FromSeconds(10));
        Navigate("Kurzusaim")!.Click();
        
        Wait(ExpectedConditions.ElementIsVisible(By.CssSelector("#app > div > main > div > div > div > div > div.card-body.text-center > a")), TimeSpan.FromSeconds(10));
        SelectElement("#app > div > main > div > div > div > div > div.card-body.text-center > a","CssSelector",true);
        
        Wait(ExpectedConditions.ElementIsVisible(By.CssSelector("#manageCourses")), TimeSpan.FromSeconds(10));
        SelectElement("#manageCourses","CssSelector",true);
        
        Wait(ExpectedConditions.ElementIsVisible(By.CssSelector("div[role='dialog']")), TimeSpan.FromSeconds(10));
        SelectElement("body > div:nth-child(7) > div > div:nth-child(3) > div:nth-child(1) > div:nth-child(3) > svg","CssSelector",true);
        
        Wait(ExpectedConditions.ElementIsVisible(By.CssSelector("div[data-pc-section='panel']")), TimeSpan.FromSeconds(10));
       
        SelectElement("ul[role='listbox'] li:first-child input[type='checkbox']","CssSelector",true);
        SelectElement("ul[role='listbox'] li:nth-child(2) input[type='checkbox']","CssSelector",true);
        SelectElement("svg[data-pc-section='closeicon']","CssSelector",true);
        
        Thread.Sleep(2000);
        SelectElement("modifyGroups","Id",true);
        
        Thread.Sleep(3000);
        
        Navigate("Kurzusaim")!.Click();
        
        Wait(ExpectedConditions.ElementIsVisible(By.CssSelector("#app > div > main > div > div > div:nth-child(2) > div")),TimeSpan.FromSeconds(10));
        Assert.AreEqual("Történelem (Módosított csoport)",_webDriver.FindElement(By.CssSelector("#app > div > main > div > div > div:nth-child(2) > div > div.card-body.text-center > h5")).Text);
    }
    
    [TestMethod]
    public void Test26_TestIfAdminCanEditNoteInsideATopic()
    {
        LoginAsAdmin();
        
        Wait(ExpectedConditions.ElementIsVisible(By.LinkText("Kurzusaim")), TimeSpan.FromSeconds(10));
        Navigate("Kurzusaim")!.Click();
        
        Wait(ExpectedConditions.ElementIsVisible(By.CssSelector("#app > div > main > div > div > div > div > div.card-body.text-center > a")), TimeSpan.FromSeconds(10));
        SelectElement("#app > div > main > div > div > div > div > div.card-body.text-center > a","CssSelector",true);
        
        Wait(ExpectedConditions.ElementIsVisible(By.CssSelector(".accordion")), TimeSpan.FromSeconds(10));
        SelectElement(".accordion-button","CssSelector",true);
        
        Wait(ExpectedConditions.ElementIsVisible(By.CssSelector(".accordion-body .row:first-child button")), TimeSpan.FromSeconds(10));
        SelectElement(".accordion-body .row:first-child button","CssSelector",true);
        
        Wait(ExpectedConditions.ElementIsVisible(By.CssSelector("div[role='dialog']")), TimeSpan.FromSeconds(10));
        
        var noteTitle = _webDriver.FindElement(By.Id("title"));
        noteTitle.Clear();
        noteTitle.SendKeys("Teszt Jegyzet2");
        
        SelectElement("modifyNoteButton","Id", true);
        
        Wait(ExpectedConditions.TextToBePresentInElementLocated(By.CssSelector("body"), "Teszt Jegyzet2"), TimeSpan.FromSeconds(10));
        Assert.AreEqual("Jegyzet címe: Teszt Jegyzet2", _webDriver.FindElement(By.CssSelector("div.card-body")).Text);  
    }
    
    [TestMethod]
    public void Test27_TestIfAdminCanDeleteNoteInsideATopic()
    {
        LoginAsAdmin();
        
        Wait(ExpectedConditions.ElementIsVisible(By.LinkText("Kurzusaim")), TimeSpan.FromSeconds(10));
        Navigate("Kurzusaim")!.Click();
        
        Wait(ExpectedConditions.ElementIsVisible(By.CssSelector("#app > div > main > div > div > div > div > div.card-body.text-center > a")), TimeSpan.FromSeconds(10));
        SelectElement("#app > div > main > div > div > div > div > div.card-body.text-center > a","CssSelector",true);
        
        Wait(ExpectedConditions.ElementIsVisible(By.CssSelector(".accordion")), TimeSpan.FromSeconds(10));
        SelectElement(".accordion-button","CssSelector",true);
        
        Wait(ExpectedConditions.ElementIsVisible(By.CssSelector(".accordion-body .row:first-child button")), TimeSpan.FromSeconds(10));
        SelectElement(".accordion-body .row:first-child button","CssSelector",true);
        
        Wait(ExpectedConditions.ElementIsVisible(By.CssSelector("div[role='dialog']")), TimeSpan.FromSeconds(10));
        
        SelectElement("deleteNoteButton","Id", true);
        
        Wait(ExpectedConditions.ElementIsVisible(By.CssSelector("div[role='alertdialog']")), TimeSpan.FromSeconds(10));;
        SelectElement("button[data-pc-name='acceptbutton']","CssSelector",true);
        
        new WebDriverWait(_webDriver, TimeSpan.FromSeconds(10)).Until(drv => drv.FindElements(By.CssSelector(".accordion .card")).Count == 1);
        Assert.AreEqual(1, _webDriver.FindElements(By.CssSelector(".accordion .card")).Count);  
    }
    
    [TestMethod]
    public void Test28_CreateQuizTest()
    {
        LoginAsAdmin();
        
        Wait(ExpectedConditions.ElementIsVisible(By.LinkText("Kurzusaim")), TimeSpan.FromSeconds(10));
        Navigate("Kurzusaim")!.Click();
        
        Wait(ExpectedConditions.ElementIsVisible(By.XPath("//*[@id=\"app\"]/div/main/div/div/div/div/div[2]/a")), TimeSpan.FromSeconds(10));
        SelectElement("//*[@id=\"app\"]/div/main/div/div/div/div/div[2]/a","XPath",true);
        
        Wait(ExpectedConditions.ElementIsVisible(By.XPath("//*[@id=\"heading1\"]/button/div/button")), TimeSpan.FromSeconds(10));
        SelectElement("//*[@id=\"heading1\"]/button/div/button","XPath",true);
        
        Wait(ExpectedConditions.ElementIsVisible(By.CssSelector("#heading1 > button > div > ul > li:nth-child(3) > button")), TimeSpan.FromSeconds(10));
        SelectElement("#heading1 > button > div > ul > li:nth-child(3) > button","CssSelector",true);
        
        Wait(ExpectedConditions.ElementIsVisible(By.Name("name")), TimeSpan.FromSeconds(10));
        
        var name = _webDriver.FindElement(By.Name("name"));
        name.SendKeys("Teszt Kvíz");
        
        var attempts = _webDriver.FindElement(By.Name("max_attempts"));
        attempts.SendKeys("1");
        
        var time = _webDriver.FindElement(By.Name("time"));
        time.SendKeys("60");
        
        SelectElement("submit","Id",true);
        
        Wait(ExpectedConditions.ElementIsVisible(By.Id("name")), TimeSpan.FromSeconds(10));
        
        var nameEdit = _webDriver.FindElement(By.Name("name"));
        Assert.AreEqual("Teszt Kvíz", nameEdit.GetAttribute("value"));
        
        var attemptsEdit = _webDriver.FindElement(By.Name("max_attempts"));
        Assert.AreEqual("1", attemptsEdit.GetAttribute("value"));
        
        var timeEdit = _webDriver.FindElement(By.Name("time"));
        Assert.AreEqual("60", timeEdit.GetAttribute("value"));
    }
    
    [TestMethod]
    public void Test29_EditQuizTest()
    {
        LoginAsAdmin();
        
        Wait(ExpectedConditions.ElementIsVisible(By.LinkText("Kurzusaim")), TimeSpan.FromSeconds(10));
        Navigate("Kurzusaim")!.Click();
        
        Wait(ExpectedConditions.ElementIsVisible(By.XPath("//*[@id=\"app\"]/div/main/div/div/div/div/div[2]/a")), TimeSpan.FromSeconds(10));
        SelectElement("//*[@id=\"app\"]/div/main/div/div/div/div/div[2]/a","XPath",true);
        
        Wait(ExpectedConditions.ElementIsVisible(By.XPath("//*[@id=\"heading1\"]/button")), TimeSpan.FromSeconds(10));
        SelectElement("//*[@id=\"heading1\"]/button","XPath",true);
        
        Wait(ExpectedConditions.ElementIsVisible(By.XPath("//*[@id=\"collapse1\"]/div/div[2]/div[1]/div/div[3]/div/button[2]")), TimeSpan.FromSeconds(10));
        SelectElement("//*[@id=\"collapse1\"]/div/div[2]/div[1]/div/div[3]/div/button[2]","XPath",true);
        
        Wait(ExpectedConditions.ElementIsVisible(By.Name("name")), TimeSpan.FromSeconds(10));
        
        var name = _webDriver.FindElement(By.Name("name"));
        name.Clear();
        name.SendKeys("Kvíz Teszt");
        
        var attempts = _webDriver.FindElement(By.Name("max_attempts"));
        attempts.Clear();
        attempts.SendKeys("2");
        
        var time = _webDriver.FindElement(By.Name("time"));
        time.Clear();
        time.SendKeys("30");
        
        SelectElement("submit","Id",true);
        
        Wait(ExpectedConditions.ElementIsVisible(By.Id("name")), TimeSpan.FromSeconds(10));
        
        var nameEdit = _webDriver.FindElement(By.Name("name"));
        Assert.AreEqual("Kvíz Teszt", nameEdit.GetAttribute("value"));
        
        var attemptsEdit = _webDriver.FindElement(By.Name("max_attempts"));
        Assert.AreEqual("2", attemptsEdit.GetAttribute("value"));
        
        var timeEdit = _webDriver.FindElement(By.Name("time"));
        Assert.AreEqual("30", timeEdit.GetAttribute("value"));
    }
    [TestMethod]
    public void Test30_AddTaskToQuizTest()
    {
        LoginAsAdmin();
        
        Wait(ExpectedConditions.ElementIsVisible(By.LinkText("Kurzusaim")), TimeSpan.FromSeconds(10));
        Navigate("Kurzusaim")!.Click();
        
        Wait(ExpectedConditions.ElementIsVisible(By.XPath("//*[@id=\"app\"]/div/main/div/div/div/div/div[2]/a")), TimeSpan.FromSeconds(10));
        SelectElement("//*[@id=\"app\"]/div/main/div/div/div/div/div[2]/a","XPath",true);
        
        Wait(ExpectedConditions.ElementIsVisible(By.XPath("//*[@id=\"heading1\"]/button")), TimeSpan.FromSeconds(10));
        SelectElement("//*[@id=\"heading1\"]/button","XPath",true);
        
        Wait(ExpectedConditions.ElementIsVisible(By.XPath("//*[@id=\"collapse1\"]/div/div[2]/div[1]/div/div[3]/div/button[2]")), TimeSpan.FromSeconds(10));
        SelectElement("//*[@id=\"collapse1\"]/div/div[2]/div[1]/div/div[3]/div/button[2]","XPath",true);
        
        Wait(ExpectedConditions.ElementIsVisible(By.CssSelector("#app > div > main > div > div > div:nth-child(2) > div:last-child > a")), TimeSpan.FromSeconds(10));
        SelectElement("#app > div > main > div > div > div:nth-child(2) > div:last-child > a","CssSelector",true);
        
        Wait(ExpectedConditions.ElementIsVisible(By.Name("header")), TimeSpan.FromSeconds(10));
        
        var header = _webDriver.FindElement(By.CssSelector("#header"));
        header.Clear();
        header.SendKeys("A feladat a teszteléssel kapcsolatos.");
        
        var text = _webDriver.FindElement(By.CssSelector("#text"));
        text.Clear();
        text.SendKeys("A források és ismeretei alapján ne oldja meg a feladatokat!");
        
        SelectElement("addSubtask","Id",true);
        
        SelectElement("#question0 > div.ql-toolbar.ql-snow > span:nth-child(2) > button.ql-bold","CssSelector",true);
        var task1Text = _webDriver.FindElement(By.CssSelector("#question0 > div.ql-container.ql-snow > div.ql-editor.ql-blank > p"));
        string script = "arguments[0].innerText = arguments[1]";
        ((IJavaScriptExecutor)_webDriver).ExecuteScript(script, task1Text, "a) Igen vagy nem?");
        
        var task1Solution = _webDriver.FindElement(By.CssSelector("#solution0 > ul > li > input"));
        task1Solution.Clear();
        task1Solution.SendKeys("Igen");
        task1Solution.SendKeys(Keys.Enter);
        
        ((IJavaScriptExecutor)_webDriver).ExecuteScript($"window.scrollTo(0, document.body.scrollHeight)");
        SelectElement("#form > div:nth-child(3) > div > div > div:nth-child(4) > p > i","CssSelector",true);
        
        var task1Marks = _webDriver.FindElement(By.CssSelector("#marks0"));
        task1Marks.Clear();
        task1Marks.SendKeys("1");
        
        ((IJavaScriptExecutor)_webDriver).ExecuteScript($"window.scrollTo(0, document.body.scrollHeight)");
        SelectElement("#form > div:nth-child(3) > div > div > div:nth-child(6) > div","CssSelector",true);
        
        SelectElement("submit","Id",true);
        
        Wait(ExpectedConditions.ElementIsVisible(By.CssSelector("#app > div > main > div > div > div:nth-child(2) > div:nth-child(2) > span.badge.text-bg-info.text-white.fs-5")), TimeSpan.FromSeconds(10));
        ReadOnlyCollection<IWebElement> elements = _webDriver.FindElements(By.CssSelector("#app > div > main > div > div > div:nth-child(2)"));
        IWebElement headerDone = elements.ElementAt(elements.Count - 1);
        Assert.IsTrue(headerDone.Text.Contains("A feladat a teszteléssel kapcsolatos."));
    }
    
    [TestMethod]
    public void Test31_EditTaskInQuizTest()
    {
        LoginAsAdmin();
        
        Wait(ExpectedConditions.ElementIsVisible(By.LinkText("Kurzusaim")), TimeSpan.FromSeconds(10));
        Navigate("Kurzusaim")!.Click();
        
        Wait(ExpectedConditions.ElementIsVisible(By.XPath("//*[@id=\"app\"]/div/main/div/div/div/div/div[2]/a")), TimeSpan.FromSeconds(10));
        SelectElement("//*[@id=\"app\"]/div/main/div/div/div/div/div[2]/a","XPath",true);
        
        Wait(ExpectedConditions.ElementIsVisible(By.XPath("//*[@id=\"heading1\"]/button")), TimeSpan.FromSeconds(10));
        SelectElement("//*[@id=\"heading1\"]/button","XPath",true);
        
        Wait(ExpectedConditions.ElementIsVisible(By.XPath("//*[@id=\"collapse1\"]/div/div[2]/div[1]/div/div[3]/div/button[2]")), TimeSpan.FromSeconds(10));
        SelectElement("//*[@id=\"collapse1\"]/div/div[2]/div[1]/div/div[3]/div/button[2]","XPath",true);
        
        Wait(ExpectedConditions.ElementIsVisible(By.CssSelector("#app > div > main > div > div > div:nth-child(2) > div:nth-child(2) > div > button:nth-child(2)")), TimeSpan.FromSeconds(10));
        SelectElement("#app > div > main > div > div > div:nth-child(2) > div:nth-child(2) > div > button:nth-child(2)","CssSelector",true);
        
        Wait(ExpectedConditions.ElementIsVisible(By.Name("header")), TimeSpan.FromSeconds(10));
        
        var header = _webDriver.FindElement(By.CssSelector("#header"));
        header.Clear();
        header.SendKeys("Ez egy teszt!");
        
        var text = _webDriver.FindElement(By.CssSelector("#text"));
        text.Clear();
        text.SendKeys("A források és ismeretei alapján ne oldja meg a feladatokat!");
        
        ((IJavaScriptExecutor)_webDriver).ExecuteScript($"window.scrollTo(0, document.body.scrollHeight)");
        Thread.Sleep(1000);
        SelectElement("submit","Id",true);
        
        Wait(ExpectedConditions.ElementIsVisible(By.CssSelector("#app > div > main > div > div > div:nth-child(2) > div:nth-child(2) > span.badge.text-bg-info.text-white.fs-5")), TimeSpan.FromSeconds(10));
        ReadOnlyCollection<IWebElement> elements = _webDriver.FindElements(By.CssSelector("#app > div > main > div > div > div:nth-child(2)"));
        IWebElement headerDone = elements.ElementAt(0);
        Assert.IsTrue(headerDone.Text.Contains("Ez egy teszt!"));
    }
}