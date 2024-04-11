using System.Text;

namespace BackendBase.Helpers
{
    public class FileHelper
    {
        public async void CreateHelloWorldFileAsync()
        {
            string path = @"xlsx/txtttttt";   // путь к файлу

            string text = "Hello METANIT.COM"; // строка для записи

            // запись в файл
            using (FileStream fstream = new FileStream(path, FileMode.OpenOrCreate))
            {
                // преобразуем строку в байты
                byte[] buffer = Encoding.Default.GetBytes(text);
                // запись массива байтов в файл
                await fstream.WriteAsync(buffer, 0, buffer.Length);
                Console.WriteLine("Текст записан в файл");
            }
        }
    }
}
