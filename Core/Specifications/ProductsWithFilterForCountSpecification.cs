using Core.Entities;

namespace Core.Specifications
{
    public class ProductsWithFilterForCountSpecification : BaseSpecification<Product>
    {
        public ProductsWithFilterForCountSpecification(ProductSpecParams productParams)
            : base(f => 
                (string.IsNullOrEmpty(productParams.Search) || f.Name.ToLower().Contains(productParams.Search)) &&
                (!productParams.BrandId.HasValue || f.ProductBrandId == productParams.BrandId) && 
                (!productParams.TypeId.HasValue || f.ProductTypeId == productParams.TypeId)
            ) 
        {
        }
    }
}