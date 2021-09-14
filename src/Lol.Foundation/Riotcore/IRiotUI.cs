﻿using Lol.Data.Main;
using Lol.Foundation.Mvvm;

namespace Lol.Foundation.Riotcore
{
    public interface IRiotUI
    {
        IRiotUI SetVM(ObservableObject vm);
        void Show(SubMenuModel menu);
    }
}
