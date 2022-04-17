using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
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

namespace CS3280_group_assignment.Items
{

    /// <summary>
    /// Interaction logic for wndItems.xaml
    /// </summary>
    public partial class wndItems : Window
    {
        /// <summary>
        /// When this window is opened, this property needs to 
        /// be updated to the current invoice
        /// </summary>
        public string InvoiceNumber { set; get; }

        /// <summary>
        /// Used to handle the items logic
        /// </summary>
        public clsItemsLogic ItemLogic { private set; get; }


        /// <summary>
        /// This list holds all the items for the current invoice
        /// </summary>
        public List<Item> Items { set; get; }

        public wndItems()
        {
            try
            {
                InitializeComponent();

                // Init logic class and items list
                ItemLogic = new clsItemsLogic();
                Items = new List<Item>();
                InvoiceNumber = "";
            }
            catch (Exception ex)
            {
                clsHandleError.HandleError(MethodInfo.GetCurrentMethod().DeclaringType.Name,
                            MethodInfo.GetCurrentMethod().Name, ex.Message);
            }
        }



        /// <summary>
        ///  USED MAINLY FOR DEBUGGING 
        /// 
        /// Triggered when the visibility is changed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Window_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            try
            {

                // Only call GetAllItems when the isVisible changes from false to true
                if ((bool)e.NewValue)
                {
                    UpdateDataGrid();
                }
            }
            catch (Exception ex)
            {
                clsHandleError.HandleError(MethodInfo.GetCurrentMethod().DeclaringType.Name,
                            MethodInfo.GetCurrentMethod().Name, ex.Message);
            }
        }

        /// <summary>
        /// Will handle all the buttons clicked on the window
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnButtonClick(object sender, RoutedEventArgs e)
        {

            try
            {
                // TODO: Do we need to trigger an event for the main window

                // Get the code
                string code = lblCode.Text.ToString();

                // Code should be set
                if (String.IsNullOrEmpty(code))
                {
                    SetErrorMsg("Please select an item to start editing or fill out form to add an item.");
                    return;
                }

                ClearError();

                Button button = (Button)sender;

                // What button was called?

                // Cancel ...
                if (button.Name == btnCancel.Name)
                {
                    // TODO: Send event to main?
                    this.Hide();

                    return;
                }

                // Add ...
                if (button.Name == btnAdd.Name)
                {
                    //TODO: validate and implement
                    Item item = (Item)dtgItems.SelectedItem;
                    string description = txtDescription.Text;
                    string cost = txtCost.Text;

                    List<Item> items = ItemLogic.GetItemByCode(code);

                    if (items != null)
                        SetErrorMsg("The code submitted already exists.");

                    // Save item
                    ItemLogic.InsertItem(code, description, cost);

                    UpdateDataGrid();
                    ClearError();
                    
                    return;
                }

                // Delete ...
                if (button.Name == btnDelete.Name)
                {
                    List<string> invoices = ItemLogic.GetInvoiceWithItem(code);
                    
                    // The item belongs to invoices
                    if (invoices != null)
                    {
                        // Set the error
                        lblErrorMsg.Visibility = Visibility.Visible;
                        lblErrorMsg.Content = "Error: The item is used in the following invoices";

                        foreach (string invoice in invoices)
                        {
                            lblErrorMsg.Content += " - " + invoice;
                        }

                        // Don't do anything
                        return;
                    }

                    // Else delete
                    ItemLogic.DeleteItem(code);

                    // Update the data grid
                    UpdateDataGrid();
                    ClearUIText();
                    return;
                }

                // Edit
                if (button.Name == btnEdit.Name)
                {
                    string description;
                    string cost;
                    Item selItem = (Item)dtgItems.SelectedItem;

                    

                    // Validate the input
                    if (!String.IsNullOrEmpty(txtDescription.Text))
                        description = txtDescription.Text;
                    else
                        description = null;

                    if (!String.IsNullOrEmpty(txtCost.Text))
                        cost = txtCost.Text;
                    else
                        cost = null;

                    // Don't do anything if the inputs have not been changed
                    if (cost == selItem.Cost)
                        cost = null;

                    if (description == selItem.Description)
                        description = null;

                    if (String.IsNullOrEmpty(description) && String.IsNullOrEmpty(cost))
                    {
                        SetErrorMsg("At least one field, except code, must be changed.");
                        return;
                    }

                    
                    // TODO: Validate string length

                    // Update item
                    ItemLogic.UpdateItem(code, description, cost);

                    UpdateDataGrid();
                    
                    return;
                }

            }
            catch (Exception ex)
            {
                clsHandleError.HandleError(MethodInfo.GetCurrentMethod().DeclaringType.Name,
                            MethodInfo.GetCurrentMethod().Name, ex.Message);
            }
        }

