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
            try
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
            catch (Exception ex)
            {
                clsHandleError.HandleError(MethodInfo.GetCurrentMethod().DeclaringType.Name,
                            MethodInfo.GetCurrentMethod().Name, ex.Message);
            }
        }

        private void clearClicked(object sender, RoutedEventArgs e)
        {
            try
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
            catch (Exception ex)
            {
                clsHandleError.HandleError(MethodInfo.GetCurrentMethod().DeclaringType.Name,
                            MethodInfo.GetCurrentMethod().Name, ex.Message);
            }
        }

        

        private void updateInvoices()
        {
            try
            {
                invoice_list.ItemsSource = searchLogic.getInvoices(filterDate, filterNum, filterCost);
                updateInvoiceNums();
                updateCosts();
            }
            catch (Exception ex)
            {
                clsHandleError.HandleError(MethodInfo.GetCurrentMethod().DeclaringType.Name,
                            MethodInfo.GetCurrentMethod().Name, ex.Message);
            }
        }

        private void updateInvoiceNums()
        {
            try
            {
                invoiceNumber.ItemsSource = searchLogic.getInvoiceNums(filterDate, filterCost);
            }
            catch (Exception ex)
            {
                clsHandleError.HandleError(MethodInfo.GetCurrentMethod().DeclaringType.Name,
                            MethodInfo.GetCurrentMethod().Name, ex.Message);
            }
        }
        private void updateCosts()
        {
            try
            {
                totalCharge.ItemsSource = searchLogic.GetTotalCharges(filterDate, filterNum);
            }
            catch (Exception ex)
            {
                clsHandleError.HandleError(MethodInfo.GetCurrentMethod().DeclaringType.Name,
                            MethodInfo.GetCurrentMethod().Name, ex.Message);
            }
        }

        private void invoice_list_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                selected = (clsInvoice)invoice_list.SelectedItem;
            }
            catch (Exception ex)
            {
                clsHandleError.HandleError(MethodInfo.GetCurrentMethod().DeclaringType.Name,
                            MethodInfo.GetCurrentMethod().Name, ex.Message);
            }
        }

        private void InvoiceNumChosen(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                if (invoiceNumber.SelectedItem != null)
                    filterNum = invoiceNumber.SelectedItem.ToString();
                updateInvoices();
            }
            catch (Exception ex)
            {
                clsHandleError.HandleError(MethodInfo.GetCurrentMethod().DeclaringType.Name,
                            MethodInfo.GetCurrentMethod().Name, ex.Message);
            }
        }
        private void TotalCostChosen(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                if (totalCharge.SelectedItem != null)
                    filterCost = totalCharge.SelectedItem.ToString();
                updateInvoices();
            }
            catch (Exception ex)
            {
                clsHandleError.HandleError(MethodInfo.GetCurrentMethod().DeclaringType.Name,
                            MethodInfo.GetCurrentMethod().Name, ex.Message);
            }
        }

        private void datePickerChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                if (invoiceDateBox.SelectedDate != null)
                    filterDate = invoiceDateBox.SelectedDate.Value.ToString("M/dd/yyyy");
                updateInvoices();
            }
            catch (Exception ex)
            {
                clsHandleError.HandleError(MethodInfo.GetCurrentMethod().DeclaringType.Name,
                            MethodInfo.GetCurrentMethod().Name, ex.Message);
            }
        }
    }
}
