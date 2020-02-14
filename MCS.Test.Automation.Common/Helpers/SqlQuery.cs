// <copyright file="SqlHelper.cs" company="MCS">
// Copyright (c) MCS. All rights reserved.
// </copyright>

namespace MCS.Test.Automation.Common.Helpers
{
    using NLog;

    /// <summary>
    /// Class is used for store SQL queries and reading data from database.
    /// </summary>
    public static class SqlQuery
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();
        public static string FunctionalAddFaq = "SELECT count(0) FROM db_RNE.FAQ WHERE FAQQuestion LIKE '%{0}%'";
        public static string ClassificationType = "select count(*) from [db_RNE].[ClassificationType] where Name LIKE '%{0}%'";
        public static string FunctionalMembershipDocuments = "SELECT * FROM  db_RNE.MembershipDocument WHERE Name LIKE '{0}'";
        public static string FunctionalAddOfficerTitleExists = "select count(0) from  [db_RNE].[OfficerTitle] where OfficerTitleName like '%{0}%'";
        public static string FunctionalAddOfficerIsASTMStaffExists = "select IsASTMStaff from [db_RNE].[OfficerTitle] where OfficerTitleName like '%{0}%'";
        public static string FunctionalAddOfficerResponsibityExists = "select ResponsibilityPrivilegeName from [db_RNE].[OfficerTitle] OTitle " +
        "join[db_RNE].[OfficerTitleResponsibilityMapping] TitleRe on OTitle.OfficertitleId = TitleRe.OfficertitleId " +
        "join[db_RNE].[ResponsibilityPrivilegeMapping] respriv on respriv.ResponsibilityPrivilegeMappingId = TitleRe.ResponsibilityPrivilegeMappingId " +
        "join[db_RNE].[ResponsibilityPrivilege] Resprivilege on Resprivilege.ResponsibilityPrivilegeId = respriv.ResponsibilityPrivilegeId " +
        "where OfficerTitleName like '%{0}%'";
        public static string FunctionalAddedCommitteeTypeExists = "select CommitteeTypeId, IsLimitedMembersPermitted, NoOfMembersPermitted,  IsBalanceRequired, IsEnableCommitteeOnWeb from [db_RNE].[CommitteeType] where CommitteeTypeName like '%{0}%'";
        public static string FunctionalSelectedMembershipTypesInCommitteeType = "select MembershipTypeName,MembershipTypeId,MembershipTypeCode from db_RNE.MembershipType where membershiptypeid in (select membershiptypeid from db_RNE.CommitteeTypeMembershipType where committeetypeid = '{0}') order by MembershipTypeName asc";
        public static string FunctionalLevelsAddedInCommitteeType = "select CommitteeLevelName,CommitteeLevelId,CommitteeTypeid from db_RNE.CommitteeLevel where CommitteeTypeId = {0} order by CommitteeLevelId asc";
        public static string FunctionalAddMembershipTypeInIntenalAppExists = "select count(0) from  [db_RNE].MembershipType where MembershipTypeName like '%{0}%'";
        public static string FunctionalUpdateStudentMemberExists = "SELECT StudyField,CONVERT(VARCHAR(10),GraduationDate, 103) as GraduationDate,Degree,Reasons,IsActive,InterestedCommittee,FirstName,LastName " +
        "FROM db_MEM.StudentApplication " +
        "INNER JOIN db_MEM.MemberDetail on db_MEM.StudentApplication.MemberId= db_MEM.MemberDetail.MemberId " +
        "INNER JOIN [db_MEM].[MemberAccountMapping] on[db_MEM].[MemberAccountMapping].memberid= db_MEM.MemberDetail.MemberId " +
        "WHERE db_MEM.[MemberAccountMapping].AccountNumber= '{0}'";
        public static string FunctionalAddClassificationType = "SELECT Name,Description,IsApplicableToAllCommittees FROM db_RNE.ClassificationType WHERE Name LIKE '%{0}%'";
        public static string FunctionalEditedMemberDetailsCommunicationExists = "select count(0) from db_MEM.MemberComment where Comment like '{0}'";
        public static string FunctionalEditedCommitteManagementCommunicationExists = "select count(0) from db_MEM.CommitteeComment where Comment like '{0}'";
        public static string FunctionalEditedRenewalTasksCommunicationExists = "select count(0) from [db_USR].[TaskComment] where Comment like '{0}'";
        public static string FunctionalAddMembershipType = "SELECT MembershipTypeName,FeeAmount,IsUnlimitedMembers,IsUnlimitedCommittees,IsSuppressFeeRenewalEmails,IsSuppressFeeRenewalPrint,AutoRenewalApplicable,EnableOnWeb FROM db_RNE.MembershipType  WHERE MembershipTypeName = '{0}'";
        public static string FunctionalVerifyMembershipTypeValuesEntered = "SELECT MembershipTypeName,Summary,Benefits,Description FROM db_RNE.MembershipType  WHERE MembershipTypeName = '{0}'";
        public static string FunctionalNegativeAddMembershipType = "SELECT MembershipTypeName,FeeAmount,IsUnlimitedMembers,IsUnlimitedCommittees,IsSuppressFeeRenewalEmails,IsSuppressFeeRenewalPrint,AutoRenewalApplicable,EnableOnWeb,MaxNumberofMember,MaxNumberofCommittee FROM db_RNE.MembershipType  WHERE MembershipTypeName = '{0}'";
        public static string FunctionalUpdatedMembershipTypeDetailsSection = "SELECT MembershipTypeName,FeeAmount FROM db_RNE.MembershipType  WHERE MembershipTypeName = '{0}'";
        public static string FunctionalUpdatedMembershipTypeSettingsSection = "SELECT MembershipTypeName, IsUnlimitedMembers, MaxNumberofMember, IsUnlimitedCommittees, MaxNumberofCommittee, RenewalPeriod FROM db_RNE.MembershipType  INNER JOIN db_RNE.RenewalPeriod  on db_RNE.MembershipType.RenewalPeriodId=db_RNE.RenewalPeriod.RenewalPeriodId AND MembershipTypeName = '{0}'";
        public static string FunctionalRepresenativeStudentMemberExists = "SELECT FirstName,LastName,CompanyName,MembershipTypeName " +
        "FROM db_MEM.MemberDetail " +
        "INNER JOIN db_MEM.MemberAccountMapping on db_MEM.MemberDetail.MemberId= db_MEM.MemberAccountMapping.MemberId " +
        "INNER JOIN db_MEM.Member on db_MEM.MemberDetail.MemberId= db_MEM.Member.MemberId " +
        "INNER JOIN db_RNE.MembershipType on db_MEM.Member.MembershipTypeId= db_RNE.MembershipType.MembershipTypeId " +
        "WHERE db_MEM.MemberAccountMapping.AccountNumber= '{0}'";

