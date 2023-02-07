using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Sharpnado.MaterialFrame.Maui
{
    public class MaterialTabItem : ObservableViewModelBase
    {
        bool _selected;
        public string Title { get; set; }
        public ImageSource Icon { get; set; }
        public ICommand Command { get; set; }
        public object CommandParameter { get; set; }
        public MaterialContentPage Content { set; get; }

        public bool Selected {
            get => _selected;
            set => SetProperty(ref _selected, value);
        }
    }
}
