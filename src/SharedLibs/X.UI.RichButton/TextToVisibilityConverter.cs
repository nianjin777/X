﻿using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Data;

namespace X.UI.RichButton
{

    public class TextToVisibilityConverter : IValueConverter
    {
   
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            var val = System.Convert.ToString(value);


            if (string.IsNullOrEmpty(val))
            {
                return Visibility.Collapsed;
            }

            return Visibility.Visible;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }

}
