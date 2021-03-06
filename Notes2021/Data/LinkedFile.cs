﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Notes2021.Data
{
    public class LinkedFile
    {
        [Required]
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public int HomeFileId { get; set; }

        [Required]
        [StringLength(20)]
        public string HomeFileName { get; set; }

        [Required]
        [StringLength(20)]
        public string RemoteFileName { get; set; }

        [Required]
        [StringLength(450)]
        public string RemoteBaseUri { get; set; }

        [Required]
        public bool AcceptFrom { get; set; }

        [Required]
        public bool SendTo { get; set; }
    }
}
