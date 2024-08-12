using Hotel.Model;
using System.Windows;

namespace Hotel
{
    public partial class EditTariff : Window
    {
        private readonly HotelEntities _context;
        private readonly Tariff _tariff;
        public EditTariff(HotelEntities context, Tariff tariff)
        {
            InitializeComponent();
            _context = context;
            _tariff = tariff;
            TarifCardCost.Text = tariff.Cost.ToString();
            TarifCardName.Text = tariff.Name;
            TarifCardSummary.Text = tariff.Summary;
            if (tariff.Food) TarifCardFood.IsChecked = true;
            if (tariff.Gym) TarifCardGym.IsChecked = true;
            if (tariff.Transfer) TarifCardTransfer.IsChecked = true;
            if (tariff.Wifi) TarifCardWifi.IsChecked = true;
        }
        private void SaveEditTariff_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                
                if (!string.IsNullOrEmpty(TarifCardCost.Text.Trim()) && !string.IsNullOrEmpty(TarifCardName.Text.Trim()) && !string.IsNullOrEmpty(TarifCardSummary.Text.Trim()) && Convert.ToInt32(TarifCardCost.Text) >= 0)
                {
                    _tariff.Name = TarifCardName.Text;
                    _tariff.Summary = TarifCardSummary.Text;
                    _tariff.Cost = Convert.ToInt32(TarifCardCost.Text); ;
                    _tariff.Food = (bool)TarifCardFood.IsChecked;
                    _tariff.Gym = (bool)TarifCardGym.IsChecked;
                    _tariff.Transfer = (bool)TarifCardTransfer.IsChecked;
                    _tariff.Wifi = (bool)TarifCardWifi.IsChecked;
                    _context.Tariff.Update(_tariff);
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
        private void EditTariff_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            MainHotelPage mainWindow = new MainHotelPage();
            mainWindow.Rooms.Height = 0;
            mainWindow.Show();
            mainWindow.Tarif.Height = mainWindow.ActualHeight;
        }
    }
}
