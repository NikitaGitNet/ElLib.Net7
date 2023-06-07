using System;
using System.ComponentModel.DataAnnotations;

namespace ElLib.Net7.Models.Comment
{
    public class AddCommentModel
    {
        public Guid Id { get; set; }
        public string CommentText { get; set; }
        public Guid BookId { get; set; }
        public string UserEmail { get; set; }
        public string UserName { get; set; }
        public string UserId { get; set; }
        public DateTime? CreatedDate { get; set; }
        public double Rating { get; set; }
    }
}
