using Notes2021.Models;
using Notes2021Lib.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace Notes2021Client
{
    public partial class CreateNote : Form
    {
        private NoteFile MyFile;
        private readonly long MybaseId;

        private readonly NoteHeader MyHeader;
        private readonly NoteContent MyContent;
        private readonly IRelistAble MyParent;
        private IEnumerable<Tags> MyTags;

        private string currentFile;
        private int checkPrint;

        private MarkupConverter.MarkupConverter markupConverter = new MarkupConverter.MarkupConverter();

        public CreateNote(NoteFile myfile, long baseId, NoteHeader noteHeader, NoteContent noteContent, IEnumerable<Tags> noteTags, IRelistAble myParent)
        {
            InitializeComponent();

            currentFile = "";

            MyFile = myfile;
            MybaseId = baseId;
            MyParent = myParent;

            MyHeader = noteHeader;
            MyContent = noteContent;
            MyTags = noteTags;

            if (MyHeader != null)
            {
                this.Text = @"Edit Note";
                textBoxSubject.Text = MyHeader.NoteSubject;
                textBoxDirMessage.Text = MyContent.DirectorMessage;
                // tags

                rtbDoc.Rtf = MarkupConverter.HtmlToRtfConverter.ConvertHtmlToRtf(MyContent.NoteBody);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            TextViewModel tv = new TextViewModel()
            {
                NoteID = 0,
                DirectorMessage = textBoxDirMessage.Text,
                MySubject = textBoxSubject.Text,
                TagLine = textBoxTags.Text,
                MyNote = markupConverter.ConvertRtfToHtml(rtbDoc.Rtf),
                NoteFileID = MyFile.Id,
                BaseNoteHeaderID = MybaseId
            };

            if (MyHeader != null)
            {
                Actions.EditNote(Program.MyClient, tv, MyHeader.Id);
            }
            else
            {
                Actions.CreateNote(Program.MyClient, tv);
            }

            Thread.Sleep(400);

            MyParent.Relist();

            Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
