//-----------------------------------------------------------------------
// <copyright file="Collaborator.cs" company="Vrushali">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace RepositoryLayer.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Text;


    public class Collaborator
    {
        /// <summary>
        /// Gets or sets the collab identifier.
        /// </summary>
        /// <value>
        /// The collab identifier.
        /// </value>
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]

        public long CollabId { get; set; }
        public string CollabEmail { get; set; }
        [ForeignKey("user")]  //referncing user
        public long Id { get; set; }
        public User user { get; set; }

        /// <summary>
        /// Gets or sets the notes identifier.
        /// </summary>
        /// <value>
        /// The notes identifier.
        /// </value>
        [ForeignKey("note")]  
        public long NotesId { get; set; }
        public Notes note { get; set; }


    }
}
