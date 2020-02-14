// <copyright file="MemebershipManagementPage.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace MCS.Test.Automation.Tests.PageObjects.PageObjects.MCS
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using global::MCS.Test.Automation.Common;
    using global::MCS.Test.Automation.Common.Extensions;
    using global::MCS.Test.Automation.Common.Helpers;
    using global::MCS.Test.Automation.Common.Types;
    using global::MCS.Test.Automation.Tests.PageObjects;
    using NLog;
    using NUnit.Framework;
    using RelevantCodes.ExtentReports;

    public class MemebershipManagementPage : ProjectPageBase
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();
        private readonly ElementLocator
 customerLogo = new ElementLocator(Locator.CssSelector, "img.ui.image");

        private readonly ElementLocator
            pageRefresh = new ElementLocator(Locator.XPath, "//body");

        private readonly ElementLocator
membershipname = new ElementLocator(Locator.Name, "MembershipTypeName");

        private readonly ElementLocator
membershipfee = new ElementLocator(Locator.Name, "FeeAmount");

        private readonly ElementLocator
nextbutton = new ElementLocator(Locator.CssSelector, "button.ui.primary.button");

        private readonly ElementLocator
          successfullMsg = new ElementLocator(Locator.CssSelector, "div.ui.compact.message.success div.content p");

        private readonly ElementLocator
savebutton = new ElementLocator(Locator.CssSelector, "button.ui.primary.button");

        private readonly ElementLocator
     managemembershiptype = new ElementLocator(Locator.CssSelector, "ul > li:nth-child(1)");

        private readonly ElementLocator
