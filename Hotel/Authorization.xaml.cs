using Hotel.Model;
using System.Windows;

namespace Hotel
{
    public partial class Authorization : Window
    {
        public Authorization()
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