
namespace Domain.Entities;

public partial class Discount
{
    public int DiscountCode { get; set; }

    public int? Price { get; set; }

    public int? DiscountType { get; set; }

    public int? Valid { get; set; }

    public virtual ICollection<Time> Times { get; set; } = new List<Time>();
}
