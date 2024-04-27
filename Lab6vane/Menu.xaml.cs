using Microsoft.Win32;
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
                    int idProducto = reader.GetInt32("idproducto");
                    // string categoriaProducto = reader.GetString("categoriaProducto");
                    // Para llamar columnas con datos Null
                    string categoriaProducto = reader.IsDBNull(reader.GetOrdinal("categoriaProducto")) ? null : reader.GetString("categoriaProducto");

                    productos.Add(new Producto
                    {
                        NombreProducto = nombreProducto,
                        CantidadPorUnidad = cantidadPorUnidad,
                        PrecioUnidad = precioUnidad,
                        CategoriaProducto = categoriaProducto,
                        IdProducto = idProducto
                    });

                }

                listproductos.ItemsSource = productos;
            }

            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar los productos: {ex.Message}");
            }

        }
        public static void EliminarRegistro(int id)
        {
            string connectionString = "Data Source=VANESSA\\SQLEXPRESS;Initial Catalog=NeptunoDB;User Id=vanne;Password=123456";

            using (SqlConnection conn = new SqlConnection(connectionString))
            {

                using (SqlCommand cmd = new SqlCommand("DeleteProduct", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@IdProducto", id);

                    conn.Open();
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Producto eliminado correctamente :D");
                }
            }
        }

        public void DeleteClick(object sender, RoutedEventArgs e)
        {
            try
            {
                Producto registroSeleccionado = (Producto)listproductos.SelectedItem;



                // Obtener el ID del registro seleccionado
                int id = registroSeleccionado.IdProducto;
                MessageBox.Show("IdProducto del registro seleccionado: " + registroSeleccionado.IdProducto);


                // Verifica si el ID es cero
                if (id == 0)
                {
                    MessageBox.Show("El ID del registro seleccionado es cero.");
                    return;
                }

                // Llama a la función para eliminar el registro
                EliminarRegistro(id);
                showproducts();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al intentar eliminar el producto: " + ex.Message);
            }
        }

        private void UpdateProductClick(object sender, RoutedEventArgs e)
        {
            Producto registroSeleccionado = (Producto)listproductos.SelectedItem;
            int id = registroSeleccionado.IdProducto;
            MessageBox.Show("IdProducto del registro seleccionado: " + registroSeleccionado.IdProducto);

            // Verifica si el ID es cero
            if (id == 0)
            {
                MessageBox.Show("El ID del registro seleccionado es cero.");
                return;
            }

            UpdateProduct updateProduct = new UpdateProduct(id);
            updateProduct.ShowDialog();
        }


        private void Button_Click(object sender, RoutedEventArgs e)
        {
            RegistrarProducto registrarform = new RegistrarProducto();
            registrarform.ShowDialog();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            showproducts();
        }
    }
}
