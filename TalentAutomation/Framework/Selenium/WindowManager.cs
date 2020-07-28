using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;
using System.Text;

namespace Framework.Selenium
{
    public class WindowManager
    {
        public ReadOnlyCollection<string> CurrentWindows => Driver.Current.WindowHandles;
        public Size ScreenSize { get {
                var jsx = "return window.screen.availWidth";
                var jsy = "return window.screen.availHeight";
                var jse = (IJavaScriptExecutor)Driver.Current;

                var x = Convert.ToInt32(jse.ExecuteScript(jsx));
                var y = Convert.ToInt32(jse.ExecuteScript(jsy));

                return new Size(x, y);
            } }

        public void SwitchTo(int windowIndex)
        {
            Driver.Current.SwitchTo().Window(CurrentWindows[windowIndex]);
        }
        public void Maximize()
        {
            Driver.Current.Manage().Window.Position = new Point(0, 0);
            Driver.Current.Manage().Window.Size = ScreenSize;
        }
    }
}
