using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Prueba11.Context;
using Prueba11.Models;
using Prueba11.Helpers;

namespace Prueba11.Controllers
{
    public class CommentsController : Controller
    {
        private readonly EscuelaDatabaseContext _context;

        public CommentsController(EscuelaDatabaseContext context)
        {
            _context = context;
        }

        [HttpPost]
        [Route("comments")]
        public async Task<IActionResult> PostCreateComment([FromBody] PostCreateComment input, [FromHeader] HeadersHelper headers)
        { 
            int status;
            string error = null;
            IDictionary<string, object> data = new Dictionary<string, object>();
            Session session = _context.Sessions.Where(session => session.token == headers.Authorization).FirstOrDefault();
            if (session == null || session.expired)
            {
                data.Add("status", HttpConstants.UNAUTHORIZED);
                data.Add("error", "USUARIO NO IDENTIFICADO");
                return Json(data);
            }
            switch (input.type){
                case "celebrities":
                    Celebrity celebrity = _context.Celebrities.Where(celebrity => celebrity.id == input.entityId).FirstOrDefault();
                    CommentCelebrity parentCommentCelebrity = _context.CommentCelebrities.Where(comment => comment.id == input.parentCommentId).FirstOrDefault();
                    if (celebrity != null)
                    {
                        var comment = new CommentCelebrity()
                        {
                            comment = input.comment,
                            ParentCommentCelebrity = parentCommentCelebrity,
                            Celebrity = celebrity,
                            userName = session.userName
                        };
                        _context.CommentCelebrities.Add(comment);
                    }
                    else
                    {
                        status = 404;
                        error = "LA CELEBRIDAD A COMENTAR NO EXISTE";
                    }
                    break;
                case "items":
                    Item item = _context.Items.Where(item => item.id == input.entityId).FirstOrDefault();
                    CommentItem parentCommentItem= _context.CommentItems.Where(comment => comment.id == input.parentCommentId).FirstOrDefault();
                    if (item != null)
                    {
                        var comment = new CommentItem()
                        {
                            comment = input.comment,
                            ParentCommentItem = parentCommentItem,
                            Item = item,
                            userName = session.userName
                        };
                        _context.CommentItems.Add(comment);
                    }
                    else
                    {
                        status = 404;
                        error = "LA CELEBRIDAD A COMENTAR NO EXISTE";
                    }

                    break;
                default:
                    status = 404;
                    error = "TIPO INVALIDO: " + input.type;
                    break;
            }
            try
            {
                await _context.SaveChangesAsync();
                status = HttpConstants.RESOURCE_CREATED;
            }
            catch (IOException e)
            {
                Console.WriteLine(e.Message);
                status = 500;
                error = "ERROR INTERNO. VERIFIQUE LOGS.";
            }
            data.Add("status", status);
            data.Add("error", error);
            return Json(data);
        }

        [HttpDelete]
        [Route("comments/items/{id}")]
        public async Task<IActionResult> DeleteCommentItem(int id)
        {
            int status;
            string error = null;
            CommentItem commentItem = _context.CommentItems.Where(comment=> comment.id == id).FirstOrDefault();
            if (commentItem != null)
            {
                commentItem.isDeleted = true;
                try
                {
                    _context.CommentItems.Update(commentItem);
                    await _context.SaveChangesAsync();
                    status = HttpConstants.SUCCESS_NO_DATA;
                }
                catch (IOException e)
                {
                    Console.WriteLine(e.Message);
                    status = 500;
                    error = "ERROR INTERNO. VERIFIQUE LOGS.";
                }
            }
            else
            {
                status = HttpConstants.NOT_FOUND;
                error = "NO SE HA ENCONTRADO EL ITEM.";
            }
            IDictionary<string, object> data = new Dictionary<string, object>();
            data.Add("status", status);
            data.Add("error", error);
            return Json(data);
        }

        [HttpDelete]
        [Route("comments/celebrities/{id}")]
        public async Task<IActionResult> DeleteCommentCelebrity(int id)
        {
            int status;
            string error = null;
            CommentCelebrity commentCelebrity = _context.CommentCelebrities.Where(comment => comment.id == id).FirstOrDefault();
            if (commentCelebrity != null)
            {
                commentCelebrity.isDeleted = true;
                try
                {
                    _context.CommentCelebrities.Update(commentCelebrity);
                    await _context.SaveChangesAsync();
                    status = HttpConstants.SUCCESS_NO_DATA;
                }
                catch (IOException e)
                {
                    Console.WriteLine(e.Message);
                    status = 500;
                    error = "ERROR INTERNO. VERIFIQUE LOGS.";
                }
            }
            else
            {
                status = HttpConstants.NOT_FOUND;
                error = "NO SE HA ENCONTRADO EL ITEM.";
            }
            IDictionary<string, object> data = new Dictionary<string, object>();
            data.Add("status", status);
            data.Add("error", error);
            return Json(data);
        }

