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

        /// <summary>
        /// Holds all the business logic
        /// </summary>
        clsMainLogic mainLogic;

        /// <summary>
        /// True if user is currently creating or editing an invoice
        /// </summary>
        Boolean creating;

        /// <summary>
        /// Holds the items for the current invoice
        /// </summary>
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
                itemsList = new List<Item>();  
                
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
                cbItems.ItemsSource = mainLogic.LoadItems();
                //update current items
                List<Item> oldItems = new List<Item>();
                int total = 0;
                foreach (Item item in itemsList)
                {
                    oldItems.Add(item);
                }
                itemsList.Clear();
                foreach(Item oldItem in oldItems)
                {
                    foreach(Item newItem in cbItems.Items)
                    {
                        if(oldItem.Code.Equals(newItem.Code))
                        {
                            total += int.Parse(newItem.Cost);
                            itemsList.Add(newItem);
                        }
                    }
                }
                dgItems.ItemsSource = itemsList;
                dgItems.Items.Refresh();
                tbTotal.Text = total.ToString("C2");
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

        /// <summary>
        /// Sets form to empty state, starts new invoice
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bCreate_Click(object sender, RoutedEventArgs e)
        {
            try
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
            catch (Exception ex)
            {
                clsHandleError.HandleError(MethodInfo.GetCurrentMethod().DeclaringType.Name,
                            MethodInfo.GetCurrentMethod().Name, ex.Message);
            }
        }

        /// <summary>
        /// Allow user to add item, show item cost in cost box
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cbItems_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                if (e.AddedItems.Count <= 0)
                {
                    return;
                }
                bAdd.IsEnabled = true;
                Item item = (Item)e.AddedItems[0];
                //Format as money
                tbCost.Text = Double.Parse(item.Cost).ToString("C2");
            }
            catch (Exception ex)
            {
                clsHandleError.HandleError(MethodInfo.GetCurrentMethod().DeclaringType.Name,
                            MethodInfo.GetCurrentMethod().Name, ex.Message);
            }
        }

        /// <summary>
        /// Add selected item to items list and datagrid, update total cost
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bAdd_Click(object sender, RoutedEventArgs e)
        {
            try
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
            catch (Exception ex)
            {
                clsHandleError.HandleError(MethodInfo.GetCurrentMethod().DeclaringType.Name,
                            MethodInfo.GetCurrentMethod().Name, ex.Message);
            }
        }

        /// <summary>
        /// Remove selected item from items list, update total cost
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bRemove_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (dgItems.SelectedItem == null)
                {
                    lblError.Visibility = Visibility.Visible;
                    lblError.Content = "Please select an item to remove from the data grid.";
                    return;
                }
                lblError.Visibility = Visibility.Hidden;

                //Remove item
                Item item = (Item)dgItems.SelectedItem;
                itemsList.Remove(item);
                dgItems.ItemsSource = itemsList;
                dgItems.Items.Refresh();

                //Update total cost
                tbTotal.Text = tbTotal.Text.Replace('$', '0');
                tbTotal.Text = (Double.Parse(tbTotal.Text) - Double.Parse(item.Cost)).ToString("C2");
            }
            catch (Exception ex)
            {
                clsHandleError.HandleError(MethodInfo.GetCurrentMethod().DeclaringType.Name,
                            MethodInfo.GetCurrentMethod().Name, ex.Message);
            }
        }

        /// <summary>
        /// Add invoice to database if it doesn't already exist, update it if it does exist, and then lock form
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bSave_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                lblError.Visibility = Visibility.Hidden;

                if (dgItems.Items.Count == 0)
                {
                    lblError.Visibility = Visibility.Visible;
                    lblError.Content = "Please add an item to the invoice.";
                    return;
                }

                //insert new invoice
                if (mainLogic.selectedInvoice == null)
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
                    foreach (Item item in itemsList)
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
            catch (Exception ex)
            {
                clsHandleError.HandleError(MethodInfo.GetCurrentMethod().DeclaringType.Name,
                            MethodInfo.GetCurrentMethod().Name, ex.Message);
            }
        }

        /// <summary>
        /// Open form for user to edit
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bEdit_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                lblError.Visibility = Visibility.Hidden;
                openForm();

                bSave.IsEnabled = true;
                bRemove.IsEnabled = true;
                dpInvoice.IsEnabled = false;
            }
            catch (Exception ex)
            {
                clsHandleError.HandleError(MethodInfo.GetCurrentMethod().DeclaringType.Name,
                            MethodInfo.GetCurrentMethod().Name, ex.Message);
            }
        }

        /// <summary>
        /// Delete invoice and all items from the invoice
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bDelete_Click(object sender, RoutedEventArgs e)
        {
            try
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
                itemsList.Clear();
                tbNumber.Text = "";
            }
            catch (Exception ex)
            {
                clsHandleError.HandleError(MethodInfo.GetCurrentMethod().DeclaringType.Name,
                            MethodInfo.GetCurrentMethod().Name, ex.Message);
            }
        }


        /// <summary>
        /// Puts form into read only state
        /// </summary>
        private void lockForm()
        {
            try
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
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." +
                                    MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        /// <summary>
        /// Lets user edit form details
        /// </summary>
        private void openForm()
        {
            try
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
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." +
                                    MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        
    }
}