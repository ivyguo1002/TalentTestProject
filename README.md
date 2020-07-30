Master Branch: Talent Selenium Nunit Project

Specflow Branch: Talent Specflow Project


Framework
1. Customize Selenium Framework: 
    Wrapper Class for IWebDriver and IWebElement. Customize the 
2. WebDriverFactory: Create local and remote webdriver
3. Utilities: ExtentReport, Excel Data Reader, Json Data Reader and other extension methods
4. Configuration: Json Data

Talent
1. Pages:
    Pages and Sections, BasePage
    PageFactory: Init all the pages object
2. Services:
    API (RestSharp)

Talent.Test
1. Tests, BaseTest
2. TestData: Json / Excel

Automation
1. Use API to set up state and clear the data
2. Parallizable on Test Method level. Each Test has a new web driver instance

