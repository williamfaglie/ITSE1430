﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Nile.Windows
{
    public /*abstract*/ partial class ProductDetailForm : Form
    {
        public ProductDetailForm()
        {
            InitializeComponent();
        }

        public ProductDetailForm( string title ) : this() //: base() Calls Form's base constructor
        {
            //InitializeComponent();

            Text = title;
        }

        public ProductDetailForm( Product product) : this("Edit Product")
        {
            //InitializeComponent();
            //Text = "Edit Product";

            Product = product;
        }

        public Product Product { get; set; }

        //public abstract DialogResult ShowDialogEx();

        //public virtual DialogResult ShowDialogEx()
        //{
        //    return ShowDialog();
        //}

        protected override void OnLoad( EventArgs e )
        {
            //Call base type
            //OnLoad(e);
            base.OnLoad(e);

            //Load product
            if (Product != null)
            {
                _txtName.Text = Product.Name;
                _txtDescription.Text = Product.Description;
                _txtPrice.Text = Product.Price.ToString();
                _checkIsDiscontinued.Checked = Product.IsDiscontinued;
            };

            ValidateChildren();
        }

        private void _txtDescription_TextChanged( object sender, EventArgs e )
        {

        }

        private void label2_Click( object sender, EventArgs e )
        {

        }

        private void OnCancel( object sender, EventArgs e )
        {
            
        }

        private void OnSave( object sender, EventArgs e )
        {
            //Force validation of child controls
            if (!ValidateChildren())
                return;


            // Create product
            var product = new Product();
            product.Name = _txtName.Text;
            product.Description = _txtDescription.Text;
            product.Price = ConvertToPrice(_txtPrice);
            product.IsDiscontinued = _checkIsDiscontinued.Checked;

            //Validate
            var message = product.Validate();
            if (!String.IsNullOrEmpty(message))
            {
                DisplayError(message);
                return;
            };
            //    return;
            //} else
            //    _errorProvider.SetError(_txtName, "");

            //Return form form
            Product = product;
            DialogResult = DialogResult.OK;
            //DialogResult = DialogResult.None;
            Close();
        }

        private void DisplayError( object message )
        {
            throw new NotImplementedException();
        }

        private void DisplayError ( string message )
        {
            MessageBox.Show(this, message, "Error", MessageBoxButtons.OK,
                MessageBoxIcon.Error);

        }

        private decimal ConvertToPrice( TextBox control )
        {
            if (Decimal.TryParse(control.Text, out var price))
                return price;

            return -1;
        }

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
