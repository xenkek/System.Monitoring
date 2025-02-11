﻿using System.Windows;
using System.Windows.Controls;
using System.Linq;
using SystemMonitoring.Controls;
using SystemMonitoring.Models;

namespace SystemMonitoring.AdminEditPages
{
    public partial class AddSensor : Page
    {
        bool edit = false;
        public AddSensor() { InitializeComponent(); }
        public AddSensor(Sensor sensor)
        {
            InitializeComponent();
            DataContext = sensor;
            edit = true;
        }
        void Back_Click(object sender, RoutedEventArgs e) { ManagerPage.Page.Navigate(ManagerPage.FieldMonitoringPage); }
        void Add_Click(object sender, RoutedEventArgs e)
        {
            string error = "Ошибка:\n";
            foreach (var tb in Grid.Children.OfType<TextBox>())
            {
                if(tb.Name == "Humidity")
                {
                    if (int.Parse(tb.Text) < 20 || int.Parse(tb.Text) > 90)
                        error += "Данные введены некорректно: Влажность принимает значения от 20 до 90\n";
                } else if(tb.Name == "Temperature")
                {
                    if(int.Parse(tb.Text) < -30 || int.Parse(tb.Text) > 50)
                        error += "Данные введены некорректно: Температура принимает значения от -30 до 50\n";
                } else if(tb.Name == "Acidity")
                {
                    if (int.Parse(tb.Text) < 0 || int.Parse(tb.Text) > 8)
                        error += "Данные введены некорректно: Кислотность принимает значения от 0 до 8\n";
                }
                else if (tb.Name == "Phosphorus")
                {
                    if (int.Parse(tb.Text) < 0 || int.Parse(tb.Text) > 200)
                        error += "Данные введены некорректно: Фосфор принимает значения от 0 до 200\n";
                }
                else if (tb.Name == "Calcium")
                {
                    if (int.Parse(tb.Text) < 400 || int.Parse(tb.Text) > 1400)
                        error += "Данные введены некорректно: Кальций принимает значения от 400 до 1400\n";
                }
                else if (tb.Name == "Magnesium")
                {
                    if (int.Parse(tb.Text) < 0 || int.Parse(tb.Text) > 15)
                        error += "Данные введены некорректно: Магний принимает значения от 0 до 15\n";
                }
                else if (tb.Name == "Calium")
                {
                    if (int.Parse(tb.Text) < 20 || int.Parse(tb.Text) > 200)
                        error += "Данные введены некорректно: Калий принимает значения от 20 до 200\n";
                }
                else if (tb.Name == "Asot")
                {
                    if (int.Parse(tb.Text) < 15 || int.Parse(tb.Text) > 170)
                        error += "Данные введены некорректно: Азот принимает значения от 15 до 170\n";
                }
            }
            if (error.Length > 20) { System.Windows.Forms.MessageBox.Show(error); return; }
            if (edit)
            {
                Db.Child.Where(x => x.ID == ID.Text).Single().Humidity = Humidity.Text;
                Db.Child.Where(x => x.ID == ID.Text).Single().Temperature = Temperature.Text;
                Db.Child.Where(x => x.ID == ID.Text).Single().Acidity = Acidity.Text;
                Db.Child.Where(x => x.ID == ID.Text).Single().Asot = Asot.Text;
                Db.Child.Where(x => x.ID == ID.Text).Single().Calcium = Calcium.Text;
                Db.Child.Where(x => x.ID == ID.Text).Single().Magnesium = Magniy.Text;
                Db.Child.Where(x => x.ID == ID.Text).Single().Calium = Calium.Text;
                Db.Child.Where(x => x.ID == ID.Text).Single().Phosphorus = Phosphorus.Text;
            }
            else
            {
                Db.Child.Add(new SensorDetails
                {
                    ID = ID.Text,
                    Humidity = Humidity.Text,
                    Temperature = Temperature.Text,
                    Acidity = Acidity.Text,
                    Asot = Asot.Text,
                    Calcium = Calcium.Text,
                    Calium = Calium.Text,
                    Magnesium = Magniy.Text,
                    Phosphorus = Phosphorus.Text
                });
            }
            ManagerPage.Page.Navigate(ManagerPage.FieldMonitoringPage);
        }
        void TB_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextBox tb = sender as TextBox;
            if (!int.TryParse(tb.Text, out _))
            {
                tb.Text = "";
                System.Windows.Forms.MessageBox.Show(@"Вводите только цифры!");
            }
        }
    }
}