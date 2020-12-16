using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Julia_Web_Shop_Core.Models
{
    public class Product   
    {
        [Key]
        public int ID { get; set; }// Номер 
        public string Name { get; set; } // Название 
        public string ShortDesc { get; set; } // Описание
        public string factoryName { get; set; } // Название производителя
        public string img { get; set; } // Путь к картинке
        public uint prise { get; set; } // Стоимость товара
        public bool available { get; set; }// есть ли довар на скаладе  на скаладе
        public bool isFavourite { get; set; } // нужно ли выводить товар на главную странцу


    }
}
