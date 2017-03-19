using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using OpenGameList.Data.Users;
using OpenGameList.Data.Comments;

namespace OpenGameList.Data.Items
{
    public class Item
    {
        public Item()
        {

        }

        [Key]
        [Required]
        public int Id { get; set; }

        [Required]
        public string Title { get; set; }

        public string Description { get; set; }

        public string Text { get; set; }

        public string Notes { get; set; }

        [Required]
        public int Type { get; set; }

        [Required]
        public int Flags { get; set; }

        [Required]
        public string UserId { get; set; }

        [Required]
        public int ViewCount { get; set; }

        [Required]
        public DateTime CreateDate { get; set; }

        [Required]
        public DateTime LastModifiedDated { get; set; }

        /// <summarey>
        /// Current Item's Author: this property will be loaded on first use using EF's Lazy-Loading feature.
        /// </summarey>
        [ForeignKey("UserId")]
        public virtual ApplicationUser Author { get; set; }

        ///<summary>
        ///Current Item's Comment list: this property will be loaded on first use using EF's Lazy-Loading feature.
        /// </summary>
        public virtual List<Comment> Comments { get; set; }
    }
}
