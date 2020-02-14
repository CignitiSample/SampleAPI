// <copyright file="MembershipDocumentsPage.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace MCS.Test.Automation.Tests.PageObjects.PageObjects.MCS
{
    using System;
    using System.Threading;
    using global::MCS.Test.Automation.Common;
    using global::MCS.Test.Automation.Common.Extensions;
    using global::MCS.Test.Automation.Common.Helpers;
    using global::MCS.Test.Automation.Common.Types;
    using NLog;
    using NUnit.Framework;
    using RelevantCodes.ExtentReports;

    public class MembershipDocumentsPage : ProjectPageBase
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        private readonly ElementLocator
        membershipDocumentHeader = new ElementLocator(Locator.CssSelector, "div.headingTitle.clearfix h2");

        private readonly ElementLocator
        editButton = new ElementLocator(Locator.CssSelector, "i.pencil.icon.squareIcon");

        private readonly ElementLocator
        editDocumnetName = new ElementLocator(Locator.Name, "Name");

        private readonly ElementLocator
        saveButton = new ElementLocator(Locator.CssSelector, "button.ui.primary.button");

        private readonly ElementLocator
        documentId = new ElementLocator(Locator.CssSelector, "i.pencil.icon.squareIcon");

        private readonly ElementLocator
            usersuccessfullmsg = new ElementLocator(Locator.CssSelector, "div.ui.compact.message.success div.content p");

        private readonly ElementLocator
            updatedOn = new ElementLocator(Locator.CssSelector, "td.UpdatedDate");

        private readonly ElementLocator
            updatedBy = new ElementLocator(Locator.CssSelector, "td.UpdatedBy");

        private readonly ElementLocator
            loggedUser = new ElementLocator(Locator.CssSelector, "span.maxName.ellip");

        private readonly ElementLocator
            errorMessage = new ElementLocator(Locator.CssSelector, "span.errorMessage");

        private readonly ElementLocator
            closePopUp = new ElementLocator(Locator.CssSelector, "i.close");

        private readonly ElementLocator
          documentIDColumn = new ElementLocator(Locator.CssSelector, "th.membershipName");

        private readonly ElementLocator
            documentNameColumn = new ElementLocator(Locator.CssSelector, "th.membershipFee");

        private readonly ElementLocator
           typeColumn = new ElementLocator(Locator.XPath, "//th[contains(text(),'Type')]");

        private readonly ElementLocator
            statusColumn = new ElementLocator(Locator.XPath, "//th[contains(text(),'Status')]");

        private readonly ElementLocator
           updatedByColumn = new ElementLocator(Locator.XPath, "//th[contains(text(),'Updated By')]");

        private readonly ElementLocator
           updatedOnColumn = new ElementLocator(Locator.XPath, "//th[contains(text(),'Updated On')]");

        private readonly ElementLocator
           actionsColumn = new ElementLocator(Locator.XPath, "//th[contains(text(),'Actions')]");

        private readonly ElementLocator
           uploadDocumentErrorMessage = new ElementLocator(Locator.CssSelector, "div.errorMessage");

        private readonly ElementLocator
           uploadDocumentMoreSizeErrorMsg = new ElementLocator(Locator.CssSelector, "div.errorMessage");

        private readonly ElementLocator
            downloadButton = new ElementLocator(Locator.XPath, "//td[contains(text(),'{0}')]/..//i[@class='download icon squareIcon']");

        private readonly ElementLocator
            uploadDocumentText = new ElementLocator(Locator.XPath, "//i[@class='file pdf outline icon']");

        private string nmeditBtn = "Select Edit Btn";
        private string nmeditdocumentname = "Document Name field";
        private string nmsaveBtn = "Save Btn";
        private string nmdocumentId = "Document Id";
        private string nmupdatedon = "Document Last Update On";
        private string nmclosePopUp = "Close Pop UP";
        private string nmdownload = "Download";
        private string nmuploadDocumentMoreSizeErrorMsg = "Upload Document field Error";

        public MembershipDocumentsPage(DriverContext driverContext)

            : base(driverContext)
        {
        }

        public string DocumentStandardUrl { get; set; }

        public int GetPDFFileCount { get; set; }

        public void IsUserAbleToViewMembershipDocumentHeader(string expected)
        {
            this.Driver.WaitUntilElementIsFound(this.membershipDocumentHeader, BaseConfiguration.LongTimeout);
            Verify.IsExpectedTextMatchWithActualTextWithSoftAssertion(this.DriverContext, this.membershipDocumentHeader, expected);
        }

        public void IsUserAbleToClickOnEditBtn()
        {
            this.Driver.IsElementClickableFromListOfElemetsBasedOnIndex(this.editButton, this.nmeditBtn, 0);
        }

        public void EnterDocumentName(string name)
        {
            this.Driver.EnterText(this.editDocumnetName, name, this.nmeditdocumentname);
        }

        public void IsUserAbleToClickOnSaveBtn()
        {
            this.Driver.IsElementClickable(this.saveButton, this.nmsaveBtn);
        }

        public string IsUserIsAbleToSelectDocumnetFromList(int i)
        {
            this.Driver.WaitForPageLoad();
            this.Driver.WaitUntilElementIsFound(this.documentId, 30);
            string actual = this.Driver.GetTextFromListOfElementsBasedOnIndex(this.documentId, this.nmdocumentId, i);
            this.Driver.IsElementClickable(this.documentId, this.nmdocumentId);
            return actual;
        }

        public void IsSuccessfullMessageDisplayedForUpdatedDocument(string name)
        {
            var text = this.Driver.GetText(this.usersuccessfullmsg);
            this.Driver.IsExpectedTextMatchWithActualText(this.usersuccessfullmsg, text, name);
        }

        public string IsUserAbleToGetTextLastUpdatedOnBeforeEdit(int i)
        {
            var text = this.Driver.GetTextFromListOfElementsBasedOnIndex(this.updatedOn, this.nmupdatedon, i);
            return text;
        }

        public void IsUserAbleToGetTextLastUpdatedOnAfterEdit(string name, int i)
        {
            var text = this.Driver.GetTextFromListOfElementsBasedOnIndex(this.updatedOn, this.nmupdatedon, i);
            Assert.AreNotEqual(name, text);
        }

        public void IsUserAbleToGetTextLastUpdatedByAfterEdit(int i)
        {
            var text = this.Driver.GetTextFromListOfElementsBasedOnIndex(this.updatedBy, this.nmupdatedon, i);
            var name = this.Driver.GetText(this.loggedUser);
            string actualtext = text.ToLower();
            string expectedtext = name.ToLower();
            if (actualtext.Contains(expectedtext))
            {
                DriverContext.ExtentStepTest.Log(LogStatus.Pass, "To verify Expected Value " + expectedtext + " is matching with Actual text ", "The expected Value is " + expectedtext + " and  actual value is " + actualtext + " matching successfully");
                Logger.Info("Expected text " + expectedtext + " and Actual text is " + actualtext);
            }
            else
            {
                Logger.Error("Failed to click on " + name + "Due to exception: ");
                throw new Exception("No Such User Found");
            }
        }

        public void IsErrorMessageDisplayedForMandatoryField(string name)
        {
            var text = this.Driver.GetText(this.errorMessage);
            this.Driver.IsExpectedTextMatchWithActualText(this.errorMessage, text, name);
        }

        public void IsUserAbleToClickOnClosePopUp()
        {
            this.Driver.IsElementClickable(this.closePopUp, this.nmclosePopUp);
        }

        public void IsUserAbleToViewMembershipDocumentIdColumn(string expected)
        {
            this.CheckMembershipDocumentColumnName(expected, this.documentIDColumn);
        }

        public void IsUserAbleToViewMembershipDocumentNameColumn(string expected)
        {
            this.CheckMembershipDocumentColumnName(expected, this.documentNameColumn);
        }

        public void IsUserAbleToViewMembershipTypeColumn(string expected)
        {
            this.CheckMembershipDocumentColumnName(expected, this.typeColumn);
        }

        public void IsUserAbleToViewMembershipStatusColumn(string expected)
        {
            this.CheckMembershipDocumentColumnName(expected, this.statusColumn);
        }

        public void IsUserAbleToViewMembershipUpdatedByColumn(string expected)
        {
            this.CheckMembershipDocumentColumnName(expected, this.updatedByColumn);
        }

        public void IsUserAbleToViewMembershipUpdatedOnColumn(string expected)
        {
            this.CheckMembershipDocumentColumnName(expected, this.updatedOnColumn);
        }

        public void IsUserAbleToViewMembershipActionsColumn(string expected)
        {
            this.CheckMembershipDocumentColumnName(expected, this.actionsColumn);
        }

        public void IsUserAbleToViewErrorMessageForForUploadingExcelFile(string expected)
        {
            this.Driver.IsExpectedTextMatchWithActualText(this.uploadDocumentErrorMessage, expected);
        }

        public void IsUserAbleToViewErrorMessageIfUploadMoreSizeFile(string expected)
        {
            Verify.ValiadteTextPresentOnElementWithSoftAssertion(this.DriverContext, this.uploadDocumentMoreSizeErrorMsg, this.nmuploadDocumentMoreSizeErrorMsg, expected);
        }

        public void IsUserAbleToClickOnDownloadBtn(string docName)
        {
            this.DocumentStandardUrl = this.Driver.Url;
            this.GetPDFFileCount = FilesHelper.GetFilesOfGivenType(BaseConfiguration.DownloadFolder, FileType.Pdf).Count;
            this.Driver.WaitForPageLoad();
            this.Driver.IsElementClickable(this.downloadButton.Format(docName), this.nmdownload);
            Thread.Sleep(5000);
        }

        public string IsUserAbleToGetUploadedFileText()
        {
            var actualMembership = this.Driver.GetText(this.uploadDocumentText);
            DriverContext.ExtentStepTest.Log(RelevantCodes.ExtentReports.LogStatus.Pass, "To verify Expected Value is " + actualMembership, "The expected Value is " + actualMembership);
            Logger.Info("Expected text is " + actualMembership);
            return actualMembership;
        }

        private void CheckMembershipDocumentColumnName(string expected, ElementLocator elemlocatar)
        {
            this.Driver.WaitUntilElementIsFound(elemlocatar, BaseConfiguration.LongTimeout);
            this.Driver.IsExpectedTextMatchWithActualText(elemlocatar, expected);
        }
    }
}
