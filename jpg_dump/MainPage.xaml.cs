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
        }

        private async void button1_Click(object sender, RoutedEventArgs e)
        {
            FileOpenPicker imageOpenPicker = new FileOpenPicker();
            imageOpenPicker.ViewMode = PickerViewMode.Thumbnail;
            imageOpenPicker.SuggestedStartLocation = PickerLocationId.PicturesLibrary;
            imageOpenPicker.FileTypeFilter.Add(".jpg");
            imageOpenPicker.FileTypeFilter.Add(".png");
            
            // Pick one file to display
            StorageFile selectedFile = await imageOpenPicker.PickSingleFileAsync();
            if (selectedFile != null)
            {
                var fileStream = await selectedFile.OpenAsync(FileAccessMode.Read);
                var bitmapImage = new BitmapImage();
                bitmapImage.SetSource(fileStream);
                image1.Source = bitmapImage;
            }
        }

        void OnNavigationFailed(object sender, NavigationFailedEventArgs e)
        {
            throw new Exception("Failed to load Page " + e.SourcePageType.FullName);
        }

        
    }
}
