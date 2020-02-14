// <copyright file="InternalStaffManageRolesPage.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace MCS.Test.Automation.Tests.PageObjects.PageObjects.MCS
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Data;
    using System.Reflection;
    using System.Globalization;
    using System.Linq;
    using global::MCS.Test.Automation.Common;
    using global::MCS.Test.Automation.Common.Extensions;
    using global::MCS.Test.Automation.Common.Helpers;
    using global::MCS.Test.Automation.Common.Types;
    using global::MCS.Test.Automation.Tests.PageObjects;
    using NLog;
    using NUnit.Framework;
    using OpenQA.Selenium;
    using RelevantCodes.ExtentReports;

    public class InternalStaffManageRolesPage : ProjectPageBase
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        private readonly ElementLocator
            rolesTasksHeader = new ElementLocator(Locator.XPath, "//h2[text()='Manage Roles']");

        private readonly ElementLocator
            addRoleBtn = new ElementLocator(Locator.XPath, "//button[text()=' Add Role']");

        private readonly ElementLocator
          loadbutton = new ElementLocator(Locator.XPath, "//*[@class='ui active transition visible dimmer']");

        private readonly ElementLocator
            addRolePopupHeader = new ElementLocator(Locator.XPath, "//div[@class='header'][text()='Add Role']");

        private readonly ElementLocator
            newRolePrivilegeCommitteeAll = new ElementLocator(Locator.XPath, "//p[text()='Committees']/..//input[@name='All']");

        private readonly ElementLocator
           newRolePrivilegeRolesAdd = new ElementLocator(Locator.XPath, "//p[text()='Roles']/..//input[@name='Add']");

        private readonly ElementLocator
           newRolePrivilegeRenewalAdd = new ElementLocator(Locator.XPath, "//p[text()='Manage Renewal Task']//..//input[@name='Add']");

        private readonly ElementLocator
          newRolePrivilegeRenewalUpdate = new ElementLocator(Locator.XPath, "//p[text()='Manage Renewal Task']//..//input[@name='Update']");

        private readonly ElementLocator
          newRolePrivilegeRenewalExport = new ElementLocator(Locator.XPath, "//p[text()='Manage Renewal Task']//..//input[@name='Export Renewal Tasks']");

        private readonly ElementLocator
            roleNameTextBox = new ElementLocator(Locator.XPath, "//*[@name='RoleName']");

        private readonly ElementLocator
            roleDescription = new ElementLocator(Locator.XPath, "//div/textarea");

        private readonly ElementLocator
            committeeMgmtTab = new ElementLocator(Locator.XPath, "//div[contains(text(),'Committee Management')]");

        private readonly ElementLocator
            adminTab = new ElementLocator(Locator.XPath, "//div[contains(text(),'Admin')]");

        private readonly ElementLocator
            renewalTasksTab = new ElementLocator(Locator.XPath, "//div[contains(text(),'Renewal Tasks')]");

        private readonly ElementLocator
            roleInAlphabeticalOrder = new ElementLocator(Locator.XPath, "//div[@class='roleCard']//h4");

        private readonly ElementLocator
            membershipManagementPrivileges = new ElementLocator(Locator.XPath, "//div[text()='{0}']");

        private readonly ElementLocator
            roleEditIcon = new ElementLocator(Locator.XPath, "//div[@class='roleCard']//i[@class='pencil icon']");

        private readonly ElementLocator
            roleNameErrorMessage = new ElementLocator(Locator.XPath, "//span[@class='errorMessage']");

        private readonly ElementLocator
        updateBtn = new ElementLocator(Locator.XPath, "//button[@type='submit']");

        private readonly ElementLocator
        rolesavebutton = new ElementLocator(Locator.XPath, "//*[@type='submit' or @name='btnReset']");

        private readonly ElementLocator
        popwindowTitle = new ElementLocator(Locator.XPath, "//div[@class='header']");

        private readonly ElementLocator
        privilegeCheckBoxforAll = new ElementLocator(Locator.XPath, "//label[contains(text(), 'All')]");

        private readonly ElementLocator
        updateButton = new ElementLocator(Locator.XPath, "//button[text()='Update']");

        private readonly ElementLocator
           successmsg = new ElementLocator(Locator.CssSelector, "div.top-message div.ui.compact.message.success div.content p");

        private readonly ElementLocator
            allChkboxLabelMembersManagement = new ElementLocator(Locator.XPath, "//p[text()='Members and Organizational Accounts']//..//label[text()='All']");

        private readonly ElementLocator
           allChkboxLabelCommitteeManagement = new ElementLocator(Locator.XPath, "//p[text()='Committees']//..//label[text()='All']");

        private readonly ElementLocator
          allChkboxLabelAdminUserPermissions = new ElementLocator(Locator.XPath, "(//p[text()='User Permissions']//..//label[text()='All'])[1]");

        private readonly ElementLocator
         addChkboxLabelAdminUserPermissions = new ElementLocator(Locator.XPath, "(//p[text()='User Permissions']//..//label[text()='Add'])[1]");

        private readonly ElementLocator
         allChkboxLabelAdminRoles = new ElementLocator(Locator.XPath, "(//p[text()='Roles']//..//label[text()='All'])[2]");

        private readonly ElementLocator
         addChkboxLabelAdminRoles = new ElementLocator(Locator.XPath, "(//p[text()='Roles']//..//label[text()='Add'])[2]");

        private readonly ElementLocator
         allChkboxLabelRenewalTasks = new ElementLocator(Locator.XPath, "//p[text()='Renewal Task']//..//label[text()='All']");

        private readonly ElementLocator
         addChkboxLabelRenewalTasks = new ElementLocator(Locator.XPath, "//p[text()='Renewal Task']//..//label[text()='Add']");

        private readonly ElementLocator
            memberManagementPermissionsList = new ElementLocator(Locator.XPath, "//p[text()='Members and Organizational Accounts']//..//label");

        private readonly ElementLocator
            memberManagementSelectedPermissionList = new ElementLocator(Locator.XPath, "//p[text()='Members and Organizational Accounts']//..//div[@class='ui checked disabled checkbox checkAccordian privilege']//label");

        private readonly ElementLocator
            committeeManagementPermissionsList = new ElementLocator(Locator.XPath, "//p[text()='Committees']//..//label");

        private readonly ElementLocator
            committeeManagementSelectedPermissionsList = new ElementLocator(Locator.XPath, "//p[text()='Committees']//..//div[@class='ui checked disabled checkbox checkAccordian privilege']//label");

        private readonly ElementLocator
            admintab = new ElementLocator(Locator.XPath, "//div[text()='Admin']");

        private readonly ElementLocator
            adminUserPermissions = new ElementLocator(Locator.XPath, "//p[text()='User Permissions']/following-sibling::div[position()=1]/div//label");

        private readonly ElementLocator
            adminRolePermissions = new ElementLocator(Locator.XPath, "//p[text()='User Permissions']/following-sibling::div[position()=2]/div//label");

        private readonly ElementLocator
           adminSelectedUserPermissions = new ElementLocator(Locator.XPath, "//p[text()='User Permissions']/following-sibling::div[position()=1]//div[@class='ui checked checkbox checkAccordian privilege']//label");

        private readonly ElementLocator
           adminSelectedRolesPermissions = new ElementLocator(Locator.XPath, "//p[text()='Roles']/following-sibling::div[position()=1]//div[@class='ui checked checkbox checkAccordian privilege']//label");

        private readonly ElementLocator
            renewalTasksPermissions = new ElementLocator(Locator.XPath, "//p[text()='Renewal Task']//..//label");

        private readonly ElementLocator
           renewalTasksSelectedPermissions = new ElementLocator(Locator.XPath, "//p[text()='Renewal Task']//..//div[@class='ui checked checkbox checkAccordian privilege']//label");

        private readonly ElementLocator
           newlyAddedRoleInManageRoleList = new ElementLocator(Locator.XPath, "//div[@class='roleCard']//h4[text()='{0}']");

        private readonly ElementLocator
            newlyAddedRoleStatusOnRoleCard = new ElementLocator(Locator.XPath, "//span[text()='Active']//following::span[text()='{0}']");

        private readonly ElementLocator
            newlyAddedRoleEditIcon = new ElementLocator(Locator.XPath, "//i[@class='pencil icon']//following::span[text()='{0}']");

        private readonly ElementLocator
            newlyAddedRoleDescription = new ElementLocator(Locator.XPath, "//div[@class='roleCard']//..//p[@class='detail']//span[text()='{0}']");

        private readonly ElementLocator
            newlyAddedRoleAuditLogIcon = new ElementLocator(Locator.XPath, "//i[@class='list alternate outline icon']//following::span[text()='{0}']");

        private readonly ElementLocator
            newlyAddedRolePrivilegesList = new ElementLocator(Locator.XPath, "//span[text()='{0}']//preceding::h4[1]//..//div[@class='privileges']//ul//..//span[@class='privelegeName']");

        private string nmrolesTasksHeader = "Manage Roles Tasks Header";
        private string nmaddRoleBtn = "Add Role Button";
        private string nmaddRolePopupHeader = "Add Role Popup Header";
        private string nmprivilegeCheckBoxforAll = "Check box- All";
        private string nmroleNameTextBox = "Role Name Text Box";
        private string nmroleDescription = "Role Description";
        private string nmcommitteeMgmtTab = "Committee Management Tab";
        private string nmadminTab = "Admin Tab";
        private string nmRenewalTasksTab = "Renewal Tasks Tab";
        private string nmroleInAlphabeticalOrder = "role In Alphabetical Order";
        private string nmupdateBtn = "Update Button";
        private string nmrolesaveBtn = "Role Save Button";
        private string nmroleNameErrorMessage = "Role Name Error Message";
        private string nmsuccessmessage = "Role Name Error Message";
        private string nmallChkboxLabelMembersManagement = "All privilege checkbox under Members and Organizational Accounts section";
        private string nmallChkboxLabelCommitteeManagement = "All privilege checkbox under Committee section";
        private string nmallChkboxLabelAdminUserPermissions = "All privilege checkbox under Admin User Permissions";
        private string nmaddChkboxLabelAdminUserPermissions = "Add privilege checkbox under Admin User Permissions";
        private string nmallChkboxLabelAdminRoles = "All privilege checkbox under Admin Roles";
        private string nmaddChkboxLabelAdminRoles = "Add privilege checkbox under Admin Roles";
        private string nmallChkboxLabelRenewalTasks = "All privilege checkbox under Renewal Tasks";
        private string nmaddChkboxLabelRenewalTasks = "Add privilege checkbox under Renewal Tasks";
        private string nmnewlyAddedRoleActiveStatus = "Newly Added Role Active Status";
        private string nmnewlyAddedRoleEditIcon = "Newly Added Role Edit Icon";
        private string nmnewlyAddedRoleDescription = "Newly Added Role Description";
        private string nmnewlyAddedRoleAuditLogIcon = "Newly Added Role Audit Log Icon";
        private string nmmemberManagementPermissionsList = "Members and Organizational Accounts section Permissions List";
        private string nmCommitteeManagementPermissionsList = "Committee Management section Permissions List";
        private string nmAdminUserPermissionsPermissionsList = "Admin UserPermissions section Permissions List";
        private string nmAdminRolesPermissionsList = "Admin Roles section Permissions List";
        private string nmRenewalTasksPermissionsList = "Renewal Tasks section Permissions List";
        private string nmnewlyAddedRolePrivilegesList = "Newly Added Role Privileges List";

        public InternalStaffManageRolesPage(DriverContext driverContext)
           : base(driverContext)
        {
        }

        public void IsManageRolessHeaderVisible()
        {
            Verify.WaitUntilElementIsFoundWithSoftAssertion(this.DriverContext, this.rolesTasksHeader, this.nmrolesTasksHeader, BaseConfiguration.LongTimeout);
            Verify.IsElementVisibleWithSoftAssertion(this.DriverContext, this.rolesTasksHeader, this.nmrolesTasksHeader);
        }

        public void IsAddRoleBtnClickable()
        {
            this.Driver.WaitUntilElementIsNoLongerFound(this.loadbutton, BaseConfiguration.LongTimeout, this.nmaddRoleBtn);
            this.Driver.IsElementVisible(this.addRoleBtn, this.nmaddRoleBtn);
            this.Driver.IsElementClickable(this.addRoleBtn, this.nmaddRoleBtn);
        }

        public void IsAddRolePopupVisible()
        {
            this.Driver.WaitForPageLoad();
            this.Driver.IsElementVisible(this.addRolePopupHeader, this.nmaddRolePopupHeader);
        }

        public void IsUserAbleToGivePrivilegesToNewRole()
        {
            try
            {
                this.Driver.WaitUntilElementIsFound(this.committeeMgmtTab, BaseConfiguration.LongTimeout);
                this.Driver.IsElementVisible(this.committeeMgmtTab, this.nmcommitteeMgmtTab);
                this.Driver.IsElementClickable(this.committeeMgmtTab, this.nmcommitteeMgmtTab);
                this.Driver.MouseOverOnWebElementAndClick(this.newRolePrivilegeCommitteeAll, this.nmcommitteeMgmtTab);
                bool chkBoxStatusCommittee = this.Driver.IsCheckBoxChecked(this.newRolePrivilegeCommitteeAll);
                if (!chkBoxStatusCommittee)
                {
                    this.Driver.SelectCheckBoxifUnselected(this.newRolePrivilegeCommitteeAll);
                }

                this.Driver.WaitUntilElementIsFound(this.adminTab, BaseConfiguration.LongTimeout);
                this.Driver.IsElementVisible(this.adminTab, this.nmadminTab);
                this.Driver.IsElementClickable(this.adminTab, this.nmadminTab);
                bool chkBoxStatusRoles = this.Driver.IsCheckBoxChecked(this.newRolePrivilegeRolesAdd);
                if (!chkBoxStatusRoles)
                {
                    this.Driver.SelectCheckBoxifUnselected(this.newRolePrivilegeRolesAdd);
                }

                this.Driver.WaitUntilElementIsFound(this.renewalTasksTab, BaseConfiguration.LongTimeout);
                this.Driver.IsElementVisible(this.renewalTasksTab, this.nmRenewalTasksTab);
                this.Driver.IsElementClickable(this.renewalTasksTab, this.nmRenewalTasksTab);
                bool chkBoxStatusRenewalAdd = this.Driver.IsCheckBoxChecked(this.newRolePrivilegeRenewalAdd);
                if (!chkBoxStatusRenewalAdd)
                {
                    this.Driver.SelectCheckBoxifUnselected(this.newRolePrivilegeRenewalAdd);
                }

                bool chkBoxStatusRenewalUpdate = this.Driver.IsCheckBoxChecked(this.newRolePrivilegeRenewalUpdate);
                if (!chkBoxStatusRenewalUpdate)
                {
                    this.Driver.SelectCheckBoxifUnselected(this.newRolePrivilegeRenewalUpdate);
                }

                bool chkBoxStatusRenewalExport = this.Driver.IsCheckBoxChecked(this.newRolePrivilegeRenewalExport);
                if (!chkBoxStatusRenewalExport)
                {
                    this.Driver.SelectCheckBoxifUnselected(this.newRolePrivilegeRenewalExport);
                }

                DriverContext.ExtentStepTest.Log(LogStatus.Pass, "To verify Privileges are given for New Role added  ", " Privilege CheckBoxs are clicked successfully");
                Logger.Info(" Privilege checkboxes are checked successfully");
            }
            catch (Exception ex)
            {
                Logger.Error("Failed  Due to exception: " + ex.ToString());

                DriverContext.ExtentStepTest.Log(LogStatus.Fail, "To verify if Privilege CheckBoxes is Selected ", "An exception occurred while giving Privileges");
                throw;
            }
        }

        public void IsUserableToEnterRoleName(string roleName)
        {
            Verify.IsElementVisibleWithSoftAssertion(this.DriverContext, this.roleNameTextBox, this.nmroleNameTextBox);
            Verify.EnterTextWithSoftAssertion(this.DriverContext, this.roleNameTextBox, roleName, this.nmroleNameTextBox);
        }

        public void IsUserAbleToEnterTextInRoleDescription(string text)
        {
            Verify.IsElementVisibleWithSoftAssertion(this.DriverContext, this.roleDescription, this.nmroleDescription);
            Verify.EnterTextWithSoftAssertion(this.DriverContext, this.roleDescription, text, this.nmroleDescription);
        }

        public void AreNewlyAddedRoleInInAlphabeticalOrder()
        {
            Verify.AreElementsSortedInAlphabeticalOrderWithSoftAssertion(this.DriverContext, this.roleInAlphabeticalOrder, this.nmroleInAlphabeticalOrder);
        }

        public void IsUseAbleToViewMembershipManagementPrivilegesSection(string name)
        {
            Verify.IsExpectedTextMatchWithActualTextWithSoftAssertion(this.DriverContext, this.membershipManagementPrivileges.Format(name), name);
        }

        public string IsGetExistingRoleNameFromRolesList()
        {
            string existingRole = string.Empty;
            try
            {
                this.Driver.WaitForPageLoad();
                var webElementLocator = this.Driver.GetElements(this.roleInAlphabeticalOrder);
                ReadOnlyCollection<IWebElement> collection = new ReadOnlyCollection<IWebElement>(webElementLocator);
                if (collection.Count >= 0)
                {
                    existingRole = collection[0].Text;
                }
                else
                {
                    DriverContext.ExtentStepTest.Log(LogStatus.Fail, "To verify the existence of Role in the Roles List", "No Role Exists in the Roles List");
                    Logger.Info("No Role Exists in the Roles List");
                }
            }
            catch (Exception ex)
            {
                DriverContext.ExtentStepTest.Log(LogStatus.Fail, "To verify the existence of Role in the Roles List", "No Role Exists in the Roles List");
                Logger.Info("No Role Exists in the Roles List" + ex.Message);
                throw;
            }

            return existingRole;
        }

        public void IsEditIconClickOnExistingRole()
        {
            try
            {
                this.Driver.WaitForPageLoad();
                var webElementLocator = this.Driver.GetElements(this.roleEditIcon);
                ReadOnlyCollection<IWebElement> collection = new ReadOnlyCollection<IWebElement>(webElementLocator);
                if (collection.Count > 0)
                {
                    collection[1].Click();
                }
                else
                {
                    DriverContext.ExtentStepTest.Log(LogStatus.Fail, "To verify the existence of Role in the Roles List", "Role does'nt Exists in the Roles List");
                    Logger.Info("Role does'nt Exists in the Roles List");
                }
            }
            catch (Exception ex)
            {
                DriverContext.ExtentStepTest.Log(LogStatus.Fail, "To verify the existence of Role in the Roles List", "Role does'nt Exists in the Roles List");
                Logger.Info("Role does'nt Exists in the Roles List" + ex.Message);
                throw;
            }
        }

        public void IsRoleNameErrorMessageDisplayed(string expected)
        {
            this.Driver.IsElementVisible(this.roleNameErrorMessage, this.nmroleNameErrorMessage);
            this.IsErrorMessageDisplayed(expected, this.roleNameErrorMessage);
        }

        public void IsErrorMessageDisplayed(string expected, ElementLocator elemlocatar)
        {
            this.Driver.WaitUntilElementIsFound(elemlocatar, BaseConfiguration.LongTimeout);
            this.Driver.IsExpectedTextMatchWithActualText(elemlocatar, expected);
        }

        public void IsUserableToUpdateTask()
        {
            this.Driver.IsElementVisible(this.updateBtn, this.nmupdateBtn);
            this.Driver.IsElementClickable(this.updateBtn, this.nmupdateBtn);
        }

        public void IsUserAbleToclickOnSaveButtonOfRole()
        {
            this.Driver.IsElementVisible(this.rolesavebutton, this.nmrolesaveBtn);
            this.Driver.IsElementClickable(this.rolesavebutton, this.nmrolesaveBtn);
        }

        public void IsRoleCreatedSuccessfullMessageDisplayed(string message)
        {
            Verify.WaitUntilElementIsFoundWithSoftAssertion(this.DriverContext, this.loadbutton, this.nmsuccessmessage, BaseConfiguration.LongTimeout);
            Verify.IsElementVisibleWithSoftAssertion(this.DriverContext, this.successmsg, this.nmsuccessmessage);
        }

        public void IsUserabletoViewNewlyAddedRoleDetailsOnRoleCard(string titleName, string status, string description)
        {
            this.Driver.ScrollToWebElement(this.newlyAddedRoleDescription.Format(description));
            bool roleDescExists = this.Driver.IsElementPresent(this.newlyAddedRoleDescription.Format(description), BaseConfiguration.LongTimeout);

            if (roleDescExists)
            {
                this.Driver.IsElementPresentOrNot(this.newlyAddedRoleStatusOnRoleCard.Format(description), this.nmnewlyAddedRoleActiveStatus, string.Empty);
                this.Driver.IsElementPresentOrNot(this.newlyAddedRoleEditIcon.Format(description), this.nmnewlyAddedRoleEditIcon, string.Empty);
                this.Driver.IsElementPresentOrNot(this.newlyAddedRoleAuditLogIcon.Format(description), this.nmnewlyAddedRoleAuditLogIcon, string.Empty);
                this.Driver.IsElementPresentOrNot(this.newlyAddedRoleDescription.Format(description), this.nmnewlyAddedRoleDescription, string.Empty);
            }
        }

        public void IsUserAbleToViewPopupTitle()
        {
            this.Driver.IsElementVisible(this.popwindowTitle, this.nmaddRolePopupHeader);
        }

        public void IsUserAbleToUpdateRolesToExistingUser()
        {
            try
            {
                var webelement = this.Driver.GetElement(this.privilegeCheckBoxforAll, BaseConfiguration.LongTimeout);
                bool value = this.Driver.IsCheckBoxChecked(this.privilegeCheckBoxforAll);
                if (value == false)
                {
                    this.Driver.JavaScriptClick(webelement, this.nmprivilegeCheckBoxforAll);
                    DriverContext.ExtentStepTest.Log(LogStatus.Pass, "To verify All- checkBox is clicked ", " All - checkBox is clicked successfully");
                    Logger.Info(" All-  checkBox is clicked successfully");
                }
                else
                {
                    this.Driver.JavaScriptClick(webelement, this.nmprivilegeCheckBoxforAll);
                    DriverContext.ExtentStepTest.Log(LogStatus.Pass, "To verify All-checkBox status ", " All- checkBox is checked already");
                }
            }
            catch (Exception)
            {
                DriverContext.ExtentStepTest.Log(LogStatus.Fail, "To verify  All- checkBox is not clicked ", " All - checkBox is not clicked ");
                throw;
            }
        }

        public void IsUserableToUpdateRoles()
        {
            this.Driver.IsElementVisible(this.updateButton, this.nmupdateBtn);
            this.Driver.IsElementClickable(this.updateButton, this.nmupdateBtn);
        }

        public void IsUseAbleToClickSelectedPrivilegesSection(string name)
        {
            this.Driver.MouseOverOnWebElementAndClick(this.membershipManagementPrivileges.Format(name), name);
        }

        public void IsUserAbleToSelectPrivilegesUnderMembersManagementSection()
        {
            Verify.IsElementClickableWithSoftAssertion(this.DriverContext, this.allChkboxLabelMembersManagement, this.nmallChkboxLabelMembersManagement);
        }

        public void IsUserAbleToSelectPrivilegesUnderCommitteeManagementSection()
        {
            Verify.IsElementClickableWithSoftAssertion(this.DriverContext, this.allChkboxLabelCommitteeManagement, this.nmallChkboxLabelCommitteeManagement);
        }

        public void IsUserAbleToSelectPrivilegesUnderUserPermissionsUnderAdminSection()
        {
            Verify.IsElementClickableWithSoftAssertion(this.DriverContext, this.allChkboxLabelAdminUserPermissions, this.nmallChkboxLabelAdminUserPermissions);
            Verify.IsElementClickableWithSoftAssertion(this.DriverContext, this.addChkboxLabelAdminUserPermissions, this.nmaddChkboxLabelAdminUserPermissions);
        }

        public void IsUserAbleToSelectPrivilegesUnderRolesUnderAdminSection()
        {
            Verify.IsElementClickableWithSoftAssertion(this.DriverContext, this.allChkboxLabelAdminRoles, this.nmallChkboxLabelAdminRoles);
            Verify.IsElementClickableWithSoftAssertion(this.DriverContext, this.addChkboxLabelAdminRoles, this.nmaddChkboxLabelAdminRoles);
        }

        public void IsUserAbleToSelectPrivilegesUnderRenewalTasksSection()
        {
            Verify.IsElementClickableWithSoftAssertion(this.DriverContext, this.allChkboxLabelRenewalTasks, this.nmallChkboxLabelRenewalTasks);
            Verify.IsElementClickableWithSoftAssertion(this.DriverContext, this.addChkboxLabelRenewalTasks, this.nmaddChkboxLabelRenewalTasks);
        }

        public void IsUserAbleToViewSpecificPermissionsUnderMembersManagementSection(List<string> expectedMembersPermissionsList)
        {
            List<string> actualMembersPermissionsList = new List<string>();

            Verify.WaitUntilElementIsFoundWithSoftAssertion(this.DriverContext, this.memberManagementPermissionsList, this.nmmemberManagementPermissionsList, BaseConfiguration.LongTimeout);
            IList<IWebElement> items = this.Driver.GetElements(this.memberManagementPermissionsList);
            foreach (var permission in items)
            {
                actualMembersPermissionsList.Add(permission.Text);
            }

            string passmessage = " actual Members and Organizational Accounts Management Permissions " + string.Join(",", actualMembersPermissionsList) + " matching with expected Members and Organizational Accounts Management Permissions " + string.Join(",", expectedMembersPermissionsList) + "| actual Members and Organizational Accounts Management Permissions " + string.Join(",", actualMembersPermissionsList) + " matching with expected Members and Organizational Accounts Management Permissions " + string.Join(",", expectedMembersPermissionsList);
            string errormessage = " actual Members and Organizational Accounts Management Permissions " + string.Join(",", actualMembersPermissionsList) + " matching with expected Members and Organizational Accounts Management Permissions " + string.Join(",", expectedMembersPermissionsList) + "| actual Members and Organizational Accounts Management Permissions " + string.Join(",", actualMembersPermissionsList) + " are not matching with expected Members and Organizational Accounts Management Permissions " + string.Join(",", expectedMembersPermissionsList);
            Verify.CompareActualExpectedListWithSoftAssertion(this.DriverContext, actualMembersPermissionsList, expectedMembersPermissionsList, passmessage, errormessage);
        }

        public void IsUserAbleToViewSpecificPermissionsUnderCommitteeManagementSection(List<string> expectedCommitteePermissionsList)
        {
            List<string> actualCommitteePermissionsList = new List<string>();

            this.Driver.ScrollToWebElement(this.admintab);
            Verify.WaitUntilElementIsFoundWithSoftAssertion(this.DriverContext, this.committeeManagementPermissionsList, this.nmCommitteeManagementPermissionsList, BaseConfiguration.LongTimeout);
            IList<IWebElement> items = this.Driver.GetElements(this.committeeManagementPermissionsList);
            foreach (var permission in items)
            {
                actualCommitteePermissionsList.Add(permission.Text);
            }

            string passmessage = " Actual Committee Management Permissions " + string.Join(",", actualCommitteePermissionsList) + " matching with expected Committee Management Permissions " + string.Join(",", expectedCommitteePermissionsList) + "| actual Committee Management Permissions " + string.Join(",", actualCommitteePermissionsList) + " matching with expected Committee Management Permissions " + string.Join(",", expectedCommitteePermissionsList);
            string errormessage = " Actual Committee Management Permissions " + string.Join(",", actualCommitteePermissionsList) + " matching with expected Committee Management Permissions " + string.Join(",", expectedCommitteePermissionsList) + "| actual Committee Management Permissions " + string.Join(",", actualCommitteePermissionsList) + " are not matching with expected Committee Management Permissions " + string.Join(",", expectedCommitteePermissionsList);
            Verify.CompareActualExpectedListWithSoftAssertion(this.DriverContext, actualCommitteePermissionsList, expectedCommitteePermissionsList, passmessage, errormessage);
        }

        public void IsUserAbleToViewSpecificPermissionsUnderUserPermissionsUnderAdminSection(List<string> expectedAdminUserPermissionsList)
        {
            List<string> actualAdminUserPermissionsList = new List<string>();
            Verify.WaitUntilElementIsFoundWithSoftAssertion(this.DriverContext, this.adminUserPermissions, this.nmAdminUserPermissionsPermissionsList, BaseConfiguration.LongTimeout);
            IList<IWebElement> items = this.Driver.GetElements(this.adminUserPermissions);
            foreach (var permission in items)
            {
                actualAdminUserPermissionsList.Add(permission.Text);
            }

            string passmessage = " actual Admin User Permissions " + string.Join(",", actualAdminUserPermissionsList) + " matching with expected Admin User Permissions " + string.Join(",", expectedAdminUserPermissionsList) + "| actual Admin User Permissions " + string.Join(",", actualAdminUserPermissionsList) + " matching with expected Admin User Permissions " + string.Join(",", expectedAdminUserPermissionsList);
            string errormessage = " actual Admin User Permissions " + string.Join(",", actualAdminUserPermissionsList) + " matching with expected Admin User Permissions " + string.Join(",", expectedAdminUserPermissionsList) + "| actual Admin User Permissions " + string.Join(",", actualAdminUserPermissionsList) + " are not matching with expected Admin User Permissions " + string.Join(",", expectedAdminUserPermissionsList);
            Verify.CompareActualExpectedListWithSoftAssertion(this.DriverContext, actualAdminUserPermissionsList, expectedAdminUserPermissionsList, passmessage, errormessage);
        }

        public void IsUserAbleToViewSpecificPermissionsUnderRolesUnderAdminSection(List<string> expectedAdminRolePermissionsList)
        {
            List<string> actualAdminRolePermissionsList = new List<string>();
            Verify.WaitUntilElementIsFoundWithSoftAssertion(this.DriverContext, this.adminRolePermissions, this.nmAdminRolesPermissionsList, BaseConfiguration.LongTimeout);
            IList<IWebElement> items = this.Driver.GetElements(this.adminRolePermissions);
            foreach (var permission in items)
            {
                actualAdminRolePermissionsList.Add(permission.Text);
            }

            string passmessage = " actual Admin Role Permissions " + string.Join(",", actualAdminRolePermissionsList) + " matching with expected Admin Role Permissions " + string.Join(",", expectedAdminRolePermissionsList) + "| actual Admin Role Permissions " + string.Join(",", actualAdminRolePermissionsList) + " matching with expected Admin Role Permissions " + string.Join(",", expectedAdminRolePermissionsList);
            string errormessage = " actual Admin Role Permissions " + string.Join(",", actualAdminRolePermissionsList) + " matching with expected Admin Role Permissions " + string.Join(",", expectedAdminRolePermissionsList) + "| actual Admin Role Permissions " + string.Join(",", actualAdminRolePermissionsList) + " are not matching with expected Admin Role Permissions " + string.Join(",", expectedAdminRolePermissionsList);
            Verify.CompareActualExpectedListWithSoftAssertion(this.DriverContext, actualAdminRolePermissionsList, expectedAdminRolePermissionsList, passmessage, errormessage);
        }

        public void IsUserAbleToViewSpecificPermissionsUnderRenewalTasksSection(List<string> expectedRenewalTaskPermissionsList)
        {
            List<string> actualRenewalTaskPermissionsList = new List<string>();
            Verify.WaitUntilElementIsFoundWithSoftAssertion(this.DriverContext, this.renewalTasksPermissions, this.nmRenewalTasksPermissionsList, BaseConfiguration.LongTimeout);
            IList<IWebElement> items = this.Driver.GetElements(this.renewalTasksPermissions);
            foreach (var permission in items)
            {
                actualRenewalTaskPermissionsList.Add(permission.Text);
            }

            string passmessage = " actual Renewal Task Permissions " + string.Join(",", actualRenewalTaskPermissionsList) + " matching with expected Renewal Task Permissions " + string.Join(",", expectedRenewalTaskPermissionsList) + "| actual Renewal Task Permissions " + string.Join(",", actualRenewalTaskPermissionsList) + " matching with expected Renewal Task Permissions " + string.Join(",", expectedRenewalTaskPermissionsList);
            string errormessage = " actual Renewal Task Permissions " + string.Join(",", actualRenewalTaskPermissionsList) + " matching with expected Renewal Task Permissions " + string.Join(",", expectedRenewalTaskPermissionsList) + "| actual Renewal Task Permissions " + string.Join(",", actualRenewalTaskPermissionsList) + " are not matching with expected Renewal Task Permissions " + string.Join(",", expectedRenewalTaskPermissionsList);
            Verify.CompareActualExpectedListWithSoftAssertion(this.DriverContext, actualRenewalTaskPermissionsList, expectedRenewalTaskPermissionsList, passmessage, errormessage);
        }

        public void IsUserAbleToViewPrivilegesOnNewlyAddedRoleCard(string description, List<string> expectedPrivilegeSectionsList)
        {
            List<string> actualPrivilegeSectionsList = new List<string>();
            this.Driver.ScrollToWebElement(this.newlyAddedRoleDescription.Format(description));
            Verify.WaitUntilElementIsFoundWithSoftAssertion(this.DriverContext, this.newlyAddedRolePrivilegesList.Format(description), this.nmnewlyAddedRolePrivilegesList, BaseConfiguration.LongTimeout);
            IList<IWebElement> items = this.Driver.GetElements(this.newlyAddedRolePrivilegesList.Format(description));
            foreach (var permission in items)
            {
                actualPrivilegeSectionsList.Add(permission.Text);
            }

            string passmessage = " actual Privilege Sections List " + string.Join(",", actualPrivilegeSectionsList) + " matching with expected Privilege Sections List " + string.Join(",", expectedPrivilegeSectionsList) + "| actual Privilege Sections List " + string.Join(",", actualPrivilegeSectionsList) + " matching with expected Privilege Sections List " + string.Join(",", expectedPrivilegeSectionsList);
            string errormessage = " actual Privilege Sections List " + string.Join(",", actualPrivilegeSectionsList) + " matching with expected Privilege Sections List " + string.Join(",", expectedPrivilegeSectionsList) + "| actual Privilege Sections List " + string.Join(",", actualPrivilegeSectionsList) + " are not matching with expected Privilege Sections List " + string.Join(",", expectedPrivilegeSectionsList);
            Verify.CompareActualExpectedListWithSoftAssertion(this.DriverContext, actualPrivilegeSectionsList, expectedPrivilegeSectionsList, passmessage, errormessage);
        }

        public void GetUserRoleExistsInDB(string roleName, string compareText, string message, List<AddUserRoleFunctionalFlow> userrole)
        {
            try
            {
                this.Driver.GetSingleValueFromDBCompareWithExpectedValue(string.Format(SqlQuery.FunctionalAddUserRole, roleName), compareText, message);
                DataSet resultTable = SqlHelper.ExecuteSqlCommandQuery(string.Format(SqlQuery.FunctionalUserRoleDbValidation, roleName));
                DataTable expectedTable = SqlHelper.ListToDataTable(userrole);
                DataTable result = SqlHelper.CompareTwoDatatables(expectedTable, resultTable.Tables[0]);
                Assert.AreEqual(0, result.Rows.Count);
                DriverContext.ExtentStepTest.Log(LogStatus.Pass, "To verify Permissions assigned to this Role " + roleName + " are  matching with Database Permission values", "Permissions assigned to this Role " + roleName + " are matching with Database Permission values");
                Logger.Info("Permissions assigned to this Role " + roleName + " are  matching with Database Permission values");
            }
            catch
            {
                DriverContext.ExtentStepTest.Log(LogStatus.Fail, "To verify Permissions assigned to this Role " + roleName + " are  matching with Database Permission values", "Permissions assigned to this Role " + roleName + " are not matching with Database Permission values");
                Logger.Info("Permissions assigned to this Role " + roleName + " are not  matching with Database Permission values");
            }
        }
    }
}