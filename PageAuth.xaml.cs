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

namespace Reports
{
    /// <summary>
    /// Логика взаимодействия для PageAuth.xaml
    /// </summary>
    public partial class PageAuth : Page
    {
        public PageAuth()
        {
            InitializeComponent();
            //using (var db = new ApplicationContext())
            //{
            //    var role1 = new Role { User_Role = "User" };
            //    var role2 = new Role { User_Role = "Head" };

            //    var u1 = new Employee { Name = "Tom", Post = "Loader", Salary = 0, RoleId = 1 };
            //    var u2 = new Employee { Name = "Alice", Post = "Manager", Salary = 0, RoleId = 1 };
            //    var u3 = new Head { Name = "Bob", Post = "Director", Salary = 0, RoleId = 2 };
            //    var u4 = new Head { Name = "John", Post = "Diputy Director", Salary = 0, RoleId = 2 };

            //    db.Roles.AddRange(role1, role2);
            //    db.Employees.AddRange(u1, u2, u3, u4);
            //    db.SaveChanges();
            //}

        }

        private void AuthorizeButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                using (var db = new ApplicationContext())
                {
                    var user_role = db.Employees.Where(x => x.Name == AuthorizeTextBox.Text).FirstOrDefault().RoleId;
                    var user_id = db.Employees.Where(x => x.Name == AuthorizeTextBox.Text).FirstOrDefault().Id;

                    switch (user_role) // показ страниц в зависимости от роли сотрудника
                    {
                        case 1:
                            NavigationService.Navigate(new EmployeePage(user_id));
                            break;
                        case 2:
                            NavigationService.Navigate(new HeadPage());
                            break;
                        default:
                            MessageBox.Show("Нет такого сотрудника");
                            break;
                    }
                }
            }
            catch
            {
                MessageBox.Show("Такого сотрудника нет");
            }
            
        }
    }

}
