// <copyright file="InternalStaffHomePage.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace MCS.Test.Automation.Tests.PageObjects.PageObjects.MCS
{
    using System;
    using System.IO;
    using System.Threading;
    using global::MCS.Test.Automation.Common;
    using global::MCS.Test.Automation.Common.Extensions;
    using global::MCS.Test.Automation.Common.Helpers;
    using global::MCS.Test.Automation.Common.Types;
    using global::MCS.Test.Automation.Tests.PageObjects;
    using NLog;
    using NUnit.Framework;
    using RelevantCodes.ExtentReports;

    public class InternalStaffHomePage : ProjectPageBase
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();
        private readonly ElementLocator
        customerLogo = new ElementLocator(Locator.CssSelector, "img.ui.image");

        private readonly ElementLocator
           loggedUser = new ElementLocator(Locator.CssSelector, "span.maxName.ellip");

        private readonly ElementLocator
       dimmerVisible = new ElementLocator(Locator.XPath, "//div[@class='ui active transition visible dimmer']");

        private readonly ElementLocator
        renewalTasksMenu = new ElementLocator(Locator.XPath, "//a[contains(text(),'Renewal Tasks')]");

        private readonly ElementLocator
    renewalTasksMenuLocator = new ElementLocator(Locator.XPath, "//div[@class='menuWrapper']//a[text()='Renewal Tasks']");

        private readonly ElementLocator
           membersManagementmenu = new ElementLocator(Locator.XPath, "//div[@class='menuWrapper']//a[text()='Member Management']");

        private readonly ElementLocator
            memberSubMenuMember = new ElementLocator(Locator.XPath, "//a[@href='/member-management/members'][text()='Members']");

        private readonly ElementLocator
            exportExcelIcon = new ElementLocator(Locator.XPath, "//button[@class='ui button secondary mr10']");

        private readonly ElementLocator
        exportWordIcon = new ElementLocator(Locator.XPath, "//button[@class='ui button secondary']");

        private readonly ElementLocator
            customGrid = new ElementLocator(Locator.XPath, "//table[@id='customGrid']");

        private readonly ElementLocator
           pageNo = new ElementLocator(Locator.XPath, "//a[@aria-current][contains(.,'{0}')]");

        private readonly ElementLocator
           totalNo = new ElementLocator(Locator.CssSelector, "div.totalPage");

        private readonly ElementLocator
           memberSubMenuOrgAccounts = new ElementLocator(Locator.XPath, "//a[@href='/member-management/organizational-accounts'][text()='Organizational Accounts']");

        private string nmexportExcelIcon = "Excel Icon";
        private string nmPageNo = "Pagination No";
        private string nmtotalNo = "Total Record Count";
        private string nmcustomGrid = "Dynamic Grid";
        private string nmexportWordIcon = "Word Icon";
        private string nmcustomerlogo = "Customer logo";
        private string nmloggeduser = "Logged In User";
        private string nmrenewalTasksMenu = "Renewal Tasks Menu";
        private string nmmembershipSubMenuMember = "Member Sub Menu";
        private string nmmembership = "Memebership Management Menu";
        private string nmmembershipSubMenuOrgAccounts = "Organizational Accounts Sub Menu";

        public InternalStaffHomePage(DriverContext driverContext)
            : base(driverContext)
        {
        }

        public void IsExportExcelIconClickable()
        {
            this.Driver.IsElementVisible(this.exportExcelIcon, this.nmexportExcelIcon);
            this.Driver.IsElementClickable(this.exportExcelIcon, this.nmexportExcelIcon);
            this.Driver.WaitForPageLoad();
            Thread.Sleep(3000);
        }

        public void IsExportExcelFileExcel()
        {
            try
            {
                string filePath = TestContext.CurrentContext.TestDirectory + "\\" + BaseConfiguration.DownloadFolder;
                int getFileCount = FilesHelper.GetFilesOfGivenType(filePath, FileType.Xlsx).Count;
                if (getFileCount <= 0)
                {
                    Assert.IsTrue(false);
                }
                else
                {
                    string[] files = Directory.GetFiles(filePath);
                    string filename = Path.GetFileName(files[0]);
                    Logger.Error("File downloaded , excel file name : " + filename);
                    DriverContext.ExtentStepTest.Log(LogStatus.Pass, "File downloaded , excel file name : " + filename);
                }
            }
            catch (Exception ex)
            {
                Logger.Error("Exception occured while downloading excel file : " + ex.ToString());
                DriverContext.ExtentStepTest.Log(LogStatus.Fail, "Exception occured while downloading excel file");
            }
        }

        public void IsDynamicGridDisplayed()
        {
            this.Driver.IsElementVisible(this.customGrid, this.nmcustomGrid);
        }

        public string Between(string text, string firstString, string lastString)
        {
            string str = text;
            string strFirst = firstString;
            string strLast = lastString;
            string finalString;
            string tempString = string.Empty;
            int pos1 = str.IndexOf(firstString) + firstString.Length;
            int pos2 = str.IndexOf(lastString);
            finalString = str.Substring(pos1, pos2 - pos1);
            return finalString;
        }

        public void IsPaginationLinkEnabled()
        {
            this.Driver.IsElementVisible(this.totalNo, this.nmtotalNo);
            string getTotalDivText = this.Driver.GetText(this.totalNo);
            int totalCount = Convert.ToInt32(this.Between(getTotalDivText, "of", "items"));
            int reminder = (totalCount % 25) > 0 ? 1 : 0;
            int totalIteration = (totalCount / 25) + reminder;

            for (int i = 1; i <= totalIteration; i++)
            {
                this.Driver.ScrollToWebElement(this.pageNo.Format(i));
                this.Driver.IsElementVisible(this.pageNo.Format(i), this.nmPageNo);
                this.Driver.IsElementClickable(this.pageNo.Format(i), this.nmPageNo);
                this.Driver.WaitForPageLoad();
                this.Driver.IsElementGivenAttributePresent(this.pageNo.Format(i), this.nmPageNo, "class", "active item");
            }
        }

        public void IsCustomerLogoDisplayed()
        {
            Verify.IsElementVisibleWithSoftAssertion(this.DriverContext, this.customerLogo, this.nmcustomerlogo);
        }

        public void IsLoggedUserDisplayed()
        {
            Verify.IsElementVisibleWithSoftAssertion(this.DriverContext, this.loggedUser, this.nmloggeduser);
        }

        public void IsRenewalTasksMenuItemClickable()
        {
            this.Driver.WaitUntilElementIsNoLongerFound(this.dimmerVisible, BaseConfiguration.LongTimeout, this.nmrenewalTasksMenu);
            var webElement = this.Driver.GetElement(this.renewalTasksMenu);
            this.Driver.JavaScriptClick(webElement, this.nmrenewalTasksMenu);
        }

        public void IsRenewalTasksMenuClickable()
        {
            this.Driver.WaitUntilElementIsNoLongerFound(this.dimmerVisible, BaseConfiguration.LongTimeout, this.nmrenewalTasksMenu);
            this.Driver.WaitUntilElementIsFound(this.renewalTasksMenuLocator, BaseConfiguration.LongTimeout);
            this.Driver.IsElementVisible(this.renewalTasksMenuLocator, this.nmrenewalTasksMenu);
            this.Driver.IsElementClickable(this.renewalTasksMenuLocator, this.nmrenewalTasksMenu);
        }

        public void IsMembersManagementSectionClickable()
        {
            this.Driver.IsElementVisible(this.membersManagementmenu, this.nmmembership);
            var webElement = this.Driver.GetElement(this.membersManagementmenu);
            this.Driver.JavaScriptClick(webElement, this.nmmembership);
            this.Driver.MouseOverOnWebElement(this.membersManagementmenu);
        }

        public void IsMembersManagementSectionClickableMouseOver()
        {
            this.Driver.WaitUntilElementIsFound(this.membersManagementmenu, BaseConfiguration.LongTimeout);
            this.Driver.IsElementVisible(this.membersManagementmenu, this.nmmembership);
            var webElement = this.Driver.GetElement(this.membersManagementmenu);
            this.Driver.MouseOverOnWebElement(this.membersManagementmenu);
            this.Driver.JavaScriptClick(webElement, this.nmmembership);
        }

        public void IsMemberManagementSubMenuMemberClickable()
        {
            this.Driver.IsElementVisible(this.memberSubMenuMember, this.nmmembershipSubMenuMember);
            if (BrowserType.Chrome.Equals("Chrome"))
            {
                this.Driver.IsElementClickable(this.memberSubMenuMember, this.nmmembershipSubMenuMember);
            }
            else
            {
                var webElement = this.Driver.GetElement(this.memberSubMenuMember);
                this.Driver.JavaScriptClick(webElement, this.nmmembershipSubMenuMember);
            }
        }

        public void IsExportWordIconClickable()
        {
            this.Driver.IsElementVisible(this.exportWordIcon, this.nmexportWordIcon);
            this.Driver.IsElementClickable(this.exportWordIcon, this.nmexportWordIcon);
            this.Driver.WaitForPageLoad();
            Thread.Sleep(3000);
        }

        public void IsExportWordFileDownload()
        {
            try
            {
                string filePath = TestContext.CurrentContext.TestDirectory + "\\" + BaseConfiguration.DownloadFolder;
                int getFileCount = FilesHelper.GetFilesOfGivenType(filePath, FileType.Docx).Count;
                if (getFileCount <= 0)
                {
                    Assert.IsTrue(false);
                }
                else
                {
                    string[] files = Directory.GetFiles(filePath);
                    string filename = Path.GetFileName(files[0]);
                    Logger.Error("File downloaded , word file name : " + filename);
                    DriverContext.ExtentStepTest.Log(LogStatus.Pass, "File downloaded , word file name : " + filename);
                }
            }
            catch (Exception ex)
            {
                Logger.Error("Exception occured while downloading word file : " + ex.ToString());
                DriverContext.ExtentStepTest.Log(LogStatus.Fail, "Exception occured while downloading word file");
            }
        }

        public void IsMemberManagementSubMenuOrgAccountsClickable()
        {
            this.Driver.IsElementVisible(this.memberSubMenuOrgAccounts, this.nmmembershipSubMenuOrgAccounts);
            if (BrowserType.Chrome.Equals("Chrome"))
            {
                this.Driver.IsElementClickable(this.memberSubMenuOrgAccounts, this.nmmembershipSubMenuOrgAccounts);
            }
            else
            {
                var webElement = this.Driver.GetElement(this.memberSubMenuOrgAccounts);
                this.Driver.JavaScriptClick(webElement, this.nmmembershipSubMenuOrgAccounts);
            }
        }

        public void IsMemberManagementSubMenuOrgAccountsClickableWithMouseOver()
        {
            this.Driver.IsElementVisible(this.membersManagementmenu, this.nmmembership);
            this.Driver.MouseOverOnWebElementAndClick(this.membersManagementmenu, this.nmmembership);
            this.Driver.IsElementVisible(this.memberSubMenuOrgAccounts, this.nmmembershipSubMenuOrgAccounts);
            var webElement = this.Driver.GetElement(this.memberSubMenuOrgAccounts);
            this.Driver.JavaScriptClick(webElement, this.nmmembershipSubMenuOrgAccounts);
        }
    }
}

