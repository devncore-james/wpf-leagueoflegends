﻿using System.Windows;
using System.Windows.Controls;

namespace Lol.TeamFight.UI.Units
{
    public class LevelControl : Control
    {
        static LevelControl()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(LevelControl), new FrameworkPropertyMetadata(typeof(LevelControl)));
        }
    }
}
