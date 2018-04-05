using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nile;

namespace Nile.Data
{
    /// <summary>Provides an in-memory product database.</summary>
    public abstract class ProductDatabase : IProductDatabase
    {



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
    
    public Product Add( Product product )
    {

            //Check for null
            //if (product == null)
            //    throw new ArgumentNullException(nameof(product));
            product = product ?? throw new ArgumentNullException(nameof(product));

            //Validate product
            product.Validate();
  
        // Verify unique product
        var existing = GetProductByNameCore(product.Name);
            if (existing != null)
                throw new Exception("Product already exists");
        //{
        //    message = "Product already exists";
        //    return null;
        //};

        return AddCore(product);
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
    public Product Update( Product product)
    {
        //message = "";

            //Check for null
            if (product == null)
                throw new ArgumentNullException(nameof(product));
            //{
            //    message = "Product cannot be null.";
            //    return null;
            //};

            //Validate product
            product.Validate();
        //var errors = ObjectValidator.TryValidate(product);
        //if (errors.Count() > 0)
        //{
        //    message = errors.ElementAt(0).ErrorMessage;
        //    return null;
        //};

        //Verify unique product except current product
        var existing = GetProductByNameCore(product.Name);
        if (existing != null && existing.Id != product.Id)
                throw new Exception("Product already exists");
        //{
        //    message = "Product already exists";
        //    return null;
        //};

        //Find existing
        existing = existing ?? GetCore(product.Id);

            if (existing == null)
                throw new ArgumentException("Product not found", nameof(product));
        //{
        //    message = "Product not found.";
        //    return null;
        //};

        return UpdateCore(product);
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

    public IEnumerable<Product> GetAll()
    {
            //Option 2 - extension methods
            //return GetAllCore()
            //    .OrderBy(p => p.Name)
            //    .ThenByDescending(p => p.Id)
            //    .Select(p => p);

            //Option 1 - LINQ
            return from p in GetAllCore()
                   orderby p.Name, p.Id descending
                   select p;          
    }

    public void Remove( int id )
    {
        if (id <= 0)
            throw new ArgumentOutOfRangeException(nameof(id), "Id must be > 0");
        //if (id > 0)
        //{
            RemoveCore(id);
        //};
    }


    

    private readonly List<Product> _products = new List<Product>();
    //private int _nextId = 1;

    protected abstract Product AddCore( Product product );
    protected abstract IEnumerable<Product> GetAllCore();
    protected abstract Product GetCore( int id );
    protected abstract void RemoveCore( int id );
    protected abstract Product UpdateCore( Product product );
    protected abstract Product GetProductByNameCore( string name );
}
}
