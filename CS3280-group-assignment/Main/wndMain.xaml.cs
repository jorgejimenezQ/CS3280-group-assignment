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
using CS3280_group_assignment.Items;
using CS3280_group_assignment.Search;
using System.Reflection;
using System.Globalization;

namespace CS3280_group_assignment.Main
{
    /// <summary>
    /// Interaction logic for wndMain.xaml
    /// </summary>
    public partial class wndMain : Window
    {
        /// <summary>
        /// Variable for items window
        /// </summary>
        wndItems items;

        /// <summary>
        /// Variable for search window
        /// </summary>
        wndSearch search;

        clsMainLogic mainLogic;

        Boolean creating;

        List<Item> itemsList;

        /// <summary>
        /// Initializes form
        /// </summary>
        public wndMain()
        {
            try
            {
                InitializeComponent();

                mainLogic = new clsMainLogic();
                creating = false;

                cbItems.ItemsSource = mainLogic.LoadItems();
                
            }
            catch (Exception ex)
            {
                clsHandleError.HandleError(MethodInfo.GetCurrentMethod().DeclaringType.Name,
                            MethodInfo.GetCurrentMethod().Name, ex.Message);
            }
        }

        /// <summary>
        /// Brings up items window and hides main window
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void itemsMenu_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (creating)
                {
                    lblError.Visibility = Visibility.Visible;
                    lblError.Content = "Cannot open items window while creating or editing an invoice.";
                    return;
                }
                lblError.Visibility = Visibility.Hidden;
                //Create and show new items window
                items = new wndItems();
                this.Hide();
                items.ShowDialog();
                this.Show();

