using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
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
using Windows.Storage;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace jpg_dump
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
            imageOpenPicker = new FileOpenPicker();
        }

        private async void button1_Click(object sender, RoutedEventArgs e)
        {
            imageOpenPicker.ViewMode = PickerViewMode.Thumbnail;
            imageOpenPicker.SuggestedStartLocation = PickerLocationId.PicturesLibrary;
            imageOpenPicker.FileTypeFilter.Add(".jpg");
            imageOpenPicker.FileTypeFilter.Add(".png");

            // Pick one file to display
            StorageFile selectedFile = await imageOpenPicker.PickSingleFileAsync();
            if (selectedFile != null)
            {
                var stream = await selectedFile.OpenAsync(FileAccessMode.Read);
                var image = new BitmapImage();
                image.SetSource(stream);
                image1.Source = image;
            }
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            if (typeof(BlankPage1).ToString() == "")
            {
                Debug.WriteLine("No blank page"); 
            }

            Frame.Navigate(typeof(BlankPage1), null);
        }

        //protected override void OnNavigatedTo(NavigationEventArgs e)
        //{
        //    this.Frame.Navigate(typeof(ContentDialog1), nu);
        //}

        void OnNavigationFailed(object sender, NavigationFailedEventArgs e)
        {
            throw new Exception("Failed to load Page " + e.SourcePageType.FullName);
        }

        FileOpenPicker imageOpenPicker;
    }
}
