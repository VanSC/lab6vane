using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace lab6vane
{
    /// <summary>
    /// Lógica de interacción para Menu.xaml
    /// </summary>
    public partial class Menu : Window
    {
        string connectionString = "Data Source=VANESSA\\SQLEXPRESS;Initial Catalog=NeptunoDB;User Id=vanne;Password=123456";
        public Menu()
        {
            InitializeComponent();
            showproducts();
        }

        public void showproducts()
        {
            try
            {
                List<Producto> productos = new List<Producto>();
                SqlConnection connection = new SqlConnection(connectionString);
                connection.Open();

                SqlCommand command = new SqlCommand("sp_GetProdcuts", connection);
                command.CommandType = CommandType.StoredProcedure;

                // CONECTADA
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    string nombreProducto = reader.GetString("nombreProducto");
                    string cantidadPorUnidad = reader.GetString("cantidadPorUnidad");
                    decimal precioUnidad = reader.GetDecimal("precioUnidad");
                    // string categoriaProducto = reader.GetString("categoriaProducto");
                    string categoriaProducto = reader.IsDBNull(reader.GetOrdinal("categoriaProducto")) ? null : reader.GetString("categoriaProducto");

                    productos.Add(new Producto
                    {
                        NombreProducto = nombreProducto,
                        CantidadPorUnidad = cantidadPorUnidad,
                        PrecioUnidad = precioUnidad,
                        CategoriaProducto = categoriaProducto
                    });

                }

                // Crear una nueva lista con solo las columnas deseadas
                var listaFiltrada = productos.Select(p => new
                {
                    Producto = p.NombreProducto,
                    cantidadPorUnidad = p.CantidadPorUnidad,
                    precioUnidad = p.PrecioUnidad,
                    categoriaProducto = p.CategoriaProducto
                }).ToList();

                listproductos.ItemsSource = listaFiltrada;
            }

            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar los productos: {ex.Message}");
            }

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            RegistrarProducto registrarform = new RegistrarProducto();
            registrarform.ShowDialog();
        }
    }
}
