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
    /// Логика взаимодействия для HeadPage.xaml
    /// </summary>
    public partial class HeadPage : Page
    {
        public HeadPage()
        {
            InitializeComponent();

            using (var db = new ApplicationContext())
            {
                foreach (var report in db.Reports.ToList()) // добавление всех отчетов в лист
                {
                    ReportsList.Items.Add(report);
                }

                foreach (var employee in db.Employees.ToList()) // добавление всех сотрудников в лист
                {
                    Employees.Items.Add(employee);
                }
            }    
        }

        private void RefreshReportsButton_Click(object sender, RoutedEventArgs e) // обновление отчетов
        {
            ReportsList.Items.Clear();

            using (var db = new ApplicationContext())
            {
                foreach (var report in db.Reports.ToList()) // добавление всех отчетов в лист
                {
                    ReportsList.Items.Add(report);
                }
            }
        }
        private void RefreshEmployeesButton_Click(object sender, RoutedEventArgs e) // обновление сотрудников
        {
            Employees.Items.Clear();
            using (var db = new ApplicationContext())
            {
                foreach (var report in db.Employees.ToList()) // добавление всех отчетов в лист
                {
                    Employees.Items.Add(report);
                }
        }   }

        private void AddEmployeeButton_Click(object sender, RoutedEventArgs e) // добавление сотрудника 
        {
            try 
            {
                
                using (var db = new ApplicationContext())
                {
                    if(Convert.ToBoolean(HeadCheck.IsChecked))
                    {
                        db.Employees.Add(new Employee { Name = NewEmployeeNameTextBox.Text, Post = NewEmployeePostTextBox.Text, Salary=0, RoleId = 2 });
                    }
                    else
                    {
                        db.Employees.Add(new Employee { Name = NewEmployeeNameTextBox.Text, Post = NewEmployeePostTextBox.Text,Salary=0, RoleId = 1 });
                    }
                    db.SaveChanges();
                }
                    
            }
            catch
            {
                MessageBox.Show("Что-то пошло не так, проверьте введенные данные");
            }
        }

        private void SalaryButton_Click(object sender, RoutedEventArgs e) // добавление зп
        {
            try 
            {
                using (var db = new ApplicationContext())
                {
                    var user = db.Employees.Where(x=>x.Name==SalaryTextBox.Text).FirstOrDefault();
                    user.Salary += Convert.ToInt32(SalaryCount.Text);
                    db.SaveChanges();
                }
            }
            catch
            {
                MessageBox.Show("Что-то пошло не так, проверьте введенные данные");
            }
        }

        private void PenaltyButton_Click(object sender, RoutedEventArgs e) // вычет зп
        {
            try
            {
                using (var db = new ApplicationContext())
                {
                    var user = db.Employees.Where(x => x.Name == SalaryTextBox.Text).FirstOrDefault();
                    user.Salary -= Convert.ToInt32(SalaryCount.Text);
                    db.SaveChanges();
                }
            }
            catch
            {
                MessageBox.Show("Что-то пошло не так, проверьте введенные данные");
            }
        }

        private void DeleteEmployeeButton_Click(object sender, RoutedEventArgs e) // удаление сотрудника
        {
            try
            {
                using (var db = new ApplicationContext())
                {
                    var user = db.Employees.Where(x => x.Name == SalaryTextBox.Text).FirstOrDefault();
                    db.Employees.Remove(user);
                }
            }
            catch
            {
                MessageBox.Show("Что-то пошло не так, проверьте введенные данные");
            }
        }
    }
}
