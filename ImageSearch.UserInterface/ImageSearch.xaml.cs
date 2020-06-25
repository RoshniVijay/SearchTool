using ImageSearch.ViewModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace ImageSearch.View
{
    /// <summary>
    /// Interaction logic for ImageSearchWindow.xaml
    /// </summary>
    public partial class ImageSearchWindow : Window
    {
        ImageSearchViewModel m_ImageSearchViewModel;

        public ImageSearchWindow()
        {
            InitializeComponent();
            m_ImageSearchViewModel = new ImageSearchViewModel();
            DataContext = m_ImageSearchViewModel;
        }

        private void ComboBox_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            int currentDisplayMode = m_displayOptionsCombo.SelectedIndex;

            switch (currentDisplayMode)
            {
                case 0://small
                    m_ImageSearchViewModel.ImageSize = new ViewModel.Size() { Height = 128, Width = 128 };
                    break;
                case 1://small
                    m_ImageSearchViewModel.ImageSize = new ViewModel.Size() { Height = 256, Width = 256 };
                    break;
                case 2://small
                    m_ImageSearchViewModel.ImageSize = new ViewModel.Size() { Height = 512, Width = 512 };
                    break;
                default:
                    break;
            }
        }

        private void OnTabControlLoad(object sender, RoutedEventArgs e)
        {
            m_displayOptionsCombo.SelectedIndex = 0;
          // string currentDisplayMode = m_displayOptionsCombo.SelectedValue.ToString();

        }

        private void M_DataSourceCombo_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            int currentDisplayMode = m_DataSourceCombo.SelectedIndex;
            switch (currentDisplayMode)
            {
                case 0://small
                    m_StackPanel.Visibility = Visibility.Collapsed;
                    m_ImageGrid.Visibility = Visibility.Visible;
                    break;
                case 1://small
                    m_StackPanel.Visibility = Visibility.Visible;
                    m_ImageGrid.Visibility = Visibility.Collapsed;

                    break;

                default:
                    break;


            }
        }

    
    }

  
}
