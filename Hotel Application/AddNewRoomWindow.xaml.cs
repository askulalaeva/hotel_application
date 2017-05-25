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
using System.Windows.Shapes;

namespace Hotel_Application
{
    /// <summary>
    /// Логика взаимодействия для AddNewRoomWindow.xaml
    /// </summary>
    public partial class AddNewRoomWindow : Window
    {
        // класс с методами
        HotelMethods _methods;
        public AddNewRoomWindow(HotelMethods methods)
        {
            InitializeComponent();
            _methods = methods;
        }

        private void btnRoomAdd_Click(object sender, RoutedEventArgs e)
        {
            // защита от некорректного ввода
            if (string.IsNullOrEmpty(txtRoomName.Text))
            {
                MessageBox.Show("Необходимо ввести название категории");
                txtRoomName.Focus();
                return;
            }
            double price;
            if (!double.TryParse(txtRoomPrice.Text, out price))
            {
                MessageBox.Show("Цена должна быть вещественным числом");
                txtRoomPrice.Focus();
                return;
            }

            // создаем комнату и добавляем через класс с методами
            Room tmp = new Room(txtRoomName.Text, price);
            _methods.AddNewRoom(tmp);

            // закрываем окно
            DialogResult = true;
        }
    }
}