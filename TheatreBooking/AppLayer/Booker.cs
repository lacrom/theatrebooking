using System.Collections.Generic;
using System.ComponentModel;

namespace TheatreBooking.AppLayer
{
    public class Booker
    {
        public int ID { get; set; }
        [DisplayName("Фамилия")]
        public string LastName { get; set; }
        [DisplayName("Имя")]
        public string FirstName { get; set; }
        [DisplayName("Эл. почта")]
        public string Email { get; set; }
        [DisplayName("Тел.")]
        public string PhoneNumber { get; set; }
        [DisplayName("Приглашающее лицо")]
        public string Face { get; set; }
        [DisplayName("Фуршет")]
        public bool? Party { get; set; }
    }

    public class BookerAndSeats
    {
        public int ID { get; set; }
        [DisplayName("Фамилия")]
        public string LastName { get; set; }
        [DisplayName("Имя")]
        public string FirstName { get; set; }
        [DisplayName("Эл. почта")]
        public string Email { get; set; }
        [DisplayName("Тел.")]
        public string PhoneNumber { get; set; }
        [DisplayName("Места")]
        public List<string> Seats { get; set; }
        [DisplayName("Приглашающее лицо")]
        public string Face { get; set; }
        [DisplayName("Фуршет")]
        public bool? Party { get; set; }
    }
}