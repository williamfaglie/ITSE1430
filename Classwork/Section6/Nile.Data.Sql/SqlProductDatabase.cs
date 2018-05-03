﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nile.Data.Sql
{
    public class SqlProductDatabase : ProductDatabase
    {
        private readonly string _connectionString;

        public SqlProductDatabase( string connectionString )
        {
            if (connectionString == null)
                throw new ArgumentNullException(nameof(connectionString));
            if (connectionString == "")
                throw new ArgumentException("Connection string cannot be empty.",
                    nameof(connectionString));

            _connectionString = connectionString;
        }
        protected override Product AddCore( Product product )
        {
            using (var conn = new SqlConnection(_connectionString))
            {
                var cmd = new SqlCommand("AddProduct", conn);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;


                cmd.Parameters.AddWithValue("@name", product.Name);
                cmd.Parameters.AddWithValue("@price", product.Price);
                cmd.Parameters.AddWithValue("@description", product.Description);

                var parm = cmd.CreateParameter();
                parm.ParameterName = "@isDiscontinued";
                parm.DbType = System.Data.DbType.Boolean;
                parm.Value = product.IsDiscontinued;
                cmd.Parameters.Add(parm);

                conn.Open();
                var result = cmd.ExecuteScalar();

                var id = Convert.ToInt32(result);
                product.Id = id;
            };

            return product;
        }
    


        protected override IEnumerable<Product> GetAllCore()
        {
            var items = new List<Product>();

            using (var conn = new SqlConnection(_connectionString))
            {
                var cmd = new SqlCommand("GetAllProducts", conn);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                conn.Open();

                var ds = new DataSet();

                var da = new SqlDataAdapter();
                da.SelectCommand = cmd;

                da.Fill(ds);

                if (ds.Tables.Count == 1)
                {
                    foreach (var row in ds.Tables[0].Rows.OfType<DataRow>())
                    {
                        //row["Id"] = 10;
                        //row.SetField("Id", 10);

                        var product = new Product() {
                            Id = Convert.ToInt32(row["Id"]),
                            Name = row.Field<string>("Name"),
                            Description = row.Field<string>("Description"),
                            Price = row.Field<decimal>("Price"),
                            IsDiscontinued = row.Field<bool>("IsDiscontinued")
                        };

                        items.Add(product);
                    };
                };
            };

            return items;
        }

        protected override Product GetCore( int id )
        {
            using (var conn = new SqlConnection(_connectionString))
            {
                var cmd = new SqlCommand("GetProduct", conn);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter("@id", id));

                conn.Open();

                using (var reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                        return ReadData(reader);                     
                };
            };

            return null; 
        }

        protected override Product GetProductByNameCore( string name )
        {
            using (var conn = new SqlConnection(_connectionString))
            {
                var cmd = new SqlCommand("GetAllProducts", conn);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                conn.Open();

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var product = ReadData(reader);
                        if (String.Compare(product.Name, name, true) == 0)
                        return product;
                    };
                };
            };

            return null;
        }

        private static Product ReadData( SqlDataReader reader )
        {
            return new Product() {
                Id = Convert.ToInt32(reader["Id"]),
                Name = reader.GetFieldValue<string>(1),
                Price = reader.GetDecimal(2),
                Description = reader.GetString(3),
                IsDiscontinued = reader.GetBoolean(4)
            };
        }

        protected override void RemoveCore( int id )
        {
            using (var conn = new SqlConnection(_connectionString))
            {
                var cmd = new SqlCommand("RemoveProduct", conn);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter("@id", id));

                conn.Open();
                cmd.ExecuteNonQuery();
            };
        }

        protected override Product UpdateCore( Product product )
        {
            using (var conn = new SqlConnection(_connectionString))
            {
                var cmd = new SqlCommand("UpdateProduct", conn);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter("@id", product.Id));
                cmd.Parameters.AddWithValue("@name", product.Name);
                cmd.Parameters.AddWithValue("@price", product.Price);
                cmd.Parameters.AddWithValue("@description", product.Description);

                var parm = cmd.CreateParameter();
                parm.ParameterName = "@isDiscontinued";
                parm.DbType = System.Data.DbType.Boolean;
                parm.Value = product.IsDiscontinued;
                cmd.Parameters.Add(parm);

                conn.Open();
                cmd.ExecuteNonQuery();
            };

            return product;
        }
    }
}
