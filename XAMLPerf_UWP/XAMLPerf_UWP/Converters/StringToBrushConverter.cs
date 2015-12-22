using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Windows.UI;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Media;

namespace XAMLPerf_UWP.Converters
{
    public class StringToBrushConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            string val = value.ToString();
            var brush = new SolidColorBrush(Color.FromArgb(255, byte.Parse(val.Substring(0, 2), System.Globalization.NumberStyles.HexNumber), 
                                                                byte.Parse(val.Substring(2, 2), System.Globalization.NumberStyles.HexNumber), 
                                                                byte.Parse(val.Substring(4, 2), System.Globalization.NumberStyles.HexNumber)));

            using (EventWaitHandle tmpEvent = new ManualResetEvent(false))
                tmpEvent.WaitOne(TimeSpan.FromMilliseconds(100));

            return brush;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
