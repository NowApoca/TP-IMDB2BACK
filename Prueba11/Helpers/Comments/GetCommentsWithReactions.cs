using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Prueba11.Context;
using Prueba11.Models;

namespace Prueba11.Helpers
{
    public class GetCommentsWithReactions
    {
        
        public static CommentWithReactions[] getCelebrity(EscuelaDatabaseContext _context, CommentCelebrity[] comments, Session session)
        {
            CommentWithReactions[] commentWithReactions = new CommentWithReactions[1000];
            int currentIndex = 0;
            foreach (CommentCelebrity comment in comments)
            {
                int likes = _context.ReactionCommentCelebritys.Where(reaction => reaction.commentCelebrityId == comment.id && reaction.reactionType == "LIKE").Count();
                int favourites = _context.ReactionCommentCelebritys.Where(reaction => reaction.commentCelebrityId == comment.id && reaction.reactionType == "FAVOURITE").Count();
                int smiles = _context.ReactionCommentCelebritys.Where(reaction => reaction.commentCelebrityId == comment.id && reaction.reactionType == "SMILE").Count();
                int frownes = _context.ReactionCommentCelebritys.Where(reaction => reaction.commentCelebrityId == comment.id && reaction.reactionType == "FROWN").Count();
                bool likedByUser = false;
                bool favouritedByUser = false;
                bool smiledByUser = false;
                bool frownedByUser = false;
                if (session != null)
                {
                    ReactionCommentCelebrity isLikedByUserElement = _context.ReactionCommentCelebritys.Where(reaction => reaction.userName == session.userName && reaction.commentCelebrityId == comment.id && reaction.reactionType == "LIKE").FirstOrDefault();
                    ReactionCommentCelebrity isFavouritedByUserElement = _context.ReactionCommentCelebritys.Where(reaction => reaction.userName == session.userName && reaction.commentCelebrityId == comment.id && reaction.reactionType == "FAVOURITE").FirstOrDefault();
                    ReactionCommentCelebrity isSmiledByUserElement = _context.ReactionCommentCelebritys.Where(reaction => reaction.userName == session.userName && reaction.commentCelebrityId == comment.id && reaction.reactionType == "SMILE").FirstOrDefault();
                    ReactionCommentCelebrity isFrownedByUserElement = _context.ReactionCommentCelebritys.Where(reaction => reaction.userName == session.userName && reaction.commentCelebrityId == comment.id && reaction.reactionType == "FROWN").FirstOrDefault();
                    if (isLikedByUserElement != null) { likedByUser = true; }
                    if (isFavouritedByUserElement != null) { favouritedByUser = true; }
                    if (isSmiledByUserElement != null) { smiledByUser = true; }
                    if (isFrownedByUserElement != null) { frownedByUser = true; }
                }
                commentWithReactions[currentIndex] = new CommentWithReactions()
                {
                    id = comment.id,
                    userName = comment.userName,
                    comment = comment.comment,
                    isDeleted = comment.isDeleted,
                    likes = likes,
                    favourites = favourites,
                    smiles = smiles,
                    frownes = frownes,
                    isLikedByUser = likedByUser,
                    isFavouritedByUser = favouritedByUser,
                    isSmiledByUser = smiledByUser,
                    isFrownedByUser = frownedByUser
                };
                currentIndex++;
            }
            return commentWithReactions;
        }
        public static CommentWithReactions[] get(EscuelaDatabaseContext _context, CommentItem[] comments, Session session)
        {
            CommentWithReactions[] commentWithReactions = new CommentWithReactions[1000];
            int currentIndex = 0;
            foreach(CommentItem comment in comments)
            {
                int likes = _context.ReactionCommentItems.Where(reaction => reaction.commentItemId == comment.id && reaction.reactionType == "LIKE" ).Count();
                int favourites = _context.ReactionCommentItems.Where(reaction => reaction.commentItemId == comment.id && reaction.reactionType == "FAVOURITE").Count();
                int smiles = _context.ReactionCommentItems.Where(reaction => reaction.commentItemId == comment.id && reaction.reactionType == "SMILE").Count();
                int frownes = _context.ReactionCommentItems.Where(reaction => reaction.commentItemId == comment.id && reaction.reactionType == "FROWN").Count();
                bool likedByUser = false;
                bool favouritedByUser = false;
                bool smiledByUser = false;
                bool frownedByUser = false;
                if (session != null)
                {
                    ReactionCommentItem isLikedByUserElement = _context.ReactionCommentItems.Where(reaction => reaction.userName == session.userName && reaction.commentItemId == comment.id && reaction.reactionType == "LIKE").FirstOrDefault();
                    ReactionCommentItem isFavouritedByUserElement = _context.ReactionCommentItems.Where(reaction => reaction.userName == session.userName && reaction.commentItemId == comment.id && reaction.reactionType == "FAVOURITE").FirstOrDefault();
                    ReactionCommentItem isSmiledByUserElement = _context.ReactionCommentItems.Where(reaction => reaction.userName == session.userName && reaction.commentItemId == comment.id && reaction.reactionType == "SMILE").FirstOrDefault();
                    ReactionCommentItem isFrownedByUserElement = _context.ReactionCommentItems.Where(reaction => reaction.userName == session.userName && reaction.commentItemId == comment.id && reaction.reactionType == "FROWN").FirstOrDefault();
                    if (isLikedByUserElement != null) { likedByUser = true; }
                    if (isFavouritedByUserElement != null) { favouritedByUser = true; }
                    if (isSmiledByUserElement != null) { smiledByUser = true; }
                    if (isFrownedByUserElement != null) { frownedByUser = true; }
                }
                commentWithReactions[currentIndex] = new CommentWithReactions()
                {
                    id = comment.id,
                    userName = comment.userName,
                    comment = comment.comment,
                    isDeleted = comment.isDeleted,
                    likes = likes,
                    favourites = favourites,
                    smiles = smiles,
                    frownes = frownes,
                    isLikedByUser = likedByUser,
                    isFavouritedByUser = favouritedByUser,
                    isSmiledByUser = smiledByUser,
                    isFrownedByUser = frownedByUser
                };
                currentIndex++;
            }
            return commentWithReactions;
        }
    }
}
