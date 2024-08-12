using Hotel.Model;
using System.Windows;

namespace Hotel
{

    public partial class AddClient : Window
    {
        public AddClient()
        {
            InitializeComponent();
            Birthday.DisplayDateStart = DateTime.Now.AddYears(-100);
            Birthday.DisplayDateEnd = DateTime.Now.AddYears(-14);
        }
        private void AddNewClient_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Validate validate = new Validate();
                
                if (!string.IsNullOrEmpty(Surname.Text.Trim()) && !string.IsNullOrEmpty(ClientName.Text.Trim()) && !string.IsNullOrEmpty(Phone.Text.Trim()) && !string.IsNullOrEmpty(Email.Text.Trim())
                    && Birthday.SelectedDate.HasValue )
                {
                    if (validate.ValidateAll(Surname.Text, ClientName.Text, Patronymic.Text, Phone.Text, Email.Text, SeriesPassport.Text, NumberPassport.Text, (DateTime)Birthday.SelectedDate)){

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
                    else MessageBox.Show(validate.message);
                }
                else MessageBox.Show("Заполните все поля!");
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
