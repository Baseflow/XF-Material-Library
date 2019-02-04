using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Xamarin.Forms;

namespace XF.Material.Forms.UI.Converters
{
    public class ChoiceConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if(parameter == null)
            {
                return new ArgumentNullException(nameof(parameter));
            }

            if(value is IEnumerable<object> choices && choices.Any())
            {
                var choiceStrings = new List<string>(choices.Count());
                var listType = choices.FirstOrDefault().GetType();
                foreach(var item in choices)
                {
                    var propInfo = listType.GetProperty(parameter.ToString());

                    if(propInfo == null)
                    {
                        System.Diagnostics.Debug.WriteLine($"Property {parameter} was not found for item in {value}.");
                        choiceStrings.Add(item.ToString());
                    }
                    else
                    {
                        var propValue = propInfo.GetValue(item);
                        choiceStrings.Add(propValue.ToString());
                    }
                }

                return choiceStrings;
            }

            throw new InvalidOperationException("The value to convert is not a collection");
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value;
        }
    }
}
