// <copyright file="ProjectBaseConfiguration.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace MCS.Test.Automation.Tests.NUnit
{
    using System.Configuration;
    using System.IO;
    using global::NUnit.Framework;
    using MCS.Test.Automation.Common;
    using MCS.Test.Automation.Common.Helpers;

    /// <summary>
    /// SeleniumConfiguration that consume app.config file.
    /// </summary>
    public static class ProjectBaseConfiguration
    {
        private static readonly string CurrentDirectory = TestContext.CurrentContext.TestDirectory;

        /// <summary>
        /// Gets the data driven file.
        /// </summary>
        /// <value>
        /// The data driven file.
        /// </value>
        public static string DataDrivenFile
        {
            get
            {
                if (BaseConfiguration.UseCurrentDirectory)
                {
                    return Path.Combine(CurrentDirectory + ConfigurationManager.AppSettings["DataDrivenFile"]);
                }

                return ConfigurationManager.AppSettings["DataDrivenFile"];
            }
        }


        /// <summary>
        /// Gets the data driven file.
        /// </summary>
        /// <value>
        /// The data driven file.
        /// </value>
        public static string APIDataDrivenFile
        {
            get
            {
                if (BaseConfiguration.UseCurrentDirectory)
                {
                    return Path.Combine(CurrentDirectory + ConfigurationManager.AppSettings["APIDataDrivenFile"]);
                }

                return ConfigurationManager.AppSettings["APIDataDrivenFile"];
            }
        }



        /// <summary>
        /// Gets the data driven file.
        /// </summary>
        /// <value>
        /// The data driven file.
        /// </value>
        public static string ProdAPIDataDrivenFile
        {
            get
            {
                if (BaseConfiguration.UseCurrentDirectory)
                {
                    return Path.Combine(CurrentDirectory + ConfigurationManager.AppSettings["ProdAPIDataDrivenFile"]);
                }

                return ConfigurationManager.AppSettings["ProdAPIDataDrivenFile"];
            }
        }

        /// <summary>
        /// Gets the data driven file.
        /// </summary>
        /// <value>
        /// The data driven file.
        /// </value>
        public static string QAAPIDataDrivenFile
        {
            get
            {
                if (BaseConfiguration.UseCurrentDirectory)
                {
                    return Path.Combine(CurrentDirectory + ConfigurationManager.AppSettings["QAAPIDataDrivenFile"]);
                }

                return ConfigurationManager.AppSettings["QAAPIDataDrivenFile"];
            }
        }

        /// <summary>
        /// Gets the data driven file.
        /// </summary>
        /// <value>
        /// The data driven file.
        /// </value>
        public static string DevAPIDataDrivenFile
        {
            get
            {
                if (BaseConfiguration.UseCurrentDirectory)
                {
                    return Path.Combine(CurrentDirectory + ConfigurationManager.AppSettings["DevAPIDataDrivenFile"]);
                }

                return ConfigurationManager.AppSettings["DevAPIDataDrivenFile"];
            }
        }



        /// <summary>
        /// Gets the data driven file.
        /// </summary>
        /// <value>
        /// The data driven file.
        /// </value>
        public static string APIIntegrationTestData
        {
            get
            {
                if (BaseConfiguration.UseCurrentDirectory)
                {
                    return Path.Combine(CurrentDirectory + ConfigurationManager.AppSettings["APIIntegrationTestData"]);
                }

                return ConfigurationManager.AppSettings["APIIntegrationTestData"];
            }
        }

        /// <summary>
        /// Gets the Excel data driven file.
        /// </summary>
        /// <value>
        /// The Excel data driven file.
        /// </value>
        public static string DataDrivenFileXlsx
        {
            get
            {
                if (BaseConfiguration.UseCurrentDirectory)
                {
                    return Path.Combine(CurrentDirectory + ConfigurationManager.AppSettings["DataDrivenFileXlsx"]);
                }

                return ConfigurationManager.AppSettings["DataDrivenFileXlsx"];
            }
        }

        /// <summary>
        /// Gets the Download Folder path.
        /// </summary>
        public static string DownloadFolderPath
        {
            get { return FilesHelper.GetFolder(ConfigurationManager.AppSettings["DownloadFolder"], CurrentDirectory); }
        }
    }
}
