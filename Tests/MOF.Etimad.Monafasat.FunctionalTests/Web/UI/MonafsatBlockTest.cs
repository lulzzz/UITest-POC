using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Threading;

namespace MOF.Etimad.Monafasat.FunctionalTests.Web.UI
{
    public class MonafsatBlockTest : IDisposable
    {
        private readonly IWebDriver _driver;
        public MonafsatBlockTest()
        {
            _driver = new ChromeDriver();
        }

        //[Fact]
        //public void ShouldLoadBlockPage()
        //{
        //    _driver.Navigate().GoToUrl("http://10.14.8.61:8058/account/login?returnUrl=%2Fconnect%2Fauthorize%2Fcallback%3Fclient_id%3D002c7f4c0b1a47079a0e49be1643f739%26redirect_uri%3Dhttp%253A%252F%252Flocalhost%253A44386%252Fsignin-oidc%26response_type%3Dcode%2520id_token%2520token%26scope%3Dopenid%2520profile%2520offline_access%2520roles%2520MonafasatApi%26response_mode%3Dform_post%26nonce%3D636966344529476263.MjM5NzQxYzItYTg1ZS00YWJlLWIxNjEtZTM4NmMxMzQ1NDc3YzAxMGUwZTctNWQ0ZC00MzY5LWFiYWQtYjk5YTdjMjNjZWIw%26state%3DCfDJ8PkHGnryGnJHtuGzXgFODoOkA9T5c55eq-UMJoT_ACPaS2y464JKXHFtzs0xnXoUVbdZ5sCnqz5nTq5Eh4SzaZAAZ_Aa1Mqkx0a1uxVMDY-1W2x6d1lfPvDhtSLR_dS1373k0BRFC0X_qGEyuSG1IYMs97IRPMfadhEA_wBZ_81LVwYdLp7dxVKsGf2G1veu1feAk0qJZcjQv0AYA0WFiIWSAUGg1z5l2d-KHDK98qPGWe5uQb6LCwb-AhL-6UVwrcCHn4_nLLO66kFtynvjkojjZ35i6uFjr9mOY1aGvg2DMbgXCNOXu7EV-EHFd4YyeXPGZkaHGrcf8RVKO12RTJo%26x-client-SKU%3DID_NET%26x-client-ver%3D2.1.4.0");
        //    _driver.FindElement(By.Id("inputaUsername")).SendKeys("1081129783");
        //    DelayForDemoVideo();
        //    DelayForDemoVideo();
        //    _driver.FindElement(By.Id("inputaPassword")).SendKeys("P@ssw0rd");
        //    DelayForDemoVideo();
        //    DelayForDemoVideo();
        //    _driver.FindElement(By.ClassName("searchBtn")).Submit();
        //    DelayForDemoVideo();
        //    DelayForDemoVideo();
        //    _driver.Navigate().GoToUrl("http://localhost:44386/Block/GetAdminBlockList");
        //    DelayForDemoVideo();
        //    DelayForDemoVideo();
        //    _driver.FindElement(By.Id("add-new")).Click();
        //    DelayForDemoVideo();
        //    DelayForDemoVideo();
        //    var table = _driver.FindElement(By.Id("SupplierTable"));
        //    var trs = table.FindElements(By.TagName("tr"));
        //    var tds = trs[3].FindElements(By.TagName("td"));
        //    tds[3].FindElement(By.Id("AddSupplier")).Click();
        //    DelayForDemoVideo();
        //    DelayForDemoVideo();
        //    _driver.FindElement(By.Id("BlockReason")).SendKeys("1081129783");
        //    string CRNo = _driver.FindElement(By.Id("CommercialRegistrationNo")).GetAttribute("value"); ;
        //    DelayForDemoVideo();
        //    DelayForDemoVideo();
        //    string filePath = "C:\\Docs\\7cbfd4c0-d604-11e8-9d6e-65d93a47075b (1) - Copy (1).pdf";
        //    _driver.FindElement(By.XPath("//input[@type='file']")).SendKeys(filePath);
        //    DelayForDemoVideo();
        //    DelayForDemoVideo();
        //    _driver.FindElement(By.Id("CreateAdminBlockAsync")).Submit();
        //    DelayForDemoVideo();
        //    DelayForDemoVideo();
        //    _driver.FindElement(By.XPath("//button[@data-target='#dw-s3']")).SendKeys(Keys.Enter);
        //    DelayForDemoVideo();
        //    DelayForDemoVideo();
        //    _driver.FindElement(By.XPath("//a[@href='/account/logout']")).SendKeys(Keys.Enter);
        //    _driver.Navigate().GoToUrl("http://localhost:44386/");
        //    DelayForDemoVideo();
        //    _driver.FindElement(By.XPath("//a[@href='/Dashboard/Index']")).SendKeys(Keys.Enter);
        //    DelayForDemoVideo();
        //    ///////////////////////////////////////////////
        //    //ShouldApproveSec();
        //    _driver.FindElement(By.Id("inputaUsername")).SendKeys("1008652206");
        //    DelayForDemoVideo();
        //    _driver.FindElement(By.Id("inputaPassword")).SendKeys("P@ssw0rd");
        //    DelayForDemoVideo();
        //    _driver.FindElement(By.ClassName("searchBtn")).Submit();
        //    DelayForDemoVideo();
        //    DelayForDemoVideo();
        //    //////////////////////////////////////////////////
        //    _driver.FindElement(By.XPath("//a[@href='#SearchInBlocked']")).Click();
        //    DelayForDemoVideo();
        //    DelayForDemoVideo();
        //    _driver.FindElement(By.Id("CommercialRegistrationNo")).SendKeys(CRNo);
        //    DelayForDemoVideo();
        //    DelayForDemoVideo();
        //    DelayForDemoVideo();
        //    _driver.FindElement(By.XPath("//button[@class='btn btn-sm btn-primary']")).Click();
        //    DelayForDemoVideo();
        //    DelayForDemoVideo();
        //    var Sectable = _driver.FindElement(By.Id("SupplierTable"));
        //    var Sectrs = Sectable.FindElements(By.TagName("tr"));
        //    var Sectds = Sectrs[6].FindElements(By.TagName("td"));
        //    Sectds[4].FindElement(By.Id("ConfirmBTN")).SendKeys(Keys.Enter);
        //    DelayForDemoVideo();
        //    DelayForDemoVideo();
        //    DelayForDemoVideo();
        //    DelayForDemoVideo();
        //    DelayForDemoVideo();
        //    DelayForDemoVideo();
        //    Assert.Equal("", _driver.Title);
        //}


