﻿using System;
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
    
    public Product Add( Product product, out string message )
    {
        //Check for null
        if (product == null)
        {
            message = "Product cannot be null.";
            return null;
        };

        //Validate product
        var errors = product.Validate();
        //var errors = ObjectValidator.Validate(product);
            //if (errors.Count() > 0)
            //{
            //    var error = Enumerable.First(errors);

            //    //Get first error
            //    message = errors.ElementAt(0).ErrorMessage;
            //    return null;
            //};var errors = errors.FirstPrDefault();
            var error = errors.FirstOrDefault();
        if (error != null)
            {
                message = error.ErrorMessage;
                return null;
            };

        // Verify unique product
        var existing = GetProductByNameCore(product.Name);
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
        message = "";

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
        var existing = GetProductByNameCore(product.Name);
        if (existing != null && existing.Id != product.Id)
        {
            message = "Product already exists";
            return null;
        };

        //Find existing
        existing = existing ?? GetCore(product.Id);

        if (existing == null)
        {
            message = "Product not found.";
            return null;
        };

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
        return GetAllCore();
    }

    public void Remove( int id )
    {
        if (id > 0)
        {
            RemoveCore(id);
        };
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
