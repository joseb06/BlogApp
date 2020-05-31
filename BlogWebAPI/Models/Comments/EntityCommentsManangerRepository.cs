using BlogDBSQLServer.Models;
using BlogWebAPI.Services;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace BlogWebAPI.Models.Comments
{
    public class EntityCommentsManangerRepository : ICommentsManagerRepository
    {
        readonly dbBlogEntities blogdb = new dbBlogEntities();
        public comments Create(int postId, Comment commentToCreate)
        {
            int loginUserId = new UsersManagerServices().GetLoginUserId();

            var post = blogdb.posts.Find(postId);
            if (post != null)
            {
                var comment = new comments
                {
                    PostId = post.Id,
                    CommentaristId = loginUserId,
                    Comment = commentToCreate.Comments
                };

                blogdb.Entry(comment).State = EntityState.Added;
                blogdb.SaveChanges();

                return comment;
            }
            return null;
        }

        public bool Delete(int postId, int commentId)
        {
            int loginUserId = new UsersManagerServices().GetLoginUserId();
            var comment = (from comments in blogdb.comments
                           where comments.Id == commentId &&
                                 comments.PostId == postId &&
                                 comments.CommentaristId == loginUserId
                           select comments).SingleOrDefault();
            
            if (comment != null)
            {
                blogdb.Entry(comment).State = EntityState.Deleted;
                blogdb.SaveChanges();
                return true;
            }
            return false;
        }

        public comments Edit(int postId, int commentId, Comment commentToEdit)
        {
            int loginUserId = new UsersManagerServices().GetLoginUserId();
            var comment = (from comments in blogdb.comments
                           where comments.PostId == postId &&
                                 comments.Id == commentId &&
                                 comments.CommentaristId == loginUserId
                           select comments).SingleOrDefault();

            if (comment != null)
            {
                comment.Comment = commentToEdit.Comments;

                blogdb.Entry(comment).State = EntityState.Modified;
                blogdb.SaveChanges();
            }
            return comment;
        }

        public IEnumerable<object> GetPostComments(int postId)
        {
            var postComments = from p in blogdb.posts
                               where p.Id == postId
                               select new
                               {
                                   idPost = p.Id,
                                   p.Content,
                                   comments = (from comments in blogdb.comments
                                               where comments.PostId == postId
                                               select new
                                               {
                                                   userId = comments.CommentaristId,
                                                   comments.Comment
                                               }).ToList()
                               };
            return postComments.ToArray();
        }
    }
}