using Notes2021Lib.Data;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;

namespace Notes2021Univ
{
    class Program
    {
        private static readonly HttpClient MyClient = new HttpClient();
        private static string AuthToken;
        private static IEnumerable<NoteFile> noteFiles;
        private static List<NoteFile> enumerable;
        private static int fileNumber;
        private static string fileName;
        private static List<NoteHeader> headers;

        static void Main(string[] args)
        {
            Console.WriteLine("Hello Notes 2021!");

            string baseUri = "https://www.drsinder.com/Notes2021/";
            if (args.Length == 1)
            {
                baseUri = args[0];
            }
            else if (!string.IsNullOrEmpty(Environment.GetEnvironmentVariable("Notes2021Uri")))
            {
                baseUri = Environment.GetEnvironmentVariable("Notes2021Uri");
            }

            MyClient.BaseAddress = new Uri(baseUri);

            Login();

            noteFiles = Actions.GetFileList(MyClient);
            // ReSharper disable once PossibleMultipleEnumeration
            enumerable = noteFiles.ToList();

        files:

            ShowFileList();

            fileNumber = GetFile();

        list:

            ShowSubjects();

        anothernote:

            int route = ShowNote();

            switch (route)
            {
                case 1: goto list;
                case 2: goto files;
                case 3: goto anothernote;
            }

        }

        private static void Login()
        {
        retry0:

            Console.Write("Enter your user name (email): ");
            string email = Console.ReadLine();

            Console.Write("Enter your password: ");

            string pw = Passwords.GetConsoleSecurePassword();

            HttpResponseMessage resp = Actions.Login(MyClient, email, pw);
            if (resp.StatusCode == HttpStatusCode.OK)
            {
                AuthToken = resp.Content.ReadAsStringAsync().Result;

                if (!string.IsNullOrEmpty(AuthToken))
                {
                    MyClient.DefaultRequestHeaders.Add("authentication", AuthToken);
                }
                else
                {
                    goto retry0;
                }
            }
            else
            {
                goto retry0;
            }
        }

        private static void ShowFileList()
        {
            Console.WriteLine();
            Console.WriteLine("Files:");

            foreach (var nf in enumerable)
            {
                Console.WriteLine(nf.NoteFileName + "  -  " + nf.NoteFileTitle);
            }

        }

        private static int GetFile()
        {
        retry1:
            Console.WriteLine();
            Console.WriteLine("Enter File Name: ");

            string filename = Console.ReadLine();

            if (filename == "exit" || filename == "bye")
            {
                Environment.Exit(-1);     // normal exit
            }

            foreach (var nf in enumerable)
            {
                if (filename == nf.NoteFileName)
                {
                    fileName = filename;
                    return nf.Id;
                }
            }

            goto retry1;

        }

        private static void ShowSubjects()
        {
            headers = Actions.GetNoteList(MyClient, fileNumber);

            Console.WriteLine();
            Console.WriteLine("Subjects:");
            foreach (var h in headers)
            {
                Console.WriteLine(h.NoteOrdinal + ") " + h.NoteSubject + "  Responses: " + h.ResponseCount);
            }

        }

        private static int ShowNote()
        {
        retry:
            Console.WriteLine();
            Console.WriteLine("Enter note # or 'list files', 'list subjects', 'export', 'html; :");

            string lines = Console.ReadLine();

            if (lines == "exit" || lines == "bye")
            {
                Environment.Exit(-1);     // normal exit
            }
            else if (lines == "list subjects")
            {
                return 1;
            }
            else if (lines == "list files")
            {
                return 2;
            }
            else if (lines == "export")
            {
                Stream myStream = Actions.Export(MyClient, fileNumber, false);
                FileStream myFile = File.Create(fileName + ".txt");
                myStream.CopyTo(myFile);
                myStream.Close();
                myStream.Dispose();
                myFile.Close();
                myFile.Dispose();
                Console.WriteLine("Completed");
                goto retry;
            }
            else if (lines == "html")
            {
                Stream myStream = Actions.Export(MyClient, fileNumber, true);
                FileStream myFile = File.Create(fileName + ".html");
                myStream.CopyTo(myFile);
                myStream.Close();
                myStream.Dispose();
                myFile.Close();
                myFile.Dispose();
                Console.WriteLine("Completed");
                goto retry;
            }

            int notenum;
            int respnum = 0;

            if (string.IsNullOrEmpty(lines))
            {
                goto retry;
            }

            string[] parts = lines.Split('.');
            if (parts.Length == 2)
            {
                bool ok = int.TryParse(parts[0], out notenum);
                if (!ok)
                    goto retry;
                if (notenum < 1 || notenum > headers.Count())
                {
                    Console.WriteLine("Note number out of range!");
                    goto retry;
                }

                NoteHeader myh = headers[notenum];
                ok = int.TryParse(parts[1], out respnum);
                if (!ok)
                    goto retry;
                if (respnum < 0 || respnum > myh.ResponseCount)
                {
                    Console.WriteLine("Response number out of range!");
                    goto retry;
                }
            }
            else
            {
                bool ok = int.TryParse(lines, out notenum);
                if (!ok)
                    goto retry;
                if (notenum < 1 || notenum > headers.Count())
                {
                    Console.WriteLine("Note number out of range!");
                    goto retry;
                }
            }

            if (respnum == 0)
            {
                foreach (var h in headers)
                {
                    if (h.NoteOrdinal == notenum)
                    {
                        DateTime cd = h.LastEdited.ToLocalTime();
                        string when = "  " + cd.ToShortTimeString() + " " + cd.ToShortDateString();
                        Console.WriteLine("Subject: " + h.NoteSubject + "  By: " + h.AuthorName + when);
                        break;
                    }
                }
            }
            else
            {
                List<NoteHeader> hlist = Actions.GetDislayNoteHeadersWithResponses(MyClient, fileNumber, notenum).ToList();
                var h0 = hlist[0];
                DateTime cd0 = h0.LastEdited.ToLocalTime();
                string when0 = "  " + cd0.ToShortTimeString() + " " + cd0.ToShortDateString();
                Console.WriteLine("Subject: " + h0.NoteSubject + "  By: " + h0.AuthorName + when0);


                var h = hlist[respnum];
                DateTime cd = h.LastEdited.ToLocalTime();
                string when = "  " + cd.ToShortTimeString() + " " + cd.ToShortDateString();
                Console.WriteLine("Subject: " + h.NoteSubject + "  By: " + h.AuthorName + when);
            }

            //Console.WriteLine("Note " + notenum + " Content:");
            Console.WriteLine();

            NoteContent cont = Actions.GetNoteContent(MyClient, fileNumber, notenum, respnum);

            Console.WriteLine();
            Console.WriteLine(cont.NoteBody);

            return 3;
        }
    }
}
