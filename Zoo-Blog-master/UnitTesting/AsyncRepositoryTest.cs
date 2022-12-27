using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Model.DAL;
using Model.Models;
using Model.Repositories;
using System.Linq;

namespace UnitTesting
{
    public class AsyncRepositoryTest
    {
        private ZooDBContext _context;
        private AnimalRepository? _animelRepository;
        private CommentRepository? _commentRepository;
        private CategoryRepository? _categoryRepository;

        private Animal? animelTest;
        private Animal? Do_Not_Insret_Animel;
        private Category? categoryTest;
        private Category? Do_Not_Insret_Category;
        private Comment? commentTest;
        private string? _connString;

        [SetUp]
        public void Setup()
        {
            var config = new ConfigurationBuilder()
                         .AddJsonFile("AppConfig.json", false, true)
                         .Build();
            _connString = config.GetConnectionString("AsyncMockDb");
            byte[] DeafualtRawData = Animal.DeafualtRawData;
            var optionsBuilder = new DbContextOptionsBuilder<ZooDBContext>();
            optionsBuilder.UseSqlite(_connString);
            _context = new ZooDBContext(optionsBuilder.Options);
            if (_context != default)
            {
                _context.Database.EnsureDeleted();
                _context.Database.EnsureCreated();
                _animelRepository = new AnimalRepository(_context);
                _commentRepository = new CommentRepository(_context);
                _categoryRepository = new CategoryRepository(_context);
                categoryTest = new() { Name = "Test", CategoryID = Guid.NewGuid() };
                animelTest = new() { ID = Guid.NewGuid(), Name = "Test", BirthDate = new DateTime(2002, 6, 12), Description = "", CategoryID = categoryTest.CategoryID, ImageRawData = DeafualtRawData };
                commentTest = new() { CommentId = Guid.NewGuid(), AnimalID = animelTest.ID, Content = "Content" };
                Do_Not_Insret_Animel = new Animal() { ID = Guid.NewGuid(), Name = "Never_Insert" };
                Do_Not_Insret_Category = new Category() { CategoryID = Guid.NewGuid(), Name = "Nevet_Insert" };
            }
            if (_animelRepository == null ||
                _categoryRepository == null ||
                _commentRepository == null ||
                _context == null)
                throw new Exception("Repositories or DBContext were not set");
        }

        [Test, RequiresThread]
        public void AddToDBUDuplicateAsync()
        {

            Task<bool> AddCategory = _categoryRepository!.CreateAsync(categoryTest!);
            AddCategory.Wait();
            Assert.That(AddCategory.Result, Is.True);

            Task<bool> AddAnimal = _animelRepository!.CreateAsync(animelTest!);
            AddAnimal.Wait();
            Assert.That(AddAnimal.Result, Is.True);

            Task<bool> AddComment = _commentRepository!.CreateAsync(commentTest!);
            AddComment.Wait();
            Assert.That(AddComment.Result, Is.True);


            Task<bool> AddCategorySecond = _categoryRepository.CreateAsync(categoryTest!);
            AddCategorySecond.Wait();
            Assert.That(AddCategorySecond.Result, Is.False);

            Task<bool> AddAnimalSecond = _animelRepository.CreateAsync(animelTest!);
            AddAnimalSecond.Wait();
            Assert.That(AddAnimalSecond.Result, Is.False);

            Task<bool> AddCommentSecond = _commentRepository.CreateAsync(commentTest!);
            AddCommentSecond.Wait();
            Assert.That(AddCommentSecond.Result, Is.False);


        }

        [Test, RequiresThread]
        public void DeleteAsync()
        {
            _categoryRepository?.Create(categoryTest!);
            _animelRepository?.Create(animelTest!);
            _commentRepository?.Create(commentTest!);


            Task<bool> animelDeleted = _animelRepository!.DeleteAsync(animelTest!);
            animelDeleted.ContinueWith(_ => { Assert.That(animelDeleted.Result, Is.True); });
            Task<bool> Invalid_animel_Deleted = _animelRepository.DeleteAsync(Do_Not_Insret_Animel!);
            Invalid_animel_Deleted.ContinueWith(_ => { Assert.That(Invalid_animel_Deleted.Result, Is.False); });
            Task<bool> categoryDeleted = _categoryRepository!.DeleteAsync(categoryTest!);
            categoryDeleted.ContinueWith(_ => { Assert.That(categoryDeleted.Result, Is.True); });
            Task<bool> commentDeleted = _commentRepository!.DeleteAsync(commentTest!);
            commentDeleted.ContinueWith(_ => { Assert.That(commentDeleted.Result, Is.True); });
        }

