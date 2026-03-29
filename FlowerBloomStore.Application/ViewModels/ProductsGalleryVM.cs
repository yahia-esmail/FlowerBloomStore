using FlowerBloomStore.Application.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace FlowerBloomStore.Application.ViewModels
{
    public class ProductsGalleryVM
    {
        public List<ProductCardVM> Products { set; get; } = new List<ProductCardVM>();

        public List<CategoryNameIdVM> CategoryNames { set; get; } = new List<CategoryNameIdVM>();

        public List<int> SelectedCatgorieIDs { set; get; } = new List<int>();

        public PriceOrder SelectedPriceOrder { set; get; }

        public PaginationInfo PagesInfo { set; get; } = new PaginationInfo();
    }
}
