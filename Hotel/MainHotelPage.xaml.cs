using Hotel.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Runtime.InteropServices.ComTypes;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace Hotel
{
    public partial class MainHotelPage : Window
    {
        private readonly HotelEntities context = new HotelEntities();
        public int idSelectRoom;
        public MainHotelPage()
        {
            InitializeComponent();
            //List<Client> clientsList = new List<Client>();
            //clientsList = context.Client.ToList();
            //ClientList.Items.Clear();
            ClientList.ItemsSource = context.Client.ToList();
            foreach (Client clients in context.Client.ToList())
            {
                ClientCard clientCard = new ClientCard();
                clientCard.CCardFIO.Content = $"{clients.Surname} {clients.Name} {clients.Patronymic}";
                clientCard.CCardPhone.Content += $"{clients.PhoneNumber}";
                clientCard.CCardEmail.Content += $"{clients.Email}";
                clientCard.CCardBirthday.Content += clients.Birthday.ToString("dd-MM-yyyy");
                if (clients.Gender) clientCard.CCardGender.Content += " женский";
                else clientCard.CCardGender.Content += " мужской";
                clientCard.CCardSeries.Content += $"{clients.PassportSeries}";
                clientCard.CCardNumber.Content += $"{clients.PassportNumber}";
                clientCard.EditClient.Click += (sender, args) => EditClientButton_Click(context, clients);
                UniformForClientCard.Children.Add(clientCard);
            }
            foreach (Tariff tariff in context.Tariff.ToList())
            {
                TarifCard tarifCard = new TarifCard();
                tarifCard.TarifCardName.Text = $"{tariff.Name}";
                tarifCard.TarifCardCost.Content = $"+ {tariff.Cost} рублей";
                tarifCard.TarifCardSummary.Text = tariff.Summary;
                if (tariff.Food) tarifCard.TarifCardList1.Content = "+ Питание";
                if (tariff.Gym) tarifCard.TarifCardList2.Content = "+ Спортивный зал";
                if (tariff.Transfer) tarifCard.TarifCardList3.Content = "+ Трансфер";
                if (tariff.Wifi) tarifCard.TarifCardList4.Content = "+ Wi-Fi";
                tarifCard.EditTariff.Click += (sender, args) => EditButton_Click(context, tariff);
                tarifCard.DeleteTariff.Click += (sender, args) => DeleteButton_Click(context, tariff, tarifCard);
                tarifCard.Uid = $"{tariff.TariffID}";
                UniformForTarifCard.Children.Add(tarifCard);
            }
            GenerationReservationCard();
        }
        private void GenerationReservationCard()
        {
            foreach (Reservation reservation in context.Reservation.ToList())
            {
                ReservationCard reservationCard = new ReservationCard();
                var clientId = reservation.ClientID;
                var client = context.Client.FirstOrDefault(a => a.ClientID == clientId);
                var reservationTariff = context.ReservationTariff.FirstOrDefault(t => t.ReservationID == reservation.ReservationID);
                var tarif = context.Tariff.FirstOrDefault(t => t.TariffID == reservationTariff.TariffID);
                if (tarif != null) reservationCard.RCardTarif.Content += $"{tarif.Name}";
                reservationCard.RCardFIO.Content = $"{client.Surname} {client.Name} {client.Patronymic}";
                reservationCard.RCardDateCheckIn.Content += reservation.CheckiInDate.ToString("dd-MM-yyyy");
                reservationCard.RCardDateCheckOut.Content += reservation.CheckOutDate.ToString("dd-MM-yyyy");
                reservationCard.RCardRoom.Content += $"{reservation.RoomID}";
                reservationCard.RCardCost.Content += $"{reservation.FullCost} рублей";
                reservationCard.CancelReservation.Click += CancelReservation_Click;
                reservationCard.CancelReservation.Name += $"{reservation.ReservationID}";
                UniformForReservationCard.Children.Add(reservationCard);
            }
        }
        private void EditClientButton_Click(HotelEntities context, Client client)
        {
            EditClient editClient = new EditClient(context, client);
            editClient.Show();
            this.Close();
        }
        private void EditButton_Click(HotelEntities context, Tariff tariff)
        {
            EditTariff editTariff = new EditTariff(context, tariff);
            editTariff.Show();
            this.Close();
        }
        private void DeleteButton_Click(HotelEntities context, Tariff tariff, TarifCard tarifCard)
        {
            if (MessageBox.Show($"Вы уверены, что хотите удалить тариф {tariff.Name} ?", "Предупреждение", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                context.Tariff.Remove(tariff);
                context.SaveChanges();
                UniformForTarifCard.Children.Remove(tarifCard);
            }
        }
        private void CancelReservation_Click(object sender, RoutedEventArgs e)
        {
            Button but = (Button)sender;
            Regex regex = new Regex(@"(\d+)$");
            Match match = regex.Match(but.Name);
            string numberString = match.Groups[1].Value;
            int id = int.Parse(numberString);
            Reservation reservations;
            reservations = context.Reservation.Find(id);
            if (MessageBox.Show($"Вы уверены, что хотите отменить выбранную бронь ?", "Предупреждение", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                context.Reservation.Remove(reservations);
                context.SaveChanges();
                UniformForReservationCard.Children.Clear();
                GenerationReservationCard();
            }
        }
        private void ReservationButton_Click(object sender, RoutedEventArgs e)
        {
            Button but = (Button)sender;
            DateTime startDate = CheckInDatepicker.SelectedDate ?? DateTime.MinValue;
            DateTime endDate = CheckOutDatepicker.SelectedDate ?? DateTime.MinValue;
            ChoiceReserv(but, startDate, endDate);
        }
        private void ChoiceReserv(Button but, DateTime startDate, DateTime endDate)
        {
            StackPanelForFinalReservCard.Children.Clear();
            Regex regex = new Regex(@"(\d+)$");
            Match match = regex.Match(but.Name);
            string numberString = match.Groups[1].Value;
            int id = int.Parse(numberString);
            var room = context.Room.Find(id);
            idSelectRoom = id;
            RoomName.Content = $"Номер {room.RoomID}";
            RoomImage.Source = new BitmapImage(new Uri($"/Photo/room{room.RoomID}.jpg", UriKind.Relative));
            Summary.Text = room.Summary;
            HumanCountinReservation.Content = $" до {room.PeopleQuantity} мест";
            RoomArea.Content = $"{room.Area} м^2";
            RoomCount.Content = $"{room.RoomQuantity} комнт.";
            if (startDate != DateTime.MinValue && endDate != DateTime.MinValue && startDate < endDate && startDate <= Convert.ToDateTime("31.12.2024") && endDate <= Convert.ToDateTime("31.12.2024") && int.TryParse(HumanCount.Text, out int humancount))
            {
                TimeSpan difference = endDate.Subtract(startDate);
                int numberOfDays = difference.Days;
                foreach (Tariff tariff in context.Tariff)
                {
                    FinalReservationCard tarifCard = new FinalReservationCard();
                    tarifCard.TariffName.Content = tariff.Name;
                    int fullcost = (room.Cost + tariff.Cost) * numberOfDays;
                    tarifCard.ReservationCost.Content = $" {fullcost} рублей";
                    if (tariff.Food) tarifCard.TarifCardList1.Content = "+ Питание";
                    if (tariff.Gym) tarifCard.TarifCardList2.Content = "+ Спортивный зал";
                    if (tariff.Transfer) tarifCard.TarifCardList3.Content = "+ Трансфер";
                    if (tariff.Wifi) tarifCard.TarifCardList4.Content = "+ Wi-Fi";
                    tarifCard.ReservationInfo.Content = $"{numberOfDays} ночей/ {humancount} гостей";
                    tarifCard.ChoiceTarifButton.Click += (sender, args) => ChoiceTarifButton_Click(tariff, startDate, endDate, fullcost);
                    StackPanelForFinalReservCard.Children.Add(tarifCard);
                }
                ScrollFinalReservCard.Height = ActualHeight / 3.2;
                Reservation2.Height = ActualHeight;
                Rooms.Height = 0;
            }
            else MessageBox.Show("Произошла ошибка в датах");
        }
        private void ChoiceTarifButton_Click(Tariff tariff, DateTime checkInDate, DateTime checkOutDate, int fullcost)
        {
            var room = context.Room.Find(idSelectRoom);
            Client selectclient = (Client)ClientList.SelectedItem;
            DateTime CheckIn = checkInDate;
            DateTime CheckOut = checkOutDate;
            if (selectclient != null)
            {
                Reservation reservation = new Reservation();
                reservation.FullCost = fullcost;
                reservation.ClientID = selectclient.ClientID;
                reservation.RoomID = room.RoomID;
                reservation.CheckiInDate = CheckIn;
                reservation.CheckOutDate = CheckOut;
                reservation.ReservationDate = DateTime.Now;
                context.Reservation.Add(reservation);
                context.SaveChanges();
                ReservationTariff reservationTariff = new ReservationTariff();
                reservationTariff.TariffID = tariff.TariffID;
                reservationTariff.ReservationID = reservation.ReservationID;
                context.ReservationTariff.Add(reservationTariff);
                context.SaveChanges();
                MessageBox.Show("Успешно забронировано!");
                Rooms.Height = ActualHeight;
                UniformForReservationCard.Children.Clear();
                GenerationReservationCard();
                CheckInDatepicker.SelectedDate = null;
                CheckOutDatepicker.SelectedDate = null;
            }
            else MessageBox.Show("Выберете клиента");
        }
        private void FindReservationButton_Click(object sender, RoutedEventArgs e)
        {
            Reservation reservation = new Reservation();
            List<Reservation> reservations = new List<Reservation>();
            ReservationTariff reservationTariff;
            Tariff tarif;
            DateTime zaezdDate = ZaezdDatepicker.SelectedDate ?? DateTime.MinValue;
            DateTime viezdDate = ViezdDatepicker.SelectedDate ?? DateTime.MinValue;
            if (zaezdDate != DateTime.MinValue && viezdDate != DateTime.MinValue && zaezdDate <= viezdDate && zaezdDate >= Convert.ToDateTime("01.01.2020") && viezdDate >= Convert.ToDateTime("01.01.2025"))
            {
                UniformForReservationCard.Children.Clear();
                reservations = context.Reservation.Where(r => r.CheckiInDate >= zaezdDate && r.CheckOutDate <= viezdDate).ToList();
                foreach (var searchReservation in reservations)
                {
                    ReservationCard reservationCard = new ReservationCard();
                    Client client = new Client();
                    client = context.Client.Find(searchReservation.ClientID);
                    reservationCard.RCardFIO.Content += $"{client.Surname} {client.Name} {client.Patronymic}";
                    reservationCard.RCardDateCheckIn.Content += searchReservation.CheckiInDate.ToString("dd-MM-yyyy");
                    reservationCard.RCardDateCheckOut.Content += searchReservation.CheckOutDate.ToString("dd-MM-yyyy");
                    reservationCard.RCardRoom.Content += $"{searchReservation.RoomID}";
                    reservationTariff = context.ReservationTariff.FirstOrDefault(t => t.ReservationID == searchReservation.ReservationID);
                    tarif = context.Tariff.FirstOrDefault(t => t.TariffID == reservationTariff.TariffID);
                    reservationCard.RCardTarif.Content += $"{tarif.Name}";
                    reservationCard.RCardCost.Content += $"{searchReservation.FullCost}";
                    reservationCard.CancelReservation.Click += CancelReservation_Click;
                    reservationCard.CancelReservation.Name += $"{searchReservation.ReservationID}";
                    UniformForReservationCard.Children.Add(reservationCard);
                }
            }
            else MessageBox.Show("Вы ввели некоректный данные для поиска");
        }
        private void SearchRoomButton_Click(object sender, RoutedEventArgs e)
        {
            List<Room> rooms = new List<Room>();
            DateTime checkInDate = CheckInDatepicker.SelectedDate ?? DateTime.MinValue;
            DateTime checkOutDate = CheckOutDatepicker.SelectedDate ?? DateTime.MinValue;
            if (checkInDate != DateTime.MinValue && checkOutDate != DateTime.MinValue && checkInDate < checkOutDate && int.TryParse(HumanCount.Text, out int humancount) && checkInDate >= Convert.ToDateTime("08.12.2023") && checkOutDate <= Convert.ToDateTime("01.01.2025"))
            {
                StackpanelForRoomCard.Children.Clear();
                humancount = int.Parse(HumanCount.Text);
                Reservation reservation = new Reservation();
                rooms = context.Room.Where(r => r.PeopleQuantity >= humancount && !context.Reservation.Any(b => b.RoomID == r.RoomID && checkInDate <= b.CheckOutDate && checkOutDate >= b.CheckiInDate)).ToList();
                foreach (var searchRoom in rooms)
                {
                    RoomCard roomCard = new RoomCard();
                    roomCard.RoomCardImage.Source = new BitmapImage(new Uri($"/Photo/room{searchRoom.RoomID}.jpg", UriKind.Relative));
                    roomCard.RoomCardName.Content = $"Номер {searchRoom.RoomID}";
                    roomCard.RoomCardCost.Content = $"от {searchRoom.Cost} рублей";
                    roomCard.RoomCardSummary.Text = searchRoom.Summary;
                    roomCard.ReservationButton.Click += new RoutedEventHandler(ReservationButton_Click);
                    roomCard.ReservationButton.Name += $"{searchRoom.RoomID}";
                    StackpanelForRoomCard.Children.Add(roomCard);
                }
            }
            else MessageBox.Show("Вы ввели некоректный данные для поиска");
        }
        private void AddNewClient_Click(object sender, RoutedEventArgs e)
        {
            AddClient addClient = new AddClient();
            addClient.Show();
            this.Close();
        }
        private void AddTariff_Click(object sender, RoutedEventArgs e)
        {
            AddTariff addTariff = new AddTariff();
            addTariff.Show();
            this.Close();
        }
        private void MenuItem_Rooms(object sender, RoutedEventArgs e)
        {
            CheckInDatepicker.SelectedDate = null;
            CheckOutDatepicker.SelectedDate = null;
            Clients.Height = 0;
            Rooms.Height = ActualHeight;
            Tarif.Height = 0;
            Reservation2.Height = 0;
            Reservations.Height = 0;
        }
        private void MenuItem_Tarif(object sender, RoutedEventArgs e)
        {
            Clients.Height = 0;
            Rooms.Height = 0;
            Tarif.Height = ActualHeight;
            Reservation2.Height = 0;
            Reservations.Height = 0;
        }
        private void MenuItem_Client(object sender, RoutedEventArgs e)
        {
            Clients.Height = ActualHeight;
            Rooms.Height = 0;
            Tarif.Height = 0;
            Reservation2.Height = 0;
            Reservations.Height = 0;
        }
        private void MenuItem_Reservation(object sender, RoutedEventArgs e)
        {
            Clients.Height = 0;
            Rooms.Height = 0;
            Tarif.Height = 0;
            Reservation2.Height = 0;
            Reservations.Height = ActualHeight;
        }
    }
}