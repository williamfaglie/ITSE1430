//////////////////////////
//Filename: FileMovieDatabase.cs
//Author: William Faglie
//Description: This is my FileMovieDatabase class
//////////////////////////
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WilliamFaglie.MovieLib;
using WilliamFaglie.MovieLib.Data;

namespace WilliamFaglie.MovieLib.Data.IO
{
    /// <summary>Loads movie from file, creates file for contents to be read into.</summary>
    public class FileMovieDatabase : MovieDatabase
    {
        /// <summary>Declares filepath to file in the constructor.</summary>
        /// <param name="filename"></param>
        public FileMovieDatabase( string filename )
        {
            _filename = filename;
        }
        protected override Movie AddCore( Movie movie )
        {
            EnsureInitialized();

            movie.Id = _id++;
            _items.Add(movie);

            SaveDataNonstream();

            return movie;
        }

        protected override IEnumerable<Movie> GetAllCore()
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

                if (_items.Any())
                {
                    _id = _items.Max(i => i.Id);
                    ++_id;
                };
            };
        }

        private List<Movie> LoadData()
        {
            var items = new List<Movie>();
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
                    var movie = new Movie() {
                        Id = ParseInt32(fields[0]),
                        Title = fields[1],
                        Description = fields[2],
                        Length = ParseDecimal(fields[3]),
                        IsOwned = ParseInt32(fields[4]) != 0
                    };
                    items.Add(movie);
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

        protected override Movie GetCore( int id )
        {
            EnsureInitialized();

            return _items.FirstOrDefault(i => i.Id == id);
        }

        protected override Movie GetMovieByTitleCore( string title )
        {
            EnsureInitialized();

            return _items.FirstOrDefault(i => String.Compare(i.Title, title, true) == 0);
        }

        protected override void RemoveCore( int id )
        {
            EnsureInitialized();

            var movie = GetCore(id);
            if (movie != null)
            {
                _items.Remove(movie);
                SaveDataNonstream();
            };
        }

        protected override Movie UpdateCore( Movie movie )
        {
            EnsureInitialized();

            var existing = GetCore(movie.Id);
           
            _items.Remove(existing);
            _items.Add(movie);

            SaveDataNonstream();

            return movie;   
        }

        private void SaveData()
        {
            using (var stream = File.OpenWrite(_filename))
            using (var writer = new StreamWriter(stream))
            {
                foreach (var item in _items)
                {
                    var line = $"{item.Id},{item.Title},{item.Description},{item.Length},{(item.IsOwned ? 1 : 0)}";

                    writer.WriteLine(line);
                };
            };
        }

        private void SaveDataNonstream ()
        {
            var lines = _items.Select(item => $"{item.Id},{item.Title},{item.Description},{item.Length},{(item.IsOwned ? 1 : 0)}");

            File.WriteAllLines(_filename, lines);
        }

        private readonly string _filename;
        private List<Movie> _items;
        private int _id;
    }
}
