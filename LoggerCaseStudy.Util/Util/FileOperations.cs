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
            TextWriter writer = null;
            try
            {
                var directory = Path.GetDirectoryName(filePath);
                if (directory == null)
                    return;
                Directory.CreateDirectory(directory);

                if(!File.Exists(filePath))
                {
                    using (var file = File.Create(filePath))
                    {
                    }
                }
                var contentsToWriteToFile = JsonConvert.SerializeObject(objectToWrite);
                writer = new StreamWriter(filePath, append);
                System.Threading.Thread.Sleep(5000);
                writer.Write(contentsToWriteToFile); 
                writer.Write(System.Environment.NewLine);
            }
            catch 
            {
            }
            finally
            {
                if (writer != null)
                    writer.Close();
            }
        }
        public static bool WaitForFile(string fullPath)
        {
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
    }
}
