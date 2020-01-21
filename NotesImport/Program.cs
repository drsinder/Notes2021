using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Notes2021Lib.Data;
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

            IConfiguration config = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", true, true)
                .Build();

            var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();
            optionsBuilder.UseSqlServer(config["ConnectionString"]);

            ApplicationDbContext _db = new ApplicationDbContext(optionsBuilder.Options);

            await imp.Import(_db, myFile, myNotes);


        }
    }

    public partial class Importer : Notes2021Lib.Import.Importer
    {
        public override void Output(string message)
        {
            Console.WriteLine(message);
        }
    }

}
