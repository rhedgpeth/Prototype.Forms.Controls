using System;
using System.Collections.Generic;

namespace PrototypeControlsSample.ViewModels
{
    public class MenuViewModel : BaseNotify
    {
        public List<string> Items { get; set; } 

        public MenuViewModel() => LoadItems();

        void LoadItems()
        {
            Items = new List<string>
            {
                "Home",
                "Checkbox",
                "CheckboxList",
                "EditableLabel",
                "ListView",
                "RadioButton",
                "RadioButtonList",
                "TappableLabel"
            };
        }
    }
}
