using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WilliamFaglie.MovieLib.Data
{
    class IMovieDatabase
    {
        Product Add( Product product, out string message );
        Product Update( Product product, out string message );
        IEnumerable<Product> GetAll();
        void Remove( int id );
    }
}
