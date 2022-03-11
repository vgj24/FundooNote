//-----------------------------------------------------------------------
// <copyright file="LabelBL.cs" company="Vrushali">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace BusinessLayer.Services
{
    using System;
    using global::BusinessLayer.Interfaces;
    using RepositoryLayer.Entity;
    using RepositoryLayer.Interfaces;
    using System.Collections.Generic;
    using System.Text;
    public class LabelBL : ILabelBL
    {
        private readonly ILabelRL labelRL;

        /// <summary>
        /// Initializes a new instance of the <see cref="LabelBL"/> class.
        /// </summary>
        /// <param name="labelRL">The label rl.</param>
        public LabelBL(ILabelRL labelRL)
        {
            this.labelRL = labelRL;
        }


        /// <summary>
        /// Adds the label.
        /// </summary>
        /// <param name="labelName">Name of the label.</param>
        /// <param name="userId">The user identifier.</param>
        /// <param name="noteId">The note identifier.</param>
        /// <returns>Addslabel</returns>
        public label AddLabel(string labelName, long userId, long noteId)
        {
            try
            {

                return labelRL.AddLabel(labelName, userId, noteId);
            }
            catch (Exception)
            {

                throw;
            }
        }

        /// <summary>
        /// Updatelabels the specified label name.
        /// </summary>
        /// <param name="labelName">Name of the label.</param>
        /// <param name="userId">The user identifier.</param>
        /// <param name="labelId">The label identifier.</param>
        /// <returns>Updatelabel</returns>
        public label Updatelabel(string labelName, long userId, long labelId)
        {
            try
            {

                return labelRL.Updatelabel(labelName,userId,labelId);
            }
            catch (Exception)
            {

                throw;
            }

        }

        /// <summary>
        /// Deletelabels the specified label identifier.
        /// </summary>
        /// <param name="labelId">The label identifier.</param>
        /// <returns>boolvalue</returns>
        public bool Deletelabel(long labelId)
        {
            try
            {

                return labelRL.Deletelabel(labelId);
            }
            catch (Exception)
            {

                throw;
            }

        }
        /// <summary>
        /// Gets all labelby userid.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <returns>list</returns>
        public IEnumerable<label> GetAllLabelbyUserid(long userId)
        {
            try
            {

                return labelRL.GetAllLabelbyUserid(userId);
            }
            catch (Exception)
            {

                throw;
            }

        }

        /// <summary>
        /// Gets all labelby noteid.
        /// </summary>
        /// <param name="noteId">The note identifier.</param>
        /// <returns>list</returns>
        public IEnumerable<label> GetAllLabelbyNoteid(long noteId)

        {
            try
            {

                return labelRL.GetAllLabelbyNoteid(noteId);

            }
            catch (Exception)
            {

                throw;
            }
        }

        /// <summary>
        /// Gets the label table data.
        /// </summary>
        /// <returns>List</returns>
        public IEnumerable<label> GetLabelTableData()
        {
            try
            {
                return labelRL.GetLabelTableData();

            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
