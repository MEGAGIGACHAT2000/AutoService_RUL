using System;
using System.Collections.Generic;
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
using rul.Entities;

namespace rul.Pages
{
    /// <summary>
    /// Логика взаимодействия для Autho.xaml
    /// </summary>
    public partial class Autho : Page
    {
        private int countUnSuccessful = 0;
        public Autho()
        {
            InitializeComponent();
            txtCaptcha.Visibility = Visibility.Hidden;
            textBlockCapcha.Visibility = Visibility.Hidden;
        }

        private void btnEnterGuest_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Client(null));
        }
        private void btnEnter_Click(object sender, RoutedEventArgs e)
        {
            string login = txtLogin.Text.Trim();
            string password = txtPassword.Text.Trim();

            User user = new User();

            user = RulEntities.GetContext().User.Where(p => p.UserLogin == login && p.UserPassword == password).FirstOrDefault();
            int userCount = RulEntities.GetContext().User.Where(p => p.UserLogin == login && p.UserPassword == password).Count();

            if (countUnSuccessful < 1)
            {
                if(userCount > 0)
                {
                    MessageBox.Show("Вы вошли под: " + user.Role.RoleName.ToString());
                    LoadForm(user.Role.RoleName.ToString(), user);
                }
                else
                {
                    MessageBox.Show("Введите данные заново");
                }
            }
            else
            {
                if(userCount > 0 && textBlockCapcha.Text == txtCaptcha.Text)
                {
                    MessageBox.Show("Вы вошли под: " + user.Role.RoleName.ToString());
                    LoadForm(user.Role.RoleName.ToString(), user);
                }
                else
                {
                    MessageBox.Show("Введите данные заново");
                }
            }
        }

        private void GenerateCapcha()
        {
            txtCaptcha.Visibility = Visibility.Hidden;
            textBlockCapcha.Visibility = Visibility.Hidden;

            Random random = new Random();
            int randNum = random.Next(0, 3);

            switch (randNum)
            {
                case 1:
                    textBlockCapcha.Text = "gfytr634";
                    textBlockCapcha.TextDecorations = TextDecorations.Strikethrough;
                    break;
                case 2:
                    textBlockCapcha.Text = "fdajg431";
                    textBlockCapcha.TextDecorations = TextDecorations.Strikethrough;
                    break;
                case 3:
                    textBlockCapcha.Text = "faliu536";
                    textBlockCapcha.TextDecorations = TextDecorations.Strikethrough;
                    break;
            }
        }

        private void LoadForm(string _role, User user)
        {
            switch (_role)
            {
                case "Клиент":
                    NavigationService.Navigate(new Client(user));
                    break;
                case "Менеджер":
                    NavigationService.Navigate(new Client(user));
                    break;
                case "Администратор":
                    NavigationService.Navigate(new Admin(user));
                    break;
            }
        }
    }
}
