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

        /// <summary>
        /// Initializes form
        /// </summary>
        public wndMain()
        {
            try
            {
                InitializeComponent();

                items = new wndItems();
                search = new wndSearch();
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
                this.Hide();
                items.ShowDialog();
                this.Show();
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
                search.ShowDialog();
                this.Show();
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
    }
}