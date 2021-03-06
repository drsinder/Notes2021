﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace Notes2021.Data
{
    public class Mark
    {
        [Required]
        [Column(Order = 0)]
        [StringLength(450)]
        public string UserId { get; set; }

        [Required]
        [Column(Order = 1)]
        public int NoteFileId { get; set; }

        [Required]
        [Column(Order = 2)]
        public int ArchiveId { get; set; }

        [Required]
        [Column(Order = 3)]
        public int MarkOrdinal { get; set; }

        [Required]
        public int NoteOrdinal { get; set; }

        [Required]
        public long NoteHeaderId { get; set; }

        [Required]
        public int ResponseOrdinal { get; set; }  // -1 == whole string, 0 base note only, > 0 Response

        [ForeignKey("NoteFileId")]
        // ReSharper disable once UnusedMember.Global
        public NoteFile NoteFile { get; set; }
    }
}
