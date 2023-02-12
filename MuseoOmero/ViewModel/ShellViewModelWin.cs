using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MuseoOmero.ViewModelWin
{
    public partial class ShellViewModelWin : ObservableObject
    {
        public int CurrentIndex = 0;
        //[ObservableProperty]
        //ImageSource homeIcon = ImageSource.FromFile("home_empty.png");

        //[ObservableProperty]
        //ImageSource negozioIcon = ImageSource.FromFile("cart_empty.png");

        //[ObservableProperty]
        //ImageSource iMieiTitoliIcon = ImageSource.FromFile("ticket_empty.png");

        //[ObservableProperty]
        //ImageSource opereIcon = ImageSource.FromFile("photo_empty.png");

        //[ObservableProperty]
        //ImageSource appuntamentiIcon = ImageSource.FromFile("calendar_empty.png");


        //public void ChangeIndex(int newIndex)
        //{
        //    if (newIndex == 0)
        //    {
        //        HomeIcon = ImageSource.FromFile("home_fill.png");
        //        NegozioIcon = ImageSource.FromFile("cart_empty.png");
        //        IMieiTitoliIcon = ImageSource.FromFile("ticket_empty.png");
        //        OpereIcon = ImageSource.FromFile("photo_empty.png");
        //        AppuntamentiIcon = ImageSource.FromFile("calendar_empty.png");
        //    }
        //    else if (newIndex == 1)
        //    {
        //        HomeIcon = ImageSource.FromFile("home_empty.png");
        //        NegozioIcon = ImageSource.FromFile("cart_empty.png");
        //        IMieiTitoliIcon = ImageSource.FromFile("ticket_fill.png");
        //        OpereIcon = ImageSource.FromFile("photo_empty.png");
        //        AppuntamentiIcon = ImageSource.FromFile("calendar_empty.png");
        //    }
        //    else if (newIndex == 2)
        //    {
        //        HomeIcon = ImageSource.FromFile("home_empty.png");
        //        NegozioIcon = ImageSource.FromFile("cart_fill.png");
        //        IMieiTitoliIcon = ImageSource.FromFile("ticket_empty.png");
        //        OpereIcon = ImageSource.FromFile("photo_empty.png");
        //        AppuntamentiIcon = ImageSource.FromFile("calendar_empty.png");
        //    }
        //    else if (newIndex == 3)
        //    {
        //        HomeIcon = ImageSource.FromFile("home_empty.png");
        //        NegozioIcon = ImageSource.FromFile("cart_empty.png");
        //        IMieiTitoliIcon = ImageSource.FromFile("ticket_empty.png");
        //        OpereIcon = ImageSource.FromFile("photo_fill.png");
        //        AppuntamentiIcon = ImageSource.FromFile("calendar_empty.png");
        //    }
        //    else if (newIndex == 4)
        //    {
        //        HomeIcon = ImageSource.FromFile("home_empty.png");
        //        NegozioIcon = ImageSource.FromFile("cart_empty.png");
        //        IMieiTitoliIcon = ImageSource.FromFile("ticket_empty.png");
        //        OpereIcon = ImageSource.FromFile("photo_empty.png");
        //        AppuntamentiIcon = ImageSource.FromFile("calendar_fill.png");
        //    }
        //}
        public void ChangeIndex(int newIndex)
        {
            CurrentIndex= newIndex;
        }
    }
}