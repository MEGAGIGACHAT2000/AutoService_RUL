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
using rul.Windows;

namespace rul.Pages
{
    /// <summary>
    /// Логика взаимодействия для Client.xaml
    /// </summary>
    public partial class Client : Page
    {
        User user = new User();
        public Client(User currentUser)
        {
            InitializeComponent();

            var product = RulEntities.GetContext().Product.ToList();
            LViewProduct.ItemsSource = product;
            DataContext = this;

            txtAllAmount.Text = product.Count().ToString();

            user = currentUser;

            UpdateData();
            User();
        }

        private void User()
        {
            if(user != null)
            {
                txtFullName.Text = user.UserSurname.ToString() + user.UserName.ToString() + " " + user.UserPatronymic.ToString();
            } else
            {
                txtFullName.Text = " Гость";
            }
        }

        public string[] SortingList { get; set; } =
        {
            "Без сортировки",
            "Стоимость по возрастанию",
            "Стоимость по убыванию"
        };

        public string[] FilterList { get; set; } =
        {
            "Все диапозоны",
            "0-9,99%",
            "10-14,99%",
            "15% и более"
        };

        private void UpdateData()
        {
            var result = RulEntities.GetContext().Product.ToList();

            if (cmbSorting.SelectedIndex == 1)
                result = result.OrderBy(p => p.ProductCost).ToList();
            if (cmbSorting.SelectedIndex == 2)
                result = result.OrderByDescending(p => p.ProductCost).ToList();

            if (cmbFilter.SelectedIndex == 1)
                result = result.Where(p => p.ProductDiscountAmount >= 0 && p.ProductDiscountAmount < 10).ToList();
            if (cmbFilter.SelectedIndex == 2)
                result = result.OrderByDescending(p => p.ProductDiscountAmount >= 10 && p.ProductDiscountAmount < 15).ToList();
            if (cmbFilter.SelectedIndex == 3)
                result = result.OrderBy(p => p.ProductDiscountAmount >= 15).ToList();

            result = result.Where(p => p.ProductName.ToLower().Contains(txtSearch.Text.ToLower())).ToList();
            LViewProduct.ItemsSource = result;

            txtResultAmount.Text = result.Count().ToString();
        }

        private void txtSearch_SelectionChanged(object sender, System.Windows.RoutedEventArgs e)
        {
            UpdateData();
        }

        private void cmbSorting_SelectionChanged(object sender, System.Windows.RoutedEventArgs e)
        {
            UpdateData();
        }

        private void cmbFilter_SelectionChanged(object sender, System.Windows.RoutedEventArgs e)
        {
            UpdateData();
        }

        List<Product> orderProducts = new List<Product>();

        private void btnAddProduct_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            orderProducts.Add(LViewProduct.SelectedItem as Product);

            if (orderProducts.Count > 0)
            {
                btnOrder.Visibility = Visibility.Visible;
            }
        }

        private void bthOrder_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            OrderWindow order = new OrderWindow(orderProducts, user);
            order.ShowDialog();
        }
    }
}
