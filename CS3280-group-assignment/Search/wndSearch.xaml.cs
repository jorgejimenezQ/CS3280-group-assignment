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
using System.Reflection;
using System.Globalization;

using System.Diagnostics;

namespace CS3280_group_assignment.Search
{
    /// <summary>
    /// Interaction logic for wndSearch.xaml
    /// </summary>
    public partial class wndSearch : Window
    {
        clsMainLogic mainLogic;
        clsSearchLogic searchLogic = new clsSearchLogic();
        public wndSearch(clsMainLogic _mainLogic)
        {
            InitializeComponent();
            mainLogic = _mainLogic;
            updateInvoices();
        }

        clsInvoice selected;

        string filterDate;
        string filterNum;
        string filterCost;

        private void select_bttn_Click(object sender, RoutedEventArgs e)
        {
            //W/e selected invoice
            if (selected != null)
            {
                ///bring the wndMain back with the selected invoice
                mainLogic.selectedInvoice = selected;
                this.Close();
            }
            else
            {
                warninglbl.Content = "No Invoice was selected";
            }
        }

        private void clearClicked(object sender, RoutedEventArgs e)
        {
            filterDate = null;
            invoiceDateBox.Text = "";
            filterNum = null;
            invoiceNumber.Text = "";
            filterCost = null;
            totalCharge.Text = "";

            selected = null;
            invoice_list.SelectedItem = null;
            updateInvoices();
        }



        private void updateInvoices()
        {
            invoice_list.ItemsSource = searchLogic.getInvoices(filterDate, filterNum, filterCost);
            updateInvoiceNums();
            updateCosts();
        }

        private void updateInvoiceNums()
        {
            invoiceNumber.ItemsSource = searchLogic.getInvoiceNums(filterDate, filterCost);
        }
        private void updateCosts()
        {
            totalCharge.ItemsSource = searchLogic.GetTotalCharges(filterDate, filterNum);
        }

        private void invoice_list_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            selected = (clsInvoice)invoice_list.SelectedItem;
        }

        private void InvoiceNumChosen(object sender, SelectionChangedEventArgs e)
        {
            if (invoiceNumber.SelectedItem != null)
                filterNum = invoiceNumber.SelectedItem.ToString();
            updateInvoices();
        }
        private void TotalCostChosen(object sender, SelectionChangedEventArgs e)
        {
            if (totalCharge.SelectedItem != null)
                filterCost= totalCharge.SelectedItem.ToString();
            updateInvoices();
        }

        private void datePickerChanged(object sender, SelectionChangedEventArgs e)
        {
            if(invoiceDateBox.SelectedDate != null)
                filterDate = invoiceDateBox.SelectedDate.Value.ToString("M/dd/yyyy");
            updateInvoices();
        }
    }
}
