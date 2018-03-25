using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

/// <summary>Allows you to edit and add movies.</summary>
namespace Nile.Windows
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
        /// <param name="product"></param>
        public MovieDetailForm( Movie product) : this("Edit Product")
        {
            Product = product;
        }

        public Movie Product { get; set; }

        /// <summary>Controls loading.</summary>
        protected override void OnLoad( EventArgs e )
        {
            //Call base type
            base.OnLoad(e);

            //Load product
            if (Product != null)
            {
                _txtTitle.Text = Product.Name;
                _txtDescription.Text = Product.Description;
                _txtLength.Text = Product.Price.ToString();
                _checkIsOwned.Checked = Product.IsDiscontinued;
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
            var movie = new Movie();
            movie.Name = _txtTitle.Text;
            movie.Description = _txtDescription.Text;
            movie.Price = ConvertToPrice(_txtLength);
            movie.IsDiscontinued = _checkIsOwned.Checked;

            //Validate
            var message = movie.Validate();
            if (!String.IsNullOrEmpty(message))
            {
                DisplayError(message);
                return;
            };

            //Return form form
            Product = movie;
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
        private decimal ConvertToPrice( TextBox control )
        {
            if (Decimal.TryParse(control.Text, out var price))
                return price;

            return 0;
        }

        /// <summary>Validation.</summary>
        private void _txtName_Validating( object sender, CancelEventArgs e )
        {
            var textbox = sender as TextBox;

            if (String.IsNullOrEmpty(textbox.Text))
            {
                _errorProvider.SetError(textbox, "Name is required");
                e.Cancel = true;
            } else
                _errorProvider.SetError(textbox, "");
        }

        /// <summary>Validation.</summary>
        private void _txtPrice_Validating( object sender, CancelEventArgs e )
        {
            var textbox = sender as TextBox;

            var price = ConvertToPrice(textbox);
            if (price < 0)
            {
                _errorProvider.SetError(textbox, "Price must be >= 0");
                e.Cancel = true;
            } else
                _errorProvider.SetError(textbox, "");
        }
    }
}
