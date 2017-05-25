using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Xml.Serialization;

namespace Hotel_Application
{
    public class HotelMethods
    {
        // все комнаты
        private List<Room> _rooms = new List<Room>();

        public List<Room> Rooms
        {
            get { return _rooms; }
        }

        public HotelMethods()
        {
            // при создании сразу десериализуем
            Deserialization();
        }

        // добавление новой комнаты + сериализация
        public void AddNewRoom(Room room)
        {
            _rooms.Add(room);
            Serialization();
        }

        // редактирование комнаты + сериализация
        public void EditRoom(Room room, string name, double price)
        {
            room.Name = name;
            room.Price = price;
            Serialization();
        }

        // удаление комнаты + сериализация
        public void DeleteRoom(Room room)
        {
            _rooms.Remove(room);
            Serialization();
        }

        // поиск через имя комнаты
        public List<Room> Search(string search)
        {
            // если строка пустая, то вернем все комнаты
            if (search == "")
                return _rooms;

            // если нет, то просмотрим все и добавим те, у которых в имени содержится поисковая строка
            List<Room> searchResult = new List<Room>();
            foreach (Room room in _rooms)
            {
                // также приведем все в нижний регистр
                if (room.Name.ToLower().Contains(search.ToLower()))
                {
                    searchResult.Add(room);
                }
            }
            // вернем результат
            return searchResult;
        }

        private void Serialization()
        {
            // для того, чтоб не было ошибок, удалим файл
            File.Delete("../../Serialization.xml");
            using(var fs= new FileStream("../../Serialization.xml", FileMode.OpenOrCreate))
            {
                // сериализуем
                XmlSerializer serializer = new XmlSerializer(typeof(List<Room>));
                serializer.Serialize(fs, _rooms);
            }
        }

        private void Deserialization()
        {
            // если существует - десериализуем
            if (File.Exists("../../Serialization.xml"))
            {
                using (var fs = new FileStream("../../Serialization.xml", FileMode.Open))
                {
                    XmlSerializer serializer = new XmlSerializer(typeof(List<Room>));
                    // присвоим нашему полю данные из файла
                    _rooms = (List<Room>)serializer.Deserialize(fs);
                }
            }
        }
    }
}