                //Refresh items by fetching list of items and setting the source for itemComboBox
                //itemComboBox.ItemsSource = mainLogic.loadItems();
            }
            catch (Exception ex)
            {
                clsHandleError.HandleError(MethodInfo.GetCurrentMethod().DeclaringType.Name,
                            MethodInfo.GetCurrentMethod().Name, ex.Message);
            }
        }

        /// <summary>
        /// Brings up search window on top of main window
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void searchMenu_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                lblError.Visibility = Visibility.Hidden;
                //Create and show new search window
                mainLogic.selectedInvoice = null;
                search = new wndSearch(mainLogic);
                this.Hide();
                search.ShowDialog();
                this.Show();

                if (mainLogic.selectedInvoice == null)
                    return;

                //Load invoice items into data grid and other data into respective boxes
                lockForm();

                itemsList = mainLogic.getInvoiceItems();
                dgItems.ItemsSource = itemsList;
                dgItems.Items.Refresh();

                mainLogic.getInvoiceDetails();
                tbTotal.Text = Double.Parse(mainLogic.selectedInvoice.TotalCost).ToString("C2");
                tbNumber.Text = mainLogic.selectedInvoice.InvoiceNum;
                dpInvoice.Text = mainLogic.selectedInvoice.InvoiceDate;
                dpInvoice.SelectedDate = DateTime.Parse(mainLogic.selectedInvoice.InvoiceDate);
                
            }
            catch (Exception ex)
            {
                clsHandleError.HandleError(MethodInfo.GetCurrentMethod().DeclaringType.Name,
                            MethodInfo.GetCurrentMethod().Name, ex.Message);
            }
        }

        /// <summary>
        /// Activates when x button is clicked
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            try
            {
                this.Hide();
                e.Cancel = true;
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." +
                                    MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        private void bCreate_Click(object sender, RoutedEventArgs e)
        {
            lblError.Visibility = Visibility.Hidden;
            if (creating)
            {
                MessageBoxResult result;
                result = MessageBox.Show("Creating a new invoice will erase the current invoice. Continue?", "New Invoice",
                    MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (result == MessageBoxResult.Cancel || result == MessageBoxResult.No)
                    return;
            }
            openForm();

            bSave.IsEnabled = false;
            dpInvoice.SelectedDate = null;
            dpInvoice.Text = "";
            tbNumber.Text = "TBD";
            tbTotal.Text = "$0.00";
            dgItems.ItemsSource = null;
            dgItems.Items.Refresh();

            itemsList = new List<Item>();
            mainLogic.selectedInvoice = null;
        }

        private void cbItems_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(e.AddedItems.Count <= 0)
            {
                return;
            }
            bAdd.IsEnabled = true;
            Item item = (Item)e.AddedItems[0];
            //Format as money
            tbCost.Text = Double.Parse(item.Cost).ToString("C2");
        }

        private void bAdd_Click(object sender, RoutedEventArgs e)
        {
            lblError.Visibility = Visibility.Hidden;
            bSave.IsEnabled = true;
            bRemove.IsEnabled = true;
            Item item = (Item)cbItems.SelectedItem;

            //Get rid of $ so string can be parsed
            tbTotal.Text = tbTotal.Text.Replace('$', '0');
            tbTotal.Text = (Double.Parse(tbTotal.Text) + Double.Parse(item.Cost)).ToString("C2");

            //Add item to datagrid
            itemsList.Add(item);
            dgItems.ItemsSource = itemsList;
            dgItems.Items.Refresh();
        }

        private void bRemove_Click(object sender, RoutedEventArgs e)
        {
            if(dgItems.SelectedItem == null)
            {
                lblError.Visibility = Visibility.Visible;
                lblError.Content = "Please select an item to remove from the data grid.";
                return;
            }
            lblError.Visibility = Visibility.Hidden;

            Item item = (Item)dgItems.SelectedItem;
            itemsList.Remove(item);
            dgItems.ItemsSource = itemsList;
            dgItems.Items.Refresh();

            tbTotal.Text = tbTotal.Text.Replace('$', '0');
            tbTotal.Text = (Double.Parse(tbTotal.Text) - Double.Parse(item.Cost)).ToString("C2");
        }

        private void bSave_Click(object sender, RoutedEventArgs e)
        {
            lblError.Visibility = Visibility.Hidden;

            if(dgItems.Items.Count == 0)
            {
                lblError.Visibility = Visibility.Visible;
                lblError.Content = "Please add an item to the invoice.";
                return;
            }

            //insert new invoice
            if(mainLogic.selectedInvoice == null)
            {
                DateTime result;
                
                if (dpInvoice.SelectedDate == null ||
                    DateTime.TryParseExact(dpInvoice.Text, "MM/dd/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out result))
                {
                    lblError.Visibility = Visibility.Visible;
                    lblError.Content = "Please enter a valid date. (mm/dd/yyyy)";
                    return;
                }
                //add
                String total = tbTotal.Text.Replace('$', '0');
                clsInvoice invoice = new clsInvoice(dpInvoice.Text, Double.Parse(total).ToString());
                mainLogic.selectedInvoice = invoice;
                mainLogic.selectedInvoice.InvoiceNum = mainLogic.insertInvoice(invoice);
                tbNumber.Text = mainLogic.selectedInvoice.InvoiceNum;
                int lineItemNum = 1;
                foreach(Item item in itemsList)
                {
                    mainLogic.insertInvoiceItem(invoice.InvoiceNum, lineItemNum.ToString(), item.Code);
                    lineItemNum++;
                }
            }
            //update invoice
            else
            {
                //update
                String total = tbTotal.Text.Replace('$', '0');
                mainLogic.updateInvoice(mainLogic.selectedInvoice.InvoiceNum, Double.Parse(total).ToString());
                //delete old items
                mainLogic.deleteInvoiceItems(mainLogic.selectedInvoice.InvoiceNum);
                int lineItemNum = 1;
                foreach (Item item in itemsList)
                {
                    mainLogic.insertInvoiceItem(mainLogic.selectedInvoice.InvoiceNum, lineItemNum.ToString(), item.Code);
                    lineItemNum++;
                }
            }
            lockForm();
        }

        private void bEdit_Click(object sender, RoutedEventArgs e)
        {
            lblError.Visibility = Visibility.Hidden;
            openForm();

            bSave.IsEnabled = true;
            bRemove.IsEnabled = true;
            dpInvoice.IsEnabled = false;
        }

        private void bDelete_Click(object sender, RoutedEventArgs e)
        {
            lblError.Visibility = Visibility.Hidden;
            MessageBoxResult result;
            result = MessageBox.Show("Are you sure you want to delete this invoice?", "Delete Invoice",
                    MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Cancel || result == MessageBoxResult.No)
                return;

            //Delete invoice and all from line items with invoiceNum
            mainLogic.deleteInvoiceItems(mainLogic.selectedInvoice.InvoiceNum);
            mainLogic.deleteInvoice(mainLogic.selectedInvoice.InvoiceNum);

            //Reset form
            lockForm();
            tbNumber.IsEnabled = false;
            tbTotal.IsEnabled = false;
            bEdit.IsEnabled = false;
            bDelete.IsEnabled = false;
            tbTotal.Text = "";
            dpInvoice.Text = "";
            dpInvoice.SelectedDate = null;
            dgItems.ItemsSource = null;
            dgItems.Items.Refresh();
            mainLogic.selectedInvoice = null;
            itemsList = null;
            tbNumber.Text = "";
        }



        private void lockForm()
        {
            creating = false;
            dpInvoice.IsEnabled = false;
            cbItems.SelectedIndex = -1;
            cbItems.IsEnabled = false;
            bAdd.IsEnabled = false;
            bRemove.IsEnabled = false;
            bSave.IsEnabled = false;
            tbCost.Text = "";
            tbCost.IsEnabled = false;

            tbNumber.IsEnabled = true;
            tbTotal.IsEnabled = true;
            bEdit.IsEnabled = true;
            bDelete.IsEnabled = true;
        }

        private void openForm()
        {
            bAdd.IsEnabled = false;
            bRemove.IsEnabled = false;
            bEdit.IsEnabled = false;
            bDelete.IsEnabled = false;

            creating = true;
            dpInvoice.IsEnabled = true;
            dpInvoice.IsDropDownOpen = false;
            tbNumber.IsEnabled = true;
            tbCost.IsEnabled = true;
            tbTotal.IsEnabled = true;
            cbItems.IsEnabled = true;
            cbItems.SelectedIndex = -1;
            tbCost.Text = "";
        }

        
    }
}