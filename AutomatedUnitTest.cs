using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using WebDriverManager;
using System;
using WebDriverManager.DriverConfigs.Impl;

public class AutomatedUnitTest : IDisposable
{
    private readonly ChromeDriver _driver;
    public AutomatedUnitTest()
    {
        ChromeOptions opt = new ChromeOptions();
        opt.AddArguments("--headless");
        new DriverManager().SetUpDriver(new ChromeConfig());
        Console.WriteLine("Setup");
        _driver = new ChromeDriver(opt);
    }

    [Test]
    public void Login_to_FWMS()
    {
        _driver.Navigate().GoToUrl("https://fwms-heroku.herokuapp.com/Admin/Login");
        _driver.FindElement(By.Id("userName")).SendKeys("Admin");
        _driver.FindElement(By.Id("pwdHash")).SendKeys("Password12345");
        _driver.FindElement(By.Id("submitBtn")).Click();
        Assert.Pass();
    }
    public void Dispose()
    {
        _driver.Quit();
        _driver.Dispose();
    }
}