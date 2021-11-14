using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using WebDriverManager;
using System;
using WebDriverManager.DriverConfigs.Impl;
using OpenQA.Selenium.Interactions;

public class AutomatedUnitTest : IDisposable
{
    private readonly ChromeDriver _driver;
    private readonly string testUsername, testPw;
    public AutomatedUnitTest()
    {
        ChromeOptions opt = new ChromeOptions();
        opt.AddArguments("--headless");
        new DriverManager().SetUpDriver(new ChromeConfig());
        Console.WriteLine("Setup");
        _driver = new ChromeDriver(opt);
        testUsername = "Admin";
        testPw = "Password12345";
    }

    [Test]
    public void Login_to_FWMS()
    {
        _driver.Navigate().GoToUrl("https://fwms-stg.herokuapp.com/Admin/Login");
        _driver.FindElement(By.Id("userName")).SendKeys(testUsername);
        _driver.FindElement(By.Id("pwdHash")).SendKeys(testPw);
        _driver.FindElement(By.Id("submitBtn")).Click();
    }

    [Test]
    public void Access_View_Donations()
    {
        _driver.Navigate().GoToUrl("https://fwms-stg.herokuapp.com/Admin/Login");
        _driver.FindElement(By.Id("userName")).SendKeys(testUsername);
        _driver.FindElement(By.Id("pwdHash")).SendKeys(testPw);
        _driver.FindElement(By.Id("submitBtn")).Click();
        _driver.Navigate().GoToUrl("https://fwms-heroku.herokuapp.com/Donations");
    }

    [Test]
    public void Create_New_Donation()
    {
        _driver.Navigate().GoToUrl("https://fwms-stg.herokuapp.com/Admin/Login");
        _driver.FindElement(By.Id("userName")).SendKeys(testUsername);
        _driver.FindElement(By.Id("pwdHash")).SendKeys(testPw);
        _driver.FindElement(By.Id("submitBtn")).Click();
        _driver.Navigate().GoToUrl("https://fwms-stg.herokuapp.com/Donations/CreateDonation");
        _driver.Manage().Window.Size = new System.Drawing.Size(1093, 694);
        _driver.FindElement(By.Id("donationName")).Click();
        _driver.FindElement(By.Id("donationName")).SendKeys("Test Buns");
        _driver.FindElement(By.Id("quantity")).SendKeys("5");
        _driver.FindElement(By.Id("ExpiryDate")).Click();
        _driver.FindElement(By.Id("ExpiryDate")).SendKeys(DateTime.Now.AddDays(1).ToString("yyyy-MM-dd HH:mm:ss"));
        IWebElement LocationSelect = _driver.FindElement(By.Name("LocationId"));
        SelectElement selectItem = new SelectElement(LocationSelect);
        selectItem.SelectByText("Four Seasons");

        IWebElement FoodSelect = _driver.FindElement(By.Name("FoodId"));
        SelectElement selectItem1 = new SelectElement(FoodSelect);
        selectItem1.SelectByText("Buns");
        _driver.FindElement(By.Id("submitBtn")).Click();
    }

    [Test]
    public void CreateCollection()
    {
        _driver.Navigate().GoToUrl("https://fwms-stg.herokuapp.com/Admin/Login");
        _driver.FindElement(By.Id("userName")).SendKeys(testUsername);
        _driver.FindElement(By.Id("pwdHash")).SendKeys(testPw);
        _driver.FindElement(By.Id("submitBtn")).Click();
        _driver.Navigate().GoToUrl("https://fwms-stg.herokuapp.com/Collections/CreateCollection");
        _driver.Manage().Window.Size = new System.Drawing.Size(1382, 744);
        _driver.FindElement(By.Id("CollectionName")).Click();
        _driver.FindElement(By.Id("CollectionName")).SendKeys("Test Collection");
        _driver.FindElement(By.Id("CollectionDate")).Click();
        _driver.FindElement(By.Id("CollectionDate")).Click();
        _driver.FindElement(By.Id("CollectionDate")).SendKeys("2021-12-31T23:05");
        _driver.FindElement(By.Id("CollectionDate")).SendKeys("2021-12-31T23:59");
        _driver.FindElement(By.CssSelector(".container-fluid > form")).Click();
        _driver.FindElement(By.Id("donationId")).Click();
        {
            var dropdown = _driver.FindElement(By.Id("donationId"));
            dropdown.FindElement(By.XPath("//option[. = 'Test Bread']")).Click();
        }
        _driver.FindElement(By.CssSelector("option:nth-child(2)")).Click();
        _driver.FindElement(By.CssSelector(".btn")).Click();
    }

    public void Dispose()
    {
        _driver.Quit();
        _driver.Dispose();
    }
}
