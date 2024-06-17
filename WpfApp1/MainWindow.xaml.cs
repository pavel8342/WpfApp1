using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
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
using Newtonsoft.Json;
using static WpfApp1.d_sewe;


namespace WpfApp1
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            LoadCities();
            using (Context d = new Context()) ;
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (ком.SelectedItem != null)
            {

                string selectedData = ком.SelectedItem.ToString();
                string urr1 = $"https://api.openweathermap.org/data/2.5/weather?q={selectedData}&units=metric&appid=38811b7cc8282eb49861021948cfc20e";
                HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(urr1);
                HttpWebResponse httpWebResponse = (HttpWebResponse)httpWebRequest.GetResponse();
                string response;
                using (StreamReader streamReader = new StreamReader(httpWebResponse.GetResponseStream()))
                {
                    response = streamReader.ReadToEnd();
                }
                WeatherResponse weatherResponse = JsonConvert.DeserializeObject<WeatherResponse>(response);
                string s = "  " + weatherResponse.Name + " " + Convert.ToString(weatherResponse.Main.temp + " С° \n" );
                tex.Text = s;
                история история = new история();
                string name = ком.Text;
                string погода = Convert.ToString(weatherResponse.Main.temp);
                DateTime currentDate = DateTime.Today;
                string email = Convert.ToString(currentDate);
                DateTime currentTime = DateTime.Now;
                TimeSpan time = currentTime.TimeOfDay;
                string Sex = Convert.ToString(time);
                история.город = name;
                история.погода = погода;
                история.дата = email;
                история.время = Sex;
                db.историяs.Add(история);
                db.SaveChanges();

            }
            else
            {
                MessageBox.Show("Пожалуйста, выберите город");
            }
        }
        private void LoadCities()
        {
            using (var context = new Context())
            {
                var cityList = context.городаs.Select(c => c.город).ToList();
                ком.ItemsSource = cityList;
            }
        }
        public void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
        }
        public void Button_Click_1(object sender, RoutedEventArgs e)
        {
            string г = гор.Text;
            города города = new города();
            города.город = г;
            db.городаs.Add(города);
            db.SaveChanges();
        }
        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            var selectedCity = ком.SelectedItem as string;
            if (!string.IsNullOrEmpty(selectedCity))
            {
                using (var context = new Context())
                {
                    var city = context.городаs.FirstOrDefault(c => c.город == selectedCity);
                    if (city != null)
                    {
                        context.городаs.Remove(city);
                        context.SaveChanges();
                        LoadCities();
                    }
                }
            }
            else
            {
                MessageBox.Show("Пожалуйста, выберите город для удаления.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            using (var db = new Context())
            {
                var data = db.историяs.ToList(); // Получаем данные из таблицы история

                foreach (var item in data)
                {
                    ист.Text += $"  {item.дата}, {item.время}, {item.город}, {item.погода} \n";
                }
            }
        }
    }
}
