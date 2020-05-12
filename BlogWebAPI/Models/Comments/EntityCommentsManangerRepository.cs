using BlogDatabase.Models;
using BlogWebAPI.Services;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace BlogWebAPI.Models.Comments
{
    public class EntityCommentsManangerRepository : ICommentsManagerRepository
    {
        blogdbEntities blogdb = new blogdbEntities();
        public comments Create(int id, CommentsModel commentToCreate)
        {
            int loginUserId = new UsersManagerServices().GetLoginUserId();
            int lastCommentId = (!blogdb.comments.Any()) ? 0 : blogdb.comments.OrderByDescending(p => p.id).First().id;
            int nextCommentId = lastCommentId + 1;

            comments newComment = new comments
            {
                id = nextCommentId,
                id_post = id,
                id_commentarist = loginUserId,
                comment = commentToCreate.Comment
            };

            blogdb.Entry(newComment).State = EntityState.Added;
            blogdb.SaveChanges();

            return newComment;
        }

        public bool Delete(int id, int id2)
        {
            int loginUserId = new UsersManagerServices().GetLoginUserId();
            comments commentToDelete = (from c in blogdb.comments
                                        where c.id == id2 && c.id_post == id && c.id_commentarist == loginUserId
                                        select c).SingleOrDefault();
            blogdb.Entry(commentToDelete).State = EntityState.Deleted;
            blogdb.SaveChanges();
            return true;
        }

        public comments Edit(int id,int id2, CommentsModel commentToEdit)
        {
            int loginUserId = new UsersManagerServices().GetLoginUserId();
            comments newComment = (from c in blogdb.comments
                                   where c.id_post == id && c.id == id2 && c.id_commentarist == loginUserId
                                   select c).SingleOrDefault();
            if (newComment == null)
            {
                return null;
            }
            newComment.comment = commentToEdit.Comment;

            blogdb.Entry(newComment).State = EntityState.Modified;
            blogdb.SaveChanges();
            
            return newComment;
        }

        public IEnumerable<object> ListOfPostsAndComments(int id)
        {
            var postAndHisComments = from p in blogdb.posts
                                     where p.id == id
                                     select new
                                     {
                                         idPost = p.id,
                                         p.content,
                                         comments = (from c in blogdb.comments
                                                     where c.id_post == id
                                                     select new
                                                     {
                                                         userId = c.id_commentarist,
                                                         c.comment
                                                     }).ToList()
                                     };

            return postAndHisComments.ToArray();
        }
    }
}