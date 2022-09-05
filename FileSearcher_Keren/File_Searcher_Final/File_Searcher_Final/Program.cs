using System;
using System.IO;
using bll_to_database;
using System.Linq;

namespace File_Searcher_Final
{
    class Program
    {
        static void Main(string[] args)
        {
            char choice;
            string fileName;
            string location;
            Search s1;
            data sendInfo = new data();

            do
            {
                s1 = new Search();
               
                s1.newInfofound += new InfoHandler((info) => Console.WriteLine(info));

                //Main menu for the user
                Console.WriteLine("Please choose:\n" +
                    "1. search by name \n" +
                    "2. search by name + location \n" +
                    "3. Exit ");

                choice = Console.ReadKey().KeyChar;
                Console.Clear();

                switch (choice)
                {
                    case '1': //option 1, search by name file only, searches the whole computer drivers
                        Console.Write("Please enter file name: ");
                        fileName = Console.ReadLine();

                        if (fileName == "")
                        {
                            Console.WriteLine("Please enter a file name ");
                            Console.WriteLine();
                            continue;

                        }
                        s1.FileSearching(fileName, "c:\\users");
                        sendInfo.InsertSearches(fileName, "c:\\users");
                        break;

                    case '2': //option 2 searchers files by specific location by user's choice
                        Console.Write("Please enter file name: ");
                        fileName = Console.ReadLine();

                        if (fileName == "") //in any case the user doesnt insert file by name
                        {
                            Console.WriteLine("Please enter a file name ");
                            Console.WriteLine();
                            continue;
                        }
                        // in any case the user doesnt insert a foler location
                        Console.Write("Please enter file location: ");
                        location = Console.ReadLine();

                        try 
                        {
                            s1.FileSearching(fileName, location);
                            sendInfo.InsertSearches(fileName, location);

                        }
                        catch (DirectoryNotFoundException) //error for denying access to folders
                        {

                            Console.WriteLine(" Location unvailed please try again\n ");
                            continue;
                        }
                        break;

                    case '3': // exit the program
                        continue;

                    default: //error in any case the user enter symbol or numbers unvaild
                        Console.WriteLine("Please enter a vaild number between 1-3");
                        continue;
                }

                if (s1.results.Count==0)
                {
                    Console.WriteLine("Files were not found ");
                }
                foreach (string i in s1.results) //searches files by name then continue to the next folder by order
                {
                    string resultFileName = s1.getResultFileName(i);
                    string resultFileLocation;
                    resultFileLocation = i.Substring(0, (i.Length - resultFileName.Length));
                    sendInfo.insertResults(resultFileName, resultFileLocation);
                }

                Console.WriteLine();
                

            } while (choice != '3');

        }
    }
}
