// <copyright file="LoginPage.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace MCS.Test.Automation.Tests.PageObjects.PageObjects.MCS
{
    using System;
    using System.Globalization;
    using global::MCS.Test.Automation.Common;
    using global::MCS.Test.Automation.Common.Extensions;
    using global::MCS.Test.Automation.Common.Types;
    using global::MCS.Test.Automation.Tests.PageObjects;
    using global::NUnit.Framework;
    using NLog;

    public class LoginPage : ProjectPageBase
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();
        private readonly ElementLocator
customerLogo = new ElementLocator(Locator.CssSelector, "img.ui.image");

        private readonly ElementLocator
loginlable = new ElementLocator(Locator.CssSelector, "h2");

        private readonly ElementLocator
usernametxt = new ElementLocator(Locator.Name, "username");

        private readonly ElementLocator
passwordtxt = new ElementLocator(Locator.Name, "password");

        private readonly ElementLocator
loginbtn = new ElementLocator(Locator.XPath, "//*[@class='ui primary button']");

        private readonly ElementLocator
            unameErrormsg = new ElementLocator(Locator.XPath, "//*[@class='errorMessage' and contains(text(), 'Username is required.')]");

        private readonly ElementLocator
            passwordErrormsg = new ElementLocator(Locator.XPath, "//*[@class='errorMessage' and contains(text(), 'Password is required.')]");

        private string nmcustomerlogo = "Customer logo";
        private string nmusernametxt = "Username text field";
        private string nmpasswordtxt = "Password text field";
        private string nmloginbtn = "Login button";
        private string nmusernameerror = "Username is required.";
        private string nmpassworderror = "Password is required.";

        public LoginPage(DriverContext driverContext)
            : base(driverContext)
        {
        }

        public LoginPage OpenLandingPage()
        {
            switch (BaseConfiguration.TestBrowser)
            {
                case BrowserType.Firefox:
                    var urlFF = BaseConfiguration.GetMCS2UrlValue;
                    this.Driver.NavigateToFirefox(new Uri(urlFF));
                    return this;
                case BrowserType.InternetExplorer:
                case BrowserType.IE:
                    var url = BaseConfiguration.GetMCS2UrlValue;
                    this.Driver.NavigateToIE(new Uri(url));
                    return this;
                case BrowserType.Chrome:
                    var urlchrome = BaseConfiguration.GetMCS2UrlValue;
                    this.Driver.NavigateTo(new Uri(urlchrome));
                    Logger.Info(CultureInfo.CurrentCulture, "Opening page {0}", BaseConfiguration.GetUrlValue);
                    return this;

                default:
                    throw new NotSupportedException(
                        string.Format(CultureInfo.CurrentCulture, "Driver {0} is not supported", BaseConfiguration.TestBrowser));
            }
        }

        public LoginPage OpenASTMInternalLandingPage()
        {
            switch (BaseConfiguration.TestBrowser)
            {
                case BrowserType.Firefox:
                    var urlFF = BaseConfiguration.GetMCS2ASTMInternalUrlValue;
                    this.Driver.NavigateToFirefox(new Uri(urlFF));
                    return this;
                case BrowserType.InternetExplorer:
                case BrowserType.IE:
                    var url = BaseConfiguration.GetMCS2ASTMInternalUrlValue;
                    this.Driver.NavigateToIE(new Uri(url));
                    return this;
                case BrowserType.Chrome:
                    var urlchrome = BaseConfiguration.GetMCS2ASTMInternalUrlValue;
                    this.Driver.NavigateTo(new Uri(urlchrome));
                    Logger.Info(CultureInfo.CurrentCulture, "Opening page {0}", BaseConfiguration.GetUrlValue);
                    return this;

                default:
                    throw new NotSupportedException(
                        string.Format(CultureInfo.CurrentCulture, "Driver {0} is not supported", BaseConfiguration.TestBrowser));
            }
        }

        public void IsCustomerLogoDisplayed()
        {
            this.Driver.IsElementVisibleWithSoftAssertion(this.customerLogo, this.nmcustomerlogo);
        }

        public void EnterUserName(string uname)
        {
            this.Driver.IsElementVisible(this.usernametxt.Format(uname), this.nmusernametxt);
            this.Driver.EnterText(this.usernametxt.Format(uname), uname, this.nmusernametxt);
        }

        public void EnterPassword(string password)
        {
            this.Driver.IsElementVisible(this.passwordtxt.Format(password), this.nmpasswordtxt);
            this.Driver.EnterText(this.passwordtxt.Format(password), password, this.nmpasswordtxt);
        }

        public void IsUserAbletoLoginMCSApp(string uname, string pass)
        {
            this.EnterUserName(uname);
            this.EnterPassword(pass);
            this.IsLoginButtonClickable();
        }

        public void IsUsernameAndPasswordErrorMessagesDisplayed(string unameerror, string passerror)
        {
            var unError = this.Driver.GetText(this.unameErrormsg);
            var pwdError = this.Driver.GetText(this.passwordErrormsg);
            this.Driver.IsElementVisible(this.unameErrormsg, this.nmusernameerror);
            this.Driver.IsElementVisible(this.passwordErrormsg, this.nmpassworderror);
            Assert.AreEqual(unError.Equals(unameerror), pwdError.Equals(passerror));
        }

        public void IsLoginButtonClickable()
        {
            this.Driver.IsElementVisible(this.loginbtn, this.nmloginbtn);
            this.Driver.IsElementClickable(this.loginbtn, this.nmloginbtn);
        }
    }
}
