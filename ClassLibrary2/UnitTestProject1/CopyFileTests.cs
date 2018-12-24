

namespace Test
{
    using System;
    using System.Diagnostics;
    using System.Runtime.InteropServices;
    using FileCopyLib;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using System.IO;

    [TestClass]
    public class CopyTest
    {

        private string sourcefile = @"C:\temp\ChartCo\OneOcean.7z.001";
        private string destinationfile = @"D:\test\OneOcean.7z.001";
        
        [TestMethod]
        public void Move()
        {
            Stopwatch sw = new Stopwatch();

            sw.Start();
            Action<FileProgress> a = (fp) => { }; /*fp.ReportToConsole();*/
            FileCopyLib.FileCopier.MoveWithProgress(sourcefile, destinationfile, a);
            sw.Stop();

            // 2GB in 00:09:23.6248097 @ 4/12/2018
            Console.WriteLine("Elapsed={0}", sw.Elapsed);

        }

        [TestMethod]
        public void CopyAllBytes()
        {
            Stopwatch sw = new Stopwatch();

            sw.Start();

            byte[] x=ReadAllBytesUnbuffered(sourcefile);
            try
            {
                System.IO.File.WriteAllBytes(this.destinationfile, x);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
    
            sw.Stop();

            Console.WriteLine("Elapsed={0}", sw.Elapsed);

        }

        [TestMethod]
        public void CopyFragmentsBytes()
        {

            Stopwatch sw = new Stopwatch();

            sw.Start();
            Directory.CreateDirectory(@"D:\test\");
            if(!File.Exists(this.destinationfile))
            File.Create(this.destinationfile);

            try
            {
                ReadWriteBytes(sourcefile,this.destinationfile);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }

            sw.Stop();

            Console.WriteLine("Elapsed={0}", sw.Elapsed);

        }


        public static void ReadWriteBytes(string source, string destination)
        {
            using (var rfs = new FileStream(source, FileMode.Open, FileAccess.Read))
            using (var wfs = new FileStream(destination, FileMode.Open, FileAccess.Write))
            {

                byte[] buffer = new byte[1024];
                int bytesRead = 0;
                while ((bytesRead = rfs.Read(buffer, 0, buffer.Length)) > 0)
                {
                    //System.IO.File.WriteAllBytes(destination, buffer);
                    wfs.Write(buffer, 0, bytesRead);
                }
            }
        }

        /// <summary>
        /// https://stackoverflow.com/questions/48679/copy-a-file-without-using-the-windows-file-cache
        /// Move a file without using the windows file cache
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        public static byte[] ReadAllBytesUnbuffered(string filePath)
        {
            const FileOptions FileFlagNoBuffering = (FileOptions)0x20000000;
            var fileInfo = new FileInfo(filePath);
            long fileLength = fileInfo.Length;
            int bufferSize = (int)Math.Min(fileLength, int.MaxValue / 2);
            bufferSize += ((bufferSize + 1023) & ~1023) - bufferSize;
            using (var stream = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.None,bufferSize, FileFlagNoBuffering | FileOptions.SequentialScan))
            {
                long length = stream.Length;
                if (length > 0x7fffffffL)
                {
                    throw new IOException("File too long over 2GB");
                }
                int offset = 0;
                int count = (int)length;
                var buffer = new byte[count];
                while (count > 0)
                {
                    int bytesRead = stream.Read(buffer, offset, count);
                    if (bytesRead == 0)
                    {
                        throw new EndOfStreamException("Read beyond end of file EOF");
                    }
                    offset += bytesRead;
                    count -= bytesRead;
                }
                return buffer;
            }
        }

    }

}


