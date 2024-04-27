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
    /// Lógica de interacción para UpdateProduct.xaml
    /// </summary>
    public partial class UpdateProduct : Window
    {
        //private int productId;

        string connectionString = "Data Source=VANESSA\\SQLEXPRESS;Initial Catalog=NeptunoDB;User Id=vanne;Password=123456";

        private int productId;
        public UpdateProduct(int IdProduct)
        {
            InitializeComponent();
            productId = IdProduct;
        }


        private void UpdateProcutoClick(object sender, RoutedEventArgs e)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                using (SqlCommand cmd = new SqlCommand("sp_UpdateProduct", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;

                    //Valores
                    cmd.Parameters.AddWithValue("@IdProducto", productId);
                    cmd.Parameters.AddWithValue("@NuevoNombreProducto", txtProductName.Text);
                    cmd.Parameters.AddWithValue("@NuevoIdProveedor", int.Parse(txtProveedor.Text));
                    cmd.Parameters.AddWithValue("@NuevoIdCategoria", int.Parse(txtCategoria.Text));
                    cmd.Parameters.AddWithValue("@NuevaCantidadPorUnidad", txtCantUni.Text);
                    cmd.Parameters.AddWithValue("@NuevoPrecioUnidad", decimal.Parse(txtPrecioUnidad.Text));
                    cmd.Parameters.AddWithValue("@NuevasUnidadesEnExistencia", int.Parse(txtUniEnExistencia.Text));
                    cmd.Parameters.AddWithValue("@NuevasUnidadesEnPedido", int.Parse(txtUniPedido.Text));
                    cmd.Parameters.AddWithValue("@NuevoNivelNuevoPedido", int.Parse(txtNivelPedido.Text));
                    cmd.Parameters.AddWithValue("@NuevoSuspendido", int.Parse(txtSuspedido.Text));
                    cmd.Parameters.AddWithValue("@NuevaCategoriaProducto", txtCategoriaProducto.Text);

                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Producto Actualizado correctamente :D");
                }
            }

            Menu menu = new Menu();

            menu.ShowDialog();
            this.Close();
        }

        private void ButtonCancelar_Click(object sender, RoutedEventArgs e)
        {
            // Cerrar la ventana
            this.Close();
        }
    }
}
