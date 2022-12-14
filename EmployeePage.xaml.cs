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
    /// Логика взаимодействия для EmployeePage.xaml
    /// </summary>
    
    public partial class EmployeePage : Page
    {
        private Employee _au;
        public EmployeePage(int id)
        {
            InitializeComponent();
            using (var db = new ApplicationContext())
            {
                _au = db.Employees.Find(id);
            }

            using (var db = new ApplicationContext())
            {


                foreach (var report in db.Reports.Where(x=>x.Name==_au.Name).ToList()) // добавление всех отчетов пользователя
                {
                    ReportsList.Items.Add(report);
                }

                Salary.Text = $"Заработанно на данный момент: {_au.Salary}"; // вывод зп

            }

          
        }

        private void ReportButton_Click(object sender, RoutedEventArgs e) // создание отчета 
        { 
            using (var db = new ApplicationContext())
            {
                db.Add(new Report { Name = _au.Name, Description = TextReportTextBox.Text, Hours = Convert.ToInt32(HoursReportTextBox.Text)});
                db.SaveChanges();
            }
        }

        private void RefreshButton_Click(object sender, RoutedEventArgs e) // обновление листа отчетов
        {

            ReportsList.Items.Clear();
            using (var db = new ApplicationContext())
            {


                foreach (var report in db.Reports.Where(x => x.Name == _au.Name).ToList()) // добавление всех отчетов пользователя
                {
                    ReportsList.Items.Add(report);
                }
        }   }
    }
}
