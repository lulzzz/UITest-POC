using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System;

namespace MOF.Etimad.Monafasat.FunctionalTests.Web.UI
{
    public partial class MonafsatTenderTest : IDisposable
    {
        private readonly IWebDriver _driver;
        private readonly StaticHelper StaticHelper;

        public MonafsatTenderTest()
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
        //    ShouldAddBasicTenderData();
        //    StaticHelper.DelayForDemoVideo();
        //    string referenceNumber = ShouldAddTenderDate();
        //    StaticHelper.DelayForDemoVideo();
        //    ShouldAddTenderPlaces();
        //    StaticHelper.DelayForDemoVideo();
        //    ShouldAddTenderQTable();
        //    StaticHelper.DelayForDemoVideo();
        //    ShouldAddTenderFiles();
        //    StaticHelper.DelayForDemoVideo();
        //    ShouldSearchTenders(referenceNumber);
        //    StaticHelper.DelayForDemoVideo();
        //    ShouldSendToApprove();
        //    StaticHelper.DelayForDemoVideo();
        //    StaticHelper.DelayForDemoVideo();
        //    ShouldLogOut();
        //    StaticHelper.DelayForDemoVideo();
        //    ShouldLogIn("1054976327");



        //}

        //[Fact]
        //public void ShouldAddDirectPurchaseTender()
        //{

        //    ShouldLogIn("1002976304");

        //    StaticHelper.DelayForDemoVideo();
        //    _driver.FindElement(By.XPath("//a[@href='/Tender/Index']")).SendKeys(Keys.Enter);
        //    StaticHelper.DelayForDemoVideo();
        //    _driver.FindElement(By.XPath("//a[@href='/Tender/BasicTenderData']")).SendKeys(Keys.Enter);
        //    StaticHelper.DelayForDemoVideo();
        //    ShouldAddBasicDirectPurchaseTenderData();
        //    StaticHelper.DelayForDemoVideo();
        //    string referenceNumber = ShouldAddTenderDate();
        //    StaticHelper.DelayForDemoVideo();
        //    ShouldAddTenderPlaces();
        //    StaticHelper.DelayForDemoVideo();
        //    ShouldAddTenderQTable();
        //    StaticHelper.DelayForDemoVideo();
        //    ShouldAddTenderFiles();
        //    StaticHelper.DelayForDemoVideo();
        //    ShouldSearchTenders(referenceNumber);
        //    StaticHelper.DelayForDemoVideo();
        //    ShouldSendToApprove();
        //    StaticHelper.DelayForDemoVideo();
        //    StaticHelper.DelayForDemoVideo();
        //    ShouldLogOut();
        //    StaticHelper.DelayForDemoVideo();
        //    ShouldLogIn("1054976327");



        //}








