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
    public partial class ProductDetailForm : Form
    {
        public ProductDetailForm()
        {
            InitializeComponent();
        }

        public Product Product { get; set; }

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
            // Create product
            var product = new Product();
            product.Name = _txtName.Text;
            product.Decsription = _txtDescription.Text;
            product.Price = ConvertToPrice(_txtPrice);
            product.IsDiscontinued = _checkIsDiscontinued.Checked;

            //Return form form
            Product = product;
            DialogResult = DialogResult.OK;
            //DialogResult = DialogResult.None;
            Close();
        }

        private decimal ConvertToPrice( TextBox control )
        {
            if (Decimal.TryParse(control.Text, out var price))
                return price;

            return -1;
        }
    }
}
