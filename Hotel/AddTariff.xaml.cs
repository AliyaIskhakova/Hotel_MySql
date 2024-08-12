using Hotel.Model;
using System.Windows;

namespace Hotel
{
    public partial class AddTariff : Window
    {
        public AddTariff()
        {
            InitializeComponent();
        }
        private void AddTariff_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(TarifCardName.Text.Trim()) && !string.IsNullOrEmpty(TarifCardSummary.Text.Trim()) && !string.IsNullOrEmpty(TarifCardCost.Text.Trim()) && Convert.ToInt32(TarifCardCost.Text) >= 0)
                {
                    HotelEntities context = new HotelEntities();
                    Tariff tariff = new Tariff();
                    tariff.Name = TarifCardName.Text.ToString();
                    tariff.Summary = TarifCardSummary.Text.ToString();
                    tariff.Cost = Convert.ToInt32(TarifCardCost.Text);
                    tariff.Food = (bool)TarifCardFood.IsChecked;
                    tariff.Gym = (bool)TarifCardGym.IsChecked;
                    tariff.Transfer = (bool)TarifCardTransfer.IsChecked;
                    tariff.Wifi = (bool)TarifCardWifi.IsChecked;
                    context.Tariff.Add(tariff);
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
        private void AddTariff_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            MainHotelPage mainWindow = new MainHotelPage();
            mainWindow.Rooms.Height = 0;
            mainWindow.Show();
            mainWindow.Tarif.Height = mainWindow.ActualHeight;
        }
    }
}
