// <copyright file="ManageOfficerTitlePage.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace MCS.Test.Automation.Tests.PageObjects.PageObjects.MCS
{
    using System;
    using System.Collections.Generic;
    using System.Threading;
    using global::MCS.Test.Automation.Common;
    using global::MCS.Test.Automation.Common.Extensions;
    using global::MCS.Test.Automation.Common.Helpers;
    using global::MCS.Test.Automation.Common.Types;
    using global::MCS.Test.Automation.Tests.PageObjects;
    using global::NUnit.Framework;
    using NLog;
    using OpenQA.Selenium;
    using RelevantCodes.ExtentReports;

    public class ManageOfficerTitlePage : ProjectPageBase
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();
        private readonly ElementLocator
customerLogo = new ElementLocator(Locator.CssSelector, "img.ui.image");

        private readonly ElementLocator
membershipmenu = new ElementLocator(Locator.CssSelector, "i.address.card.icon");

        private readonly ElementLocator
committeemanagement = new ElementLocator(Locator.CssSelector, "i.users.icon");

        private readonly ElementLocator
            applicationManagement = new ElementLocator(Locator.CssSelector, "i.cog.icon");

        private readonly ElementLocator
            managemembershiptype = new ElementLocator(Locator.CssSelector, "ul > li:nth-child(1)");

        private readonly ElementLocator
            manageMembershipFAQs = new ElementLocator(Locator.CssSelector, "ul > li:nth-child(2)");

        private readonly ElementLocator
            manageMembershipDocuments = new ElementLocator(Locator.CssSelector, "ul > li:nth-child(3)");

        private readonly ElementLocator
            manageMemberClassifications = new ElementLocator(Locator.CssSelector, "ul > li:nth-child(4)");

        private readonly ElementLocator
            officerTitleheader = new ElementLocator(Locator.CssSelector, "div.headingTitle.clearfix h2");

        private readonly ElementLocator
            addOfficerTitleButton = new ElementLocator(Locator.CssSelector, "button.ui.secondary.button");

        private readonly ElementLocator
            cardtitleEditbutton = new ElementLocator(Locator.XPath, "//*[@class='roleCard']/h4[text()='{0}']/following::div[2]/i");

        private readonly ElementLocator
            officerTitleUpdatebutton = new ElementLocator(Locator.CssSelector, "button.ui.primary.button");

        private readonly ElementLocator
            listofRolCard = new ElementLocator(Locator.CssSelector, "div.roleCard h4");

        private readonly ElementLocator
            popupaddnewofficerTitleheader = new ElementLocator(Locator.CssSelector, "div.header");

        private readonly ElementLocator
            officertitletext = new ElementLocator(Locator.Name, "add_OfficerTitleName");

        private readonly ElementLocator
           astmStaffCheckBox = new ElementLocator(Locator.XPath, "//div[@class='ui checkbox mr10']/label[text()='{0}']");

        private readonly ElementLocator
           labelRosterMaintenance = new ElementLocator(Locator.XPath, "//div[text()='Roster Maintenance']");

        private readonly ElementLocator
            addofficersavebutton = new ElementLocator(Locator.CssSelector, "button.ui.primary.button");

        private readonly ElementLocator
            successfullMsg = new ElementLocator(Locator.CssSelector, "div.ui.compact.message.success div.content p");

        private readonly ElementLocator
          loadbutton = new ElementLocator(Locator.XPath, "//*[@class='ui active transition visible dimmer']");

        private readonly ElementLocator
            errorMsg = new ElementLocator(Locator.CssSelector, "div.ui.compact.message.error div.content p");

        private readonly ElementLocator
            cardTitlesList = new ElementLocator(Locator.CssSelector, "div.roleCard h4");

        private readonly ElementLocator
            cardTitleStatusfield = new ElementLocator(Locator.XPath, " //h4[text()='{0}']/..//div[@class='roleStatus']/span[starts-with(@class, 'statusIndicator')]");

        private readonly ElementLocator
             cardTitleResponsibilities = new ElementLocator(Locator.XPath, " //*[@class='roleCard']/h4[text()='{0}']/following::div[3]/span[@class='privelegeHead']");

        private readonly ElementLocator
            officerTitleAlredyExistmsg = new ElementLocator(Locator.CssSelector, "span.errorMessage");

        private readonly ElementLocator
            officerTitlePopUpwindowcloseIcon = new ElementLocator(Locator.CssSelector, "div.header i.close");

        private readonly ElementLocator
            officerTitleStausActive = new ElementLocator(Locator.XPath, "//*[@class='roleCard']/h4[text()='{0}']/following::div[1]/span[@class='statusIndicator active']");

        private readonly ElementLocator
            officerTitleMandatory = new ElementLocator(Locator.CssSelector, "span.errorMessage");

        private readonly ElementLocator
            rostermaintenacefield = new ElementLocator(Locator.XPath, "(//span[text()='Roster Maintenance'])[2]");

        private readonly ElementLocator
           officerTitleCardSequnceNo = new ElementLocator(Locator.XPath, "//span[@class='positionMumber mr10']");

        private readonly ElementLocator
           officerTitleCardSequnceNoDragFrom = new ElementLocator(Locator.XPath, "//div[@class='ui four cards']//span[@class='positionMumber mr10' and text()='{0}']");

        private readonly ElementLocator
           officerTitleCardSequnceNoDragTo = new ElementLocator(Locator.XPath, "//div[@class='ui four cards']//span[@class='positionMumber mr10' and text()='{0}']");

        private readonly ElementLocator
            searchboxtxt = new ElementLocator(Locator.XPath, "//*[@placeholder='Search by Officer Title']");

        private readonly ElementLocator
        popupofficerTitle = new ElementLocator(Locator.XPath, "//*[@class='results transition']/div");

        private readonly ElementLocator
        popupRoster = new ElementLocator(Locator.XPath, "//div[@class='ui right center flowing popup transition visible privListTooltip']");

        private readonly ElementLocator
            rosterPermissionList = new ElementLocator(Locator.XPath, "//span[@class='permsnName' and contains(text(),'{0}')]");

        private readonly ElementLocator
            responsibilityCheckbox = new ElementLocator(Locator.XPath, "(//span[text()='Roster Maintenance'])[1]");

        private readonly ElementLocator
            responsibilityCallOutBox = new ElementLocator(Locator.XPath, "//div[@class='ui right center flowing popup transition visible privListTooltip']");

        private readonly ElementLocator
            updateClassificationChkBox = new ElementLocator(Locator.XPath, "//span[text()='Update Classification']");

        private readonly ElementLocator
            viewChkBox = new ElementLocator(Locator.XPath, "//span[text()='View']");

        private readonly ElementLocator
            membershipReportChkBox = new ElementLocator(Locator.XPath, "//span[text()='Membership Report']");

        private readonly ElementLocator
           rosterAllCheckBox = new ElementLocator(Locator.XPath, "//p[text()='{0}']/following-sibling::div[1]/div//span[text()=' All']/preceding-sibling::div/label");

        private readonly ElementLocator
           rosterIsUnCheckCheckBox = new ElementLocator(Locator.XPath, "//p[@class='commonTitle' and text()='{0}']/../div[{1}]//div[@class='ui fitted checkbox']");

        private readonly ElementLocator
           rosterIsCheckedCheckBox = new ElementLocator(Locator.XPath, "//p[@class='commonTitle' and text()='{0}']/../div[{1}]//div[@class='ui checked fitted checkbox']");

        private readonly ElementLocator
            sequencenoLabel = new ElementLocator(Locator.XPath, "//div[@class='ui four cards']//span[@class='positionMumber mr10']");

        private readonly ElementLocator
          activeLabel = new ElementLocator(Locator.XPath, "//div[@class='ui four cards']//span[@class='statusIndicator active' and text()='{0}']");

        private readonly ElementLocator
        officerTitleLabel = new ElementLocator(Locator.XPath, "//div[@class='ui four cards']//h4[text()='{0}']");

        private readonly ElementLocator
    dimmerVisible = new ElementLocator(Locator.XPath, "//div[@class='ui active transition visible dimmer']");

        private readonly ElementLocator
  applicableToCommiteeCheck = new ElementLocator(Locator.XPath, "//div[@class='ui checked checkbox mr10']");

        private string nmofficerTitleErrormessage = "Officer Title already exists.";
        private string nmrosterIsUnCheckCheckBox = "all check boxes under {0} are unchecked";
        private string nmrosterIsCheckedCheckBox = "all check boxes under {0} are checked ";
        private string nmapplicableToCommiteeCheck = "Applicable To Commitee Checkbox";
        private string nmsequencenoLabel = "Sequence No Label";
        private string nmactiveLabel = "Active Label";
        private string nmactiveofficerTitleLabel = "Officer Title Label ({0})";
        private string nmrosterCheckBoxAll = "{0} CheckBox";
        private string nmlabelRosterMaintenance = "Roster Maintenance Label";
        private string nmastmStaffCheckBox = "ASTM Staff checkbox";
        private string nmmembershipReportChkBox = "Membership Report checkbox";
        private string nmupdateClassificationChkBox = "Update Classification checkbox";
        private string nmviewChkBox = "View checkbox";
        private string nmresponsibilityCallOutBox = "Responsibility Call Out Box";
        private string nmresponsibilityCheckbox = "responsibility Checkbox";
        private string nmRosterPopup = "Roster responsibilities popup";
        private string nmrostermaintenacefield = "Roster Maintenace Field";
        private string nmofficerTitleAlredyExistmsg = "Officer Title already exists.";
        private string nmsearchboxtxt = "Search box ";
        private string nmcardtitleEditbutton = "Edit icon in Officer Title Card";
        private string nmofficerStatusfield = "Status-field in Officer Title Card";
        private string nmofficerTitlesuccessfullmessage = "New Officer Title added successfully.";
        private string nmaddOfficerTitleButton = "Add Officer Title button";
        private string nmpopupaddnewofficerTitleheader = "ADD NEW OFFICER TITLE";
        private string nmofficertitletext = "Officer Title Text";
        private string nmaddooficersavebutton = "Add New Officer Title Save button";
        private string nmcustomerlogo = "Customer logo";
        private string nmmembership = "Memebership Management Menu";
        private string nmcommitteemanagement = "Committee Management Menu";
        private string nmapplicationmanagement = "Application Management Menu";
        private string nmmanagememebershiptype = "Manage Membership Type option";
        private string nmmanagemembershipFaqs = "Manage Membership FAQ's option";
        private string nmmanagemembershipdocument = "Manage Membership Documents option";
        private string nmmanagememeberclassifications = "Manage Member Classifications option";
        private string nmofficerTitleheader = "Officer Titles header";
        private string nmofficerTitleUpdateButton = "Officer Title Update button";
        private string nmOfficerTitlePopUpwindowcloseIcon = "Close button";
        private string nmofficerTitleCardSequnceNo = "Sequence No";
        private string nmofficerTitleCardSequnceTitleOne = "Sequence No : {0}";
        private string nmdimmerloading = "dimmer loading";
        private string nmofficerTitleMandatory = "Officer Title required Message";

        public ManageOfficerTitlePage(DriverContext driverContext)
            : base(driverContext)
        {
        }

        public void IsUserAbletoViewSequenceLabelSoftAssertion()
        {
            string seuqenceNo = Verify.getTextIfElementIsVisibleWithSoftAssertion(this.DriverContext, this.sequencenoLabel, this.nmsequencenoLabel);
            try
            {
                int result = int.Parse(seuqenceNo);
                DriverContext.ExtentStepTest.Log(LogStatus.Pass, "To verify Sequence number is integer ", "Sequence number :" + result.ToString());
            }
            catch (Exception ex)
            {
                DriverContext.ExtentStepTest.Log(LogStatus.Pass, "To verify Sequence number is integer ", "Expection occured because seqeunce no is not Integer : " + ex.InnerException);
            }
        }

        public void IsUserAbletoViewActiveLabelSoftAssertion(string expectedText)
        {
            Verify.IsElementVisibleWithSoftAssertion(this.DriverContext, this.activeLabel.Format(expectedText), this.nmactiveLabel);
        }

        public void IsUserAbletoViewOfficerTitleLabelSoftAssertion(string expectedText)
        {
            Verify.IsElementVisibleWithSoftAssertion(this.DriverContext, this.officerTitleLabel.Format(expectedText), string.Format(this.nmactiveofficerTitleLabel, expectedText));
        }

        public void GetOfficeTitleExistsInDBSoftAssertion(string officerTitle, string compareText, string message)
        {
            Verify.GetSingleValueFromDBCompareWithExpectedValue(this.DriverContext, string.Format(SqlQuery.FunctionalAddOfficerTitleExists, officerTitle), compareText, message);
        }

        public void GetOfficeTitleIsASTMStaffExistsInDBSoftAssertion(string officerTitle, string compareText, string message)
        {
            Verify.GetSingleValueFromDBCompareWithExpectedValue(this.DriverContext, string.Format(SqlQuery.FunctionalAddOfficerIsASTMStaffExists, officerTitle), compareText, message);
        }

        public void GetOfficeTitleIsASTMResponsiblityInDB(string officerTitle, string column, List<string> compareList, string message)
        {
            this.Driver.GetSingleColumnValuesAndCompareWithExpectedListFromDB(string.Format(SqlQuery.FunctionalAddOfficerResponsibityExists, officerTitle), column, compareList, message);
        }

        public void DragSeqenceFromOneIndexToOtherIndex(string startindex, string lastindex)
        {
            this.Driver.DragDropFromLocatorToLocator(this.officerTitleCardSequnceNoDragFrom.Format(1), this.officerTitleCardSequnceNoDragTo.Format(lastindex));
            this.Driver.WaitForPageLoad();
            Thread.Sleep(4000);
        }

        public bool OfficerTitleIndexDisplayed(string startindex)
        {
            return this.Driver.IsElementPresentOrNot(this.officerTitleCardSequnceNoDragFrom.Format(startindex), string.Format(this.nmofficerTitleCardSequnceTitleOne, startindex), string.Empty);
        }

        public void GetListSequenceNoOfficerList()
        {
            this.Driver.WaitForPageLoad();
            this.Driver.AreElementsSortedInorderForDateTime(this.officerTitleCardSequnceNo, this.nmofficerTitleCardSequnceNo);
        }

        public void ClickOnCloseButtonOfAddOfficerTitlePopUpWindow()
        {
            this.Driver.IsElementClickable(this.officerTitlePopUpwindowcloseIcon, this.nmOfficerTitlePopUpwindowcloseIcon);
        }

        public void IsCustomerLogoDisplayed()
        {
            this.Driver.IsElementVisible(this.customerLogo, this.nmcustomerlogo);
        }

        public void IsUpdateButtonOfOfficerTitleClickable()
        {
            this.Driver.IsElementVisible(this.officerTitleUpdatebutton, this.nmofficerTitleUpdateButton);
            this.Driver.IsElementClickable(this.officerTitleUpdatebutton, this.nmofficerTitleUpdateButton);
        }

        public void IsMembershipManagementSectionClickable(string name)
        {
            this.Driver.IsElementVisible(this.membershipmenu.Format(name), this.nmmembership);
            this.Driver.IsElementClickable(this.membershipmenu.Format(name), this.nmmembership);
        }

        public void IsOffierTitleRecordEditableFromListOfRecords(string officerTitle)
        {
            this.Driver.IsElementClickableFromListofElement(this.cardtitleEditbutton.Format(officerTitle), officerTitle);
        }

        public void IsOfficerTitleDisplayedInListofOfficerTitle(string officerTitleText)
        {
            this.Driver.WaitUntilElementIsFound(this.listofRolCard, BaseConfiguration.LongTimeout);
            this.Driver.IsElementVisibleFromListOfElement(this.listofRolCard.Format(officerTitleText), officerTitleText);
        }

        public void IsOfficerTitleDisplayedInListofOfficerTitleSoftAssertion(string officerTitleText)
        {
            Verify.WaitUntilElementIsNoLongerFoundWithSoftAssertion(this.DriverContext, this.dimmerVisible, this.nmdimmerloading, BaseConfiguration.LongTimeout);
            Verify.WaitUntilElementIsFoundWithSoftAssertion(this.DriverContext, this.listofRolCard, officerTitleText, BaseConfiguration.LongTimeout);
            Verify.IsElementVisibleFromListOfElementWithSoftAssertion(this.DriverContext, this.listofRolCard.Format(officerTitleText), officerTitleText);
        }

        public void IsCommitteeManagementSectionClickable(string name)
        {
            this.Driver.IsElementVisible(this.committeemanagement.Format(name), this.nmcommitteemanagement);
            this.Driver.IsElementClickable(this.committeemanagement.Format(name), this.nmcommitteemanagement);
        }

        public void IsApplicationManagementSectionClickable(string name)
        {
            this.Driver.IsElementVisible(this.applicationManagement.Format(name), this.nmapplicationmanagement);
            this.Driver.IsElementClickable(this.applicationManagement.Format(name), this.nmapplicationmanagement);
        }

        public void IsManageOfficerTitleHeaderDisplayed(string header)
        {
            var text = this.Driver.GetText(this.officerTitleheader);
            this.Driver.IsElementVisible(this.officerTitleheader.Format(header), this.nmofficerTitleheader);
            Assert.AreEqual(text, header, "Officer Title header is missing");
        }

        public void IsManageOfficerTitleHeaderDisplayedSoftAssertion(string header)
        {
            Verify.IsElementVisibleWithSoftAssertion(this.DriverContext, this.officerTitleheader.Format(header), this.nmofficerTitleheader);
        }

        public void IsAddOfficerTitleButtonClickable()
        {
            this.Driver.WaitUntilElementIsFound(this.addOfficerTitleButton, BaseConfiguration.LongTimeout);
            var webElement = this.Driver.GetElement(this.addOfficerTitleButton);
            this.Driver.IsElementVisible(this.addOfficerTitleButton, this.nmaddOfficerTitleButton);
            this.Driver.JavaScriptClick(webElement, this.nmaddOfficerTitleButton);
        }

        public void IsAddNewOfficerTitleHeaderDisplayedInPopUpWindow(string popuptitle)
        {
            this.Driver.IsElementVisible(this.popupaddnewofficerTitleheader, this.nmpopupaddnewofficerTitleheader);
            this.Driver.IsExpectedTextMatchWithActualText(this.popupaddnewofficerTitleheader, popuptitle, this.nmpopupaddnewofficerTitleheader);
        }

        public void IsNewOfficerTitleaddedsuccessfullyDisplayed(string message)
        {
            this.Driver.IsElementVisible(this.successfullMsg, this.nmofficerTitlesuccessfullmessage);
            this.Driver.IsExpectedTextMatchWithActualText(this.successfullMsg, message, this.nmofficerTitlesuccessfullmessage);
        }

        public void IsNewOfficerTitleaddedsuccessfullyDisplayedSoftAssertion(string message)
        {
            Verify.IsElementVisibleWithSoftAssertion(this.DriverContext, this.successfullMsg, this.nmofficerTitlesuccessfullmessage);
            Verify.IsExpectedTextMatchWithActualTextWithSoftAssertion(this.DriverContext, this.successfullMsg, message, this.nmofficerTitlesuccessfullmessage);
        }

        public void IsErrorMessageDisplayedOnOfficerTitleOnDuplicateEntryDisplayedSoftAssertion(string message)
        {
            Verify.IsElementVisibleWithSoftAssertion(this.DriverContext, this.errorMsg, this.nmofficerTitleErrormessage);
            Verify.IsExpectedTextMatchWithActualTextWithSoftAssertion(this.DriverContext, this.errorMsg, message, this.nmofficerTitleErrormessage);
        }

        public void IsUserAbleToEnterCommitteTitleinSearchBox(string officerTitleText)
        {
            Verify.WaitUntilElementIsNoLongerFoundWithSoftAssertion(this.DriverContext, this.loadbutton, this.nmsearchboxtxt, BaseConfiguration.LongTimeout);
            this.Driver.EnterText(this.searchboxtxt, officerTitleText, this.nmsearchboxtxt);
            this.Driver.EnterArrowDown(this.searchboxtxt);
        }

        public void IsUserAbleToEnterCommitteTitleinSearchBoxSoftAssertion(string officerTitleText)
        {
            Verify.WaitUntilElementIsFoundWithSoftAssertion(this.DriverContext, this.searchboxtxt, this.nmsearchboxtxt, BaseConfiguration.LongTimeout);
            Verify.EnterTextWithSoftAssertion(this.DriverContext, this.searchboxtxt, officerTitleText, this.nmsearchboxtxt);
            this.Driver.EnterArrowDown(this.searchboxtxt);
        }

        public void IsUserAbletoEnterOfficerTitleInTextBox(string officerTitleText)
        {
            this.Driver.IsElementVisible(this.officertitletext, this.nmofficertitletext);
            this.Driver.EnterText(this.officertitletext, officerTitleText, this.nmofficertitletext);
        }

        public void IsUserAbletoEnterOfficerTitleInTextBoxSoftAssertion(string officerTitleText)
        {
            Verify.IsElementVisibleWithSoftAssertion(this.DriverContext, this.officertitletext, this.nmofficertitletext);
            Verify.EnterTextWithSoftAssertion(this.DriverContext, this.officertitletext, officerTitleText, this.nmofficertitletext);
        }

        public void IsUserAbletoClickOnASTMStaffCheckBoxSoftAssertion(string expected)
        {
            Verify.IsElementVisibleWithSoftAssertion(this.DriverContext, this.astmStaffCheckBox.Format(expected), this.nmastmStaffCheckBox);
            Verify.IsElementClickableWithSoftAssertion(this.DriverContext, this.astmStaffCheckBox.Format(expected), this.nmastmStaffCheckBox);
        }

        public string IsUserAbletoViewApplicableToCommiteeCheckedSoftAssertion()
        {
            if (Verify.IsElementVisibleWithSoftAssertion(this.DriverContext, this.applicableToCommiteeCheck, this.nmapplicableToCommiteeCheck) == true)
            {
                return "True";
            }
            else
            {
                return "False";
            }
        }

        public void IsUserAbletoClickOnAllCheckBoxOnRosterMaintance(string expected)
        {
            Verify.IsElementVisibleHiddenModeWithSoftAssertion(this.DriverContext, this.rosterAllCheckBox.Format(expected), string.Format(this.nmrosterCheckBoxAll, expected));
            Verify.IsElementClickableinHiddenModeWithSoftAssertion(this.DriverContext, this.rosterAllCheckBox.Format(expected), string.Format(this.nmrosterCheckBoxAll, expected));
        }

        public int IsUserAbletoGetCountOfCheckedandUnCheckedCheckBoxOnRoster(string expected, string ischeckedorUnCheked, string index)
        {
            if (ischeckedorUnCheked == "Checked")
            {
                Verify.IsElementVisibleWithSoftAssertion(this.DriverContext, this.rosterIsCheckedCheckBox.Format(expected, index), string.Format(this.nmrosterIsCheckedCheckBox, expected));
                return Verify.GetCountOfCheckedUnCheckedCheckBoxSoftAssertion(this.DriverContext, this.rosterIsCheckedCheckBox.Format(expected, index), string.Format(this.nmrosterIsCheckedCheckBox, expected), ischeckedorUnCheked);
            }
            else
            {
                Verify.IsElementVisibleWithSoftAssertion(this.DriverContext, this.rosterIsUnCheckCheckBox.Format(expected, index), string.Format(this.nmrosterIsUnCheckCheckBox, expected));
                return Verify.GetCountOfCheckedUnCheckedCheckBoxSoftAssertion(this.DriverContext, this.rosterIsUnCheckCheckBox.Format(expected, index), string.Format(this.nmrosterIsUnCheckCheckBox, expected), ischeckedorUnCheked);
            }
        }

        public void IsUserAbletoViewRosterMaintananceLabelSoftAssertion(string expected)
        {
            Verify.IsElementVisibleWithSoftAssertion(this.DriverContext, this.labelRosterMaintenance, this.nmlabelRosterMaintenance);
            Verify.IsExpectedTextMatchWithActualTextWithSoftAssertion(this.DriverContext, this.labelRosterMaintenance, expected, this.nmlabelRosterMaintenance);
        }

        public void IsAddNewOfficerSaveButtonClickable()
        {
            this.Driver.IsElementVisible(this.addofficersavebutton, this.nmaddooficersavebutton);
            this.Driver.IsElementClickable(this.addofficersavebutton, this.nmaddooficersavebutton);
        }

        public void IsManageMembershipTypeClickable(string name)
        {
            this.Driver.IsElementVisible(this.managemembershiptype.Format(name), this.nmmanagememebershiptype);
            this.Driver.IsElementClickable(this.managemembershiptype.Format(name), this.nmmanagememebershiptype);
        }

        public void IsManageMembershipFAQsClickable(string name)
        {
            this.Driver.IsElementVisible(this.manageMembershipFAQs.Format(name), this.nmmanagemembershipFaqs);
            this.Driver.IsElementClickable(this.manageMembershipFAQs.Format(name), this.nmmanagemembershipFaqs);
        }

        public void IsManageMembershipDocumentsClickable(string name)
        {
            this.Driver.IsElementVisible(this.manageMembershipDocuments.Format(name), this.nmmanagemembershipdocument);
            this.Driver.IsElementClickable(this.manageMembershipDocuments.Format(name), this.nmmanagemembershipdocument);
        }

        public void IsOfficerTitleUpdateMessageDisplayedSuccessfully(string updateSuccessmessage)
        {
            this.Driver.IsElementVisible(this.successfullMsg, this.nmofficerTitlesuccessfullmessage);
            this.Driver.IsExpectedTextMatchWithActualText(this.successfullMsg, updateSuccessmessage, this.nmofficerTitlesuccessfullmessage);
        }

        /// <summary>
        /// Manage Memebership Classification option.
        /// </summary>
        /// <param name="name">Element Name.</param>
        public void IsManageMembershipClassificationsClickable(string name)
        {
            this.Driver.IsElementVisible(this.manageMemberClassifications.Format(name), this.nmmanagememeberclassifications);
            this.Driver.IsElementClickable(this.manageMemberClassifications.Format(name), this.nmmanagememeberclassifications);
        }

        public void IsUserAbletoViewNewlyAddedOfficeTitleInListButtomSoftAssertion(string expectedText)
        {
            string actualText = string.Empty;
            this.Driver.WaitUntilElementIsNoLongerFound(this.dimmerVisible, BaseConfiguration.LongTimeout, this.nmdimmerloading);
            try
            {
                int totalcount = this.IsuserabletogetListofofficerCards();
                actualText = this.IsuserabletoGetTextofOfficerTitle(totalcount - 1);
                Assert.AreEqual(expectedText, actualText.Trim());
                DriverContext.ExtentStepTest.Log(LogStatus.Pass, "To verify newly added Officer Title  ( " + expectedText + ") is Displayed at Bottom of Officer Title's List", "Newly added Officer Title ( " + expectedText + ") is Displayed at Bottom of Officer Title's List");
                Logger.Info("Expected text : (" + expectedText + ") and Actual text is (" + actualText + ")");
            }
            catch (Exception ex)
            {
                Logger.Error("Failed to verify text on " + expectedText + "with Actual Text " + actualText + " Due to exception: " + ex.ToString());
                DriverContext.ExtentStepTest.Log(LogStatus.Fail, "To verify newly added Officer Title Question ( " + expectedText + ") is Displayed at Bottom of Officer Title's List", "Newly added Officer Title ( " + expectedText + ") is not Displayed at Bottom of Officer Title's List but its displaying another Officer Title Question (" + actualText + ")");
            }
        }

        public int IsuserabletogetListofofficerCards()
        {
            IList<IWebElement> lstElements = this.Driver.GetElements(this.cardTitlesList);
            int elementcount = lstElements.Count;
            return elementcount;
        }

        public string IsuserabletoGetTextofOfficerTitle(int index)
        {
            this.Driver.WaitUntilElementIsFound(this.cardTitlesList, BaseConfiguration.LongTimeout);
            IList<IWebElement> lstElements = this.Driver.GetElements(this.cardTitlesList);
            string officertext = lstElements[index].GetTextContent();
            return officertext;
        }

        public void IsOfficerAlredyExistMessageDisplayed(string officerAlredyExistMessage)
        {
            this.Driver.IsElementVisible(this.officerTitleAlredyExistmsg, this.nmofficerTitleAlredyExistmsg);
            this.Driver.IsExpectedTextMatchWithActualText(this.officerTitleAlredyExistmsg, officerAlredyExistMessage, this.nmofficerTitleAlredyExistmsg);
        }

        public void IsuserabletoViewStatusfieldOfOfficerCard(string name)
        {
            this.Driver.IsElementVisible(this.cardTitleStatusfield.Format(name), this.nmofficerStatusfield);
        }

        public void IsuserabletoViewEditButtonOfOfficerCard(string name)
        {
            this.Driver.IsElementVisible(this.cardtitleEditbutton.Format(name), this.nmcardtitleEditbutton);
        }

        public void IsUserableToViewResponsibilitiesField(string name, string actualText)
        {
            try
            {
                string expectedText = this.Driver.GetText(this.cardTitleResponsibilities.Format(name));
                Assert.AreEqual(expectedText, actualText);
                DriverContext.ExtentStepTest.Log(LogStatus.Pass, "To verify Expected Value is matching with Actual text ", "The expected Value is " + expectedText + " and  actual value is " + actualText + " matching successfully");
                DriverContext.ExtentStepTest.Log(LogStatus.Pass, "To Verify Responsibilities field is visible", "Responsibilities field is visible successfully");
                Logger.Info("Expected text " + expectedText + " and Actual text is " + actualText);
            }
            catch (Exception ex)
            {
                Logger.Error("Failed Due to exception: " + ex.ToString());
                DriverContext.ExtentStepTest.Log(LogStatus.Fail, "To verify whether Responsibilities field is visible", " An exception occured as Responsibilities field is not visible");
                throw;
            }
        }

        public void IsUserAbleToViewNewOfficerIsByDefaultActiveStatus(string expected, string officerName)
        {
            var actualStatus = this.Driver.GetText(this.officerTitleStausActive.Format(officerName));
            this.Driver.IsExpectedTextMatchWithActualText(this.officerTitleStausActive.Format(officerName), expected, actualStatus);
        }

        public void IsOfficerTitleMandatory(string expected)
        {
            var actual = this.Driver.GetText(this.officerTitleMandatory);
            this.Driver.IsExpectedTextMatchWithActualText(this.officerTitleMandatory, expected, actual);
        }

        public void IsOfficerTitleMandatorySoftAssertion(string expected)
        {
            var actual = Verify.getTextIfElementIsVisibleWithSoftAssertion(this.DriverContext, this.officerTitleMandatory, this.nmofficerTitleMandatory);
            Verify.IsExpectedTextMatchWithActualTextWithSoftAssertion(this.DriverContext, this.officerTitleMandatory, expected, actual);
        }

        public void IsOfficerTitleResponsibiltyClicked()
        {
            this.Driver.IsElementVisible(this.responsibilityCheckbox, this.nmresponsibilityCheckbox);
            this.Driver.IsElementClickable(this.responsibilityCheckbox, this.nmresponsibilityCheckbox);
        }

        public void IsOfficerTitleResponsibiltyCallOutBoxVisible()
        {
            this.Driver.IsElementVisible(this.responsibilityCallOutBox, this.nmresponsibilityCallOutBox);
        }

        public void IsUserAbleToUpdatePermissionsInResponsibilityCallOutBox()
        {
            this.Driver.IsElementVisible(this.updateClassificationChkBox, this.nmupdateClassificationChkBox);
            this.Driver.IsElementClickable(this.updateClassificationChkBox, this.nmupdateClassificationChkBox);
            this.Driver.IsElementVisible(this.viewChkBox, this.nmviewChkBox);
            this.Driver.IsElementClickable(this.viewChkBox, this.nmviewChkBox);
            this.Driver.IsElementVisible(this.membershipReportChkBox, this.nmmembershipReportChkBox);
            this.Driver.IsElementClickable(this.membershipReportChkBox, this.nmmembershipReportChkBox);
        }

        public void IsRosterMaintenanceTextClickable()
        {
            this.Driver.MouseOverOnWebElementAndClick(this.rostermaintenacefield, this.nmrostermaintenacefield);
        }

        public void IsRosterPopupVisible()
        {
            this.Driver.IsElementVisible(this.popupRoster, this.nmRosterPopup);
        }

        public void IsRosterPermissionListVisible()
        {
            List<string> slist = new List<string> { " All", "View", "Update Classification", "Update Voting Rights", "Roster Report", "Meeting Attendance List", "Membership Report" };
            for (int i = 0; i < slist.Count; i++)
            {
                bool value = this.Driver.IsElementPresent(this.rosterPermissionList.Format(slist[i]), BaseConfiguration.LongTimeout);
                Verify.That(this.DriverContext, () => Assert.IsTrue(value), "To Verify" + slist[i] + "is  visible", slist[i] + "is  visible", slist[i] + " is not visible");
            }
        }

        public void ValidateCheckboxCountBeforeAndAfterClickSoftAssertion(Dictionary<string, string> parameters, ManageOfficerTitlePage manageOfficerTitlePage, int rostercount, int rosterreportcount)
        {
            int beforeRosterclick = manageOfficerTitlePage.IsUserAbletoGetCountOfCheckedandUnCheckedCheckBoxOnRoster(parameters["rosterAllCheckbox"], "Unchecked", "1");
            string verifyingMessageRosterBeforeClick = "To verify count of the unChecked Box  Under Roster before selecting  check Box (All) as Expected (" + rostercount + ")";
            string messageWhenPassedRosterBeforeClick = "Count of the unChecked Box  Under Roster before selecting  check Box (All) as Expected (" + rostercount + ")";
            string messageWhenFailedRosterBeforeClick = "Count of the unChecked Box  Under Roster before selecting  check Box (All) is not  as  Expected (" + rostercount + ") but showing actual Count" + beforeRosterclick;
            Verify.That(this.DriverContext, () => Assert.AreEqual(beforeRosterclick, rostercount), verifyingMessageRosterBeforeClick, messageWhenPassedRosterBeforeClick, messageWhenFailedRosterBeforeClick);

            manageOfficerTitlePage.IsUserAbletoClickOnAllCheckBoxOnRosterMaintance(parameters["rosterAllCheckbox"]);
            int afterRosterclick = manageOfficerTitlePage.IsUserAbletoGetCountOfCheckedandUnCheckedCheckBoxOnRoster(parameters["rosterAllCheckbox"], "Checked", "1");

            string verifyingMessageRosterAfterclick = "To verify count of the Checked Box  Under Roster after selecting  check Box (All) as Expected (" + rostercount + ")";
            string messageWhenPassedRosterAfterclick = "Count of the Checked Box  Under Roster after selecting  check Box (All) as Expected (" + rostercount + ")";
            string messageWhenFailedRosterAfterclick = "Count of the Checked Box  Under Roster after selecting  check Box (All) is not  as  Expected (" + rostercount + ") but showing actual Count" + afterRosterclick;

            Verify.That(this.DriverContext, () => Assert.AreEqual(afterRosterclick, rostercount), verifyingMessageRosterAfterclick, messageWhenPassedRosterAfterclick, messageWhenFailedRosterAfterclick);

            int beforeRosterReportclick = manageOfficerTitlePage.IsUserAbletoGetCountOfCheckedandUnCheckedCheckBoxOnRoster(parameters["rosterReportAllCheckbox"], "Unchecked", "2");
            string verifyingMessageRosterReportBeforeClick = "To verify count of the unChecked Box  Under Roster Report before selecting  check Box (All) as Expected (" + rosterreportcount + ")";
            string messageWhenPassedRosterReportBeforeClick = "Count of the unChecked Box  Under Roster Report before selecting  check Box (All) as Expected (" + rosterreportcount + ")";
            string messageWhenFailedRosterReportBeforeClick = "Count of the unChecked Box  Under Roster Report before selecting  check Box (All) is not  as  Expected (" + rosterreportcount + ") but showing actual Count" + beforeRosterReportclick;

            Verify.That(this.DriverContext, () => Assert.AreEqual(beforeRosterReportclick, rosterreportcount), verifyingMessageRosterReportBeforeClick, messageWhenPassedRosterReportBeforeClick, messageWhenFailedRosterReportBeforeClick);

            manageOfficerTitlePage.IsUserAbletoClickOnAllCheckBoxOnRosterMaintance(parameters["rosterReportAllCheckbox"]);
            int afterRosterReportclick = manageOfficerTitlePage.IsUserAbletoGetCountOfCheckedandUnCheckedCheckBoxOnRoster(parameters["rosterReportAllCheckbox"], "Checked", "1");
            string verifyingMessageRosterReportAfterclick = "To verify count of the Checked Box  Under Roster Report after selecting  check Box (All) as Expected (" + rosterreportcount + ")";
            string messageWhenPassedRosterReportAfterclick = "Count of the Checked Box  Under Roster Report after selecting  check Box (All) as Expected (" + rosterreportcount + ")";
            string messageWhenFailedRosterReportAfterclick = "Count of the Checked Box  Under Roster Report after selecting  check Box (All) is not  as  Expected (" + rosterreportcount + ") but showing actual Count" + afterRosterReportclick;

            Verify.That(this.DriverContext, () => Assert.AreEqual(afterRosterReportclick, rosterreportcount), verifyingMessageRosterReportAfterclick, messageWhenPassedRosterReportAfterclick, messageWhenFailedRosterReportAfterclick);
        }
    }
}