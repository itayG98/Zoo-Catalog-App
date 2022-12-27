using Microsoft.EntityFrameworkCore;
using Model.Models;
using Model.Services;

namespace Model.DAL
{
    public class ZooDBContext : DbContext
    {
        public ZooDBContext(DbContextOptions<ZooDBContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            List<Category> categories = new();
            foreach (string categotyName in Enum.GetNames(typeof(CategoriesEnum)))
            {
                categories.Add(new Category() { Name = categotyName, CategoryID = Guid.NewGuid() });
            }

            Category Mammal = categories.Where(c => c.Name == "Mammal").First();
            Category Avian = categories.Where(c => c.Name == "Avian").First();
            Category Aquadic = categories.Where(c => c.Name == "Aquadic").First();
            Category Insect = categories.Where(c => c.Name == "Insect").First();
            Category Reptile = categories.Where(c => c.Name == "Reptile").First();

            modelBuilder.Entity<Category>().HasData(categories);

            Animal Elephant = new() { ID = Guid.NewGuid(), Name = "Elephent", BirthDate = new DateTime(2002, 6, 12), Description = "Test", CategoryID = Mammal.CategoryID, ImageRawData = ImagesFormater.ImageToBytesArrayFromLocalPath("InitImages/Elephant.jpg") };
            Animal Eagel = new() { ID = Guid.NewGuid(), Name = "Eagel", BirthDate = new DateTime(2009, 12, 12), Description = "Test", CategoryID = Avian.CategoryID, ImageRawData = ImagesFormater.ImageToBytesArrayFromLocalPath("InitImages/Eagle.webp") };
            Animal Squirl = new() { ID = Guid.NewGuid(), Name = "Squirrel", BirthDate = new DateTime(2009, 12, 12), Description = "Test", CategoryID = Mammal.CategoryID, ImageRawData = ImagesFormater.ImageToBytesArrayFromLocalPath("InitImages/SQIRL.jpg") };
            Animal Monkey = new() { ID = Guid.NewGuid(), Name = "Monkey", BirthDate = new DateTime(2011, 12, 12), Description = "Test", CategoryID = Mammal.CategoryID, ImageRawData = ImagesFormater.ImageToBytesArrayFromLocalPath("InitImages/Monkey.jpg") };
            Animal Cow = new() { ID = Guid.NewGuid(), Name = "Cow", BirthDate = new DateTime(2011, 12, 12), Description = "Test", CategoryID = Mammal.CategoryID, ImageRawData = ImagesFormater.ImageToBytesArrayFromLocalPath("InitImages/Cow.jpg") };
            Animal Dolphin = new() { ID = Guid.NewGuid(), Name = "Dolphin", BirthDate = new DateTime(2011, 12, 12), Description = "Test", CategoryID = Mammal.CategoryID, ImageRawData = ImagesFormater.ImageToBytesArrayFromLocalPath("InitImages/Dolphin.jpg") };
            Animal Lion = new() { ID = Guid.NewGuid(), Name = "Lion", BirthDate = new DateTime(2011, 12, 12), Description = "Test", CategoryID = Mammal.CategoryID, ImageRawData = ImagesFormater.ImageToBytesArrayFromLocalPath("InitImages/Lion.jpg") };
            Animal Lizard = new() { ID = Guid.NewGuid(), Name = "Lizard", BirthDate = new DateTime(2011, 12, 12), Description = "Test", CategoryID = Reptile.CategoryID, ImageRawData = ImagesFormater.ImageToBytesArrayFromLocalPath("InitImages/Lizard.jpg") };
            Animal Owl = new() { ID = Guid.NewGuid(), Name = "Owl", BirthDate = new DateTime(2011, 12, 12), Description = "Test", CategoryID = Avian.CategoryID, ImageRawData = ImagesFormater.ImageToBytesArrayFromLocalPath("InitImages/Owl.jpg") };
            Animal Shark = new() { ID = Guid.NewGuid(), Name = "Shark", BirthDate = new DateTime(2011, 12, 12), Description = "Test", CategoryID = Aquadic.CategoryID, ImageRawData = ImagesFormater.ImageToBytesArrayFromLocalPath("InitImages/Shark.webp") };
            Animal Snake = new() { ID = Guid.NewGuid(), Name = "Snake", BirthDate = new DateTime(2011, 12, 12), Description = "Test", CategoryID = Reptile.CategoryID, ImageRawData = ImagesFormater.ImageToBytesArrayFromLocalPath("InitImages/Snake.jpg") };
            Animal Spider = new() { ID = Guid.NewGuid(), Name = "Spider", BirthDate = new DateTime(2011, 12, 12), Description = "Test", CategoryID = Insect.CategoryID, ImageRawData = ImagesFormater.ImageToBytesArrayFromLocalPath("InitImages/Spider.webp") };
            Animal wagtail = new() { ID = Guid.NewGuid(), Name = "Wagtail", BirthDate = new DateTime(2011, 12, 12), Description = "Test", CategoryID = Avian.CategoryID, ImageRawData = ImagesFormater.ImageToBytesArrayFromLocalPath("InitImages/wagtail.jpg") };
            Animal bee = new() { ID = Guid.NewGuid(), Name = "Bee", BirthDate = new DateTime(2011, 12, 12), Description = "Test", CategoryID = Insect.CategoryID, ImageRawData = ImagesFormater.ImageToBytesArrayFromLocalPath("InitImages/Bee.png") };

            modelBuilder.Entity<Animal>().HasData(Elephant, Eagel, Squirl, Monkey, Cow, Dolphin, Lion, Lizard, Owl, Shark, Snake, Spider, wagtail, bee);

            modelBuilder.Entity<Comment>().HasData(
                new Comment() { AnimalID = Eagel.ID, Content = "First init comment", CommentId = Guid.NewGuid() },
                new Comment() { AnimalID = Eagel.ID, Content = "second init comment", CommentId = Guid.NewGuid() },
                new Comment() { AnimalID = Eagel.ID, Content = "Third init comment", CommentId = Guid.NewGuid() },
                new Comment() { AnimalID = Elephant.ID, Content = "Initial comment", CommentId = Guid.NewGuid() }
                );

        }

    }
}
