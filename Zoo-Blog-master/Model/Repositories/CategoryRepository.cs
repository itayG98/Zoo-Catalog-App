using Model.DAL;
using Model.Models;

namespace Model.Repositories
{
    public class CategoryRepository : ZooRepository<Category>
    {
        public CategoryRepository(ZooDBContext dbContext) : base(dbContext)
        {
        }

        public Guid MatchCategoryEnumToGuidId(CategoriesEnum categoryEnum)
            => FindFirstByConditionOrDefault(c => c.Name == categoryEnum.ToString()).CategoryID;

        public CategoriesEnum MatchGuidIdToCategoryEnum(Guid id)
        {
            if (id != default)
                return (CategoriesEnum)Enum.Parse(typeof(CategoriesEnum), FindFirstByConditionOrDefault(c => c.CategoryID == id).Name);
            return default;
        }

        public static CategoriesEnum[] GetAllEnumCategories()
             =>(CategoriesEnum[])Enum.GetValues(typeof(CategoriesEnum));
        
    }
}
