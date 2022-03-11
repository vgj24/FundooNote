// -----------------------------------------------------------------------
// <copyright file="UserLogin.cs" company="Vrushali">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------

namespace CommonLayer.Model
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class UserLogin
    {
        //Login Validation Entity
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
