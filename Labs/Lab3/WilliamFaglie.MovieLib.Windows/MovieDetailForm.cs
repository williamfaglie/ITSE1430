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

/// <summary>Allows you to edit and add movies.</summary>
namespace WilliamFaglie.MovieLib.Windows
{
    public partial class MovieDetailForm : Form
    {
        /// <summary>Initializes the form. Constructor.</summary>
        public MovieDetailForm()
        {
            InitializeComponent();
        }

        /// <summary>Allows you to enter the movie title</summary>
        /// <param name="title"></param>
        public MovieDetailForm( string title ) : this()
        { 

            Text = title;
        }

        /// <summary>Creates a movie.</summary>
        /// <param name="movie"></param>
        public MovieDetailForm( Movie movie) : this("Update Product")
        {
            Movie = movie;
        }

        public Movie Movie { get; set; }

        /// <summary>Controls loading.</summary>
        protected override void OnLoad( EventArgs e )
        {
            //Call base type
            base.OnLoad(e);

            //Load movie
            if (Movie != null)
            {
                _txtTitle.Text = Movie.Title;
                _txtDescription.Text = Movie.Description;
                _txtLength.Text = Movie.Length.ToString();
                _checkIsOwned.Checked = Movie.IsOwned;
            };

            ValidateChildren();
        }

        /// <summary>Controls description.</summary>
        private void _txtDescription_TextChanged( object sender, EventArgs e )
        {

        }

        /// <summary>Controls clicks.</summary>
        private void label2_Click( object sender, EventArgs e )
        {

        }

        /// <summary>Controls what happens when cancel is clicked.</summary>
        private void OnCancel( object sender, EventArgs e )
        {
            
        }

        /// <summary>Controls what happens when Save is clicked.</summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnSave( object sender, EventArgs e )
        {
            //Force validation of child controls
            if (!ValidateChildren())
                return;


            // Create product
            var movie = new Movie() {
                Title = _txtTitle.Text,
                Description = _txtDescription.Text,
                Length = ConvertToLength(_txtLength),
                IsOwned = _checkIsOwned.Checked,
            };

            //Validate
            var errors = ObjectValidator.Validate(movie);
            if (errors.Count() > 0)
            {
                //Get first error
                DisplayError(errors.ElementAt(0).ErrorMessage);
                return;
            };

            //Return form form
            Movie = movie;
            DialogResult = DialogResult.OK;
            Close();
        }

        /// <summary>Displays error.</summary>
        private void DisplayError( object message )
        {
            throw new NotImplementedException();
        }

        /// <summary>Displays error.</summary>
        /// <param name="message"></param>
        private void DisplayError ( string message )
        {
            MessageBox.Show(this, message, "Error", MessageBoxButtons.OK,
                MessageBoxIcon.Error);

        }

        /// <summary>Converts to price.</summary>
        private decimal ConvertToLength( TextBox control )
        {
            if (Decimal.TryParse(control.Text, out var price))
                return price;

            return 0;
        }

        /// <summary>Validation.</summary>
        private void _txtTitle_Validating( object sender, CancelEventArgs e )
        {
            var textbox = sender as TextBox;

            if (String.IsNullOrEmpty(textbox.Text))
            {
                _errorProvider.SetError(textbox, "Title is required");
                e.Cancel = true;
            } else
                _errorProvider.SetError(textbox, "");
        }

        /// <summary>Validation.</summary>
        private void _txtPrice_Validating( object sender, CancelEventArgs e )
        {
            var textbox = sender as TextBox;

            var length = ConvertToLength(textbox);
            if (length < 0)
            {
                _errorProvider.SetError(textbox, "Length must be >= 0");
                e.Cancel = true;
            } else
                _errorProvider.SetError(textbox, "");
        }
    }
}