        public void ShouldAddBasicTenderData()
        {
            _driver.FindElement(By.Id("tb_TenderName")).SendKeys("منافسه عامه حالى");
            StaticHelper.DelayForDemoVideo();
            var driver = _driver.FindElement(By.Id("dD_TenderTypeId"));
            var selectElement = new SelectElement(driver);
            StaticHelper.DelayForDemoVideo();
            selectElement.SelectByValue("9");
            StaticHelper.DelayForDemoVideo();
            _driver.FindElement(By.Id("tb_ConditionsBookletPrice")).SendKeys("900");
            StaticHelper.DelayForDemoVideo();
            var driver1 = _driver.FindElement(By.Id("dD_TechnicalOrganizationId"));
            var selectElement1 = new SelectElement(driver1);
            StaticHelper.DelayForDemoVideo();
            selectElement1.SelectByValue("1");
            StaticHelper.DelayForDemoVideo();
            var driver2 = _driver.FindElement(By.Id("dD_OffersOpeningCommitteeId"));
            var selectElement2 = new SelectElement(driver2);
            StaticHelper.DelayForDemoVideo();
            selectElement2.SelectByValue("2");
            StaticHelper.DelayForDemoVideo();
            var driver3 = _driver.FindElement(By.Id("dD_OffersCheckingCommitteeId"));
            var selectElement3 = new SelectElement(driver3);
            StaticHelper.DelayForDemoVideo();
            selectElement3.SelectByValue("3");
            StaticHelper.DelayForDemoVideo();
            var driver4 = _driver.FindElement(By.Id("offersOpeningAddressId"));
            var selectElement4 = new SelectElement(driver4);
            StaticHelper.DelayForDemoVideo();
            selectElement4.SelectByValue("1");
            StaticHelper.DelayForDemoVideo();
            var driver5 = _driver.FindElement(By.Id("dD_SpendingCategoryId"));
            var selectElement5 = new SelectElement(driver5);
            StaticHelper.DelayForDemoVideo();
            selectElement5.SelectByValue("1");
            StaticHelper.DelayForDemoVideo();
            _driver.FindElement(By.Id("Purpose")).SendKeys(" منافسه عامه حالى غرضمنافسه عامه حالى غرضمنافسه عامه حالى غرضمنافسه عامه حالى غرضمنافسه عامه حالى غرضمنافسه عامه حالى غرضمنافسه عامه حالى غرضمنافسه عامه حالى غرضمنافسه عامه حالى غرضمنافسه عامه حالى غرض");
            StaticHelper.DelayForDemoVideo();
            _driver.FindElement(By.Id("txtEstimatedValue")).SendKeys("100");
            StaticHelper.DelayForDemoVideo();
            _driver.FindElement(By.Id("btnSave")).Submit();

        }
        public void ShouldAddBasicDirectPurchaseTenderData()
        {
            _driver.FindElement(By.Id("tb_TenderName")).SendKeys("منافسه عامه حالى");
            StaticHelper.DelayForDemoVideo();
            var driver = _driver.FindElement(By.Id("dD_TenderTypeId"));
            var selectElement = new SelectElement(driver);
            StaticHelper.DelayForDemoVideo();
            selectElement.SelectByValue("10");
            StaticHelper.DelayForDemoVideo();
            //_driver.FindElement(By.Id("tb_ConditionsBookletPrice")).SendKeys("900");
            StaticHelper.DelayForDemoVideo();
            var driver1 = _driver.FindElement(By.Id("dD_TechnicalOrganizationId"));
            var selectElement1 = new SelectElement(driver1);
            StaticHelper.DelayForDemoVideo();
            selectElement1.SelectByValue("1");
            StaticHelper.DelayForDemoVideo();
            var driver2 = _driver.FindElement(By.Id("dD_OffersOpeningCommitteeId"));
            var selectElement2 = new SelectElement(driver2);
            StaticHelper.DelayForDemoVideo();
            selectElement2.SelectByValue("2");
            StaticHelper.DelayForDemoVideo();
            var driver3 = _driver.FindElement(By.Id("dD_OffersCheckingCommitteeId"));
            var selectElement3 = new SelectElement(driver3);
            StaticHelper.DelayForDemoVideo();
            selectElement3.SelectByValue("3");
            StaticHelper.DelayForDemoVideo();
            var driver4 = _driver.FindElement(By.Id("offersOpeningAddressId"));
            var selectElement4 = new SelectElement(driver4);
            StaticHelper.DelayForDemoVideo();
            selectElement4.SelectByValue("1");
            StaticHelper.DelayForDemoVideo();
            var driver5 = _driver.FindElement(By.Id("dD_SpendingCategoryId"));
            var selectElement5 = new SelectElement(driver5);
            StaticHelper.DelayForDemoVideo();
            selectElement5.SelectByValue("1");
            StaticHelper.DelayForDemoVideo();
            _driver.FindElement(By.Id("Purpose")).SendKeys(" منافسه عامه حالى غرضمنافسه عامه حالى غرضمنافسه عامه حالى غرضمنافسه عامه حالى غرضمنافسه عامه حالى غرضمنافسه عامه حالى غرضمنافسه عامه حالى غرضمنافسه عامه حالى غرضمنافسه عامه حالى غرضمنافسه عامه حالى غرض");
            StaticHelper.DelayForDemoVideo();
            _driver.FindElement(By.Id("txtEstimatedValue")).SendKeys("100");
            StaticHelper.DelayForDemoVideo();
            _driver.FindElement(By.Id("btnSave")).Submit();

        }

        public string ShouldAddTenderDate()
        {
            _driver.FindElement(By.Id("lastOfferPressentationDate")).SendKeys("29/11/1440");
            StaticHelper.DelayForDemoVideo();
            _driver.FindElement(By.Id("lastEnquiesDate")).SendKeys("28/11/1440");
            StaticHelper.DelayForDemoVideo();
            _driver.FindElement(By.Id("lastOfferPresentationTime")).SendKeys("10:30 AM");
            StaticHelper.DelayForDemoVideo();
            _driver.FindElement(By.Id("offersOpeningTime")).SendKeys("10:30 AM");
            StaticHelper.DelayForDemoVideo();
            string tenderNumber = _driver.FindElement(By.Id("hdnTenderReferenceNumber")).GetAttribute("value");
            StaticHelper.DelayForDemoVideo();
            _driver.FindElement(By.Id("btnSave")).Submit();
            return tenderNumber;
        }

