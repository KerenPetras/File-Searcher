using System;
using System.Collections.Generic;
using System.IO;
using System.Text;


namespace File_Searcher_Final
{
    public delegate void InfoHandler(string newInfo);
    class Search
    {
        public event InfoHandler newInfofound;
        public List<string> results = new List<string>();
        public void FileSearching(string fileName , string fileLocation) //funcation for search
        {
            if (!Access(fileLocation)) 
            {
                return;
            }
            string[] fileList=Directory.GetFiles(fileLocation,$"*{fileName}*"); // one time searching by name
            string[] folderList=Directory.GetDirectories(fileLocation); // second time searching for more optional location with the requied name

            foreach (string i in fileList)
            {
                newInfofound(i);
                results.Add(i);
            }

            foreach (string i in folderList)
            {
                FileSearching(fileName, i);
            }
        }

        private bool Access(string locationCheck) 
        {
            try
            {
                Directory.GetDirectories(locationCheck);
                return true;
            }
            catch (Exception)
            {

                return false;
            }
        }


        public string getResultFileName(string fullName) {

            return fullName.Split("\\")[^1];
        
        }

    }
}
