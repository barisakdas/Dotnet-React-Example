import { Hotel } from "../model/Hotel"; // Kendi oluşturduğumuz modeli ekliyoruz.

class HotelService {
  static AddHotelData(hotels: Hotel[]) {
    var url = `'https://localhost:7295/api/Hotel`; // api projemize ait post endpointimizi buraya yazıyoruz.

    console.log(JSON.stringify(hotels)); // api projesine dataları göndermeden önce son json halini tarayıcımızın Console sekmesinde görebiliriz.

    hotels.forEach((hotel) => {
      // fetch: bu kütüphane typescript kodlarıyla bir api call işlemi sunuyor.
      fetch(url, {
        method: "POST",
        body: JSON.stringify(hotel),
        headers: {
          "Content-Type": "application/json; charset=UTF-8",
        },
      });
    });
  }
}
export default HotelService;
