namespace AppTest;

[TestClass]
public class AppTests
{
    private readonly int _sectionSystemId = 0;
    private readonly int _sectionCpuId = 1;
    private readonly int _sectionDisplayId = 2;
    private readonly int _sectionNetworkId = 3;
    private readonly int _sectionBatteryId = 4;
    private readonly int _sectionAndroidId = 5;
    private readonly int _sectionDevicesId = 6;
    private readonly int _sectionThermalId = 7;

    private static AppiumDriver<AppiumWebElement> _driver;

    private static string _host = "http://127.0.0.1:4723/wd/hub";

    [ClassInitialize]
    public static void DriverInitMethod(TestContext context)
    {
        var driverOptions = new AppiumOptions();
        driverOptions.AddAdditionalCapability("platformName", "Android");
        driverOptions.AddAdditionalCapability("deviceName", "emulator-5554");
        driverOptions.AddAdditionalCapability("app", @"C:\Users\HP\Downloads\aida64.apk");
        _driver = new AndroidDriver<AppiumWebElement>(new Uri(_host), driverOptions);
        Assert.IsNotNull(_driver);
        Assert.IsNotNull(_driver.Context);
    }

    [ClassCleanup]
    public static void DriverFiniMethod()
    {
        if (_driver != null)
            _driver.Dispose();
    }
    
    [TestMethod]
    public void AidaTest1()
    {
        var expectedNames = new string[]
        {
            "System",
            "CPU",
            "Display",
            "Network",
            "Battery",
            "Android",
            "Devices",
            "Thermal",
            "Sensors",
            "Apps",
            "Codecs",
            "Directories",
            "System Files",
            "About"
        };
        var sections = this.RetrieveSections();
        Assert.AreEqual(expectedNames.Length, sections.Count);
        for (int i = 0; i < expectedNames.Length; i++)
            Assert.AreEqual(expectedNames[i], sections[i].Text);
    }

    [TestMethod]
    public void AidaTest2()
    {
        var expectedSectionName = "System";
        var sections = this.RetrieveSections();
        sections[this._sectionSystemId].Click();
        Thread.Sleep(1500);
        var sectionLabel = _driver.FindElement(
            By.Id("com.finalwire.aida64:id/actCaption"));
        var retrievedSectionName = sectionLabel.Text;
        Assert.AreEqual(expectedSectionName, retrievedSectionName);
        this.SwitchBack();
    }

    [TestMethod]
    public void AidaTest3()
    {
        var expectedSectionName = "CPU";
        var sections = this.RetrieveSections();
        sections[this._sectionCpuId].Click();
        Thread.Sleep(1500);
        var sectionLabel = _driver.FindElement(
            By.Id("com.finalwire.aida64:id/actCaption"));
        var retrievedSectionName = sectionLabel.Text;
        Assert.AreEqual(expectedSectionName, retrievedSectionName);
        this.SwitchBack();
    }

    [TestMethod]
    public void AidaTest4()
    {
        var expectedSectionName = "Display";
        var sections = this.RetrieveSections();
        sections[this._sectionDisplayId].Click();
        Thread.Sleep(1500);
        var sectionLabel = _driver.FindElement(
            By.Id("com.finalwire.aida64:id/actCaption"));
        var retrievedSectionName = sectionLabel.Text;
        Assert.AreEqual(expectedSectionName, retrievedSectionName);
        this.SwitchBack();
    }

    [TestMethod]
    public void AidaTest5()
    {
        var expectedSectionName = "Network";
        var sections = this.RetrieveSections();
        sections[this._sectionNetworkId].Click();
        Thread.Sleep(1500);
        var sectionLabel = _driver.FindElement(
            By.Id("com.finalwire.aida64:id/actCaption"));
        var retrievedSectionName = sectionLabel.Text;
        Assert.AreEqual(expectedSectionName, retrievedSectionName);
        this.SwitchBack();
    }

    [TestMethod]
    public void AidaTest6()
    {
        var expectedSectionName = "Battery";
        var sections = this.RetrieveSections();
        sections[this._sectionBatteryId].Click();
        Thread.Sleep(1500);
        var sectionLabel = _driver.FindElement(
            By.Id("com.finalwire.aida64:id/actCaption"));
        var retrievedSectionName = sectionLabel.Text;
        Assert.AreEqual(expectedSectionName, retrievedSectionName);
        this.SwitchBack();
    }

    [TestMethod]
    public void AidaTest7()
    {
        var expectedSectionName = "Android";
        var sections = this.RetrieveSections();
        sections[this._sectionAndroidId].Click();
        Thread.Sleep(1500);
        var sectionLabel = _driver.FindElement(
            By.Id("com.finalwire.aida64:id/actCaption"));
        var retrievedSectionName = sectionLabel.Text;
        Assert.AreEqual(expectedSectionName, retrievedSectionName);
        this.SwitchBack();
    }

    [TestMethod]
    public void AidaTest8()
    {
        var expectedSectionName = "Devices";
        var sections = this.RetrieveSections();
        sections[this._sectionDevicesId].Click();
        Thread.Sleep(1500);
        var sectionLabel = _driver.FindElement(
            By.Id("com.finalwire.aida64:id/actCaption"));
        var retrievedSectionName = sectionLabel.Text;
        Assert.AreEqual(expectedSectionName, retrievedSectionName);
        this.SwitchBack();
    }

    [TestMethod]
    public void AidaTest9()
    {
        var expectedSectionName = "Thermal";
        var sections = this.RetrieveSections();
        sections[this._sectionThermalId].Click();
        Thread.Sleep(1500);
        var sectionLabel = _driver.FindElement(
            By.Id("com.finalwire.aida64:id/actCaption"));
        var retrievedSectionName = sectionLabel.Text;
        Assert.AreEqual(expectedSectionName, retrievedSectionName);
        this.SwitchBack();
    }

    private void SwitchBack()
    {
        try
        {
            var backButton = _driver.FindElement(By.XPath(
                "\t\r\n//android.widget.ImageButton[@content-desc=\"Navigate up\"]"));
            backButton.Click();
            Thread.Sleep(1500);
        }
        catch (WebDriverException)
        {
            Assert.Fail("Cannot navigate back.");
        }
    }

    private List<AppiumWebElement> RetrieveSections()
    {
        var listViewElement = _driver.FindElement(
            By.Id("com.finalwire.aida64:id/pageList"));
        var roElements = listViewElement.FindElements(
            By.ClassName("android.widget.TextView"));
        var elements = new List<AppiumWebElement>();
        foreach (var element in roElements)
            elements.Add(element);
        return elements;
    }
}