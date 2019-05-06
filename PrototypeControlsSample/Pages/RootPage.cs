using System;
using Xamarin.Forms;

namespace PrototypeControlsSample.Pages
{
    public class RootPage : MasterDetailPage
    {
        public RootPage()
        {
            Detail = new NavigationPage(new HomePage());
            Master = new MenuPage(SetDetail);
        }

        void SetDetail(Type type)
        {
            if (Activator.CreateInstance(type) is Page page)
            {
                Detail = new NavigationPage(page);
                IsPresented = false;
            }
        }
    }
}

