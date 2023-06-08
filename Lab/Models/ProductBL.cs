namespace Lab.Models
{
	public static class ProductBL
	{
		static List<Product> products;
		public static List<Product> GetAllProducts()
		{
			products = new List<Product>()
			{
				new Product{ID=1,Name="Phone1",Description="Product-1",Price = 2000,Image = "1.jpg"},
				new Product{ID=2,Name="Phone2",Description="Product-2",Price = 1500,Image = "2.jpg"},
				new Product{ID=3,Name="Phone3",Description="Product-3",Price = 3000,Image = "3.jpg"},
				new Product{ID=4,Name="Phone4",Description="Product-4",Price = 1000,Image = "1.jpg"},
			};
			return products;
		}
		public static Product GetProductByID(int _id)
		{
			var product = GetAllProducts().FirstOrDefault(p => p.ID == _id);
			return product;
		}
	}
}
