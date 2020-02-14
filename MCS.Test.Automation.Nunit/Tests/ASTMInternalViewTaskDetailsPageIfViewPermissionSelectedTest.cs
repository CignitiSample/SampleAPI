// <copyright file="ASTMInternalViewTaskDetailsPageIfViewPermissionSelectedTest.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace MCS.Test.Automation.Tests.NUnit.Tests
{
    using System;
    using System.Collections.Generic;
    using global::NUnit.Framework;
    using MCS.Test.Automation.Tests.NUnit.DataDriven;
    using MCS.Test.Automation.Tests.PageObjects.PageObjects.MCS;

    [TestFixture]
#pragma warning disable SA1600 // Elements should be documented
    public class ASTMInternalViewTaskDetailsPageIfViewPermissionSelectedTest : ProjectTestBase
#pragma warning restore SA1600 // Elements should be documented
    {
        [Test]
        [Category("ASTM_Internal")]
        [Category("SP17")]
        [Category("Regression")]
        [TestCaseDescription(Id = "MCS2-2810", Name = "")]
        [TestCaseSource(typeof(TestData), "GetDetailsFromXML", new object[] { "ViewTaskDetailsPageIfViewPermissionSelected" })]
#pragma warning disable SA1600 // Elements should be documented
        public void ASTMInternalViewTaskDetailsPageIfViewPermissionSelected(Dictionary<string, string> parameters)
#pragma warning restore SA1600 // Elements should be documented
        {
            var loginPage = new LoginPage(this.DriverContext);
            var homePage = new HomePage(this.DriverContext);
            var internalhomePage = new InternalStaffHomePage(this.DriverContext);
            var internalStaffUsersPage = new InternallStaffUsersPage(this.DriverContext);
            var renewalTaskPage = new InternalStaffRenewalTasksPage(this.DriverContext);
            string previlegeTaskName = parameters["previlegeTaskName"].Trim();
            string previlegeCheckBoxType = parameters["previlegeCheckBoxType"].Trim();
            string uname = parameters["uname"].Trim();
            string password = parameters["password"].Trim();
            string previlegeName = parameters["previlegeName"].Trim();
            string successmessage = parameters["successmessage"].Trim();

            loginPage.OpenASTMInternalLandingPage();
            loginPage.IsCustomerLogoDisplayed();

            loginPage.IsUserAbletoLoginMCSApp(uname, password);
            internalhomePage.IsCustomerLogoDisplayed();
            homePage.IsLoggedInUserDisplayed(uname);
            homePage.SelectUserPermissionFromAdminMenuitem();

            internalStaffUsersPage.EnterTextAndSelectAutoSuggestion(uname);
            internalStaffUsersPage.IsUserNameClickableFromUsersList();
            internalStaffUsersPage.IsUserAbleToClickOnEditIcon();

            internalStaffUsersPage.IsAdditionalPreviligesDropdownClickable(previlegeName);
            internalStaffUsersPage.IsUserableToEnablePermission(previlegeTaskName, previlegeCheckBoxType);
            internalStaffUsersPage.IsUserIsAbleToClickOnUpdateButton();
            internalStaffUsersPage.IsUserUpdatedMessageDisplayedSuccessfully(successmessage);

            homePage.IsSignOutClickable();
            loginPage.OpenASTMInternalLandingPage();
            loginPage.IsCustomerLogoDisplayed();
            loginPage.IsUserAbletoLoginMCSApp(uname, password);
            internalhomePage.IsCustomerLogoDisplayed();
            homePage.IsLoggedInUserDisplayed(uname);
            internalhomePage.IsRenewalTasksMenuClickable();
            renewalTaskPage.IsUserableToSelectTaskCard();
            string title = parameters["title"].Trim();
            renewalTaskPage.IsUserableToViewTaskDetailsTitle(title);
        }
    }
}
