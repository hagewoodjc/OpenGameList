using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using OpenGameList.Data.Items;
using OpenGameList.Data.Comments;
using OpenIddict.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace OpenGameList.Data.Users
{
    public class ApplicationUser : IdentityUser
    {

        public ApplicationUser()
        {

        }

        public string DisplayName { get; set; }

        public string Notes { get; set; }

        [Required]
        public int Type { get; set; }

        [Required]
        public int Flags { get; set; }

        [Required]
        public DateTime CreatedDate { get; set; }

        [Required]
        public DateTime LastModifiedDate { get; set; }

        ///<summary>
        ///A list of items written by this user: this property will be loaded on first use using EF's Lazy Loading feature.
        /// </summary>
        public virtual List<Item> Items { get; set; }

        ///<summary>
        ///A list of comments written by this user: this proeprty will be loaded on first use using EF's Lazy Loading feature.
        /// </summary>
        public virtual List<Comment> Comments { get; set; }
    }
}
