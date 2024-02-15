using WebApplication1.Entities;

namespace WebApplication1;

public class ComparerAsValueObjects : IEqualityComparer<Good>
{
    public bool Equals(Good x, Good y)
    {
        if (ReferenceEquals(x, y)) return true;
        if (ReferenceEquals(x, null)) return false;
        if (ReferenceEquals(y, null)) return false;
        if (x.GetType() != y.GetType()) return false;
        return x.GoodId == y.GoodId && 
               x.Name == y.Name &&
               x.Description == y.Description && 
               x.Category == y.Category && 
               x.Weight == y.Weight &&
               x.Price == y.Price;
    }

    public int GetHashCode(Good obj)
    {
        return HashCode.Combine(obj.GoodId, obj.Name, obj.Description, obj.Category, obj.Weight, obj.Price);
    }
}