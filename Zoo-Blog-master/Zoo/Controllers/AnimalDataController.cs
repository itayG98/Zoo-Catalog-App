using Microsoft.AspNetCore.Mvc;
using Model.Models;
using Model.Repositories;
using System.Collections.Immutable;
using System.Linq;
using System.Xml.Linq;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Authorization;
using System.Data;

namespace Zoo.Controllers
{
    public class AnimalDataController : Controller
    {
        private readonly AnimalRepository _animelRepository;
        private readonly CommentRepository _commentRepository;
        private readonly CategoryRepository _categoryRepository;

        public AnimalDataController(AnimalRepository animelRepository, CommentRepository commentRepository, CategoryRepository categoryRepository)
        {
            ViewBag.Title = "Animal's Data";
            _animelRepository = animelRepository;
            _commentRepository = commentRepository;
            _categoryRepository = categoryRepository;
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Index(Guid id)
        {
            Animal animel = _animelRepository.FindWithComments(id);
            if (animel != null)
            {
                animel.CategoryEnum = _categoryRepository.MatchGuidIdToCategoryEnum(animel.CategoryID);
                ViewBag.Animal = animel;
                return View(new Comment() { CommentId = Guid.NewGuid() });
            }
            return Redirect("Home");
        }

        /// <summary>
        /// handle fetch api submiting
        /// </summary>
        [HttpPost]
        [Authorize(Roles = "User,Admin")]
        public void Index([FromBody]Comment comment)
        {
            comment.CommentId= Guid.NewGuid();
            if (ModelState.IsValid ||
                (ModelState.ErrorCount==1 && ModelState.GetFieldValidationState("Animal") == ModelValidationState.Invalid))
            _commentRepository.Create(comment);
        }

        /// <summary>
        /// handle fetch api get request
        /// </summary>
        [HttpGet]
        [AllowAnonymous]
        public ActionResult<List<string>> GetComments(string id)
        {
            if (id != null && Guid.Parse(id) is Guid guidId)
            {
                List<Comment> comments = (_animelRepository.FindWithComments(guidId).Comments.ToList());
                List<string> contents = new List<string>();
                if (comments != default && comments.Count > 0)
                    foreach (Comment comment in comments)
                        contents.Add(comment.Content);
                return contents;
            }
            else
                return default;
        }
    }
}
