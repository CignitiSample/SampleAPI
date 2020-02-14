// <copyright file="InternalStaffRenewalTasksPage.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace MCS.Test.Automation.Tests.PageObjects.PageObjects.MCS
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.IO;
    using System.Linq;
    using System.Text.RegularExpressions;
    using System.Threading;
    using global::MCS.Test.Automation.Common;
    using global::MCS.Test.Automation.Common.Extensions;
    using global::MCS.Test.Automation.Common.Helpers;
    using global::MCS.Test.Automation.Common.Types;
    using global::MCS.Test.Automation.Tests.PageObjects;
    using NLog;
    using NUnit.Framework;
    using OpenQA.Selenium;
    using RelevantCodes.ExtentReports;

    public class InternalStaffRenewalTasksPage : ProjectPageBase
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();
        private readonly ElementLocator
            renewalTasksHeader = new ElementLocator(Locator.CssSelector, " div.headingTitle.clearfix > h2");

        private readonly ElementLocator
            addTasksBtn = new ElementLocator(Locator.XPath, "//*[text()=' Add Task']");

        private readonly ElementLocator
            addTasksPopupHeader = new ElementLocator(Locator.CssSelector, "div.header");

        private readonly ElementLocator
            titleTextBox = new ElementLocator(Locator.XPath, "//*[@name='Title']");

        private readonly ElementLocator
           renewalTaskYear = new ElementLocator(Locator.XPath, "(//div[@class='ui selection dropdown'])[2]");

        private readonly ElementLocator
           assigneeDropdownXPath = new ElementLocator(Locator.XPath, "//label[text()='Status']//..//div[@class='ui selection dropdown']//i");

        private readonly ElementLocator
           renewaltaskHomePageDropdownXPath = new ElementLocator(Locator.XPath, "//div['renewalTask']//div[@class='ui selection dropdown']//i");

        private readonly ElementLocator
        renewaltaskHomePageDropDownOptions = new ElementLocator(Locator.CssSelector, "div.visible.menu.transition div.item span.text");

        private readonly ElementLocator
        renewaltaskHomePageUpComingCard = new ElementLocator(Locator.CssSelector, "div.upcomingDv div.no-task-msg");

        private readonly ElementLocator
        renewaltaskHomePageOpenCard = new ElementLocator(Locator.CssSelector, "div.openDv div.no-task-msg");

        private readonly ElementLocator
        renewaltaskHomePageDoneCard = new ElementLocator(Locator.CssSelector, "div.doneDv div.no-task-msg");

        private readonly ElementLocator
        assigneeDropDownOptions = new ElementLocator(Locator.CssSelector, "div.visible.menu.transition div.item span.text");

        private readonly ElementLocator
            startDateSelection = new ElementLocator(Locator.XPath, "//*[@name='StartDate']");

        private readonly ElementLocator
            endDateSelection = new ElementLocator(Locator.XPath, "//*[@name='EndDate']");

        private readonly ElementLocator
          descriptionBox = new ElementLocator(Locator.CssSelector, "div.notranslate.public-DraftEditor-content");

        private readonly ElementLocator
         assigneeTextBox = new ElementLocator(Locator.XPath, "//*[@name='AssigneeId']/*[@type='text']");

        private readonly ElementLocator
        dependsOnTextBox = new ElementLocator(Locator.XPath, "//*[@name='NewDependsList']/*[@type='text']");

        private readonly ElementLocator
            textInsidedependsOnTextBox = new ElementLocator(Locator.XPath, "//*[@name='NewDependsList']/*[@class='ui label']");

        private readonly ElementLocator
         saveBtn = new ElementLocator(Locator.XPath, "//*[@type='submit']");

        private readonly ElementLocator
         renewalTaskName = new ElementLocator(Locator.XPath, "//div[@class='taskName ellip']/a");

        private readonly ElementLocator
         taskTitle = new ElementLocator(Locator.CssSelector, "div.taskTitle");

        private readonly ElementLocator
         editBtn = new ElementLocator(Locator.XPath, "//h4[text()='{0}']/a/i");

        private readonly ElementLocator
           cloneTaskPopupTitle = new ElementLocator(Locator.XPath, "//input[@name='Title'][@value='{0}']");

        private readonly ElementLocator
         assigneeDropdownIcon = new ElementLocator(Locator.XPath, "//div[@name='AssigneeId']/i");

        private readonly ElementLocator
        taskId = new ElementLocator(Locator.CssSelector, "span.taskID");

        private readonly ElementLocator
        inactiveCheckBox = new ElementLocator(Locator.XPath, "//*[@type='checkbox']");

        private readonly ElementLocator
        reasonTextfield = new ElementLocator(Locator.XPath, "//*[@name='DeleteReason']");

        private readonly ElementLocator
        updateBtn = new ElementLocator(Locator.CssSelector, "i.check.icon");

        private readonly ElementLocator
           successfullMsg = new ElementLocator(Locator.CssSelector, "div.content > p");

        private readonly ElementLocator
            successmsg = new ElementLocator(Locator.CssSelector, "div.top-message div.ui.compact.message.success div.content p");

        private readonly ElementLocator
       openTasksList = new ElementLocator(Locator.CssSelector, "div.taskName.ellip a");

        private readonly ElementLocator
            cards = new ElementLocator(Locator.CssSelector, "div.ui.card.taskCard.overdue");

        private readonly ElementLocator
            generalInfoEditbutton = new ElementLocator(Locator.XPath, "//*[text()='General Information']/a/i");

        private readonly ElementLocator
            assigneeEditbutton = new ElementLocator(Locator.XPath, "//*[text()='Assignee']/a/i");

        private readonly ElementLocator
        closeBtn = new ElementLocator(Locator.XPath, "//h4[text()='{0}']//i[@class='close icon']");

        private readonly ElementLocator
         inactivestatusChkBoxText = new ElementLocator(Locator.XPath, "//*[text()='Inactive this Task']");

        private readonly ElementLocator
            inactiveTagOnTask = new ElementLocator(Locator.XPath, "//div[text()='Inactive']");

        private readonly ElementLocator
            inactiveDescTextBox = new ElementLocator(Locator.XPath, "//*[@name='DeleteReason']");

        private readonly ElementLocator
            updateBtnForInactiveTask = new ElementLocator(Locator.CssSelector, "i.check.icon");

        private readonly ElementLocator
            watcherslabel = new ElementLocator(Locator.CssSelector, "span.view-watchers");

        private readonly ElementLocator
            dependsOnAddButton = new ElementLocator(Locator.XPath, "//button[text()=' Add ']");

        private readonly ElementLocator
            dependencySearchBox = new ElementLocator(Locator.XPath, "//div[@name='DependsOnId']/input");

        private readonly ElementLocator
            dependenciesList = new ElementLocator(Locator.XPath, "//*[@class='visible menu transition']/div/span[contains(text(), '{0}')]");

        private readonly ElementLocator
            newlyaddedDependenciesTable = new ElementLocator(Locator.CssSelector, " th.membershipName");

        private readonly ElementLocator
           addButtonAfterDependenciesadded = new ElementLocator(Locator.XPath, "//*[@class='description']//*[@class='column actions']/button[text()='Add']");

        private readonly ElementLocator
            noDependenciesText = new ElementLocator(Locator.XPath, "//div[text()='Not depended on any Tasks.']");

        private readonly ElementLocator
           defaultRenewalYear = new ElementLocator(Locator.XPath, "//*[@class='ui label']");

        private readonly ElementLocator
           selectRenewalYear = new ElementLocator(Locator.XPath, "//*[@class='visible menu transition']/div/span");

        private readonly ElementLocator
           clickRenewalYearTextBox = new ElementLocator(Locator.XPath, "//*[@class='ui fluid multiple selection dropdown']");

        private readonly ElementLocator
           generalInfoCancelbutton = new ElementLocator(Locator.XPath, "//h4//button[@title='Cancel']");

        private readonly ElementLocator
          loadbutton = new ElementLocator(Locator.XPath, "//*[@class='ui active transition visible dimmer']");

        private readonly ElementLocator
           watcherEyeIcon = new ElementLocator(Locator.XPath, "//i[@class='eye slash icon']");

        private readonly ElementLocator
           titleErrorMessage = new ElementLocator(Locator.XPath, "//span[contains(text(),'Title is required.')]");

        private readonly ElementLocator
           startDateErrorMessage = new ElementLocator(Locator.XPath, "//span[contains(text(),'Start Date is required')]");

        private readonly ElementLocator
           endDateErrorMessage = new ElementLocator(Locator.XPath, "//span[contains(text(),'End Date is required')]");

        private readonly ElementLocator
          fileFormatErrorMessage = new ElementLocator(Locator.XPath, "//div[@class='errorMessage']");

        private readonly ElementLocator
           inputStartDateErrorMessage = new ElementLocator(Locator.XPath, "//span[contains(text(),'Start Date is invalid')]");

        private readonly ElementLocator
           inputEndDateErrorMessage = new ElementLocator(Locator.XPath, "//span[contains(text(),'End Date is invalid')]");

        private readonly ElementLocator
            popupCloseBtn = new ElementLocator(Locator.XPath, "//i[@class='close']");

        private readonly ElementLocator
            upcomingTasksCount = new ElementLocator(Locator.XPath, "//*[starts-with(text(),'Upcoming')]/span");

        private readonly ElementLocator
            upcomingtasksListNo = new ElementLocator(Locator.XPath, "//*[@class='upcomingDv']//*[@class='taskName ellip']");

        private readonly ElementLocator
           addWatcherIcon = new ElementLocator(Locator.XPath, "//span[@class='add-watcher-btn'][@title='Save']");

        private readonly ElementLocator
          watcherIcon = new ElementLocator(Locator.XPath, "//div[@class='actionItem']/ul/li[3]/i");

        private readonly ElementLocator
            watchersList = new ElementLocator(Locator.XPath, "//p[text()='Watchers']//..//ul//li[text()]");

        private readonly ElementLocator
            watchersSubHeading = new ElementLocator(Locator.XPath, "//p[text()='Watchers']");

        private readonly ElementLocator
            watcherListWrapper = new ElementLocator(Locator.XPath, "//div[@class='watcher-list']");

        private readonly ElementLocator
            userAddedFromTaskListToWatchers = new ElementLocator(Locator.XPath, "//p[text()='Watchers']//..//li");

        private readonly ElementLocator
           openDvTasksCount = new ElementLocator(Locator.XPath, "//*[starts-with(text(),'Open')]/span");

        private readonly ElementLocator
            openDvtasksListNo = new ElementLocator(Locator.XPath, "//*[@class='openDv']//*[@class='taskName ellip']");

        private readonly ElementLocator
         doneDvTasksCount = new ElementLocator(Locator.XPath, "//*[starts-with(text(),'Done')]/span");

        private readonly ElementLocator
           doneDvtasksListNo = new ElementLocator(Locator.XPath, "//*[@class='doneDv']//*[@class='taskName ellip']");

        private readonly ElementLocator
            viewTaskId = new ElementLocator(Locator.XPath, "//span[text()='Task ID']");

        private readonly ElementLocator
           viewCloneTaskConfirmationMessage = new ElementLocator(Locator.XPath, "//div[text()='Do you want to clone the attachments?']");

        private readonly ElementLocator
          cloneTaskAttachedDocList = new ElementLocator(Locator.XPath, "//div//span[@class='fileName']");

        private readonly ElementLocator
            viewCloneTaskAttachedDocument = new ElementLocator(Locator.XPath, "//span[@class='fileName'][text()='sample.pdf']");

        private readonly ElementLocator
            viewTitle = new ElementLocator(Locator.XPath, "//span[text()='Title']");

        private readonly ElementLocator
            viewStartDate = new ElementLocator(Locator.XPath, "//div[@class='column twoColInput']//span[text()='Start Date']");

        private readonly ElementLocator
            viewEndDate = new ElementLocator(Locator.XPath, "//div[@class='column twoColInput']//span[text()='End Date']");

        private readonly ElementLocator
            viewRenewalYear = new ElementLocator(Locator.XPath, "//div[@class='column twoColInput']//span[text()='Renewal Year']");

        private readonly ElementLocator
            viewDescription = new ElementLocator(Locator.XPath, "//span[text()='Description']");

        private readonly ElementLocator
            viewAssignee = new ElementLocator(Locator.XPath, "//div[@class='column twoColInput']//span[text()='Assignee']");

        private readonly ElementLocator
            viewStatus = new ElementLocator(Locator.XPath, "//div[@class='column twoColInput']//span[text()='Status']");

        private readonly ElementLocator
            viewRenewalTaskDetailsPage = new ElementLocator(Locator.CssSelector, "div.active.section");

        private readonly ElementLocator
           viewSortyBy = new ElementLocator(Locator.XPath, "//span[@id='Upcoming']");

        private readonly ElementLocator
           viewSortEndDate = new ElementLocator(Locator.XPath, "//li[contains(.,'End Date')]");

        private readonly ElementLocator
            viewIncomingTitle = new ElementLocator(Locator.XPath, "//a[contains(.,'{0}')]");

        private readonly ElementLocator
           viewOpenSortyBy = new ElementLocator(Locator.XPath, "//span[@id='Open']");

        private readonly ElementLocator
           viewOpenSortEndDate = new ElementLocator(Locator.XPath, "//li[contains(.,'End Date')]");

        private readonly ElementLocator
            viewOpenTitle = new ElementLocator(Locator.XPath, "//a[contains(.,'{0}')]");

        private readonly ElementLocator
            viewBannerTitle = new ElementLocator(Locator.XPath, "//div[@class='taskTitle'][contains(.,'{0})]");

        private readonly ElementLocator
            viewBannerStatus = new ElementLocator(Locator.XPath, "//div[@class='taskStatus false']");

        private readonly ElementLocator
            viewBannerTaskID = new ElementLocator(Locator.XPath, "//span[contains(@class,'taskID')]");

        private readonly ElementLocator
            viewBannerRenewalYear = new ElementLocator(Locator.XPath, "//div[contains(@class,'taskYear')]");

        private readonly ElementLocator
            viewBannerAssignee = new ElementLocator(Locator.XPath, "(//a[@class='banner-link'])[2]");

        private readonly ElementLocator
            viewBannerTaskOwner = new ElementLocator(Locator.XPath, "(//a[@class='banner-link'])[1]");

        private readonly ElementLocator
           viewBannerWatchersCount = new ElementLocator(Locator.XPath, "//span[@class='watch-counter']");

        private readonly ElementLocator
          attachmentIcon = new ElementLocator(Locator.XPath, "//span[@class='attachment-icon']");

        private readonly ElementLocator
         noWatchersText = new ElementLocator(Locator.XPath, "//span[text()='No watchers to display.']");

        private readonly ElementLocator
           viewBannerDateTimeStamp = new ElementLocator(Locator.XPath, "//div[@class='updatedByInfo']");

        private readonly ElementLocator
          viewBannerStartWatcherIssue = new ElementLocator(Locator.XPath, "//*[@class='actionItem']/ul/li[3]/i");

        private readonly ElementLocator
          viewBannerSetReminder = new ElementLocator(Locator.XPath, "//*[@class='actionItem']/ul/li[1]/i");

        private readonly ElementLocator
           reminderDateTextbox = new ElementLocator(Locator.XPath, "//input[@name='ReminderDate']");

        private readonly ElementLocator
            setButton = new ElementLocator(Locator.XPath, "//button[text()='Set']");

        private readonly ElementLocator
          viewBannerCloneIssue = new ElementLocator(Locator.XPath, "//*[@class='actionItem']/ul/li[2]/i");

        private readonly ElementLocator
        communicationsTab = new ElementLocator(Locator.XPath, "//a[text()='Communication Log']");

        private readonly ElementLocator
            communicationLogCommentsText = new ElementLocator(Locator.XPath, "//a[@class='avatar']//..//div[@class='text']");

        private readonly ElementLocator
            totalNoOfComments = new ElementLocator(Locator.XPath, "//div[@class='ui minimal comments']//..//h4");

        private readonly ElementLocator
            thirdCommunicationLogAvatar = new ElementLocator(Locator.XPath, "(//div//..//a[@class='avatar'])[3]");

        private readonly ElementLocator
            editIconCommunicationLog = new ElementLocator(Locator.XPath, "//i[@class='pencil icon'][@aria-hidden='true']");

        private readonly ElementLocator
            userAvatarIcon = new ElementLocator(Locator.XPath, "//*[@class='commentposteditor']/following::div[2]");

        private readonly ElementLocator
           divEditComment = new ElementLocator(Locator.XPath, "(//div[@class='commentposteditor'])[2]");

        private readonly ElementLocator
            updateCommentCommunicationLog = new ElementLocator(Locator.XPath, "//button[@type='submit']");

        private readonly ElementLocator
            commentTextBox = new ElementLocator(Locator.CssSelector, "div.commentposteditor");

        private readonly ElementLocator
           submitBtn = new ElementLocator(Locator.XPath, "//button[text()='Submit']");

        private readonly ElementLocator
          usernameOfComment = new ElementLocator(Locator.XPath, "//div[@class='comment']//span[@class='edit-delete']");

        private readonly ElementLocator
        deleteCommentIcon = new ElementLocator(Locator.XPath, "//*[@class='edit-delete']//i[@class='delete icon']");

        private readonly ElementLocator
            deleteCommentPopupWindow = new ElementLocator(Locator.XPath, "//*[@class='ui small modal transition visible active confirm-box']//div[@class='content']");

        private readonly ElementLocator
       deleteYesButton = new ElementLocator(Locator.XPath, "//*[@class='ui primary button']");

        private readonly ElementLocator
               searchBarforRenewalTask = new ElementLocator(Locator.XPath, "//div[@class='ui input searchInput']/input");

        private readonly ElementLocator
            searchIconforTasks = new ElementLocator(Locator.XPath, "//*[@class='ui button primary']/i");

        private readonly ElementLocator
           renewalTaskCardName = new ElementLocator(Locator.XPath, "//div[@class='taskName ellip']/a[text()='{0}']");

        private readonly ElementLocator
         renewalTaskSearchBox = new ElementLocator(Locator.XPath, " //div[@role='alert']/following::input[1]");

        private readonly ElementLocator
        searchBtnIcon = new ElementLocator(Locator.CssSelector, "i.search.icon");

        private readonly ElementLocator
                systemGeneratedRenewalTaskID = new ElementLocator(Locator.XPath, "//p[@class='taskID']/a[1]");

        private readonly ElementLocator
           renewalTaskSelection = new ElementLocator(Locator.XPath, "//div[@class='text'][contains(text(),'Renewal Tasks')]");

        private readonly ElementLocator
       removeDependencyTask = new ElementLocator(Locator.XPath, "(//i[contains(@class,'delete icon')])[1]");

        private readonly ElementLocator
          renewalSearchHint = new ElementLocator(Locator.XPath, "//input[contains(@title,'{0}')]");

        private readonly ElementLocator
          renewalSearchInput = new ElementLocator(Locator.XPath, "//input[@placeholder='Search Task by Task ID, Title, Assignee']");

        private readonly ElementLocator
         renewalSearch = new ElementLocator(Locator.XPath, "//i[@class='search icon']");

        private readonly ElementLocator
         cloneTaskConfirmationMsgYesBtn = new ElementLocator(Locator.XPath, "//button[text()='Yes']");

        private readonly ElementLocator
        cloneTaskConfirmationMsgNoBtn = new ElementLocator(Locator.XPath, "//button[text()='No']");

        private readonly ElementLocator
       cloneTaskConfirmationPopupYesButton = new ElementLocator(Locator.XPath, "//*[@class='ui primary button']");

        private readonly ElementLocator
        searchfeatureDropdownIcon = new ElementLocator(Locator.XPath, "//*[@class='ui compact selection dropdown']/i");

        private readonly ElementLocator
        searchfeatureDropdownList = new ElementLocator(Locator.XPath, "//*[@class='visible menu transition']/div");

        private readonly ElementLocator
            dependencyTaskInfo = new ElementLocator(Locator.XPath, "//td/a[text()='{0}']");

        private readonly ElementLocator
           cloneTaskPopupDiscardBtn = new ElementLocator(Locator.XPath, "//button[@class='ui button cancel']");

        private readonly ElementLocator
            dependantTasksColumn = new ElementLocator(Locator.XPath, "//h4[text()='Dependent Tasks']/../../..//th[text()='{0}']");

        private readonly ElementLocator
        renewalDependentCombo = new ElementLocator(Locator.XPath, "//div[contains(@name,'NewDependsList')]");

        private readonly ElementLocator
         renewalDependentInput = new ElementLocator(Locator.XPath, "//div[@name='NewDependsList']//input[@class='search']");

        private readonly ElementLocator
        renewalDependentSort = new ElementLocator(Locator.XPath, "(//td[@class='Name'])[1]");

        private readonly ElementLocator
        renewalDependentSelection = new ElementLocator(Locator.XPath, "//span[contains(.,'- {0}')]");

        private readonly ElementLocator
            dependsOnassigneeNameToRedirect = new ElementLocator(Locator.XPath, "//table[@class='customTable']//td/a[text()='{0}']");

        private readonly ElementLocator
            bannerUserName = new ElementLocator(Locator.XPath, "//span[text()='Username']/following-sibling :: span[text()='{0}']");

        private readonly ElementLocator
          renewalTaskHeaders = new ElementLocator(Locator.XPath, "//div[@class='taskCount' and contains(text(),'{0}')]");

        private readonly ElementLocator
           assigneeRenewalTask = new ElementLocator(Locator.XPath, "//a[contains(@title,'Assignee')]");

        private readonly ElementLocator
          yearDateRenewalTask = new ElementLocator(Locator.XPath, "//span[contains(@class,'yearDate')]");

        private readonly ElementLocator
          startDateRenewalTask = new ElementLocator(Locator.XPath, "//span[contains(@title,'Start Date')]");

        private readonly ElementLocator
          endDateRenewalTask = new ElementLocator(Locator.XPath, "//span[contains(@title,'End Date')]");

        private readonly ElementLocator
          taskIDRenewalTask = new ElementLocator(Locator.XPath, "//p[@class='taskID']//a");

        private readonly ElementLocator
          clockIconRenewalTask = new ElementLocator(Locator.XPath, "//i[contains(@class,'clock outline icon')]");

        private readonly ElementLocator
          copyIconRenewalTask = new ElementLocator(Locator.XPath, "//i[@class='copy outline icon']");

        private readonly ElementLocator
          eyeIconRenewalTask = new ElementLocator(Locator.XPath, "//i[@class='eye icon']");

        private readonly ElementLocator
          cloneTaskIcon = new ElementLocator(Locator.XPath, "//i[@class='copy outline icon']");

        private readonly ElementLocator
         dragToOpen = new ElementLocator(Locator.XPath, "//div[@class='openDv']");

        private readonly ElementLocator
         dragFromOpen = new ElementLocator(Locator.XPath, "//p[@class='taskID']//a");

        private readonly ElementLocator
         dragToDone = new ElementLocator(Locator.XPath, "//div[@class='doneDv']");

        private readonly ElementLocator
         dragfromUpComing = new ElementLocator(Locator.XPath, "//p[@class='taskID']//a");

        private readonly ElementLocator
         dragfromUpComingIndexWise = new ElementLocator(Locator.XPath, "//div[@class='upcomingDv']//div[@class='cardCell'][{0}]//p[@class='taskID']//a[1]");

        private readonly ElementLocator
         popupMessage1Assignee = new ElementLocator(Locator.XPath, "//div[@class='content'][contains(.,'{0}')]");

        private readonly ElementLocator
         popupBtn1Assignee = new ElementLocator(Locator.CssSelector, "div.actions button.ui.primary.button");

        private readonly ElementLocator
        cloneBtnAssignee = new ElementLocator(Locator.CssSelector, "div.column.actions button.ui.primary.button");

        private readonly ElementLocator
            uploadbutton = new ElementLocator(Locator.CssSelector, "div.uploadBox div.iconHolder");

        private readonly ElementLocator
            browsebutton = new ElementLocator(Locator.CssSelector, "div.infoHolder p a");

        private readonly ElementLocator
           searchWatcherByUserIdInput = new ElementLocator(Locator.XPath, "//div[@name='NewWatchersList']//input");

        private readonly ElementLocator
            searchWatcherDropdownList = new ElementLocator(Locator.XPath, "//div[@name='NewWatchersList']//span[@class='text']");

        private readonly ElementLocator
           searchWatcherDropdownIcon = new ElementLocator(Locator.XPath, "//div[@name='NewWatchersList']/i");

        private readonly ElementLocator
           searchWatcherByUserId = new ElementLocator(Locator.XPath, "//div[@name='NewWatchersList']");

        private readonly ElementLocator
        searchWatcherByUserIdOptions = new ElementLocator(Locator.XPath, "//div[@name='NewWatchersList']/..//div[@role='option']");

        private readonly ElementLocator
       inactiveTaskRenewal = new ElementLocator(Locator.XPath, "//p[@class='inactive-status']");

        private readonly ElementLocator
      sectionRenewalTask = new ElementLocator(Locator.XPath, "//a[@class='section']/a");

        private readonly ElementLocator
      checkCardExistsUpcoming = new ElementLocator(Locator.XPath, "//div[@class='upcomingDv']//div[@class='cardCell'][1]");

        private readonly ElementLocator
    checkCardExistsOpen = new ElementLocator(Locator.XPath, "//div[@class='openDv']//div[@class='cardCell'][1]");

        private readonly ElementLocator
    checkCardExistsDone = new ElementLocator(Locator.XPath, " //div[@class='doneDv']//div[@class='cardCell'][1]");

        private readonly ElementLocator
            inactiveTaskCardList = new ElementLocator(Locator.XPath, "//p[@class='inactive-status']/../preceding-sibling :: div[@class='taskName ellip']/a");

        private readonly ElementLocator
          auditLogTab = new ElementLocator(Locator.XPath, "//a[text()=' Audit Log']");

        private readonly ElementLocator
          fromDateauditLogTab = new ElementLocator(Locator.XPath, "//input[@name='dateFrom']");

        private readonly ElementLocator
          toDateauditLogTab = new ElementLocator(Locator.XPath, "//input[@name='dateTo']");

        private readonly ElementLocator
          searchbuttonAuditLogTab = new ElementLocator(Locator.XPath, "//button[text()='Search']");

        private readonly ElementLocator
          errorMessageFromDate = new ElementLocator(Locator.XPath, "//span[@class='errorMessage']");

        private readonly ElementLocator
         overDueIndicator = new ElementLocator(Locator.XPath, "//div[@class='ui card taskCard  overdue']");

        private readonly ElementLocator
         doneColorIndicator = new ElementLocator(Locator.XPath, "//div[@class='ui card taskCard  ']");

        private readonly ElementLocator
         nobuttonPopup = new ElementLocator(Locator.XPath, "//button[@class='ui button'][contains(.,'No')]");

        private readonly ElementLocator
                 exportExcelBtn = new ElementLocator(Locator.XPath, "//button[@class='ui button secondary']");

        private readonly ElementLocator
        popUpMsgOnExportExcel = new ElementLocator(Locator.XPath, "//div[@class='content']");

        private readonly ElementLocator
        dependentIcon = new ElementLocator(Locator.XPath, "//i[@class='bookmark icon']");

        private readonly ElementLocator
        toolTipDependentIcon = new ElementLocator(Locator.XPath, "//span[contains(@title,'{0}')]");

        private readonly ElementLocator
        remainingdays = new ElementLocator(Locator.XPath, "//p[@class='remainingDays']");

        private readonly ElementLocator
       cardInOpenDiv = new ElementLocator(Locator.XPath, "//*[@class='openDv']//*[@class='taskName ellip']/a[text()='{0}']");

        private readonly ElementLocator
      hintTextUnderneathUploadBox = new ElementLocator(Locator.XPath, "//p[contains(.,'{0}')]");

        private readonly ElementLocator
          smallLoaderOnCardSection = new ElementLocator(Locator.XPath, "//div[contains(@class,'ui small text loader small-loader')]");

        private readonly ElementLocator
          removeDocument = new ElementLocator(Locator.XPath, "//i[contains(@class,'close icon')]");

        private readonly ElementLocator
  downloadDocument = new ElementLocator(Locator.XPath, "//div[@class='uploadedItem']//i[contains(@class,'download icon')]");

        private readonly ElementLocator
           downloadDocumentAll = new ElementLocator(Locator.XPath, "//button[@class='ui button ui button cancel downloadBtn']");

        private readonly ElementLocator
            hintTextInCommentsField = new ElementLocator(Locator.XPath, "//div[contains(@placeholder,'{0}')]");

        private readonly ElementLocator
   allcommunicationLogDateTime = new ElementLocator(Locator.XPath, "//div[@class='metadata']//div");

        private readonly ElementLocator
      taggeduserlist = new ElementLocator(Locator.XPath, "//div[@class='tribute-container']//li[1]");

        private readonly ElementLocator
            selectedUserToTag = new ElementLocator(Locator.XPath, "//a[contains(@title,'rkudikala')]");

        private readonly ElementLocator
           editableInput = new ElementLocator(Locator.XPath, "//div[@class='commentposteditor' and @contenteditable='true']");

        private readonly ElementLocator
            ownerComminicationLog = new ElementLocator(Locator.XPath, "//div//a[@class='author']");

        private readonly ElementLocator
            userTaggedCommunicationLog = new ElementLocator(Locator.XPath, "//a[@taguserid]");

        private readonly ElementLocator
      isTaggedUser = new ElementLocator(Locator.XPath, "//a[contains(@title,'{0}')]");

        private readonly ElementLocator
            editCommunicationLogComment = new ElementLocator(Locator.XPath, "(//div//div[@class='commentposteditor'])[2]");

        private readonly ElementLocator
            availableUsersListToTag = new ElementLocator(Locator.XPath, "//div[@class='tribute-container']//ul//li");

        private readonly ElementLocator
           deleteBreakDependency = new ElementLocator(Locator.XPath, "//div[contains(text(),'break the dependency?')]");

        private readonly ElementLocator
            msgBreakDependency = new ElementLocator(Locator.XPath, "//div[contains(@class,'truncatedText')]");

        private readonly ElementLocator
        advsearchBarDropDown = new ElementLocator(Locator.XPath, "//i[@class='dropdown icon openAdvSearch']");

        private readonly ElementLocator
      advsearchYearDropDown = new ElementLocator(Locator.XPath, "(//i[@class='dropdown icon'])[3]");

        private readonly ElementLocator
        advsearchSelectYear = new ElementLocator(Locator.XPath, "//div[@role='option'][contains(.,'{0}')]");

        private readonly ElementLocator
       advsearchSelectYearDelete = new ElementLocator(Locator.XPath, " (//i[contains(@class,'delete icon')])[1]");

        private readonly ElementLocator
       advsearchSearchButton = new ElementLocator(Locator.XPath, "//button[contains(@class,'ui primary button')]");

        private readonly ElementLocator
      advsearchSelectedYear = new ElementLocator(Locator.XPath, "//a[@class='ui label']");

        private readonly ElementLocator
                    onlyShowDependentasks = new ElementLocator(Locator.XPath, "//label[contains(.,'{0}')]");

        private readonly ElementLocator
        dependentcheckboxClicked = new ElementLocator(Locator.XPath, "//div[@class='ui checked checkbox checkAccordian']");

        private readonly ElementLocator
        dependentcheckboxUnClick = new ElementLocator(Locator.XPath, "//div[@class='ui checkbox checkAccordian']");

        private readonly ElementLocator
            breakDependencyBtn = new ElementLocator(Locator.XPath, "//button[@class='ui button']");

        private readonly ElementLocator
           noDependencyMsg = new ElementLocator(Locator.XPath, "(//div[@class='tableWrapper']/div[@class='no-item'])[2][text()='{0}']");

        private readonly ElementLocator
         browseForFilesLink = new ElementLocator(Locator.XPath, "//a[text()='Browse for files']");

        private readonly ElementLocator
         committeeManagementmenu = new ElementLocator(Locator.XPath, "//div[@class='menuWrapper']//a[text()='Committee Management']");

        private readonly ElementLocator
          selectedCommitteeAccount = new ElementLocator(Locator.XPath, "//table[@id='customGrid']//..//tr[{0}]//a[@class='column--CommitteeDesignation']");

        private readonly ElementLocator
         tabCommuncationLog = new ElementLocator(Locator.XPath, "//a[text()='Communication Log']");

        private readonly ElementLocator
         tabAuditLog = new ElementLocator(Locator.XPath, "//a[text()=' Audit Log']");

        private readonly ElementLocator
       inputCommuncationLog = new ElementLocator(Locator.XPath, "//div[@contenteditable='true']");

        private readonly ElementLocator
      dimmerVisible = new ElementLocator(Locator.XPath, "//div[@class='ui active transition visible dimmer']");

        private readonly ElementLocator
        tagusersList = new ElementLocator(Locator.XPath, "//div[@class='tribute-container']");

        private readonly ElementLocator
        selectTagUser = new ElementLocator(Locator.XPath, "//div[@class='tribute-container']//li[{0}]");

        private readonly ElementLocator
             getIndexLogText = new ElementLocator(Locator.XPath, "//div[@class='content']//div[@class='text']");

        private readonly ElementLocator
             getLogFirstIndex = new ElementLocator(Locator.XPath, "//div[@class='content']//div[@class='text'][{0}]");

        private readonly ElementLocator
          updateSuccessMessage = new ElementLocator(Locator.CssSelector, "div.content > p");

        private readonly ElementLocator
        submitBtnCommuncationLog = new ElementLocator(Locator.XPath, "//button[@class='ui primary button mr10']");

        private readonly ElementLocator
       editCommuncationLog = new ElementLocator(Locator.XPath, "(//i[contains(@class,'pencil icon')])[1]");

        private readonly ElementLocator
      editinputCommuncationLog = new ElementLocator(Locator.XPath, "(//div[@class='commentposteditor'])[2]");

        private readonly ElementLocator
        editsubmitBtnCommuncationLog = new ElementLocator(Locator.XPath, "//button[@type='submit']");

        private readonly ElementLocator
        editSubmitButton = new ElementLocator(Locator.XPath, "//span[@class='update-cancel']//button[@class='ui primary button mr10']");

        private readonly ElementLocator
       removeCommuncationLog = new ElementLocator(Locator.XPath, "(//i[contains(@class,'delete icon')])[1]");

        private readonly ElementLocator
      btnCommunationLogPopupMessage = new ElementLocator(Locator.XPath, "(//div[@class='content'])[10]");

        private readonly ElementLocator
       communcationlogPopupOkButtron = new ElementLocator(Locator.XPath, "(//button[@class='ui primary button'])[2]");

        private readonly ElementLocator
  communicationLogUser = new ElementLocator(Locator.XPath, "(//a[@class='author'])[{0}]");

        private readonly ElementLocator
        logDateTime = new ElementLocator(Locator.XPath, "(//div[@class='metadata']/div)[{0}]");

        private readonly ElementLocator
            communicationLogDateTime = new ElementLocator(Locator.XPath, "(//div[@class='metadata']/div)[{0}]");

        private string nmcommunicationLogUser = "Communication Log User Name";
        private string nmlogText = "log Text  : {0}";
        private string nmcommunicationLogDateTime = "Communication Log DateTime";
        private string nmtabCommuncationLog = "Communication Log Tab";
        private string nminputCommuncationLog = "Communcation Log Input";
        private string nmTagUsersList = "Tag users List";
        private string nmupdateMessageCommunicationLog = "Communication Log Message";
        private string nmremoveCommuncationLog = "Delete icon for Communication Log";
        private string nmtaguser = "Communication Log - Selected Tag User ({0}) ";
        private string nmsubmitBtnCommuncationLog = "Submit Btn Communcation Log";
        private string nmEditButtonCommuncationLog = "Edit Button Communcation Log ";
        private string nmEditinputCommuncationLog = "Edit Communcation Log Input";
        private string nmbtnCommunationLogPopupMessage = "Communcation Log Popup Message";
        private string nmcommuncationlogPopupOkButtron = "communcationlog Popup Ok Button";
        private string nmdeleteBreakDependency = "Delete Break Dependency Confirmation message";
        private string nmdimmerloading = "dimmer loading";
        private string nmrenewaltaskcardName = "Renewal Task Card name";
        private string nmaddtaskbutton = "Add task Button";
        private string nmcloneTaskPopupDiscardBtn = "Clone Task Popup Discard Button";
        private string nmNoDependencyMsg = "No Tasks dependent on it";
        private string nmbreakDependencyBtn = "Break Dependency Btn";
        private string nmremoveDependencyBtn = "Remove Dependency Btn";
        private string nmdownloadicon = "Icon to download single document";
        private string nmdownloadAllicon = "Icon to download all documents";
        private string nmbrowseForFilesLink = " Browse for files anchor link";
        private string nmdependentcheckboxClicked = "Only Show Dependent Tasks checkbox checked";
        private string nmdependentcheckboxUnClick = "Only Show Dependent Tasks checkbox unchecked";
        private string nmonlyShowDependentasks = "Only Show Dependent Tasks";
        private string nmdivEditComment = "div element Edit Comment";
        private string nmisTaggedUser = "Tagged User";
        private string nmtaggeduserlists = "Communication Log - Tag Users List";
        private string nmtab = "Communication Log tab";
        private string nmallcommunicationLogDateTime = "Communication Log Dates";
        private string nmhintTextInCommentsField = "Hint text in Comments box";
        private string nmtoolTipDependentIcon = "ToolTip Dependent Card";
        private string nmaddtasksBtn = "Add Tasks Button";
        private string nmassigneeDropdown = "Assignee dropdown icon";
        private string nmAssigneeEditBtn = "Edit Button for assignee";
        private string nmcloneBtn = "Clone button";
        private string nmpopupOkBtn = "Ok Button";
        private string nmdependentIcon = "Dependent Card";
        private string nmcardInOpenDiv = "Card in Open Div : {0}";
        private string nmremainingdays = "Remaining Days";
        private string nmhintTextUnderneathUploadBox = "Hint Text Upload Box";
        private string nmcheckCardExistsUpcoming = "Upcoming Card";
        private string nmcheckCardExistsOpen = "Open Card";
        private string nmcheckCardExistsDone = "Done Card";
        private string nmgeneralInfoEditbutton = "General Edit Button";
        private string nmassigneeEditbutton = "Assignee Edit button";
        private string nminactiveTagOnTask = "Inactive Tag on Task";
        private string nminactiveTaskRenewal = "Inactive";
        private string nmcloneTaskConfirmationMsgYesBtn = "Yes button";
        private string nmcloneTaskConfirmationMsgNoBtn = "No button";
        private string nmSeachUpcomingBoxIndex = "Renewal Task Card Index : {0}";
        private string nmassigneeRenewalTask = "Assignee";
        private string nmyearDateRenewalTask = "Year Date";
        private string nmstartDateRenewalTask = "Start Date";
        private string nmendDateRenewalTask = "End Date";
        private string nmtaskIDRenewalTask = "Task ID";
        private string nmclockIconRenewalTask = "Clock Icon";
        private string nmcopyIconRenewalTask = "Copy Icon";
        private string nmeyeIconRenewalTask = "Eye Icon";
        private string nmcloneTaskIcon = "Clone Task Icon";
        private string nmRenewalTaskHeaders = "Renewal Header : {0}";
        private string nmdependantTasksColumn = "Task column displayed in dependant Task";
        private string nmsearchfeatureDropdownIcon = "Dropdown icon on search box";
        private string nmdependenciesList = "Task displayed in dependencies List";
        private string nmdependencySearchBox = "Search Box to add dependencies";
        private string nmdeleteCommentIcon = "Delete icon for Comment";
        private string nmusernameOfComment = "User name of Comment";
        private string nmclickRenewalYearTextBox = "click RenewalYear TextBox";
        private string nmselectRenewalYear = "Selecting Renewal Year";
        private string nmNoDependenciesText = "Text to validate if No Dependencies";
        private string nmnewlyaddedDependenciesTable = "Dependency list after dependants are Added";
        private string nmaddButtonAfterDependenciesadded = "Add button in  dependencies Popup Window";
        private string nminactiveDescTextBox = "TextBox to enter Reason for Making Task as Inactive";
        private string nminactivestatusChkBox = "Inactive CheckBox";
        private string nmrenewalTasksHeader = "Renewal Tasks Header";
        private string nmaddTasksPopupHeader = "Add Tasks Window Header";
        private string nmtitleTextBox = "Title Name text field";
        private string nmrenewalTaskYear = "Renewal Task Yearfield";
        private string nmstartDateSelection = "Start Date";
        private string nmendDateSelection = "End Date";
        private string nmdescriptionBox = "Description Text Area";
        private string nmassigneeTextBox = "Assignee Field";
        private string nmdependsOnTextBox = "Depends On Field";
        private string nmdependsOnAddButton = "Depends On Add Button";
        private string nmsaveBtn = "Save Button";
        private string nmrenewalTaskName = "Name of Renewal Task";
        private string nmtaskTitle = "Task Title";
        private string nmtaskId = "Task Id";
        private string nmeditBtn = "Edit Button";
        private string nmcloneTaskDocumentAttached = "Clone Task Document Attached";
        private string nmcloneTaskPopupTitle = "Clone Task Popup Title with Clone Keyword - Task ID of main task";
        private string nmreasonTextfield = "Inactive CheckBox Text Field";
        private string nmupdateBtn = "Update Button";
        private string nmsuccessfullmessage = "Record created successfully.";
        private string nmupdatedmessage = "Record updated successfully.";
        private string nmaddedWatcherSuccessMsg = "You have been successfully added to the watcher list.";
        private string nmremovedWatcherSuccessMsg = "You have been successfully removed from the watcher list.";
        private string nmCommunicationLogSuccessMsg = "Comment added successfully.";
        private string nmCommunicationLogUpdateMsg = "Comment updated successfully.";
        private string nmpopupcloseBtn = "Popup Closed Button";
        private string nmaddSortByBtn = "Sort By Button";
        private string nmaddSortEndDateBtn = "End Date Button";
        private string nmaRenewalTitleBtn = "Renewal Title Link";
        private string nmBannerStatus = "Banner Status";
        private string nmBannerTaskID = "Banner TaskId";
        private string nmBannerRenewalYear = "Banner Renewal Year";
        private string nmBannerAssignee = "Banner Assignee";
        private string nmBannerTaskOwner = "Banner Task Owner";
        private string nmBannerWatchersCount = "Banner Watchers Count";
        private string nmBannerDateTimeStampCount = "Banner DateTime Stamp";
        private string nmBannerStartWachingIssue = "Banner - Start Watching this Issue";
        private string nmBannerStopWachingIssue = "Banner - Stop Watching this Issue";
        private string nmStartWachingIssueClass = "eye icon";
        private string nmStopWachingIssueClass = "eye slash icon";
        private string nmStartWachingErrorMessage = "Watching Issue Class";
        private string nmRenewalTaskSearchBox = "Renewal Task Search Box";
        private string nmSearchIcon = "Search Icon";
        private string nmAuditLogtab = "Audit Log tab";
        private string nmremoveDocument = "Remove document";
        private string nmdeleteYesBtn = "Yes button on Delete Popup";
        private string nmsystemGeneratedRenewalTaskID = "system Generated Renewal Task ID";
        private string nmBannerSetReminder = "Banner - Set Reminder";
        private string nmSetReminderClass = "clock outline icon";
        private string nmSetReminderErrorMessage = "Set Reminder Class";
        private string nmBannerCloneTask = "Banner - Clone Task";
        private string nmCloneTaskClass = "copy outline icon";
        private string nmCloneTaskErrorMessage = "Clone Task Class";
        private string nmrenewalSearchHint = "Renewal Task Hint Text (Search Task by Task ID, Title, Assignee)";
        private string nmAllTaskTypeDropdown = "Renewal Task Type Dropdown";
        private string nmrenewalSearchTask = "Renewal Task Search";
        private string nmexportExcelBtn = "Export Button";
        private string nmrenewalSearchTaskBtn = "Renewal Task Search Button";
        private string nmaddrenewalDependent = "Renewal Task Dependent";
        private string nmrenewalDependentSelectionItem = "Renewal Dependent Selection Item";
        private string nmnoWatchersText = "No watchers to display";
        private string nmViewWatchers = "View Watchers";
        private string nmsearchWatcherByUserId = "Search Watcher By UserId";
        private string nmsearchWatcherDropdownIcon = "Search Watcher Dropdown icon";
        private string nmaddWatcherIcon = "Add Watcher Icon";
        private string nmwatcherIcon = "Banner - Watcher Icon";
        private string nmWatchersCount = "Watchers Count";
        private string nmwatchersSubHeading = "Watchers Sub Heading";
        private string nmSectionRenewalTask = "Section Renewal Task";
        private string nmFromDate = "From Date Field";
        private string nmToDate = "To Date Field";
        private string nmEditCommentIcon = "Edit icon for Comment";
        private string nmupdateCommunicationLog = "Update Communication Log";
        private string nmassigneeDropDownOptions = "List of Users in assignee dropdown";
        private string nmassigneeDropDownicon = "Assignee dropdown icon";
        private string nmreminderDateTextbox = "Textbox for ReminderDate";
        private string nmsetButton = "Set Button for Reminder";
        private string nmdivloader = "div loading";
        private string nmadvancedsearch = "Advanced Search";
        private string nmyeardaterenewalTask = "Year Date Renewal Task";
        private string nmdownloaddocument = "Download Document";
        private string nmadvsearchYearDropDown = "dropdown icon for year in Adv search";
        private string nmadvsearchBarDropDown = "dropdown icon for Adv search";
        private string nmadvsearchSelectYearDelete = "Delete icon for Year";
        private string nmadvsearchSelectYear = "Year textbox in Adv search";
        private string nmadvsearchSearchButton = "Adv search button";

        public InternalStaffRenewalTasksPage(DriverContext driverContext)
            : base(driverContext)
        {
        }

        public void IscommunicationLogUserNameVisibleByIndex(string index)
        {
            this.Driver.IsElementVisible(this.communicationLogUser.Format(index), this.nmcommunicationLogUser);
        }

        public void IscommunicationLogDescriptionVisible(string expected, int index)
        {
            string text = this.Driver.GetTextForSelectedElementfromList(this.getIndexLogText, string.Format(this.nmlogText, expected), index);
            Assert.AreEqual(expected, text, "Expected text is not matching with return text from element");
        }

        public void IscommunicationLogDateTimeVisibleByIndex(string index)
        {
            this.Driver.IsElementVisible(this.communicationLogDateTime.Format(index), this.nmcommunicationLogDateTime);
        }

        public void IsUserIsAbleToClickOnAdvSearchDropDownIconButton()
        {
            this.Driver.WaitUntilElementIsFound(this.advsearchBarDropDown, BaseConfiguration.LongTimeout);
            var webElement = this.Driver.GetElement(this.advsearchBarDropDown);
            this.Driver.JavaScriptClick(webElement, this.nmadvsearchBarDropDown);
        }

        public void IsUserIsAbleToClickOnAdvSearchYearDropDownIconButton()
        {
            this.Driver.WaitUntilElementIsFound(this.advsearchYearDropDown, BaseConfiguration.LongTimeout);
            var webElement = this.Driver.GetElement(this.advsearchYearDropDown);
            this.Driver.JavaScriptClick(webElement, this.nmadvsearchYearDropDown);
        }

        public void IsUserIsAbleToClickOnAdvSelectedYear(string year)
        {
            this.Driver.WaitUntilElementIsFound(this.advsearchSelectYear.Format(year), BaseConfiguration.LongTimeout);
            var webElement = this.Driver.GetElement(this.advsearchSelectYear.Format(year));
            this.Driver.JavaScriptClick(webElement, this.nmadvsearchSelectYear);
        }

        public void IsUserIsAbleToClickOnAdvsearchSelectYearDelete()
        {
            this.Driver.WaitUntilElementIsFound(this.advsearchSelectYearDelete, BaseConfiguration.LongTimeout);
            var webElement = this.Driver.GetElement(this.advsearchSelectYearDelete);
            this.Driver.JavaScriptClick(webElement, this.nmadvsearchSelectYearDelete);
        }

        public string IsUserIsAbleToClickOnadvsearchSearchButton()
        {
            this.Driver.WaitUntilElementIsNoLongerFound(this.loadbutton, BaseConfiguration.LongTimeout, this.nmadvancedsearch);
            this.Driver.WaitUntilElementIsFound(this.advsearchSearchButton, BaseConfiguration.LongTimeout);
            this.Driver.WaitUntilElementIsFound(this.advsearchSelectedYear, BaseConfiguration.LongTimeout);
            var selectedYear = this.Driver.GetText(this.advsearchSelectedYear);
            var webElement = this.Driver.GetElement(this.advsearchSearchButton);
            this.Driver.JavaScriptClick(webElement, this.nmadvsearchSearchButton);
            return selectedYear;
        }

        public void IsUserIsAbleToValidateDateAsperSearch(string yearname)
        {
            this.Driver.WaitUntilElementIsNoLongerFound(this.loadbutton, BaseConfiguration.LongTimeout, this.nmyeardaterenewalTask);
            var items = this.Driver.GetElements(this.yearDateRenewalTask);
            bool exist = items.All(x => x.Text == yearname);
            if (exist == true)
            {
                DriverContext.ExtentStepTest.Log(LogStatus.Pass, "To verify " + yearname + " is visible under each card ", yearname + " is visible successfully under each card");
                Logger.Info(yearname + " is visible successfully under each card");
            }
            else
            {
                Assert.IsFalse(exist);
            }
        }

        public void IsnoDependencyMessageAvaible(bool status, string expectedMessage)
        {
            this.Driver.WaitUntilElementIsNoLongerFound(this.dimmerVisible, BaseConfiguration.LongTimeout, this.nmNoDependencyMsg);
            if (status == true)
            {
                Verify.That(this.DriverContext, () => Assert.IsTrue(this.Driver.IsElementPresentOrNot(this.noDependencyMsg.Format(expectedMessage), this.nmNoDependencyMsg, string.Empty)), "Verifying whether the :" + expectedMessage + ": field is displayed ", expectedMessage + ": Field is not displayed", "Field is not displayed");
            }
            else
            {
                Verify.That(this.DriverContext, () => Assert.IsFalse(this.Driver.IsElementPresentOrNot(this.noDependencyMsg.Format(expectedMessage), this.nmNoDependencyMsg, string.Empty)), "Verifying whether the :" + expectedMessage + ": field is displayed ", expectedMessage + ": Field is displayed", "Field is displayed");
            }
        }

        public void IsbreakDependencyBtnClicked()
        {
            this.Driver.WaitUntilElementIsFound(this.breakDependencyBtn, BaseConfiguration.LongTimeout);
            var webElement = this.Driver.GetElement(this.breakDependencyBtn);
            this.Driver.JavaScriptClick(webElement, this.nmbreakDependencyBtn);
        }

        public void IsmsgBreakDependencyDisplayed(string expected)
        {
            this.Driver.WaitUntilElementIsFound(this.msgBreakDependency, BaseConfiguration.LongTimeout);
            this.Driver.IsExpectedTextMatchWithActualText(this.msgBreakDependency, expected, string.Empty);
        }

        public void IsDocumentDownloadedOnClicked()
        {
            try
            {
                Thread.Sleep(5000);
                string filePath = TestContext.CurrentContext.TestDirectory + "\\" + BaseConfiguration.DownloadFolder;
                int getFileCount = FilesHelper.GetFilesOfGivenType(filePath, FileType.Pdf).Count;
                if (getFileCount <= 0)
                {
                    Assert.IsTrue(false, "No files downloaded to the specified location" + filePath);
                }
                else
                {
                    string[] files = Directory.GetFiles(filePath);
                    string filename = Path.GetFileName(files[0]);
                    Logger.Error("File downloaded , file name : " + filename);
                    DriverContext.ExtentStepTest.Log(LogStatus.Pass, "File downloaded , file name : " + filename);
                }
            }
            catch (Exception ex)
            {
                Logger.Error("Exception occured while downloading file : " + ex.ToString());
                DriverContext.ExtentStepTest.Log(LogStatus.Fail, "Exception occured while downloading file");
            }
        }

        public void IsUserIsAbleToClickRemoveDependencyTask()
        {
            var by = this.removeDependencyTask.ToBy();
            var webElement = this.Driver.FindElement(by);
            this.Driver.JavaScriptClick(webElement, this.nmremoveDependencyBtn);
            this.Driver.WaitForPageLoad();
        }

        public void IsUserIsAbleToClickOnDocumentDownload()
        {
            this.Driver.WaitUntilElementIsNoLongerFound(this.loadbutton, BaseConfiguration.LongTimeout, this.nmdownloaddocument);
            this.Driver.WaitUntilElementIsFound(this.downloadDocument, BaseConfiguration.LongTimeout);
            var webElement = this.Driver.GetElement(this.downloadDocument);
            this.Driver.JavaScriptClick(webElement, this.nmdownloadicon);
        }

        public void IsUserIsAbleToClickOnAllDocumentDownload()
        {
            this.Driver.WaitUntilElementIsFound(this.downloadDocumentAll, BaseConfiguration.LongTimeout);
            var webElement = this.Driver.GetElement(this.downloadDocumentAll);
            this.Driver.JavaScriptClick(webElement, this.nmdownloadAllicon);
        }

        public void IsUserIsAbleToViewPopupExportExcelIconClicked(string expectedText)
        {
            this.Driver.WaitForPageLoad();
            this.Driver.IsExpectedTextMatchWithActualText(this.popUpMsgOnExportExcel, expectedText);
        }

        public void IsExportExcelFileExcelColumnCount()
        {
            try
            {
                Thread.Sleep(4000);
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
                    DataSet getExcelColCount = FilesHelper.ReadExcelFileReturnTable(files[0]);
                    int columnCount = 0;
                    if (getExcelColCount != null && getExcelColCount.Tables.Count > 0)
                    {
                        columnCount = getExcelColCount.Tables[0].Columns.Count;
                    }

                    Assert.GreaterOrEqual(columnCount, 1);
                    Logger.Error("File downloaded , excel file name : " + filename + " Column Count :" + columnCount);
                    DriverContext.ExtentStepTest.Log(LogStatus.Pass, "File downloaded , excel file name : " + filename);
                }
            }
            catch (Exception ex)
            {
                Logger.Error("Exception occured while downloading excel file : " + ex.ToString());
                DriverContext.ExtentStepTest.Log(LogStatus.Fail, "Exception occured while downloading excel file");
            }
        }

        public void IshintTextUnderneathUploadBoxVisible(string text)
        {
            this.Driver.WaitUntilElementIsFound(this.hintTextUnderneathUploadBox.Format(text), BaseConfiguration.LongTimeout);
            this.Driver.IsElementVisible(this.hintTextUnderneathUploadBox.Format(text), this.nmhintTextUnderneathUploadBox);
        }

        public void IsUserIsAbleToClickOnRenewalTaskHomePageDropDownList()
        {
            this.Driver.WaitUntilElementIsFound(this.renewaltaskHomePageDropdownXPath, BaseConfiguration.LongTimeout);
            var webElement = this.Driver.GetElement(this.renewaltaskHomePageDropdownXPath);
            this.Driver.JavaScriptClick(webElement, this.nmAllTaskTypeDropdown);
        }

        public void IsCardOverDueIndicatorInOpenVisible(string expectedColorchrome, string expectedColorFirefox)
        {
            this.Driver.WaitUntilElementIsFound(this.overDueIndicator, BaseConfiguration.LongTimeout);
            var webElement = this.Driver.GetElement(this.overDueIndicator);
            string color = webElement.GetCssValue("border-top-color");
            if (WebDriverExtensions.GetBrowerName() == WebDriverExtensions.Firefox)
            {
                Assert.AreEqual(expectedColorFirefox, color, "Expected Color:" + expectedColorFirefox + " is not maching to current element");
            }
            else if (WebDriverExtensions.GetBrowerName() == WebDriverExtensions.Chrome)
            {
                Assert.AreEqual(expectedColorchrome, color, "Expected Color:" + expectedColorchrome + " is not maching to current element");
            }
        }

        public void IsCardOverDueIndicatorInDoneVisible(string expectedColorchrome, string expectedColorFirefox)
        {
            this.Driver.WaitUntilElementIsFound(this.doneColorIndicator, BaseConfiguration.LongTimeout);
            var webElement = this.Driver.GetElement(this.doneColorIndicator);
            string color = webElement.GetCssValue("border-top-color");
            if (WebDriverExtensions.GetBrowerName() == WebDriverExtensions.Firefox)
            {
                Assert.AreNotEqual(expectedColorFirefox, color, "Expected Color:" + expectedColorFirefox + " is not maching to current element");
            }
            else if (WebDriverExtensions.GetBrowerName() == WebDriverExtensions.Chrome)
            {
                Assert.AreNotEqual(expectedColorchrome, color, "Expected Color:" + expectedColorchrome + " is not maching to current element");
            }
        }

        public void IsTaskTypeClickableInRenewalTaskDrpDwn(string option)
        {
            this.Driver.WaitUntilElementIsFound(this.renewaltaskHomePageDropDownOptions, BaseConfiguration.LongTimeout);
            this.Driver.IsElementClickableFromListofElementWithTextJavaScript(this.renewaltaskHomePageDropDownOptions, option);
            System.Threading.Thread.Sleep(2000);
        }

        public void IsUploadAddDocument(string name)
        {
            Thread.Sleep(1000);
            this.Driver.UploadFile(this.uploadbutton, name);
        }

        public void IspopupBtn1AssigneeClicked()
        {
            this.Driver.WaitUntilElementIsFound(this.popupBtn1Assignee, BaseConfiguration.LongTimeout);
            var webElement = this.Driver.GetElement(this.popupBtn1Assignee);
            this.Driver.JavaScriptClick(webElement, this.nmpopupOkBtn);
            this.Driver.WaitUntilElementIsNoLongerFound(this.successmsg, BaseConfiguration.LongTimeout, this.nmsuccessfullmessage);
        }

        public void IsCloneBtnAssigneeClicked()
        {
            this.Driver.WaitUntilElementIsFound(this.cloneBtnAssignee, BaseConfiguration.LongTimeout);
            var webElement = this.Driver.GetElement(this.cloneBtnAssignee);
            this.Driver.JavaScriptClick(webElement, this.nmcloneBtn);
            Thread.Sleep(2000);
        }

        public void IsAssigneeStatusDropDownListClicked()
        {
            this.Driver.WaitUntilElementIsFound(this.assigneeDropdownXPath, BaseConfiguration.LongTimeout);
            var webElement = this.Driver.GetElement(this.assigneeDropdownXPath);
            this.Driver.JavaScriptClick(webElement, this.nmassigneeDropdown);
        }

        public void PopUpContent1Assignee(string expected)
        {
            this.Driver.WaitUntilElementIsFound(this.popupMessage1Assignee.Format(expected), BaseConfiguration.LongTimeout);
            this.Driver.IsExpectedTextMatchWithActualText(this.popupMessage1Assignee.Format(expected), expected);
        }

        public void IsAssigneeStatusSelectedTextClickableDrpDwn(string selectedText)
        {
            this.Driver.WaitUntilElementIsFound(this.assigneeDropDownOptions, BaseConfiguration.LongTimeout);
            this.Driver.IsElementClickableFromListofElementWithTextJavaScript(this.assigneeDropDownOptions, selectedText);
            System.Threading.Thread.Sleep(2000);
        }

        public void IsUserableToEnterTitleInSearchBoxClear()
        {
            this.Driver.WaitUntilElementIsNoLongerFound(this.successmsg, BaseConfiguration.LongTimeout, this.nmsuccessfullmessage);
            this.Driver.IsElementVisible(this.renewalSearchInput, this.nmrenewalSearchTask);
            this.Driver.EnterText(this.renewalSearchInput, string.Empty, this.nmrenewalSearchTask);
        }

        public string DragEligibleUpComingIndexWise(int firstIndex, int secondIndex)
        {
            this.Driver.WaitUntilElementIsFound(this.dragfromUpComingIndexWise.Format(firstIndex), BaseConfiguration.LongTimeout);
            this.Driver.IsElementVisible(this.dragfromUpComingIndexWise.Format(firstIndex), string.Format(this.nmSeachUpcomingBoxIndex, firstIndex));
            string text = this.Driver.GetText(this.dragfromUpComingIndexWise.Format(firstIndex));
            this.Driver.DragDropFromLocatorToLocator(this.dragfromUpComingIndexWise.Format(firstIndex), this.dragfromUpComingIndexWise.Format(secondIndex));
            return text;
        }

        public void ValidateRenewalTaskFirstRecordCardName(string expected, int firstIndex)
        {
            this.Driver.WaitUntilElementIsFound(this.dragfromUpComingIndexWise.Format(firstIndex), BaseConfiguration.LongTimeout);
            this.Driver.IsElementVisible(this.dragfromUpComingIndexWise.Format(firstIndex), string.Format(this.nmSeachUpcomingBoxIndex, firstIndex));
            this.Driver.IsExpectedTextMatchWithActualText(this.dragfromUpComingIndexWise.Format(firstIndex), expected);
        }

        public void DragEligibleToOpenFromUpComing()
        {
            this.Driver.DragDropFromLocatorToLocator(this.dragfromUpComing, this.dragToOpen);
        }

        public void DragEligibleToOpenToDone()
        {
            this.Driver.DragDropFromLocatorToLocator(this.dragFromOpen, this.dragToDone);
            this.Driver.WaitUntilElementIsFound(this.nobuttonPopup, BaseConfiguration.LongTimeout);
            var webElement = this.Driver.GetElement(this.nobuttonPopup);
            this.Driver.JavaScriptClick(webElement, this.nmNoDependencyMsg);
        }

        public void IsAssigneeRenewalTaskVisible()
        {
            this.Driver.WaitUntilElementIsFound(this.assigneeRenewalTask, BaseConfiguration.LongTimeout);
            this.Driver.IsElementVisible(this.assigneeRenewalTask, this.nmassigneeRenewalTask);
        }

        public void IsYearDateRenewalTaskVisible()
        {
            this.Driver.WaitUntilElementIsFound(this.yearDateRenewalTask, BaseConfiguration.LongTimeout);
            this.Driver.IsElementVisible(this.yearDateRenewalTask, this.nmyearDateRenewalTask);
        }

        public void IsStartDateRenewalTaskVisible()
        {
            this.Driver.WaitUntilElementIsFound(this.startDateRenewalTask, BaseConfiguration.LongTimeout);
            this.Driver.IsElementVisible(this.startDateRenewalTask, this.nmstartDateRenewalTask);
        }

        public void IsEndDateRenewalTaskVisible()
        {
            this.Driver.WaitUntilElementIsFound(this.endDateRenewalTask, BaseConfiguration.LongTimeout);
            this.Driver.IsElementVisible(this.endDateRenewalTask, this.nmendDateRenewalTask);
        }

        public void IsTaskIDRenewalTaskVisible()
        {
            this.Driver.WaitUntilElementIsFound(this.taskIDRenewalTask, BaseConfiguration.LongTimeout);
            this.Driver.IsElementVisible(this.taskIDRenewalTask, this.nmtaskIDRenewalTask);
        }

        public void IsClockIconRenewalTaskVisible()
        {
            this.Driver.CheckElementPresentOrNot(this.clockIconRenewalTask, this.nmclockIconRenewalTask, string.Empty);
        }

        public void IsCopyIconRenewalTaskVisible()
        {
            this.Driver.CheckElementPresentOrNot(this.copyIconRenewalTask, this.nmcopyIconRenewalTask, string.Empty);
        }

        public void IsEyeIconRenewalTaskVisible()
        {
            this.Driver.CheckElementPresentOrNot(this.eyeIconRenewalTask, this.nmeyeIconRenewalTask, string.Empty);
        }

        public void IsattachmentIconRenewalTaskVisible()
        {
            this.Driver.CheckElementPresentOrNot(this.attachmentIcon, this.nmeyeIconRenewalTask, string.Empty);
        }

        public void IsDependentIconVisible(string text)
        {
            this.Driver.CheckElementPresentOrNot(this.dependentIcon, this.nmdependentIcon, string.Empty);
            this.Driver.CheckElementPresentOrNot(this.toolTipDependentIcon.Format(text), string.Format(this.nmtoolTipDependentIcon, text), string.Empty);
        }

        public void IsInactiveRenewalTaskVisible()
        {
            this.Driver.CheckElementPresentOrNot(this.inactiveTaskRenewal, this.nminactiveTaskRenewal, string.Empty);
        }

        public void IsRemainingDaysRenewalTaskValidateVisible()
        {
            this.Driver.IsElementVisible(this.remainingdays, this.nmremainingdays);
            string getremainingDays = this.Driver.GetText(this.remainingdays);
            getremainingDays = getremainingDays.Substring(0, 2);
            this.Driver.IsElementVisible(this.endDateRenewalTask, this.nmendDateRenewalTask);
            string enddate = this.Driver.GetText(this.endDateRenewalTask);
            System.TimeSpan diffResult = Convert.ToDateTime(enddate).Subtract(DateTime.Now.Date);
            double diff = diffResult.TotalDays;
            Assert.AreEqual(getremainingDays.Trim(), diff.ToString());
        }

        public void IsCardPresentInOpenDiv(string expectedText)
        {
            this.Driver.IsElementVisible(this.cardInOpenDiv.Format(expectedText), string.Format(this.nmcardInOpenDiv, expectedText));
        }

        public void IsGeneralEditButtonRenewalTaskVisible()
        {
            bool returnGeneralEditButtonVisible = this.Driver.IsElementPresentOrNot(this.generalInfoEditbutton, this.nmgeneralInfoEditbutton, string.Empty);
            if (!returnGeneralEditButtonVisible)
            {
                Assert.IsFalse(returnGeneralEditButtonVisible);
            }
        }

        public void IscheckCardExistsUpcomingRenewalTaskVisible()
        {
            this.Driver.IsElementPresentOrNot(this.checkCardExistsUpcoming, this.nmcheckCardExistsUpcoming, string.Empty);
        }

        public void IscheckCardExistsOpenRenewalTaskVisible()
        {
            this.Driver.IsElementPresentOrNot(this.checkCardExistsOpen, this.nmcheckCardExistsOpen, string.Empty);
        }

        public void IscheckCardExistsDoneRenewalTaskVisible()
        {
            this.Driver.IsElementPresentOrNot(this.checkCardExistsDone, this.nmcheckCardExistsDone, string.Empty);
        }

        public void IsRenewalHeaderDisplayed(string expected)
        {
            this.Driver.WaitForPageLoad();
            this.Driver.WaitUntilElementIsFound(this.renewalTaskHeaders.Format(expected), BaseConfiguration.LongTimeout);
            this.Driver.IsElementVisible(this.renewalTaskHeaders.Format(expected), string.Format(this.nmRenewalTaskHeaders, expected));
        }

        public void IsRenewalTasksHeaderVisible()
        {
            this.Driver.WaitUntilElementIsFound(this.renewalTasksHeader, BaseConfiguration.LongTimeout);
            this.Driver.IsElementVisible(this.renewalTasksHeader, this.nmrenewalTasksHeader);
        }

        public void IsRenewalTasksHeaderVisibleAsSoftAssertion()
        {
            Verify.WaitUntilElementIsFoundWithSoftAssertion(this.DriverContext, this.renewalTasksHeader, this.nmrenewalTasksHeader, BaseConfiguration.LongTimeout);
            Verify.IsElementVisibleWithSoftAssertion(this.DriverContext, this.renewalTasksHeader, this.nmrenewalTasksHeader);
        }

        public void IsAddTasksBtnClickable()
        {
            this.Driver.WaitUntilElementIsNoLongerFound(this.loadbutton, BaseConfiguration.LongTimeout, this.nmaddtaskbutton);
            var webElement = this.Driver.GetElement(this.addTasksBtn);
            this.Driver.JavaScriptClick(webElement, this.nmaddtasksBtn);
        }

        public void IsUserAbleToViewHintTextInCommentsField(string text)
        {
            try
            {
                this.Driver.WaitUntilElementIsFound(this.hintTextInCommentsField.Format(text), BaseConfiguration.LongTimeout);
                this.Driver.IsElementVisible(this.hintTextInCommentsField.Format(text), this.nmhintTextInCommentsField);
                DriverContext.ExtentStepTest.Log(LogStatus.Pass, "Validate that the user is able to view the hint text 'Type @ to mention a person' in comments field", " Validation Successful");
                Logger.Info("User is able to view the hint text 'Type @ to mention a person' in comments field");
            }
            catch (Exception ex)
            {
                Logger.Error("Failed  Due to exception: " + ex.ToString());

                DriverContext.ExtentStepTest.Log(LogStatus.Fail, "Validate that the user is able to view the hint text 'Type @ to mention a person' in comments field", "An exception occurred while Verifying hint text in Comments box.");
                throw;
            }
        }

        public void IsAddTasksPopupVisible()
        {
            this.Driver.WaitForPageLoad();
            this.Driver.WaitUntilElementIsNoLongerFound(this.dimmerVisible, BaseConfiguration.LongTimeout, this.nmaddTasksPopupHeader);
            this.Driver.IsElementVisible(this.addTasksPopupHeader, this.nmaddTasksPopupHeader);
        }

        public string IsUserableToEnterTitle(string text)
        {
            this.Driver.WaitUntilElementIsFound(this.titleTextBox, BaseConfiguration.LongTimeout);
            this.Driver.IsElementVisible(this.titleTextBox, this.nmtitleTextBox);
            var webelement = this.Driver.GetElement(this.titleTextBox, BaseConfiguration.LongTimeout);
            this.Driver.JavaScriptClick(webelement, this.nmtitleTextBox);
            this.Driver.EnterText(this.titleTextBox, text, this.nmtitleTextBox);
            this.Driver.WaitUntilElementIsFound(this.titleTextBox, BaseConfiguration.LongTimeout);
            string titleText = this.Driver.GetValue(this.titleTextBox);
            return titleText;
        }

        public string IsUserableToEnterTitleInSearchBox(string text)
        {
            this.Driver.WaitUntilElementIsNoLongerFound(this.successmsg, BaseConfiguration.LongTimeout, this.nmsuccessfullmessage);
            this.Driver.IsElementVisible(this.renewalSearchInput, this.nmrenewalSearchTask);
            this.Driver.EnterText(this.renewalSearchInput, text, this.nmrenewalSearchTask);
            this.Driver.WaitUntilElementIsFound(this.renewalSearchInput, BaseConfiguration.LongTimeout);
            string titleText = this.Driver.GetValue(this.renewalSearchInput);
            return titleText;
        }

        public void IsUserableToSelectRenewalYear(string value)
        {
            this.Driver.IsElementVisible(this.renewalTaskYear, this.nmrenewalTaskYear);
            this.Driver.SelectByValue(this.renewalTaskYear, value);
        }

        public string IsUserAbleToSelectStartDate(string date)
        {
            this.Driver.IsElementVisible(this.startDateSelection, this.nmstartDateSelection);
            this.Driver.IsElementClickable(this.startDateSelection, this.nmstartDateSelection);
            var webelement = this.Driver.GetElement(this.startDateSelection, BaseConfiguration.LongTimeout);
            for (int i = 0; i <= 10; i++)
            {
                webelement.SendKeys(Keys.Backspace);
            }

            this.Driver.EnterText(this.startDateSelection, date, this.nmstartDateSelection);
            this.Driver.WaitUntilElementIsFound(this.startDateSelection, BaseConfiguration.LongTimeout);
            string startDate = this.Driver.GetValue(this.startDateSelection);
            return startDate.Replace('/', '-');
        }

        public string IsUserAbleToSelectEndDate(string date)
        {
            this.Driver.IsElementVisible(this.endDateSelection, this.nmendDateSelection);
            this.Driver.IsElementClickable(this.endDateSelection, this.nmendDateSelection);
            var webelement = this.Driver.GetElement(this.endDateSelection, BaseConfiguration.LongTimeout);
            for (int i = 0; i <= 10; i++)
            {
                webelement.SendKeys(Keys.Backspace);
            }

            this.Driver.EnterText(this.endDateSelection, date, this.nmendDateSelection);
            this.Driver.WaitUntilElementIsFound(this.endDateSelection, BaseConfiguration.LongTimeout);
            string endDate = this.Driver.GetValue(this.endDateSelection);
            return endDate.Replace('/', '-');
        }

        public string IsUserAbleToEnterTextInDescription(string text)
        {
            this.Driver.IsElementVisible(this.descriptionBox, this.nmdescriptionBox);
            this.Driver.EnterText(this.descriptionBox, text, this.nmdescriptionBox);
            this.Driver.WaitUntilElementIsFound(this.descriptionBox, BaseConfiguration.LongTimeout);
            string descriptionText = this.Driver.GetValue(this.descriptionBox);
            return descriptionText;
        }

        public string IsSystemGeneratedUniqueTaskID()
        {
            this.Driver.IsElementVisible(this.systemGeneratedRenewalTaskID, this.nmsystemGeneratedRenewalTaskID);
            var uniqueTaskID = this.Driver.GetText(this.systemGeneratedRenewalTaskID);
            return uniqueTaskID;
        }

        public void IsEnterTextInRenewalTaskSearchBoxAndClickSearchIcon(string title)
        {
            this.Driver.WaitUntilElementIsNoLongerFound(this.successmsg, BaseConfiguration.LongTimeout, this.nmsuccessfullmessage);
            this.Driver.WaitUntilElementIsFound(this.renewalTaskSearchBox, BaseConfiguration.LongTimeout);
            var webElement = this.Driver.GetElement(this.renewalTaskSearchBox);
            this.Driver.PageScrollUpToTop();
            this.Driver.WaitUntilElementIsFound(this.renewalTaskSearchBox, BaseConfiguration.LongTimeout);
            this.Driver.EnterText(this.renewalTaskSearchBox, title, this.nmRenewalTaskSearchBox);
            this.Driver.IsElementVisible(this.searchBtnIcon, this.nmSearchIcon);
            this.Driver.IsElementClickable(this.searchBtnIcon, this.nmSearchIcon);
        }

        public string IsUserAbleToEnterAssigneeName(string text)
        {
            this.Driver.IsElementVisible(this.assigneeTextBox, this.nmassigneeTextBox);
            var webelement = this.Driver.GetElement(this.assigneeTextBox, BaseConfiguration.LongTimeout);
            this.Driver.JavaScriptClick(webelement, this.nmassigneeTextBox);
            this.Driver.EnterText(this.assigneeTextBox, text, this.nmassigneeTextBox);
            this.Driver.WaitUntilElementIsFound(this.assigneeTextBox, BaseConfiguration.LongTimeout);
            string assigneeText = this.Driver.GetValue(this.assigneeTextBox);
            return assigneeText;
        }

        public void IsBrowseForFilesLinkClickable()
        {
            this.Driver.IsElementVisible(this.browseForFilesLink, this.nmbrowseForFilesLink);
            var webElement = this.Driver.GetElement(this.browseForFilesLink);
            this.Driver.JavaScriptClick(webElement, this.nmbrowseForFilesLink);
        }

        public void IsUserAbleToEnterTextInBrowseForFilesPopupToAttach(string name)
        {
            this.Driver.UploadFile(this.browsebutton, name);
            DriverContext.ExtentStepTest.Log(LogStatus.Pass, "To Validate that the  user can upload document", "user is able to upload document");
            Logger.Info("user is able to upload document");
        }

        public string IsUserAbleToEnterDependsOnTask(string dependsTask)
        {
            this.Driver.IsElementVisible(this.dependsOnTextBox, this.nmdependsOnTextBox);
            var webelement = this.Driver.GetElement(this.dependsOnTextBox, BaseConfiguration.LongTimeout);
            this.Driver.JavaScriptClick(webelement, this.nmdependsOnTextBox);
            this.Driver.EnterText(this.dependsOnTextBox, dependsTask, this.nmdependsOnTextBox);
            this.Driver.IsElementClickable(this.dependenciesList.Format(dependsTask), this.nmdependenciesList);
            this.Driver.WaitUntilElementIsFound(this.textInsidedependsOnTextBox, BaseConfiguration.LongTimeout);
            string dependsOnText = this.Driver.GetText(this.textInsidedependsOnTextBox);
            return dependsOnText;
        }

        public void IsUserableToSaveTask()
        {
            this.Driver.IsElementVisible(this.saveBtn, this.nmsaveBtn);
            this.Driver.IsElementClickable(this.saveBtn, this.nmsaveBtn);
        }

        public void IsUserAbleToClickDiscardButton()
        {
            this.Driver.WaitUntilElementIsNoLongerFound(this.dimmerVisible, BaseConfiguration.LongTimeout, this.nmcloneTaskPopupDiscardBtn);
            this.Driver.IsElementVisible(this.cloneTaskPopupDiscardBtn, this.nmcloneTaskPopupDiscardBtn);
            this.Driver.IsElementClickable(this.cloneTaskPopupDiscardBtn, this.nmcloneTaskPopupDiscardBtn);
        }

        public void IsUserableToSlectTasksForEditFromList(int i)
        {
            this.Driver.IsElementVisibleFromListOfElement(this.renewalTaskName, this.nmrenewalTaskName);
            this.Driver.IsElementClickableFromListOfElemetsBasedOnIndex(this.renewalTaskName, this.nmrenewalTaskName, i);
        }

        public string IsTaskTitleVisible()
        {
            this.Driver.WaitForPageLoad();
            this.Driver.IsElementVisible(this.taskTitle, this.nmtaskTitle);
            var title = this.Driver.GetText(this.taskTitle);
            return title;
        }

        public string IsUserableToGetTaskId()
        {
            this.Driver.IsElementVisible(this.taskId, this.nmtaskId);
            var taskIdNo = this.Driver.GetText(this.taskId);
            return taskIdNo;
        }

        public void IsEditButtonClickable(string name = "General Information")
        {
            this.Driver.IsElementVisible(this.editBtn.Format(name), this.nmeditBtn);
            this.Driver.IsElementClickable(this.editBtn.Format(name), this.nmeditBtn);
        }

        public void IsInactiveCheckBoxVisible(string text)
        {
            try
            {
                bool chkBoxStatus = this.Driver.IsCheckBoxChecked(this.inactiveCheckBox);
                if (chkBoxStatus)
                {
                    this.Driver.SelectCheckBoxifUnselected(this.inactiveCheckBox);
                    this.Driver.EnterText(this.reasonTextfield, text, this.nmreasonTextfield);
                }

                DriverContext.ExtentStepTest.Log(LogStatus.Pass, "To verify CheckBox Status is Unselected and then Select CheckBox ", " CheckBox is clicked successfully");
                Logger.Info(" is clicked successfully");
            }
            catch (Exception ex)
            {
                Logger.Error("Failed  Due to exception: " + ex.ToString());

                DriverContext.ExtentStepTest.Log(LogStatus.Fail, "To verify if CheckBox Status is Selected ", "An exception occurred while Verifying Status Of CheckBox ");
                throw;
            }
        }

        public void IsUserableToClickUpdateBtn()
        {
            this.Driver.IsElementVisible(this.updateBtn, this.nmupdateBtn);
            this.Driver.IsElementClickable(this.updateBtn, this.nmupdateBtn);
        }

        public void IsTaskRecordUpdatedSuccessfully(string updateSuccessmessage)
        {
            this.Driver.WaitUntilElementIsFound(this.successfullMsg, BaseConfiguration.LongTimeout);
            this.Driver.IsElementVisible(this.successfullMsg, this.nmupdatedmessage);
            this.Driver.IsExpectedTextMatchWithActualText(this.successfullMsg, updateSuccessmessage, this.nmupdatedmessage);
        }

        public void IsSuccessMessageDisplayed(string updateSuccessmessage)
        {
            this.Driver.WaitUntilElementIsFound(this.successmsg, BaseConfiguration.LongTimeout);
            this.Driver.IsElementVisible(this.successmsg, this.nmsuccessfullmessage);
            this.Driver.IsExpectedTextMatchWithActualText(this.successmsg, updateSuccessmessage, this.nmsuccessfullmessage);
        }

        public void IsSuccessMessageDisplayedForAddingWatcherInRenewalTaskDetailsPage(string successMessageAddWatcher)
        {
            Thread.Sleep(3000);
            this.Driver.IsExpectedTextMatchWithActualText(this.successmsg, successMessageAddWatcher, this.nmaddedWatcherSuccessMsg);
        }

        public void IsUpdateMessageDisplayedForAddingCommunicationLog(string updateSuccessMessage)
        {
            this.Driver.WaitUntilElementIsFound(this.successmsg, BaseConfiguration.LongTimeout);
            this.Driver.IsElementVisible(this.successmsg, this.nmCommunicationLogUpdateMsg);
            this.Driver.IsExpectedTextMatchWithActualText(this.successmsg, updateSuccessMessage, this.nmCommunicationLogUpdateMsg);
        }

        public void IsSuccessMessageDisplayedForRemovingWatcherInRenewalTaskDetailsPage(string removedWatcherSuccessMsg)
        {
            this.Driver.WaitUntilElementIsFound(this.successmsg, BaseConfiguration.LongTimeout);
            this.Driver.IsElementVisible(this.successmsg, this.nmremovedWatcherSuccessMsg);
            this.Driver.IsExpectedTextMatchWithActualText(this.successmsg, removedWatcherSuccessMsg, this.nmremovedWatcherSuccessMsg);
        }

        public void IsSuccessMessageDisplayedForCommunicationLog(string successMessage)
        {
            this.Driver.WaitUntilElementIsFound(this.successmsg, BaseConfiguration.LongTimeout);
            this.Driver.IsElementVisible(this.successmsg, this.nmCommunicationLogSuccessMsg);
            this.Driver.IsExpectedTextMatchWithActualText(this.successmsg, successMessage, this.nmCommunicationLogSuccessMsg);
        }

        public void IsCloneSuccessMessageDisplayed(string updateSuccessmessage)
        {
            this.Driver.WaitUntilElementIsFound(this.successmsg, BaseConfiguration.LongTimeout);
            this.Driver.IsElementVisible(this.successmsg, this.nmsuccessfullmessage);
            this.Driver.IsExpectedTextMatchWithActualText(this.successmsg, updateSuccessmessage, this.nmsuccessfullmessage);
        }

        public void IsUserableToSelectTaskCard()
        {
            this.Driver.WaitForPageLoad();
            this.Driver.WaitUntilElementIsFound(this.openTasksList, BaseConfiguration.LongTimeout);
            IList<IWebElement> items = this.Driver.GetElements(this.openTasksList);
            items.First().Click();
        }

        public void IsGeneralInfoEditButtonClickable()
        {
            this.Driver.WaitUntilElementIsNoLongerFound(this.loadbutton, BaseConfiguration.LongTimeout, this.nmgeneralInfoEditbutton);
            this.Driver.WaitUntilElementIsFound(this.generalInfoEditbutton, BaseConfiguration.LongTimeout);
            var webElement = this.Driver.GetElement(this.generalInfoEditbutton);
            this.Driver.JavaScriptClick(webElement, this.nmgeneralInfoEditbutton);
        }

        public void IsAssigneeEditButtonClickable()
        {
            this.Driver.WaitUntilElementIsNoLongerFound(this.loadbutton, BaseConfiguration.LongTimeout, this.nmassigneeEditbutton);
            this.Driver.WaitUntilElementIsFound(this.assigneeEditbutton, BaseConfiguration.LongTimeout);
            var webElement = this.Driver.GetElement(this.assigneeEditbutton);
            this.Driver.JavaScriptClick(webElement, this.nmAssigneeEditBtn);
        }

        public void IsGeneralInfoCancelButtonClickable()
        {
            this.Driver.WaitForPageLoad();
            this.Driver.WaitUntilElementIsFound(this.generalInfoCancelbutton, BaseConfiguration.LongTimeout);
            var webElement = this.Driver.GetElement(this.generalInfoCancelbutton);
            this.Driver.JavaScriptClick(webElement, this.nmgeneralInfoEditbutton);
        }

        public void IsInactiveCheckBoxClickable()
        {
            if (WebDriverExtensions.GetBrowerName() == WebDriverExtensions.Firefox)
            {
                this.Driver.IsElementVisible(this.inactivestatusChkBoxText, this.nminactivestatusChkBox);
                this.Driver.IsElementClickable(this.inactivestatusChkBoxText, this.nminactivestatusChkBox);
            }
            else if (WebDriverExtensions.GetBrowerName() == WebDriverExtensions.Chrome)
            {
                this.Driver.MouseOverOnWebElementAndClick(this.inactivestatusChkBoxText, "Inactive this Task");
                var webElement = this.Driver.GetElement(this.inactivestatusChkBoxText);
                this.Driver.JavaScriptClick(webElement, this.nminactivestatusChkBox);
            }
        }

        public void IsInactiveTagOnTaskVisible()
        {
            this.Driver.PageScrollUpToTop();
            bool returnInactiveTagOnTaskVisible = this.Driver.IsElementPresentOrNot(this.inactiveTagOnTask, this.nminactiveTagOnTask, string.Empty);
            if (!returnInactiveTagOnTaskVisible)
            {
                Assert.IsFalse(returnInactiveTagOnTaskVisible);
            }
            else
            {
                DriverContext.ExtentStepTest.Log(LogStatus.Pass, "To Validate that the user can view Inactive status for the InActive Task", "user can view Inactive status for the InActive Task");
                Logger.Info("user can view Inactive status for the InActive Task");
            }
        }

        public void AreWatchersRemovedForInActiveTask()
        {
            Thread.Sleep(3000);
            bool returnWatchersForInActiveTaskVisible = this.Driver.IsElementPresentOrNot(this.watcherslabel, this.nmViewWatchers, string.Empty);
            if (!returnWatchersForInActiveTaskVisible)
            {
                DriverContext.ExtentStepTest.Log(LogStatus.Pass, "Validate that Watchers are removed for the inactive task in the task banner on task details page", "Watchers are removed for the inactive task in the task banner on task details page");
                Logger.Info("Watchers are removed for the inactive task in the task banner on task details page");
            }
            else
            {
                Assert.IsFalse(returnWatchersForInActiveTaskVisible);
            }
        }

        public void IsInactiveCheckBoxVisible()
        {
            this.Driver.IsElementTextDisplayed(this.inactivestatusChkBoxText, "Inactive this Task");
        }

        public void IsCloseButtonClickable(string name = "General Information")
        {
            this.Driver.WaitUntilElementIsFound(this.closeBtn.Format(name), BaseConfiguration.LongTimeout);
            var webElement = this.Driver.GetElement(this.closeBtn.Format(name));
            this.Driver.JavaScriptClick(webElement, this.nmcloneBtn);
        }

        public void EnterTextInInactiveDescriptionBox(string text)
        {
            this.Driver.EnterText(this.inactiveDescTextBox, text, this.nminactiveDescTextBox);
        }

        public void IsupdateBtnClickableforInactiveTask()
        {
            this.Driver.WaitUntilElementIsFound(this.updateBtnForInactiveTask, BaseConfiguration.LongTimeout);
            var webElement = this.Driver.GetElement(this.updateBtnForInactiveTask);
            this.Driver.JavaScriptClick(webElement, this.nmupdateBtn);
        }

        public void IsupdateBtnClickableforAssignee()
        {
            this.Driver.WaitUntilElementIsFound(this.updateBtnForInactiveTask, BaseConfiguration.LongTimeout);
            var webElement = this.Driver.GetElement(this.updateBtnForInactiveTask);
            this.Driver.JavaScriptClick(webElement, this.nmupdateBtn);
        }

        public void IsWatchersDetailsVisible()
        {
            this.Driver.IsElementTextDisplayed(this.watcherslabel, this.nmViewWatchers);
        }

        public void IsViewWatchersPopupClickable()
        {
            this.Driver.WaitUntilElementIsFound(this.watcherslabel, BaseConfiguration.MediumTimeout);
            System.Threading.Thread.Sleep(3000);
            this.Driver.IsElementClickable(this.watcherslabel, this.nmViewWatchers);
        }

        public void IsWatcherEyeIconClickable()
        {
            this.Driver.WaitUntilElementIsNoLongerFound(this.dimmerVisible, BaseConfiguration.LongTimeout, this.nmwatcherIcon);
            this.Driver.WaitUntilElementIsFound(this.watcherEyeIcon, BaseConfiguration.LongTimeout);
            var webElement = this.Driver.GetElement(this.watcherEyeIcon);
            this.Driver.JavaScriptClick(webElement, this.nmwatcherIcon);
        }

        public void IsUserAbleToClickEyeIconToAddUserInRenewalTaskDetailsPage()
        {
            this.Driver.WaitUntilElementIsFound(this.eyeIconRenewalTask, BaseConfiguration.LongTimeout);
            var webElement = this.Driver.GetElement(this.eyeIconRenewalTask);
            this.Driver.JavaScriptClick(webElement, this.nmeyeIconRenewalTask);
        }

        public void IsRenewalTaskEyeIconClickable()
        {
            this.Driver.IsElementVisible(this.remainingdays, this.nmremainingdays);
            this.Driver.MouseOverOnWebElement(this.remainingdays);
            this.Driver.IsElementClickable(this.eyeIconRenewalTask, this.nmeyeIconRenewalTask);
        }

        public void IsCloneTaskIconClickable()
        {
            this.Driver.IsElementVisible(this.systemGeneratedRenewalTaskID, this.nmsystemGeneratedRenewalTaskID);
            this.Driver.MouseOverOnWebElement(this.systemGeneratedRenewalTaskID);
            this.Driver.IsElementClickable(this.cloneTaskIcon, this.nmcloneTaskIcon);
        }

        public void IsDependsOnAddButtonClickable()
        {
            this.Driver.WaitUntilElementIsNoLongerFound(this.loadbutton, BaseConfiguration.LongTimeout, this.nmdependsOnAddButton);
            this.Driver.WaitUntilElementIsFound(this.dependsOnAddButton, BaseConfiguration.LongTimeout);
            var webElement = this.Driver.GetElement(this.dependsOnAddButton);
            this.Driver.JavaScriptClick(webElement, this.nmdependsOnAddButton);
        }

        public void IsDependencySearchBoxClickable()
        {
            this.Driver.WaitUntilElementIsFound(this.dependencySearchBox, BaseConfiguration.LongTimeout);
            var webElement = this.Driver.GetElement(this.dependencySearchBox);
            this.Driver.JavaScriptClick(webElement, this.nmdependencySearchBox);
        }

        public void IsDependenciesSelectablefromList(string name)
        {
            this.Driver.WaitUntilElementIsFound(this.dependencySearchBox, BaseConfiguration.LongTimeout);
            this.Driver.EnterText(this.dependencySearchBox, name, this.nmdependencySearchBox);
            this.Driver.IsElementClickable(this.dependenciesList.Format(name), this.nmdependenciesList);
        }

        public void IsAddButtonClickableInADDDependenciesPopupWindow()
        {
            this.Driver.IsElementClickable(this.addButtonAfterDependenciesadded, this.nmaddButtonAfterDependenciesadded);
        }

        public void IsAddedDependenciesvisible()
        {
            bool value = this.Driver.IsElementVisibleWithSoftAssertion(this.newlyaddedDependenciesTable, this.nmnewlyaddedDependenciesTable);
            Assert.IsTrue(value);
        }

        public void IsDependenciesNOTVisible()
        {
            try
            {
                bool value = this.Driver.IsElementVisibleWithSoftAssertion(this.noDependenciesText, this.nmNoDependenciesText);
                Assert.IsTrue(value);

                DriverContext.ExtentStepTest.Log(LogStatus.Pass, "To verify Dependencies options Visible ", " Dependencies option is Not visible successfully");
                Logger.Info("Dependencies not visible successfully");
            }
            catch (Exception ex)
            {
                Logger.Error("Failed  Due to exception: " + ex.ToString());

                DriverContext.ExtentStepTest.Log(LogStatus.Fail, "To verify if Dependencies options is visible", "An exception occurred while Verifying Dependencies visible");
                throw;
            }
        }

        public string IsUserableToaddNewTask(string title, string date, string descText, string assigneeName, string dependsOn = "")
        {
            this.Driver.WaitForPageLoad();
            string taskid = string.Empty;
            this.IsAddTasksBtnClickable();
            this.IsUserableToEnterTitle(title);
            this.IsUserAbleToSelectStartDate(date);
            this.IsUserAbleToSelectEndDate(date);
            this.IsUserAbleToEnterTextInDescription(descText);
            this.IsUserAbleToEnterAssigneeName(assigneeName);
            if (dependsOn != string.Empty)
            {
                taskid = this.IsUserAbleToEnterDependsOnTask(dependsOn);
            }

            this.IsUserableToSaveTask();
            return taskid;
        }

        public void IsDefaultRenewalYearDisplayed()
        {
            var text = this.Driver.GetText(this.defaultRenewalYear);
            Assert.IsNotNull(text);
        }

        public void IsRenewalDropDownClickable()
        {
            this.Driver.IsElementClickable(this.clickRenewalYearTextBox, this.nmclickRenewalYearTextBox);
            IList<IWebElement> lstElements = this.Driver.GetElements(this.selectRenewalYear);
            int countOfYears = lstElements.Count;
            Assert.That(countOfYears, Is.GreaterThanOrEqualTo(1));
        }

        public void SelectYearfromList()
        {
            this.Driver.IsElementClickable(this.selectRenewalYear, this.nmselectRenewalYear);
        }

        public void IsTitleErrorMessageDisplayed(string expected)
        {
            this.IsErrorMessageDisplayed(expected, this.titleErrorMessage);
        }

        public void IsEndDateErrorMessageDisplayed(string expected)
        {
            this.IsErrorMessageDisplayed(expected, this.endDateErrorMessage);
        }

        public void IsFileFormatErrorMessageDisplayed(string expected)
        {
            this.IsErrorMessageDisplayed(expected, this.fileFormatErrorMessage);
        }

        public void IsStartDateErrorMessageDisplayed(string expected)
        {
            this.IsErrorMessageDisplayed(expected, this.startDateErrorMessage);
        }

        public void IsInputEndDateErrorMessageDisplayed(string expected)
        {
            this.IsErrorMessageDisplayed(expected, this.inputEndDateErrorMessage);
        }

        public void IsInputStartDateErrorMessageDisplayed(string expected)
        {
            this.IsErrorMessageDisplayed(expected, this.inputStartDateErrorMessage);
        }

        public void IsUserableToPopUpCloseTask()
        {
            this.Driver.IsElementVisible(this.popupCloseBtn, this.nmpopupcloseBtn);
            this.Driver.IsElementClickable(this.popupCloseBtn, this.nmpopupcloseBtn);
        }

        public void IsUpcomingStatusLaneTaskCountVisible()
        {
            this.Driver.WaitUntilElementIsFound(this.upcomingTasksCount, BaseConfiguration.LongTimeout);
            var count = this.Driver.GetText(this.upcomingTasksCount);
            IList<IWebElement> lstElements = this.Driver.GetElements(this.upcomingtasksListNo);
            int cardcount = lstElements.Count;
            Console.WriteLine("Count = " + cardcount);
            Assert.AreEqual(count, cardcount);
        }

        public void IsDoneStatusLaneTaskCountVisible()
        {
            this.Driver.WaitUntilElementIsFound(this.doneDvTasksCount, BaseConfiguration.LongTimeout);
            var count = this.Driver.GetText(this.doneDvTasksCount);
            this.Driver.ScrollToWebElement(this.cards);
            IList<IWebElement> lstElements = this.Driver.GetElements(this.doneDvtasksListNo);
            int cardcount = lstElements.Count;
            Console.WriteLine("Count = " + cardcount);
            Assert.AreEqual(count, cardcount);
        }

        public void IsOpenStatusLaneTaskCountVisible()
        {
            this.Driver.WaitUntilElementIsFound(this.openDvTasksCount, BaseConfiguration.LongTimeout);
            var count = this.Driver.GetText(this.openDvTasksCount);
            this.Driver.ScrollToWebElement(this.cards);
            IList<IWebElement> lstElements = this.Driver.GetElements(this.openDvtasksListNo);
            int cardcount = lstElements.Count;
            Console.WriteLine("Count = " + cardcount);
            Assert.AreEqual(count, cardcount);
        }

        public void IsUserableToViewTaskId(string expected)
        {
            this.Driver.IsExpectedTextMatchWithActualText(this.viewTaskId, expected);
        }

        public void IsUserableToViewConfirmationPopupMessageToCloneTask(string expected)
        {
            this.Driver.IsExpectedTextMatchWithActualText(this.viewCloneTaskConfirmationMessage, expected);
            this.Driver.IsElementVisible(this.cloneTaskConfirmationMsgYesBtn, this.nmcloneTaskConfirmationMsgYesBtn);
            this.Driver.IsElementVisible(this.cloneTaskConfirmationMsgNoBtn, this.nmcloneTaskConfirmationMsgNoBtn);
        }

        public void IsUserableToViewAttachedDocumentInCloneTask(string expected)
        {
            try
            {
                this.Driver.WaitUntilElementIsFound(this.cloneTaskAttachedDocList, BaseConfiguration.LongTimeout);
                IList<IWebElement> items = this.Driver.GetElements(this.cloneTaskAttachedDocList);
                int docsCount = items.Count();
                if (docsCount != 0)
                {
                    string firstUploadedDoc = items[0].Text;
                    if (firstUploadedDoc == expected)
                    {
                        DriverContext.ExtentStepTest.Log(LogStatus.Pass, "To Validate that the  user can view attached documents while cloning task", "user can view attached documents while cloning task");
                        Logger.Info("user can view attached documents while cloning task");
                    }
                    else
                    {
                        throw new Exception("An exception occured as the user is not able to view attached documents while cloning task");
                    }
                }
                else
                {
                    throw new Exception("An exception occured as the user is not able to view attached documents while cloning task");
                }
            }
            catch (Exception ex)
            {
                DriverContext.ExtentStepTest.Log(LogStatus.Fail, "To Validate that the user can view attached documents while cloning task", "user cannot view attached documents while cloning task" + ex.ToString());
                Logger.Info("user cannot view attached documents while cloning task");
            }
        }

        public void UserNotAbleToViewAttachedDocumentInCloneTaskOnNoBtnClick(string expected)
        {
            try
            {
                bool documentAttached = this.Driver.IsElementPresentOrNot(this.cloneTaskAttachedDocList, this.nmcloneTaskDocumentAttached, string.Empty);
                if (documentAttached == false)
                {
                    DriverContext.ExtentStepTest.Log(LogStatus.Pass, "To Validate that the user is not able to view the attached document upon clicking on No button", "User is not able to view the attached document upon clicking on No button");
                    Logger.Info("User is not able to view the attached document upon clicking on No button");
                }
                else
                {
                    throw new Exception("An exception occured as the user is not able to view the changes get discarded upon clicking on No button");
                }
            }
            catch (Exception)
            {
                DriverContext.ExtentStepTest.Log(LogStatus.Fail, "To Validate that the user is not able to view the attached document upon clicking on No button", "User is able to view the attached document upon clicking on No button");
                Logger.Info("User is able to view the attached document upon clicking on No button");
            }
        }

        public void IsUserAbleToViewCloneKeywordInTitleInCloneTaskPopup(string title)
        {
            string expectedTitle = "CLONE" + " - " + title;
            this.Driver.IsElementVisible(this.cloneTaskPopupTitle.Format(expectedTitle), this.nmcloneTaskPopupTitle);
        }

        public void IsUserableToViewTaskTitle(string expected)
        {
            this.Driver.IsExpectedTextMatchWithActualText(this.viewTitle, expected);
        }

        public void IsUserableToViewStartDate(string expected)
        {
            this.Driver.IsExpectedTextMatchWithActualText(this.viewStartDate, expected);
        }

        public void IsUserableToViewEndDate(string expected)
        {
            this.Driver.IsExpectedTextMatchWithActualText(this.viewEndDate, expected);
        }

        public void IsUserableToViewRenewalYear(string expected)
        {
            this.Driver.IsExpectedTextMatchWithActualText(this.viewRenewalYear, expected);
        }

        public void IsUserableToViewDescription(string expected)
        {
            this.Driver.IsExpectedTextMatchWithActualText(this.viewDescription, expected);
        }

        public void IsUserableToViewAssignee(string expected)
        {
            this.Driver.IsExpectedTextMatchWithActualText(this.viewAssignee, expected);
        }

        public void IsUserableToViewSatus(string expected)
        {
            this.Driver.IsExpectedTextMatchWithActualText(this.viewStatus, expected);
        }

        public void IsUserableToViewTaskDetailsTitle(string title)
        {
            try
            {
                string actualTitle = this.Driver.Title;
                Assert.AreEqual(actualTitle, title);
                DriverContext.ExtentStepTest.Log(LogStatus.Pass, "To verify Expected Value " + title + " Title is matching with Actual text ", "The expected title is " + title + " and  actual title is " + actualTitle + " matching successfully");
                Logger.Info("Expected text " + title + " and Actual text is " + actualTitle);
            }
            catch (Exception ex)
            {
                Logger.Error("Failed to verify text on " + title + "with Actual Text Due to exception: " + ex.ToString());

                DriverContext.ExtentStepTest.Log(LogStatus.Fail, "To Verify text on " + title + "with Actual Text ", "An exception occurred while finding text on " + title);
                throw;
            }
        }

        public void GetRenewalTasksPageTitle(string expectedTitle)
        {
            try
            {
                string actualTitle = this.Driver.Title;
                Assert.AreEqual(expectedTitle, actualTitle);
                DriverContext.ExtentStepTest.Log(LogStatus.Pass, "To verify Page title Text  is matching with Actual title text ", "The expected Value is " + expectedTitle + " and  actual value is " + actualTitle + " matching successfully");
                Logger.Info("Expected text " + expectedTitle + " and Actual text is " + actualTitle);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void IsSortBtnClickable()
        {
            this.Driver.IsElementVisible(this.viewSortyBy, this.nmaddSortByBtn);
            this.Driver.IsElementClickable(this.viewSortyBy, this.nmaddSortByBtn);
        }

        public void IsDeleteBreakDependencyPopUpVisible()
        {
            this.Driver.IsElementVisible(this.deleteBreakDependency, this.nmdeleteBreakDependency);
            DriverContext.ExtentStepTest.Log(LogStatus.Pass, "To validate that the user is able to delete the dependency of that Task ", "User is able to delete the dependency of that Task");
            Logger.Info("User is able to delete the dependency of that Task");
        }

        public void EndDateErrorMessageDisplayed(string expectedText)
        {
            string actualText = this.Driver.GetText(this.endDateErrorMessage);
            this.Driver.IsExpectedTextMatchWithActualText(this.endDateErrorMessage, expectedText, actualText);
        }

        public void IsSortEndDateBtnClickable()
        {
            this.Driver.IsElementVisible(this.viewSortEndDate, this.nmaddSortEndDateBtn);
            this.Driver.IsElementClickable(this.viewSortEndDate, this.nmaddSortEndDateBtn);
        }

        public void IsUpcomingRenewalTaskTitleDisplayed(string expected)
        {
            Thread.Sleep(2500);
            this.Driver.WaitUntilElementIsFound(this.viewIncomingTitle.Format(expected), BaseConfiguration.LongTimeout);
            this.Driver.IsExpectedTextMatchWithActualText(this.viewIncomingTitle.Format(expected), expected);
        }

        public void IsOpenSortBtnClickable()
        {
            this.Driver.IsElementVisible(this.viewOpenSortyBy, this.nmaddSortByBtn);
            this.Driver.IsElementClickable(this.viewOpenSortyBy, this.nmaddSortByBtn);
        }

        public void IsOpenSortEndDateBtnClickable()
        {
            this.Driver.IsElementVisible(this.viewOpenSortEndDate, this.nmaddSortEndDateBtn);
            this.Driver.IsElementClickable(this.viewOpenSortEndDate, this.nmaddSortEndDateBtn);
        }

        public void IsOpenRenewalTaskTitleDisplayed(string expected)
        {
            this.Driver.WaitUntilElementIsFound(this.viewOpenTitle.Format(expected), BaseConfiguration.LongTimeout);
            this.Driver.IsExpectedTextMatchWithActualText(this.viewOpenTitle.Format(expected), expected);
        }

        public void IsOpenRenewalTaskWithDependentIconDisplayed()
        {
            this.Driver.IsElementVisible(this.dependentIcon, this.nmdependentIcon);
        }

        public void ISCheckBoxClickedRenewalTaskWithDependent(bool check)
        {
            if (check == true)
            {
                this.Driver.IsElementVisible(this.dependentcheckboxClicked, this.nmdependentcheckboxClicked);
            }
            else
            {
                this.Driver.IsElementVisible(this.dependentcheckboxUnClick, this.nmdependentcheckboxUnClick);
            }
        }

        public void IsOpenRenewalTaskTitle(string expected)
        {
            this.Driver.WaitForPageLoad();
            this.Driver.WaitUntilElementIsFound(this.viewOpenTitle.Format(expected), BaseConfiguration.LongTimeout);
            this.Driver.IsElementClickable(this.viewOpenTitle.Format(expected), expected);
            this.Driver.WaitForPageLoad();
        }

        public void IsOnlyShowDependentTaskClicked()
        {
            var webEle = this.Driver.GetElement(this.onlyShowDependentasks.Format(this.nmonlyShowDependentasks), BaseConfiguration.LongTimeout);
            this.Driver.IsElementClickable(this.onlyShowDependentasks.Format(this.nmonlyShowDependentasks), this.nmonlyShowDependentasks);
        }

        public void ClickOnDocumentRemoveButton()
        {
            var webEle = this.Driver.GetElement(this.removeDocument, BaseConfiguration.LongTimeout);
            this.Driver.JavaScriptClick(webEle, this.nmremoveDocument);
        }

        public void ClickOnDocumentRemovePopupOkButton()
        {
            var webEle = this.Driver.GetElement(this.deleteYesButton, BaseConfiguration.LongTimeout);
            this.Driver.JavaScriptClick(webEle, this.nmdeleteYesBtn);
        }

        public void IsOpenRenewalTaskTitleWithThreeDotsDisplayed(string expected)
        {
            this.GetTextFromElementSearchThreeDots(this.viewOpenTitle.Format(expected), this.nmrenewalSearchTask);
        }

        public void IsRenewalTaskTitleBtnClickable(string expected)
        {
            this.Driver.IsElementVisible(this.viewOpenTitle.Format(expected), this.nmaRenewalTitleBtn);
            this.Driver.IsElementClickable(this.viewOpenTitle.Format(expected), this.nmaRenewalTitleBtn);
        }

        public void IsBannerTitleDisplayed(string expected)
        {
            this.Driver.WaitUntilElementIsFound(this.viewBannerTitle.Format(expected), BaseConfiguration.LongTimeout);
            this.Driver.IsExpectedTextMatchWithActualText(this.viewBannerTitle.Format(expected), expected);
        }

        public void IsBannerStatusDisplayed()
        {
            this.GetTextFromElement(this.viewBannerStatus, this.nmBannerStatus);
        }

        public void IsBannerTaskIdDisplayed()
        {
            this.GetTextFromElement(this.viewBannerTaskID, this.nmBannerTaskID);
        }

        public void IsBannerRenewalYearDisplayed()
        {
            this.GetTextFromElement(this.viewBannerRenewalYear, this.nmBannerRenewalYear);
        }

        public void IsBannerAssigneeDisplayed()
        {
            this.GetTextFromElement(this.viewBannerAssignee, this.nmBannerAssignee);
        }

        public void IsBannerTaskOwnerDisplayed()
        {
            this.GetTextFromElement(this.viewBannerTaskOwner, this.nmBannerTaskOwner);
        }

        public void IsBannerWatchersCountDisplayed()
        {
            this.GetTextFromElement(this.viewBannerWatchersCount, this.nmBannerWatchersCount);
        }

        public void IsBannerDateTimeStampDisplayed()
        {
            this.GetTextFromElement(this.viewBannerDateTimeStamp, this.nmBannerDateTimeStampCount);
        }

        public void IsWatcherStampDisplayed()
        {
            this.GetClassFromElement(this.viewBannerStartWatcherIssue, this.nmStartWachingIssueClass, this.nmStopWachingIssueClass, this.nmBannerStartWachingIssue, this.nmBannerStopWachingIssue, this.nmStartWachingErrorMessage);
        }

        public void IsSetReminderDisplayed()
        {
            this.GetClassFromElementForSingleClass(this.viewBannerSetReminder, this.nmSetReminderClass, this.nmBannerSetReminder, this.nmSetReminderErrorMessage);
        }

        public void IsCloneTaskDisplayed()
        {
            this.GetClassFromElementForSingleClass(this.viewBannerCloneIssue, this.nmCloneTaskClass, this.nmBannerCloneTask, this.nmCloneTaskErrorMessage);
        }

        public void IsRenewalSearchBtnClickable()
        {
            this.Driver.IsElementVisible(this.renewalSearch, this.nmrenewalSearchTaskBtn);
            this.Driver.IsElementClickable(this.renewalSearch, this.nmrenewalSearchTaskBtn);
        }

        public void GetClassFromElementForSingleClass(ElementLocator elementLoctr, string className, string classElementText, string errorMessage)
        {
            bool checkClassAvaible = false;
            try
            {
                var getElement = this.Driver.GetElement(elementLoctr);
                var getClassName = getElement.GetAttribute("class");
                if (getClassName == className)
                {
                    checkClassAvaible = true;
                    DriverContext.ExtentStepTest.Log(LogStatus.Pass, "Element Text is :" + classElementText, "Element Text is " + classElementText + " found successfully");
                    Logger.Info("Element Text is :" + classElementText);
                }

                if (checkClassAvaible == false)
                {
                    Assert.IsTrue(checkClassAvaible);
                }
            }
            catch (Exception ex)
            {
                Logger.Error("Element Class is :" + errorMessage + " Due to exception: " + ex.ToString());
                DriverContext.ExtentStepTest.Log(LogStatus.Fail, "Failed to get Element Class : " + errorMessage, "An exception occurred while finding Element Class : " + errorMessage);
                throw;
            }
        }

        public void GetClassFromElement(ElementLocator elementLoctr, string className1, string className2, string classElementText1, string classElementText2, string errorMessage)
        {
            bool checkClassAvaible = false;
            try
            {
                var getElement = this.Driver.GetElement(elementLoctr);
                var getClassName = getElement.GetAttribute("class");
                if (getClassName == className1)
                {
                    checkClassAvaible = true;
                    DriverContext.ExtentStepTest.Log(LogStatus.Pass, "Element Text is :" + classElementText1, "Element Text is " + classElementText1 + " found successfully");
                    Logger.Info("Element Text is :" + classElementText1);
                }

                if (getClassName == className2)
                {
                    checkClassAvaible = true;
                    DriverContext.ExtentStepTest.Log(LogStatus.Pass, "Element Text is :" + classElementText2, "Element Text is " + classElementText2 + " found successfully");
                    Logger.Info("Element Text is :" + classElementText2);
                }

                if (checkClassAvaible == false)
                {
                    Assert.IsTrue(checkClassAvaible);
                }
            }
            catch (Exception ex)
            {
                Logger.Error("Element Class is :" + errorMessage + " Due to exception: " + ex.ToString());
                DriverContext.ExtentStepTest.Log(LogStatus.Fail, "Failed to get Element Class : " + errorMessage, "An exception occurred while finding Element Class : " + errorMessage);
                throw;
            }
        }

        public void IsRenewalTaskSelectionDisplayed(string expected)
        {
            this.Driver.WaitUntilElementIsNoLongerFound(this.successmsg, BaseConfiguration.LongTimeout, this.nmsuccessfullmessage);
            this.Driver.WaitUntilElementIsFound(this.renewalTaskSelection, BaseConfiguration.LongTimeout);
            this.Driver.IsExpectedTextMatchWithActualText(this.renewalTaskSelection, expected);
        }

        public void IsRenewalSearchHintDisplayed(string expected)
        {
            this.Driver.WaitUntilElementIsFound(this.renewalSearchHint.Format(expected), BaseConfiguration.LongTimeout);
            this.Driver.MouseOverOnWebElementAndClick(this.renewalSearchHint.Format(expected), this.nmrenewalSearchHint);
        }

        public void IsRenewalDepedent(string text)
        {
            this.Driver.IsElementVisible(this.renewalDependentCombo, this.nmaddrenewalDependent);
            this.Driver.IsElementClickable(this.renewalDependentCombo, this.nmaddrenewalDependent);
            this.Driver.EnterText(this.renewalDependentInput, text, this.nmaddrenewalDependent);
            Thread.Sleep(1000);
            this.IsDependOnSelectionClickable(text);
        }

        public void IsDependOnSelectionClickable(string text)
        {
            this.Driver.WaitUntilElementIsFound(this.renewalDependentSelection.Format(text), BaseConfiguration.LongTimeout);
            var webElement = this.Driver.GetElement(this.renewalDependentSelection.Format(text));
            this.Driver.JavaScriptClick(webElement, this.nmrenewalDependentSelectionItem);
        }

        public void IsRenewalTaskDependentSortDisplayed(string expected)
        {
            this.Driver.WaitUntilElementIsFound(this.renewalDependentSort, BaseConfiguration.LongTimeout);
            this.Driver.IsExpectedTextMatchWithActualText(this.renewalDependentSort, expected);
        }

        public void GetTextFromElementSearchThreeDots(ElementLocator elementLoctr, string elementName)
        {
            string actualText = string.Empty;
            try
            {
                this.Driver.WaitUntilElementIsFound(elementLoctr, BaseConfiguration.LongTimeout);
                this.Driver.IsElementVisible(elementLoctr, elementName);
                var getElement = this.Driver.GetElement(elementLoctr);
                actualText = getElement.Text;
                if (actualText.Contains("..."))
                {
                    DriverContext.ExtentStepTest.Log(LogStatus.Pass, "Element Name: " + elementName + " and Element Text is (" + actualText + ")", "The  Element Name is " + elementName + " and  Element Text is " + actualText + " matching successfully");
                    Logger.Info("Element Name: " + elementName + " and Element Text is (" + actualText + ")");
                }
            }
            catch (Exception ex)
            {
                Logger.Error("Failed to get Element Name: " + elementName + " and Element Text is not maching with condition applied (...) (" + actualText + " ) Due to exception: " + ex.ToString());
                DriverContext.ExtentStepTest.Log(LogStatus.Fail, "Failed to get Element Name: " + elementName + " and Element Text is not maching with condition applied (...) (" + actualText + " )", "An exception occurred while finding Element : " + elementName);
                throw;
            }
        }

        public void GetTextFromElement(ElementLocator elementLoctr, string elementName)
        {
            string actualText = string.Empty;
            try
            {
                this.Driver.WaitUntilElementIsFound(elementLoctr, BaseConfiguration.LongTimeout);
                this.Driver.IsElementVisible(elementLoctr, elementName);
                var getElement = this.Driver.GetElement(elementLoctr);
                actualText = getElement.Text;
                DriverContext.ExtentStepTest.Log(LogStatus.Pass, "Element Name: " + elementName + " and Element Text is (" + actualText + ")", "The  Element Name is " + elementName + " and  Element Text is " + actualText + " matching successfully");
                Logger.Info("Element Name: " + elementName + " and Element Text is (" + actualText + ")");
            }
            catch (Exception ex)
            {
                Logger.Error("Failed to get Element Name: " + elementName + " and Element Text is " + actualText + " Due to exception: " + ex.ToString());
                DriverContext.ExtentStepTest.Log(LogStatus.Fail, "Failed to get Element Name: " + elementName + " and Element Text is (" + actualText + ")", "An exception occurred while finding Element : " + elementName);
                throw;
            }
        }

        public void IsErrorMessageDisplayed(string expected, ElementLocator elemlocatar)
        {
            this.Driver.WaitUntilElementIsFound(elemlocatar, BaseConfiguration.LongTimeout);
            this.Driver.IsExpectedTextMatchWithActualText(elemlocatar, expected);
        }

        public void VerifyingAllSectionEditableForTask(string actual, string expected, string name)
        {
            Verify.That(this.DriverContext, () => Assert.AreEqual(expected, actual), "Verifying whether the :" + name + ": field is in editable format", name + ": Field is editable", "Field is non-editable");
        }

        public void IsCommunicationsLogTabClickable()
        {
            this.Driver.WaitUntilElementIsFound(this.communicationsTab, BaseConfiguration.LongTimeout);
            var webElement = this.Driver.GetElement(this.communicationsTab);
            this.Driver.JavaScriptClick(webElement, this.nmtab);
        }

        public void IsUserAbleToViewTotalNoOfCommentsForTaskRecord()
        {
            try
            {
                Thread.Sleep(2000);
                this.Driver.WaitUntilElementIsFound(this.totalNoOfComments, BaseConfiguration.LongTimeout);
                string commentsText = this.Driver.GetText(this.totalNoOfComments);
                string[] commentsTextSplit = commentsText.Split(' ');
                string commentsCount = commentsTextSplit[0];
                DriverContext.ExtentStepTest.Log(LogStatus.Pass, "To Validate that the user can view the count of total number of comments added against that Task record", commentsCount + " : is the Comments Count Visible Successfully");
            }
            catch (Exception ex)
            {
                Logger.Error("An exception occured while the user views the Comments Count " + ex.ToString());

                DriverContext.ExtentStepTest.Log(LogStatus.Fail, "To Validate that the user can view the count of total number of comments added against that Task record", "An exception occured while the user views the Comments Count");
            }
        }

        public void IsUserAbleToViewCommunicationLogSaved(string logAdded)
        {
            {
                try
                {
                    this.Driver.WaitUntilElementIsFound(this.communicationLogCommentsText, BaseConfiguration.LongTimeout);
                    IList<IWebElement> lstTableElements = this.Driver.GetElements(this.communicationLogCommentsText);
                    bool exist = lstTableElements.All(x => x.Text == logAdded);
                    if (exist == true)
                    {
                        DriverContext.ExtentStepTest.Log(LogStatus.Pass, "To Validate that the User can view the Communication Log saved", "The User can view the Communication Log saved");
                        Logger.Info("User can view the Communication Log saved");
                    }
                    else
                    {
                        Assert.IsFalse(exist);
                    }
                }
                catch (Exception ex)
                {
                    Logger.Error("An exception occured while the user views the Communication Log saved " + ex.ToString());

                    DriverContext.ExtentStepTest.Log(LogStatus.Fail, "To Validate that the User can view the Communication Log saved", "An exception occurred while the user views the Communication Log saved");
                }
            }
        }

        public void IscommunicationLogDateTimeSortedInDecending()
        {
            this.Driver.AreElementsSortedInorderForDateTime(this.allcommunicationLogDateTime, this.nmallcommunicationLogDateTime, "desc", "DateTime", "at, • Edited");
            DriverContext.ExtentStepTest.Log(LogStatus.Pass, "To Validate that the user can view the logs in the descending order of the Creation", "user can view the logs in the descending order of the Creation");
            Logger.Info("user can view the logs in the descending order of the Creation");
        }

        public string VerifyUsersinCommunicationLogInput(string firstCommunicationLog)
        {
            this.Driver.WaitUntilElementIsFound(this.taggeduserlist, BaseConfiguration.LongTimeout);
            this.Driver.IsElementVisible(this.taggeduserlist, this.nmtaggeduserlists);
            string getusername = this.Driver.GetText(this.taggeduserlist);
            var webElement = this.Driver.GetElement(this.taggeduserlist);
            webElement.Click();
            var webElementCommentBox = this.Driver.GetElement(this.commentTextBox);
            webElementCommentBox.SendKeys(firstCommunicationLog);
            this.Driver.WaitForPageLoad();
            return getusername;
        }

        public void VerifyUserIsAbleToTagSelectedUserInCommunicationLog(string firstCommunicationLog)
        {
            var webElementCommentBox = this.Driver.GetElement(this.commentTextBox);
            webElementCommentBox.SendKeys(Keys.Enter);
            webElementCommentBox.SendKeys(firstCommunicationLog);
            this.Driver.WaitForPageLoad();
        }

        public void IsUserAbleToViewAllCommunicationLogAgainstTask()
        {
            try
            {
                this.Driver.WaitUntilElementIsFound(this.communicationLogCommentsText, BaseConfiguration.LongTimeout);
                IList<IWebElement> items = this.Driver.GetElements(this.communicationLogCommentsText);
                int cardCount = items.Count();
                string firstLogText = items.Last().Text;
                string secondLogText = items.First().Text;
                if (firstLogText != string.Empty && secondLogText != string.Empty)
                {
                    DriverContext.ExtentStepTest.Log(LogStatus.Pass, "To Validate that the  user can view Communication Logs Count against the Task", "user can view Communication Logs Count : " + cardCount + " against that Task");
                    this.Driver.WaitUntilElementIsFound(this.allcommunicationLogDateTime, BaseConfiguration.LongTimeout);
                    IList<IWebElement> itemsDescription = this.Driver.GetElements(this.allcommunicationLogDateTime);
                    string firstLogDateTime = itemsDescription.Last().Text;
                    string secondLogDateTime = itemsDescription.First().Text;
                    IList<IWebElement> itemsOwner = this.Driver.GetElements(this.ownerComminicationLog);
                    string firstLogOwner = itemsOwner.Last().Text;
                    string secondLogOwner = itemsOwner.First().Text;
                    DriverContext.ExtentStepTest.Log(LogStatus.Pass, "To Validate that the  user can view First Communication Log details", "user can view First Communication Log details-Description as " + firstLogText + ", Date and Time as " + firstLogDateTime + ", ASTM Staff who added it as " + firstLogOwner);
                    IList<IWebElement> taggedUser = this.Driver.GetElements(this.userTaggedCommunicationLog);
                    string userTaggedSecondLog = taggedUser.First().Text;
                    DriverContext.ExtentStepTest.Log(LogStatus.Pass, "To Validate that the  user can view Second Communication Log details", "user can view Second Communication Log details-Description as " + secondLogText + ", Date and Time as " + secondLogDateTime + ", Tagged User as " + userTaggedSecondLog + " , ASTM Staff who added it as " + secondLogOwner);
                    Logger.Info("user can view all the communication logged against that Task");
                }
                else
                {
                    throw new Exception("An exception occured while the user views the Communication Log against the Task");
                }
            }
            catch (Exception ex)
            {
                Logger.Error("An exception occured while the user views the Communication Log against the Task " + ex.ToString());

                DriverContext.ExtentStepTest.Log(LogStatus.Fail, "To Validate that the  user can view all the communication logged against the Task", "An exception occured while the user views the Communication Log against the Task");
            }
        }

        public void IsUserAbleToClickEditIconCommunicationLog()
        {
            this.Driver.PageScrollDown();
            this.Driver.IsElementVisible(this.userAvatarIcon, this.nmeditBtn);
            this.Driver.MouseOverOnWebElement(this.userAvatarIcon);
            this.Driver.IsElementVisible(this.editIconCommunicationLog, this.nmeditBtn);
            this.Driver.IsElementClickable(this.editIconCommunicationLog, this.nmEditCommentIcon);
        }

        public void IsUserAbleToEditCommentCommunicationLog(string commentText)
        {
            this.Driver.PageScrollDown();
            this.Driver.IsElementVisible(this.divEditComment, this.nmdivEditComment);
            var webElement = this.Driver.GetElement(this.divEditComment);
            this.Driver.IsElementClickable(this.divEditComment, this.nmdivEditComment);
            Thread.Sleep(1000);
            webElement.SendKeys(commentText);
            this.Driver.IsElementVisible(this.updateCommentCommunicationLog, this.nmupdateCommunicationLog);
            this.Driver.IsElementClickable(this.updateCommentCommunicationLog, this.nmupdateCommunicationLog);
        }

        public void IsUserAbleToAddComment(string text)
        {
            Thread.Sleep(2000);
            this.Driver.WaitUntilElementIsFound(this.commentTextBox, BaseConfiguration.LongTimeout);
            this.Driver.ScrollToWebElement(this.commentTextBox);
            var webElement = this.Driver.GetElement(this.commentTextBox);
            webElement.SendKeys(Keys.Enter);
            webElement.SendKeys(text);
        }

        public void IsUserAbletoClickSubmitButton()
        {
            Thread.Sleep(2000);
            this.Driver.WaitUntilElementIsFound(this.submitBtn, BaseConfiguration.LongTimeout);
            this.Driver.ScrollToWebElement(this.submitBtn);
            var webElement = this.Driver.GetElement(this.submitBtn, BaseConfiguration.LongTimeout);
            webElement.Click();
        }

        public void IsUserableToClickDeleteComment()
        {
            this.Driver.ScrollToWebElement(this.usernameOfComment);
            this.Driver.MouseOverOnWebElementAndClick(this.usernameOfComment, this.nmusernameOfComment);
            this.Driver.IsElementClickable(this.deleteCommentIcon, this.nmdeleteCommentIcon);
        }

        public void IsDeleteCommentPopupWindowVisible(string expectedText)
        {
            this.Driver.WaitUntilElementIsFound(this.deleteCommentPopupWindow, BaseConfiguration.LongTimeout);
            var actualText = this.Driver.GetText(this.deleteCommentPopupWindow);
            this.Driver.IsExpectedTextMatchWithActualText(this.deleteCommentPopupWindow, expectedText, actualText);
            var webEle = this.Driver.GetElement(this.deleteYesButton, BaseConfiguration.LongTimeout);
            this.Driver.JavaScriptClick(webEle, this.nmdeleteYesBtn);
        }

        public void IsOkButtonClickedOnExportExcel()
        {
            this.Driver.WaitUntilElementIsFound(this.deleteYesButton, BaseConfiguration.LongTimeout);
            var webEle = this.Driver.GetElement(this.deleteYesButton, BaseConfiguration.LongTimeout);
            this.Driver.JavaScriptClick(webEle, this.nmdeleteYesBtn);
        }

        public void IsuserAbleToEnterTextInSearchBoxFeature(string titlename)
        {
            this.Driver.WaitUntilElementIsNoLongerFound(this.successmsg, BaseConfiguration.LongTimeout, this.nmRenewalTaskSearchBox);
            this.Driver.WaitUntilElementIsFound(this.renewalTaskSearchBox, BaseConfiguration.LongTimeout);
            var webElement = this.Driver.GetElement(this.renewalTaskSearchBox);
            this.Driver.PageScrollUpToTop();
            this.Driver.WaitUntilElementIsFound(this.renewalTaskSearchBox, BaseConfiguration.LongTimeout);
            this.Driver.EnterText(this.renewalTaskSearchBox, titlename, this.nmRenewalTaskSearchBox);
            this.Driver.IsElementVisible(this.searchBtnIcon, this.nmSearchIcon);
            this.Driver.IsElementClickable(this.searchBtnIcon, this.nmSearchIcon);
        }

        public void IsUserAbleToClickYesBtnOnCloneTaskConfirmationPopup()
        {
            try
            {
                var webEle = this.Driver.GetElement(this.cloneTaskConfirmationMsgYesBtn, BaseConfiguration.LongTimeout);
                this.Driver.JavaScriptClick(webEle, this.nmcloneTaskConfirmationMsgYesBtn);
                DriverContext.ExtentStepTest.Log(LogStatus.Pass, "To verify Yes button  is clicked successfully", "Yes button is clicked successfully ");
                Logger.Info("Yes button clicked/visible successfully");
            }
            catch (Exception ex)
            {
                Logger.Error("Failed to get Element clickable on yes button Due to exception: " + ex.ToString());
                DriverContext.ExtentStepTest.Log(LogStatus.Fail, "To verify Yes button  is clicked successfully", "Yes button is not clicked successfully ");
                throw;
            }
        }

        public void IsUserAbleToClickNoBtnOnCloneTaskConfirmationPopup()
        {
            try
            {
                var webEle = this.Driver.GetElement(this.cloneTaskConfirmationMsgNoBtn, BaseConfiguration.LongTimeout);
                this.Driver.JavaScriptClick(webEle, this.nmcloneTaskConfirmationMsgNoBtn);
                DriverContext.ExtentStepTest.Log(LogStatus.Pass, "To verify No button is clicked successfully", "No button is clicked successfully ");
                Logger.Info("No button clicked/visible successfully");
            }
            catch (Exception ex)
            {
                Logger.Error("Failed to get Element clickable on No button Due to exception: " + ex.ToString());
                DriverContext.ExtentStepTest.Log(LogStatus.Fail, "To verify No button is clicked successfully", "No button is not clicked successfully ");
                throw;
            }
        }

        public void SectionRenewalTaskClicked()
        {
            this.Driver.WaitUntilElementIsNoLongerFound(this.loadbutton, BaseConfiguration.LongTimeout, this.nmsuccessfullmessage);
            this.Driver.IsElementVisible(this.sectionRenewalTask, this.nmSectionRenewalTask);
            this.Driver.IsElementClickable(this.sectionRenewalTask, this.nmSectionRenewalTask);
        }

        public void IsUserableToSelectTaskCardBasedOnName(string titlename)
        {
            this.Driver.IsElementClickable(this.renewalTaskCardName.Format(titlename), this.nmrenewalTaskName);
        }

        public void IsDependentTaskColumnVisible(string columnName)
        {
            bool value = this.Driver.IsElementVisibleWithSoftAssertion(this.dependantTasksColumn.Format(columnName), this.nmdependantTasksColumn);
            Verify.That(this.DriverContext, () => Assert.IsTrue(value), "Verifying whether the :" + columnName + ": column is visible", columnName + ": column  is visible", "column field  is not visible");
        }

        public void IsUserableToSelectTypeOfSearchUsingDropdown(string name)
        {
            var webElement = this.Driver.GetElement(this.searchfeatureDropdownIcon);
            this.Driver.JavaScriptClick(webElement, this.nmsearchfeatureDropdownIcon);
            this.Driver.IsElementClickableFromListofElementWithText(this.searchfeatureDropdownList, name);
            this.Driver.WaitForPageLoad();
        }

        public void IsUserabletoViewTaskAfterPerformingSearch(string titlename)
        {
            this.Driver.WaitUntilElementIsNoLongerFound(this.loadbutton, BaseConfiguration.LongTimeout, this.nmrenewaltaskcardName);
            this.Driver.IsExpectedTextMatchWithActualText(this.renewalTaskCardName.Format(titlename), titlename);
        }

        public void IsuserAbleToViewTaskDetails(string title, string taskid, string sdate, string edate, string desc, string assignee, string status)
        {
            this.IsUserableToViewTaskTitle(title);
            this.IsUserableToViewTaskId(taskid);
            this.IsUserableToViewStartDate(sdate);
            this.IsUserableToViewEndDate(edate);
            this.IsUserableToViewDescription(desc);
            this.IsUserableToViewAssignee(assignee);
            this.IsUserableToViewSatus(status);
        }

        public void IsAddedDependenciesTaskInfovisible(string name)
        {
            try
            {
                bool value = this.Driver.IsElementVisibleWithSoftAssertion(this.dependencyTaskInfo.Format(name), this.nmnewlyaddedDependenciesTable);
                Assert.IsTrue(value);
            }
            catch (Exception ex)
            {
                Logger.Error("Failed to get Element Visible: " + name + " and Element Text is " + name + " Due to exception: " + ex.ToString());
                DriverContext.ExtentStepTest.Log(LogStatus.Fail, "Failed to get Element Visible: " + name + " and Element Visible is (" + name + ")", "An exception occurred while finding Element : " + name);
                throw;
            }
        }

        public bool IsSystemGeneratedUniqueTaskIdWithPrefixREN(string uniqueTaskID)
        {
            bool isPrefixREN = false;
            try
            {
                if (uniqueTaskID != null)
                {
                    string prefix = uniqueTaskID.Substring(0, 3);
                    if (prefix == "REN")
                    {
                        isPrefixREN = true;
                        DriverContext.ExtentStepTest.Log(LogStatus.Pass, "To verify Unique Task ID " + uniqueTaskID + " begins with prefix REN ", "The expected System Generated Unique Task ID is " + uniqueTaskID + " and  actual System Generated Unique Task ID is " + uniqueTaskID + " matching successfully");
                        Logger.Info("Expected System Generated Unique Task ID " + uniqueTaskID + " and Actual System Generated Unique Task ID is " + uniqueTaskID);
                    }
                }

                if (isPrefixREN == false)
                {
                    Assert.IsTrue(isPrefixREN);
                }
            }
            catch (Exception ex)
            {
                Logger.Error("Failed  Due to exception: " + ex.ToString());

                DriverContext.ExtentStepTest.Log(LogStatus.Fail, "To verify Unique Task ID with prefix REN ", "An exception occurred while Verifying Unique Task ID prefix REN ");
                throw;
            }

            return isPrefixREN;
        }

        public string IsGetDefaultRenewalYear()
        {
            var text = this.Driver.GetText(this.defaultRenewalYear);
            Assert.IsNotNull(text);
            return text;
        }

        public void IsUniqueIDWithSelectedYear(string uniqueTaskID, string renewalYear)
        {
            bool isSelectedRenewalYear = false;
            try
            {
                if (renewalYear != null)
                {
                    string[] numbers = Regex.Split(uniqueTaskID, @"\D+");
                    string dateChk = numbers[1];
                    string year = dateChk.Substring(0, 4);
                    if (year == renewalYear)
                    {
                        isSelectedRenewalYear = true;
                        DriverContext.ExtentStepTest.Log(LogStatus.Pass, "To verify Unique Task ID holds Renewal Year " + renewalYear + " ", "The expected System Generated Unique Task ID holds Renewal Year " + renewalYear + " and  actual System Generated Unique Task ID holds Renewal Year " + renewalYear + " ");
                        Logger.Info("Expected System Generated Unique Task ID holds Renewal Year " + renewalYear + " and Actual System Generated Unique Task ID holds Renewal Year " + renewalYear);
                    }
                    else
                    {
                        Assert.IsTrue(isSelectedRenewalYear);
                    }

                    var integerSequence = dateChk.Substring(dateChk.Length - 3);
                    if (Convert.ToInt32(integerSequence) <= 999)
                    {
                        DriverContext.ExtentStepTest.Log(LogStatus.Pass, "To verify Unique Task ID holds Sequential Series of numbers for Renewal Year " + renewalYear + " ", "The expected System Generated Unique Task ID holds  Sequential Series of numbers for Renewal Year " + renewalYear + " and  actual System Generated Unique Task ID holds Sequential Series of numbers for Renewal Year " + renewalYear + " ");
                        Logger.Info("Expected System Generated Unique Task ID holds  Sequential Series of numbers for Renewal Year " + renewalYear + " and Actual System Generated Unique Task ID holds  Sequential Series of numbers for Renewal Year " + renewalYear);
                    }
                    else
                    {
                        throw new Exception("Sequencial series of the Record number for the Renewal Year " + renewalYear + " exceeds 999");
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.Error("Failed  Due to exception: " + ex.ToString());

                DriverContext.ExtentStepTest.Log(LogStatus.Fail, "To verify Unique Task ID holds Sequential Series of numbers for Renewal Year ", "An exception occurred while Verifying Unique Task ID with Sequential Series of numbers for Renewal Year ");
                throw;
            }
        }

        public void IsUserAbleToRedirectToAssigneePage(string assigneeName)
        {
            this.Driver.IsElementVisibleWithSoftAssertion(this.dependsOnassigneeNameToRedirect.Format(assigneeName), assigneeName);
            var webelement = this.Driver.GetElement(this.dependsOnassigneeNameToRedirect.Format(assigneeName), BaseConfiguration.LongTimeout);
            this.Driver.JavaScriptClick(webelement, "AssigneeNameLink");
            this.Driver.WaitUntilElementIsNoLongerFound(this.loadbutton, BaseConfiguration.LongTimeout, this.nmdimmerloading);
            bool value = this.Driver.IsElementVisibleWithSoftAssertion(this.bannerUserName.Format(assigneeName), assigneeName);
            Verify.That(this.DriverContext, () => Assert.IsTrue(value), "Verifying whether user is redirected to :" + assigneeName + ": userName Page", assigneeName + ": userName  is visible and redirected successfully ", "userName   is not visible and user is redirected to different page");
        }

        public void IsUserAbleToViewNoWatchersToDisplayText()
        {
            Thread.Sleep(2500);
            this.Driver.WaitUntilElementIsFound(this.viewBannerWatchersCount, BaseConfiguration.LongTimeout);
            var watchersCount = this.Driver.GetText(this.viewBannerWatchersCount);
            if (watchersCount == "0")
            {
                this.IsWatchersDetailsVisible();
                this.IsViewWatchersPopupClickable();
                this.Driver.IsElementVisible(this.noWatchersText, this.nmnoWatchersText);
            }
            else
            {
                throw new Exception("Watchers Count is not equal to 0");
            }
        }

        public void IsUserAbleToViewWatchersCount()
        {
            try
            {
                this.Driver.WaitUntilElementIsFound(this.viewBannerWatchersCount, BaseConfiguration.LongTimeout);
                this.Driver.IsElementVisible(this.viewBannerWatchersCount, this.nmWatchersCount);
                var watchersCount = this.Driver.GetText(this.viewBannerWatchersCount);
                DriverContext.ExtentStepTest.Log(LogStatus.Pass, "To Verify User is able to view the Watchers count ", "User is able to view the Watchers count as " + watchersCount);
                Logger.Info("User is able to view the Watchers count as " + watchersCount);
            }
            catch (Exception)
            {
                DriverContext.ExtentStepTest.Log(LogStatus.Pass, "To Verify User is able to view the Watchers count ", "User is not able to view the Watchers count");
                Logger.Info("User is not able to view the Watchers count");
                throw;
            }
        }

        public void IsUserableToAddWatcherFromList()
        {
            this.Driver.WaitUntilElementIsFound(this.searchWatcherByUserId, BaseConfiguration.LongTimeout);
            var webelement = this.Driver.GetElement(this.searchWatcherDropdownIcon, BaseConfiguration.LongTimeout);
            this.Driver.JavaScriptClick(webelement, this.nmsearchWatcherDropdownIcon);
            IList<IWebElement> items = this.Driver.GetElements(this.searchWatcherDropdownList);
            items[1].Click();
        }

        public void IsUserAbleToSearchByUserIdForAddingWatcher(string userID)
        {
            try
            {
                this.Driver.WaitUntilElementIsFound(this.searchWatcherByUserId, BaseConfiguration.LongTimeout);
                this.Driver.IsElementVisible(this.searchWatcherByUserId, this.nmsearchWatcherByUserId);
                var searchWatcherByUserIdInput = this.Driver.GetElement(this.searchWatcherByUserIdInput);
                searchWatcherByUserIdInput.SendKeys(userID);
                this.Driver.WaitUntilElementIsFound(this.searchWatcherByUserIdOptions, BaseConfiguration.LongTimeout);
                this.Driver.IsElementClickableFromListofElementWithText(this.searchWatcherByUserIdOptions, userID);
                DriverContext.ExtentStepTest.Log(LogStatus.Pass, "To verify User should able to enter USER Id's to add watchers to the list", "User is able to enter USER Id's to add watchers to the list");
                Logger.Info("User is able to enter USER Id's to add watchers to the list");
            }
            catch (Exception ex)
            {
                Logger.Error("An exception occurred while entering USER Id's to add watchers to the list " + ex.ToString());

                DriverContext.ExtentStepTest.Log(LogStatus.Fail, "To verify User should able to enter USER Id's to add watchers to the list", "An exception occurred while entering USER Id's to add watchers to the list");
                throw;
            }
        }

        public void IsAddWatcherIconClickable()
        {
            this.Driver.WaitUntilElementIsFound(this.addWatcherIcon, BaseConfiguration.LongTimeout);
            this.Driver.IsElementClickable(this.addWatcherIcon, this.nmaddWatcherIcon);
        }

        public void IsWatcherIconClickableOnBannerInTaskDetailsPage()
        {
            this.Driver.WaitUntilElementIsFound(this.watcherIcon, BaseConfiguration.LongTimeout);
            this.Driver.IsElementClickable(this.watcherIcon, this.nmwatcherIcon);
        }

        public void IsWatcherSubHeadingVisible()
        {
            this.Driver.IsElementTextDisplayed(this.watchersSubHeading, this.nmwatchersSubHeading);
        }

        public void IsUserAbleToViewWatchersList()
        {
            this.Driver.WaitUntilElementIsFound(this.watchersList, BaseConfiguration.LongTimeout);
            IList<IWebElement> items = this.Driver.GetElements(this.watchersList);
            int itemsCount = items.Count();
            if (itemsCount > 0)
            {
                var webElementLocator = this.Driver.GetElement(this.watcherListWrapper);
                this.Driver.HighlightingWebElement(webElementLocator);
                DriverContext.ExtentStepTest.Log(LogStatus.Pass, "To verify User should able to view watchers list", "User is able to view watchers list");
                Logger.Info("User is able to view watchers list");
            }
            else
            {
                DriverContext.ExtentStepTest.Log(LogStatus.Fail, "To verify User should able to view watchers list", "User is not able to view watchers list");
                Logger.Info("User is not able to view watchers list");
            }
        }

        public void IsUserAddedInWatchersFromTaskList()
        {
            this.Driver.WaitForPageLoad();
            this.Driver.WaitUntilElementIsFound(this.watchersList, BaseConfiguration.LongTimeout);
            IList<IWebElement> items = this.Driver.GetElements(this.watchersList);
            int itemsCount = items.Count();
            if (itemsCount > 0)
            {
                var webElementLocator = this.Driver.GetText(this.userAddedFromTaskListToWatchers);
                DriverContext.ExtentStepTest.Log(LogStatus.Pass, "To validate that user " + webElementLocator + " is added as a watcher in the Watcher List", "user " + webElementLocator + " is added as a watcher in the Watcher List");
                Logger.Info("user is added as a watcher in the Watcher List");
            }
            else
            {
                DriverContext.ExtentStepTest.Log(LogStatus.Fail, "To verify User should able to view watchers list", "User is not able to view watchers list");
                Logger.Info("User is not able to view watchers list");
            }
        }

        public void IsUserAbleToViewUpcomingStatusLaneTaskCount()
        {
            this.Driver.WaitUntilElementIsFound(this.upcomingTasksCount, BaseConfiguration.LongTimeout);
            var count = this.Driver.GetText(this.upcomingTasksCount);
            DriverContext.ExtentStepTest.Log(LogStatus.Pass, "To verify User should able to view Upcoming Status Lane Task Count", "User is able to view Upcoming Status Lane Task Count and the current Count is : " + count);
            Logger.Info("User is able to view  Upcoming Status Lane Task Count");
        }

        public void IsUserAbleToViewOpenStatusLaneTaskCount()
        {
            this.Driver.WaitUntilElementIsFound(this.openDvTasksCount, BaseConfiguration.LongTimeout);
            var count = this.Driver.GetText(this.openDvTasksCount);
            DriverContext.ExtentStepTest.Log(LogStatus.Pass, "To verify User should able to view Open Status Lane Task Count", "User is able to view Open Status Lane Task Count and the current Count is : " + count);
            Logger.Info("User is able to view  Open Status Lane Task Count");
        }

        public void IsUserAbleToViewDoneStatusLaneTaskCount()
        {
            this.Driver.WaitUntilElementIsFound(this.doneDvTasksCount, BaseConfiguration.LongTimeout);
            var count = this.Driver.GetText(this.doneDvTasksCount);
            DriverContext.ExtentStepTest.Log(LogStatus.Pass, "To verify User should able to view Done Status Lane Task Count", "User is able to view Done Status Lane Task Count and the current Count is : " + count);
            Logger.Info("User is able to view  Done Status Lane Task Count");
        }

        public void RenewalTaskCardMessageOnBlankUpComing(string expected)
        {
            this.Driver.WaitUntilElementIsFound(this.renewaltaskHomePageUpComingCard, BaseConfiguration.LongTimeout);
            this.Driver.IsExpectedTextMatchWithActualText(this.renewaltaskHomePageUpComingCard, expected);
        }

        public void RenewalTaskCardMessageOnBlankOpen(string expected)
        {
            this.Driver.WaitUntilElementIsFound(this.renewaltaskHomePageOpenCard, BaseConfiguration.LongTimeout);
            this.Driver.IsExpectedTextMatchWithActualText(this.renewaltaskHomePageOpenCard, expected);
        }

        public void RenewalTaskCardMessageOnBlankDone(string expected)
        {
            this.Driver.WaitUntilElementIsFound(this.renewaltaskHomePageDoneCard, BaseConfiguration.LongTimeout);
            this.Driver.IsExpectedTextMatchWithActualText(this.renewaltaskHomePageDoneCard, expected);
        }

        public void IsAddNewWatchersTextBoxVisible()
        {
            bool value = this.Driver.IsElementPresent(this.searchWatcherByUserId, BaseConfiguration.ShortTimeout);
            Verify.That(this.DriverContext, () => Assert.IsFalse(value), "To Verify Add new Watchers textbox is not visible", "Add New Watchers textbox is hidden", "Add New Watchers textbox is visible");
        }

        public void IsUserableToSelectInactiveTaskCard()
        {
            this.Driver.WaitForPageLoad();
            this.Driver.WaitUntilElementIsFound(this.inactiveTaskCardList, BaseConfiguration.LongTimeout);
            IList<IWebElement> items = this.Driver.GetElements(this.inactiveTaskCardList);
            items.First().Click();
        }

        public void IsWatcherslabelVisible()
        {
            bool value = this.Driver.IsElementPresent(this.watcherslabel, BaseConfiguration.ShortTimeout);
            Verify.That(this.DriverContext, () => Assert.IsFalse(value), "To Verify Add new Watchers label is not visible for Inactive Tasks", "Add New Watchers label is not visible for Inactive Tasks", "Add New Watchers label is visible for Inactive Tasks");
        }

        public void IsUserAbleToClickOnAuditLogTab()
        {
            this.Driver.WaitUntilElementIsFound(this.auditLogTab, BaseConfiguration.LongTimeout);
            var webelement = this.Driver.GetElement(this.auditLogTab, BaseConfiguration.LongTimeout);
            this.Driver.JavaScriptClick(webelement, this.nmAuditLogtab);
        }

        public void IsuserAbleToEnterTextFromDateFieldInAuditLogTab(string date, string errormsg)
        {
            this.Driver.WaitUntilElementIsFound(this.fromDateauditLogTab, BaseConfiguration.LongTimeout);
            this.Driver.EnterText(this.fromDateauditLogTab, date, this.nmFromDate);
            this.Driver.IsElementVisible(this.searchbuttonAuditLogTab, this.nmSearchIcon);
            var webelement = this.Driver.GetElement(this.searchbuttonAuditLogTab, BaseConfiguration.LongTimeout);
            this.Driver.JavaScriptClick(webelement, this.nmSearchIcon);
            this.Driver.IsExpectedTextMatchWithActualText(this.errorMessageFromDate, errormsg);
        }

        public void IsuserAbleToEnterTextToDateFieldInAuditLogTab(string date, string errormsg)
        {
            this.Driver.WaitUntilElementIsFound(this.toDateauditLogTab, BaseConfiguration.LongTimeout);
            this.Driver.EnterText(this.toDateauditLogTab, date, this.nmToDate);
            this.Driver.IsElementVisible(this.searchbuttonAuditLogTab, this.nmSearchIcon);
            this.Driver.IsElementClickable(this.searchbuttonAuditLogTab, this.nmSearchIcon);
            this.Driver.IsExpectedTextMatchWithActualText(this.errorMessageFromDate, errormsg);
        }

        public void IsUserableToViewRenewalTasksList()
        {
            bool value = this.Driver.IsElementPresent(this.openTasksList, BaseConfiguration.MediumTimeout);
            Verify.That(this.DriverContext, () => Assert.IsTrue(value), "To Verify Task List is visible", "Tasks List  is  visible", "Tasks List is not visible");
        }

        public void IsUserIsAbleToClickOnExportExcelIcon()
        {
            Thread.Sleep(3000);
            this.Driver.WaitForPageLoad();
            this.Driver.WaitUntilElementIsFound(this.exportExcelBtn, BaseConfiguration.LongTimeout);
            var webElement = this.Driver.GetElement(this.exportExcelBtn);
            this.Driver.JavaScriptClick(webElement, this.nmexportExcelBtn);
        }

        public void IsExportExcelFileExcelRowCount(int expectedCount)
        {
            try
            {
                Thread.Sleep(5000);
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
                    DataSet getExcelColCount = FilesHelper.ReadExcelFileReturnTable(files[0]);
                    int rowCount = 0;
                    if (getExcelColCount != null && getExcelColCount.Tables.Count > 0)
                    {
                        rowCount = getExcelColCount.Tables[0].Rows.Count;
                    }

                    Assert.AreEqual(expectedCount, rowCount);
                    Logger.Error("File downloaded , excel file name : " + filename + " row Count :" + rowCount);
                    DriverContext.ExtentStepTest.Log(LogStatus.Pass, "File downloaded , excel file name : " + filename);
                }
            }
            catch (Exception ex)
            {
                Logger.Error("Exception occured while downloading excel file : " + ex.ToString());
                DriverContext.ExtentStepTest.Log(LogStatus.Fail, "Exception occured while downloading excel file");
            }
        }

        public void IsExcelFileDownloaded()
        {
            try
            {
                Thread.Sleep(5000);
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

        public int GetUpcomingDivCount()
        {
            this.Driver.WaitUntilElementIsFound(this.upcomingTasksCount, BaseConfiguration.LongTimeout);
            var count = this.Driver.GetText(this.upcomingTasksCount);
            return Convert.ToInt32(count);
        }

        public void WaitforSmallLoaderDiv()
        {
            this.Driver.WaitUntilElementIsNoLongerFound(this.smallLoaderOnCardSection, BaseConfiguration.LongTimeout, this.nmdivloader);
        }

        public int GetOpenDivCount()
        {
            this.Driver.WaitForPageLoad();
            this.Driver.WaitUntilElementIsFound(this.openDvTasksCount, BaseConfiguration.LongTimeout);
            var count = this.Driver.GetText(this.openDvTasksCount);
            return Convert.ToInt32(count);
        }

        public int GetDoneDivCount()
        {
            this.Driver.WaitUntilElementIsFound(this.doneDvTasksCount, BaseConfiguration.LongTimeout);
            var count = this.Driver.GetText(this.doneDvTasksCount);
            return Convert.ToInt32(count);
        }

        public void IsUserAbleToClickOnRenewalTaskID()
        {
            this.Driver.WaitUntilElementIsFound(this.taskIDRenewalTask, BaseConfiguration.LongTimeout);
            var webelement = this.Driver.GetElement(this.taskIDRenewalTask, BaseConfiguration.LongTimeout);
            webelement.Click();
        }

        public void IsUserAbleToViewTaggedUserInCommunicationLogAdded(string expected)
        {
            Assert.IsTrue(this.Driver.IsElementPresentOrNot(this.isTaggedUser.Format(expected), string.Format(this.nmisTaggedUser, expected), string.Empty));
        }

        public void IsUserAbleToRemoveTaggedUserFromComment()
        {
            try
            {
                this.Driver.PageScrollDown();
                this.Driver.IsElementVisible(this.editCommunicationLogComment, this.nmdivEditComment);
                var webElement = this.Driver.GetElement(this.editCommunicationLogComment);
                this.Driver.IsElementClickable(this.editCommunicationLogComment, this.nmdivEditComment);
                string actualComment = this.Driver.GetText(this.editCommunicationLogComment);
                string taggedUser = actualComment.Split(' ')[0];
                string[] expectedComment = actualComment.Split(' ').Skip(1).ToArray();
                string comment = string.Join(" ", expectedComment);
                Thread.Sleep(1000);
                webElement.Clear();
                webElement.SendKeys(comment);
                Logger.Info("User is allowed to Remove the tagged user from the comment");
                DriverContext.ExtentStepTest.Log(LogStatus.Pass, "To Validate that User is allowed to Remove the tagged user from the comment", "User is allowed to Remove the tagged user from the comment");
            }
            catch (Exception ex)
            {
                Logger.Error("User is not allowed to Remove the tagged user from the comment : " + ex.ToString());
                DriverContext.ExtentStepTest.Log(LogStatus.Fail, "To Validate that User is allowed to Remove the tagged user from the comment", "User is not allowed to Remove the tagged user from the comment");
                throw;
            }
        }

        public void IsUserAbleToClickUpdateBtnForCommunicationLogComment()
        {
            this.Driver.IsElementVisible(this.updateCommentCommunicationLog, this.nmupdateCommunicationLog);
            this.Driver.IsElementClickable(this.updateCommentCommunicationLog, this.nmupdateCommunicationLog);
        }

        public void IsEditButtonVisible(string name = "General Information")
        {
            bool value = this.Driver.IsElementPresent(this.editBtn.Format(name), BaseConfiguration.MediumTimeout);
            Verify.That(this.DriverContext, () => Assert.IsFalse(value), "To Verify Edit Button is not visible", "Edit Button is not visible", "Edit Button is visible");
        }

        public void IsAssigneeListVisible()
        {
            var webEle = this.Driver.GetElement(this.assigneeDropdownIcon, BaseConfiguration.LongTimeout);
            this.Driver.JavaScriptClick(webEle, this.nmassigneeDropDownicon);
            this.Driver.AreElementsVisible(this.assigneeDropDownOptions, this.nmassigneeDropDownOptions);
        }

        public void IsUserAbleToViewAllAttachedDocumentsInCloneTask(string firstDoc, string secondDoc, string thirdDoc)
        {
            try
            {
                this.Driver.WaitUntilElementIsFound(this.cloneTaskAttachedDocList, BaseConfiguration.LongTimeout);
                IList<IWebElement> items = this.Driver.GetElements(this.cloneTaskAttachedDocList);
                int docsCount = items.Count();
                if (docsCount != 0)
                {
                    string firstUploadedDoc = items[0].Text;
                    string secondUploadedDoc = items[1].Text;
                    string thirdUploadedDoc = items[2].Text;
                    if (firstUploadedDoc == firstDoc && secondUploadedDoc == secondDoc && thirdUploadedDoc == thirdDoc)
                    {
                        DriverContext.ExtentStepTest.Log(LogStatus.Pass, "To Validate that the  user can view all the attached uploaded documents", "user is able to view all the attached uploaded documents");
                        Logger.Info("user is able to view all the attached uploaded documents");
                    }
                    else
                    {
                        throw new Exception("An exception occured while the user user is not able to view all the attached uploaded documents");
                    }
                }
                else
                {
                    throw new Exception("An exception occured while the user user is not able to view the attached uploaded documents");
                }
            }
            catch (Exception)
            {
                DriverContext.ExtentStepTest.Log(LogStatus.Fail, "To Validate that the  user can view all the attached uploaded documents", "user is not able to view all the attached uploaded documents");
                Logger.Info("user is not able to view all the attached uploaded documents");
            }
        }

        public void IsUserableToSetReminder()
        {
            var webElement = this.Driver.GetElement(this.viewBannerSetReminder, BaseConfiguration.LongTimeout);
            this.Driver.JavaScriptClick(webElement, this.nmBannerSetReminder);
            string date = DateHelper.GetFutureDateInMMDDYYYY(2);
            var element = this.Driver.GetElement(this.reminderDateTextbox, BaseConfiguration.LongTimeout);
            this.Driver.JavaScriptClick(element, this.nmreminderDateTextbox);
            this.Driver.EnterText(this.reminderDateTextbox, date, this.nmreminderDateTextbox);
            this.Driver.IsElementClickable(this.setButton, this.nmsetButton);
        }

        public void IsUserIsAbleToClickcommuncationlogPopupOkButtron()
        {
            this.Driver.WaitUntilElementIsFound(this.communcationlogPopupOkButtron, BaseConfiguration.LongTimeout);
            var webElement = this.Driver.GetElement(this.communcationlogPopupOkButtron);
            this.Driver.JavaScriptClick(webElement, this.nmcommuncationlogPopupOkButtron);
            if (this.Driver.IsElementPresentOrNot(this.communcationlogPopupOkButtron, this.nmcommuncationlogPopupOkButtron, string.Empty) == true)
            {
                webElement = this.Driver.GetElement(this.communcationlogPopupOkButtron);
                this.Driver.JavaScriptClick(webElement, this.nmcommuncationlogPopupOkButtron);
            }

            this.Driver.WaitUntilElementIsNoLongerFound(this.dimmerVisible, BaseConfiguration.LongTimeout, this.nmdimmerloading);
        }

        public void IsUserAbletoViewMessageOnCommuncationLogPOpupSoftAssertion(string updateSuccessMessage)
        {
            Verify.WaitUntilElementIsFoundWithSoftAssertion(this.DriverContext, this.btnCommunationLogPopupMessage, this.nmbtnCommunationLogPopupMessage, BaseConfiguration.LongTimeout);
            Verify.IsElementVisibleWithSoftAssertion(this.DriverContext, this.btnCommunationLogPopupMessage, this.nmbtnCommunationLogPopupMessage);
            Verify.IsExpectedTextMatchWithActualTextWithSoftAssertion(this.DriverContext, this.btnCommunationLogPopupMessage, updateSuccessMessage, this.nmbtnCommunationLogPopupMessage);
        }

        public void IsUserIsAbleToClickRemoveCommunicationLog()
        {
            this.Driver.WaitUntilElementIsNoLongerFound(this.dimmerVisible, BaseConfiguration.LongTimeout, this.nmdimmerloading);
            var by = this.removeCommuncationLog.ToBy();
            var webElement = this.Driver.FindElement(by);
            this.Driver.JavaScriptClick(webElement, this.nmremoveCommuncationLog);
            this.Driver.WaitForPageLoad();
        }

        public void IsUserAbleToViewCommuncationLog(string updateSuccessMessage)
        {
            this.Driver.IsElementVisible(this.updateSuccessMessage, this.nmupdateMessageCommunicationLog);
            this.Driver.IsExpectedTextMatchWithActualText(this.updateSuccessMessage, updateSuccessMessage, string.Format(this.nmupdateMessageCommunicationLog, updateSuccessMessage));
            this.Driver.WaitUntilElementIsNoLongerFound(this.updateSuccessMessage, BaseConfiguration.LongTimeout, string.Format(this.nmupdateMessageCommunicationLog, updateSuccessMessage));
            this.Driver.WaitForPageLoad();
        }

        public void IsUserIsAbleToClickEditCommunicationLogSubmitButtron()
        {
            this.Driver.WaitUntilElementIsFound(this.editSubmitButton, BaseConfiguration.LongTimeout);
            var webElement = this.Driver.GetElement(this.editSubmitButton);
            this.Driver.JavaScriptClick(webElement, this.nmsubmitBtnCommuncationLog);
            this.Driver.WaitForPageLoad();
            this.Driver.WaitUntilElementIsNoLongerFound(this.dimmerVisible, BaseConfiguration.LongTimeout, this.nmdimmerloading);
        }

        public void IsUserAbleToEnterTextInEditCommuncationLog(string communicationLog)
        {
            this.Driver.WaitForPageLoad();
            this.Driver.WaitUntilElementIsNoLongerFound(this.dimmerVisible, BaseConfiguration.LongTimeout, this.nmdimmerloading);
            this.Driver.WaitUntilElementIsFound(this.editinputCommuncationLog, BaseConfiguration.LongTimeout);
            this.Driver.IsElementVisible(this.editinputCommuncationLog, this.nmEditinputCommuncationLog);
            this.Driver.EnterText(this.editinputCommuncationLog, communicationLog, this.nmEditinputCommuncationLog);
        }

        public void IsUserIsAbleToClickEditCommunicationLog()
        {
            this.Driver.WaitUntilElementIsNoLongerFound(this.loadbutton, BaseConfiguration.LongTimeout, this.nmdimmerloading);
            var by = this.editCommuncationLog.ToBy();
            var webElement = this.Driver.FindElement(by);
            this.Driver.JavaScriptClick(webElement, this.nmEditButtonCommuncationLog);
            this.Driver.WaitForPageLoad();
        }

        public void GetEditedCommuncationLogExistsInDBSoftAssertion(string communcationLogText, string compareText, string message)
        {
            Verify.GetSingleValueFromDBCompareWithExpectedValue(this.DriverContext, string.Format(SqlQuery.FunctionalEditedRenewalTasksCommunicationExists, communcationLogText), compareText, message);
        }

        public void IscommunicationLogDescriptionVisibleSoftAssertion(string expected, int index)
        {
            Verify.IsExpectedTextMatchWithActualTextWithSoftAssertion(this.DriverContext, this.getLogFirstIndex.Format(index), expected);
        }

        public void ValidateMessageAfterSavingCommunicatioLogSoftAssertion(string updateSuccessMessage)
        {
            Verify.IsElementVisibleWithSoftAssertion(this.DriverContext, this.updateSuccessMessage, this.nmupdateMessageCommunicationLog);
            Verify.IsExpectedTextMatchWithActualTextWithSoftAssertion(this.DriverContext, this.updateSuccessMessage, updateSuccessMessage, string.Format(this.nmupdateMessageCommunicationLog, updateSuccessMessage));
            Verify.WaitUntilElementIsNoLongerFoundWithSoftAssertion(this.DriverContext, this.updateSuccessMessage, this.nmupdateMessageCommunicationLog, BaseConfiguration.LongTimeout);
        }

        public void IsUserIsAbleToClickCommunicationLogSubmitButtron()
        {
            this.Driver.WaitUntilElementIsNoLongerFound(this.dimmerVisible, BaseConfiguration.LongTimeout, this.nmdimmerloading);
            var webElement = this.Driver.GetElement(this.submitBtnCommuncationLog);
            this.Driver.JavaScriptClick(webElement, this.nmsubmitBtnCommuncationLog);
            this.Driver.WaitForPageLoad();
            this.Driver.WaitUntilElementIsNoLongerFound(this.dimmerVisible, BaseConfiguration.LongTimeout, this.nmdimmerloading);
        }

        public string InputTagUserinCommunicationLogInputSoftAssertion(string index)
        {
            Verify.WaitUntilElementIsFoundWithSoftAssertion(this.DriverContext, this.selectTagUser.Format(index), this.nmtaguser, BaseConfiguration.LongTimeout);
            string getusername = Verify.getTextIfElementIsVisibleWithSoftAssertion(this.DriverContext, this.selectTagUser.Format(index), "First user in the list");
            Verify.IsElementClickableWithSoftAssertion(this.DriverContext, this.selectTagUser.Format(index), string.Format(this.nmtaguser, getusername));
            this.Driver.WaitForPageLoad();
            return getusername;
        }

        public void VerifyTagUserListOnTypingAtSoftAssertion()
        {
            Verify.WaitUntilElementIsFoundWithSoftAssertion(this.DriverContext, this.tagusersList, this.nmTagUsersList, BaseConfiguration.LongTimeout);
            Verify.IsElementVisibleWithSoftAssertion(this.DriverContext, this.tagusersList, this.nmTagUsersList);
        }

        public void IsUserAbleToEnterTextInCommuncationLog(string communicationLog)
        {
            this.Driver.WaitUntilElementIsNoLongerFound(this.dimmerVisible, BaseConfiguration.LongTimeout, this.nmdimmerloading);
            this.Driver.WaitUntilElementIsFound(this.inputCommuncationLog, BaseConfiguration.LongTimeout);
            this.Driver.IsElementVisible(this.inputCommuncationLog, this.nminputCommuncationLog);
            this.Driver.EnterText(this.inputCommuncationLog, communicationLog, this.nminputCommuncationLog);
        }

        public void IsUserIsAbleToClickCommunicationLogTab()
        {
            this.Driver.WaitUntilElementIsNoLongerFound(this.dimmerVisible, BaseConfiguration.LongTimeout, this.nmdimmerloading);
            this.Driver.WaitUntilElementIsFound(this.tabCommuncationLog, BaseConfiguration.LongTimeout);
            var webElement1 = this.Driver.GetElement(this.tabCommuncationLog);
            this.Driver.JavaScriptClick(webElement1, this.nmtabCommuncationLog);
            this.Driver.WaitForPageLoad();
        }
    }
}