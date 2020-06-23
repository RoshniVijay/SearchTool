using ImageSearch.ViewModel;
using System.Windows;

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
    }
}
