Talent Specflow Project
Framework
1. Customize Selenium Framework: 
    Wrapper Class for IWebDriver and IWebElement. 
2. WebDriverFactory: Create local and remote webdriver
3. Utilities: ExtentReport, Excel Data Reader, Json Data Reader and other extension methods
4. Configuration: Json Data

Talent
1. Pages:
    Pages and Sections, BasePage
2. Services:
    API (RestSharp)

Talent.Specflow
1. Hookup: Init Driver and Pages, Register in IObjectContainer
2. TestData: Json / Excel
3. Features: Behaviour driven, Not procedure driven
4. Steps: Context Injection, ScenarioContext, Pages Injection
5. Pages: Driver Injection

Automation
1. Use API to set up state and clear the data
2. Parallizable on Fixture level. 
Each scenario has a new web driver instance.

## Specflow: All scenarios in a feature are executed on the same thread.

