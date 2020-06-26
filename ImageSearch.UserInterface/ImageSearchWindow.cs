using System.Windows;
using ImageSearch.ViewModel;

namespace ImageSearch.View
{
    /// <summary>
    /// Interaction logic for ImageSearchWindow.xaml
    /// </summary>
    public partial class ImageSearchWindow : Window
    {
        ImageSearchViewModel m_ImageSearchViewModel;

        /// <summary>
        /// Main Window. 
        /// </summary>
        public ImageSearchWindow()
        {
            InitializeComponent();
            //Bind View model
            m_ImageSearchViewModel = new ImageSearchViewModel();
            DataContext = m_ImageSearchViewModel;
        }

        private void OnTabControlLoad(object sender, RoutedEventArgs e)
        {
            //set first entry as default.
            m_displayOptionsCombo.SelectedIndex = 0; 

        }

        private void OnDataSourceSelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            int currentDisplayMode = m_DataSourceCombo.SelectedIndex;
            switch (currentDisplayMode)
            {
                case 0://Flicker
                    m_NewsStackPanel.Visibility = Visibility.Collapsed;
                    m_ImageGrid.Visibility = Visibility.Visible;
                    m_ImageGridControl.Visibility = Visibility.Visible;
                    break;
                case 1://News
                    m_NewsStackPanel.Visibility = Visibility.Visible;
                    m_ImageGrid.Visibility = Visibility.Collapsed;
                    m_ImageGridControl.Visibility = Visibility.Collapsed;
                    break;

                default:
                    break;//ignore. dont change anything
            }
        }
    }
}
