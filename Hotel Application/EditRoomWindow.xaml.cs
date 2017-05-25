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
    /// Логика взаимодействия для EditRoomWindow.xaml
    /// </summary>
    public partial class EditRoomWindow : Window
    {
        // класс с методами и комната, которую редактируют
        HotelMethods _methods;
        Room _editRoom;

        public EditRoomWindow(HotelMethods methods, Room editRoom)
        {
            InitializeComponent();
            _methods = methods;
            _editRoom = editRoom;
            // заполним поля
            txtRoomName.Text = _editRoom.Name;
            txtRoomPrice.Text = _editRoom.Price.ToString();
        }

        private void btnRoomEdit_Click(object sender, RoutedEventArgs e)
        {
            // защита от некорректного ввода данных
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

            // изменим комнату через класс с методами (отправим туда новые поля)
            _methods.EditRoom(_editRoom, txtRoomName.Text, price);

            // закроем окно
            DialogResult = true;
        }
    }
}
