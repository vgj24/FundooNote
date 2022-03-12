//-----------------------------------------------------------------------
// <copyright file="LabelRL" company="Vrushali">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace RepositoryLayer.Services
{
    using System;
    using global::RepositoryLayer.Context;
    using global::RepositoryLayer.Entity;
    using global::RepositoryLayer.Interfaces;
    using System.Collections.Generic;
    using System.Linq;
    using CommonLayer;

    namespace RepositoryLayer.Services
    {
        public class LabelRL : ILabelRL
        {
            private readonly FundooContext fundooContext;

            /// <summary>
            /// Initializes a new instance of the <see cref="LabelRL"/> class.
            /// </summary>
            /// <param name="fundooContext">The fundoo context.</param>
            public LabelRL(FundooContext fundooContext)
            {
                this.fundooContext = fundooContext;
            }

            /// <summary>
            /// Adds the label.
            /// </summary>
            /// <param name="labelName">Name of the label.</param>
            /// <param name="userId">The user identifier.</param>
            /// <param name="noteId">The note identifier.</param>
            /// <returns>newlabel</returns>
            public label AddLabel(string labelName, long userId, long noteId)
            {
                try
                {

                    label newlabel = new label();
                    newlabel.LabelName = labelName;
                    newlabel.NotesId = noteId;
                    newlabel.Id = userId;
                    fundooContext.LabelTable.Add(newlabel);
                    int result = fundooContext.SaveChanges();
                    if (result > 0)
                    {
                        return newlabel;
                    }
                    else
                    {
                        //return null;
                        throw new AppException("label not added");

                    }
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
            /// <returns>updatedlabel</returns>
            public label Updatelabel(string labelName, long userId, long labelId)
            {
                try
                {
                    var result = fundooContext.LabelTable.Where(e => e.LabelId == labelId).FirstOrDefault();
                    if (result != null)
                    {
                        label newlabel = new label();
                        newlabel.LabelName = labelName;
                        fundooContext.LabelTable.Update(result);
                        fundooContext.SaveChanges();
                        return result;
                    }
                    else
                    {
                        //  return null; 
                        throw new AppException("Invalid labelId");


                    }
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
            /// <returns>deletelabel</returns>
            public bool Deletelabel(long labelId)
            {
                try
                {
                    var result = fundooContext.LabelTable.Where(e => e.LabelId == labelId).FirstOrDefault();
                    if (result != null)
                    {
                        fundooContext.LabelTable.Remove(result);
                        fundooContext.SaveChanges();
                        return true;
                    }
                    else
                    {
                        //  return false;
                        throw new AppException("Invalid labelid");

                    }
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
            /// <returns>listOfLabelbyUserid</returns>
            public IEnumerable<label> GetAllLabelbyUserid(long userId)
            {
                try
                {
                    var result = fundooContext.LabelTable.Where(e => e.Id == userId).ToList();
                    if (result != null)
                    {
                        return result;
                    }
                    else
                    {
                        //return null;
                        throw new AppException("Invalid userId");

                    }
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
            /// <returns>listoflabelbyNoteid</returns>
            public IEnumerable<label> GetAllLabelbyNoteid(long noteId)
            {
                try
                {
                    var result = fundooContext.LabelTable.Where(e => e.NotesId == noteId).ToList();
                    if (result != null)
                    {
                        return result;
                    }
                    else
                    {
                        //return null;
                        throw new AppException("invalid noteid ");

                    }
                }
                catch (Exception)
                {

                    throw;
                }
            }

            /// <summary>
            /// Gets the label table data.
            /// </summary>
            /// <returns>listofalllabel</returns>
            public IEnumerable<label> GetLabelTableData()
            {
                try
                {
                    var result = fundooContext.LabelTable.ToList();
                    if (result != null)
                    {
                        return result;
                    }
                    else
                    {
                        //return null;
                        throw new AppException("No Notes yet");

                    }
                }
                catch (Exception)
                {

                    throw;
                }
            }
        }   
    }
}
