using System;
using System.Linq.Expressions;
using Core.Entities;

namespace Core.Specifications
{
    public class ProductsWithTypesAndBrandsSpecification : BaseSpecification<Product>
    {
        public ProductsWithTypesAndBrandsSpecification(ProductSpecParams productParams)  
            : base(f => 
                (string.IsNullOrEmpty(productParams.Search) || f.Name.ToLower().Contains(productParams.Search)) &&
                (!productParams.BrandId.HasValue || f.ProductBrandId == productParams.BrandId) && 
                (!productParams.TypeId.HasValue || f.ProductTypeId == productParams.TypeId)
            ) 
        {
            AddInclude(x => x.ProductType);
            AddInclude(x => x.ProductBrand);
            AddOrderBy(x => x.Name);
            ApplyPaging(productParams.PageSize * (productParams.PageIndex - 1),productParams.PageSize);

            if (!string.IsNullOrEmpty(productParams.Sort))
            {
                switch (productParams.Sort)
                {
                    case "priceAsc":
                        AddOrderBy(s => s.Price);
                        break;
                    case "priceDesc":
                        AddOrderByDescending(s => s.Price);
                        break;
                    default:
                        AddOrderBy(s => s.Name);
                        break;
                }
            }
        }

        public ProductsWithTypesAndBrandsSpecification(int id) : base(c => c.Id == id)
        {
            AddInclude(x => x.ProductType);
            AddInclude(x => x.ProductBrand);
        }
    }
}