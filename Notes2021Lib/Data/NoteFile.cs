using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace Notes2021Lib.Data
{
    public class NoteFile
    {
        // Identity of the file
        [Required]
        [Key]
        [PersonalData]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [PersonalData]
        public int NumberArchives { get; set; }

        [Required]
        [Display(Name = "Owner ID")]
        [PersonalData]
        [StringLength(450)]
        public string OwnerId { get; set; }

        [ForeignKey("OwnerId")]
        [PersonalData]
        public UserAuxData Owner { get; set; }

        // file name of the file
        [Required]
        [StringLength(20)]
        [Display(Name = "NoteFile Name")]
        [PersonalData]
        public string NoteFileName { get; set; }

        // title of the file
        [Required]
        [StringLength(200)]
        [Display(Name = "NoteFile Title")]
        [PersonalData]
        public string NoteFileTitle { get; set; }

        // when anything in the file was last created or edited
        [Required]
        [Display(Name = "Last Edited")]
        [PersonalData]
        public DateTime LastEdited { get; set; }

        public List<NoteHeader> NoteHeaders { get; set; }

    }
}
