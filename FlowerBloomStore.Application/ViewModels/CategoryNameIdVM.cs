using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using System;
using System.Collections.Generic;
using System.Text;

namespace FlowerBloomStore.Application.ViewModels
{
    public class CategoryNameIdVM
    {
        public int Id { set; get; } 
        public string Name { set; get; }
    }
}
