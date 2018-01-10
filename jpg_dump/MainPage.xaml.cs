using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;
using Windows.Storage.Pickers;
using Windows.Storage.FileProperties;
using Windows.Storage;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace jpg_dump
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        StorageFile imageFile;
        string title;
        DateTimeOffset date;
        ushort orientation;
        double aperture;
        double? latitude;
        double? longitude;

        public MainPage()
        {
            this.InitializeComponent();

            if (image1.Source != null)
            {
                SetSource();
                GetImageProperties();
                FillText();
            }
        }

        private void SetSource()
        {
            var image = new Image();
            var bitmapImage = new BitmapImage();
            bitmapImage.UriSource = new Uri(image.BaseUri, "TestImages/IMG_2731.jpg");
            image1.Source = bitmapImage;
        }

        private async void button1_Click(object sender, RoutedEventArgs e)
        {
            FileOpenPicker imageOpenPicker = new FileOpenPicker();
            imageOpenPicker.ViewMode = PickerViewMode.Thumbnail;
            imageOpenPicker.SuggestedStartLocation = PickerLocationId.PicturesLibrary;
            imageOpenPicker.FileTypeFilter.Add(".jpg");
            imageOpenPicker.FileTypeFilter.Add(".png");
            
            // Pick one file to display
            imageFile = await imageOpenPicker.PickSingleFileAsync();
            if (imageFile != null)
            {
                var fileStream = await imageFile.OpenAsync(FileAccessMode.Read);
                var bitmapImage = new BitmapImage();
                bitmapImage.SetSource(fileStream);
                image1.Source = bitmapImage;
            }

            GetImageProperties();
            FillText();
        }

        private async void GetImageProperties()
        {
            ImageProperties props = await imageFile.Properties.GetImagePropertiesAsync();

            var requests = new System.Collections.Generic.List<string>();
            //requests.Add("System.Photo.Title");
            //requests.Add("System.Photo.DateTaken");
            requests.Add("System.Photo.Orientation");
            requests.Add("System.Photo.Aperture");
            //requests.Add("System.Photo.Latitude");
            //requests.Add("System.Photo.Longitude");

            IDictionary<string, object> retrievedProps = await props.RetrievePropertiesAsync(requests);

            //string title;
            //if (retrievedProps.ContainsKey("System.Photo.Title"))
            //{
            //title = (string)retrievedProps["System.Photo.Title"];
            //}

            title = props.Title;
            if (title == null)
            {
                title = "Title not supported";
            }

            //DateTimeOffset date;
            //if (retrievedProps.ContainsKey("System.Photo.DateTaken"))
            //{
            // date = (DateTimeOffset)retrievedProps["System.Photo.DateTaken"];
            //}
            date = props.DateTaken;
            if (date == null)
            {
                //
            }

            //ushort orientation;
            if (retrievedProps.ContainsKey("System.Photo.Orientation"))
            {
                orientation = (ushort)retrievedProps["System.Photo.Orientation"];
            }

            //double aperture;
            if (retrievedProps.ContainsKey("System.Photo.Aperture"))
            {
                aperture = (double)retrievedProps["System.Photo.Aperture"];
            }

            //if (retrievedProps.ContainsKey("System.Photo.Latitude"))
            //{
            //    latitude = (double)retrievedProps["System.Photo.Latitude"];
            //}
            latitude = props.Latitude;


            //if (retrievedProps.ContainsKey("System.Photo.Longitude"))
            //{
            //longitude = (double)retrievedProps["System.Photo.Longitude"];
            //}
            longitude = props.Longitude;

        }

        private void FillText()
        {
            StringBuilder metadataText = new StringBuilder();
            metadataText.AppendLine("Title: " + title);
            metadataText.AppendLine("Date: " + date);
            metadataText.AppendLine("Orientation: " + orientation);
            metadataText.AppendLine("Aperture: " + aperture);
            metadataText.AppendLine("Latitude: " + latitude);
            metadataText.AppendLine("Longitude: " + longitude);

            textBlock.Text = metadataText.ToString();
        }

        void OnNavigationFailed(object sender, NavigationFailedEventArgs e)
        {
            throw new Exception("Failed to load Page " + e.SourcePageType.FullName);
        }

        
    }
}
