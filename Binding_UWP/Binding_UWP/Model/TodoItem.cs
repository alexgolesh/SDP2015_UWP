using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;

namespace Binding_UWP.Model
{
    public class TodoItem : Item
    {
        public string Title { get; set; }
        public string Body { get; set; }

        #region Compiled DataBinding
        public void Foo(object sender, RoutedEventArgs args)
        {
            Debug.WriteLine(string.Format("Item {0} was executed!", ID));
        }
        #endregion

        #region Classic DataBinding
        System.Windows.Input.ICommand barCommand;

        public System.Windows.Input.ICommand BarCommand
        {
            get
            {
                return barCommand;
            }
        }

        public TodoItem()
        {
            barCommand = new RelayCommand(new Action(doBar));
        }

        private void doBar()
        {
            Debug.WriteLine(string.Format("Item {0} was executed!", ID));
        }
        #endregion
    }
}
