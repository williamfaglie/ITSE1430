//////////////////////////
//Filename: MainForm.cs
//Author: William Faglie
//Description: This is my MainForm class
//////////////////////////
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WilliamFaglie.MovieLib;
using WilliamFaglie.MovieLib.Data;
using WilliamFaglie.MovieLib.Data.Memory;
using WilliamFaglie.MovieLib.Data.Sql;

namespace WilliamFaglie.MovieLib.Windows
{
    /// <summary>Opening form to movie-add, movie-edit, movie-remove, file-exit, and help-about</summary>
    public partial class MainForm : Form
    {
        /// <summary>Initializes MainForm.</summary>
        public MainForm()
        {
            InitializeComponent();
        }

        protected override void OnLoad( EventArgs e )
        {
            base.OnLoad(e);

            var connString = ConfigurationManager.ConnectionStrings["MovieDatabase"];
            _database = new SqlMovieDatabase(connString.ConnectionString);


            RefreshUI();
        }

        private void RefreshUI()
        {
            //Get products
            IEnumerable<Movie> movies = null;
            try
            {
                movies = _database.GetAll();
            } catch (Exception)
            {
                MessageBox.Show("Error loading movies");
            }

            movieBindingSource.DataSource = movies?.ToList();
        }

        private void OnProductAdd( object sender, EventArgs e )
        {
            var button = sender as ToolStripMenuItem;

            var form = new MovieDetailForm("Add Movie");

            //Show form modally
            var result = form.ShowDialog(this);
            if (result != DialogResult.OK)
                return;

            //Add to database
            try
            {
                _database.Add(form.Movie);
            } catch (NotImplementedException)
            {
                MessageBox.Show("not implemented yet");
            } catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            };

            RefreshUI();
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
            try
            {
                _database.Remove(movie.Id);
            } catch (Exception e)
            {
                MessageBox.Show(e.Message);
            };

            RefreshUI();
        }

        private void OnProductEdit( object sender, EventArgs e )
        {
            //Get selected movie
            var movie = GetSelectedMovie();
            if (movie == null)
            {
                MessageBox.Show("No movies selected", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            };
            EditMovie(movie);
        }

        private void EditMovie( Movie movie )
        {
            var form = new MovieDetailForm(movie);
            var result = form.ShowDialog(this);
            if (result != DialogResult.OK)
                return;

            //Update the movie
            form.Movie.Id = movie.Id;

            try
            {
                _database.Update(form.Movie);
            } catch (Exception e)
            {
                MessageBox.Show(e.Message);
            };

            RefreshUI();
        }

        private void OnFileExit( object sender, EventArgs e )
        {
            Close();
        }

        private void OnHelpAbout( object sender, EventArgs e )
        {
            var form = new OnHelpAboutForm();

            var result = form.ShowDialog(this);
            if (result != DialogResult.OK)
                return;
        }

        private bool ShowConfirmation( string message, string title )
        {
            return MessageBox.Show(this, message, title, MessageBoxButtons.YesNo, MessageBoxIcon.Question)
                == DialogResult.Yes;
        }

        private Movie GetSelectedMovie()
        {
            //Anonymous type items
            var items = (from r in dataGridView1.SelectedRows.OfType<DataGridViewRow>()
                         select new {
                             Index = r.Index,
                             Movie = r.DataBoundItem as Movie
                         }).FirstOrDefault();


            return items.Movie;
        }


        private IMovieDatabase _database;

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
