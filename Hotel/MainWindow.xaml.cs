using Hotel.Model;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Hotel
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Admin admin;
            using (var context = new HotelEntities())
            {
                var login = Login.Text;
                var password = Password.Text;
                admin = context.Admin.FirstOrDefault(a => a.Login == login && a.Password == password);
                if (admin != null)
                {
                    MessageBox.Show("Добро пожаловать!", "Авторизация");
                    MainHotelPage mainWindow = new MainHotelPage();
                    mainWindow.Show();
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Неверный логин или пароль!", "Ошибка");
                }
            }
        }
    }
}