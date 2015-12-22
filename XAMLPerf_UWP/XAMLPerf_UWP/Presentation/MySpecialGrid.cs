using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;

namespace XAMLPerf_UWP.Presentation
{
    public class MySpecialGrid : Grid
    {
        private static int counter = 0;

        public MySpecialGrid()
        {
            this.Height = this.Width = 100;
            this.Margin = new Thickness(10);
            this.Background = new SolidColorBrush(Colors.SteelBlue);

            Debug.WriteLine(string.Format("Created custom grid #{0}", counter++));
        }
    }
}
