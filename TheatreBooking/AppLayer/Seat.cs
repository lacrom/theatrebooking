using System;
using System.ComponentModel;

namespace TheatreBooking.AppLayer
{
    public class Seat
    {
        public int ID { get; set; }
        [DisplayName("Тип")]
        public string RowName { get; set; }
        [DisplayName("Номер")]
        public string RowNumber { get; set; }
        [DisplayName("Место")]
        public string SeatNumber { get; set; }
        [DisplayName("Область")]
        public string AreaDescription { get; set; }
        [DisplayName("Цена")]
        public string Price { get; set; }
        [DisplayName("Информация")]
        public string Information { get; set; }
        [DisplayName("Статус")]
        public SeatStatus Status { get; set; }
        [DisplayName("Забронировано в")]
        public DateTime? BookedAt { get; set; }
        public DateTime? SelectedAt { get; set; }
        public virtual Booker BookedBy { get; set; }

        [DisplayName("Тип")]
        public string RowNameEn { get; set; }

        [DisplayName("Область")]
        public string AreaDescriptionEn { get; set; }

        [DisplayName("Информация")]
        public string InformationEn { get; set; }
    }
}