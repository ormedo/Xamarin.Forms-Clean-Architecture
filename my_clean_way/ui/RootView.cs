using System;

using Xamarin.Forms;

namespace my_clean_way.ui
{
    public class RootView : ContentPage
    {
        public RootView()
        {
            Content = new StackLayout
            {
                Children = {
                    new Label { Text = "Hello ContentPage" }
                }
            };
        }
    }
}

