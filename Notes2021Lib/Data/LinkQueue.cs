using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace Notes2021Lib.Data
{
    public enum LinkAction
    {
        CreateBase,
        CreateResponse,
        Edit,
        Delete
    };

    public class LinkQueue
    {
        [Required]
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }

        [Required]
        public int LinkedFileId { get; set; }

        [Required]
        [StringLength(100)]
        public string LinkGuid { get; set; }

        [Required]
        public LinkAction Activity { get; set; }

        [Required]
        public string BaseUri { get; set; }

        public bool Enqueued { get; set; }
        }
}
