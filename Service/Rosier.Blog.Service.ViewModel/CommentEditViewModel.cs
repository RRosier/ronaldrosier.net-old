using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace Rosier.Blog.Service.ViewModel
{
    /// <summary>
    /// View model for comments entering
    /// </summary>
    public class CommentEditViewModel
    {
        public CommentEditViewModel() { }
        public CommentEditViewModel(string entryID, string permaLink)
        {
            this.EntryID = entryID;
            this.Permalink = permaLink;
        }

        public string EntryID { get; set; }
        public string Permalink { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 2)]
        [Display(Name = "Display Name", Description = "This is how your name will be displayed")]
        public string DisplayName { get; set; }
        [DataType(DataType.Url)]
        [Display(Name = "Home page")]
        public string Website { get; set; }

        [Display(Description = "Used to get your avatar")]
        [RegularExpression(@"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$", ErrorMessage = "Enter a valid email address")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required(ErrorMessage = "A content is required")]
        [DataType(DataType.MultilineText)]
        public string Content { get; set; }
    }
}
