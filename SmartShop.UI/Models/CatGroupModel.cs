using SmartShop.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SmartShop.UI
{
    public class CatGroupProductModel
    {
        Category objCategory;

        public Category ObjCategory
        {
            get { return objCategory; }
            set { objCategory = value; }
        }

        ProductGroup objProductGroup;

        public ProductGroup ObjProductGroup
        {
            get { return objProductGroup; }
            set { objProductGroup = value; }
        }

        List<ProductGroup> listProductGroup;

        public List<ProductGroup> ListProductGroup
        {
            get { return listProductGroup; }
            set { listProductGroup = value; }
        }

        Product objProduct;

        public Product ObjProduct
        {
            get { return objProduct; }
            set { objProduct = value; }
        }
    }
}