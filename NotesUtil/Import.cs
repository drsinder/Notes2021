using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Microsoft.EntityFrameworkCore;
using Notes2021Lib.Data;
using Notes2021Lib.Manager;
using System.IO;
using System.Linq;
using System.Threading.Tasks;



namespace NotesUtil
{
    public partial class Import : Form
    {
        public Import()
        {
            InitializeComponent();
        }

        private async void comboBox1_Layout(object sender, LayoutEventArgs e)
        {
            IConfiguration config = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", true, true)
                .Build();

            var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();
            optionsBuilder.UseSqlServer(config["ConnectionString"]);

            ApplicationDbContext _db = new ApplicationDbContext(optionsBuilder.Options);

            List<NoteFile> nfl = await _db.NoteFile.ToListAsync();
            
            foreach (NoteFile nf in nfl)
            {
                comboBox1.Items.Add(nf.NoteFileName);
            }

        }

        private void label1_Click(object sender, EventArgs e)
        {
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string notefilename = comboBox1.SelectedItem.ToString();
            string filename = textBox1.Text;

            Importer.myTextBox = textBox2;

            IConfiguration config = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", true, true)
                .Build();

            var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();
            optionsBuilder.UseSqlServer(config["ConnectionString"]);

            ApplicationDbContext _db = new ApplicationDbContext(optionsBuilder.Options);

            NoteFile nf = _db.NoteFile.Where(p => p.NoteFileName == notefilename).SingleOrDefault();

            if (nf == null)
            {
                textBox2.Text = ("Note file" + notefilename + " not found!");
                return;
            }

            StreamReader file;
            try
            {
                file = new StreamReader(filename);
            }
            catch
            {
                
                textBox2.Text = ("File " + filename + " not found!");
                return;
            }

            file.Close();

            button1.Enabled = false;


            Importer imp = new Importer();
           
            imp.Import(_db, filename, notefilename);

            //button1.Enabled = true;
        }
    }
}

