// <copyright file="ManageCommitteeTypesPage.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace MCS.Test.Automation.Tests.PageObjects.PageObjects.MCS
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Data;
    using System.Linq;
    using global::MCS.Test.Automation.Common;
    using global::MCS.Test.Automation.Common.Extensions;
    using global::MCS.Test.Automation.Common.Helpers;
    using global::MCS.Test.Automation.Common.Types;
    using global::MCS.Test.Automation.Tests.PageObjects;
    using global::NUnit.Framework;
    using NLog;
    using OpenQA.Selenium;
    using OpenQA.Selenium.Interactions;
    using RelevantCodes.ExtentReports;

    public class ManageCommitteeTypesPage : ProjectPageBase
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        private readonly ElementLocator
            committeTypeheader = new ElementLocator(Locator.CssSelector, "div.headingTitle.clearfix h2");

        private readonly ElementLocator
            addCommitteeTypeButton = new ElementLocator(Locator.CssSelector, "button.ui.secondary.button");

        private readonly ElementLocator
            listofcommitteeType = new ElementLocator(Locator.CssSelector, "table.customTable.CommitteeTable tbody tr td a");

        private readonly ElementLocator
            committeeTypetext = new ElementLocator(Locator.Name, "CommitteeTypeName");

        private readonly ElementLocator
           manageCommitteeHierarchytext = new ElementLocator(Locator.Name, "level");

        private readonly ElementLocator
            levelCallOutGreyBox = new ElementLocator(Locator.XPath, "//section[@class='greyBox']");

        private readonly ElementLocator
            addCommitteeTypesavebutton = new ElementLocator(Locator.CssSelector, "button.ui.primary.button");

        private readonly ElementLocator
            addLevel = new ElementLocator(Locator.XPath, "//a[@class='addBtn']");

        private readonly ElementLocator
            classificationCheckBox = new ElementLocator(Locator.XPath, "//label[text()='Classification']");

        private readonly ElementLocator
            ////scopeCheckBox = new ElementLocator(Locator.XPath, "//label[text()='Scope']");
            scopeCheckBox = new ElementLocator(Locator.XPath, "//div[text()='Eligible Fields']//..//label[text()='Scope']");

        private readonly ElementLocator
           ////overviewCheckBox = new ElementLocator(Locator.XPath, "//label[text()='Overview']");
           overviewCheckBox = new ElementLocator(Locator.XPath, "//div[text()='Eligible Fields']//..//label[text()='Overview']");

        private readonly ElementLocator
          bylawsCheckBox = new ElementLocator(Locator.XPath, "//label[text()='Bylaws']");

        private readonly ElementLocator
            levelOneManageEligibleFieldLink = new ElementLocator(Locator.XPath, "(//a[text()='Manage Eligible Fields'])[1]");

        private readonly ElementLocator
           levelTwoManageEligibleFieldLink = new ElementLocator(Locator.XPath, "(//a[text()='Manage Eligible Fields'])[2]");

        private readonly ElementLocator
            successfullMsg = new ElementLocator(Locator.CssSelector, "div.ui.compact.message.success div.content p");

        private readonly ElementLocator
            committeeTypeError = new ElementLocator(Locator.CssSelector, "span.errorMessage");

        private readonly ElementLocator
            levelErrorMsg = new ElementLocator(Locator.CssSelector, "span.errorMessage.mt0");

        private readonly ElementLocator
            atleastOneLevelErrorMsg = new ElementLocator(Locator.XPath, "//span[text()='Please add at least one level for the Committee Type.']");

        private readonly ElementLocator
            balanceRequiredForNoButton = new ElementLocator(Locator.XPath, "//label[text()='Balance Required']/..//div[contains(@class,'ui checked')]/input");

        private readonly ElementLocator
            balanceRequiredForYesButton = new ElementLocator(Locator.XPath, "//label[text()='Yes']");

        private readonly ElementLocator
           eligibleFieldsCallOutBox = new ElementLocator(Locator.XPath, "//div[contains(@class,'ui left center flowing popup transition visible manageFields')]");

        private readonly ElementLocator
            membershipTypeInBR = new ElementLocator(Locator.XPath, "(//div[@class='memClassType ']//label)[2]");

        private readonly ElementLocator
               membershipTypeInBR2 = new ElementLocator(Locator.XPath, "(//div[@id='classificationError']//label)[3]");

        private readonly ElementLocator
              thirdMembershipTypeInBR2 = new ElementLocator(Locator.XPath, "(//div[@id='classificationError']//label)[10]");

        private readonly ElementLocator
            producerMembershipTypeBR = new ElementLocator(Locator.XPath, "(//div[@class='memClassType ']//label)[8]");

        private readonly ElementLocator
           thirdMembershipTypeBR = new ElementLocator(Locator.XPath, "(//div[@class='memClassType ']//label)[9]");

        private readonly ElementLocator
            newClassMembershipTypeInBR2 = new ElementLocator(Locator.XPath, "(//div[@id='classificationError']//label)[4]");

        private readonly ElementLocator
          loadbutton = new ElementLocator(Locator.XPath, "//*[@class='ui active transition visible dimmer']");

        private readonly ElementLocator
            maxMembersPermittedDefaultValue = new ElementLocator(Locator.XPath, "//label[text()='Max. Number of Members Permitted']/..//div[contains(@class,'ui checked')]/input");

        private readonly ElementLocator
            maxMembersPermittedLabelText = new ElementLocator(Locator.XPath, "//label[text()='Max. Number of Members Permitted']");

        private readonly ElementLocator
            availableMembershipTypes = new ElementLocator(Locator.XPath, "//h3[text()='Available Membership Types']");

        private readonly ElementLocator
            selectDropdownForOperator = new ElementLocator(Locator.CssSelector, "div.ui.selection.dropdown div.text");

        private readonly ElementLocator
            operatorConditionSelection = new ElementLocator(Locator.CssSelector, "div.visible.menu.transition div span");

        private readonly ElementLocator
            brlabel = new ElementLocator(Locator.XPath, "//label[text()='Balance Required']");

        private readonly ElementLocator
             manageCommitteeHierarchyHeader = new ElementLocator(Locator.XPath, "//h5[contains(text(),'{0}')]");

        private readonly ElementLocator
            mandatoryFieldErrorMsg = new ElementLocator(Locator.CssSelector, "[class^='errorMessage']");

        private readonly ElementLocator
           membershipTypeEligibleTojoinHeader = new ElementLocator(Locator.XPath, "//h5[contains(text(),'{0}')]");

        private readonly ElementLocator
           balanceRequiredHeader = new ElementLocator(Locator.XPath, "//label[contains(text(),'{0}')]");

        private readonly ElementLocator
           enableCommitteeTypeonwebHeader = new ElementLocator(Locator.XPath, "//label[contains(text(),'{0}')]");

        private readonly ElementLocator
            errorMsgUnique = new ElementLocator(Locator.CssSelector, "div.ui.compact.message.error div.content p");

        private readonly ElementLocator
            dropDownIconforOperator = new ElementLocator(Locator.XPath, "//div[@class='operator ']//i");

        private readonly ElementLocator
            errorCommitteeTypeName = new ElementLocator(Locator.XPath, "//span[@class='errorMessage'][contains(.,'{0}')]");

        private readonly ElementLocator
           errorCommitteeTypeLevels = new ElementLocator(Locator.XPath, "//span[@class='errorMessage'][contains(.,'{0}')]");

        private readonly ElementLocator
           availableMembershipTypesHeader = new ElementLocator(Locator.XPath, "//h3[contains(text(),'{0}')]");

        private readonly ElementLocator
           selectedMembershipTypesHeader = new ElementLocator(Locator.XPath, "//h3[contains(text(),'{0}')]");

        private readonly ElementLocator
           selectedelegibletoJOinFrom = new ElementLocator(Locator.XPath, "(//div[@data-react-beautiful-dnd-draggable])[1]");

        private readonly ElementLocator
           selectedelegibletoJOinEdit = new ElementLocator(Locator.XPath, "(//div[@data-react-beautiful-dnd-draggable])[2]");

        private readonly ElementLocator
           selectedelegibletoJOinTo = new ElementLocator(Locator.XPath, "(//div[@class='sc-bxivhb bpKHpE'])[2]");

        private readonly ElementLocator
            selectdelegibletoJoinFromSelectedLane = new ElementLocator(Locator.XPath, "(//h3[text()='Selected Membership Types']/..//div[@data-react-beautiful-dnd-draggable])[1]");

        private readonly ElementLocator
            selectedMembershipTypesList = new ElementLocator(Locator.XPath, "//h3[text()='Selected Membership Types']/..//div[@data-react-beautiful-dnd-draggable]");

        private readonly ElementLocator
            selectdelegibletoJoinToAvailableLane = new ElementLocator(Locator.XPath, "(//div[@class='sc-bxivhb bpKHpE'])[1]");

        private readonly ElementLocator
            updatebutton = new ElementLocator(Locator.XPath, "//button[text()='Update ']");

        private readonly ElementLocator
           removedcommitteeInViewPage = new ElementLocator(Locator.XPath, "//span[text()='AFFILIATE']");

        private readonly ElementLocator
         removedcommitteeInAddPage = new ElementLocator(Locator.XPath, "//h3[text()='Selected Membership Types']/..//div[text()='{0}']");

        private readonly ElementLocator
           getLatestRecord = new ElementLocator(Locator.XPath, "//table[1]//tr[1]//td[1]//a[contains(.,'{0}')]");

        private readonly ElementLocator
         errorMessageBlankOnCommiteeType = new ElementLocator(Locator.XPath, "//span[@class='errorMessage']");

        private readonly ElementLocator
            incorrectInput = new ElementLocator(Locator.CssSelector, "span.errorMessage");

        private readonly ElementLocator
            mandatoryInput = new ElementLocator(Locator.CssSelector, "span.errorMessage.mt0");

        private readonly ElementLocator
        editButton = new ElementLocator(Locator.XPath, "//i[@class='pencil icon']");

        private readonly ElementLocator
        getSecondRecord = new ElementLocator(Locator.XPath, "//table[1]//tr[2]//td[1]//a//text()");

        private readonly ElementLocator
        errorHierarchyUniqueLevel = new ElementLocator(Locator.XPath, "//span[contains(.,'{0}')]");

        private readonly ElementLocator
        parentChildHierarchyLevel = new ElementLocator(Locator.XPath, "(//span[@class='comitteName'])[{0}]");

        private readonly ElementLocator
       parentChildHierarchyLevelCloseButton = new ElementLocator(Locator.XPath, "(//i[@class='close icon'])[{0}]");

        private readonly ElementLocator
       parentChildHierarchyLevelDisplay = new ElementLocator(Locator.XPath, " //span[@class='levelPills'][contains(.,'{0}')]");

        private readonly ElementLocator
        selectedMembershipTypesText = new ElementLocator(Locator.XPath, "//div[@data-react-beautiful-dnd-draggable][contains(.,'{0}')]");

        private readonly ElementLocator
        selectedMembershipTypesTextInViewCommitteePage = new ElementLocator(Locator.XPath, "//label[text()='Selected Membership Types']/../div/span[text()='{0}']");

        private readonly ElementLocator
            sortIconOfCommitteeType = new ElementLocator(Locator.CssSelector, "th.CommitteeName i.sort.icon");

        private readonly ElementLocator
          sortedIConForCommitteeType = new ElementLocator(Locator.CssSelector, "th.CommitteeName i.long.arrow.alternate.up.icon.activeSort");

        private readonly ElementLocator
           firstCommitteeTypeLink = new ElementLocator(Locator.XPath, "//table[@class='customTable CommitteeTable']/tbody//tr//a");

        private readonly ElementLocator
            enableCommitteeTypeonwebCheckBox = new ElementLocator(Locator.XPath, "//label[contains(.,'{0}')]");

        private readonly ElementLocator
            checkedEnableCommitteeTypeOnWebChkBox = new ElementLocator(Locator.XPath, "//div[@setlabeltitleprop='Enable Committee Type on web'][@class='ui checked checkbox chbx']");

        private readonly ElementLocator
            enableCommitteeTypeonwebEnableDiv = new ElementLocator(Locator.XPath, "(//div[contains(@class,'title')])[1]");

        private readonly ElementLocator
            sortIconOfbalanceRequired = new ElementLocator(Locator.CssSelector, "th.CommitteeFee i.sort.icon");

        private readonly ElementLocator
          sortedIConForbalanceRequired = new ElementLocator(Locator.CssSelector, "th.CommitteeFee i.long.arrow.alternate.up.icon.activeSort");

        private readonly ElementLocator
           balanceRequiredLink = new ElementLocator(Locator.XPath, "//table[@class='customTable CommitteeTable']/tbody//tr//td[2]");

        private readonly ElementLocator
           sortIconOfPermittedMembers = new ElementLocator(Locator.CssSelector, "th.isEnabled i.sort.icon");

        private readonly ElementLocator
          sortedIConForPermittedMembers = new ElementLocator(Locator.CssSelector, "th.isEnabled i.long.arrow.alternate.up.icon.activeSort");

        private readonly ElementLocator
             permittedMembersLink = new ElementLocator(Locator.XPath, "//table[@class='customTable CommitteeTable']/tbody//tr//td[3]");

        private readonly ElementLocator
             radioButtonForLimitedMembers = new ElementLocator(Locator.XPath, "//label[text()='Limited']");

        private readonly ElementLocator
            textBoxforNumbers = new ElementLocator(Locator.XPath, "//input[@name='NoOfMembersPermitted']");

        private readonly ElementLocator
            errorMsgOperatorRequired = new ElementLocator(Locator.XPath, "//span[text()='Please select Operator.']");

        private readonly ElementLocator
            errorMsgMembersPermittedRequired = new ElementLocator(Locator.XPath, "//span[contains(text(),'Number of Members Permitted is required.')]");

        private readonly ElementLocator
            errorMsgClassificationTypeLeftPanel = new ElementLocator(Locator.XPath, "(//span[@class='errorMessage mt0'])[1]");

        private readonly ElementLocator
            errorMsgClassificationTypeRightPanel = new ElementLocator(Locator.XPath, "(//span[@class='errorMessage mt0'])[2]");

        private readonly ElementLocator
          enableCommitteeTypeonwebEnablViewInfoDiv = new ElementLocator(Locator.XPath, "(//div[contains(@class,'title')])[{0}]");

        private readonly ElementLocator
          committeeTypeonwebEnablViewInfoDivLables = new ElementLocator(Locator.XPath, "(//label[contains(.,'{0}')])[{1}]");

        private readonly ElementLocator
           balanceRuleInViewPage = new ElementLocator(Locator.XPath, "//label[text()='Set Balance Rule']");

        private readonly ElementLocator
           operatorConditionInViewPage = new ElementLocator(Locator.XPath, "//div[@class='operator ']/div/div/div");

        private readonly ElementLocator
           manageCommitteeHierarchyLabels = new ElementLocator(Locator.XPath, "//label[@class='titleLabel'][text()='{0}']");

        private readonly ElementLocator
            balanceRequiredYes = new ElementLocator(Locator.XPath, "//div[@class='titleInfo'][text()='{0}']");

        private readonly ElementLocator
            operatorbalanceRequired = new ElementLocator(Locator.XPath, "//div[@class='text'][text()='{0}']");

        private readonly ElementLocator
            memberClassificationTypeFirstSecond = new ElementLocator(Locator.XPath, "//label[contains(text(),'{0}')]");

        private readonly ElementLocator
            unlimitedRadioButton = new ElementLocator(Locator.XPath, "//input[@name='IsUnlimitedMembers' and @value='Unlimited']");

        private readonly ElementLocator
            unlimtedRadioBtnLabelText = new ElementLocator(Locator.XPath, "//label[text()='Unlimited']");

        private readonly ElementLocator
            limitedRadioButton = new ElementLocator(Locator.XPath, "//input[@name='IsUnlimitedMembers' and @value='Limited']");

        private readonly ElementLocator
            limitedandUnlimitedRadioLabel = new ElementLocator(Locator.XPath, "//label[text()='{0}']");

        private readonly ElementLocator
            tableColumns = new ElementLocator(Locator.XPath, "//table[@class='customTable CommitteeTable']//th[text()='{0}']");

        private readonly ElementLocator
            operatorsList = new ElementLocator(Locator.XPath, "//div[@class='visible menu transition']/div");

        private readonly ElementLocator
          memberClassificationsCountFirstSection = new ElementLocator(Locator.XPath, "(//div[@class='memClassType ']/ul)[1]/li");

        private readonly ElementLocator
        memberClassificationsCountSecondSection = new ElementLocator(Locator.XPath, "//div[@id='classificationError']/ul/li");

        private readonly ElementLocator
          committeManagementSectionOptions = new ElementLocator(Locator.CssSelector, "div.menuWrapper ul.subMenu li");

        private readonly ElementLocator
            errorMsgLimitedPermittedMembers = new ElementLocator(Locator.XPath, "//span[contains(text(),'should be numeric.')]");

        private readonly ElementLocator
            newlyAddedCommitteeTypeValidate = new ElementLocator(Locator.XPath, "//a[text()='{0}']");

        private readonly ElementLocator
            setBalanceRuleDivision = new ElementLocator(Locator.XPath, "//div[@id='committeeBalanceRule']");

        private readonly ElementLocator
            getCommitteeTypeBR = new ElementLocator(Locator.XPath, "//tr//td//a[text()='{0}']/../following-sibling::td[1]");

        private readonly ElementLocator
            committeeTypeLink = new ElementLocator(Locator.XPath, "//table[@class='customTable CommitteeTable']//..//tr[1]//td[1]");

        private readonly ElementLocator
            getCommitteeTypeLimitedMembers = new ElementLocator(Locator.XPath, "//tr//td//a[text()='{0}']/../following-sibling::td[2]");

        private readonly ElementLocator
            selectedMembershipTypesListItems = new ElementLocator(Locator.XPath, "//h3[text()='Selected Membership Types']//..//div[@data-react-beautiful-dnd-draggable]");

        private readonly ElementLocator
            selectedMembershipTypesEmptyList = new ElementLocator(Locator.XPath, "//h3[text()='Selected Membership Types']//..//div[@data-react-beautiful-dnd-droppable='0']");

        private readonly ElementLocator availableMembershipTypesListItems = new ElementLocator(Locator.XPath, "//h3[text()='Available Membership Types']//..//div[@data-react-beautiful-dnd-draggable]");
        private readonly ElementLocator
                    levelsAddedListItems = new ElementLocator(Locator.XPath, "//div[@id='levelDiv']//..//span[text()]");

        private readonly ElementLocator
            noItemsInSelectedMembershipTypes = new ElementLocator(Locator.XPath, "//h3[text()='Selected Membership Types']//..//div");

        private readonly ElementLocator
            enableCommitteeTypeChkBox = new ElementLocator(Locator.XPath, "//input[@name='Enable Committee Type on web'][@type='checkbox']");

        private readonly ElementLocator
            enableCommitteeChkboxStatus = new ElementLocator(Locator.XPath, "//div[@setlabeltitleprop='Enable Committee Type on web']");

        private string nmmanageCommitteType = "Manage Committee Types";
        private string nmlistofCommitteeType = "Committee Type from List of Values";
        private string nmtextBoxforNumbers = "TextBox to Enter Members Permitted";
        private string nmradioButtonForLimitedMembers = "Max No Of Members Permitted Radio Button";
        private string nmselectDropdown = " Operator selection Dropdown";
        private string nmHierarchyUniqueLevelerrormessage = "Hierarchy Unique Level";
        private string nmmembershipTypeInBR = "Select Membership Type";
        private string nmOtherMembershipTypeInBR = "Select Other Membership Type";
        private string nmThirdMembershipTypeInBR = "Select Third Membership Type";
        private string nmbalanceRequiredForYesButton = "Radio button :Yes : For Balance Required";
        private string nmaddcommiteeTypesuccessfullmessage = "Committee Type added successfully.";
        private string nmaddcommiteeTypeRequiredErrormessage = "Committee Type Name required Error Message";
        private string nmlevelNameExistsErrormessage = "Level name already exists Error message";
        private string nmatleastOneLevelToAddErrorMessage = "Add at least one level for the Committee Type Error Message";
        private string nmaddcommiteeTypeFormatErrormessage = "Committee Type Name Text Format Error Message";
        private string nmupdatecommiteeTypesuccessfullmessage = "Committee Type updated successfully.";
        private string nmaddCommitteeTypeTitleButton = "Add Committee Type button";
        private string nmcommitteeTypetext = "Committee Type Name";
        private string nmcmanageCommitteeHierarchytext = "Committee Hierarchy Add level";
        private string nmaddCommitteeTypeSaveButton = "Add New Committee Type Save button";
        private string nmCommitteeUpdateButton = "Update Button";
        private string nmaddlevelbutton = "Add Committee Hierarchy level button";
        private string nmcommitteTypeheader = "Committee Type header";
        private string nmmanageCommitteeHierarchyheader = "Committee Hierarchy header";
        private string nmmembershipTypeEligibleTojoinHeader = "Membership Type Eligible To Join header";
        private string nmbalanceRequiredHeader = "Balance Required header";
        private string nmenableCommitteeTypeonwebHeader = "Enable Committee Type On Web header";
        private string nmavailableMembershipTypesHeader = "Available Membership Types header";
        private string nmselectedMembershipTypesHeader = "Selected Membership Types Header";
        private string nmgetlatestRecord = "Latest Record:  {0} ,";
        private string nmEditcommiteeMembersButton = "Edit Commitee Type Name : {0}";
        private string nmEditButton = "Edit Button";
        private string nmlevelOneManageEligibleFieldLink = "Level One Manage Eligible Field Link";
        private string nmlevelTwoManageEligibleFieldLink = "Level Two Manage Eligible Field Link";
        private string nmTextParentClient = "1.";
        private string nmParentChildHierarchyLevelCloseButton = "Hierarchy Level Close Button";
        private string nmParentHierarchyLevelVisible = "Parent Hierarchy Level";
        private string nmAvailableMembershipTypes = "Available Membership Types";
        private string nmSelectedMembershipTypesText = "Selected Membership Types {0}";
        private string nmSelectedMembershipTypesListItems = "Selected Membership Types List Items";
        private string nmsortIconCommitteeType = "Committee Type Sort ICon";
        private string nmsortedIconOfCommitteeType = "Committee Type Sorted ICon";
        private string nmCommitteeTypelink = "Committee Type Name";
        private string nmenableCommitteeTypeonwebCheckBox = "Enable Commiteee Type on Web CheckBox";
        private string nmenableCommitteeTypeonwebEnableDiv = "Enable Commiteee Type on Web Div";
        private string nmsortIconBalanceRequired = "Balance Required Sort ICon";
        private string nmsortedIconOfBalanceRequired = "Balance Required Sorted ICon";
        private string nmBalanceRequiredlink = "Balance Required Name";
        private string nmsortIconPermittedMembers = "Permitted Members Sort ICon";
        private string nmsortedIconOfPermittedMembers = "Permitted Members Sorted ICon";
        private string nmPermittedMemberslink = "Permitted Members Name";
        private string nmbalanceRequiredYes = "Balance Required Yes Button";
        private string nmbalanceRequiredOperator = "Balance Required Operator";
        private string nmmemberClassificationTypeFirstSecond = "Balance Required Member Classification Type {0}";
        private string nmUnlimitedRadioButton = "Unlimited Radio Button";
        private string nmLimitedRadioButton = "Limited Radio Button";
        private string nmUnlimitedRadioLabel = "Unlimited Radio Button Label";
        private string nmLimitedRadioLabel = "Limited Radio Button Label";
        private string nmTableColumns = "Table Column: {0}";
        private string nmeligibleFieldsCallOutBox = "Eligible Fields Call Out Box";
        private string nmclassificationChkboxLabel = "Classification checkbox";
        private string nmscopeChkboxLabel = "Scope checkbox";
        private string nmoverviewChkboxLabel = "Overview checkbox";
        private string nmbylawsChkboxLabel = "Bylaws checkbox";
        private string nmmaxMembersPermitted = "Max. Number of Members Permitted - Limited";
        private string nmMembersPermittedRequiredErrorMsg = "Number of Members Permitted Required Error Message";
        private string nmerrorMsgClassificationTypeLeftPanel = "Please select at least one Classification Type - Left Panel";
        private string nmerrorMsgClassificationTypeRightPanel = "Please select at least one Classification Type - Right Panel";
        private string nmerrorMsgOperatorRequired = "Please Select Operator Error Message";
        private string nmlevelsAvailable = "Levels under Manage Committee Hierarchy";
        private string nmdimmerloading = "dimmer loading";
        private string nmsetBalanceRuleDivision = "Set Balance Rule division";

        public ManageCommitteeTypesPage(DriverContext driverContext)
            : base(driverContext)
        {
        }

        public void IsEligibleFieldsAllCheckBoxesClickable()
        {
            Verify.IsElementVisibleWithSoftAssertion(this.DriverContext, this.classificationCheckBox, this.nmclassificationChkboxLabel);
            Verify.IsElementClickableWithSoftAssertion(this.DriverContext, this.classificationCheckBox, this.nmclassificationChkboxLabel);

            Verify.IsElementVisibleWithSoftAssertion(this.DriverContext, this.scopeCheckBox, this.nmscopeChkboxLabel);
            Verify.IsElementClickableWithSoftAssertion(this.DriverContext, this.scopeCheckBox, this.nmscopeChkboxLabel);

            Verify.IsElementVisibleWithSoftAssertion(this.DriverContext, this.overviewCheckBox, this.nmoverviewChkboxLabel);
            Verify.IsElementClickableWithSoftAssertion(this.DriverContext, this.overviewCheckBox, this.nmoverviewChkboxLabel);

            Verify.IsElementVisibleWithSoftAssertion(this.DriverContext, this.bylawsCheckBox, this.nmbylawsChkboxLabel);
            Verify.IsElementClickableWithSoftAssertion(this.DriverContext, this.bylawsCheckBox, this.nmbylawsChkboxLabel);

            Actions action = new Actions(this.Driver);
            action.SendKeys(Keys.Escape).Build().Perform();
        }

        public void IsDefaultMaxNoOfMembersPermittedAsUnlimited(string defaultMaxMembersPermitted)
        {
            var by = this.maxMembersPermittedDefaultValue.ToBy();
            IWebElement element = this.Driver.FindElement(by);
            var defaultValue = element.GetAttribute("value");
            Verify.That(this.DriverContext, () => Assert.AreEqual(defaultMaxMembersPermitted, defaultValue, "Verifying whether default value selected for Max Number of Members Permitted is Unlimited or not"), "To Verify whether default value selected for Max Number of Members Permitted is : Unlimited", "default value selected for Max Number of Members Permitted is : Unlimited", "default value selected for Max Number of Members Permitted is :" + defaultValue);
        }

        public void IsMaxNoOfMembersPermittedAsLimitedEnterLimitedNumber(string defaultMaxMembersPermitted, string limitedMembersRadioBtnLabel, string limitedPersons)
        {
            var by = this.maxMembersPermittedDefaultValue.ToBy();
            IWebElement element = this.Driver.FindElement(by);
            var defaultValue = element.GetAttribute("value");

            if (defaultValue != limitedMembersRadioBtnLabel)
            {
                Verify.IsElementClickableWithSoftAssertion(this.DriverContext, this.radioButtonForLimitedMembers, this.nmradioButtonForLimitedMembers);
                Verify.EnterTextWithSoftAssertion(this.DriverContext, this.textBoxforNumbers, limitedPersons, this.nmtextBoxforNumbers);
            }
            else
            {
                Verify.EnterTextWithSoftAssertion(this.DriverContext, this.textBoxforNumbers, limitedPersons, this.nmtextBoxforNumbers);
            }
        }

        public void BalanceRequiredSelectAsYes(string defaultBalanceRequired, string yesBalanceRequired)
        {
            this.Driver.PageScrollDownToBottom();
            var by = this.balanceRequiredForNoButton.ToBy();
            IWebElement element = this.Driver.FindElement(by);
            var defaultValue = element.GetAttribute("value");

            if (defaultValue != yesBalanceRequired)
            {
                this.Driver.PageScrollDownToBottom();
                Verify.IsElementVisibleWithSoftAssertion(this.DriverContext, this.balanceRequiredForYesButton, this.nmbalanceRequiredForYesButton);
                Verify.IsElementClickableWithSoftAssertion(this.DriverContext, this.balanceRequiredForYesButton, this.nmbalanceRequiredForYesButton);
            }
        }

        public void IsUserAbleToClickEnableCommitteeTypeOnWebLabels(string lablename, int index)
        {
            this.Driver.ScrollToWebElement(this.addCommitteeTypesavebutton);
            this.Driver.WaitUntilElementIsFound(this.committeeTypeonwebEnablViewInfoDivLables.Format(lablename, index), BaseConfiguration.LongTimeout);
            this.Driver.IsElementVisible(this.committeeTypeonwebEnablViewInfoDivLables.Format(lablename, index), this.nmenableCommitteeTypeonwebEnableDiv + " Label Name :" + lablename);
            this.Driver.IsElementClickable(this.committeeTypeonwebEnablViewInfoDivLables.Format(lablename, index), this.nmenableCommitteeTypeonwebEnableDiv + " Label Name :" + lablename);
        }

        public void CheckCommitteeTypeonwebEnablViewInfoDivLables(string lablename, int index)
        {
            this.Driver.WaitUntilElementIsFound(this.committeeTypeonwebEnablViewInfoDivLables.Format(lablename, index), BaseConfiguration.LongTimeout);
            this.Driver.IsElementVisible(this.committeeTypeonwebEnablViewInfoDivLables.Format(lablename, index), this.nmenableCommitteeTypeonwebEnableDiv + " Label Name :" + lablename);
        }

        public void CheckManageCommitteeHierarchyLabels(string lablename)
        {
            this.Driver.WaitUntilElementIsFound(this.manageCommitteeHierarchyLabels.Format(lablename), BaseConfiguration.LongTimeout);
            this.Driver.IsElementVisible(this.manageCommitteeHierarchyLabels.Format(lablename), this.nmenableCommitteeTypeonwebEnableDiv + " Label Name :" + lablename);
        }

        public void IsbalanceRequiredYesDisplayed(string searchText)
        {
            this.Driver.WaitUntilElementIsFound(this.balanceRequiredYes.Format(searchText), BaseConfiguration.LongTimeout);
            this.Driver.IsElementVisible(this.balanceRequiredYes.Format(searchText), string.Format(this.nmbalanceRequiredYes));
        }

        public void IsnmbalanceRequiredOperatorDisplayed(string searchText)
        {
            this.Driver.WaitUntilElementIsFound(this.operatorbalanceRequired.Format(searchText), BaseConfiguration.LongTimeout);
            this.Driver.IsElementVisible(this.operatorbalanceRequired.Format(searchText), this.nmbalanceRequiredOperator);
        }

        public void IsmemberClassificationTypeFirstSecond(string searchText)
        {
            this.Driver.WaitUntilElementIsFound(this.memberClassificationTypeFirstSecond.Format(searchText), BaseConfiguration.LongTimeout);
            this.Driver.IsElementVisible(this.memberClassificationTypeFirstSecond.Format(searchText), string.Format(this.nmmemberClassificationTypeFirstSecond, searchText));
        }

        public void IsUnlimitedLabelDisplayed(string header)
        {
            this.Driver.WaitUntilElementIsFound(this.limitedandUnlimitedRadioLabel.Format(header), BaseConfiguration.LongTimeout);
            this.Driver.IsElementVisible(this.limitedandUnlimitedRadioLabel.Format(header), this.nmUnlimitedRadioLabel);
        }

        public void IsLimitedLabelDisplayed(string header)
        {
            this.Driver.WaitUntilElementIsFound(this.limitedandUnlimitedRadioLabel.Format(header), BaseConfiguration.LongTimeout);
            this.Driver.IsElementVisible(this.limitedandUnlimitedRadioLabel.Format(header), this.nmLimitedRadioLabel);
        }

        public void IsUnlimitedAttributeDisplayed(string expected)
        {
            this.Driver.IsElementGivenAttributePresent(this.unlimitedRadioButton, this.nmUnlimitedRadioButton, "type", expected, "ReadOnly");
        }

        public void IsLimitedAttributeDisplayed(string expected)
        {
            this.Driver.IsElementGivenAttributePresent(this.limitedRadioButton, this.nmLimitedRadioButton, "type", expected, "ReadOnly");
        }

        public void AreListOfPermittedMembersElementsDisplayInAlphabeticalOrderOrNot()
        {
            this.Driver.AreElementsSortedInAlphabeticalOrder(this.permittedMembersLink, this.nmPermittedMemberslink);
        }

        public void IsTableColumnDisplayed(string column)
        {
            this.Driver.WaitUntilElementIsFound(this.tableColumns.Format(column), BaseConfiguration.LongTimeout);
            this.Driver.IsElementVisible(this.tableColumns.Format(column), string.Format(this.nmTableColumns, column));
        }

        public void IsPermittedMembersIConSortedSuccessfully()
        {
            this.Driver.WaitUntilElementIsFound(this.sortedIConForPermittedMembers, BaseConfiguration.LongTimeout);
            this.Driver.IsElementVisible(this.sortedIConForPermittedMembers, this.nmsortedIconOfPermittedMembers);
        }

        public void IsPermittedMembersUserAbleToClickOnSortIcon()
        {
            this.Driver.WaitUntilElementIsFound(this.sortIconOfPermittedMembers, BaseConfiguration.LongTimeout);
            this.Driver.IsElementClickable(this.sortIconOfPermittedMembers, this.nmsortIconPermittedMembers);
        }

        public void IsCommitteeTypeonwebEnablViewInfoDivClick(int index)
        {
            Verify.WaitUntilElementIsFoundWithSoftAssertion(this.DriverContext, this.enableCommitteeTypeonwebEnablViewInfoDiv.Format(index), this.nmenableCommitteeTypeonwebEnableDiv, BaseConfiguration.LongTimeout);
            Verify.IsElementClickableWithSoftAssertion(this.DriverContext, this.enableCommitteeTypeonwebEnablViewInfoDiv.Format(index), this.nmenableCommitteeTypeonwebEnableDiv);
        }

        public void AreListOfElementsDisplayInAlphabeticalOrderOrNot()
        {
            this.Driver.AreElementsSortedInAlphabeticalOrder(this.committeeTypeLink, this.nmCommitteeTypelink);
        }

        public void IsCommitteeTypeIConSortedSuccessfully()
        {
            this.Driver.WaitUntilElementIsFound(this.sortedIConForCommitteeType, BaseConfiguration.LongTimeout);
            this.Driver.IsElementVisible(this.sortedIConForCommitteeType, this.nmsortedIconOfCommitteeType);
        }

        public void IsCommitteeTypeUserAbleToClickOnSortIcon()
        {
            this.Driver.WaitUntilElementIsFound(this.sortIconOfCommitteeType, BaseConfiguration.LongTimeout);
            this.Driver.IsElementClickable(this.sortIconOfCommitteeType, this.nmsortIconCommitteeType);
        }

        public void AreBalanceRequiredListOfElementsDisplayInAlphabeticalOrderOrNot()
        {
            this.Driver.AreElementsSortedInAlphabeticalOrder(this.balanceRequiredLink, this.nmBalanceRequiredlink);
        }

        public void IsBalanceRequiredIConSortedSuccessfully()
        {
            this.Driver.WaitUntilElementIsFound(this.sortedIConForbalanceRequired, BaseConfiguration.LongTimeout);
            this.Driver.IsElementVisible(this.sortedIConForbalanceRequired, this.nmsortedIconOfBalanceRequired);
        }

        public void IsBalanceRequiredUserAbleToClickOnSortIcon()
        {
            this.Driver.WaitUntilElementIsFound(this.sortIconOfbalanceRequired, BaseConfiguration.LongTimeout);
            this.Driver.IsElementClickable(this.sortIconOfbalanceRequired, this.nmsortIconBalanceRequired);
        }

        public void DragEligibleToFrom()
        {
            this.Driver.DragDropFromLocatorToLocator(this.selectedelegibletoJOinFrom, this.selectedelegibletoJOinTo);
            DriverContext.ExtentStepTest.Log(LogStatus.Pass, "To verify Drag and Drop of Membership Types from Available List to Selected List is successful ", " Drag and Drop of Membership Types from Available List to Selected List is successful");
        }

        public void IsSequencedisplayedForMembershipType(List<string> listToCompare)
        {
            IList<IWebElement> lstelements = this.Driver.GetElements(this.selectedMembershipTypesList);
            for (int i = 0; i < lstelements.Count; i++)
            {
                if (lstelements[i].Text == listToCompare[i].Trim())
                {
                    DriverContext.ExtentStepTest.Log(LogStatus.Pass, "To verify Member Types are displayed in sequence :", "An exception occurred while Verifying MembershipType Sequence");
                }

                i++;
            }
        }

        public void IsMembershipTypeEligibleItemDisplayed(string expected)
        {
            this.Driver.WaitUntilElementIsFound(this.selectedMembershipTypesText.Format(expected), BaseConfiguration.LongTimeout);
            this.Driver.IsElementVisible(this.selectedMembershipTypesText.Format(expected), string.Format(this.nmSelectedMembershipTypesText, expected));
            System.Threading.Thread.Sleep(5000);
        }

        public void IsAvailableMembershipTypeDisplayedwithAlloption(string expected)
        {
            this.Driver.WaitUntilElementIsFound(this.selectedMembershipTypesText.Format(expected), BaseConfiguration.LongTimeout);
            this.Driver.IsElementVisible(this.selectedMembershipTypesText.Format(expected), string.Format(this.nmSelectedMembershipTypesText, expected));
        }

        public void IsMembershipTypeEligibleItemDisplayedInViewCommiteePage(string expected)
        {
            this.Driver.WaitUntilElementIsFound(this.selectedMembershipTypesTextInViewCommitteePage.Format(expected), BaseConfiguration.LongTimeout);
            this.Driver.IsElementVisible(this.selectedMembershipTypesTextInViewCommitteePage.Format(expected), string.Format(this.nmSelectedMembershipTypesText, expected + " In View Committtee Page"));
        }

        public string IsAvailableMembershipTypesSelection()
        {
            var webElementLocator = this.Driver.GetElement(this.selectedelegibletoJOinEdit);
            string returnDragText = webElementLocator.Text.Trim();
            this.Driver.IsDragDropFromLocatorToLocator(this.selectedelegibletoJOinEdit, this.selectedelegibletoJOinTo, this.nmAvailableMembershipTypes);
            return returnDragText;
        }

        public List<string> IsUserableToverifyDraggedItems(string selectedMembershipTypesHeader)
        {
            List<string> lstElements = new List<string>();
            string dragElementText = this.IsAvailableMembershipTypesSelection();
            this.IsMembershipTypeEligibleItemDisplayed(dragElementText);
            lstElements.Add(dragElementText);
            this.IsEnableselectedMembershipTypesHeaderDisplayed(selectedMembershipTypesHeader);
            string dragElement2Text = this.IsAvailableMembershipTypesSelection();
            this.IsMembershipTypeEligibleItemDisplayed(dragElement2Text);
            lstElements.Add(dragElement2Text);
            return lstElements;
        }

        public string IsMembershipTypeRemovedFromSelectedLane()
        {
            var webElementLocator = this.Driver.GetElement(this.selectdelegibletoJoinFromSelectedLane);
            string returnDragText = webElementLocator.Text.Trim();
            this.Driver.IsDragDropFromLocatorToLocator(this.selectdelegibletoJoinFromSelectedLane, this.selectdelegibletoJoinToAvailableLane, this.nmSelectedMembershipTypesText);
            DriverContext.ExtentStepTest.Log(LogStatus.Pass, "To verify Drag and Drop of Membership Type from Selected List to Available List is successful ", " Drag and Drop of Membership Type from Selected List to Available List is successful");
            return returnDragText;
        }

        public void IsCommitteeTypeRemovedSuccessfully()
        {
            bool value = this.Driver.IsElementPresent(this.removedcommitteeInViewPage, BaseConfiguration.MediumTimeout);
            Verify.That(this.DriverContext, () => Assert.IsFalse(value), "Verify whether Committee Type is removed", "Member Committe Type removed successfully", "Member Committe Type is not removed");
        }

        public void IsCommitteeTypeDeSelectedSuccessfullyInAddCommitteePage(string name)
        {
            bool value = this.Driver.IsElementPresent(this.removedcommitteeInAddPage.Format(name), BaseConfiguration.MediumTimeout);
            Verify.That(this.DriverContext, () => Assert.IsFalse(value), "Verify whether Committee Type is deselected", "Member Committe Type is DeSelected successfully", "Member Committe Type is not deselected");
        }

        public void IsenableCommitteeTypeonwebCheckBoxClicked(string message)
        {
            Verify.IsElementVisibleWithSoftAssertion(this.DriverContext, this.enableCommitteeTypeonwebCheckBox.Format(message), this.nmenableCommitteeTypeonwebCheckBox);
            Verify.IsElementClickableWithSoftAssertion(this.DriverContext, this.enableCommitteeTypeonwebCheckBox.Format(message), this.nmenableCommitteeTypeonwebCheckBox);
        }

        public void IsenableCommitteeTypeonwebCheckBoxClickIfUnchecked(string enableCommitteeLabelText)
        {
            this.ScrollToSaveElement();
            IWebElement webElement = this.Driver.GetElement(this.enableCommitteeChkboxStatus, BaseConfiguration.LongTimeout);

            if (!webElement.GetAttribute("class").Contains("checked"))
            {
                this.Driver.PageScrollDownToBottom();
                Verify.IsElementVisibleWithSoftAssertion(this.DriverContext, this.enableCommitteeTypeonwebCheckBox.Format(enableCommitteeLabelText), this.nmenableCommitteeTypeonwebCheckBox);
                Verify.IsElementClickableWithSoftAssertion(this.DriverContext, this.enableCommitteeTypeonwebCheckBox.Format(enableCommitteeLabelText), this.nmenableCommitteeTypeonwebCheckBox);
            }
        }

        public void IsenableCommitteeTypeonwebCheckBoxDiv()
        {
            this.Driver.IsElementPresentOrNot(this.enableCommitteeTypeonwebEnableDiv, this.nmenableCommitteeTypeonwebEnableDiv, string.Empty);
        }

        public void IsManageCommitteeHierarchyHeaderDisplayed(string header)
        {
            this.Driver.IsElementVisible(this.manageCommitteeHierarchyHeader.Format(header), this.nmmanageCommitteeHierarchyheader);
            this.Driver.WaitUntilElementIsFound(this.manageCommitteeHierarchyHeader.Format(header), BaseConfiguration.LongTimeout);
            this.Driver.IsElementVisible(this.manageCommitteeHierarchyHeader.Format(header), this.nmmanageCommitteeHierarchyheader);
        }

        public void IsMembershipTypeEligibleTojoinHeaderDisplayed(string header)
        {
            this.Driver.WaitUntilElementIsNoLongerFound(this.loadbutton, BaseConfiguration.LongTimeout, this.nmmembershipTypeEligibleTojoinHeader);
            this.Driver.WaitUntilElementIsFound(this.membershipTypeEligibleTojoinHeader.Format(header), BaseConfiguration.LongTimeout);
            this.Driver.IsElementVisible(this.membershipTypeEligibleTojoinHeader.Format(header), this.nmmembershipTypeEligibleTojoinHeader);
        }

        public void IsBalanceRequiredHeaderDisplayed(string header)
        {
            Verify.WaitUntilElementIsNoLongerFoundWithSoftAssertion(this.DriverContext, this.loadbutton, this.nmmembershipTypeEligibleTojoinHeader, BaseConfiguration.LongTimeout);
            Verify.IsElementVisibleWithSoftAssertion(this.DriverContext, this.balanceRequiredHeader.Format(header), this.nmbalanceRequiredHeader);
        }

        public void IsEligibleFieldsCallOutBoxDisplayed()
        {
            Verify.IsElementVisibleWithSoftAssertion(this.DriverContext, this.eligibleFieldsCallOutBox, this.nmeligibleFieldsCallOutBox);
        }

        public void IsEnableCommitteeTypeonwebHeaderDisplayed(string header)
        {
            this.Driver.WaitUntilElementIsFound(this.enableCommitteeTypeonwebHeader.Format(header), BaseConfiguration.LongTimeout);
            this.Driver.IsElementVisible(this.enableCommitteeTypeonwebHeader.Format(header), this.nmenableCommitteeTypeonwebHeader);
        }

        public void ScrollToCommitteeNameElement()
        {
            this.Driver.PageScrollUpToTop();
        }

        public void ScrollToSaveElement()
        {
            this.Driver.PageScrollDownToBottom();
        }

        public void IsEnableavailableMembershipTypesHeaderDisplayed(string header)
        {
            this.Driver.WaitUntilElementIsFound(this.availableMembershipTypesHeader.Format(header), BaseConfiguration.LongTimeout);
            this.Driver.IsElementVisible(this.availableMembershipTypesHeader.Format(header), this.nmavailableMembershipTypesHeader);
        }

        public void IsEnableselectedMembershipTypesHeaderDisplayed(string header)
        {
            this.Driver.WaitUntilElementIsFound(this.selectedMembershipTypesHeader.Format(header), BaseConfiguration.LongTimeout);
            this.Driver.IsElementVisible(this.selectedMembershipTypesHeader.Format(header), this.nmselectedMembershipTypesHeader);
        }

        public void IsManageCommitteTypeClickable(string manageCommitteType)
        {
            this.Driver.AreElementsVisible(this.committeManagementSectionOptions.Format(manageCommitteType), this.nmmanageCommitteType);
            this.Driver.IsElementClickableFromListofElementWithText(this.committeManagementSectionOptions.Format(manageCommitteType), this.nmmanageCommitteType);
        }

        public void IsManageCommitteTypeHeaderDisplayed(string header)
        {
            var text = this.Driver.GetText(this.committeTypeheader);
            Verify.IsElementVisibleWithSoftAssertion(this.DriverContext, this.committeTypeheader.Format(header), this.nmcommitteTypeheader);
            Verify.That(this.DriverContext, () => Assert.AreEqual(header, text, "verifying Member Committe Type Title header displayed or not"), "To verify Member Committe Type Title header displayed or not", "Member committee type header displayed", "Member committee type header is not Displayed");
        }

        public void IsHeaderDisplayedOnCommitteeDetailsPage(string header)
        {
            var text = this.Driver.GetText(this.committeTypeheader);
            Verify.IsElementVisibleWithSoftAssertion(this.DriverContext, this.committeTypeheader.Format(header), this.nmcommitteTypeheader);
            Verify.That(this.DriverContext, () => Assert.AreEqual(header, text, "verifying Manage Committe Type Title header displayed or not"), "To verify Manage Committe Type Title header displayed or not", "Manage Committee Type header displayed", "Manage Committee Type header is not Displayed");
        }

        public void IsAddCommitteeTypeButtonClickable()
        {
            this.Driver.WaitUntilElementIsFound(this.addCommitteeTypeButton, BaseConfiguration.LongTimeout);
            var webElement = this.Driver.GetElement(this.addCommitteeTypeButton);
            this.Driver.IsElementVisible(this.addCommitteeTypeButton, this.nmaddCommitteeTypeTitleButton);
            this.Driver.JavaScriptClick(webElement, this.nmaddCommitteeTypeTitleButton);
        }

        public void IsUserAbleToSeeCommitteTypeAddedintoListOfValues(string committeType)
        {
            this.Driver.IsExpectedTextMatchWithActualTextFromListOfElements(this.listofcommitteeType, committeType, this.nmlistofCommitteeType);
        }

        public void IsNewCommitteeTypeaddedsuccessfullyDisplayed(string message)
        {
            this.Driver.IsElementVisible(this.successfullMsg, this.nmaddcommiteeTypesuccessfullmessage);
            this.Driver.IsExpectedTextMatchWithActualText(this.successfullMsg, message, this.nmaddcommiteeTypesuccessfullmessage);
        }

        public void IsCommitteeTypeNameRequiredErrorMsgSuccessfullyDisplayed(string errorMessage)
        {
            Verify.IsElementVisibleWithSoftAssertion(this.DriverContext, this.committeeTypeError, this.nmaddcommiteeTypeRequiredErrormessage);
            Verify.IsExpectedTextMatchWithActualTextWithSoftAssertion(this.DriverContext, this.committeeTypeError, errorMessage, this.nmaddcommiteeTypeRequiredErrormessage);
        }

        public void IsLevelNameExistsErrorMsgSuccessfullyDisplayed(string errorMessage)
        {
            Verify.IsElementVisibleWithSoftAssertion(this.DriverContext, this.levelErrorMsg, this.nmlevelNameExistsErrormessage);
            Verify.IsExpectedTextMatchWithActualTextWithSoftAssertion(this.DriverContext, this.levelErrorMsg, errorMessage, this.nmlevelNameExistsErrormessage);
        }

        public void IsMaxNoOfMembersPermittedLimitedErrorMsgSuccessfullyDisplayed(string errorMessage)
        {
            this.Driver.ScrollToWebElement(this.committeeTypetext);
            Verify.IsElementVisibleWithSoftAssertion(this.DriverContext, this.errorMsgLimitedPermittedMembers, this.nmmaxMembersPermitted);
            Verify.IsExpectedTextMatchWithActualTextWithSoftAssertion(this.DriverContext, this.errorMsgLimitedPermittedMembers, errorMessage, this.nmmaxMembersPermitted);
        }

        public void IsNoOfMembersPermittedRequiredErrorMsgSuccessfullyDisplayed(string errorMessage)
        {
            Verify.IsElementVisibleWithSoftAssertion(this.DriverContext, this.errorMsgMembersPermittedRequired, this.nmMembersPermittedRequiredErrorMsg);
            Verify.IsExpectedTextMatchWithActualTextWithSoftAssertion(this.DriverContext, this.errorMsgMembersPermittedRequired, errorMessage, this.nmMembersPermittedRequiredErrorMsg);
        }

        public void IsOperatorRequiredErrorMsgSuccessfullyDisplayed(string errorMessage)
        {
            Verify.IsElementVisibleWithSoftAssertion(this.DriverContext, this.errorMsgOperatorRequired, this.nmerrorMsgOperatorRequired);
            Verify.IsExpectedTextMatchWithActualTextWithSoftAssertion(this.DriverContext, this.errorMsgOperatorRequired, errorMessage, this.nmerrorMsgOperatorRequired);
        }

        public void IsLeftPanelClassificationTypeRequiredErrorMsgSuccessfullyDisplayed(string errorMessage)
        {
            Verify.IsElementVisibleWithSoftAssertion(this.DriverContext, this.errorMsgClassificationTypeLeftPanel, this.nmerrorMsgClassificationTypeLeftPanel);
            Verify.IsExpectedTextMatchWithActualTextWithSoftAssertion(this.DriverContext, this.errorMsgClassificationTypeLeftPanel, errorMessage, this.nmerrorMsgClassificationTypeLeftPanel);
        }

        public void IsRightPanelClassificationTypeRequiredErrorMsgSuccessfullyDisplayed(string errorMessage)
        {
            Verify.IsElementVisibleWithSoftAssertion(this.DriverContext, this.errorMsgClassificationTypeRightPanel, this.nmerrorMsgClassificationTypeRightPanel);
            Verify.IsExpectedTextMatchWithActualTextWithSoftAssertion(this.DriverContext, this.errorMsgClassificationTypeRightPanel, errorMessage, this.nmerrorMsgClassificationTypeRightPanel);
        }

        public void IsAtleastOneLevelToAddErrorMsgSuccessfullyDisplayed(string errorMessage)
        {
            Verify.IsElementVisibleWithSoftAssertion(this.DriverContext, this.atleastOneLevelErrorMsg, this.nmatleastOneLevelToAddErrorMessage);
            Verify.IsExpectedTextMatchWithActualTextWithSoftAssertion(this.DriverContext, this.atleastOneLevelErrorMsg, errorMessage, this.nmatleastOneLevelToAddErrorMessage);
        }

        public void IsCommitteeTypeNameFormatErrorMsgSuccessfullyDisplayed(string errorMessage)
        {
            Verify.IsElementVisibleWithSoftAssertion(this.DriverContext, this.committeeTypeError, this.nmaddcommiteeTypeFormatErrormessage);
            Verify.IsExpectedTextMatchWithActualTextWithSoftAssertion(this.DriverContext, this.committeeTypeError, errorMessage, this.nmaddcommiteeTypeFormatErrormessage);
        }

        public void IsUserAbleToViewNewlyCreatedCommitteeType(string committeeType, string balanceRequiredExpected, string limitedMembersExpected)
        {
            this.Driver.WaitUntilElementIsFound(this.newlyAddedCommitteeTypeValidate.Format(committeeType), BaseConfiguration.LongTimeout);
            this.Driver.IsElementVisible(this.newlyAddedCommitteeTypeValidate.Format(committeeType), this.nmcommitteeTypetext);

            string actualBRText = this.Driver.GetText(this.getCommitteeTypeBR.Format(committeeType));
            Verify.That(this.DriverContext, () => Assert.AreEqual(balanceRequiredExpected, actualBRText, "verifying that Committed Type is displayed successfuly along with Balance Required field data"), "To Verify whether Balance Required field data is displayed as Yes", "Balance Required field data is : Yes ", "Default value selected for Balance Required is : " + actualBRText);

            string actualLimitedMembers = this.Driver.GetText(this.getCommitteeTypeLimitedMembers.Format(committeeType));
            Verify.That(this.DriverContext, () => Assert.AreEqual(limitedMembersExpected, actualLimitedMembers, "verifying that Committed Type is displayed successfuly along with Permitted Members field data"), "To Verify whether Permitted Members field data is displayed or not", "Permitted Members field data is displayed", "Permitted Members field data is displayed as : " + actualLimitedMembers);

            DriverContext.ExtentStepTest.Log(LogStatus.Pass, "To verify Committed Type is displayed successfuly along with Balance Required and Permitted Members field data", "Committed Type is displayed successfuly along with Balance Required and Permitted Members field data");
        }

        public void IsUserIsAbleToClickOnAccountNumberOfMemberType()
        {
            int countOfCommitteeTypesTypes = 0;
            try
            {
                this.Driver.WaitUntilElementIsFound(this.committeeTypeLink, BaseConfiguration.LongTimeout);
                IList<IWebElement> lstElements = this.Driver.GetElements(this.committeeTypeLink);
                countOfCommitteeTypesTypes = lstElements.Count;
                if (countOfCommitteeTypesTypes > 0)
                {
                    this.Driver.WaitUntilElementIsFound(this.firstCommitteeTypeLink, BaseConfiguration.LongTimeout);
                    var webElementCommitteeLink = this.Driver.GetElement(this.firstCommitteeTypeLink);
                    this.Driver.JavaScriptClick(webElementCommitteeLink, this.nmCommitteeTypelink);
                    this.Driver.WaitForPageLoad();
                    DriverContext.ExtentStepTest.Log(LogStatus.Pass, "To verify that User is able to click the available Committee Type link", "User is able to click the available Committee Type link");
                    Logger.Info("User is able to click the available Committee Type link");
                }
                else
                {
                    Assert.IsFalse(true);
                }
            }
            catch (Exception)
            {
                Logger.Error("User is not able to click the Committee Type link");

                DriverContext.ExtentStepTest.Log(LogStatus.Fail, "To verify that User is able to click the available Committee Type link ", "An exception occurred while the user about to click Committee Type link");
                throw;
            }
        }

        public void IsCommitteeTypeUpdatedsuccessfullyDisplayed(string message)
        {
            this.Driver.IsElementVisible(this.successfullMsg, this.nmupdatecommiteeTypesuccessfullmessage);
            this.Driver.IsExpectedTextMatchWithActualText(this.successfullMsg, message, this.nmupdatecommiteeTypesuccessfullmessage);
        }

        public void IsGetLatestRecordDisplayed(string expected)
        {
            this.Driver.WaitUntilElementIsFound(this.getLatestRecord.Format(expected), BaseConfiguration.LongTimeout);
            this.Driver.IsElementVisible(this.getLatestRecord.Format(expected), string.Format(this.nmgetlatestRecord, expected));
        }

        public void IsNewCommitteeTypeUniqueErrorMessageDisplayed(string expected)
        {
            string actual = this.Driver.GetText(this.errorMsgUnique);
            this.Driver.IsExpectedTextMatchWithActualText(this.errorMsgUnique, expected, actual);
        }

        public void IsNewCommitteeTypeNameErrorMessageDisplayed(string expected)
        {
            string actual = this.Driver.GetText(this.errorCommitteeTypeName.Format(expected));
            this.Driver.IsExpectedTextMatchWithActualText(this.errorCommitteeTypeName.Format(expected), expected, actual);
        }

        public void IsNewCommitteeTypeLevelErrorMessageDisplayed(string expected)
        {
            string actual = this.Driver.GetText(this.errorCommitteeTypeLevels.Format(expected));
            this.Driver.IsExpectedTextMatchWithActualText(this.errorCommitteeTypeLevels.Format(expected), expected, actual);
        }

        public void IsUserAbletoEnterCommitteeTypeInTextBox(string committeeTypeNameText)
        {
            Verify.IsElementVisibleWithSoftAssertion(this.DriverContext, this.committeeTypetext, this.nmcommitteeTypetext);
            Verify.EnterTextWithSoftAssertion(this.DriverContext, this.committeeTypetext, committeeTypeNameText, this.nmcommitteeTypetext);
        }

        public void IsUserAbleToClickOnCommitteeTypeNameTextBox()
        {
            Verify.IsElementVisibleWithSoftAssertion(this.DriverContext, this.committeeTypetext, this.nmcommitteeTypetext);
            var txtBox = this.Driver.GetElement(this.committeeTypetext);
            if (txtBox.Enabled)
            {
                Logger.Info(this.nmcommitteeTypetext + " is viewed successfully");
                txtBox.Click();
                DriverContext.ExtentStepTest.Log(LogStatus.Pass, "To Verify that User is able to click on the " + this.nmcommitteeTypetext, "User is able to click on the " + this.nmcommitteeTypetext);
                Logger.Info("User is able to click on the " + this.nmcommitteeTypetext);
            }
        }

        public void IsUserAbletoRemoveInputFromNoOfMembersTextbox()
        {
            Verify.IsElementVisibleWithSoftAssertion(this.DriverContext, this.textBoxforNumbers, this.nmtextBoxforNumbers);
            var txtBox = this.Driver.GetElement(this.textBoxforNumbers);
            if (txtBox.Enabled)
            {
                Logger.Info(this.nmtextBoxforNumbers + " is viewed successfully");
                txtBox.Click();
                txtBox.Clear();
                DriverContext.ExtentStepTest.Log(LogStatus.Pass, "To Verify that User is able to Remove Incorrect Input from " + this.nmcommitteeTypetext, "User is able to Remove Incorrect Input from " + this.nmcommitteeTypetext);
                Logger.Info("User is able to Remove Incorrect Input from " + this.nmcommitteeTypetext);
            }
        }

        public void IsLimitedPersonsSelectedAndEnterNumber(string num)
        {
            Verify.IsElementClickableWithSoftAssertion(this.DriverContext, this.radioButtonForLimitedMembers, this.nmradioButtonForLimitedMembers);
            Verify.EnterTextWithSoftAssertion(this.DriverContext, this.textBoxforNumbers, num, this.nmtextBoxforNumbers);
        }

        public void IsLimitedPersonsSelected()
        {
            this.Driver.IsElementClickable(this.radioButtonForLimitedMembers, this.nmradioButtonForLimitedMembers);
        }

        public void ScrollToTopWebPage()
        {
            this.Driver.PageScrollUpToTop();
        }

        public string GetListofValues(string isLimitedMembers, string noOfLimitedMembers, string isBalanceRequired, string isEnableOnWeb)
        {
            return isLimitedMembers + "," + noOfLimitedMembers + "," + isBalanceRequired + "," + isEnableOnWeb;
        }

        public string GetListofValuesUpdateCommitteeType(string isLimitedMembers, string noOfLimitedMembers, string isBalanceRequired, string isEnableOnWeb)
        {
            return isLimitedMembers + "," + noOfLimitedMembers + "," + isBalanceRequired + "," + isEnableOnWeb;
        }

        public void GetCommitteeTypeExistsInDB(string committeeTypeName, List<string> columnnames, List<string> values, string membershipTypesColumnName, List<string> membershipTypesColumnvalues, string levelsAddedColumnName, List<string> levelsAddedColumnvalues)
        {
            DataSet resultTable = SqlHelper.ExecuteSqlCommandQuery(string.Format(SqlQuery.FunctionalAddedCommitteeTypeExists, committeeTypeName));
            Dictionary<string, string> actualDict = SqlHelper.GetTableColumnValueInDictionary(resultTable.Tables[0], columnnames);
            Dictionary<string, string> expectedDict = SqlHelper.GenerateDictionaryfromLists(columnnames, values);
            Verify.CompareTwoDictionaryFromPageAndDBWithSoftAssertion(this.DriverContext, actualDict, expectedDict);
            int committeeTypeID = Convert.ToInt32(resultTable.Tables[0].Rows[0][0].ToString());
            Verify.GetSingleColumnValuesElementsMatchingwithFromDBIrrespectiveOfOrderWithSoftAssertion(this.DriverContext, string.Format(SqlQuery.FunctionalSelectedMembershipTypesInCommitteeType, committeeTypeID), membershipTypesColumnName, membershipTypesColumnvalues, "Selected Membership Types List");
            this.Driver.GetSingleColumnValuesAndCompareWithExpectedListFromDB(string.Format(SqlQuery.FunctionalLevelsAddedInCommitteeType, committeeTypeID), levelsAddedColumnName, levelsAddedColumnvalues, "Levels Added List");
        }

        public void IsUserAbletoEnterCommitteeHierarchyInTextBox(string committeeHierarchyNameText)
        {
            if (Verify.IsElementVisibleWithSoftAssertion(this.DriverContext, this.manageCommitteeHierarchytext, this.nmcmanageCommitteeHierarchytext))
            {
                Verify.EnterTextWithSoftAssertion(this.DriverContext, this.manageCommitteeHierarchytext, committeeHierarchyNameText, this.nmcmanageCommitteeHierarchytext);
            }
        }

        public void IsUserAbleToClickOnAddLevelsTextBoxUnderManageCommitteeHierarchy()
        {
            Verify.IsElementVisibleWithSoftAssertion(this.DriverContext, this.manageCommitteeHierarchytext, this.nmcmanageCommitteeHierarchytext);
            var txtBox = this.Driver.GetElement(this.manageCommitteeHierarchytext);
            if (txtBox.Enabled)
            {
                Logger.Info(this.nmcmanageCommitteeHierarchytext + " is viewed successfully");
                txtBox.Click();
                DriverContext.ExtentStepTest.Log(LogStatus.Pass, "To Verify that User is able to click on the " + this.nmcmanageCommitteeHierarchytext, "User is able to click on the " + this.nmcmanageCommitteeHierarchytext);
                Logger.Info("User is able to click on the " + this.nmcmanageCommitteeHierarchytext);
            }
        }

        public Collection<string> IsMembershipTypesAvailableInSelectedMembershipTypes()
        {
            int countOfselectedMembershipTypes = 0;
            try
            {
                Verify.WaitUntilElementIsFoundWithSoftAssertion(this.DriverContext, this.selectedMembershipTypesListItems, this.nmSelectedMembershipTypesListItems, BaseConfiguration.LongTimeout);
                IList<IWebElement> lstElements = this.Driver.GetElements(this.selectedMembershipTypesListItems);
                Collection<string> membershipTypeList = this.Driver.GetTableElementsList(this.selectedMembershipTypesListItems);
                countOfselectedMembershipTypes = lstElements.Count;

                if (countOfselectedMembershipTypes > 0)
                {
                    DriverContext.ExtentStepTest.Log(LogStatus.Pass, "To verify that Membership Types are available In Selected Membership Types", "Membership Types are available In Selected Membership Types");
                    Logger.Info("Membership Types are available In Selected Membership Types");
                }

                return membershipTypeList;
            }
            catch (Exception)
            {
                Logger.Error("Membership Types are not available In Selected Membership Types");

                DriverContext.ExtentStepTest.Log(LogStatus.Fail, "To verify that Membership Types are available In Selected Membership Types ", "An exception occurred as the Selected Membership Types list count is  " + countOfselectedMembershipTypes);
                throw;
            }
        }

        public void IsMembershipTypesAvailableInSelectedMembershipTypesInEditCommitteeType()
        {
            int countOfSelectedMembershipTypes = 0;
            try
            {
                IList<IWebElement> lstElements = this.Driver.GetElements(this.noItemsInSelectedMembershipTypes);
                countOfSelectedMembershipTypes = lstElements.Count;
                if (countOfSelectedMembershipTypes == 1)
                {
                    this.DragEligibleToFrom();
                }
            }
            catch (Exception)
            {
                Logger.Error("Membership Types are not available In  Selected Membership Types");

                DriverContext.ExtentStepTest.Log(LogStatus.Fail, "To verify that Membership Types are available In Selected Membership Types List ", "An exception occurred as the Selected Membership Types List does not hold any Membership Types");
                throw;
            }
        }

        public void IsMembershipTypesAvailableInAvailableMembershipTypes()
        {
            int countOfAvailableMembershipTypes = 0;
            try
            {
                this.Driver.WaitUntilElementIsFound(this.availableMembershipTypesListItems, BaseConfiguration.LongTimeout);
                IList<IWebElement> lstElements = this.Driver.GetElements(this.availableMembershipTypesListItems);
                countOfAvailableMembershipTypes = lstElements.Count;
                if (countOfAvailableMembershipTypes > 0)
                {
                    this.DragEligibleToFrom();
                }
                else
                {
                    this.IsMembershipTypeRemovedFromSelectedLane();
                }
            }
            catch (Exception)
            {
                Logger.Error("Membership Types are not available In  Membership Types");

                DriverContext.ExtentStepTest.Log(LogStatus.Fail, "To verify that Membership Types are available In Membership Types List ", "An exception occurred as the Membership Types List does not hold any Membership Types");
                throw;
            }
        }

        public void IsLevelInputType()
        {
            this.Driver.IsElementVisible(this.manageCommitteeHierarchytext, this.nmcmanageCommitteeHierarchytext);
            this.Driver.IsElementGivenAttributePresent(this.manageCommitteeHierarchytext, this.nmcmanageCommitteeHierarchytext, "type", "text");
        }

        public void IsAddNewCommitteeTypeSaveButtonClickable()
        {
            this.Driver.IsElementVisible(this.addCommitteeTypesavebutton, this.nmaddCommitteeTypeSaveButton);
            this.Driver.IsElementClickable(this.addCommitteeTypesavebutton, this.nmaddCommitteeTypeSaveButton);
        }

        public void IsAddNewCommitteeTypeUpdateButtonClickable()
        {
            this.Driver.IsElementVisible(this.updatebutton, this.nmCommitteeUpdateButton);
            this.Driver.IsElementClickable(this.updatebutton, this.nmCommitteeUpdateButton);
        }

        public void IserrorMsgDisplayedForIncorrectInput(string errorMessage)
        {
            this.Driver.IsExpectedTextMatchWithActualText(this.incorrectInput, errorMessage);
        }

        public void IserrorMsgDisplayedFormandatoryInput(string errorMessage)
        {
            this.Driver.IsExpectedTextMatchWithActualText(this.mandatoryInput, errorMessage);
        }

        public void IsAddLevelButtonClickable()
        {
            Verify.IsElementVisibleWithSoftAssertion(this.DriverContext, this.addLevel, this.nmaddlevelbutton);
            Verify.IsElementClickableWithSoftAssertion(this.DriverContext, this.addLevel, this.nmaddlevelbutton);
        }

        public void IsSetBalanceRuleDivisionVisible()
        {
            Verify.IsElementVisibleWithSoftAssertion(this.DriverContext, this.setBalanceRuleDivision, this.nmsetBalanceRuleDivision);
        }

        public Collection<string> AreLevelsAvailableForCommitteeType()
        {
            int countOfLevelsAdded = 0;
            try
            {
                Verify.WaitUntilElementIsFoundWithSoftAssertion(this.DriverContext, this.levelsAddedListItems, this.nmlevelsAvailable, BaseConfiguration.LongTimeout);
                IList<IWebElement> lstElements = this.Driver.GetElements(this.levelsAddedListItems);

                Collection<string> levelsList = this.Driver.GetTableElementsList(this.levelsAddedListItems);
                Collection<string> lstTableValues = new Collection<string>();
                int i = 0;
                foreach (var value in levelsList)
                {
                    string s = levelsList.ToList()[i].Split('\r')[0];
                    lstTableValues.Add(s);
                    i++;
                }

                countOfLevelsAdded = lstElements.Count;

                if (countOfLevelsAdded > 0)
                {
                    DriverContext.ExtentStepTest.Log(LogStatus.Pass, "To verify the Levels added In Committee Type", "Levels added In Committee Type verification is successful");
                    Logger.Info("Levels added In Committee Type verification is successful");
                }
                ////else
                ////{
                ////    Assert.IsFalse(true);
                ////}

                return lstTableValues;
            }
            catch (Exception)
            {
                Logger.Error("Added Levels are not available In Committee Type");

                DriverContext.ExtentStepTest.Log(LogStatus.Fail, "To verify the Levels added In Committee Type ", "An exception occurred as levels added count in Committee Type is " + countOfLevelsAdded);
                throw;
            }
        }

        public void IsEditNewCommitteeTypeClickable(string expected)
        {
            this.Driver.WaitUntilElementIsNoLongerFound(this.successfullMsg, BaseConfiguration.LongTimeout, this.nmEditcommiteeMembersButton);
            this.Driver.IsElementVisible(this.getLatestRecord.Format(expected), string.Format(this.nmEditcommiteeMembersButton, expected));
            this.Driver.IsElementClickable(this.getLatestRecord.Format(expected), string.Format(this.nmEditcommiteeMembersButton, expected));
        }

        public void IsNewCommitteeTypeLinkDisplayedClickable(string expected)
        {
            this.Driver.IsElementVisible(this.getLatestRecord.Format(expected), string.Format(this.nmEditcommiteeMembersButton, expected));
            this.Driver.IsElementClickable(this.getLatestRecord.Format(expected), string.Format(this.nmEditcommiteeMembersButton, expected));
        }

        public void IsLevelOneManageEligibleFieldsLinkDisplayedClickable()
        {
            Verify.IsElementVisibleWithSoftAssertion(this.DriverContext, this.levelOneManageEligibleFieldLink, this.nmlevelOneManageEligibleFieldLink);
            Verify.IsElementClickableWithSoftAssertion(this.DriverContext, this.levelOneManageEligibleFieldLink, this.nmlevelOneManageEligibleFieldLink);
        }

        public void IsLevelTwoManageEligibleFieldsLinkDisplayedClickable()
        {
            Verify.IsElementVisibleWithSoftAssertion(this.DriverContext, this.levelTwoManageEligibleFieldLink, this.nmlevelTwoManageEligibleFieldLink);
            Verify.IsElementClickableWithSoftAssertion(this.DriverContext, this.levelTwoManageEligibleFieldLink, this.nmlevelTwoManageEligibleFieldLink);
        }

        public void IsHierarchyUniqueLevelErrorDisplayed(string header)
        {
            this.Driver.WaitUntilElementIsFound(this.errorHierarchyUniqueLevel.Format(header), BaseConfiguration.LongTimeout);
            this.Driver.IsElementVisible(this.errorHierarchyUniqueLevel.Format(header), this.nmHierarchyUniqueLevelerrormessage);
        }

        public void IsCommitteeTypeTextAttributeDisplayed()
        {
            this.Driver.IsElementGivenAttributePresent(this.committeeTypetext, this.nmcommitteeTypetext, "type", "text");
        }

        public void IsCommitteeTypeTextBlankErrorMessageDisplayed(string expected)
        {
            this.Driver.EnterText(this.committeeTypetext, string.Empty, this.nmcommitteeTypetext);
            string actual = this.Driver.GetText(this.errorMessageBlankOnCommiteeType);
            this.Driver.IsExpectedTextMatchWithActualText(this.errorMessageBlankOnCommiteeType, expected, actual);
        }

        public void IsCommitteeTypeTextAlreadyExistsErrorMessageDisplayed(string expected)
        {
            string actual = this.Driver.GetText(this.errorMsgUnique);
            this.Driver.IsExpectedTextMatchWithActualText(this.errorMsgUnique, expected, actual);
        }

        public void IsEditButtonCommitteeTypeClickable()
        {
            this.Driver.WaitUntilElementIsNoLongerFound(this.loadbutton, BaseConfiguration.LongTimeout, this.nmdimmerloading);
            this.Driver.WaitUntilElementIsFound(this.editButton, BaseConfiguration.LongTimeout);
            this.Driver.IsElementVisible(this.editButton, this.nmEditButton);
            this.Driver.IsElementClickable(this.editButton, this.nmEditButton);
        }

        public void IsDefaultBalanceReqiuredSelectedAsNo(string defaultBalanceRequired)
        {
            try
            {
                this.Driver.PageScrollDownToBottom();
                var by = this.balanceRequiredForNoButton.ToBy();
                IWebElement element = this.Driver.FindElement(by);
                var defaultValue = element.GetAttribute("value");
                Verify.That(this.DriverContext, () => Assert.AreEqual(defaultBalanceRequired, defaultValue), "Verifying whether default value selected for Balance Required is : NO :", "default value selected for Balance Required is : NO : ", "Default value selected for Balance Required is " + defaultValue);
            }
            catch (Exception)
            {
                DriverContext.ExtentStepTest.Log(LogStatus.Fail, "To verify Default Balance Required button not visible", "An exception occurred waiting for Default Balance Required to selected as NO");
                Logger.Error("Failed to select Balance Required Radio button");
                throw;
            }
        }

        public void IsUserAbleToClickYesButtonForBalancerequired()
        {
            this.Driver.PageScrollDownToBottom();
            Verify.IsElementVisibleWithSoftAssertion(this.DriverContext, this.brlabel, this.nmaddlevelbutton);
            Verify.IsElementClickableWithSoftAssertion(this.DriverContext, this.balanceRequiredForYesButton, this.nmbalanceRequiredForYesButton);
        }

        public void SelectMembershipTypefromFirstListInBalanceRequired()
        {
            Verify.WaitUntilElementIsNoLongerFoundWithSoftAssertion(this.DriverContext, this.loadbutton, this.nmmembershipTypeInBR, BaseConfiguration.LongTimeout);
            Verify.IsElementClickableWithSoftAssertion(this.DriverContext, this.membershipTypeInBR, this.nmmembershipTypeInBR);
        }

        public void SelectOtherMembershipTypefromFirstListInBalanceRequired()
        {
            Verify.WaitUntilElementIsNoLongerFoundWithSoftAssertion(this.DriverContext, this.loadbutton, this.nmOtherMembershipTypeInBR, BaseConfiguration.LongTimeout);
            Verify.IsElementClickableWithSoftAssertion(this.DriverContext, this.producerMembershipTypeBR, this.nmOtherMembershipTypeInBR);
        }

        public void SelectthirdMembershipTypefromFirstListInBalanceRequired()
        {
            Verify.WaitUntilElementIsNoLongerFoundWithSoftAssertion(this.DriverContext, this.loadbutton, this.nmThirdMembershipTypeInBR, BaseConfiguration.LongTimeout);
            Verify.IsElementClickableWithSoftAssertion(this.DriverContext, this.thirdMembershipTypeBR, this.nmThirdMembershipTypeInBR);
        }

        public void SelectthirdMembershipTypefromSecondListInBalanceRequired()
        {
            Verify.WaitUntilElementIsFoundWithSoftAssertion(this.DriverContext, this.thirdMembershipTypeInBR2, this.nmmembershipTypeInBR, BaseConfiguration.LongTimeout);
            Verify.IsElementClickableWithSoftAssertion(this.DriverContext, this.thirdMembershipTypeInBR2, this.nmmembershipTypeInBR);
        }

        public void SelectMembershipTypefromSecondListInBalanceRequired()
        {
            Verify.WaitUntilElementIsFoundWithSoftAssertion(this.DriverContext, this.membershipTypeInBR2, this.nmmembershipTypeInBR, BaseConfiguration.LongTimeout);
            Verify.IsElementClickableWithSoftAssertion(this.DriverContext, this.membershipTypeInBR2, this.nmmembershipTypeInBR);
        }

        public void SelectOtherMembershipTypefromSecondListInBalanceRequired()
        {
            Verify.WaitUntilElementIsFoundWithSoftAssertion(this.DriverContext, this.membershipTypeInBR2, this.nmOtherMembershipTypeInBR, BaseConfiguration.LongTimeout);
            Verify.IsElementClickableWithSoftAssertion(this.DriverContext, this.newClassMembershipTypeInBR2, this.nmOtherMembershipTypeInBR);
        }

        public string GetTextSelectMembershipTypefromFirstListInBalanceRequired()
        {
            this.Driver.WaitUntilElementIsNoLongerFound(this.loadbutton, BaseConfiguration.LongTimeout, this.nmmembershipTypeInBR);
            return this.Driver.GetTextForSelectedElementfromList(this.membershipTypeInBR, this.nmmembershipTypeInBR, 1);
        }

        public string GetTextSelectMembershipTypefromSecondListInBalanceRequired()
        {
            this.Driver.WaitUntilElementIsNoLongerFound(this.loadbutton, BaseConfiguration.LongTimeout, this.nmmembershipTypeInBR);
            return this.Driver.GetTextForSelectedElementfromList(this.membershipTypeInBR2, this.nmmembershipTypeInBR, 3);
        }

        public void IsErrorMessageDisplayedForNotSelectingOperator(string message)
        {
            this.Driver.GetText(this.mandatoryFieldErrorMsg);
            this.Driver.IsExpectedTextMatchWithActualText(this.mandatoryFieldErrorMsg, message);
        }

        public void IsOperatorSelectable(string conditionName)
        {
            Verify.IsElementClickableWithSoftAssertion(this.DriverContext, this.selectDropdownForOperator, this.nmselectDropdown);
            Verify.IsElementClickableFromListofElementWithTextWithSoftAssertion(this.DriverContext, this.operatorConditionSelection, conditionName);
        }

        public void IsOperatorFieldADropdown()
        {
            try
            {
                var webElement = this.Driver.GetElement(this.dropDownIconforOperator, BaseConfiguration.LongTimeout);
                webElement.GetAttribute("class").Contains("dropdown");
                DriverContext.ExtentStepTest.Log(LogStatus.Pass, "To verify Operator field is dropDown ", " Operator field is dropdown");
            }
            catch (Exception ex)
            {
                Logger.Error("Failed  Due to exception: " + ex.ToString());

                DriverContext.ExtentStepTest.Log(LogStatus.Fail, "To verify Operator field is dropDown ", "An exception occurred while Verifying operator field ");
                throw;
            }
        }

        public void IsChildDisplayedafterParentLevel(string message, int hierarchyLevelNo)
        {
            this.Driver.IsExpectedTextMatchWithActualText(this.parentChildHierarchyLevel.Format(hierarchyLevelNo), this.GetStringHierarchyLevel(this.nmTextParentClient, hierarchyLevelNo) + message);
        }

        public void IsChildLevelDisplayed(string message, int hierarchyLevelNo, string pagename)
        {
            string hierarchyLevelChildText = this.GetStringHierarchyLevel(this.nmTextParentClient, hierarchyLevelNo, pagename) + message;
            this.Driver.IsElementPresentOrNot(this.parentChildHierarchyLevelDisplay.Format(hierarchyLevelChildText), this.nmParentHierarchyLevelVisible, hierarchyLevelChildText);
        }

        public void IsParentLevelDisplayed(string message, int hierarchyLevelNo, string pagename)
        {
            string hierarchyLevelParentText = this.GetStringHierarchyLevel(this.nmTextParentClient, hierarchyLevelNo, pagename) + message;
            this.Driver.IsElementPresentOrNot(this.parentChildHierarchyLevelDisplay.Format(hierarchyLevelParentText), this.nmParentHierarchyLevelVisible, hierarchyLevelParentText);
        }

        public void ParentClildHieracyCloseButton(int hierarchyLevelNo)
        {
            Verify.IsElementVisibleWithSoftAssertion(this.DriverContext, this.parentChildHierarchyLevelCloseButton.Format(hierarchyLevelNo), this.nmParentChildHierarchyLevelCloseButton);
            Verify.IsElementClickableWithSoftAssertion(this.DriverContext, this.parentChildHierarchyLevelCloseButton.Format(hierarchyLevelNo), this.nmParentChildHierarchyLevelCloseButton);
        }

        public string GetStringHierarchyLevel(string text, int loop, string pagename = "")
        {
            string resultText = string.Empty;
            if (loop > 1)
            {
                for (int i = 1; i <= loop; i++)
                {
                    resultText += text;
                }

                resultText = resultText.Remove(resultText.Length - 1, 1);
                resultText = resultText + " ";
            }
            else
            {
                resultText = "1.0 ";
            }

            if (pagename == "Display")
            {
                resultText = resultText + " ";
            }

            return resultText;
        }

        public void IsOperatorConditionVisibleAsExpected(string condition)
        {
            bool value = this.Driver.IsElementPresent(this.balanceRuleInViewPage, BaseConfiguration.LongTimeout);
            Verify.That(this.DriverContext, () => Assert.IsTrue(value), "Verifying whether Set Balance Rule is : Visible :", "Set Balance Rule  is : Visible : ", "Set  Balance Rule is : Not Visible :");
            this.Driver.IsExpectedTextMatchWithActualText(this.operatorConditionInViewPage, condition);
        }

        public void IsUserableToGetOperatorsList()
        {
            string verifyMessage = "To Verify operator list with all conditions ";
            string failMessage = "An exception occured while verifying operator conditions list";
            string passMessage = "Operator list contains all expected conditions";
            try
            {
                this.Driver.PageScrollDownToBottom();
                this.Driver.IsElementClickable(this.selectDropdownForOperator, this.nmselectDropdown);
                IList<string> lstElements1 = new List<string>() { "greater or equal", "less or equal", "equals", "not equal to", "less than", "greater than", "Select" };
                IList<string> lstElements2 = new List<string>();
                IList<IWebElement> webelementslist = this.Driver.GetElements(this.operatorsList);
                foreach (IWebElement i in webelementslist)
                {
                    lstElements2.Add(i.Text);
                }

                for (int i = 0; i < lstElements1.Count; i++)
                {
                    if (lstElements1[i] != lstElements2[i])
                    {
                        Verify.That(this.DriverContext, () => Assert.AreSame(lstElements2[i], lstElements1[i], failMessage));
                        break;
                    }
                }

                DriverContext.ExtentStepTest.Log(LogStatus.Pass, verifyMessage, passMessage);
            }
            catch (Exception ex)
            {
                Logger.Error("Failed  Due to exception: " + ex.ToString());

                DriverContext.ExtentStepTest.Log(LogStatus.Fail, verifyMessage, failMessage);
                throw;
            }
        }

        public int IsUserableToGetClassificationCountforFirstsection()
        {
            var lstElements = this.Driver.GetElements(this.memberClassificationsCountFirstSection);
            int count = lstElements.Count;
            return count;
        }

        public int IsUserableToGetClassificationCountforSecondsection()
        {
            var lstElements = this.Driver.GetElements(this.memberClassificationsCountSecondSection);
            int count = lstElements.Count;
            return count;
        }

        public void IsClassificationCountDisplayedasConfiguredInSystem(int actualCount, int expectedCount)
        {
            Verify.That(this.DriverContext, () => Assert.AreEqual(actualCount, expectedCount), "Verifying whether classification count is displayed as per system Configuration:", "Classification count is displayed as per system Configuration", "classification count is not displayed as per system Configuration");
        }
    }
}