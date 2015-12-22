using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Binding_UWP.Model
{
    public class MainViewModel : INotifyPropertyChanged
    {
        public MainViewModel()
        {
            for (int i = 0; i < 100; i++)
            {
                var item = new TodoItem()
                {
                    ID = i,
                    Title = string.Format("Todo Title {0}", i),
                    Body = string.Format("Body of item {0}", i)
                };

                //var item2 = new NoteItem()
                //{
                //    ID = i,
                //    TheTitle = string.Format("Note Title {0}", i),
                //    TheBody = string.Format("Body of item {0}", i)
                //};


                Items.Add(item);
                //Items.Add(item2);

            }
        }

        public ObservableCollection<Item> Items { get; private set; } = new ObservableCollection<Item>();

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
