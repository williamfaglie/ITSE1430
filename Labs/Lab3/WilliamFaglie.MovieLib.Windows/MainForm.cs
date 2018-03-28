using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WilliamFaglie.MovieLib;
using WilliamFaglie.MovieLib.Data;
using WilliamFaglie.MovieLib.Data.Memory;

/// <summary>Opening form to movie-add, movie-edit, movie-remove, file-exit, and help-about</summary>
namespace WilliamFaglie.MovieLib.Windows
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        protected override void OnLoad( EventArgs e )
        {
            base.OnLoad(e);

            RefreshUI();
        }

        private void RefreshUI()
        {
            //Get products
            var movies = _database.GetAll();

            //Bind to grid
            //productBindingSource.DataSource = new List<Product>(products);
            //productBindingSource.DataSource = Enumerable.ToList(products);
            movieBindingSource.DataSource = movies.ToList();
            //dataGridView1.DataSource
        }

        private void OnProductAdd( object sender, EventArgs e )
        {
            var button = sender as ToolStripMenuItem;

            var form = new MovieDetailForm("Add Product");
            //form.Text = "Add Product";

            //Show form modally
            var result = form.ShowDialog(this);
            if (result != DialogResult.OK)
                return;

            //Add to database
            _database.Add(form.Movie, out var message);
            if (!String.IsNullOrEmpty(message))
                MessageBox.Show(message);

            RefreshUI();

            //var index = FindEmptyProductIndex();
            //    if (index >= 0)
            //        _products[index] = form.Product;

        }

        private void OnProductRemove( object sender, EventArgs e )
        {
            //Get the selected product
            var movie = GetSelectedMovie();
            if (movie == null)
            {
                MessageBox.Show("No movies selected", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            };

            DeleteMovie(movie);
        }

        private void DeleteMovie( Movie movie )
        {
            if (!ShowConfirmation("Are you sure?", "Remove Movie"))
                return;

            //Remove product
            _database.Remove(movie.Id);

            RefreshUI();
        }

        private void OnProductEdit( object sender, EventArgs e )
        {
            //Get selected product
            var product = GetSelectedMovie();
            if (product == null)
            {
                MessageBox.Show("No products selected", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            };
            EditMovie(product);
        }

        private void EditMovie( Movie movie )
        {
            var form = new MovieDetailForm(movie);
            var result = form.ShowDialog(this);
            if (result != DialogResult.OK)
                return;

            //Update the movie
            form.Movie.Id = movie.Id;
            _database.Update(form.Movie, out var message);
            if (!String.IsNullOrEmpty(message))
                MessageBox.Show(message);

            RefreshUI();
        }

        private void OnFileExit( object sender, EventArgs e )
        {
            Close();
        }

        private void OnHelpAbout( object sender, EventArgs e )
        {
            MessageBox.Show(this, "Not Implemented", "Help About", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
        }

        private bool ShowConfirmation( string message, string title )
        {
            return MessageBox.Show(this, message, title, MessageBoxButtons.YesNo, MessageBoxIcon.Question)
                == DialogResult.Yes;
        }

        private Movie GetSelectedMovie()
        {
            if (dataGridView1.SelectedRows.Count > 0)
                return dataGridView1.SelectedRows[0].DataBoundItem as Movie;

            return null;
        }


        private IMovieDatabase _database = new MemoryMovieDatabase();

        private void OnCellDoubleClick( object sender, DataGridViewCellEventArgs e )
        {
            var movie = GetSelectedMovie();
            if (movie == null)
                return;

            EditMovie(movie);
        }

        private void OnCellKeyDown( object sender, KeyEventArgs e )
        {
            var movie = GetSelectedMovie();
            if (movie == null)
                return;

            if (e.KeyCode == Keys.Delete)
            {
                e.Handled = true;
                DeleteMovie(movie);
            } else if (e.KeyCode == Keys.Enter)
            {
                e.Handled = true;
                EditMovie(movie);
            };
        }
    }
}
