using Microsoft.AspNetCore.Mvc;
using Model.Models;
using Model.Repositories;

namespace Zoo.Controllers
{
    public class CatalogController : Controller
    {
        private readonly AnimalRepository _animelRepository;
        public CatalogController(AnimalRepository animelRepository)
        {
            _animelRepository = animelRepository;
        }
        public IActionResult Index()
        {
            return View(_animelRepository.FindAllWithComments().ToList());
        }
    }
}
