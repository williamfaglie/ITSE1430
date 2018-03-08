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
    public abstract class MemoryProductDatabase : IProductDatabase
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
    }
    public Product Add( Product product, out string message )
    {
        //Check for null
        if (product == null)
        {
            message = "Product cannot be null.";
            return null;
        };

        //Validate product
        var errors = ObjectValidator.Validate(product);
        if (errors.Count() > 0)
        {
            message = errors.ElementAt(0).ErrorMessage;
            return null;
        };

        // Verify unique product
        var existing = GetProductByName(product.Name);
        if (existing != null)
        {
            message = "Product already exists";
            return null;
        };

        message = null;
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
    public Product Update( Product product, out string message )
    {
        //Check for null
        if (product == null)
        {
            message = "Product cannot be null.";
            return null;
        };

        //Validate product
        var errors = ObjectValidator.Validate(product);
        if (errors.Count() > 0)
        {
            message = errors.ElementAt(0).ErrorMessage;
            return null;
        };

        //Verify unique product except current product
        var existing = GetProductByName(product.Name);
        if (existing != null && existing.Id != product.Id)
        {
            message = "Product already exists";
            return null;
        };

        //Find existing
        existing = existing ?? GetById(product.Id);

        if (existing == null)
        {
            message = "Product not found.";
            return null;
        };

        //Clone the object
        //_products[existingIndex] = Clone(product);
        Copy(existing, product);
        message = null;

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

    public IEnumerable<Product> GetAll()
    {
        return GetAllCore();
    }

    public void Remove( int id )
    {
        if (id > 0)
        {
            var existing = GetById(id);
            if (existing != null)
                _products.Remove(existing);
        };
    }

    private Product Clone( Product item )
    {
        var newProduct = new Product();
        Copy(newProduct, item);

        return newProduct;
    }

    private void Copy( Product target, Product source )
    {
        target.Id = source.Id;
        target.Name = source.Name;
        target.Description = source.Description;
        target.Price = source.Price;
        target.IsDiscontinued = source.IsDiscontinued;
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

    private Product GetProductByName( string name )
    {
        foreach (var product in _products)
        {
            //product.Name.CompareTo
            if (String.Compare(product.Name, name, true) == 0)
                return product;
        };

        return null;
    }

    private readonly List<Product> _products = new List<Product>();
    private int _nextId = 1;

    protected abstract Product AddCore( Product product );
    protected abstract public IEnumerable<Product> GetAllCore();
    protected abstract Product GetCore( int id );
}
}
