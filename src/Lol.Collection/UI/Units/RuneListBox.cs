﻿using System.Windows;
using System.Windows.Controls;

namespace Lol.Collection.UI.Units
{
    public class RuneListBox : ListBox
    {
        #region DefaultStyleKey

        static RuneListBox()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(RuneListBox), new FrameworkPropertyMetadata(typeof(RuneListBox)));
        }
        #endregion

        protected override DependencyObject GetContainerForItemOverride()
        {
            return new RuneListBoxItem();
        }
    }
}
