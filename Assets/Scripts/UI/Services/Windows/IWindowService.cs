﻿using Scripts.Services;
using UnityEngine;

namespace Scripts.UI.Services.Windows
{
    public interface IWindowService : IService
    {
        void Open(WindowId windowId);
    }
}