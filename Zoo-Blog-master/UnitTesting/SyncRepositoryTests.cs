using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Model.DAL;
using Model.Models;
using Model.Repositories;
using NUnit.Framework.Internal;

namespace ModelTests
{

    public class SyncRepositoryTest
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

        private string _connString;

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
            if (_animelRepository is null ||
                _categoryRepository is null ||
                _commentRepository is null)
                throw new Exception("Repositories were not set");
        }

        [Test]
        public void CreatingRepositories()
        {
            Assert.Multiple(() =>
            {
                Assert.That(_animelRepository, Is.Not.Null);
                Assert.That(_commentRepository, Is.Not.Null);
                Assert.That(_categoryRepository, Is.Not.Null);
            });
        }

        [Test]
        public void AddToDB()
        {
            Task<bool> AddCategoty = _categoryRepository!.CreateAsync(categoryTest!);
            AddCategoty.Wait();
            Assert.That(AddCategoty.Result, Is.True);

            Task<bool> AddAnimal = _animelRepository!.CreateAsync(animelTest!);
            AddAnimal.Wait();
            Assert.That(AddAnimal.Result, Is.True);

            Task<bool> AddComment = _commentRepository!.CreateAsync(commentTest!);
            AddComment.Wait();
            Assert.That(AddComment.Result, Is.True);
        }

        [Test]
        public void AddToDBUDuplicate()
        {
            bool AddCategory = _categoryRepository!.Create(categoryTest!);
            Assert.That(AddCategory, Is.True);

            bool AddAnimal = _animelRepository!.Create(animelTest!);
            Assert.That(AddAnimal, Is.True);

            bool AddComment = _commentRepository!.Create(commentTest!);
            Assert.That(AddComment, Is.True);

            AddCategory = _categoryRepository.Create(categoryTest!);
            Assert.That(AddCategory, Is.False);

            AddAnimal = _animelRepository.Create(animelTest!);
            Assert.That(AddAnimal, Is.False);

            AddComment = _commentRepository.Create(commentTest!);
            Assert.That(AddComment, Is.False);
        }

        [Test]
        public void DeleteNotExicst()
        {
            bool DeleteNotExicstAnimel = _animelRepository!.Delete(Do_Not_Insret_Animel!);
            Assert.That(DeleteNotExicstAnimel, Is.False);
        }

        [Test]
        public void DeleteInDBUDuplicate()
        {
            _categoryRepository!.Create(categoryTest!);
            _animelRepository!.Create(animelTest!);
            _commentRepository!.Create(commentTest!);


            bool DeleteAddComment = _commentRepository.Delete(commentTest!);
            Assert.That(DeleteAddComment, Is.True);

            bool DeleteAddAnimal = _animelRepository.Delete(animelTest!);
            Assert.That(DeleteAddAnimal, Is.True);

            bool DeleteCategory = _categoryRepository.Delete(categoryTest!);
            Assert.That(DeleteCategory, Is.True);

            DeleteAddComment = _commentRepository.Delete(commentTest!);
            Assert.That(DeleteAddComment, Is.False);

            DeleteAddAnimal = _animelRepository.Delete(animelTest!);
            Assert.That(DeleteAddAnimal, Is.False);

            DeleteCategory = _categoryRepository.Delete(categoryTest!);
            Assert.That(DeleteCategory, Is.False);
        }

        [Test]
        public void Update()
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

            bool animelUpdated = _animelRepository.Update(animelTest);
            bool Invalid_animel_Updated = _animelRepository.Update(Do_Not_Insret_Animel);
            bool categoryUpdated = _categoryRepository.Update(categoryTest);
            bool commentUpdated = _commentRepository.Update(commentTest);

            Assert.That(animelUpdated, Is.True);
            Assert.That(categoryUpdated, Is.True);
            Assert.That(commentUpdated, Is.True);
            Assert.That(Invalid_animel_Updated, Is.False);

        }

        [Test]
        public void FindById()
        {
            _categoryRepository!.Create(categoryTest!);
            _animelRepository!.Create(animelTest!);
            _commentRepository!.Create(commentTest!);
            Assert.Multiple(() =>
            {
                Assert.That(_animelRepository.Find(Do_Not_Insret_Animel!.ID), Is.Null);
                Assert.That(_animelRepository.Find(animelTest!.ID), Is.Not.Null);
                Assert.That(_commentRepository.Find(commentTest!.CommentId), Is.Not.Null);
                Assert.That(_categoryRepository.Find(categoryTest!.CategoryID), Is.Not.Null);
            });
        }

        [Test]
        public void FindByReference()
        {
            _categoryRepository?.Create(categoryTest!);
            _animelRepository?.Create(animelTest!);
            _commentRepository?.Create(commentTest!);
            Assert.That(_animelRepository?.FindByReference(Do_Not_Insret_Animel!), Is.Null);
            Assert.That(_animelRepository?.FindByReference(animelTest!), Is.Not.Null);
            Assert.That(_commentRepository?.FindByReference(commentTest!), Is.Not.Null);
            Assert.That(_categoryRepository?.FindByReference(categoryTest!), Is.Not.Null);
        }

        [Test]
        public void FindAll()
        {
            int count = _categoryRepository!.FindAll().ToList().Count();
            _categoryRepository.Create(categoryTest!);
            Assert.That(_categoryRepository!.FindAll().ToList().Count(), Is.EqualTo(count + 1));
        }

        [Test]
        public void FindByCondition()
        {
            _categoryRepository!.Create(categoryTest!);
            Category categoryFound = _categoryRepository.FindFirstByConditionOrDefault(x => x.Equals(categoryTest));
            Category categoryNotFound = _categoryRepository.FindFirstByConditionOrDefault(x => x.Equals(Do_Not_Insret_Category));
            Assert.That(categoryFound, Is.EqualTo(categoryTest));
            Assert.That(categoryNotFound, Is.EqualTo(null));
        }

        [Test]
        public void FindAllByCondition()
        {
            _categoryRepository!.Create(categoryTest!);
            IEnumerable<Category> categories = _categoryRepository.FindByCondition(c => c.Name!.Contains("aA"));
            Assert.That(categories.Contains(categoryTest), Is.Not.True);
            Assert.That(categories.All(C => C.Name!.Contains("aA")), Is.True);
        }

        [Test]
        public void FindAllWithComments()
        {
            List<Animal> animels = _animelRepository!.FindAllWithComments().ToList();
            Assert.That(animels.All(a => a.Comments != null), Is.True);
        }
        [Test]
        public void FindTopTrending()
        {
            int check = 2;
            List<Animal> animels = _animelRepository!.FindTopTrending(check);
            Assert.That(animels.Count == check, Is.True);
            Assert.That(animels.OrderByDescending(a => a?.Comments?.Count).Take(check).SequenceEqual(animels), Is.True); ;
        }

        [Test]
        public void GetCategoryEnum()
        {
            List<Category> categories = _categoryRepository!.FindAll().Where(c => c.Name != "Test").ToList();
            Guid id = categories.First().CategoryID;
            string name = categories.First().Name;
            CategoriesEnum catEnum = _categoryRepository.MatchGuidIdToCategoryEnum(id);
            Assert.That(catEnum.ToString(), Is.EqualTo(name));
        }
        [Test]
        public void GetCategoryID()
        {
            List<Category> categories = _categoryRepository!.FindAll().Where(c => c.Name != "Test").ToList();
            string name = categories.First().Name;
            Guid id = categories.First().CategoryID;
            CategoriesEnum cat = (CategoriesEnum)Enum.Parse(typeof(CategoriesEnum), name);
            Guid generetedId = _categoryRepository.MatchCategoryEnumToGuidId(cat);
            Assert.That(generetedId == id, Is.True);
        }

        [Test]
        public void FindWithComments()
        {
            _categoryRepository.Create(categoryTest);
            _animelRepository.Create(animelTest);
            _commentRepository.Create(commentTest);
            List<Comment> comments = _commentRepository.FindByCondition(c => c.AnimalID == animelTest.ID).ToList();
            Animal a = _animelRepository.FindWithComments(animelTest.ID);
            Assert.That(comments.SequenceEqual(a.Comments), Is.True);

        }
    }
}
