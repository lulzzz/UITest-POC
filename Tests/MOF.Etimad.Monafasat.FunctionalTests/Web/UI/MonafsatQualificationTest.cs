using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;

namespace MOF.Etimad.Monafasat.FunctionalTests.Web.UI
{
    public class MonafsatQualificationTest : IDisposable
    {
        private readonly IWebDriver _driver;
        private readonly StaticHelper StaticHelper;

        public MonafsatQualificationTest()
        {
            _driver = new ChromeDriver();
            StaticHelper = new StaticHelper();
        }
        //[Fact]
        //public void ShouldAddQualification()
        //{
        //    ShouldLogIn("1083471993");
        //    _driver.FindElement(By.XPath("//a[@href='/Qualification/Index']")).SendKeys(Keys.Enter);
        //    StaticHelper.DelayForDemoVideo();
        //    _driver.FindElement(By.XPath("//a[@href='/Qualification/SavePreQualification']")).SendKeys(Keys.Enter);
        //    StaticHelper.DelayForDemoVideo();
        //    _driver.FindElement(By.Id("TenderName")).SendKeys("دعوه تاهيل تست");
        //    StaticHelper.DelayForDemoVideo();
        //    new SelectElement(_driver.FindElement(By.Id("dD_TechnicalOrganizationId"))).SelectByValue("1");
        //    StaticHelper.DelayForDemoVideo();
        //    new SelectElement(_driver.FindElement(By.Id("dD_PreQualificationCommitteeId"))).SelectByValue("13");
        //    StaticHelper.DelayForDemoVideo();
        //    _driver.FindElement(By.XPath("//a[@href='#DatesStep']")).SendKeys(Keys.Enter);




        //    _driver.FindElement(By.Id("lastOfferPressentationDate")).SendKeys("29/11/1440");
        //    StaticHelper.DelayForDemoVideo();
        //    //_driver.FindElement(By.Id("offersCheckingDate")).SendKeys("27/11/1440");
        //    //StaticHelper.DelayForDemoVideo();
        //    _driver.FindElement(By.Id("lastEnquiesDate")).SendKeys("28/11/1440");
        //    StaticHelper.DelayForDemoVideo();
        //    _driver.FindElement(By.Id("lastOfferPresentationTime")).SendKeys("10:30 AM");
        //    StaticHelper.DelayForDemoVideo();
        //    _driver.FindElement(By.Id("offersCheckingTime")).SendKeys("10:30 AM");
        //    StaticHelper.DelayForDemoVideo();



        //    StaticHelper.DelayForDemoVideo();
        //    _driver.FindElement(By.XPath("//a[@href='#RelationStep']")).SendKeys(Keys.Enter);


        //    new SelectElement(_driver.FindElement(By.Id("dD_TenderAreas"))).SelectByValue("1");
        //    StaticHelper.DelayForDemoVideo();
        //    new SelectElement(_driver.FindElement(By.Id("dD_TenderAreas"))).SelectByValue("2");
        //    StaticHelper.DelayForDemoVideo();


        //    new SelectElement(_driver.FindElement(By.Id("TenderActivitieIDs"))).SelectByValue("701");
        //    StaticHelper.DelayForDemoVideo();
        //    new SelectElement(_driver.FindElement(By.Id("TenderActivitieIDs"))).SelectByValue("702");
        //    StaticHelper.DelayForDemoVideo();



        //    StaticHelper.DelayForDemoVideo();
        //    _driver.FindElement(By.XPath("//a[@href='#attachments']")).SendKeys(Keys.Enter);
        //    StaticHelper.DelayForDemoVideo();
        //    string filePath = "C:\\Docs\\7cbfd4c0-d604-11e8-9d6e-65d93a47075b (1) - Copy (1).pdf";
        //    StaticHelper.DelayForDemoVideo();
        //    _driver.FindElement(By.XPath("//input[@type='file']")).SendKeys(filePath);
        //    StaticHelper.DelayForDemoVideo();
        //    _driver.FindElement(By.Id("btnSave")).SendKeys(Keys.Enter);
        //    StaticHelper.DelayForDemoVideo();
        //    StaticHelper.DelayForDemoVideo();
        //    StaticHelper.DelayForDemoVideo();
        //    StaticHelper.DelayForDemoVideo();
        //    StaticHelper.DelayForDemoVideo();
        //    StaticHelper.DelayForDemoVideo();
        //    StaticHelper.DelayForDemoVideo();
        //    StaticHelper.DelayForDemoVideo(); StaticHelper.DelayForDemoVideo(); StaticHelper.DelayForDemoVideo();


        //}


        public void ShouldLogIn(string userName)
        {
            _driver.Navigate().GoToUrl("http://localhost:44386/");
            StaticHelper.DelayForDemoVideo();
            StaticHelper.DelayForDemoVideo();
            _driver.FindElement(By.XPath("//a[@href='/Dashboard/Index']")).SendKeys(Keys.Enter);
            _driver.FindElement(By.Id("inputaUsername")).SendKeys(userName);
            StaticHelper.DelayForDemoVideo();
            _driver.FindElement(By.Id("inputaPassword")).SendKeys("P@ssw0rd");
            StaticHelper.DelayForDemoVideo();
            _driver.FindElement(By.ClassName("searchBtn")).Submit();
            StaticHelper.DelayForDemoVideo();
            StaticHelper.DelayForDemoVideo();
        }
        public void ShouldLogOut()
        {
            StaticHelper.DelayForDemoVideo();

            _driver.FindElement(By.XPath("//button[@data-target='#dw-s3']")).SendKeys(Keys.Enter);
            StaticHelper.DelayForDemoVideo();
            _driver.FindElement(By.XPath("//a[@href='/account/logout']")).Click();
            StaticHelper.DelayForDemoVideo();
            _driver.Navigate().GoToUrl("http://localhost:44386/");
            StaticHelper.DelayForDemoVideo();
        }


        public void Dispose()
        {
            _driver.Quit();
            _driver.Dispose();
        }
    }
}
