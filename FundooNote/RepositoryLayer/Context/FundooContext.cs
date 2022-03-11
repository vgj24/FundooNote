//-----------------------------------------------------------------------
// <copyright file="FundooContext.cs" company="Vrushali">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace RepositoryLayer.Context
{
    using System;
    using Microsoft.EntityFrameworkCore;
    using RepositoryLayer.Entity;
    using System.Collections.Generic;
    using System.Text;

    public class FundooContext : DbContext
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="FundooContext"/> class.
        /// </summary>
        /// <param name="options">The options for this context.</param>
        public FundooContext(DbContextOptions options) : base(options)
        {
        }

        /// <summary>
        /// Gets or sets the user table.
        /// </summary>
        /// <value>
        /// The user table.
        /// </value>
        public DbSet<User> UserTable { get; set; }

        /// <summary>
        /// Gets or sets the notes table.
        /// </summary>
        /// <value>
        /// The notes table.
        /// </value>
        public DbSet<Notes> NotesTable { get; set; }

        /// <summary>
        /// Gets or sets the collborator table.
        /// </summary>
        /// <value>
        /// The collborator table.
        /// </value>
        public DbSet<Collaborator> CollboratorTable { get; set; }


        /// <summary>
        /// Gets or sets the label table.
        /// </summary>
        /// <value>
        /// The label table.
        /// </value>
        public DbSet<label> LabelTable { get; set; }

    }
}
