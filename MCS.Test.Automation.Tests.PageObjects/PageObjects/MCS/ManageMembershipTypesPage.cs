// <copyright file="ManageMembershipTypesPage.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace MCS.Test.Automation.Tests.PageObjects.PageObjects.MCS
{
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

    public class ManageMembershipTypesPage : ProjectPageBase
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();
        private readonly ElementLocator
        pageRefresh = new ElementLocator(Locator.XPath, "//body");

        private readonly ElementLocator
         addmemebershiptype = new ElementLocator(Locator.CssSelector, "button.ui.secondary.button");

        private readonly ElementLocator
        membershipname = new ElementLocator(Locator.Name, "MembershipTypeName");

        private readonly ElementLocator
        membershipfee = new ElementLocator(Locator.Name, "FeeAmount");

        private readonly ElementLocator
        nextbutton = new ElementLocator(Locator.CssSelector, "button.ui.primary.button");

        private readonly ElementLocator
        radioButtonForLimitedMembers = new ElementLocator(Locator.XPath, "//label[text()='Limited']");

        private readonly ElementLocator
        radioButtonForLimitedCommittees = new ElementLocator(Locator.XPath, "//label[text()='Max. Number of Committees Permitted to Join']/..//label[text()='Limited']");

        private readonly ElementLocator
        textBoxforNumbers = new ElementLocator(Locator.XPath, "//input[@name='{0}']");

        private readonly ElementLocator
        membershipTypeSummary = new ElementLocator(Locator.XPath, "//label[text()='Summary']/following-sibling::div//div[@class='DraftEditor-root']/div/div");

        private readonly ElementLocator
         membershipTypeBenefits = new ElementLocator(Locator.XPath, "//label[text()='Benefits']/following-sibling::div//div[@class='DraftEditor-root']/div/div");

        private readonly ElementLocator
        membershipTypeDescription = new ElementLocator(Locator.XPath, "//label[text()='Description']/following-sibling::div//div[@class='DraftEditor-root']/div/div");

        private readonly ElementLocator
         errorMessageforfield = new ElementLocator(Locator.XPath, "//span[contains(text(), '{0}')]");

        private readonly ElementLocator
         checkboxLabelfield = new ElementLocator(Locator.XPath, "//div[@class='ui checkbox']/label[contains(.,'{0}')]");

        private readonly ElementLocator
        renewalDropDown = new ElementLocator(Locator.Name, "RenewalPeriodId");

        private readonly ElementLocator
            renewalPeriodDefaultText = new ElementLocator(Locator.XPath, "//div[@name='RenewalPeriodId']/div");

        private readonly ElementLocator
         renewalDropDownDiv = new ElementLocator(Locator.CssSelector, "div.visible.menu.transition div.item");

        private readonly ElementLocator
         checkedCheckBoxSuppressFeeRenewalEmail = new ElementLocator(Locator.XPath, "//div[@class='ui checked checkbox']/label[contains(.,'{0}')]");

        private readonly ElementLocator
        savebutton = new ElementLocator(Locator.CssSelector, "button.ui.primary.button");

        private readonly ElementLocator
        updatebutton = new ElementLocator(Locator.XPath, "//div[@class='tabWithBtn']//button[text()='Update']");

        private readonly ElementLocator
            membershipTypetab = new ElementLocator(Locator.XPath, "//div/a[text()='Settings']");

        private readonly ElementLocator
        membershipTypeNameLink = new ElementLocator(Locator.CssSelector, "table.customTable.memberShipTable tbody tr td a");

        private readonly ElementLocator
          radioButtonforMaxNoOfMembers = new ElementLocator(Locator.CssSelector, "div.ui.checked.radio.checkbox");

        private readonly ElementLocator
         membershipRecord = new ElementLocator(Locator.XPath, "//a[text()='{0}']");

        private readonly ElementLocator
        editMemeberShipTypeRecord = new ElementLocator(Locator.CssSelector, "i.pencil.icon");

        private readonly ElementLocator
          membershipTypefields = new ElementLocator(Locator.XPath, "//label[text()='{0}']/../span/p");

        private readonly ElementLocator
       radioButtonStatus = new ElementLocator(Locator.XPath, "//label[text()='{0}']/..//label[text()='{1}']/..");

        private readonly ElementLocator
            pageHeading = new ElementLocator(Locator.XPath, "//h2[text()='{0}']");

        private readonly ElementLocator
            radioButtonStausafterUpdate = new ElementLocator(Locator.XPath, "//label[text()='{0}']/../span");

        private readonly ElementLocator
          loadbutton = new ElementLocator(Locator.XPath, "//*[@class='ui active transition visible dimmer']");

        private string nmaddmembershiptype = "Add Membership Type Button";
        private string nmmembershipname = "Membership Name field";
        private string nmMembershipTypeSummary = "Membership Type Summary Field";
        private string nmMembershipTypeBenefits = "Membership Type Benefits Field";
        private string nmMembershipTypeDescription = "Membership Type Description  Field";
        private string nmsavebtn = "Save button";
        private string nmupdatebtn = "Update button";
        private string nmmembershipTypeTab = " Settings tab";
        private string nmnextbutton = "Next button";
        private string nmerrorMessage = "Error Message Text";
        private string nmmembershipfee = "Membership Fee field";
        private string nmcheckboxLabelName = "CheckBox : {0}";
        private string nmrenewaldropdown = "Renewal Period Drop Down";
        private string nmradioButtonForLimitedMembers = "Max No Of Members Permitted Radio Button";

        // private string nmradioButtonForLimitedCommittees = "Max No Of committees Permitted Radio Button";
        private string nmtextBoxforNumbers = "TextBox to Enter Members Permitted";
        private string nmmembershipRecord = "membership type record ";
        private string nmeditMemeberShipTypeRecord = "Edit button";
        private string nmrenewalDefaultText = "Renewal Period Drop Down default Text";
        private string nmdimmerloading = "dimmer loading";

        public ManageMembershipTypesPage(DriverContext driverContext)
          : base(driverContext)
        {
        }

        public void IsAddMemberShipTypeButtonClickable()
        {
            this.Driver.WaitUntilElementIsFound(this.addmemebershiptype, BaseConfiguration.LongTimeout);
            var webElement = this.Driver.GetElement(this.addmemebershiptype);
            this.Driver.IsElementVisible(this.addmemebershiptype, this.nmaddmembershiptype);
            this.Driver.JavaScriptClick(webElement, this.nmaddmembershiptype);
            this.Driver.WaitForPageLoad();
        }

        public void EnterMembershipName(string name)
        {
            this.Driver.WaitForPageLoad();
            Verify.IsElementVisibleWithSoftAssertion(this.DriverContext, this.membershipname.Format(name), this.nmmembershipname);
            Verify.EnterTextWithSoftAssertion(this.DriverContext, this.membershipname.Format(name), name, this.nmmembershipname);
        }

        public void EnterMembershipFee(string name)
        {
            Verify.IsElementVisibleWithSoftAssertion(this.DriverContext, this.membershipfee.Format(name), this.nmmembershipfee);
            Verify.EnterTextWithSoftAssertion(this.DriverContext, this.membershipfee.Format(name), name, this.nmmembershipfee);
        }

        public void IsValidationMessageDisplayed(string errorMessage)
        {
            Verify.WaitUntilElementIsFoundWithSoftAssertion(this.DriverContext, this.errorMessageforfield.Format(errorMessage), this.nmerrorMessage, BaseConfiguration.LongTimeout);
            var actualMembershipNameerrorMessage = Verify.getTextIfElementIsVisibleWithSoftAssertion(this.DriverContext, this.errorMessageforfield.Format(errorMessage), this.nmerrorMessage);
            Verify.IsExpectedTextMatchWithActualTextWithSoftAssertion(this.DriverContext, this.errorMessageforfield.Format(errorMessage), errorMessage, actualMembershipNameerrorMessage);
        }

        public void IsNextButtonClickable()
        {
            this.Driver.WaitUntilElementIsFound(this.nextbutton, BaseConfiguration.LongTimeout);
            var webElement = this.Driver.GetElement(this.nextbutton);
            Verify.IsElementVisibleWithSoftAssertion(this.DriverContext, this.nextbutton, this.nmnextbutton);
            this.Driver.JavaScriptClick(webElement, this.nmnextbutton);
            this.Driver.WaitForPageLoad();
        }

        public void IsSaveButtonClickable()
        {
            this.Driver.WaitUntilElementIsFound(this.savebutton, BaseConfiguration.LongTimeout);
            var webElement = this.Driver.GetElement(this.savebutton);
            Verify.IsElementVisibleWithSoftAssertion(this.DriverContext, this.savebutton, this.nmsavebtn);
            this.Driver.JavaScriptClick(webElement, this.nmsavebtn);
        }

        public void IsUpdateButtonClickable()
        {
            this.Driver.WaitUntilElementIsFound(this.updatebutton, BaseConfiguration.LongTimeout);
            this.Driver.IsElementVisible(this.updatebutton, this.nmupdatebtn);
            this.Driver.JavaScriptClick(this.Driver.GetElement(this.updatebutton), this.nmupdatebtn);
        }

        public string GetMembershipName()
        {
            string text = this.Driver.GetValue(this.membershipname);
            return text;
        }

        public void IsMembershipTypeTabClickable()
        {
            var webElement = this.Driver.GetElement(this.membershipTypetab);
            this.Driver.JavaScriptClick(webElement, this.nmmembershipTypeTab);
            this.Driver.WaitForPageLoad();
        }

        public void EnterMembershipTypeSummary(string name)
        {
            this.Driver.WaitForPageLoad();
            Verify.EnterTextWithSoftAssertion(this.DriverContext, this.membershipTypeSummary, name, this.nmMembershipTypeSummary);
        }

        public void EnterMembershipTypeBenefits(string name)
        {
            this.Driver.WaitForPageLoad();
            Verify.EnterTextWithSoftAssertion(this.DriverContext, this.membershipTypeBenefits, name, this.nmMembershipTypeBenefits);
        }

        public void EnterMembershipTypeDescription(string name)
        {
            this.Driver.WaitForPageLoad();
            Verify.EnterTextWithSoftAssertion(this.DriverContext, this.membershipTypeDescription, name, this.nmMembershipTypeDescription);
        }

        public void IsMaxNoOfMembersPermittedslectedbyDefault(string name)
        {
            string verifyMessage = "To Verify  Radio button - " + name + " is checked by default";
            string passMessage = "Radio button is checked by default for" + name;
            string failMessage = "Error occcured while  verifying  Radio button -" + name + " is checked by default";
            var webElement = this.Driver.GetElement(this.radioButtonforMaxNoOfMembers);
            webElement.GetAttribute("class").Contains("checked");
            Verify.That(this.DriverContext, () => Assert.IsTrue(webElement.GetAttribute("class").Contains("checked")), verifyMessage, passMessage, failMessage);
        }

        public void IsSuppressFeeRenewalPrintCheckBoxClickable(string name)
        {
            Verify.IsElementVisibleWithSoftAssertion(this.DriverContext, this.checkboxLabelfield.Format(name), string.Format(this.nmcheckboxLabelName, name));
            Verify.IsElementClickableWithSoftAssertion(this.DriverContext, this.checkboxLabelfield.Format(name), string.Format(this.nmcheckboxLabelName, name));
            Verify.WaitForPageLoadWithSoftAssertion(this.DriverContext);
        }

        public void IsRenewalPeriodDropDownSelected(string renewalPeriod)
        {
            Verify.IsElementClickableWithSoftAssertion(this.DriverContext, this.renewalDropDown, this.nmrenewaldropdown);
            Verify.IsElementClickableFromListofElementWithTextWithSoftAssertion(this.DriverContext, this.renewalDropDownDiv, renewalPeriod);
        }

        public void IsLimitedPersonsSelected()
        {
            Verify.IsElementClickableWithSoftAssertion(this.DriverContext, this.radioButtonForLimitedMembers, this.nmradioButtonForLimitedMembers);
        }

        public void IsLimitedPersonsSelectedEnterNumber(string num, string textBoxname)
        {
            Verify.EnterTextWithSoftAssertion(this.DriverContext, this.textBoxforNumbers.Format(textBoxname), num, this.nmtextBoxforNumbers);
        }

        public void IsLimitedCommitteesSelected()
        {
            Verify.IsElementClickableWithSoftAssertion(this.DriverContext, this.radioButtonForLimitedCommittees, this.nmradioButtonForLimitedMembers);
        }

        public void IsUserAbletoVerifyDefaultRenwalPeriod(string expectedText)
        {
            string verifyMessage = "To verify default text of RenewalPeriod Dropdown field";
            string passMessage = "Default text [" + expectedText + "]  is verified successfully for RenewalPeriod Dropdown field";
            string failMessage = "Error occcured while verifying default text [" + expectedText + "]  for Renewal Period Dropdown field";
            var actualText = Verify.getTextIfElementIsVisibleWithSoftAssertion(this.DriverContext, this.renewalPeriodDefaultText, this.nmrenewalDefaultText);
            Verify.That(this.DriverContext, () => Assert.AreEqual(actualText, expectedText), verifyMessage, passMessage, failMessage);
        }

        public void IsUserAbleToViewManageMembershipTypeListInLifoOrder(int i, string expected)
        {
            this.Driver.PageRefresh(this.pageRefresh);   // @defect raised for this issue
            var actualMembership = this.Driver.GetText(this.membershipTypeNameLink);
            Verify.IsExpectedTextMatchWithActualTextToLowerWithSoftAssertion(this.DriverContext, this.membershipTypeNameLink, expected, actualMembership);
        }

        public void IsMembershipTypeselectedBasedOnName(string membershipName)
        {
            Verify.IsElementVisibleWithSoftAssertion(this.DriverContext, this.membershipRecord.Format(membershipName), this.nmmembershipRecord);
            Verify.IsElementClickableWithSoftAssertion(this.DriverContext, this.membershipRecord.Format(membershipName), this.nmmembershipRecord);
        }

        public void IsMemebershipTypeRecordEditable()
        {
            Verify.IsElementVisibleWithSoftAssertion(this.DriverContext, this.editMemeberShipTypeRecord, this.nmeditMemeberShipTypeRecord);
            Verify.IsElementClickableWithSoftAssertion(this.DriverContext, this.editMemeberShipTypeRecord, this.nmeditMemeberShipTypeRecord);
        }

        public void IsUpdatedTextVisibleInMembershipTypeFields(string fieldName, string expectedText)
        {
            Verify.IsExpectedTextMatchWithActualTextWithSoftAssertion(this.DriverContext, this.membershipTypefields.Format(fieldName), expectedText: expectedText);
            DriverContext.ExtentStepTest.Log(LogStatus.Pass, "To verify" + fieldName + "field text after Updating record", "Updated text is displayed in " + fieldName + "field");
        }

        public string IsRadioButtonSelected(string radioBtnName, string lable1, string lable2, string name, string count)
        {
            Verify.WaitForPageLoadWithSoftAssertion(this.DriverContext);
            Verify.WaitUntilElementIsFoundWithSoftAssertion(this.DriverContext, this.radioButtonStatus.Format(radioBtnName, lable1), radioBtnName + " radio Button", BaseConfiguration.LongTimeout);
            var webElement = this.Driver.GetElement(this.radioButtonStatus.Format(radioBtnName, lable1));
            bool value = webElement.GetAttribute("class").Contains("checked");
            if (value)
            {
                Verify.WaitUntilElementIsFoundWithSoftAssertion(this.DriverContext, this.radioButtonStatus.Format(radioBtnName, lable2), radioBtnName + " radio Button", BaseConfiguration.LongTimeout);
                System.Threading.Thread.Sleep(5000);
                Verify.IsElementClickableWithSoftAssertion(this.DriverContext, this.radioButtonStatus.Format(radioBtnName, lable2), radioBtnName + " radio Button");
                System.Threading.Thread.Sleep(5000);
                this.IsLimitedPersonsSelectedEnterNumber(count, name);
                return lable2;
            }
            else
            {
                Verify.WaitUntilElementIsFoundWithSoftAssertion(this.DriverContext, this.radioButtonStatus.Format(radioBtnName, lable1), radioBtnName + " radio Button", BaseConfiguration.LongTimeout);
                System.Threading.Thread.Sleep(5000);
                Verify.IsElementClickableWithSoftAssertion(this.DriverContext, this.radioButtonStatus.Format(radioBtnName, lable1), radioBtnName + " radio Button");
                return lable1;
            }
        }

        public void VerifyPageTitle(string expectedText)
        {
            string verifyMessage = "To verify user is redirected to Membership Types page";
            string passMessage = "User is redirected to MembershipTypes Page successfully ";
            string failMessage = "Error occcured while user is redirected to Membership Types Page";
            string actualHeadingText = Verify.getTextIfElementIsVisibleWithSoftAssertion(this.DriverContext, this.pageHeading.Format(expectedText), expectedText + " heading");
            Verify.That(this.DriverContext, () => Assert.AreEqual(actualHeadingText, expectedText), verifyMessage, passMessage, failMessage);
        }

        public void VerifyValuesAfterUpdation(string name, string checkedBtnName, string label)
        {
            Verify.getTextIfElementIsVisibleWithSoftAssertion(this.DriverContext, this.radioButtonStausafterUpdate.Format(name), label);
            string actualValue = this.Driver.GetText(this.radioButtonStausafterUpdate.Format(name));
            string verifyMessage = "To verify user is displayed with updated details" + checkedBtnName + " in the Membershiptype record - " + label;
            string passMessage = "User is displayed with updated Membership details" + checkedBtnName + " in Membershiptype record - " + label;
            string failMessage = "Error occcured while verifying MembershipType record with updated details - " + label + " expected: " + checkedBtnName + "but actual was: " + actualValue;
            Verify.That(this.DriverContext, () => Assert.AreEqual(actualValue, checkedBtnName), verifyMessage, passMessage, failMessage);
        }

        public void GetMembershipTypeTabStatus(string tabName)
        {
            string verifyMessage = "To verify user is redirected to  " + tabName + " page";
            string passMessage = "User is redirected to  " + tabName + " page";
            string failMessage = "Error occcured while verifying user is redirected to - " + tabName + " page";
            var webElement = this.Driver.GetElement(this.membershipRecord.Format(tabName));
            this.Driver.WaitUntilElementIsNoLongerFound(this.loadbutton, BaseConfiguration.LongTimeout, this.nmdimmerloading);
            bool value = webElement.GetAttribute("class").Contains("active");
            Verify.That(this.DriverContext, () => Assert.IsTrue(value), verifyMessage, passMessage, failMessage);
        }

        public void GetUpdatedMembershipTypeSettingsSectionExistsInDB(string trueVal, string falseVal, string num, string membershipColName, string membershipName, string renewalPeriod, string checkedMemberslabelName, string checkedCommitteeslabelName, string maxmember, string maxcommittee, string limitedstring, string unlimitedstring, string unlimitedCommitteeColName, string unlimitedMembersColName)
        {
            List<string> columnList = new List<string>();
            List<string> valueList = new List<string>();
            columnList.Add(membershipColName);
            columnList.Add("RenewalPeriod");
            valueList.Add(membershipName);
            valueList.Add(renewalPeriod);
            if (checkedMemberslabelName == limitedstring && checkedCommitteeslabelName == limitedstring)
            {
                columnList.Add(maxmember);
                columnList.Add(maxcommittee);
                valueList.Add(num);
                valueList.Add(num);
                DataSet resultTable = SqlHelper.ExecuteSqlCommandQuery(string.Format(SqlQuery.FunctionalUpdatedMembershipTypeSettingsSection, membershipName));
                Dictionary<string, string> actualDict = SqlHelper.GetTableColumnValueInDictionary(resultTable.Tables[0], columnList);
                Dictionary<string, string> expectedDict = SqlHelper.GenerateDictionaryfromLists(columnList, valueList);
                Verify.CompareTwoDictionaryFromPageAndDBWithSoftAssertion(this.DriverContext, actualDict, expectedDict);
            }

            if (checkedMemberslabelName == unlimitedstring && checkedCommitteeslabelName == limitedstring)
            {
                columnList.Add(unlimitedMembersColName);
                columnList.Add(maxcommittee);
                valueList.Add(trueVal);
                valueList.Add(num);
                DataSet resultTable = SqlHelper.ExecuteSqlCommandQuery(string.Format(SqlQuery.FunctionalUpdatedMembershipTypeSettingsSection, membershipName));
                Dictionary<string, string> actualDict = SqlHelper.GetTableColumnValueInDictionary(resultTable.Tables[0], columnList);
                Dictionary<string, string> expectedDict = SqlHelper.GenerateDictionaryfromLists(columnList, valueList);
                Verify.CompareTwoDictionaryFromPageAndDBWithSoftAssertion(this.DriverContext, actualDict, expectedDict);
            }

            if (checkedMemberslabelName == limitedstring && checkedCommitteeslabelName == unlimitedstring)
            {
                columnList.Add(maxmember);
                columnList.Add(unlimitedCommitteeColName);
                valueList.Add(num);
                valueList.Add(falseVal);
                DataSet resultTable = SqlHelper.ExecuteSqlCommandQuery(string.Format(SqlQuery.FunctionalUpdatedMembershipTypeSettingsSection, membershipName));
                Dictionary<string, string> actualDict = SqlHelper.GetTableColumnValueInDictionary(resultTable.Tables[0], columnList);
                Dictionary<string, string> expectedDict = SqlHelper.GenerateDictionaryfromLists(columnList, valueList);
                Verify.CompareTwoDictionaryFromPageAndDBWithSoftAssertion(this.DriverContext, actualDict, expectedDict);
            }

            if (checkedMemberslabelName == unlimitedstring && checkedCommitteeslabelName == unlimitedstring)
            {
                columnList.Add(unlimitedMembersColName);
                columnList.Add(unlimitedCommitteeColName);
                valueList.Add(trueVal);
                valueList.Add(trueVal);
                DataSet resultTable = SqlHelper.ExecuteSqlCommandQuery(string.Format(SqlQuery.FunctionalUpdatedMembershipTypeSettingsSection, membershipName));
                Dictionary<string, string> actualDict = SqlHelper.GetTableColumnValueInDictionary(resultTable.Tables[0], columnList);
                Dictionary<string, string> expectedDict = SqlHelper.GenerateDictionaryfromLists(columnList, valueList);
                Verify.CompareTwoDictionaryFromPageAndDBWithSoftAssertion(this.DriverContext, actualDict, expectedDict);
            }
        }
    }
}
