using System;
using System.Collections.Generic;
using System.Text;

namespace FlowerBloomStore.Application.ViewModels
{
    public class ProductCardVM
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string ImageUrl { set; get; }
    }
}
