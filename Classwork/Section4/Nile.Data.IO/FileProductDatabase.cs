﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nile.Data.IO
{
    public class FileProductDatabase : ProductDatabase
    {
        public FileProductDatabase( string filename )
        {
            _filename = filename;
        }

        protected override Product AddCore( Product product )
        {
            EnsureInitialized();

            product.Id = _id++;
            _items.Add(product);

            SaveDataNonstream();

            return product;
        }

        protected override IEnumerable<Product> GetAllCore()
        {
            EnsureInitialized();

            //LoadData();
            return _items;
        }
     
        //Ensure file is loaded
        private void EnsureInitialized()
        {
            if (_items == null)
            {
                _items = LoadData();

                _id = 0;
                foreach (var item in _items)
                {
                    if (item.Id > _id)
                        _id = item.Id;
                };
                ++_id;
            };
        }

        private List<Product> LoadData()
        {
            var items = new List<Product>();
            try
            {
                //Make sure the file exists
                if (!File.Exists(_filename))
                    return items;

                var lines = File.ReadAllLines(_filename);
                foreach (var line in lines)
                {
                    var fields = line.Split(',');

                    //Not checking for missing fields here
                    var product = new Product() {
                        Id = ParseInt32(fields[0]),
                        Name = fields[1],
                        Description = fields[2],
                        Price = ParseDecimal(fields[3]),
                        IsDiscontinued = ParseInt32(fields[4]) != 0
                    };
                    items.Add(product);
                };

                return items;
            } catch (Exception e)
            {
                throw new Exception("Failure loading data", e);
            };
        }

        private decimal ParseDecimal( string value )
        {
            if (Decimal.TryParse(value, out var result))
                return result;

            return -1;
        }

        private int ParseInt32 ( string value )
        {
            if (Int32.TryParse(value, out var result))
                return result;

            return -1;
        }

        protected override Product GetCore( int id )
        {
            EnsureInitialized();

            foreach (var item in _items)
            {
                if (item.Id == id)
                    return item;
            };

            return null;
        }

        protected override Product GetProductByNameCore( string name )
        {
            EnsureInitialized();

            foreach (var item in _items)
            {
                if (String.Compare(item.Name, name, true) == 0)
                    return item;
            };

            return null;
        }

        protected override void RemoveCore( int id )
        {
            EnsureInitialized();

            var product = GetCore(id);
            if (product != null)
            {
                _items.Remove(product);
                SaveDataNonstream();
            };
        }

        protected override Product UpdateCore( Product product )
        {
            EnsureInitialized();

            var existing = GetCore(product.Id);
           
            _items.Remove(existing);
            _items.Add(product);

            SaveDataNonstream();

            return product;   
        }

        private void SaveData()
        {
            try
            {
                var stream = File.OpenWrite(_filename);
                var writer = new StreamWriter(stream);

                foreach (var item in _items)
                {
                    var line = $"{item.Id},{item.Name},{item.Description},{item.Price},{(item.IsDiscontinued ? 1 : 0)}";

                    writer.WriteLine(line);
                };

                writer.Close();
                stream.Close();
            } catch (ArgumentException e)
            {
                //Never right!!!
                //throw e;
                throw;
            } catch (Exception e)
            {
                throw new Exception("Save failed", e);
            };
        }

        private void SaveDataNonstream ()
        {
            var lines = new List<string>();

            foreach (var item in _items)
            {
                var line = $"{item.Id},{item.Name},{item.Description},{item.Price},{(item.IsDiscontinued ? 1 : 0)}";
                lines.Add(line);
            };

            File.WriteAllLines(_filename, lines);
        }

        private readonly string _filename;
        private List<Product> _items;
        private int _id;
    }
}
