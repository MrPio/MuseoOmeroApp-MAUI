﻿using CommunityToolkit.Mvvm.ComponentModel;
using MuseoOmero.Managers;
using MuseoOmero.Resources.Material;

namespace MuseoOmero.ViewModel.Templates
{
    public partial class TopBarViewModel : ObservableObject
    {
        [ObservableProperty]
        MyEntryViewModel _myEntryViewModel =
            new("Ricerca", "", IconFont.Magnify, 0.9, DeviceManager.Instance.Colors[1], 1, 2.6, default, true);

        [ObservableProperty]
        string title = "Anagrafica";

        [ObservableProperty]
        double ricercaOpacity = 1, translationY = 0, opacity = 1;
    }
}
