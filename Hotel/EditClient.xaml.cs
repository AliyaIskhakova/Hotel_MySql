using Hotel.Model;
using System.Windows;

namespace Hotel
{
    public partial class EditClient : Window
    {
        private readonly HotelEntities _context;
        private readonly Client _client;
        public EditClient(HotelEntities context, Client client)
        {
            InitializeComponent();
            _context = context;
            _client = client;
            Surname.Text = client.Surname;
            ClientName.Text = client.Name;
            Patronymic.Text = client.Patronymic;
            Phone.Text = client.PhoneNumber;
            Email.Text = client.Email;
            Birthday.Text = client.Birthday.ToString("dd-MM-yyyy");
            if (client.Gender) GenderWomen.IsChecked = true;
            else GenderMen.IsChecked = true;
            SeriesPassport.Text = client.PassportSeries.ToString();
            NumberPassport.Text = client.PassportNumber.ToString();
        }
        private void SaveClient_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(Surname.Text.Trim()) && !string.IsNullOrEmpty(ClientName.Text.Trim()) && !string.IsNullOrEmpty(Phone.Text.Trim()) && !string.IsNullOrEmpty(Email.Text.Trim())
                    && Birthday.SelectedDate.HasValue && Convert.ToInt32(SeriesPassport.Text) >= 1000 && Convert.ToInt32(NumberPassport.Text) >= 100000 && Convert.ToInt64(Phone.Text) >= 10000000000)
                {
                    _client.Surname = Surname.Text;
                    _client.Name = ClientName.Text;
                    _client.Patronymic = Patronymic.Text;
                    DateTime birthday = Birthday.SelectedDate ?? DateTime.MinValue;
                    _client.Birthday = birthday;
                    if (GenderWomen.IsChecked == true) _client.Gender = true;
                    if (GenderMen.IsChecked == true) _client.Gender = false;
                    _client.PassportSeries = Convert.ToInt32(SeriesPassport.Text);
                    _client.PassportNumber = Convert.ToInt32(NumberPassport.Text);
                    _client.PhoneNumber = Phone.Text;
                    _client.Email = Email.Text;
                    _context.Client.Update(_client);
                    _context.SaveChanges();
                    this.Close();
                }
                else MessageBox.Show("Заполните все поля! Проверьте корректность данных!");
            }
            catch
            {
                MessageBox.Show("Проверьте корректность данных!");
            }
        }
        private void EditClient_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            MainHotelPage mainWindow = new MainHotelPage();
            mainWindow.Rooms.Height = 0;
            mainWindow.Show();
            mainWindow.Clients.Height = mainWindow.ActualHeight;
        }
    }
}