        public void ShouldAddTenderPlaces()
        {
            new SelectElement(_driver.FindElement(By.Id("dD_TenderAreas"))).SelectByValue("1");
            StaticHelper.DelayForDemoVideo();
            new SelectElement(_driver.FindElement(By.Id("dD_TenderAreas"))).SelectByValue("2");
            StaticHelper.DelayForDemoVideo();
            new SelectElement(_driver.FindElement(By.Name("TenderActivitieIDs"))).SelectByValue("501");
            StaticHelper.DelayForDemoVideo();
            new SelectElement(_driver.FindElement(By.Name("TenderActivitieIDs"))).SelectByValue("502");
            StaticHelper.DelayForDemoVideo();
            _driver.FindElement(By.Id("btnSubmit22")).Submit();

        }

        public void ShouldAddTenderQTable()
        {
            _driver.FindElement(By.Name("txtNDName")).SendKeys("1");
            StaticHelper.DelayForDemoVideo();
            _driver.FindElement(By.Id("btnadd")).SendKeys(Keys.Enter);
            StaticHelper.DelayForDemoVideo();
            _driver.FindElement(By.Id("btnClassModal")).SendKeys(Keys.Enter);
            StaticHelper.DelayForDemoVideo();
            _driver.FindElement(By.Id("txtItemName")).SendKeys("Keys.Enter");
            StaticHelper.DelayForDemoVideo();
            _driver.FindElement(By.Id("txtItemQuantity")).SendKeys("123");
            StaticHelper.DelayForDemoVideo();
            _driver.FindElement(By.Id("uploadClassification")).SendKeys(Keys.Enter);
            StaticHelper.DelayForDemoVideo();
            _driver.FindElement(By.Id("btnSend")).SendKeys(Keys.Enter);


        }

        public void ShouldAddTenderFiles()
        {
            string filePath = "C:\\Docs\\7cbfd4c0-d604-11e8-9d6e-65d93a47075b (1) - Copy (1).pdf";
            StaticHelper.DelayForDemoVideo();
            _driver.FindElement(By.XPath("//input[@type='file']")).SendKeys(filePath);
            StaticHelper.DelayForDemoVideo();
            _driver.FindElement(By.Id("btnSave")).Submit();

        }

        public void ShouldSearchTenders(string referenceNumber)
        {
            _driver.FindElement(By.Id("searchBtnColaps")).SendKeys(Keys.Enter);
            StaticHelper.DelayForDemoVideo(); StaticHelper.DelayForDemoVideo();
            _driver.FindElement(By.XPath("//a[@href='#basicInfo']")).SendKeys(Keys.Enter);
            StaticHelper.DelayForDemoVideo(); StaticHelper.DelayForDemoVideo();
            _driver.FindElement(By.Id("txtReferenceNumber")).SendKeys(referenceNumber);
            StaticHelper.DelayForDemoVideo(); StaticHelper.DelayForDemoVideo();
            _driver.FindElement(By.Id("searchBtn")).SendKeys(Keys.Enter);
            StaticHelper.DelayForDemoVideo(); StaticHelper.DelayForDemoVideo();

        }

        private void ShouldSendToApprove()
        {
            var Sectable = _driver.FindElement(By.Id("TenderTable"));
            var Sectrs = Sectable.FindElements(By.TagName("tr"));
            var Sectds = Sectrs[1].FindElements(By.TagName("td"));
            StaticHelper.DelayForDemoVideo(); StaticHelper.DelayForDemoVideo();
            Sectds[13].FindElement(By.TagName("a")).SendKeys(Keys.Enter);
            StaticHelper.DelayForDemoVideo(); StaticHelper.DelayForDemoVideo();
            _driver.FindElement(By.Id("btnSendToApprove")).SendKeys(Keys.Enter);
            StaticHelper.DelayForDemoVideo(); StaticHelper.DelayForDemoVideo();
            _driver.FindElement(By.Id("btnSendToApproveConfirmation")).SendKeys(Keys.Enter);
        }

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
