using Microsoft.AspNetCore.Mvc;
using Model.Models;
using Model.Repositories;
using System.Linq.Expressions;

namespace Zoo.Views.ViewComponents
{
    public class AnimalsViewComponent : ViewComponent
    {
        private readonly AnimalRepository _animelRepository;
        private readonly CategoryRepository _categoryRepository;

        public AnimalsViewComponent(AnimalRepository animelRepository,CategoryRepository categoryRepository)
        {
            _animelRepository = animelRepository;
            _categoryRepository = categoryRepository;
        }

        public IViewComponentResult Invoke(ICollection<Animal> animals)
        {
            foreach (Animal animal in animals)
            {
                animal.CategoryEnum = _categoryRepository.MatchGuidIdToCategoryEnum(animal.CategoryID);
            }
            return View(animals);
        }
    }
}
