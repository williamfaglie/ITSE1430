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
using Nile.Data;
using Nile.Data.IO;
using Nile.Data.Memory;
using Nile.Data.Sql;

namespace Nile.Windows
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

            //_database = new FileProductDatabase("products.csv");

            var connString = ConfigurationManager.ConnectionStrings["NileDatabase"];
            _database = new SqlProductDatabase(connString.ConnectionString);

            RefreshUI();
        }

        private void RefreshUI()
        {
            //Get products
            IEnumerable<Product> products = null;
            try
            {
                products = _database.GetAll();
            } catch (Exception)
            {
                MessageBox.Show("Error loading products");
            };

            productBindingSource.DataSource = products?.ToList();
        }

        private void PlayingWithProductMembers()
        {
            var product = new Product();

            Decimal.TryParse("123", out var price);
            product.Price = price;

            var name = product.Name;
            //var name = product.GetName();
            //product.Name = "Product A";
            product.Name = "Product A";
            product.Price = 50;
            product.IsDiscontinued = true;

            //product.ActualPrice = 10;
            var price2 = product.ActualPrice;

            //product.SetName("Product A");
            //product.Description = "None";
            //var error = product.Validate();

            var str = product.ToString();

            var productB = new Product();
            //productB.Name = "Product B";
            //productB.SetName("Product B");
            //productB.Description = product.Description;
            //error = productB.Validate();
        }

        private void OnProductAdd( object sender, EventArgs e )
        {
            var button = sender as ToolStripMenuItem;

            var form = new ProductDetailForm("Add Product");
            //form.Text = "Add Product";

            //Show form modally
            var result = form.ShowDialog(this);
            if (result != DialogResult.OK)
                return;

            //Add to database
            //_database.Add(form.Product);
            try
            {
                _database.Add(null);
            } catch (NotImplementedException)
            {
                MessageBox.Show("not implemented yet");
            } catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            };


            RefreshUI();

            //var index = FindEmptyProductIndex();
            //    if (index >= 0)
            //        _products[index] = form.Product;

        }

        private void OnProductRemove( object sender, EventArgs e )
        {
            //Get the selected product
            var product = GetSelectedProduct();
            if (product == null)
            {
                MessageBox.Show("No products selected", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            };

            DeleteProduct(product);
        }

        private void DeleteProduct( Product product )
        {
            if (!ShowConfirmation("Are you sure?", "Remove Product"))
                return;

            //Remove product
            try
            {
                _database.Remove(product.Id);
            } catch (Exception e)
            {
                MessageBox.Show(e.Message);
            };

            RefreshUI();
        }

        private void OnProductEdit( object sender, EventArgs e )
        {
            //Get selected product
            var product = GetSelectedProduct();
            if (product == null)
            {
                MessageBox.Show("No products selected", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            };


            EditProduct(product);
        }

        private void EditProduct( Product product )
        {
            var form = new ProductDetailForm(product);
            var result = form.ShowDialog(this);
            if (result != DialogResult.OK)
                return;

            //Update the product
            form.Product.Id = product.Id;

            try
            {
                _database.Update(form.Product);
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
            MessageBox.Show(this, "Not Implemented", "Help About", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
        }

        private bool ShowConfirmation( string message, string title )
        {
            return MessageBox.Show(this, message, title, MessageBoxButtons.YesNo, MessageBoxIcon.Question)
                == DialogResult.Yes;
        }

        //private sealed class SelectedRowType
        //{
        //    public int Index { get; set; }
        //    public Product Product { get; set; }
        //}
        private Product GetSelectedProduct ()
        {
            //This is correct, just demoing something new...
            // Get the first selected row in the grid, if any
            //var items = (from r in dataGridView1.SelectedRows.OfType<DataGridViewRow>()
            //       select new SelectedRowType() {
            //           Index = r.Index,
            //           Product = r.DataBoundItem as Product
            //       }).FirstOrDefault();

            //Anonymous type items
            var items = (from r in dataGridView1.SelectedRows.OfType<DataGridViewRow>()
                         select new {
                             Index = r.Index,
                             Product = r.DataBoundItem as Product
                         }).FirstOrDefault();


            return items.Product;

            //if (dataGridView1.SelectedRows.Count > 0)
            //return dataGridView1.SelectedRows[0].DataBoundItem as Product;

            //return null;
        }


        private IProductDatabase _database;

        private void OnCellDoubleClick( object sender, DataGridViewCellEventArgs e )
        {
            var product = GetSelectedProduct();
            if (product == null)
                return;

            EditProduct(product);
        }

        private void OnCellKeyDown( object sender, KeyEventArgs e )
        {
            var product = GetSelectedProduct();
            if (product == null)
                return;

            if (e.KeyCode == Keys.Delete)
            {
               e.Handled = true;
               DeleteProduct(product);
            } else if (e.KeyCode == Keys.Enter)
            {
               e.Handled = true;
               EditProduct(product);
            };
        }
    }
}
