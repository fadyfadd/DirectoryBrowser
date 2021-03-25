using System;
using System.Collections.Generic;
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
 

namespace DirectoryBrowser
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Grid_Loaded(object sender, RoutedEventArgs e)
        {
            var drives = Directory.GetLogicalDrives();         
           

            foreach (var drive in drives)
            {
                var currentItem = new TreeViewItem();
                currentItem.Items.Add(null);                
                currentItem.Header = drive.ToString();
                currentItem.Tag = drive.ToString();
                treeView.Items.Add(currentItem);

                currentItem.Expanded += CurrentItem_Expanded;
              
            }
        }

        private void CurrentItem_Expanded(object sender, RoutedEventArgs e)
        {
            try
            {
               
                var currentItem = (TreeViewItem)sender;

                if (currentItem.Items.Count ==0 || currentItem.Items[0] != null)
                    return;

              
                currentItem.Items.RemoveAt(0);

                var tag = currentItem.Tag;
                var entries = Directory.GetFileSystemEntries(tag.ToString());

                foreach (String entry in entries)
                {
                    DirectoryInfo info = new DirectoryInfo(entry);

                   
                    

                    var fileName = System.IO.Path.GetFileName(entry);
                    var itemToAdd = new TreeViewItem();
                    if ((info.Attributes & FileAttributes.Directory) == FileAttributes.Directory)
                        itemToAdd.Items.Add(null);
                    itemToAdd.Header = fileName;
                    itemToAdd.Tag = entry.ToString();
                    itemToAdd.Expanded += CurrentItem_Expanded;
                    currentItem.Items.Add(itemToAdd);
                   
                }
                
            }
            catch
            {

            }    

        }
    }
}