        public static string FunctionalAddMeetingType = "SELECT count(0) FROM [db_RNE].[MeetingType] WHERE Name = '{0}'";
        public static string FunctionalMeetingTypeDbValidation = "select Name, Description, IsActive from [db_RNE].[MeetingType] WHERE Name = '{0}'";
        public static string FunctionalAddUserRole = "SELECT count(0) FROM [db_USR].[Role] WHERE RoleName = '{0}'";
        public static string FunctionalUserRoleDbValidation = "select ModuleName,SubModuleName,PrivilegeName from [db_USR].[Role] A join [db_USR].[RolePrivilege] B on A.RoleId=B.RoleId join [db_USR].[SubModulePrivilege] C on B.SubModulePrivilegeId=C.SubModulePrivilegeId join [db_USR].[SubModule] D on C.SubModuleId=D.SubModuleId join [db_USR].[Module] E on D.ModuleId=E.ModuleId join [db_USR].[Privilege] F on F.PrivilegeId= C.PrivilegeId where RoleName='{0}'";
        public static string FunctionalNewMeetingTypeDbValidation = "select count(0) from [db_RNE].[MeetingType] A "
        + " join[db_MEM].[CommitteeMeetingSequence] B on A.MeetingTypeId= B.MeetingTypeId "
        + " join[db_MEM].[Committee] C on B.CommitteeId=C.CommitteeId where Code='{0}' and[name]='{1}'";

        public static string FunctionalNewMeetingDateDbValidation = " select count(0) from[db_MEM].[CommitteeMeetingDates] "
        + "A join[db_MEM].[Committee] B on A.CommitteeId=B.CommitteeId where Code='{0}' and isactive = 1 and meetingdate='{1}'";

    }
}