        [Test, RequiresThread]
        public void UpdateAsync()
        {
            string test = "Update Test";

            _categoryRepository!.Create(categoryTest!);
            _animelRepository!.Create(animelTest!);
            _commentRepository!.Create(commentTest!);

            Do_Not_Insret_Animel!.Name = test;
            animelTest!.Name = test;
            animelTest!.Description = test;
            commentTest!.Content = test;
            categoryTest!.Name = test;

            Task<bool> animelUpdated = _animelRepository.UpdateAsync(animelTest);
            animelUpdated.ContinueWith(_ => { Assert.That(animelUpdated.Result, Is.True); });
            Task<bool> Invalid_animel_Updated = _animelRepository.UpdateAsync(Do_Not_Insret_Animel);
            Invalid_animel_Updated.ContinueWith(_ => { Assert.That(Invalid_animel_Updated.Result, Is.False); });
            Task<bool> categoryUpdated = _categoryRepository.UpdateAsync(categoryTest);
            categoryUpdated.ContinueWith(_ => { Assert.That(categoryUpdated.Result, Is.True); });
            Task<bool> commentUpdated = _commentRepository.UpdateAsync(commentTest);
            commentUpdated.ContinueWith(_ => { Assert.That(commentUpdated.Result, Is.True); });
        }
        [Test, RequiresThread]
        public void FindByIdAsync()
        {
            _categoryRepository?.Create(categoryTest!);
            _animelRepository?.Create(animelTest!);
            _commentRepository?.Create(commentTest!);

            Task<Animal> animelFound = _animelRepository!.FindByIDAsync(animelTest!.ID);
            animelFound.ContinueWith(_ => { Assert.That(animelFound.Result, Is.EqualTo(animelTest)); });
            Task<Animal> animelNotFound = _animelRepository.FindByIDAsync(Do_Not_Insret_Animel!.ID);
            animelNotFound.ContinueWith(_ => { Assert.That(animelFound.Result, Is.Null); });
            Task<Comment> commentFound = _commentRepository!.FindByIDAsync(commentTest!.CommentId);
            commentFound.ContinueWith(_ => { Assert.That(commentFound.Result, Is.EqualTo(commentTest)); });
            Task<Category> categoryFound = _categoryRepository!.FindByIDAsync(categoryTest!.CategoryID);
            categoryFound.ContinueWith(_ => { Assert.That(categoryFound.Result, Is.EqualTo(categoryTest)); });
        }

        [Test, RequiresThread]
        public void FindAllAsync()
        {
            Task categoryTask = new(() =>
            {
                Assert.That(_categoryRepository!.FindAll().ToList().Count(), Is.EqualTo(2));
                Task<bool> categoryCreation = _categoryRepository.CreateAsync(categoryTest!);
                Task<IEnumerable<Category>> categoryLength = _categoryRepository.FindAllAsync();
                categoryCreation.ContinueWith(_ => categoryLength);
                categoryLength.ContinueWith(_ => Assert.That(categoryLength.Result.ToArray().Length.Equals(3)));
            });
            Task animelTask = new(() =>
            {
                Assert.That(_animelRepository!.FindAll().ToList().Count(), Is.EqualTo(2));
                Task<bool> animelCreation = _animelRepository.CreateAsync(animelTest!);
                Task<IEnumerable<Animal>> animelLength = _animelRepository.FindAllAsync();
                animelCreation.ContinueWith(_ => animelLength);
                animelLength.ContinueWith(_ => Assert.That(animelLength.Result.ToArray().Length.Equals(3)));

            });
            Task commentTask = new(() =>
            {
                Assert.That(_commentRepository!.FindAll().ToList().Count(), Is.EqualTo(2));
                Task<bool> commentCreation = _commentRepository.CreateAsync(commentTest!);
                Task<IEnumerable<Comment>> commentLength = _commentRepository.FindAllAsync();
                commentCreation.ContinueWith(_ => commentLength);
                commentLength.ContinueWith(_ => Assert.That(commentLength.Result.ToArray().Length.Equals(3)));

            });
        }

        [Test, RequiresThread]
        public void FindByConditionAsync()
        {
            _categoryRepository!.Create(categoryTest!);
            Task<Category> taskFound = _categoryRepository.FindFirstByConditionOrDefaultAsync(x => x.Equals(categoryTest));
            Task<Category> taskNotFound = _categoryRepository.FindFirstByConditionOrDefaultAsync(x => x.Equals(Do_Not_Insret_Category));
            taskFound.ContinueWith(_ => { Assert.That(taskFound.Result, Is.EqualTo(categoryTest)); });
            taskNotFound.ContinueWith(_ => { Assert.That(taskNotFound.Result, Is.EqualTo(null)); });
        }

        [Test, RequiresThread]
        public void FindAllByConditionAsync()
        {
            _categoryRepository!.Create(categoryTest!);
            Task<IEnumerable<Category>> categoriesTask = _categoryRepository.FindByConditionAsync(c => c.Name!.Contains("aA"));
            categoriesTask.ContinueWith(_ =>
            {
                Assert.That(categoriesTask.Result.Contains(categoryTest), Is.Not.True);
                Assert.That(categoriesTask.Result.All(C => C.Name!.Contains("aA")), Is.True);
            });
        }
    }
}