        /// <summary>
        /// Only allow numbers to be entered in the cost field
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtCost_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                // Only allow numbers to be entered
                if (!((e.Key >= Key.D0 && e.Key <= Key.D9) || ((e.Key >= Key.NumPad0 && e.Key <= Key.NumPad9))))
                {
                    // Allow the user to delete with backspace
                    if (!(e.Key == Key.Back || e.Key == Key.Delete))
                    {
                        e.Handled = true;
                    }
                }
            }
            catch (Exception ex)
            {
                clsHandleError.HandleError(MethodInfo.GetCurrentMethod().DeclaringType.Name,
                            MethodInfo.GetCurrentMethod().Name, ex.Message);
            }
        }

        private void dtgItems_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                // Get the selected item from the data grid
                DataGrid dg = (DataGrid)sender;
                Item selectedItem = (Item)dg.SelectedItem;

                if (selectedItem == null) return;

                // Update the UI
                SetUIText(selectedItem.Description, selectedItem.Cost, selectedItem.Code);
            }
            catch (Exception ex)
            {
                clsHandleError.HandleError(MethodInfo.GetCurrentMethod().DeclaringType.Name,
                            MethodInfo.GetCurrentMethod().Name, ex.Message);
            }
        }


        /********  HELPERS *********/


        /// <summary>
        /// Removes the message error
        /// </summary>
        /// <exception cref="Exception"></exception>
        public void ClearError()
        {
            try
            {
                lblErrorMsg.Content = "";
                lblErrorMsg.Visibility = Visibility.Collapsed;
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." +
                                    MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        /// <summary>
        /// Show an error message to the user 
        /// </summary>
        /// <param name="msg"></param>
        /// <exception cref="Exception"></exception>
        public void SetErrorMsg(string msg)
        {
            try
            {
                lblErrorMsg.Content = msg;
                lblErrorMsg.Visibility = Visibility.Visible;
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." +
                                    MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        /// <summary>
        /// Updates the description, cost, and code fields
        /// </summary>
        /// <param name="description"></param>
        /// <param name="cost"></param>
        /// <param name="code"></param>
        /// <exception cref="Exception"></exception>
        public void SetUIText(string description, string cost, string code)
        {
            try
            {
                txtDescription.Text = description;
                txtCost.Text = cost;
                lblCode.Text = code;
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." +
                                    MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        /// <summary>
        /// Clears the code and the user input
        /// </summary>
        /// <exception cref="Exception"></exception>
        public void ClearUIText()
        {
            try
            {
                txtDescription.Text = string.Empty;
                txtCost.Text = string.Empty;
                lblCode.Text = string.Empty;
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." +
                                    MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }
 
        /// <summary>
        /// Updates and sorts the Datagrid
        /// </summary>
        public void UpdateDataGrid()
        {
            try
            {
                dtgItems.ItemsSource = ItemLogic.GetAllItems();
                dtgItems.Items.SortDescriptions.Add(new SortDescription("Code", ListSortDirection.Ascending));
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." +
                                    MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }
    }
}
