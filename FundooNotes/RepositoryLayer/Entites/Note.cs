using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace RepositoryLayer.Entites
{
    public class Note
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long NoteId { get; set; }
        
        [ForeignKey("User")]
        public long Id { get; set; }
        public virtual User User { get; set; }

        public string Title { get; set; }
        public string Message { get; set; }
        public DateTime Remainder { get; set; }
        public string Color { get; set; }
        public string Image { get; set; }
        public bool IsArchive { get; set; }

        public bool IsPin { get; set; }
        public bool IsTrash { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime? Createat { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime? Modifiedat { get; set; }

    }
}

