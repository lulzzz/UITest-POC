using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace MOF.Etimad.Monafasat.FunctionalTests.Web.UI
{
    public class MonafsatPrePlaningTest
    {
        private readonly IWebDriver _driver;
        private readonly StaticHelper StaticHelper;

        public MonafsatPrePlaningTest()
        {
            _driver = new ChromeDriver();
            StaticHelper = new StaticHelper();
        }

        //[Fact]
        //public void ShouldAddGeneralTender()
        //{

        //    ShouldLogIn("1083471993");

        //    StaticHelper.DelayForDemoVideo();
        //    _driver.FindElement(By.XPath("//a[@href='/Tender/Index']")).SendKeys(Keys.Enter);
        //    StaticHelper.DelayForDemoVideo();
        //    _driver.FindElement(By.XPath("//a[@href='/Tender/BasicTenderData']")).SendKeys(Keys.Enter);
        //    StaticHelper.DelayForDemoVideo();
        //    // ShouldAddBasicTenderData();
        //    StaticHelper.DelayForDemoVideo();
        //    //   string referenceNumber = ShouldAddTenderDate();
        //    StaticHelper.DelayForDemoVideo();
        //    //  ShouldAddTenderPlaces();
        //    StaticHelper.DelayForDemoVideo();
        //    //   ShouldAddTenderQTable();
        //    StaticHelper.DelayForDemoVideo();
        //    //  ShouldAddTenderFiles();
        //    StaticHelper.DelayForDemoVideo();
        //    //   ShouldSearchTenders(referenceNumber);
        //    StaticHelper.DelayForDemoVideo();
        //    //  ShouldSendToApprove();
        //    StaticHelper.DelayForDemoVideo();
        //    StaticHelper.DelayForDemoVideo();
        //    ShouldLogOut();
        //    StaticHelper.DelayForDemoVideo();
        //    ShouldLogIn("1054976327");
        //}

        //public void ShouldLogIn(string userName)
        //{
        //    _driver.Navigate().GoToUrl("http://localhost:44386/");
        //    StaticHelper.DelayForDemoVideo();
        //    StaticHelper.DelayForDemoVideo();
        //    _driver.FindElement(By.XPath("//a[@href='/Dashboard/Index']")).SendKeys(Keys.Enter);
        //    _driver.FindElement(By.Id("inputaUsername")).SendKeys(userName);
        //    StaticHelper.DelayForDemoVideo();
        //    _driver.FindElement(By.Id("inputaPassword")).SendKeys("P@ssw0rd");
        //    StaticHelper.DelayForDemoVideo();
        //    _driver.FindElement(By.ClassName("searchBtn")).Submit();
        //    StaticHelper.DelayForDemoVideo();
        //    StaticHelper.DelayForDemoVideo();
        //}
        //public void ShouldLogOut()
        //{
        //    StaticHelper.DelayForDemoVideo();

        //    _driver.FindElement(By.XPath("//button[@data-target='#dw-s3']")).SendKeys(Keys.Enter);
        //    StaticHelper.DelayForDemoVideo();
        //    _driver.FindElement(By.XPath("//a[@href='/account/logout']")).Click();
        //    StaticHelper.DelayForDemoVideo();
        //    _driver.Navigate().GoToUrl("http://localhost:44386/");
        //    StaticHelper.DelayForDemoVideo();
        //}
    }
}
