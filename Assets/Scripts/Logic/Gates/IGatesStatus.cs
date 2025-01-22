using Scripts.Infrastructure.States;
using Scripts.Services.PersistentProgress;
using Scripts.UI.Services.Windows;

namespace Scripts.Logic.Gates
{
    public interface IGatesStatus
    {
        void UpdateStatus();
    }
}