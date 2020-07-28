using Framework.Enums;
using Framework.Utils;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace Framework.Selenium
{
    public class Element
    {
        public IWebElement Current { get; set; }

        public string Name { get; set; }

        public By By { get; set; }

        public string Text
        {
            get
            {
                WaitForTextLoaded();
                return Current.Text;
            }
        }

        public bool Displayed => Current.Displayed;

        public bool Enabled => Current.Enabled;

        public bool Selected => Current.Selected;

        public bool Clickable => Current.Displayed && Current.Enabled && Current.Size.Height > 0 && Current.Size.Width > 0;

        public Size Size => Current.Size;
        public Point Location => Current.Location;

        public string Value => GetAttribute("value");
        public Element(IWebElement element)
        {
            Current = element;
        }

        public Element(IWebElement element, By by)
        {
            Current = element;
            By = by;
        }

        public string GetAttribute(string attrName) => Current.GetAttribute(attrName);
        public string GetCssValue(string cssProperty) => Current.GetCssValue(cssProperty);

        public void Clear() => Current.Clear();
        public void SendKeys(string keys, int timeout = TimeoutSetting.ElementWaitTimeout)
        {
            WaitForClickable(timeout);
            Current.Clear();
            Current.SendKeys(keys);
        }

        public void Click(int timeout = TimeoutSetting.ElementWaitTimeout)
        {
            int attempts = 0;
            while (attempts < 3)
            {
                try
                {
                    WaitForClickable(timeout);
                    Current.Click();

                    break;
                }
                catch (ElementClickInterceptedException)
                {
                    // simply retry FindElementing the element in the refreshed DOM
                    
                }
                attempts++;
            }

        }

        public void Submit() => Current.Submit();

        public void Hover()
        {
            var action = new Actions(Driver.Current);
            action.MoveToElement(Current).Perform();
        }

        public Element FindElement(By by, int timeout = TimeoutSetting.ElementWaitTimeout)
        {
            try
            {
                var wait = new WebDriverWait(Driver.Current, TimeSpan.FromSeconds(timeout));
                var element = wait.Until(driver => Current.FindElement(by));
                return new Element(element, by);
            }
            catch (WebDriverTimeoutException)
            {
                throw new WebDriverException($"Exception in FindElement: element located by {by} not present within {timeout} seconds"); ;
            }
        }

        public List<Element> FindElements(By by, int timeout = TimeoutSetting.ElementWaitTimeout)
        {
            try
            {
                var wait = new WebDriverWait(Driver.Current, TimeSpan.FromSeconds(timeout));
                var elements = wait.Until(driver => Current.FindElements(by));
                var elementList = new List<Element>();
                foreach (var element in elements)
                {
                    elementList.Add(new Element(element));
                }
                return elementList;
            }
            catch (WebDriverTimeoutException)
            {
                throw new WebDriverException($"Exception in FindElements: elements located by {by} not present within {timeout} seconds");
            }
        }

        public void WaitForDisplayed(int timeout = TimeoutSetting.ElementWaitTimeout)
        {
            try
            {
                var wait = new WebDriverWait(Driver.Current, TimeSpan.FromSeconds(timeout));
                wait.Until(d => Displayed);
            }
            catch (WebDriverTimeoutException)
            {
                throw new WebDriverTimeoutException($"Exception in WaitForDisplayed: element located by {By} not displayed within {timeout} seconds");
            }
        }

        public void WaitForEnabled(int timeout = TimeoutSetting.ElementWaitTimeout)
        {
            try
            {
                var wait = new WebDriverWait(Driver.Current, TimeSpan.FromSeconds(timeout));
                wait.Until(d => Enabled);
            }
            catch (WebDriverTimeoutException)
            {
                throw new WebDriverTimeoutException($"Exception in WaitForEnabled: element located by {By} not enabled within {timeout} seconds");
            }
        }

        public void WaitForClickable(int timeout = TimeoutSetting.ElementWaitTimeout)
        {
            try
            {
                var wait = new WebDriverWait(Driver.Current, TimeSpan.FromSeconds(timeout));
                wait.Until(d => Clickable);
            }
            catch (WebDriverTimeoutException)
            {
                throw new WebDriverTimeoutException($"Exception in WaitForClickable: element located by {By} not clickable within {timeout} seconds");
            }
        }

        public void WaitForTextLoaded(int timeout = TimeoutSetting.ElementWaitTimeout)
        {
            try
            {
                var wait = new WebDriverWait(Driver.Current, TimeSpan.FromSeconds(timeout));
                wait.Until(driver => !String.IsNullOrEmpty(Current.Text));
            }
            catch (WebDriverTimeoutException)
            {
                ReportHelper.LogTestStepInfo($"Exception in WaitForTextLoaded: text in element located by {By} not loaded within {timeout} seconds");
            }
        }

        public void ExpandHeader()
        {
            if(GetAttribute("aria-expanded") == "false")
            {
                Click();
            }
        }

        public int SearchDataFromTable(Dictionary<string, string> dataset)
        {
            List<Element> rows = FindElements(By.CssSelector("tbody tr"));

            for (int i = 0; i < rows.Count; i++)
            {
                List<Element> columns = rows[i].FindElements(By.TagName("td"));
                int j = 0;
                foreach (KeyValuePair<string, string> kvp in dataset)
                {
                    
                    int columnIndex = GetColumnIndex(kvp.Key);
                    if (columnIndex == -1)
                    {
                        throw new ArgumentException("The specified column doesn't exist");
                    }
                    string columnValue = kvp.Value;
                    if (columns[columnIndex].Text == columnValue)
                    {
                        j++;
                        continue;
                    }
                    else
                    {
                        break;
                    }
                }

                if (j == dataset.Count)
                {
                    return i;
                }
            }
            return -1;
        }

        public int GetColumnIndex(string key)
        {
            var headerRow = FindElement(By.CssSelector("thead>tr"));
            List<Element> headerColumns = FindElements(By.CssSelector("th"));

            for(int i = 0; i < headerColumns.Count; i ++)
            {
                if (headerColumns[i].Text.Equals(key, StringComparison.OrdinalIgnoreCase))
                {
                    return i;
                }
            }

            return -1;
        }

    }

}
