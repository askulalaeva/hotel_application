using System;
using System.Collections.Generic;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Hotel_Application
{
    /// <summary>
    /// Логика взаимодействия для CategoryHotelPage.xaml
    /// </summary>
    public partial class RoomHotelPage : Page
    {
        // Класс с методами
        HotelMethods _methods;

        public RoomHotelPage()
        {
            _methods = new HotelMethods();
            InitializeComponent();

            // все элементы листбокса свяжем с листом комнат из класса с методами
            roomHotelList.ItemsSource = _methods.Rooms;
        }

        private void roomHotelList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // если элемент выбран, то активируем кнопки удаления и редактирования
            btnRoomRemove.IsEnabled = roomHotelList.SelectedIndex != -1;
            btnRoomEdit.IsEnabled = roomHotelList.SelectedIndex != -1;
        }

        private void btnRoomAdd_Click(object sender, RoutedEventArgs e)
        {
            // при добавлении откроем новое окно
            AddNewRoomWindow addNewRoomWindow = new AddNewRoomWindow(_methods);
            if (addNewRoomWindow.ShowDialog().Value)
            {
                // для точности обнулим поиск
                txtRoomSearch.Text = "";
                // обновим листбокс
                RefreshListBox();
            }
        }

        private void btnRoomEdit_Click(object sender, RoutedEventArgs e)
        {
            // если что-то выбрано, то откроем новое окно и передадим туда выбранный элемент
            if (roomHotelList.SelectedIndex != -1)
            {
                EditRoomWindow editRoomWindow = new EditRoomWindow(_methods, roomHotelList.SelectedItem as Room);
                if (editRoomWindow.ShowDialog().Value)
                {
                    // обнулим поисковую строку
                    txtRoomSearch.Text = "";
                    // обновим листбокс
                    RefreshListBox();
                }
            }
        }

        private void btnRoomRemove_Click(object sender, RoutedEventArgs e)
        {
            // если что-то выбрано, удалим выбранный элемент и обновим листбокс
            if (roomHotelList.SelectedIndex != -1)
            {
                _methods.DeleteRoom(roomHotelList.SelectedItem as Room);
                RefreshListBox();
            }
        }

        private void roomHotelList_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            // при двойном щелчке - если что-то выбрано, передем на новую страницу с информацией о команте
            if (roomHotelList.SelectedIndex != -1)
            {
                // передадим туда выбранный элемент
                NavigationService.Navigate(new RoomInfoPage(roomHotelList.SelectedItem as Room));
            }
        }

        private void txtRoomSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            // при изменении текста поиска, будем менять контент листбокса
            string search = txtRoomSearch.Text;
            roomHotelList.ItemsSource = _methods.Search(search);
        }

        // обновление листбокса
        private void RefreshListBox()
        {
            roomHotelList.ItemsSource = null;
            roomHotelList.ItemsSource = _methods.Rooms;
        }
    }
}
