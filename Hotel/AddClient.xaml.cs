using Hotel.Model;
using System;
using System.Windows;

namespace Hotel
{

    public partial class AddClient : Window
    {
        public AddClient()
        {
            InitializeComponent();
        }
        private void AddNewClient_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(Surname.Text.Trim()) && !string.IsNullOrEmpty(ClientName.Text.Trim()) && !string.IsNullOrEmpty(Phone.Text.Trim()) && !string.IsNullOrEmpty(Email.Text.Trim())
                    && Birthday.SelectedDate.HasValue && Birthday.SelectedDate >= Convert.ToDateTime("01.01.1923") && Birthday.SelectedDate <= Convert.ToDateTime("07.12.2009") && Convert.ToInt32(SeriesPassport.Text) >= 1000 && Convert.ToInt32(NumberPassport.Text) >= 100000 && Convert.ToInt64(Phone.Text) >= 10000000000)
                {
                    Client client = new Client();
                    HotelEntities context = new HotelEntities();
                    client.Surname = Surname.Text;
                    client.Name = ClientName.Text;
                    client.Patronymic = Patronymic.Text;
                    DateTime birthday = Birthday.SelectedDate ?? DateTime.MinValue;
                    client.Birthday = birthday;
                    if (GenderWomen.IsChecked == true) client.Gender = true;
                    if (GenderMen.IsChecked == true) client.Gender = false;
                    client.PassportSeries = Convert.ToInt32(SeriesPassport.Text);
                    client.PassportNumber = Convert.ToInt32(NumberPassport.Text);
                    client.PhoneNumber = Phone.Text;
                    client.Email = Email.Text;
                    client.Login = "";
                    client.Password = "";
                    context.Client.Add(client);
                    context.SaveChanges();
                    this.Close();
                }
                else MessageBox.Show("Заполните все поля! Проверьте корректность данных!");
            }
            catch
            {
                MessageBox.Show("Проверьте корректность данных!");
            }
        }
        private void AddClient_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            MainHotelPage mainWindow = new MainHotelPage();
            mainWindow.Rooms.Height = 0;
            mainWindow.Show();
            mainWindow.Clients.Height = mainWindow.ActualHeight;
        }
    }
}
