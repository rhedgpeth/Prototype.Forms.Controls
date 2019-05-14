using System;
using System.Collections.Generic;
using System.Windows.Input;
using Xamarin.Forms;

namespace PrototypeControlsSample.ViewModels
{
    public class RadioButtonListViewModel : BaseNotify
    {
        public List<string> Items => new List<string>{ "Item 1", "Item 2", "Item 3" };

        ICommand _itemSelectedCommand;
        public ICommand ItemSelectedCommand
        {
            get
            {
                if (_itemSelectedCommand == null)
                {
                    _itemSelectedCommand = new Command<int>((index) => Console.WriteLine($"RadioButtonList Item Selected = {index}"));
                }

                return _itemSelectedCommand;
            }
        }   
    }
}
