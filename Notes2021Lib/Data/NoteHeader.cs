﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Notes2021Lib.Data
{
    public class NoteHeader
    {
        // Uniquely identifies the note
        [Required]
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }

        // The fileid the note belongs to
        [Required]
        public int NoteFileId { get; set; }

        [ForeignKey("NoteFileId")]
        // ReSharper disable once UnusedAutoPropertyAccessor.Global
        public NoteFile NoteFile { get; set; }

        [Required]
        public int ArchiveId { get; set; }

        public long BaseNoteId { get; set; }

        // the ordinal on a Base note and all its responses
        [Required]
        [Display(Name = "Note #")]
        public int NoteOrdinal { get; set; }

        // The ordinal of the response where 0 is a Base Note
        [Required]
        [Display(Name = "Response #")]
        public int ResponseOrdinal { get; set; }

        // Subject/Title of a note
        [Required]
        [StringLength(200)]
        [Display(Name = "Subject")]
        public string NoteSubject { get; set; }

        // When the note was created or last edited
        [Required]
        [Display(Name = "Last Edited")]
        public DateTime LastEdited { get; set; }

        // When the thread was last edited
        [Required]
        [Display(Name = "Thread Last Edited")]
        public DateTime ThreadLastEdited { get; set; }

        [Required]
        [Display(Name = "Created")]
        public DateTime CreateDate { get; set; }

        // Meaningful only if ResponseOrdinal = 0
        [Required]
        public int ResponseCount { get; set; }

        // ReSharper disable once InconsistentNaming
        [StringLength(450)]
        public string AuthorID { get; set; }

        [Required]
        [StringLength(50)]
        public string AuthorName { get; set; }

        [StringLength(100)]
        public string LinkGuid { get; set; }

        public NoteContent NoteContent { get; set; }

        public List<Tags> Tags { get; set; }

        public NoteHeader CloneForLink()
        {
            NoteHeader nh = new NoteHeader()
            {
                Id = Id,
                NoteSubject = NoteSubject,
                LastEdited = LastEdited,
                ThreadLastEdited = ThreadLastEdited,
                CreateDate = CreateDate,
                AuthorID = AuthorID,
                AuthorName = AuthorName,
                LinkGuid = LinkGuid
            };

            return nh;
        }

        public NoteHeader CloneForLinkR()
        {
            NoteHeader nh = new NoteHeader()
            {
                Id = Id,
                NoteSubject = NoteSubject,
                LastEdited = LastEdited,
                ThreadLastEdited = ThreadLastEdited,
                CreateDate = CreateDate,
                AuthorID = AuthorID,
                AuthorName = AuthorName,
                LinkGuid = LinkGuid,
                ResponseOrdinal = ResponseOrdinal
            };

            return nh;
        }

        //#region IDisposable Support
        //private bool disposedValue = false; // To detect redundant calls
        //SafeHandle handle = new SafeFileHandle(IntPtr.Zero, true);

        //protected virtual void Dispose(bool disposing)
        //{
        //    if (!disposedValue)
        //    {
        //        if (disposing)
        //            return;

        //        if (disposing)
        //        {
        //            handle.Dispose();
        //            // Free any other managed objects here.
        //            //
        //        }

        //        disposedValue = true;
        //    }
        //}

        ////TODO: override a finalizer only if Dispose(bool disposing) above has code to free unmanaged resources.
        //~NoteHeader()
        //{
        //    // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
        //    Dispose(false);
        //}

        //// This code added to correctly implement the disposable pattern.
        //public void Dispose()
        //{
        //    Dispose(true);
        //    GC.SuppressFinalize(this);
        //}
        //#endregion
    }
}
