using Notes2021Lib.Data;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Notes2021Client
{
    public partial class ListNotes : Form, IRelistAble
    {
        private readonly NoteFile MyFile;
        private NoteAccess MyAccess;
        private readonly ListNotes mySelf;

        StringFormat strFormat; //Used to format the grid rows.
        ArrayList arrColumnLefts = new ArrayList();//Used to save left coordinates of columns
        ArrayList arrColumnWidths = new ArrayList();//Used to save column widths
        int iCellHeight; //Used to get/set the datagridview cell height
        int iTotalWidth; //
        int iRow;//Used as counter
        bool bFirstPage; //Used to check whether we are printing first page
        bool bNewPage;// Used to check whether we are printing a new page
        int iHeaderHeight; //Used for the header height

        public ListNotes(NoteFile myFile)
        {
            InitializeComponent();
            mySelf = this;

            MyFile = myFile;

            label1.Text = MyFile.NoteFileName;
            label2.Text = MyFile.NoteFileTitle;

            List<NoteHeader> noteHeader = Actions.GetNoteList(Program.MyClient, MyFile.Id);
            MyAccess = Actions.GetAccess(Program.MyClient, MyFile.Id);

            button1.Enabled = MyAccess.Write;

            int count = 0;
            foreach (var header in noteHeader)
            {
                header.CreateDate = header.CreateDate.ToLocalTime();
                count++;
            }

            label3.Text = count + @"  Base Notes";

            dataGridView1.DataSource = noteHeader;

            dataGridView1.Columns[0].Visible = false;
            dataGridView1.Columns[1].Visible = false;
            dataGridView1.Columns[2].Visible = false;
            dataGridView1.Columns[3].Visible = false;
            dataGridView1.Columns[4].Visible = false;
            dataGridView1.Columns[6].Visible = false;
            dataGridView1.Columns[9].Visible = false;
            dataGridView1.Columns[10].Visible = false;
            dataGridView1.Columns[12].Visible = false;
            dataGridView1.Columns[14].Visible = false;
            dataGridView1.Columns[15].Visible = false;

            dataGridView1.Columns[5].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dataGridView1.Columns[7].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dataGridView1.Columns[8].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dataGridView1.Columns[11].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dataGridView1.Columns[13].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
        }

        private void dataGridView1_RowHeaderMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            NoteHeader myHeader = (NoteHeader)dataGridView1.Rows[e.RowIndex].DataBoundItem;

            //new DisplayNote(MyFile, myHeader.NoteOrdinal, this)
            //{
            //    Visible = true
            //};
        }

        private void dataGridView1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode != Keys.Enter)
                return;
            var dataGridViewRow = ((DataGridView)(sender)).CurrentRow;
            if (dataGridViewRow != null)
            {
                NoteHeader myHeader = (NoteHeader)dataGridViewRow.DataBoundItem;
                //new DisplayNote(MyFile, myHeader.NoteOrdinal, this)
                //{
                //    Visible = true
                //};
            }
        }

        private void dataGridView1_ColumnWidthChanged(object sender, DataGridViewColumnEventArgs e)
        {
            mySelf.Width = WinObjFunctions.CountGridWidth(dataGridView1) + 90;
        }


        public void Relist()
        {
            List<NoteHeader> noteHeader = Actions.GetNoteList(Program.MyClient, MyFile.Id);
            MyAccess = Actions.GetAccess(Program.MyClient, MyFile.Id);

            button1.Enabled = MyAccess.Write;

            int count = 0;
            foreach (var header in noteHeader)
            {
                header.CreateDate = header.CreateDate.ToLocalTime();
                count++;
            }

            label3.Text = count + @"  Base Notes";

            dataGridView1.DataSource = noteHeader;

            dataGridView1.Refresh();
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {

        }

        private void button5_Click(object sender, EventArgs e)
        {

        }
    }
}