addmemebershiptype = new ElementLocator(Locator.CssSelector, "button.ui.secondary.button");

        private readonly ElementLocator
          membershipList = new ElementLocator(Locator.CssSelector, "table.customTable.memberShipTable tbody tr td a");

        private readonly ElementLocator
            renewalDropDown = new ElementLocator(Locator.Name, "RenewalPeriodId");

        private readonly ElementLocator
            renewalDropDownDiv = new ElementLocator(Locator.CssSelector, "div.visible.menu.transition div.item");

        private readonly ElementLocator
            addMembershipTypeNameError = new ElementLocator(Locator.XPath, "//span[contains(text(), 'Membership Name should be letters A to Z.')]");

        private readonly ElementLocator
            addMembershipFeeError = new ElementLocator(Locator.XPath, "//span[contains(text(), 'Membership Fee should be numeric.')]");

        private readonly ElementLocator
            membershipTypeNameLink = new ElementLocator(Locator.CssSelector, "table.customTable.memberShipTable tbody tr td a");

        private readonly ElementLocator
            membershipTypeSummary = new ElementLocator(Locator.XPath, "//label[text()='Summary']/following-sibling::div//div[@class='DraftEditor-root']/div/div");

        private readonly ElementLocator
    membershipTypeBenefits = new ElementLocator(Locator.XPath, "//label[text()='Benefits']/following-sibling::div//div[@class='DraftEditor-root']/div/div");

        private readonly ElementLocator
    membershipTypeDescription = new ElementLocator(Locator.XPath, "//label[text()='Description']/following-sibling::div//div[@class='DraftEditor-root']/div/div");

        private readonly ElementLocator
           membershipTypeList = new ElementLocator(Locator.CssSelector, "table.customTable.memberShipTable tbody tr td a");

        private readonly ElementLocator
            membershipfieldtext = new ElementLocator(Locator.XPath, " //*[contains(text(),'{0}')]");

        private readonly ElementLocator
            membershipNameColumn = new ElementLocator(Locator.CssSelector, "th.membershipName");

        private readonly ElementLocator
            membershipFeeColumn = new ElementLocator(Locator.CssSelector, "th.membershipFee");

        private readonly ElementLocator
            enabledColumn = new ElementLocator(Locator.CssSelector, "th.isEnabled");

        private readonly ElementLocator
            pagerefresh = new ElementLocator(Locator.XPath, "//body");

        private readonly ElementLocator
            membershipTypeDetails = new ElementLocator(Locator.CssSelector, "span.titleInfo");

        private readonly ElementLocator
            uncheckedCheckBoxSuppressFeeRenewalEmail = new ElementLocator(Locator.XPath, "//div[@class='ui checkbox']/label[contains(.,'{0}')]");

        private readonly ElementLocator
           checkedCheckBoxSuppressFeeRenewalEmail = new ElementLocator(Locator.XPath, "//div[@class='ui checked checkbox']/label[contains(.,'{0}')]");

        private readonly ElementLocator
           settingTab = new ElementLocator(Locator.XPath, "//a[@class='item'][contains(.,'Settings')]");

        private readonly ElementLocator
          editBtn = new ElementLocator(Locator.XPath, "//i[@class='pencil icon']");

        private readonly ElementLocator
          popUpOkBtn = new ElementLocator(Locator.XPath, " //button[contains(.,'Ok')]");

        private readonly ElementLocator
         popUpContent = new ElementLocator(Locator.CssSelector, "div.content");

        private string nmmanagememebershiptype = "Manage Membership Type option";
        private string nmPopupDiv = "Popup";
        private string nmpopUpOkBtn = "Popup Ok Button";
        private string nmEditButton = "Edit Button";
        private string nmsettingTab = "Setting Tab";
        private string nmuncheckedCheckBoxSuppressFeeRenewalEmail = "CheckBox : {0}";
        private string nmMTNamefield = "Membership Type Name field";
        private string nmMTFeefield = "Membership Type Fee field";
        private string nmMTSummaryfield = "Membership Type summary field";
        private string nmMTBenefitsfield = "Membership Type Benefits field";
        private string nmMTDescriptionfield = "Membership Type description field";
        private string nmnextbutton = "Next button";
        private string nmmembershiptypeList = "Membership Type from List of records";
        private string nmMembershipTypeSummary = "Membership Type Summary Field";
        private string nmMembershipTypeBenefits = "Membership Type Benefits Field";
        private string nmMembershipTypeDescription = "Membership Type Description  Field";
        private string nmrenewaldropdown = "Renewal Period Drop Down";
        private string nmcustomerlogo = "Customer logo";
        private string nmaddmembershiptype = "Add Membership Type Button";
        private string nmmembershipname = "Membership Name field";
        private string nmmembershipfee = "Membership Fee field";
        private string nmsuccessfullmsg = "Membership Type Details save Successfully";
        private string nmsavebtn = "Save button";
        private string nmPopupMsg = "Update will be applicable to only those records which are not associated with this Membership Type yet.";

        public MemebershipManagementPage(DriverContext driverContext)
            : base(driverContext)
        {
        }

        public void IsMembershipNameAddedIntoResultsList()
        {
            throw new NotImplementedException();
        }

        public void IsSuppressFeeRenewalEmailCheckBoxClickable(string name)
        {
            this.Driver.IsElementVisible(this.uncheckedCheckBoxSuppressFeeRenewalEmail.Format(name), string.Format(this.nmuncheckedCheckBoxSuppressFeeRenewalEmail, name));
            this.Driver.IsElementClickable(this.uncheckedCheckBoxSuppressFeeRenewalEmail.Format(name), string.Format(this.nmuncheckedCheckBoxSuppressFeeRenewalEmail, name));
            this.Driver.WaitForPageLoad();
        }

        public void IsSuppressFeeRenewalPrintCheckBoxClickable(string name)
        {
            this.Driver.IsElementVisible(this.uncheckedCheckBoxSuppressFeeRenewalEmail.Format(name), string.Format(this.nmuncheckedCheckBoxSuppressFeeRenewalEmail, name));
            this.Driver.IsElementClickable(this.uncheckedCheckBoxSuppressFeeRenewalEmail.Format(name), string.Format(this.nmuncheckedCheckBoxSuppressFeeRenewalEmail, name));
            this.Driver.WaitForPageLoad();
        }

        public void VerifySuppressFeeRenewalEmailCheckBoxCheckedClickable(string name)
        {
            this.Driver.IsElementVisible(this.checkedCheckBoxSuppressFeeRenewalEmail.Format(name), string.Format(this.nmuncheckedCheckBoxSuppressFeeRenewalEmail, name));
            this.Driver.IsElementClickable(this.checkedCheckBoxSuppressFeeRenewalEmail.Format(name), string.Format(this.nmuncheckedCheckBoxSuppressFeeRenewalEmail, name));
            this.Driver.WaitForPageLoad();
        }

        public void VerifySuppressFeeRenewalPrintCheckBoxCheckedClickable(string name)
        {
            this.Driver.IsElementVisible(this.checkedCheckBoxSuppressFeeRenewalEmail.Format(name), string.Format(this.nmuncheckedCheckBoxSuppressFeeRenewalEmail, name));
            this.Driver.IsElementClickable(this.checkedCheckBoxSuppressFeeRenewalEmail.Format(name), string.Format(this.nmuncheckedCheckBoxSuppressFeeRenewalEmail, name));
            this.Driver.WaitForPageLoad();
        }

        public void IsSettingTabClickable()
        {
            this.Driver.IsElementVisible(this.settingTab, this.nmsettingTab);
            this.Driver.IsElementClickable(this.settingTab, this.nmsettingTab);
            this.Driver.WaitForPageLoad();
        }

        public void IsPopUpOkButtonClickable()
        {
            this.Driver.IsElementVisible(this.popUpContent, this.nmPopupDiv);
            this.Driver.IsExpectedTextMatchWithActualText(this.popUpContent, this.nmPopupMsg);
            this.Driver.IsElementVisible(this.popUpOkBtn, this.nmpopUpOkBtn);
            this.Driver.IsElementClickable(this.popUpOkBtn, this.nmpopUpOkBtn);
            this.Driver.WaitForPageLoad();
        }

        public void IsSettingTabEditButtonClickable()
        {
            this.Driver.WaitUntilElementIsFound(this.editBtn, BaseConfiguration.LongTimeout);
            var webElement = this.Driver.GetElement(this.editBtn);
            this.Driver.IsElementVisible(this.editBtn, this.nmEditButton);
            this.Driver.JavaScriptClick(webElement, this.nmEditButton);
            this.Driver.WaitForPageLoad();
        }

        public void IsCustomerLogoDisplayed()
        {
            this.Driver.IsElementVisible(this.customerLogo, this.nmcustomerlogo);
        }

        public void IsAddMemberShipTypeButtonClickable()
        {
            this.Driver.WaitUntilElementIsFound(this.addmemebershiptype, BaseConfiguration.LongTimeout);
            var webElement = this.Driver.GetElement(this.addmemebershiptype);
            this.Driver.IsElementVisible(this.addmemebershiptype, this.nmaddmembershiptype);
            this.Driver.JavaScriptClick(webElement, this.nmaddmembershiptype);
            this.Driver.WaitForPageLoad();
        }

        public void IsManageMembershipTypeClickable()
        {
            this.Driver.IsElementVisible(this.managemembershiptype, this.nmmanagememebershiptype);
            this.Driver.IsElementClickable(this.managemembershiptype, this.nmmanagememebershiptype);
        }

        public void EnterMembershipFee(string name)
        {
            this.Driver.IsElementVisible(this.membershipfee.Format(name), this.nmmembershipfee);
            this.Driver.EnterText(this.membershipfee.Format(name), name, this.nmmembershipfee);
        }

        public void EnterMembershipName(string name)
        {
            this.Driver.WaitForPageLoad();
            this.Driver.IsElementVisible(this.membershipname.Format(name), this.nmmembershipname);
            this.Driver.EnterText(this.membershipname.Format(name), name, this.nmmembershipname);
        }

        public void IsRenewalPeriodDropDownSelected(string renewalPeriod)
        {
            this.Driver.IsElementClickable(this.renewalDropDown, this.nmrenewaldropdown);
            this.Driver.IsElementClickableFromListofElementWithText(this.renewalDropDownDiv, renewalPeriod);
        }

        public void IsNextButtonClickable()
        {
            this.Driver.WaitUntilElementIsFound(this.nextbutton, BaseConfiguration.LongTimeout);
            var webElement = this.Driver.GetElement(this.nextbutton);
            this.Driver.IsElementVisible(this.nextbutton, this.nmnextbutton);
            this.Driver.JavaScriptClick(webElement, this.nmnextbutton);
        }

        public void IsSuccessfullMessageDisplayed()
        {
            this.Driver.WaitUntilElementIsFound(this.successfullMsg, 90);
            this.Driver.IsElementVisibleWithSoftAssertion(this.successfullMsg, this.nmsuccessfullmsg);
            this.Driver.PageRefresh(this.pagerefresh);
        }

        public void IsUserAbleToClickonManageMembershipTypeFromList(string type)
        {
            this.Driver.WaitForPageLoad();
            this.Driver.WaitUntilElementIsFound(this.membershipList, BaseConfiguration.LongTimeout);
            this.Driver.IsElementClickableFromListofElementWithText(this.membershipList, type);
        }

        public void IsManageMembershipTypeDetailsAreDisplayedInNonEditableMode(string titleInfo)
        {
            this.Driver.WaitForPageLoad();
            this.AreElementsAreNonEditableMode(titleInfo);
        }

        public bool AreElementsAreNonEditableMode(string titleInfo)
        {
            this.Driver.WaitUntilElementIsFound(this.membershipTypeDetails, 90);
            var getELements = this.Driver.GetElements(this.membershipTypeDetails);
            foreach (var item in getELements)
            {
                if (item.GetAttribute("class").Contains(titleInfo))
                {
                    DriverContext.ExtentStepTest.Log(LogStatus.Pass, "To verify " + titleInfo + " are non editable Mode ", titleInfo + " are visible Non Editable mode successfully");
                    Logger.Info(titleInfo + " are non editable successfully");
                    return true;
                }
                else
                {
                    DriverContext.ExtentStepTest.Log(LogStatus.Fail, "To verify " + titleInfo + " are  editable Mode ", titleInfo + " are visible  Editable mode successfully");
                    Logger.Info(titleInfo + " are editable successfully");
                    return false;
                }
            }

            return false;
        }

        public void IsSaveButtonClickable()
        {
            this.Driver.WaitUntilElementIsFound(this.savebutton, BaseConfiguration.LongTimeout);
            var webElement = this.Driver.GetElement(this.savebutton);
            this.Driver.IsElementVisible(this.savebutton, this.nmsavebtn);
            this.Driver.JavaScriptClick(webElement, this.nmsavebtn);
        }

        public void AreAddManagementTypeValidationMessagesDisplayed(string expectedMembershipNameError)
        {
            this.Driver.WaitUntilElementIsFound(this.addMembershipTypeNameError, BaseConfiguration.LongTimeout);
            var actualMembershipNameerrorMessage = this.Driver.GetText(this.addMembershipTypeNameError);
            this.Driver.IsExpectedTextMatchWithActualText(this.addMembershipTypeNameError, expectedMembershipNameError, actualMembershipNameerrorMessage);
        }

        public void AreAddMembershipFeeValidationMessagesDisplayed(string expectedMembershipFeeError)
        {
            this.Driver.WaitUntilElementIsFound(this.addMembershipFeeError, BaseConfiguration.LongTimeout);
            var actualMembershipFeeerrorMessage = this.Driver.GetText(this.addMembershipFeeError);
            this.Driver.IsExpectedTextMatchWithActualText(this.addMembershipFeeError, expectedMembershipFeeError, actualMembershipFeeerrorMessage);
        }

        public void IsUserAbleToViewManageMembershipTypeListInLifoOrder(int i, string expected)
        {
            this.Driver.PageRefresh(this.pageRefresh);

            // this.Driver.WaitUntilElementIsFound(this.membershipTypeNameLink, BaseConfiguration.LongTimeout);
            var actualMembership = this.Driver.GetText(this.membershipTypeNameLink);
            this.Driver.IsExpectedTextMatchWithActualTextToLower(this.membershipTypeNameLink, expected, actualMembership);
        }

        public void EnterMembershipTypeSummary(string name)
        {
            this.Driver.WaitForPageLoad();
            this.Driver.EnterText(this.membershipTypeSummary, name, this.nmMembershipTypeSummary);
        }

        public void EnterMembershipTypeBenefits(string name)
        {
            this.Driver.WaitForPageLoad();
            this.Driver.EnterText(this.membershipTypeBenefits, name, this.nmMembershipTypeBenefits);
        }

        public void EnterMembershipTypeDescription(string name)
        {
            this.Driver.WaitForPageLoad();
            this.Driver.EnterText(this.membershipTypeDescription, name, this.nmMembershipTypeDescription);
        }

        public void IsMembershipTypeRecordClickableFromList(string membershiptype)
        {
            this.Driver.WaitUntilElementIsFound(this.membershipTypeList, BaseConfiguration.LongTimeout);
            this.Driver.ClickOnMembershipTypeFromListofRecords(this.membershipTypeList, this.nmmembershiptypeList, membershiptype);
        }

        public void AddnewMembershipType(string membershipName, string membershipFee, string summarytext, string descText, string benefitsText)
        {
            this.IsAddMemberShipTypeButtonClickable();
            this.EnterMembershipName(membershipName);
            this.EnterMembershipFee(membershipFee);
            this.EnterMembershipTypeSummary(summarytext);
            this.EnterMembershipTypeBenefits(benefitsText);
            this.EnterMembershipTypeDescription(descText);
            this.IsNextButtonClickable();
            string renewalperiod = "Yearly";
            this.IsRenewalPeriodDropDownSelected(renewalperiod);
            this.IsSaveButtonClickable();
        }

        public void IsMembershipNamefieldNoneditable(string membershipNamefieldtext)
        {
            this.Driver.IsElementcontainsGivenattribute(this.membershipfieldtext.Format(membershipNamefieldtext), this.nmMTNamefield);
        }

        public void IsMembershipFeefieldNoneditable(string membershipFeefieldtext)
        {
            this.Driver.IsElementcontainsGivenattribute(this.membershipfieldtext.Format(membershipFeefieldtext), this.nmMTFeefield);
        }

        public void IsMembershipSummaryfieldNoneditable(string membershipSummaryfieldtext)
        {
            this.Driver.IsElementcontainsGivenattribute(this.membershipfieldtext.Format(membershipSummaryfieldtext), this.nmMTSummaryfield);
        }

        public void IsMembershipBenefitsfieldNoneditable(string membershipBenefitsfieldtext)
        {
            this.Driver.IsElementcontainsGivenattribute(this.membershipfieldtext.Format(membershipBenefitsfieldtext), this.nmMTBenefitsfield);
        }

        public void IsMembershipDescriptionfieldNoneditable(string membershipDescriptionfieldtext)
        {
            this.Driver.IsElementcontainsGivenattribute(this.membershipfieldtext.Format(membershipDescriptionfieldtext), this.nmMTDescriptionfield);
        }

        public void IsUserAbleToViewManageMembershipNameColumn(string expected)
        {
            var actualMembership = this.Driver.GetText(this.membershipNameColumn);
            this.Driver.IsExpectedTextMatchWithActualTextToLower(this.membershipNameColumn, expected, actualMembership);
        }

        public void IsUserAbleToViewManageMembershipFeeColumn(string expected)
        {
            var actualMembership = this.Driver.GetText(this.membershipFeeColumn);
            this.Driver.IsExpectedTextMatchWithActualTextToLower(this.membershipFeeColumn, expected, actualMembership);
        }

        public void IsUserAbleToViewManageMembershipEnabledColumn(string expected)
        {
            var actualMembership = this.Driver.GetText(this.enabledColumn);
            this.Driver.IsExpectedTextMatchWithActualTextToLower(this.enabledColumn, expected, actualMembership);
        }

        public void GetAddedMembershipTypeExistsInDB(string membershipName, List<string> columnnames, List<string> values)
        {
            DataSet resultTable = SqlHelper.ExecuteSqlCommandQuery(string.Format(SqlQuery.FunctionalAddMembershipType, membershipName));
            Dictionary<string, string> actualDict = SqlHelper.GetTableColumnValueInDictionary(resultTable.Tables[0], columnnames);
            Dictionary<string, string> expectedDict = SqlHelper.GenerateDictionaryfromLists(columnnames, values);
            Verify.CompareTwoDictionaryFromPageAndDBWithSoftAssertion(this.DriverContext, actualDict, expectedDict);
        }

        public void VerifyMembershipDataContainsinTable(string membershipName, List<string> columnnames, string key, string value)
        {
            DataSet listoftables = SqlHelper.ExecuteSqlCommandQuery(string.Format(SqlQuery.FunctionalVerifyMembershipTypeValuesEntered, membershipName));
            Dictionary<string, string> actualDict = SqlHelper.GetTableColumnValueInDictionary(listoftables.Tables[0], columnnames);
            string verifyMessage = "To verify " + key + " column contains " + value + " value in database";
            string passMessage = key + " column contains " + value + " value in database";
            string failMessage = "Error occcured while  verifying " + key + " column contains " + value + " value in database";
            Verify.That(this.DriverContext, () => Assert.True(actualDict[key].Contains(value)), verifyMessage, passMessage, failMessage);
        }

        public void GetNegativeMembershipTypeExistsInDB(string membershipName, List<string> columnnames, List<string> values)
        {
            DataSet resultTable = SqlHelper.ExecuteSqlCommandQuery(string.Format(SqlQuery.FunctionalNegativeAddMembershipType, membershipName));
            Dictionary<string, string> actualDict = SqlHelper.GetTableColumnValueInDictionary(resultTable.Tables[0], columnnames);
            Dictionary<string, string> expectedDict = SqlHelper.GenerateDictionaryfromLists(columnnames, values);
            Verify.CompareTwoDictionaryFromPageAndDBWithSoftAssertion(this.DriverContext, actualDict, expectedDict);
        }

        public void GetUpdatedMembershipTypeDetailsSectionExistsInDB(string membershipName, List<string> columnnames, List<string> values)
        {
            DataSet resultTable = SqlHelper.ExecuteSqlCommandQuery(string.Format(SqlQuery.FunctionalUpdatedMembershipTypeDetailsSection, membershipName));
            Dictionary<string, string> actualDict = SqlHelper.GetTableColumnValueInDictionary(resultTable.Tables[0], columnnames);
            Dictionary<string, string> expectedDict = SqlHelper.GenerateDictionaryfromLists(columnnames, values);
            Verify.CompareTwoDictionaryFromPageAndDBWithSoftAssertion(this.DriverContext, actualDict, expectedDict);
        }
    }
}
