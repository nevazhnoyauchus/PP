using System;
using System.Collections.Generic;
using System.IO;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace AlenaTest.Pages
{
    /// <summary>
    /// Логика взаимодействия для TestPage.xaml
    /// </summary>
    public partial class TestPage : Page
    {
        public int PointsCoder = 0;
        public int PointsWeb = 0;
        public int PointsDB = 0;

        public string Path = "\\q.csv";

        public class Question
        {
            public string Title { get; set; }
            public string A1 { get; set; }
            public string A2 { get; set; }
            public string A3 { get; set; }
            public string A1r { get; set; }
            public string A2r { get; set; }
            public string A3r { get; set; }
        }

        List<Question> questions = new List<Question>();

        List<(string, string)> answers = new List<(string, string)>();
        
        public TestPage()
        {
            InitializeComponent();
            var lines = File.ReadAllLines(Environment.CurrentDirectory + Path);

            int j = 1;
            var q = new Question();

            for (int i = 0; i < lines.Length; i++)
            {
                var data = lines[i].Split(";");
                q.Title = data[0];
                

                if (j == 1)
                {
                    q.A1 = data[1];
                    q.A1r = data[2];
                }
                if (j == 2)
                {
                    q.A2 = data[1];
                    q.A2r = data[2];
                }
                if (j == 3)
                {
                    q.A3 = data[1];
                    q.A3r = data[2];


                    j = 1;
                    questions.Add(q);
                    answers.Add((q.Title, ""));
                    q = new Question();
                    continue;
                }

                j++;

            }
            list.ItemsSource = questions;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            PointsCoder = 0;
            PointsDB = 0;
            PointsWeb = 0;

            for (int i = 0; i < answers.Count; i++)
            {
                if (answers[i].Item2 == "")
                {
                    MessageBox.Show("Не все ответы выбраны!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
                switch (answers[i].Item2)
                {
                    case "Прогер":
                        {
                            PointsCoder++;

                        } break;
                    case "БД":
                        {
                            PointsDB++;
                        }
                        break;
                    case "Веб":
                        {
                            PointsWeb++;
                        }
                        break;
                    default:
                        break;
                }
            }

            if (PointsCoder > PointsDB)
            {
                if (PointsCoder > PointsWeb)
                {
                    MessageBox.Show("Ваш результат - программист!", "Результат");
                }
                else
                {
                    if (PointsWeb > PointsDB)
                    {
                        if (PointsCoder == PointsWeb)
                        {
                            MessageBox.Show("Ваш результат - программист и веб-разработчик!", "Результат");
                        }
                        else
                        {
                            MessageBox.Show("Ваш результат - веб-разработчик!", "Результат");
                        }
                    }
                }
            }
            else
            {
                if (PointsWeb > PointsCoder)
                {
                    if (PointsDB > PointsWeb)
                    {
                        MessageBox.Show("Ваш результат - администратор баз данных", "Результат");
                    }
                    else
                    {
                        if (PointsDB == PointsWeb)
                        {
                            MessageBox.Show("Ваш результат - администратор баз данных и веб-разработчик!", "Результат");
                        }
                        else
                        {
                            MessageBox.Show("Ваш результат - веб-разработчик!", "Результат");
                        }
                    }
                }
                else
                {
                    if (PointsDB > PointsWeb)
                    {
                        if (PointsCoder == PointsDB)
                        {
                            MessageBox.Show("Ваш результат - администратор баз данных и программист", "Результат");
                        }
                        else
                        {
                            MessageBox.Show("Ваш результат - администратор баз данных", "Результат");
                        }
                    }
                }
            }
            if (PointsWeb == PointsCoder && PointsWeb == PointsDB && PointsDB == PointsCoder)
            {
                MessageBox.Show("Поздравляю! Вам подходят все виды профессий!", "Результат");
            }
            
        }

        private void RadioButton_Checked(object sender, RoutedEventArgs e)
        {
            var radio = (RadioButton)sender;
            var q = (Question)(radio).DataContext;
            string role = "";
            if (q.A1 == radio.Content.ToString()) role = q.A1r;
            if (q.A2 == radio.Content.ToString()) role = q.A2r;
            if (q.A3 == radio.Content.ToString()) role = q.A3r;

            var index = answers.FindIndex(a => a.Item1 == q.Title);
            answers[index] = (q.Title, role);



        }
    }
}
