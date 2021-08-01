using System.Data;
using System.Data.SqlClient;
using System.Windows;

namespace TesteWPFItau
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            LoadGrid();
        }
        SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-J4G9GTD\SQLEXPRESS;Initial Catalog=TesteWPF;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
        
        public void ClearData()
        {
            
            Nome_txt.Clear();
            RG_txt.Clear();
            CPF_txt.Clear();
            Endereco_txt.Clear();
            Produto_txt.Clear();
            Preco_txt.Clear();
        }
        public void LoadGrid()
        {
            SqlCommand cmd = new SqlCommand("select * from [dbo].[Clientes]", con);
            DataTable dt = new DataTable();
            con.Open();
            SqlDataReader sdr = cmd.ExecuteReader();
            dt.Load(sdr);
            con.Close();
            dataGrid.ItemsSource = dt.DefaultView;
        }

        private void ClearBtn_Click(object sender, RoutedEventArgs e)
        {
            ClearData();
        }
        public bool IsValid()
        {
            if (Nome_txt.Text == string.Empty)
            {
                MessageBox.Show("Nome é necessario", "Failed", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            return true;
        }

        private void InsertBtn_Click(object sender, RoutedEventArgs e)
        {
            if (IsValid())
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("INSERT INTO [dbo].[Clientes] Values ( @Nome, @RG, @CPF, @Endereco, @Produto, @Preco) ", con);
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.AddWithValue("@Nome", Nome_txt.Text);
                    cmd.Parameters.AddWithValue("@RG", RG_txt.Text);
                    cmd.Parameters.AddWithValue("@CPF", CPF_txt.Text);
                    cmd.Parameters.AddWithValue("@Enderco", Endereco_txt.Text);
                    cmd.Parameters.AddWithValue("@Produto", Produto_txt.Text);
                    cmd.Parameters.AddWithValue("@Preco", Preco_txt.Text);
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                    LoadGrid();
                    MessageBox.Show("Salvo com Sucesso", "Salvo", MessageBoxButton.OK, MessageBoxImage.Information);
                    ClearData();
                }
                catch (SqlException ex)
                {

                    MessageBox.Show("Não Foi Inserido" + ex.Message);
                }
            }

        }

        private void DeleteBtn_Click(object sender, RoutedEventArgs e)
        {
            con.Open();
            SqlCommand cmd = new SqlCommand("delete from [dbo].[Clientes] where Id = "+ Id_int.Text +"", con);
            try
            {
                cmd.ExecuteNonQuery();
                MessageBox.Show("Deletado com Sucesso", "Deletado", MessageBoxButton.OK, MessageBoxImage.Information);
                
               
            }
            catch (SqlException ex)
            {

                MessageBox.Show("Não Foi Deletado" + ex.Message);
            }
            finally
            {
                con.Close();
                ClearData();
                LoadGrid();
            }
        }

        private void UpdateBtn_Click(object sender, RoutedEventArgs e)
        {
            con.Open();
            SqlCommand cmd = new SqlCommand("Update [dbo].[Clientes] set Nome = '"+Nome_txt.Text+"', RG = '"+RG_txt.Text+"', CPF = '"+CPF_txt.Text+"', Endereco = '"+Endereco_txt.Text+"', Produto = '"+Produto_txt.Text+"', Preco = '"+Preco_txt.Text+"' Where Id = '"+Id_int.Text+"' ", con);
            try
            {
                cmd.ExecuteNonQuery();
                MessageBox.Show("Editado com Sucesso", "Editado", MessageBoxButton.OK, MessageBoxImage.Information);
               
            }
            catch (SqlException ex)
            {

                MessageBox.Show("Não Foi Editado" + ex.Message);
            }
            finally
            {
                con.Close();
                ClearData();
                LoadGrid();
            }
        }
    }
}
