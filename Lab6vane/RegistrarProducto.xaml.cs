using System;
using System.Collections.Generic;
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
    /// Lógica de interacción para RegistrarProducto.xaml
    /// </summary>
    public partial class RegistrarProducto : Window
    {
        string connectionString = "Data Source=VANESSA\\SQLEXPRESS;Initial Catalog=NeptunoDB;User Id=vanne;Password=123456";

        public RegistrarProducto()
        {
            InitializeComponent();
        }

        private void RegistrarProcutoClick(object sender, RoutedEventArgs e)
        {

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                using (SqlCommand cmd = new SqlCommand("sp_AddProduct", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;

                    //Valores
                    cmd.Parameters.AddWithValue("@nombreProducto", txtProductName.Text);
                    cmd.Parameters.AddWithValue("@idProveedor", int.Parse( txtProveedor.Text));
                    cmd.Parameters.AddWithValue("@idCategoria", int.Parse(txtCategoria.Text));
                    cmd.Parameters.AddWithValue("@cantidadPorUnidad", txtCantUni.Text);
                    cmd.Parameters.AddWithValue("@precioUnidad", decimal.Parse(txtPrecioUnidad.Text));
                    cmd.Parameters.AddWithValue("@unidadesEnExistencia", int.Parse(txtUniEnExistencia.Text));
                    cmd.Parameters.AddWithValue("@unidadesEnPedido", int.Parse(txtUniPedido.Text));
                    cmd.Parameters.AddWithValue("@nivelNuevoPedido", int.Parse(txtNivelPedido.Text));
                    cmd.Parameters.AddWithValue("@suspendido", int.Parse(txtSuspedido.Text));
                    cmd.Parameters.AddWithValue("@categoriaProducto", txtCategoriaProducto.Text);

                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Producto registrado correctamente :D");
                }
            }
        }

        private void ButtonCancelar_Click(object sender, RoutedEventArgs e)
        {
            Menu list = new Menu();
            list.ShowDialog();
        }
    }
}
