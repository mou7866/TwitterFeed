using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwitterFeed.DAL.FileHelper
{
    public class FileReader
    {
        private static StreamReader _TwitterFile;

        private static FileReader _instance = new FileReader();

        private FileReader()
        {

        }

        public static FileReader Instance
        {
            get
            {
                return _instance;
            }
        }

        public List<string> ReadFromFile(string p_Path, Encoding p_Encoding)
        {
            
            var reader = GetReader(p_Path, p_Encoding);
            try
            {
                if (!File.Exists(p_Path)) throw new Exception("The file at path " + p_Path + " does not exist");
                List<string> lstFileLines = new List<string>();
                string line = string.Empty;
                while ((line = reader.ReadLine()) != null)
                {
                    lstFileLines.Add(line);
                }
                return lstFileLines;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                reader.Dispose();
                reader.Close();
            }
        }

        private StreamReader GetReader(string p_Path, Encoding p_Encoding)
        {
            return _TwitterFile = new StreamReader(p_Path, p_Encoding);
        }
    }
}
