using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace RepositoryLayer.Entites
{
    public class Notes
    {
    
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long NoteId { get; set; }

        [ForeignKey("user")]
        public long Id { get; set; }
        public virtual User user { get; set; }

        public string Title { get; set; }

        public string Message { get; set; }

        public DateTime Remainder { get; set; }


        public string Color { get; set; }

        public string Image { get; set; }

        public bool IsArchive { get; set; }

        public bool IsPin { get; set; }

        public bool IsTrash { get; set; }
        public DateTime? Createat { get; set; }
        public DateTime? Modifiedat { get; set; }

    }
}

