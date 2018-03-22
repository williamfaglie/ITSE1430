using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nile.Data.Memory
{
    /// <summary>Provides an in-memory product database.</summary>
    public class MemoryProductDatabase : ProductDatabase
    {

        //public MemoryProductDatabase()
        //{
        //    //Array version
        //    //var prods = new Product[]
        //    //var prods = new []  //Array of Products size 2
        //    //    {
        //    //        new Product(),
        //    //        new Product()
        //    //    };

        //    //_products = new Product[25];
        //    _products = new List<Product>()
        //    {
        //        new Product()
        //        {
        //            Id = _nextId++,
        //            Name = "iPhone X",
        //            IsDiscontinued = true,
        //            Price = 1500,
        //        },
        //        new Product() {
        //            Id = _nextId++,
        //            Name = "Windows Phone",
        //            IsDiscontinued = true,
        //            Price = 15,
        //        },
        //        new Product() {
        //            Id = _nextId++,
        //            Name = "Samsung S8",
        //            IsDiscontinued = false,
        //            Price = 800
        //        }


        //};

            //var product = new Product() {
            //    Id = _nextId++,
            //    Name = "iPhone X",
            //    IsDiscontinued = true,
            //    Price = 1500,
            //};
            //_products.Add(product);


            //product = new Product() {
            //    Id = _nextId++,
            //    Name = "Windows Phone",
            //    IsDiscontinued = true,
            //    Price = 15,
            //};
            //_products.Add(product);

            //product = new Product() {
            //    Id = _nextId++,
            //    Name = "Samsung S8",
            //    IsDiscontinued = false,
            //    Price = 800
            //};
            //_products.Add(product);
        }
        protected override Product AddCore ( Product product)
        {
            //Clone the object
            product.Id = _nextId++;
            _products.Add(Clone(product));

            // Return the copy
            return product;
        }

        //private int FindEmptyProductIndex()
        //{
        //    for (var index = 0; index < _products.Length; ++index)
        //    {
        //        if (_products[index] == null)
        //            return index;

        //    };

        //    return -1;
        //}
        protected override Product UpdateCore( Product product )
        {
            //Clone the object
            var existing = GetCore(product.Id);
            //_products[existingIndex] = Clone(product);
            Copy(existing, product);

            //Return a copy
            return product;
        }

        //public IEnumerable<Product> GetAll ()
        //{
        //    //Return a copy so caller 
        //    var items = new List<Product>();

        //    //for (var index = 0; index < _products.Length; ++index)
        //    foreach (var product in _products)
        //    {
        //        if (product != null)
        //            items.Add(Clone(product));
        //    };

        //    return items;
        //}

        protected override IEnumerable<Product> GetAllCore()
        {
            foreach (var product in _products)
            {
                if (product != null)
                    yield return Clone(product);
            };
        }

        protected override void RemoveCore ( int id )
        {
              var existing = GetCore(id);
              if (existing != null)
                    _products.Remove(existing); 
        }

        private Product Clone ( Product item )
        {
            var newProduct = new Product();
            Copy(newProduct, item);

            return newProduct;
        }

        private void Copy ( Product target, Product source )
        {
            target.Id = source.Id;
            target.Name = source.Name;
            target.Description = source.Description;
            target.Price = source.Price;
            target.IsDiscontinued = source.IsDiscontinued;
        }

        protected override Product GetCore ( int id )
        {
            //for (var index = 0; index < _products.Length; ++index)
            foreach (var product in _products)
            {
                if (product.Id == id)
                    return product;
            };

            return null;
        }

        protected override Product GetProductByNameCore ( string name )
        {
            foreach (var product in _products)
            {
                //product.Name.CompareTo
                if (String.Compare(product.Name, name, true) == 0)
                    return product;
            };

            return null;
        }
        private Product GetById( int id )
        {
            //for (var index = 0; index < _products.Length; ++index)
            foreach (var product in _products)
            {
                if (product.Id == id)
                    return product;
            };

            return null;
        }

        private readonly List<Product> _products = new List<Product>();
        private int _nextId = 1;
    }
}
