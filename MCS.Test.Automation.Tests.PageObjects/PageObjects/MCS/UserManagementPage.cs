// <copyright file="UserManagementPage.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace MCS.Test.Automation.Tests.PageObjects.PageObjects.MCS
{
    using System;
    using global::MCS.Test.Automation.Common;
    using global::MCS.Test.Automation.Common.Extensions;
    using global::MCS.Test.Automation.Common.Types;
    using global::MCS.Test.Automation.Tests.PageObjects;
    using NLog;
    using NUnit.Framework;
    using RelevantCodes.ExtentReports;

    public class UserManagementPage : ProjectPageBase
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
                  addUserbtn = new ElementLocator(Locator.CssSelector, "button.ui.secondary.button");

        private readonly ElementLocator
            validateInADButton = new ElementLocator(Locator.CssSelector, "button.ui.button.primary");

        private readonly ElementLocator
        addusersavebutton = new ElementLocator(Locator.XPath, "//*[@class='ui button primary' and text()='Save']");

        private readonly ElementLocator
               username = new ElementLocator(Locator.Name, "UserName");

        private readonly ElementLocator
               password = new ElementLocator(Locator.Name, "password");

        private readonly ElementLocator
               confirmpassword = new ElementLocator(Locator.Name, "ConfirmUserPassword");

        private readonly ElementLocator
      addUserSearchBox = new ElementLocator(Locator.CssSelector, "div.ui.icon.input input.prompt");

        private readonly ElementLocator
           userPrivilegePermission = new ElementLocator(Locator.XPath, "//label[contains(text(), 'All')]");

        private readonly ElementLocator
             saveUserPrivilegebtn = new ElementLocator(Locator.XPath, "//*[text()='Save']");

        private readonly ElementLocator
          usernameVerification = new ElementLocator(Locator.XPath, " //a[contains(text(), '{0}')]");

        private readonly ElementLocator
            usersuccessfullmsg = new ElementLocator(Locator.CssSelector, "div.ui.compact.message.success div.content p");

        private readonly ElementLocator
          loadbutton = new ElementLocator(Locator.XPath, "//*[@class='ui active transition visible dimmer']");

        private readonly ElementLocator
            editBtn = new ElementLocator(Locator.CssSelector, "span.editBtn");

        private readonly ElementLocator
            updateBtn = new ElementLocator(Locator.CssSelector, "//button[contains(text(), 'Update')]");

        private readonly ElementLocator
            editUserHeader = new ElementLocator(Locator.CssSelector, " div.headingTitle.clearfix.mb20 > h2");

        private readonly ElementLocator
            nopermissionErrorMessage = new ElementLocator(Locator.CssSelector, "span.errorMessage");

        private readonly ElementLocator
            userNameColumn = new ElementLocator(Locator.XPath, "//th[contains(text(),'Username')]");

        private readonly ElementLocator
            nameColumn = new ElementLocator(Locator.XPath, "//th[contains(text(),'Name')]");

        private readonly ElementLocator
            emailColumn = new ElementLocator(Locator.CssSelector, "th.email");

        private readonly ElementLocator
           userTypeColumn = new ElementLocator(Locator.CssSelector, "th.usertype");

        private readonly ElementLocator
            statusColumn = new ElementLocator(Locator.CssSelector, "th.status");

        private readonly ElementLocator
            userName = new ElementLocator(Locator.CssSelector, "td.userName a");

        private readonly ElementLocator
            viewUserDetails = new ElementLocator(Locator.CssSelector, "div.headingTitle.clearfix.mb20 h2");

        private readonly ElementLocator
           viewUserName = new ElementLocator(Locator.XPath, "//span[contains(text(),'Username') and @class='titleLabel']");

        private readonly ElementLocator
           viewLastLogin = new ElementLocator(Locator.XPath, "//span[contains(text(),'Last Login')]");

        private readonly ElementLocator
           viewEmail = new ElementLocator(Locator.XPath, "//span[contains(text(),'Email')]");

        private readonly ElementLocator
           viewFirstName = new ElementLocator(Locator.XPath, "//span[contains(text(),'First Name')]");

        private readonly ElementLocator
           viewLastName = new ElementLocator(Locator.XPath, "//span[contains(text(),'Last Name')]");

        private readonly ElementLocator
           viewPhone = new ElementLocator(Locator.XPath, "//span[contains(text(),'Phone')]");

        private readonly ElementLocator
           viewStatus = new ElementLocator(Locator.XPath, "//span[contains(text(),'Status')]");

        private readonly ElementLocator
           viewUserType = new ElementLocator(Locator.XPath, "//span[contains(text(),'User Type')]");

        private readonly ElementLocator
           viewPrivileges = new ElementLocator(Locator.CssSelector, "h5.mb10");

        private readonly ElementLocator
           userTypeDropdown = new ElementLocator(Locator.XPath, "//div[@class='ui selection dropdown']//i");

        private readonly ElementLocator
           astmStaff = new ElementLocator(Locator.XPath, "//span[contains(text(),'ASTM Staff')]");

        private readonly ElementLocator
           errorMessage = new ElementLocator(Locator.CssSelector, "div.mt30 span.errorMessage");

        private readonly ElementLocator
           updateButton = new ElementLocator(Locator.XPath, "//button[contains(text(),'Update')]");

        private readonly ElementLocator
           userNameField = new ElementLocator(Locator.XPath, "//span[contains(text(),'Username')]/following-sibling::span");

        private readonly ElementLocator
           searchboxplaceholder = new ElementLocator(Locator.CssSelector, "div.ui.icon.input input.prompt");

        private readonly ElementLocator
          autoSuggestionsList = new ElementLocator(Locator.CssSelector, "div.results.transition.visible");

        private readonly ElementLocator
          autosuggestionField = new ElementLocator(Locator.CssSelector, "div.result");

        private readonly ElementLocator
         userNameAfterSearch = new ElementLocator(Locator.CssSelector, "td.userName");

        private readonly ElementLocator
          nameAfterSearch = new ElementLocator(Locator.CssSelector, "td.name");

        private readonly ElementLocator
          userEmailAfterSearch = new ElementLocator(Locator.CssSelector, "td.email");

        private readonly ElementLocator
          userTypeAfterSearch = new ElementLocator(Locator.CssSelector, "td.userType");

        private readonly ElementLocator
            userpermissionCheckBox = new ElementLocator(Locator.XPath, "//*[text()='Manage Membership Types']//..//input[@class='hidden' and @name='All']");

        private readonly ElementLocator
            privileges = new ElementLocator(Locator.XPath, "//h5[text()='Privileges']");

        private readonly ElementLocator
            membershipManagementDiv = new ElementLocator(Locator.CssSelector, "div.accordion.ui.fluid");

        private readonly ElementLocator
             membershipManagementTypep = new ElementLocator(Locator.XPath, "//p[text()='Manage Membership Types']");

        private readonly ElementLocator
            additionalPrivilegesDropDown = new ElementLocator(Locator.XPath, "//div[@id='{0}']//i");

        private string nmautoSuggestionsList = "List of elements after search";
        private string nmautosuggestionField = "Auto suggestion TextBox Field";
        private string nmNameAfterSearch = "Name field displayed after search";
        private string nmuserEmailAfterSearch = "Email field displayed after search";
        private string nmuserTypeAfterSearch = "Role field displayed after search";
        private string nmuserNameAfterSearch = "UserName field displayed after search";
        private string nmnopermissionErrorMessage = "No permissions assigned. Please assign atleast one permission.";
        private string nmprovideusernamerrormsg = "Please provide the Username.";
        private string nmUsersuccessfullmsg = "User Type added Successfully";
        private string nmUsernameVerification = "User Created as ";
        private string nmSaveUserPrivilegebtn = "Save button";
        private string nmadduserbtn = "Add User Button";
        private string nmcustomerlogo = "Customer logo";
        private string nmmembership = "Memebership Management Menu";
        private string nmcommitteemanagement = "Committee Management Menu";
        private string nmapplicationmanagement = "Application Management Menu";
        private string nmmanagememebershiptype = "Manage Membership Type option";
        private string nmmanagemembershipFaqs = "Manage Membership FAQ's option";
        private string nmmanagemembershipdocument = "Manage Membership Documents option";
        private string nmmanagememeberclassifications = "Manage Member Classifications option";
        private string nmusername = "User Name field";
        private string nmpassword = "Password field";
        private string nmconfirmpassword = "Confirm Password field";
        private string nmvalidatebutton = "Validate Button";
        private string nmaddusersavebutton = "Save button";
        private string nmeditBtn = "Select Edit Btn";
        private string nmUpdateBtn = "Click update btn";
        private string nmEdituserHeader = "Edit User Header";
        private string nmUserPrivilegePermission = "User Privilege Selection";
        private string nmuserType = "User Type field";
        private string nmastmStaffUserType = "Astm Staff User Type";
        private string nmsearchboxplaceholder = "Search User by Username, First Name, Last Name, Email";
        private string nmadditionalPrivileges = "Additional Privileges";

        public UserManagementPage(DriverContext driverContext)

            : base(driverContext)
        {
        }

        public void IsEditBtnClickable(string headername)
        {
            this.Driver.IsElementVisible(this.editBtn, this.nmeditBtn);
            this.Driver.IsElementClickable(this.editBtn, this.nmeditBtn);
            this.Driver.IsElementVisible(this.editUserHeader.Format(headername), this.nmEdituserHeader);
        }

        public void IsCustomerLogoDisplayed()
        {
            this.Driver.IsElementVisible(this.customerLogo, this.nmcustomerlogo);
        }

        public void IsAddSearchBoxContainsPlaceholder(string placeholdertxt)
        {
            this.Driver.IsElementContainGivenAttribute(this.searchboxplaceholder, placeholdertxt, this.nmsearchboxplaceholder);
        }

        public void IsAddUserButtonClickable()
        {
            var webElement = this.Driver.GetElement(this.addUserbtn);
            this.Driver.IsElementVisible(this.addUserbtn, this.nmadduserbtn);
            this.Driver.JavaScriptClick(webElement, this.nmadduserbtn);
        }

        public void IsAddSearchBoxDisplayed()
        {
            this.Driver.IsElementVisible(this.username, this.nmusername);
        }

        public void EnterUserName(string name)
        {
            this.Driver.IsElementVisible(this.username, this.nmusername);
            this.Driver.EnterText(this.username, name, this.nmusername);
        }

        public void EnterPassword(string pass)
        {
            this.Driver.IsElementVisible(this.password, this.nmpassword);
            this.Driver.EnterText(this.password, pass, this.nmpassword);
        }

        public void EnterConfirmPassword(string confirmpass)
        {
            this.Driver.IsElementVisible(this.confirmpassword, this.nmconfirmpassword);
            this.Driver.EnterText(this.confirmpassword, confirmpass, this.nmconfirmpassword);
        }

        public void IsPleaseprovidetheUsernameErrorMessageDisplayed(string errormessage)
        {
            this.Driver.WaitUntilElementIsFound(this.nopermissionErrorMessage, BaseConfiguration.LongTimeout);
            var text = this.Driver.GetText(this.nopermissionErrorMessage);
            this.Driver.IsElementVisible(this.nopermissionErrorMessage, this.nmprovideusernamerrormsg);
            this.Driver.IsExpectedTextMatchWithActualText(this.nopermissionErrorMessage, text, errormessage);
        }

        public void IsNoPermissionAssignedErrorMessageDisplayed(string errormessage)
        {
            this.Driver.WaitUntilElementIsFound(this.nopermissionErrorMessage, BaseConfiguration.LongTimeout);
            var text = this.Driver.GetText(this.nopermissionErrorMessage);
            this.Driver.IsElementVisible(this.nopermissionErrorMessage, this.nmnopermissionErrorMessage);
            this.Driver.IsExpectedTextMatchWithActualText(this.nopermissionErrorMessage, text, errormessage);
        }

        public void IsValidateInADButtonClickable()
        {
            var webElement = this.Driver.GetElement(this.validateInADButton);
            this.Driver.IsElementVisible(this.validateInADButton, this.nmvalidatebutton);
            this.Driver.JavaScriptClick(webElement, this.nmvalidatebutton);
        }

        public void IsAddUserSaveButtonClickable()
        {
            this.Driver.WaitUntilElementIsFound(this.addusersavebutton, BaseConfiguration.LongTimeout);
            var webElement = this.Driver.GetElement(this.addusersavebutton);
            this.Driver.IsElementVisible(this.addusersavebutton, this.nmaddusersavebutton);
            this.Driver.JavaScriptClick(webElement, this.nmaddusersavebutton);
        }

        public void AddPreviligesToUser(int index)
        {
            this.Driver.WaitUntilElementIsFound(this.userPrivilegePermission, 90);
            this.Driver.IsElementClickableFromListOfElemetsBasedOnIndex(this.userPrivilegePermission, this.nmUserPrivilegePermission, index);
        }

        public void IsUpdateBtnClickable()
        {
            this.Driver.WaitUntilElementIsFound(this.updateBtn, BaseConfiguration.LongTimeout);
            var webElement = this.Driver.GetElement(this.updateBtn);
            this.Driver.IsElementClickable(this.updateBtn, this.nmUpdateBtn);
            this.Driver.JavaScriptClick(webElement, this.nmUpdateBtn);
        }

        public void IsMembershipManagementSectionClickable(string name)
        {
            this.Driver.IsElementVisible(this.membershipmenu.Format(name), this.nmmembership);
            this.Driver.IsElementClickable(this.membershipmenu.Format(name), this.nmmembership);
        }

        public void SaveUserCreation()
        {
            this.Driver.ScrollToWebElement(this.saveUserPrivilegebtn);
            this.Driver.IsElementClickable(this.saveUserPrivilegebtn, this.nmSaveUserPrivilegebtn);
        }

        public void VerifyCreatedUserIsPresent(string name)
        {
            this.Driver.IsElementVisible(this.usernameVerification.Format(name), this.nmUsernameVerification);
        }

        public void IsSuccessfullMessageDisplayedForNewUser(string name)
        {
            this.Driver.WaitUntilElementIsNoLongerFound(this.loadbutton, 90, this.nmUsersuccessfullmsg);
            var text = this.Driver.GetText(this.usersuccessfullmsg);
            this.Driver.IsExpectedTextMatchWithActualText(this.usersuccessfullmsg, text, name);
            this.Driver.IsElementVisibleWithSoftAssertion(this.usersuccessfullmsg.Format(name), this.nmUsersuccessfullmsg);
        }

        /// <summary>
        /// Committee Management Section.
        /// </summary>
        /// <param name="name">Element Name.</param>
        public void IsCommitteeManagementSectionClickable(string name)
        {
            this.Driver.IsElementVisible(this.committeemanagement.Format(name), this.nmcommitteemanagement);
            this.Driver.IsElementClickable(this.committeemanagement.Format(name), this.nmcommitteemanagement);
        }

        /// <summary>
        /// Application Management Section.
        /// </summary>
        /// <param name="name">Element Name.</param>
        public void IsApplicationManagementSectionClickable(string name)
        {
            this.Driver.IsElementVisible(this.applicationManagement.Format(name), this.nmapplicationmanagement);
            this.Driver.IsElementClickable(this.applicationManagement.Format(name), this.nmapplicationmanagement);
        }

        /// <summary>
        /// Manage Memebership Type.
        /// </summary>
        /// <param name="name">Element Name.</param>
        public void IsManageMembershipTypeClickable(string name)
        {
            this.Driver.IsElementVisible(this.managemembershiptype.Format(name), this.nmmanagememebershiptype);
            this.Driver.IsElementClickable(this.managemembershiptype.Format(name), this.nmmanagememebershiptype);
        }

        /// <summary>
        /// Manage Memebership FAqs.
        /// </summary>
        /// <param name="name">Element Name.</param>
        public void IsManageMembershipFAQsClickable(string name)
        {
            this.Driver.IsElementVisible(this.manageMembershipFAQs.Format(name), this.nmmanagemembershipFaqs);
            this.Driver.IsElementClickable(this.manageMembershipFAQs.Format(name), this.nmmanagemembershipFaqs);
        }

        /// <summary>
        /// Manage Memebership Documents.
        /// </summary>
        /// <param name="name">Element Name.</param>
        public void IsManageMembershipDocumentsClickable(string name)
        {
            this.Driver.IsElementVisible(this.manageMembershipDocuments.Format(name), this.nmmanagemembershipdocument);
            this.Driver.IsElementClickable(this.manageMembershipDocuments.Format(name), this.nmmanagemembershipdocument);
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

        public void IsUserAbleToViewUserNameColumn(string expectedUserName)
        {
            string actual = this.Driver.GetText(this.userNameColumn);
            this.Driver.IsExpectedTextMatchWithActualText(this.userNameColumn, expectedUserName, actual);
        }

        public void IsUserAbleToViewNameColumn(string expectedUserName)
        {
            string actual = this.Driver.GetText(this.nameColumn);
            this.Driver.IsExpectedTextMatchWithActualText(this.nameColumn, expectedUserName, actual);
        }

        public void IsUserAbleToViewEmailColumn(string expected)
        {
            string actual = this.Driver.GetText(this.emailColumn);
            this.Driver.IsExpectedTextMatchWithActualText(this.emailColumn, expected, actual);
        }

        public void IsUserAbleToViewUserTypeColumn(string expected)
        {
            string actual = this.Driver.GetText(this.userTypeColumn);
            this.Driver.IsExpectedTextMatchWithActualText(this.userTypeColumn, expected, actual);
        }

        public void IsUserAbleToViewStatusColumn(string expected)
        {
            string actual = this.Driver.GetText(this.statusColumn);
            this.Driver.IsExpectedTextMatchWithActualText(this.statusColumn, expected, actual);
        }

        public string IsUserIsAbleToSelectUserNameFromList(int i)
        {
            string actual = this.Driver.GetTextFromListOfElementsBasedOnIndex(this.userName, this.nmusername, i);
            this.Driver.IsElementClickable(this.userName, this.nmusername);
            return actual;
        }

        public void IsUserAbleToViewUserDetails(string expected)
        {
            this.Driver.WaitUntilElementIsFound(this.viewUserDetails, BaseConfiguration.LongTimeout);
            string actual = this.Driver.GetText(this.viewUserDetails);
            this.Driver.IsExpectedTextMatchWithActualText(this.viewUserDetails, expected, actual);
        }

        public void IsUserAbleToViewUserName(string expectedUserName)
        {
            string actual = this.Driver.GetText(this.viewUserName);
            this.Driver.IsExpectedTextMatchWithActualText(this.viewUserName, expectedUserName, actual);
        }

        public void IsUserAbleToViewLastLogin(string expectedUserName)
        {
            string actual = this.Driver.GetText(this.viewLastLogin);
            this.Driver.IsExpectedTextMatchWithActualText(this.viewLastLogin, expectedUserName, actual);
        }

        public void IsUserAbleToViewEmail(string expectedUserName)
        {
            string actual = this.Driver.GetText(this.viewEmail);
            this.Driver.IsExpectedTextMatchWithActualText(this.viewEmail, expectedUserName, actual);
        }

        public void IsUserAbleToViewFirstName(string expectedUserName)
        {
            string actual = this.Driver.GetText(this.viewFirstName);
            this.Driver.IsExpectedTextMatchWithActualText(this.viewFirstName, expectedUserName, actual);
        }

        public void IsUserAbleToViewLastName(string expectedUserName)
        {
            string actual = this.Driver.GetText(this.viewLastName);
            this.Driver.IsExpectedTextMatchWithActualText(this.viewLastName, expectedUserName, actual);
        }

        public void IsUserAbleToViewPhone(string expectedUserName)
        {
            string actual = this.Driver.GetText(this.viewPhone);
            this.Driver.IsExpectedTextMatchWithActualText(this.viewPhone, expectedUserName, actual);
        }

        public void IsUserAbleToViewStatus(string expectedUserName)
        {
            string actual = this.Driver.GetText(this.viewStatus);
            this.Driver.IsExpectedTextMatchWithActualText(this.viewStatus, expectedUserName, actual);
        }

        public void IsUserAbleToViewUserType(string expectedUserName)
        {
            string actual = this.Driver.GetText(this.viewUserType);
            this.Driver.IsExpectedTextMatchWithActualText(this.viewUserType, expectedUserName, actual);
        }

        public void IsUserAbleToViewPrivileges(string expectedUserName)
        {
            string actual = this.Driver.GetText(this.viewPrivileges);
            this.Driver.IsExpectedTextMatchWithActualText(this.viewPrivileges, expectedUserName, actual);
        }

        public void IsUserAbleToClickOnDropDown()
        {
            this.Driver.IsElementClickable(this.userTypeDropdown, this.nmuserType);
        }

        public void IsUserAbleToClickOnAstmStaffUserType()
        {
            this.Driver.IsElementClickable(this.astmStaff, this.nmastmStaffUserType);
        }

        public void IsUserAbleToViewErrorMessage(string expected)
        {
            string actual = this.Driver.GetText(this.errorMessage);
            this.Driver.IsExpectedTextMatchWithActualText(this.errorMessage, expected, actual);
        }

        public void IsUserAbleToClickOnEditBtn()
        {
            this.Driver.IsElementClickable(this.editBtn, this.nmeditBtn);
        }

        public void IsUserAbleToClickOnUpdateBtn()
        {
            this.Driver.IsElementClickable(this.updateButton, this.nmUpdateBtn);
        }

        public void IsUserAbleToUserNameFieldValueInView(string expectedUserName)
        {
            string actual = this.Driver.GetText(this.userNameField);
            this.Driver.IsExpectedTextMatchWithActualText(this.userNameField, expectedUserName, actual);
        }

        public void IsUserableToEnterTextInSearchBox(string searchText)
        {
            this.Driver.WaitUntilElementIsFound(this.searchboxplaceholder, BaseConfiguration.LongTimeout);
            this.Driver.EnterText(this.searchboxplaceholder, searchText, this.nmsearchboxplaceholder);
        }

        public void IsUserableToGetCountOfSuggenstionsFromSearchBox(string searchText, int expectedCount)
        {
            this.IsUserableToEnterTextInSearchBox(searchText);
            this.Driver.AreElementsVisible(this.autoSuggestionsList, this.nmautoSuggestionsList);
            var lstElements = this.Driver.GetElements(this.autosuggestionField);
            int actualCount = lstElements.Count;
            try
            {
                Assert.AreEqual(expectedCount, actualCount);
                DriverContext.ExtentStepTest.Log(LogStatus.Pass, "To verify Expected Value is matching with Actual text ", "The expected Value is " + expectedCount + " and  actual value is " + actualCount + " matching successfully");
                DriverContext.ExtentStepTest.Log(LogStatus.Pass, "To Verify whether autoSuggestions search List has 10 records", "Search List consists of 10 records successfully");
            }
            catch (Exception ex)
            {
                Logger.Error("Failed to verify text on " + -expectedCount + "with Actual Text " + actualCount + " Due to exception: " + ex.ToString());

                DriverContext.ExtentStepTest.Log(LogStatus.Fail, "To Verify count on " + expectedCount + "with Actual count " + actualCount, "An exception occurred while finding count on " + expectedCount);
                throw;
            }
        }

        public void EnterUserNameAndSelectAutoSuggestion(string name)
        {
            this.Driver.IsElementVisible(this.searchboxplaceholder, this.nmsearchboxplaceholder);
            this.Driver.EnterText(this.searchboxplaceholder, name, this.nmsearchboxplaceholder);
            this.Driver.IsElementClickable(this.searchboxplaceholder, this.nmsearchboxplaceholder);
            this.Driver.IsElementClickable(this.autosuggestionField, this.nmautosuggestionField);
        }

        public void IsExpectedUserNameDisplayedAfterSearchOperation(string expecteduserName)
        {
            this.Driver.WaitUntilElementIsFound(this.userNameAfterSearch, BaseConfiguration.LongTimeout);
            string actualuserName = this.Driver.IsElementTextDisplayed(this.userNameAfterSearch, this.nmuserNameAfterSearch);
            this.Driver.IsExpectedTextMatchWithActualText(this.userNameAfterSearch, expecteduserName, actualuserName);
        }

        public void IsExpectedNameDisplayedAfterSearchOperation(string expectedName)
        {
            string actualName = this.Driver.IsElementTextDisplayed(this.nameAfterSearch, this.nmNameAfterSearch);
            this.Driver.IsExpectedTextMatchWithActualText(this.nameAfterSearch, expectedName, actualName);
        }

        public void IsExpectedEmailDisplayedAfterSearchOperation(string expecteduserEmail)
        {
            string actualuserEmail = this.Driver.IsElementTextDisplayed(this.userEmailAfterSearch, this.nmuserEmailAfterSearch);
            this.Driver.IsExpectedTextMatchWithActualText(this.userEmailAfterSearch, expecteduserEmail, actualuserEmail);
        }

        public void IsExpectedRoleDisplayedAfterSearchOperation(string expecteduserRole)
        {
            string actualuserRole = this.Driver.IsElementTextDisplayed(this.userTypeAfterSearch, this.nmuserTypeAfterSearch);
            this.Driver.IsExpectedTextMatchWithActualText(this.userTypeAfterSearch, expecteduserRole, actualuserRole);
        }

        public void IsHintTextVisibleInSearchBox(string expectedHintText)
        {
            var webelement = this.Driver.GetElement(this.searchboxplaceholder);
            string actualHintText = webelement.GetAttribute("placeholder");
            try
            {
                Assert.AreEqual(expectedHintText, actualHintText);
                DriverContext.ExtentStepTest.Log(LogStatus.Pass, "To verify Expected Value is matching with Actual text ", "The expected Value is " + expectedHintText + " and  actual value is " + actualHintText + " matching successfully");
            }
            catch (Exception)
            {
                DriverContext.ExtentStepTest.Log(LogStatus.Fail, "To Verify text on " + expectedHintText + "with Actual Text " + actualHintText, "An exception occurred while finding text on " + expectedHintText);
                throw;
            }
        }

        public void IsUserAbleToViewCheckBoxIsNotClicked(string name)
        {
            this.Driver.WaitUntilElementIsFound(this.membershipManagementTypep, BaseConfiguration.LongTimeout);
            this.Driver.IsElementVisible(this.membershipManagementTypep, this.nmadditionalPrivileges);

            bool status = this.Driver.IsCheckBoxChecked(this.userpermissionCheckBox);
            try
            {
                if (status == false)
                {
                    DriverContext.ExtentStepTest.Log(LogStatus.Pass, "To verify Permission is not selected", "The Permission for " + name + " is unselected ");
                }
                else
                {
                    DriverContext.ExtentStepTest.Log(LogStatus.Fail, "To verify Permission is selected", "The Permission for " + name + " is selected ");
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void IsUserAbleToClickPermissionDropDown(string name)
        {
            this.Driver.IsElementClickable(this.additionalPrivilegesDropDown.Format(name), this.nmadditionalPrivileges);
        }
    }
}
