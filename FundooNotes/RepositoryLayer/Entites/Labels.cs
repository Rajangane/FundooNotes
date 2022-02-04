using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace RepositoryLayer.Entites
{
    public class Labels
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long LabelID { get; set; }
        public string LabelName { get; set; }
        [ForeignKey("notes")]
        public long NoteID { get; set; }
        [ForeignKey("user")]
        public long Id { get; set; }
        public virtual User user { get; set; }
        public virtual Notes notes { get; set; }

    }
}