        //[Fact]
        //public void ShouldApproveSec()
        //{
        //    _driver.Navigate().GoToUrl("http://localhost:44386/");
        //    DelayForDemoVideo();
        //    _driver.FindElement(By.XPath("//a[@href='/Dashboard/Index']")).SendKeys(Keys.Enter);
        //    DelayForDemoVideo();
        //    _driver.FindElement(By.Id("inputaUsername")).SendKeys("1008652206");
        //    DelayForDemoVideo();
        //    _driver.FindElement(By.Id("inputaPassword")).SendKeys("P@ssw0rd");
        //    DelayForDemoVideo();
        //    _driver.FindElement(By.ClassName("searchBtn")).Submit();
        //    DelayForDemoVideo();
        //    DelayForDemoVideo();
        //    DelayForDemoVideo();
        //    DelayForDemoVideo();
        //    _driver.FindElement(By.XPath("//a[@href='#SearchInBlocked']")).Click();
        //    DelayForDemoVideo();
        //    DelayForDemoVideo();
        //    _driver.FindElement(By.Id("CommercialRegistrationNo")).SendKeys("1010046165");
        //    DelayForDemoVideo();
        //    DelayForDemoVideo();
        //    DelayForDemoVideo();
        //    _driver.FindElement(By.XPath("//button[@class='btn btn-sm btn-primary']")).Click();
        //    DelayForDemoVideo();
        //    DelayForDemoVideo();
        //    var Sectable = _driver.FindElement(By.Id("SupplierTable"));
        //    var Sectrs = Sectable.FindElements(By.TagName("tr"));
        //    var Sectds = Sectrs[6].FindElements(By.TagName("td"));
        //    Sectds[4].FindElement(By.Id("ConfirmBTN")).SendKeys(Keys.Enter);
        //    DelayForDemoVideo();
        //    DelayForDemoVideo();
        //    DelayForDemoVideo();
        //    _driver.FindElement(By.Name("ConfirmBtn")).SendKeys(Keys.Enter);
        //    DelayForDemoVideo();
        //    DelayForDemoVideo();
        //    _driver.FindElement(By.Id("SaveModel")).SendKeys(Keys.Enter);
        //    DelayForDemoVideo();
        //    DelayForDemoVideo();
        //    Assert.Equal("", _driver.Title);
        //}
        private void DelayForDemoVideo()
        {
            Thread.Sleep(1000);
        }

        public void Dispose()
        {
            _driver.Quit();
            _driver.Dispose();
        }
    }
}
