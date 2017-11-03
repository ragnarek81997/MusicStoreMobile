using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.Views.InputMethods;
using Android.Graphics;
using MusicStoreMobile.Droid.Extensions;
using Android.Util;

namespace MusicStoreMobile.Droid.Helpers
{
    public class KeyboardUtils
    {
        public static void HideKeyboard(Activity activity)
        {
            if (activity?.CurrentFocus?.WindowToken == null)
                return;

            InputMethodManager keyboard = (InputMethodManager)activity.GetSystemService(Android.Content.Context.InputMethodService);
            keyboard.HideSoftInputFromWindow(activity.CurrentFocus.WindowToken, 0);
        }

        public static void ShowKeyboard(Activity activity)
        {
            if (activity?.CurrentFocus == null)
                return;
            InputMethodManager inputMethodManager = (InputMethodManager)(activity.GetSystemService(Android.Content.Context.InputMethodService));
            inputMethodManager.ShowSoftInput(activity.CurrentFocus, 0);
        }

        //public static void AddKeyboardVisibilityListener(Activity activity, IOnKeyboardVisibiltyListener onKeyboardVisibiltyListener)
        //{
        //    var rootLayout = activity?.Window?.DecorView?.RootView;

        //    if (rootLayout == null || onKeyboardVisibiltyListener == null || activity.Resources.Configuration.Orientation != Android.Content.Res.Orientation.Portrait)
        //        return;

        //    rootLayout.ViewTreeObserver.AddOnGlobalLayoutListener(new OnGlobalLayoutListener() { RootLayout = rootLayout, OnKeyboardVisibiltyListener = onKeyboardVisibiltyListener });
        //}

        //private class OnGlobalLayoutListener : Java.Lang.Object, ViewTreeObserver.IOnGlobalLayoutListener
        //{
        //    public View RootLayout { get; set; }
        //    public IOnKeyboardVisibiltyListener OnKeyboardVisibiltyListener { get; set; }

        //    private bool? _isVisibleState { get; set; }

        //    public void OnGlobalLayout()
        //    {
        //        Rect r = new Rect();
        //        RootLayout.GetWindowVisibleDisplayFrame(r);
        //        int screenHeight = RootLayout.RootView.Height;

        //        // r.bottom is the position above soft keypad or device button.
        //        // if keypad is shown, the r.bottom is smaller than that before.
        //        int keypadHeight = screenHeight - r.Bottom;

        //        bool isVisible = keypadHeight > screenHeight * 0.15; // 0.15 ratio is perhaps enough to determine keypad height.
        //        if (_isVisibleState == null || _isVisibleState.Value != isVisible)
        //        {
        //            _isVisibleState = new bool?(isVisible);
        //            OnKeyboardVisibiltyListener.OnVisibilityChange(isVisible);
        //        }
        //    }
        //}

        //public interface IOnKeyboardVisibiltyListener
        //{
        //    void OnVisibilityChange(bool isVisible);
        //}
    }
}