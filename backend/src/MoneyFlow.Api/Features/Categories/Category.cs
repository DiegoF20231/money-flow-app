namespace MoneyFlow.Api.Features.Categories;

public class Category
{
    public Guid CategoryId { get; private set; }
    public string Name { get; private set; }
    public string Color { get; private set; }
    public string Type { get; private set; }

    private Category(Guid categoryId, string name, string color, string type)
    {
        CategoryId = categoryId;
        Name = name;
        Color = color;
        Type = type;
    }

    public static Category Create(Guid categoryId, string name, string color, string type)
    {
        return new Category(categoryId, name, color, type);
    }
}