using System;
using System.Collections.Generic;
using System.Data;
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

namespace Applicant
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private DataTable dt;
        private const string Fields = "Name,Salary,Salary_Deductions,Net_Salary(salary-sal deduction),Quilified(net_sal >= quilifing sal),Expenses,Repayments,Approved(expen+repay <= net_sal/2)";
        private string[] fields;
        private string csvstring = "";
        public MainWindow()
        {
            InitializeComponent();
            Init();
        }

        private void Init()
        {
            tracetable.SizeChanged += Tracetable_SizeChanged;
            dt = new DataTable("TraceTable");
            fields = Fields.Split(',');
            foreach (string field in fields)
            {
                csvstring += field + ",";
                dt.Columns.Add(field);
            }
            csvstring = csvstring.Substring(0, csvstring.LastIndexOf(',')) + "\n";

            tracetable.ItemsSource = dt.DefaultView;
            com.Click += ComputeForApplicant;
            excel.Click += Excel_Click;
        }

        private void Excel_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string filename = AppDomain.CurrentDomain.BaseDirectory + "tracetable.csv";
                File.WriteAllText(filename, csvstring);
                System.Diagnostics.Process.Start(filename);
            }
            catch (Exception ex) { MessageBox.Show(ex.Message+"\n\nTry To close current excel sheet.","Generator - Error", MessageBoxButton.OK,MessageBoxImage.Error); }
        }

        private void Tracetable_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            foreach (DataGridColumn col in tracetable.Columns)
                col.Width = (tracetable.ActualWidth / tracetable.Columns.Count);
        }

        private void ComputeForApplicant(object sender, RoutedEventArgs e)
        {
            try
            {
                ApplicantO a = new ApplicantO { name = na.Text, sal = Convert.ToDouble(sal.Text), sald = Convert.ToDouble(sald.Text), expen = Convert.ToDouble(ex.Text), repay = Convert.ToDouble(rep.Text) };
                dt.Rows.Add(a.name, a.sal, a.sald, a.sal_net, a.quilified, a.expen, a.repay, a.approved);

                for (int i = 0; i < dt.Columns.Count; i++)
                    csvstring += dt.Rows[dt.Rows.Count - 1][i].ToString() + ",";

                csvstring = csvstring.Substring(0, csvstring.LastIndexOf(',')) + "\n";
            }
            catch (Exception ex) { MessageBox.Show(ex.Message + "\n\nTry filling in all fields with proper types", "Generator - Error", MessageBoxButton.OK, MessageBoxImage.Error); }
        }

    }
}
