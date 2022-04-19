using CS3280_group_assignment.Main;
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
using System.Windows.Shapes;

namespace CS3280_group_assignment.Search
{
    /// <summary>
    /// Interaction logic for wndSearch.xaml
    /// </summary>
    public partial class wndSearch : Window
    {
        clsMainLogic mainLogic;
        public wndSearch(clsMainLogic _mainLogic)
        {
            InitializeComponent();
            mainLogic = _mainLogic;
        }

        clsInvoice selected;

        private void select_bttn_Click(object sender, RoutedEventArgs e)
        {
            //W/e selected invoice
            if (selected != null)
            {
                ///bring the wndMain back with the selected invoice
                mainLogic.setSelectedInvoice(selected);
                Window window = new wndMain();
                this.Hide();
                window.ShowDialog();
                this.Show();
            }
            else
            {
                warninglbl.Content = "No Invoice was selected";
            }
        }

        private void invoice_list_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
