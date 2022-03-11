// -----------------------------------------------------------------------
// <copyright file="ILabelBL.cs" company="Vrushali">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace BusinessLayer.Interfaces
{
    using System;
    using RepositoryLayer.Entity;
    using System.Collections.Generic;
    using System.Text;
    public interface ILabelBL
    {
        /// <summary>
        /// Adds the label.
        /// </summary>
        /// <param name="labelName">Name of the label.</param>
        /// <param name="userId">The user identifier.</param>
        /// <param name="noteId">The note identifier.</param>
        /// <returns>label</returns>
        public label AddLabel(string labelName, long userId, long noteId);
        /// <summary>
        /// Updatelabels the specified label name.
        /// </summary>
        /// <param name="labelName">Name of the label.</param>
        /// <param name="userId">The user identifier.</param>
        /// <param name="labelId">The label identifier.</param>
        /// <returns>updatelabel</returns>
        public label Updatelabel(string labelName, long userId, long labelId);

        /// <summary>
        /// Deletelabels the specified label identifier.
        /// </summary>
        /// <param name="labelId">The label identifier.</param>
        /// <returns>booleanvalue</returns>
        public bool Deletelabel(long labelId);

        /// <summary>
        /// Gets all labelby userid.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <returns>list</returns>
        public IEnumerable<label> GetAllLabelbyUserid(long userId);

        /// <summary>
        /// Gets all labelby noteid.
        /// </summary>
        /// <param name="noteId">The note identifier.</param>
        /// <returns>list</returns>
        public IEnumerable<label> GetAllLabelbyNoteid(long noteId);

        /// <summary>
        /// Gets the label table data.
        /// </summary>
        /// <returns>list</returns>
        public IEnumerable<label> GetLabelTableData();

    }
}
