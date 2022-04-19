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
                search = new wndSearch(mainLogic);
                this.Hide();
                search.ShowDialog();
                this.Show();

                //Load invoice items into data grid and other data into respective boxes
                lockForm();

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
            bSave.IsEnabled = true;
            Item item = (Item)cbItems.SelectedItem;
            //Get rid of $ so string can be parsed
            tbTotal.Text = tbTotal.Text.Replace('$', '0');
            tbTotal.Text = (Double.Parse(tbTotal.Text) + Double.Parse(item.Cost)).ToString("C2");
            //Add item to datagrid
        }

        private void bSave_Click(object sender, RoutedEventArgs e)
        {
            lockForm();

            //update invoice
        }

        private void bEdit_Click(object sender, RoutedEventArgs e)
        {
            openForm();

            bSave.IsEnabled = true;
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

        private void bDelete_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result;
            result = MessageBox.Show("Are you sure you want to delete this invoice?", "Delete Invoice",
                MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Cancel || result == MessageBoxResult.No)
                return;
        }
    }
}