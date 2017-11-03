using Android.Runtime;
using Android.Views;

namespace MusicStoreMobile.Droid.Controls
{
    public interface IKeyEventListener
    {
        bool OnKeyUp([GeneratedEnum] Keycode keyCode, KeyEvent e);
        bool OnKeyDown([GeneratedEnum] Keycode keyCode, KeyEvent e);
        bool OnKeyLongPress([GeneratedEnum] Keycode keyCode, KeyEvent e);
        bool OnKeyMultiple([GeneratedEnum] Keycode keyCode, int repeatCount, KeyEvent e);
        bool OnKeyShortcut([GeneratedEnum] Keycode keyCode, KeyEvent e);
    }
}