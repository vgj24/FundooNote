//-----------------------------------------------------------------------
// <copyright file="UserNotesData.cs" company="Vrushali">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace CommonLayer.Model
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    public class UserNotesData
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime Remainder { get; set; }
        public string Color { get; set; }
        public string Image { get; set; }
        public bool IsArchive { get; set; }
        public bool IsTrash { get; set; }
        public bool IsPin { get; set; }
        public DateTime? CreateAt { get; set; }
        public DateTime? ModifiedAt { get; set; }
    }
}
