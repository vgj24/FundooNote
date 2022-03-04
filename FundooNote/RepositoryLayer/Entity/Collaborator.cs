﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace RepositoryLayer.Entity
{
    public class Collaborator
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long CollabId { get; set; }
        public string CollabEmail { get; set; }
        [ForeignKey("user")]  //referncing user
        public long Id { get; set; }
        public User user { get; set; }

        [ForeignKey("note")]  //referncing user
        public long NotesId { get; set; }
        public Notes note { get; set; }


    }
}
