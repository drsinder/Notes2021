using System;
using System.Threading.Tasks;

namespace NotesImport
{
    class Program
    {
        static async Task Main(string[] args)
        {
            if (args.Length != 2)
            {
                Console.WriteLine("Usage:  NoteImport full_path_of_text_file name_of_notefile");
                return;
            }

            string myFile = args[0];
            string myNotes = args[1];

            Importer imp = new Importer();

             await imp.Import(myFile, myNotes);


        }
    }
}
