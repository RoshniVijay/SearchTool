using ImageSearch.Common;
using ImageSearch.DataModel;
using ImageSearch.DataModel.Contracts;
using System.Collections.ObjectModel;

namespace ImageSearch.ViewModel
{
    /// <summary>
    /// Partial class for Flicker view model
    /// </summary>
    public partial class ImageSearchViewModel
    {
        #region Private variables
        /// <summary>
        /// Size of the thumbnail image
        /// </summary>
        private Size m_Size;
        /// <summary>
        /// URI data for thumbnail display
        /// </summary>
        private ObservableCollection<ThumbnailData> m_ImageResponseURICollection;
        /// <summary>
        /// Display options like - small medium large
        /// </summary>
        private ObservableCollection<string> m_DisplayOptions;
        /// <summary>
        /// Currently selected option
        /// </summary>
        private string m_CurrentThumbnailDisplayOption;

        #endregion Private variables

        #region Init

        /// <summary>
        /// Initialized flicker specific view control values
        /// </summary>
        private void InitializeFlickerData()
        {
            m_Size = new Size() { Height = 128, Width = 128 }; //initialize to small by default

            m_DisplayOptions = new ObservableCollection<string>
            {
                ThumbnailDisplaySize.SMALL, ThumbnailDisplaySize.MEDIUM, ThumbnailDisplaySize.LARGE
            };

            OnPropertyChange("ThumbnailDisplayOptions");
        }

        #endregion Init

        #region View model binding Properties

        /// <summary>
        /// Thumbnail image size - small medium large
        /// </summary>
        public Size ImageSize
        {
            get => m_Size;
            set
            {
                if (m_Size != value)
                {
                    m_Size = value;
                    OnPropertyChange("ImageSize");
                    //If image size is updated, reload the view for images in new size also
                    OnPropertyChange("ImageResponseURICollection");
                }
            }

        }

        /// <summary>
        /// Thumbnail display options available - small medium large
        /// </summary>
        public ObservableCollection<string> ThumbnailDisplayOptions
        {
            get => m_DisplayOptions;
            set
            {
                m_DisplayOptions = value;
                OnPropertyChange("ThumbnailDisplayOptions");
            }
        }

        /// <summary>
        /// Currently selected thumbnail display options. Once this is changed, update the corresponding size also.
        /// </summary>
        public string CurrentThumbnailDisplayOption
        {
            get => m_CurrentThumbnailDisplayOption;
            set
            {
                m_CurrentThumbnailDisplayOption = value;
                OnPropertyChange("CurrentThumbnailDisplayOption");

                //Once display option is changed, update the image size also for the image 
                UpdateImageSize();

            }
        }

        /// <summary>
        /// Data required to display in the flicker control. as of now only URl of the image is returned
        /// </summary>
        public ObservableCollection<ThumbnailData> ImageResponseURICollection
        {
            get => m_ImageResponseURICollection;
            set => m_ImageResponseURICollection = value;
        }

        #endregion View model binding Properties

        #region private methods


        /// <summary>
        /// From the resoponse from server, check if it is valid for flicker controls, if yes, assign appropriate data controls
        /// </summary>
        /// <param name="response"></param>
        private void PopulateFlickerDataFields(IResponseContext response)
        {
            if (response is ImageResponseDataModel respContext)
            {
                m_ImageResponseURICollection = new ObservableCollection<ThumbnailData>();

                foreach (string str in respContext.URI)
                {
                    ThumbnailData thumbnailData = new ThumbnailData {URI = str};
                    m_ImageResponseURICollection.Add(thumbnailData);
                }
                OnPropertyChange("ImageResponseURICollection");

                //Update any status - error etc
                m_Status = respContext.Status;
                OnPropertyChange("Status");

            }
        }

        /// <summary>
        /// Update thumbnail size based on selection
        /// </summary>
        private void UpdateImageSize()
        {
            string currentDisplayMode = CurrentThumbnailDisplayOption;
            int iSize = 128;//default
            switch (currentDisplayMode)
            {
                case ThumbnailDisplaySize.SMALL:
                    iSize = 128;
                    break;
                case ThumbnailDisplaySize.MEDIUM://small
                    iSize = 256;
                    break;
                case ThumbnailDisplaySize.LARGE://small
                    iSize = 512;
                    break;
                default:
                    break;
            }

            ImageSize.Height = iSize;
            ImageSize.Width = iSize;

            OnPropertyChange("ImageSize");
            //If image size is updated, reload the view for images in new size also
            OnPropertyChange("ImageResponseURICollection");
        }

        #endregion private methods

    }
}
