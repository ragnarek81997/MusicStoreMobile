using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Graphics;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;

namespace MusicStoreMobile.Droid.Controls
{
    public class SelectedTextView : TextView
    {
        public SelectedTextView(Context context) : base(context)
        {
        }

        public SelectedTextView(Context context, IAttributeSet attrs) : base(context, attrs)
        {
        }

        public SelectedTextView(Context context, IAttributeSet attrs, int defStyleAttr) : base(context, attrs, defStyleAttr)
        {
        }

        public SelectedTextView(Context context, IAttributeSet attrs, int defStyleAttr, int defStyleRes) : base(context, attrs, defStyleAttr, defStyleRes)
        {
        }

        protected SelectedTextView(IntPtr javaReference, JniHandleOwnership transfer) : base(javaReference, transfer)
        {
        }
    }
}