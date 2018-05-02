//////////////////////////
//Filename: IMovieDatabase.cs
//Author: William Faglie
//Description: This is my IMovieDatabase class
//////////////////////////
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WilliamFaglie.MovieLib.Data
{
    /// <summary>Interface movie database.</summary>
    public interface IMovieDatabase
    {
        Movie Add( Movie movie );
        Movie Update( Movie movie );
        IEnumerable<Movie> GetAll();
        void Remove( int id );
    }
}
