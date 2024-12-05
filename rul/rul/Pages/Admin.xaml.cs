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
using rul.Pages;

namespace rul.Pages
{
    /// <summary>
    /// Логика взаимодействия для Admin.xaml
    /// </summary>
    public partial class Admin : Page
    {
        User user = new User();
        public Admin(User currentUser)
        {
            InitializeComponent();

            var product = RulEntities.GetContext().Product.ToList();
            lViewProduct.ItemsSource = product;
            DataContext = this;

            txtAllAmount.Text = product.Count().ToString();

            user = currentUser;

            UpdateData();
            User();
        }

        private void User()
        {
            if (user != null)
            {
                txtFullName.Text = user.UserSurname.ToString() + user.UserName.ToString() + " " + user.UserPatronymic.ToString();
            }
            else
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
            lViewProduct.ItemsSource = result;

            txtResultCount.Text = result.Count().ToString();
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

        private void lViewProduct_MouseDoubleClick(object sender,System.Windows.Input.MouseButtonEventArgs e)
        {
            NavigationService.Navigate(new AddEditProductPage(lViewProduct.SelectedItems as Product));
        }

        private void btnAddNewProduct_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new AddEditProductPage(null));
        }
        private void Page_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if(Visibility == Visibility.Visible)
            {
                RulEntities.GetContext().ChangeTracker.Entries().ToList().ForEach(p => p.Reload());
                lViewProduct.ItemsSource = RulEntities.GetContext().Product.ToList();
            }
        }
    }
}
