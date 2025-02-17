﻿using Scripts.Services;
using Scripts.UI.Windows.Menu;

namespace Scripts.UI.Services.Windows
{
    public interface IWindowService : IService
    {
        void Open(WindowId windowId);

        //Need here???
        ChoiceWindow ChoiceWindow { get; set; }
        ConfimWindow ConfimWindow { get; set; }
        PauseMenu PauseMenu { get; set; }

        DetailedViewAbility DetailedViewAbility { get; set; }

    }
}