using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Datingsida.Models
{
    public class MessageModel
    {
        [Key]
        public int MessageId { get; set; }
        [Required]
        public string Subjekt { get; set; }
        [Required]
        public string MessageText { get; set; }
        [ForeignKey ("ToId")]
        public string ToId { get; set; }
        [ForeignKey("FromId")]
        public string FromId { get; set; }
        public DateTime DateOfPost { get; set; }


    }
}
