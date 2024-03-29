using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;

namespace BolcsessegBarkaSeleniumTests;

[TestClass]
public class BolcsessegBarkaTests
{
    private WebDriver _webDriver;
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
        _webDriver.Quit();
    }

    [TestMethod]
    public void LoginPageTitleVerificationTest()
    {
        _webDriver.Url = _sut + "/login";
        Assert.AreEqual("Bejelentkezés", _webDriver.Title);
    }
}