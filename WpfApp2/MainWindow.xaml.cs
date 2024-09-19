using System.IO;
using System.Windows;
using System.Windows.Controls;

namespace WpfApp2;


public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();

        foreach (var file in App.Files!)
        {
            var menuItem = new MenuItem
            {
                Header = file
            };
            menuItem.Click += MenuItem_Style_Click;
            
            MenuItemStyles.Items.Add(menuItem);
        }
    }
    
    private void MenuItem_Exit_Click(object sender, RoutedEventArgs e)
    {
        Environment.Exit(0);
    }
 
    private void MenuItem_Style_Click(object sender, RoutedEventArgs e)
    {
        var mi = sender as MenuItem;
        var fileName = Path.Combine(App.StylesDirectory!, $"{mi!.Header}.xaml");
        App.Instance!.LoadStyleDictionaryFromFile(fileName);
    }
}