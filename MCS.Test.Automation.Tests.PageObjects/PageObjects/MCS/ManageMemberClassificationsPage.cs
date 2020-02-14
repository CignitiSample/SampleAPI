// <copyright file="ManageMemberClassificationsPage.cs" company="PlaceholderCompany">
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
    using global::NUnit.Framework;
    using NLog;
    using RelevantCodes.ExtentReports;

    public class ManageMemberClassificationsPage : ProjectPageBase
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        private readonly ElementLocator
          countOfMemberClassifications = new ElementLocator(Locator.XPath, "//table[@class='customTable']//tr");

        private readonly ElementLocator
        pageRefresh = new ElementLocator(Locator.XPath, "//body");

        private readonly ElementLocator
            successmsg = new ElementLocator(Locator.CssSelector, "div.top-message div.ui.compact.message.success div.content p");

        private readonly ElementLocator
            addClassifficationTypebtn = new ElementLocator(Locator.CssSelector, "button.ui.secondary.button");

        private readonly ElementLocator
           classificationtxt = new ElementLocator(Locator.XPath, "//*[@placeholder='Classification Type']");

        private readonly ElementLocator
            descriptiontxt = new ElementLocator(Locator.XPath, "//*[@name='Description']");

        private readonly ElementLocator
            descriptionrichtxt = new ElementLocator(Locator.CssSelector, "div.field textarea#txtString");

        private readonly ElementLocator
            descriptionpopuptext = new ElementLocator(Locator.XPath, "//*[@name='description']");

        private readonly ElementLocator
            applicabletocommitte = new ElementLocator(Locator.XPath, "//*[@class='radioWrap']/div");

        private readonly ElementLocator
            classificationsavebtn = new ElementLocator(Locator.CssSelector, "button.ui.primary.button");

        private readonly ElementLocator
       classificationCancelbtn = new ElementLocator(Locator.CssSelector, "button.ui.button.cancel");

        private readonly ElementLocator
       classificationrequirederror = new ElementLocator(Locator.XPath, "//span[@class='errorMessage' and text()='Classification Type is required.']");

        private readonly ElementLocator
      colorErrorMsg = new ElementLocator(Locator.XPath, "//*[@class='errorMessage mt0' or @text='Color is required.']");

        private readonly ElementLocator
           classificationType = new ElementLocator(Locator.CssSelector, "td.appName a");

        private readonly ElementLocator
            classificationTypePopup = new ElementLocator(Locator.XPath, "//*[text()='Classification Type']/following::span[text()='{0}']");

        private readonly ElementLocator
            descriptionPopup = new ElementLocator(Locator.XPath, "//*[text()='Description']/following::span[text()='{0}']");

        private readonly ElementLocator
            isapplicableToAllcommitteFromPopup = new ElementLocator(Locator.XPath, "//*[text()='Applicable to all Committees']/following::span[text()='{0}']");

        private readonly ElementLocator
          classificationDescription = new ElementLocator(Locator.CssSelector, "td.appId");

        private readonly ElementLocator
           successfullMsg = new ElementLocator(Locator.CssSelector, "div.ui.compact.message.success div.content p");

        private readonly ElementLocator
          loadbutton = new ElementLocator(Locator.XPath, "//*[@class='ui active transition visible dimmer']");

        private readonly ElementLocator
         addColor = new ElementLocator(Locator.XPath, "//span[@name='AddColor']");

        private readonly ElementLocator
         colorSliderDiv = new ElementLocator(Locator.XPath, "//div[@class='hue-horizontal']/div");

        private readonly ElementLocator
            colordivgrid = new ElementLocator(Locator.XPath, "//*[@class='slider-picker ']/div[2]/div/div");

        private readonly ElementLocator
         colorNameInput = new ElementLocator(Locator.XPath, "//input[@name='customColor']");

        private readonly ElementLocator
        addColorNameOkButton = new ElementLocator(Locator.XPath, "//a[@class='addBtn ml10']");

        private readonly ElementLocator
        editbutton = new ElementLocator(Locator.CssSelector, "a.editBtn");

        private readonly ElementLocator
         errorMessageAddColor = new ElementLocator(Locator.XPath, "//span[@class='errorMessage mt0']");

        private readonly ElementLocator
         secondColorChange = new ElementLocator(Locator.XPath, "//div[@class='hue-horizontal']//div//div");

        private readonly ElementLocator
         memberclassificationrecord = new ElementLocator(Locator.CssSelector, "td.appName a");

        private string nmclassficationTypeFieldlength = "Classfication Type Field Length";
        private string nmclassficationDescriptionFieldlength = "Classfication Type Field Length";
        private string nmdivColorChange = "Click Div Change Color";
        private string nmaddColorNameOkButton = "Add Color Ok Button";
        private string nmerrorMessageAddColor = "Error Message Add Color";
        private string nmaddColor = "Add Color Button";
        private string nmcolorNameInput = "Color Name";
        private string nmsecondColor = "second Color";
        private string nmfirstColor = "first Color";
        private string nmAddClassificationTypeBtn = "Add Classification button";
        private string nmClassificationDescrition = "Classification Description Text Field";
        private string nmClassficationtxt = "Classfication Text field";
        private string nmclassificationsavebutton = "Save button";
        private string nmclassificationCancelbutton = "Cancel button";
        private string nmsuccessfullmsg = "Membership Classification added Successfully";
        private string nmapplicabletocommitte = "Applicable To All Committee";
        private string nmmemberclassfificationmsg = "Member Classification updated successfully";
        private string nmmemberclassificationtype = "Member Classification Type";
        private string nmmemberclassificationeditbutton = "Member Classification Edit Button";
        private string nmupdatesuccessfullmsg = "Member Classification updated successfully. message ";
        private string nmclassificationTypeFromList = "Member Classification Type";
        private string nmclassificationTypetxtPopup = "Member Classification Type text field in Popup";
        private string nmisapplicableToAllCommitte = "Is Applicable To All Committee check box";

        public ManageMemberClassificationsPage(DriverContext driverContext)
        : base(driverContext)
        {
        }

        public int IsUserableToGetClassificationCount()
        {
           var lstElements = this.Driver.GetElements(this.countOfMemberClassifications);
           int count = lstElements.Count;
           return count - 1;
        }

        public void IsMemberClassificationTypeFieldAcceptMaximumCharacter(string expectedValue, int classificationTypeLength)
        {
            int expectedlength = int.Parse(expectedValue);
            string verifyingMessage = "To verify expected " + this.nmclassficationTypeFieldlength + " is " + expectedlength;
            string messageWhenPassed = "Actual " + this.nmclassficationTypeFieldlength + " is " + classificationTypeLength + " matching with expected " + this.nmclassficationTypeFieldlength + " is " + expectedlength;
            string messageWhenFailed = "Expected Length is " + expectedlength + "Not Matching with Actual Length" + classificationTypeLength;
            Verify.That(this.DriverContext, () => Assert.AreEqual(expectedlength, classificationTypeLength), verifyingMessage, messageWhenPassed, messageWhenFailed);
        }

        public void IsMemberClassificationDescriptionFieldAcceptMaximumCharacter(string expectedValue)
        {
            string classificationDescriptionLength = this.Driver.GetText(this.descriptiontxt);
            int actualLength = classificationDescriptionLength.Length;
            int expectedlength = int.Parse(expectedValue);
            string verifyingMessage = "To verify expected " + this.nmclassficationDescriptionFieldlength + " is " + expectedlength;
            string messageWhenPassed = "Actual " + this.nmclassficationDescriptionFieldlength + " is " + actualLength + " matching with expected " + this.nmclassficationDescriptionFieldlength + " is " + expectedlength;
            string messageWhenFailed = "Expected Length is " + expectedlength + "Not Matching with Actual Length" + actualLength;
            Verify.That(this.DriverContext, () => Assert.AreEqual(expectedlength, actualLength), verifyingMessage, messageWhenPassed, messageWhenFailed);
        }

        public void IsAddClassificationTypeButtonClickable()
        {
            Verify.WaitUntilElementIsNoLongerFoundWithSoftAssertion(this.DriverContext, this.loadbutton, this.nmAddClassificationTypeBtn, BaseConfiguration.LongTimeout);
            this.Driver.IsElementVisibleWithSoftAssertion(this.addClassifficationTypebtn, this.nmAddClassificationTypeBtn);
            var webElement = this.Driver.GetElement(this.addClassifficationTypebtn);
            this.Driver.JavaScriptClick(webElement, this.nmAddClassificationTypeBtn);
        }

        public void IsUserAbleToEnterClassificationTypeInPopUpWindowOfAddClassificationType(string name)
        {
            this.Driver.IsElementVisibleWithSoftAssertion(this.classificationtxt, this.nmClassficationtxt);
            this.Driver.EnterText(this.classificationtxt, name, this.nmClassficationtxt);
        }

        public void IsUserAbleToEnterDescriptionInPopUpWindowOfAddClassificationType(string name)
        {
            this.Driver.IsElementVisibleWithSoftAssertion(this.descriptionpopuptext, this.nmClassificationDescrition);
            this.Driver.EnterText(this.descriptionpopuptext, name, this.nmClassificationDescrition);
        }

        public string GetClassificationTypeValue()
        {
            return this.Driver.GetValue(this.classificationtxt);
        }

        public string GetClassificationType(string name)
        {
            string classificationType = Verify.getTextIfElementIsVisibleWithSoftAssertionFromListOfElement(this.DriverContext, this.classificationTypePopup.Format(name), name);
            return classificationType;
        }

        public string GetClassificationDescription(string index)
        {
            string descriptionValue = Verify.getTextIfElementIsVisibleWithSoftAssertionFromListOfElement(this.DriverContext, this.descriptionPopup.Format(index), index);
            return descriptionValue;
        }

        public string GetColorValue(string index)
        {
            return this.Driver.GetValue(this.descriptionrichtxt);
        }

        public bool GetAllApplicableCommitteeSelected(string index)
        {
            string applicableAllCommittee = Verify.getTextIfElementIsVisibleWithSoftAssertionFromListOfElement(this.DriverContext, this.isapplicableToAllcommitteFromPopup.Format(index), index);
            if (applicableAllCommittee == "Yes")
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public string GetDescriptionValue()
        {
            return this.Driver.GetValue(this.descriptionrichtxt);
        }

        public void AddMemberClassification(string classificationType, string description)
        {
            Verify.WaitUntilElementIsNoLongerFoundWithSoftAssertion(this.DriverContext, this.loadbutton, this.nmclassificationTypetxtPopup, BaseConfiguration.LongTimeout);
            this.IsUserAbleToEnterClassificationTypeInPopUpWindowOfAddClassificationType(classificationType);
            this.IsUserAbleToEnterClassificationDescriptionInPopUpWindowOfAddClassificationType(description);
            this.IsAddColorClikable();
            this.FillColorInputTextBox();
            this.IsApplicableToAllCommitteeCheckBoxSelected();
        }

        public void IsSuccessMessageDisplayed(string updateSuccessmessage)
        {
            Verify.WaitUntilElementIsNoLongerFoundWithSoftAssertion(this.DriverContext, this.loadbutton, this.nmmemberclassfificationmsg, BaseConfiguration.LongTimeout);
            this.Driver.IsElementVisible(this.successmsg, this.nmmemberclassfificationmsg);
            this.Driver.IsExpectedTextMatchWithActualText(this.successmsg, updateSuccessmessage, this.nmmemberclassfificationmsg);
        }

        public void IsUserAbleToEnterClassificationDescriptionInPopUpWindowOfAddClassificationType(string desc)
        {
            this.Driver.WaitUntilElementIsFound(this.descriptiontxt, BaseConfiguration.LongTimeout);
            this.Driver.EnterText(this.descriptiontxt, desc, this.nmClassificationDescrition);
        }

        public void GetMembershipClassificationDataFromDB(string classificationType, List<string> columnnames, List<string> values)
        {
            DataSet resultTable = SqlHelper.ExecuteSqlCommandQuery(string.Format(SqlQuery.FunctionalAddClassificationType, classificationType));
            Dictionary<string, string> actualDict = SqlHelper.GetTableColumnValueInDictionary(resultTable.Tables[0], columnnames);
            Dictionary<string, string> expectedDict = SqlHelper.GenerateDictionaryfromLists(columnnames, values);
            this.Driver.CompareTwoDictionaryFromPageAndDB(actualDict, expectedDict);
        }

        public bool IsApplicableToAllCommittedSelected()
        {
            var selected = this.Driver.GetElement(this.applicabletocommitte);
            if (!selected.GetAttribute("class").Contains("checked"))
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public void IsApplicableToAllCommitteeCheckBoxSelected()
        {
            try
            {
                Verify.IsElementVisibleWithSoftAssertion(this.DriverContext, this.applicabletocommitte, this.nmisapplicableToAllCommitte);
                var selected = this.Driver.GetElement(this.applicabletocommitte);
                if (!selected.GetAttribute("class").Contains("checked"))
                {
                    Assert.That(false);
                }
                else
                {
                    DriverContext.ExtentStepTest.Log(LogStatus.Pass, "To Verify the User is able to selected by defautly  " + this.nmapplicabletocommitte + " in the Add Classification Pop up Window", this.nmapplicabletocommitte + " is selected successfully in");
                    Logger.Info(this.nmapplicabletocommitte + " is Entered successfully in " + this.nmapplicabletocommitte);
                }
            }
            catch (Exception)
            {
                this.Driver.IsElementClickable(this.applicabletocommitte, this.nmapplicabletocommitte);
                DriverContext.ExtentStepTest.Log(LogStatus.Fail, "To Validate the Applicable To All Committee is not selected in the  by Default", "Exception occured while verify check box selected");
                Logger.Error("Failed to Validate the " + this.nmapplicabletocommitte + " in the " + this.nmapplicabletocommitte);
                throw;
            }
        }

        public void IsClassificationTypeisrequiredErrorMessageDisplayed(string expectederrormessage)
        {
            var actualMsg = this.Driver.GetText(this.classificationrequirederror);
            this.Driver.IsElementVisible(this.classificationrequirederror, actualMsg);
            this.Driver.IsExpectedTextMatchWithActualText(this.classificationrequirederror, expectederrormessage, actualMsg);
        }

        public void IsColorIsRequiredErrorMessageDisplayed(string expected)
        {
            var actualMsg = this.Driver.GetText(this.colorErrorMsg);
            this.Driver.IsExpectedTextMatchWithActualText(this.colorErrorMsg, expected, actualMsg);
        }

        public void IsSaveButtonClickableOfPopUpWindowOfAddClassificationType()
        {
            Verify.WaitUntilElementIsNoLongerFoundWithSoftAssertion(this.DriverContext, this.loadbutton, this.nmclassificationsavebutton, BaseConfiguration.LongTimeout);
            Verify.IsElementClickableWithSoftAssertion(this.DriverContext, this.classificationsavebtn, this.nmclassificationsavebutton);
        }

        public void IsCancelButtonClickableOfPopUpWindowOfAddClassificationType()
        {
            this.Driver.IsElementClickable(this.classificationCancelbtn, this.nmclassificationCancelbutton);
        }

        public void IsAddColorClikable()
        {
            Verify.WaitUntilElementIsNoLongerFoundWithSoftAssertion(this.DriverContext, this.loadbutton, this.nmaddColor, BaseConfiguration.LongTimeout);
            this.Driver.IsElementClickableWithoutLog(this.addColor, this.nmaddColor);
            this.DragColor();
        }

        public void MoveSlider()
        {
            this.Driver.SliderSetAttribute(this.colorSliderDiv, this.nmdivColorChange);
        }

        public void FillColorInputText(string colorname)
        {
            this.Driver.IsElementClickable(this.secondColorChange, this.nmsecondColor);
            this.Driver.EnterText(this.colorNameInput, colorname, this.nmcolorNameInput);
            this.Driver.IsElementClickable(this.addColorNameOkButton, this.nmaddColorNameOkButton);
            while (this.Driver.IsElementPresentOrNotWithoutLog(this.errorMessageAddColor, this.nmerrorMessageAddColor, string.Empty) == true)
            {
                this.MoveSlider();
                this.DragColor();
                this.Driver.IsElementClickableWithoutLog(this.secondColorChange, this.nmfirstColor);
                this.Driver.IsElementClickableWithoutLog(this.addColorNameOkButton, this.nmaddColorNameOkButton);
           }
        }

        public void DragColor()
        {
            this.Driver.IsElementDragSliderToSetHieght(this.colordivgrid, this.nmsecondColor, 3);
        }

        public void IsUserAbletoverifyMemberClassificationUpdateMessage(string updateSuccessmessage)
        {
            Verify.IsElementVisibleWithSoftAssertion(this.DriverContext, this.successfullMsg, this.nmupdatesuccessfullmsg);
            this.Driver.IsExpectedTextMatchWithActualText(this.successfullMsg, updateSuccessmessage, this.nmupdatesuccessfullmsg);
        }

        public void IsAddColorButtonClickable()
        {
            this.IsAddColorClikable();
        }

        public void FillColorInputTextBox()
        {
            this.FillColorInputText(DateHelper.RandomString(5, false));
        }

        public void IsClassificationTypesAreDisplayinginAlphabeticalOrder()
        {
            this.Driver.AreElementsSortedInAlphabeticalOrder(this.classificationType, this.nmclassificationTypeFromList, "asc");
        }

        public void IsSuccessfullMessageForAddMembershipClassificationDisplayed(string expectedmsg)
        {
            this.Driver.IsElementVisibleWithSoftAssertion(this.successfullMsg, this.nmsuccessfullmsg);
            string actualMSg = this.Driver.GetText(this.successfullMsg);
            this.Driver.IsExpectedTextMatchWithActualText(this.successfullMsg, expectedmsg, actualMSg);
            this.Driver.PageRefresh(this.pageRefresh);
        }

        public void IsClassificationTypeClickableFromListofElementsForEdit(string classificationType, string description, string successmsg)
        {
            var classificationTypes = this.Driver.GetElements(this.classificationType);
            if (classificationTypes.Count == 0)
            {
                Verify.WaitUntilElementIsNoLongerFoundWithSoftAssertion(this.DriverContext, this.loadbutton, this.nmclassificationTypetxtPopup, BaseConfiguration.LongTimeout);
                this.IsUserAbleToEnterClassificationTypeInPopUpWindowOfAddClassificationType(classificationType);
                this.IsUserAbleToEnterClassificationDescriptionInPopUpWindowOfAddClassificationType(description);
                this.IsApplicableToAllCommitteeCheckBoxSelected();
                this.IsAddColorClikable();
                this.FillColorInputTextBox();
                this.IsSaveButtonClickableOfPopUpWindowOfAddClassificationType();
                this.IsSuccessfullMessageForAddMembershipClassificationDisplayed(successmsg);
            }
            else
            {
                this.Driver.IsElementClickableFromListOfElemetsBasedOnIndex(this.classificationType, this.nmclassificationTypeFromList, 0);
            }
        }

        public void IsClassificationTypeClickableFromListofElements(string value)
        {
            Verify.WaitUntilElementIsNoLongerFoundWithSoftAssertion(this.DriverContext, this.loadbutton, this.nmsuccessfullmsg, BaseConfiguration.LongTimeout);
            this.Driver.IsElementClickableFromListofValuesWithText(this.classificationType, value, this.nmclassificationTypeFromList);
        }

        public void IsSavedClassificationTypeDisplayedInListOfValues(string expvalue)
        {
            string actual = this.Driver.IsExpectedTextMatchWithActualTextFromListOFValues(this.classificationType, expvalue);

            string verifyingMessage = "To verify expected " + this.nmclassificationTypeFromList + " is " + expvalue + " from list of values";
            string messageWhenPassed = "Actual " + this.nmclassificationTypeFromList + " is " + actual + " matching with expected " + this.nmclassificationTypeFromList + " is " + expvalue + " from the list of values";
            string messageWhenFailed = "Expected Length is " + expvalue + "Not Matching with Actual " + actual + " From list of values";
            Verify.That(this.DriverContext, () => Assert.AreEqual(expvalue, actual), verifyingMessage, messageWhenPassed, messageWhenFailed);
        }

        public void IsUserAbletoClickOnClassificationTypeFromListOfValuesBasedonText(string value)
        {
            Verify.WaitUntilElementIsNoLongerFoundWithSoftAssertion(this.DriverContext, this.loadbutton, this.nmclassificationTypeFromList, BaseConfiguration.LongTimeout);
            this.Driver.IsElementClickableFromListofValuesWithText(this.classificationType, value, this.nmclassificationTypeFromList);
        }

        public void IsUserAbleToViewManageMembershipClassificationListInLifoOrder(string expected)
        {
            Verify.WaitUntilElementIsNoLongerFoundWithSoftAssertion(this.DriverContext, this.loadbutton, this.nmclassificationTypeFromList, BaseConfiguration.LongTimeout);
            Verify.IsElementClickableWithSoftAssertion(this.DriverContext, this.classificationType, this.nmclassificationTypeFromList);
            this.Driver.IsElementTextVisibleFromListOfValues(this.classificationType, expected, this.nmclassificationTypeFromList);
    }

        public void GetClassificationTypeFromDB(string classificationType, string compareText)
        {
           // this.Driver.GetSingleValueFromDB(string.Format(SqlQuery.ClassificationType, classificationType), compareText);
        }

        public void IsUserAbletoEditMemberClassificationRecordFromListOfValues(string classification)
        {
            this.Driver.IsElementClickableFromListofElementWithTextByIndex(this.memberclassificationrecord, this.nmmemberclassificationtype, classification);
        }

        public void IsUserAbletoClickOnEditIconInMemberClassificationPopUp()
        {
            Verify.WaitUntilElementIsNoLongerFoundWithSoftAssertion(this.DriverContext, this.loadbutton, this.nmmemberclassificationeditbutton, BaseConfiguration.LongTimeout);
            Verify.IsElementClickableWithSoftAssertion(this.DriverContext, this.editbutton, this.nmmemberclassificationeditbutton);
        }

        public void IsUserAbletoVerifyUpdatedmembershipClassificationFromListOfValues(string updatedclassificationtype)
        {
            Verify.WaitUntilElementIsNoLongerFoundWithSoftAssertion(this.DriverContext, this.loadbutton, this.nmclassificationTypeFromList, BaseConfiguration.LongTimeout);
            this.Driver.IsElementTextVisibleFromListOfValues(this.classificationType, updatedclassificationtype, this.nmclassificationTypeFromList);
        }
    }
}
