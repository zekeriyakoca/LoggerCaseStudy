using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace LoggerCaseStudy.Util.Util
{
    public static class FileOperations
    {
        public static void WriteToJsonFile<T>(string filePath, T objectToWrite, bool append = false)
        {
            try
            {
                CreateFileIfNotExist(filePath);
                
                var contentsToWriteToFile = JsonConvert.SerializeObject(objectToWrite);
                using (TextWriter writer = new StreamWriter(filePath, append))
                {
                    writer.Write(contentsToWriteToFile);
                    writer.Write(System.Environment.NewLine);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static bool WaitForFile(string fullPath)
        {
            CreateFileIfNotExist(fullPath);

            int numTries = 0;
            while (true)
            {
                ++numTries;
                try
                {
                    using (FileStream fs = new FileStream(fullPath,
                        FileMode.Open, FileAccess.ReadWrite,
                        FileShare.None, 100))
                    {
                        fs.ReadByte();
                        break;
                    }
                }
                catch (Exception ex)
                {
                    if (numTries > 10)
                    {
                        return false;
                    }
                    System.Threading.Thread.Sleep(500);
                }
            }
            return true;
        }

        private static void CreateFileIfNotExist(string fullPath)
        {
            var directory = Path.GetDirectoryName(fullPath);
            if (directory == null)
                return;
            Directory.CreateDirectory(directory);

            if (!File.Exists(fullPath))
            {
                using (File.Create(fullPath)) { }
            }
        }
    }
}
