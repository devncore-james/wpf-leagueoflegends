﻿using System.Windows;
using System.Windows.Controls;

namespace Lol.Main.UI.Units
{
    public class FriendsSortPopup : ContentControl
    {
        static FriendsSortPopup()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(FriendsSortPopup), new FrameworkPropertyMetadata(typeof(FriendsSortPopup)));
        }
    }
}
