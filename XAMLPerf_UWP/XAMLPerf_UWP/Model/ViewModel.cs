using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XAMLPerf_UWP.Model
{
    public class ViewModel
    {

        public ViewModel()
        {
            for (int i = 0; i < 100; i++)
            {
                var item = new TodoItem()
                {
                    Title = string.Format("Todo Title {0}", i),
                    Body = string.Format("Body of item {0}", i),
                    Color = string.Format("FF00{0:D2}", i)
                };

                Items.Add(item);
            }
        }

        public ObservableCollection<TodoItem> Items { get; private set; } = new ObservableCollection<TodoItem>();

    }
}