        [HttpPost]
        [Route("comments/items/react/{id}")]
        public async Task<IActionResult> AddReactToCommentItem([FromBody] AddReactToComment input, int id, [FromHeader] HeadersHelper headers)
        {
            int status;
            string error = null;
            Session session = _context.Sessions.Where(session => session.token == headers.Authorization).FirstOrDefault();
            IDictionary<string, object> data = new Dictionary<string, object>();
            if (session != null && !session.expired)
            {
                ReactionCommentItem reactionCommentItem= _context.ReactionCommentItems.Where(
                    reaction=> reaction.commentItemId == id && reaction.reactionType == input.reactionType && session.userName == session.userName
                ).FirstOrDefault();
                if(reactionCommentItem == null){
                    ReactionCommentItem reactionComment = new ReactionCommentItem(){
                        reactionType = input.reactionType,
                        userName = session.userName,
                        commentItemId = id
                    };
                    try
                    {
                        _context.ReactionCommentItems.Add(reactionComment);
                        await _context.SaveChangesAsync();
                        status = HttpConstants.SUCCESS_DATA;
                    }
                    catch (IOException e)
                    {
                        Console.WriteLine(e.Message);
                        status = 500;
                        error = "ERROR INTERNO. VERIFIQUE LOGS.";
                    }
                }
                else{
                    try
                    {
                        _context.ReactionCommentItems.Attach(reactionCommentItem);
                        _context.ReactionCommentItems.Remove(reactionCommentItem);
                        _context.SaveChanges();
                        status = HttpConstants.SUCCESS_DATA;
                    }
                    catch (IOException e)
                    {
                        Console.WriteLine(e.Message);
                        status = 500;
                        error = "ERROR INTERNO. VERIFIQUE LOGS.";
                    }
                }
            }
            else
            {
                status = HttpConstants.BAD_REQUEST;
                error = "TOKEN INVALIDO.";
            }
            data.Add("status", status);
            data.Add("error", error);
            return Json(data);
        }

        [HttpPost]
        [Route("comments/celebrities/react/{id}")]
        public async Task<IActionResult> AddReactToCommentCelebrity([FromBody] AddReactToComment input, int id, [FromHeader] HeadersHelper headers)
        {
            int status;
            string error = null;
            Session session = _context.Sessions.Where(session => session.token == headers.Authorization).FirstOrDefault();
            IDictionary<string, object> data = new Dictionary<string, object>();
            if (session != null && !session.expired)
            {
                ReactionCommentCelebrity reactionCommentCelebrity = _context.ReactionCommentCelebritys.Where(
                    reaction => reaction.commentCelebrityId == id && reaction.reactionType == input.reactionType && session.userName == session.userName
                ).FirstOrDefault();
                if (reactionCommentCelebrity == null)
                {
                    ReactionCommentCelebrity reactionComment = new ReactionCommentCelebrity()
                    {
                        reactionType = input.reactionType,
                        userName = session.userName,
                        commentCelebrityId = id
                    };
                    try
                    {
                        _context.ReactionCommentCelebritys.Add(reactionComment);
                        await _context.SaveChangesAsync();
                        status = HttpConstants.SUCCESS_DATA;
                    }
                    catch (IOException e)
                    {
                        Console.WriteLine(e.Message);
                        status = 500;
                        error = "ERROR INTERNO. VERIFIQUE LOGS.";
                    }
                }
                else
                {
                    try
                    {
                        _context.ReactionCommentCelebritys.Attach(reactionCommentCelebrity);
                        _context.ReactionCommentCelebritys.Remove(reactionCommentCelebrity);
                        _context.SaveChanges();
                        status = HttpConstants.SUCCESS_DATA;
                    }
                    catch (IOException e)
                    {
                        Console.WriteLine(e.Message);
                        status = 500;
                        error = "ERROR INTERNO. VERIFIQUE LOGS.";
                    }
                }
            }
            else
            {
                status = HttpConstants.BAD_REQUEST;
                error = "TOKEN INVALIDO.";
            }
            data.Add("status", status);
            data.Add("error", error);
            return Json(data);
        }
    }
}

