import React, { useEffect, useState } from "react"; // React paketleri içindeki Hooks ile gelen yeni özellikleri kullanabilmemiz için projeye ekliyoruz.
import { Hotel } from "./model/Hotel"; // Kendi oluşturduğumuz modeli ekliyoruz.
import { parse } from "papaparse"; // Papaparse kütüğhanesi bizim dışarıdan aldığımız datayı istediğimiz formata çekmemize yarayacak olan basit ve kolay kullanışlı bir pakettir.
import HotelService from "./service/HotelService"; // Business olarak çalıştırmak istediğimiz işlemleri service düzeyinde farklı bir katmanda toplayıp burada referans ile kullanıyoruz.

function App() {
  const [dataSet, setDataSet] = useState<Hotel[]>([]); // Bizim kullanacağımız dataları ve o dataları editleyeceğimiz fonksiyonu oluşturuyoruz.
  const [isSelected, setIsSelected] = React.useState(false); // Drop box ımız için kullanacağımız bir bool değişken

  // UseEffect içerideki alınan aksiyonlara göre hareket etmemizi sağlayacak. Burada kullanım amacı `dataSet` değişkenimiz değiştiğinde bizim yapmamızı istediğimiz olayları gerçekleştirmek.
  useEffect(() => {
    SendItemsToApi(); // Service üzerinden çağıracağımız api call fonksiyonu.
  }, [dataSet]);

  // Async olarak oluşturduğumuz ve service içindeki fonksiyonu çağıran bir arrow fonksiyon.
  const SendItemsToApi = async () => {
    if (dataSet.length > 1) {
      // Eğer ki datamız alınamamışsa bu işlemi yapmaya gerek yok.
      HotelService.AddHotelData(dataSet); // Data ları service katmanını kullanarak istediğimiz yere gönderiyoruz.
    }
  };

  return (
    <div className="App">
      <h1 className="text-center text-4xl">Take Data From Local</h1>
      <div
        className={`p-6 my-2 mx-auto max-w-md border-2 ${
          isSelected ? "border-green-600 bg-green-100" : "border-gray-600"
        }`}
        onDragEnter={() => {
          setIsSelected(true);
        }}
        onDragLeave={() => {
          setIsSelected(false);
        }}
        onDragOver={(e) => {
          e.preventDefault();
        }}
        onDrop={(e) => {
          e.preventDefault();
          setIsSelected(false);
          console.log(e.dataTransfer.files);

          Array.from(e.dataTransfer.files)
            .filter((file) => file.type === "application/vnd.ms-excel")
            .forEach(async (file) => {
              const text = await file.text();
              const result = parse<Hotel>(text, { header: true });
              setDataSet((x) => [...x, ...result.data]);
            });
        }}
      >
        Drop Here Your CSV File
      </div>
      {dataSet.length > 0 ? (
        <table>
          <thead>
            <tr>
              <th>Hotel Name</th>
              <th>Contact</th>
              <th>Phone</th>
              <th>Address</th>
              <th>Uri</th>
            </tr>
          </thead>

          <tbody>
            {dataSet.map((data, index) => (
              <>
                <tr key={index}>
                  <td>{data.name}</td>
                  <td>{data.contact}</td>
                  <td>{data.phone}</td>
                  <td>{data.address}</td>
                  <td>{data.uri}</td>
                </tr>
              </>
            ))}
          </tbody>
        </table>
      ) : (
        <></>
      )}
    </div>
  );
}

export default App;
