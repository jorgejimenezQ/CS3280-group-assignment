using System;
using System.Collections.Generic;
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

                    string currentInvoice = string.IsNullOrEmpty(InvoiceNumber) ? InvoiceNumber: "5000";
                    dtgItems.ItemsSource = ItemLogic.GetAllItems(currentInvoice);

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
                Button button = (Button)sender;

                if (button.Name == btnCancel.Name)
                {
                    this.Hide();

                    return;
                }

                if (button.Name == btnAdd.Name)
                {
                    return;
                }

                if (button.Name == btnDelete.Name)
                {
                    return;
                }

                if (button.Name == btnEdit.Name)
                {
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
    }
}
