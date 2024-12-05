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
    /// Логика взаимодействия для OrderPage.xaml
    /// </summary>
    public partial class OrderPage : Page
    {
        List<Product> productList = new List<Product>();
        public OrderPage(List<Product> products, User user)
        {
            InitializeComponent();

            DataContext = this;
            productList = products;
            lViewOrder.ItemsSource = productList;

            cmbPickupPoint.ItemsSource = RulEntities.GetContext().PickupPoint.ToList();
            
            if(user != null)
            {
                txtUser.Text = user.UserSurname.ToString() + user.UserName.ToString() + " " + user.UserPatronymic.ToString();
            }
        }

        public string Total
        {
            get
            {
                var total = productList.Sum(p => Convert.ToDouble(p.ProductCost) - Convert.ToDouble(p.ProductCost) * Convert.ToDouble(p.ProductDiscountAmount / 100.00));
                return total.ToString();
            }
        }

        private void btnDeleteProduct_Click(object sender, RoutedEventArgs e)
        {
            if(MessageBox.Show("Вы уверены, что хотите удалить этот элемент?", "Предупреждение", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
            {
                productList.Remove(lViewOrder.SelectedItem as Product);
            }
        }

        private void btnOrderSave_Click(object sender, RoutedEventArgs e)
        {
            var productArticle = productList.Select(p => p.ProductArticleNumber).ToArray();
            Random random = new Random();
            var date = DateTime.Now;
            if (productList.Any(p => p.ProductQuantityInStock < 3))
                date = date.AddDays(6);
            else
                date = date.AddDays(3);

            if (cmbPickupPoint.SelectedItem == null)
            {
                MessageBox.Show("Выберите пункт выдачи!", "Информация", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }

            try
            {
                using (var context = RulEntities.GetContext())
                {
                    Order newOrder = new Order()
                    {
                        OrderStatus = "Новый",
                        OrderDate = DateTime.Now,
                        OrderPickupPoint = cmbPickupPoint.SelectedIndex + 1,
                        OrderDeliveryDate = date,
                        RecieptCode = random.Next(100, 1000),
                        ClientFullName = txtUser.Text,
                    };
                    context.Order.Add(newOrder);
                    context.SaveChanges(); // Сохраняем заказ, чтобы получить его ID

                    foreach (var product in productList)
                    {
                        OrderProduct newOrderProduct = new OrderProduct()
                        {
                            OrderID = newOrder.OrderID,
                            ProductArticleNumber = product.ProductArticleNumber,
                            ProductCount = 1 // Предполагается, что количество продуктов равно 1
                        };
                        RulEntities.GetContext().OrderProduct.Add(newOrderProduct);
                    }
                    context.SaveChanges(); // Сохраняем записи в таблице OrderProduct
                }

                MessageBox.Show("Заказ оформлен!", "Информация", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }

    }
}
