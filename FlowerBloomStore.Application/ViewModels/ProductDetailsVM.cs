using System;
using System.Collections.Generic;
using System.Text;

namespace FlowerBloomStore.Application.ViewModels
{
    public class ProductDetailsVM
    {
        public int Id { set; get; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { set; get; }
        public string ImageUrl {set; get;}
        public decimal AverageRating { get; set; } = 0;
        public List<ProductCardVM> RelatedProducts = new List<ProductCardVM>();

        public List<ProductImage> ProductImages = new List<ProductImage>();
        public bool InWishlist { set; get; }
    }
}
