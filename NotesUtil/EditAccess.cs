using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Notes2021Lib.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace NotesUtil
{
    public partial class EditAccess : Form
    {
        public EditAccess()
        {
            InitializeComponent();
        }

        private void checkedListBox1_Layout(object sender, LayoutEventArgs e)
        {

        }

        private async void comboBox1_Layout(object sender, LayoutEventArgs e)
        {
            IConfiguration config = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", true, true)
                .Build();

            var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();
            optionsBuilder.UseSqlServer(config["ConnectionString"]);

            ApplicationDbContext _db = new ApplicationDbContext(optionsBuilder.Options);

            List<NoteFile> nfl = await _db.NoteFile
                .OrderBy(p => p.NoteFileName)
                .ToListAsync();

            if (comboBox1.Items.Count == 0)
            {
                foreach (NoteFile nf in nfl)
                {
                    comboBox1.Items.Add(nf.NoteFileName);
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string notefilename = comboBox1.SelectedItem.ToString();

            IConfiguration config = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", true, true)
                .Build();

            var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();
            optionsBuilder.UseSqlServer(config["ConnectionString"]);

            ApplicationDbContext _db = new ApplicationDbContext(optionsBuilder.Options);

            NoteFile nf = _db.NoteFile.Where(p => p.NoteFileName == notefilename).SingleOrDefault();




        }
    }
}
