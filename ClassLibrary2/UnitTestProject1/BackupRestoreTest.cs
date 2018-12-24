
//namespace Test
//{
//    using ChartCo.ZipFileArchiver.Enums;
//    using ChartCo.ZipFileArchiver.Interfaces;
//    using ChartCo.ZipFileArchiver.Helpers;
//    using ChartCo.PassageManager.Core.Services.Backup;
//    using Microsoft.VisualStudio.TestTools.UnitTesting;
//    using SevenZip;
//    using Ionic.Zip;
//    using System;
//    using System.Diagnostics;
//    using System.IO;

//    [TestClass]
//    public class BackupRestoreTest
//    {

//        /// <summary>
//        /// the path to 7zip executable
//        /// </summary>
//        private const string SevenZipPath = @"C:\Work\OneOcean\ChartCo.PassageManager.Shell\bin\x86\Debug\Tools\7za.exe";
        
//        /// <summary>
//        /// path to original location of backupfile
//        /// </summary>
//        private const string TempBackup = @"C:\Users\declan.taylor\AppData\Local\Temp\OneOcean.tmp";

//        /// <summary>
//        /// the root for the chartco application 
//        /// </summary>
//        private const string ChartCoRoot = @"C:\chartco";

//        /// <summary>
//        /// test back up process with default archive command
//        /// </summary>
//        [TestMethod]
//        public void TestMethodBackUp()
//        {

//            string archiveRoot = Path.GetDirectoryName(TempBackup);
//            Directory.CreateDirectory(archiveRoot);
//            string filesList = Path.Combine(archiveRoot, "chartco_filesList.tmp");

//            var commandLineArguments = SevenZipHelpers.BuildDefaultArchiveCommand(TempBackup, filesList, true);
   

//            var process = SevenZipHelpers.CreateArchiveProcess(SevenZipPath, commandLineArguments, ChartCoRoot);

//            SevenZipHelpers.GetSevenZipArchiverInstance(null, true).StartArchiveProcess(process);

//            Debug.Assert(File.Exists(TempBackup));

//        }

//        /// <summary>
//        /// test back up process with split archive command
//        /// </summary>
//        [TestMethod]
//        public void TestMethodSplitBackup()
//        {
//            // var commandLineArguments =  SevenZipHelpers.BuildSplitArchiveCommand(TempBackup, filesList, true,DriveFormatEnum.Fat16);
//            string splitCommand =
//                "a " +
//                TempBackup +
//                @"C:\Users\declan.taylor\AppData\Local\Temp\*.zip " +
//                "-v1m";

//            var process = SevenZipHelpers.CreateArchiveProcess(SevenZipPath, splitCommand, ChartCoRoot);

//            SevenZipHelpers.GetSevenZipArchiverInstance(null, true).StartArchiveProcess(process);

//        }



//    }
//}
