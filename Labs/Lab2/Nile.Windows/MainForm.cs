using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

/// <summary>Opening form to movie-add, movie-edit, movie-remove, file-exit, and help-about</summary>
namespace Nile.Windows
{
    public partial class MainForm : Form
    {
        /// <summary>Constructor.</summary>
        public MainForm()
        {
            InitializeComponent();
        }

        /// <summary>Loads form.</summary>
        protected override void OnLoad( EventArgs e )
        {
            base.OnLoad(e);
        }

        /// <summary>Controls form for adding movie.</summary>
        private void OnProductAdd( object sender, EventArgs e )
        {
            var button = sender as ToolStripMenuItem;

            var form = new MovieDetailForm("Add Movie");

            //Show form modally
            var result = form.ShowDialog(this);
            if (result != DialogResult.OK)
                return;

            //"Add" the movie
            _movie = form.Product;
        }

        /// <summary>Controls form for removing movie.</summary>
        private void OnProductRemove( object sender, EventArgs e )
        {
            if (_movie == null)
            {
                MessageBox.Show(this, "No Movies to Delete", "Delete", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            var name = Name;

            if (!ShowConfirmation(String.Format("Are you sure you want to delete {0}", name) + "?", "Remove Movie"))
                return;
            
            //Remove product
            _movie = null;
        }

        /// <summary>Controls form for editing movie.</summary>
        private void OnProductEdit( object sender, EventArgs e )
        {
            if (_movie == null)
            {
                MessageBox.Show(this, "No Movies to Edit", "Edit", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            var form = new MovieDetailForm(_movie);

            //Show form modally
            var result = form.ShowDialog(this);
            if (result != DialogResult.OK)
                return;

            //"Editing" the product
            _movie = form.Product;
        }

        /// <summary>Controls exiting.</summary>
        private void OnFileExit( object sender, EventArgs e )
        {
            Close();
        }

        /// <summary>Controls about page.</summary>
        private void OnHelpAbout( object sender, EventArgs e )
        {
            var form = new OnHelpAbout();

            var result = form.ShowDialog(this);
            if (result != DialogResult.OK)
                return;
        }

        /// <summary>Controls confirmation dialog.</summary>
        private bool ShowConfirmation( string message, string title )
        {
            return MessageBox.Show(this, message, title, MessageBoxButtons.YesNo, MessageBoxIcon.Question)
                == DialogResult.Yes;
        }


        private Movie _movie;

        /// <summary>Controls menu strip items.</summary>
        private void productToolStripMenuItem_Click( object sender, EventArgs e )
        {

        }
    }
}
