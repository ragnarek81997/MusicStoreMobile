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
using MvvmCross.Droid.Support.V7.RecyclerView;
using Android.Util;
using Android.Graphics.Drawables;
using Android.Support.V7.Widget;
using MvvmCross.Binding.Droid.BindingContext;
using MvvmCross.Binding.BindingContext;
using DividerItemDecoration = MusicStoreMobile.Droid.Controls.DividerItemDecoration;
using MusicStoreMobile.Droid.Extensions;

namespace MusicStoreMobile.Droid.Controls
{
    [Register("mvvmcross.droid.support.v7.recyclerview.TransparentDividedRecyclerView")]
    public sealed class TransparentDividedRecyclerView : MvxRecyclerView
    {
        public TransparentDividedRecyclerView(Context context, IAttributeSet attrs) : this(context, attrs, 0, new MvxRecyclerAdapter())
        {
        }

        public TransparentDividedRecyclerView(Context context, IAttributeSet attrs, int defStyle) : this(context, attrs, defStyle, new MvxRecyclerAdapter())
        {
        }

        public TransparentDividedRecyclerView(Context context, IAttributeSet attrs, int defStyle, IMvxRecyclerAdapter adapter) : base(context, attrs, defStyle, adapter)
        {
            Setup(context);
        }

        private void SetDivider(Context context, Drawable drawable)
        {
            LinearLayoutManager linearLayoutManager = new LinearLayoutManager(context);
            this.SetLayoutManager(linearLayoutManager);
            this.AddItemDecoration(new DividerItemDecoration(drawable, false, false));
        }

        private void Setup(Context context)
        {
            LinearLayoutManager linearLayoutManager = new LinearLayoutManager(context);
            this.SetLayoutManager(linearLayoutManager);

            SetDivider(context, AppCompatExtensions.GetDrawable(Resource.Drawable.transparent_divider_shape).Mutate());
        }
    }

    [Register("mvvmcross.droid.support.v7.recyclerview.LineDividedRecyclerView")]
    public sealed class LineDividedRecyclerView : MvxRecyclerView
    {
        public LineDividedRecyclerView(Context context, IAttributeSet attrs) : this(context, attrs, 0, new MvxRecyclerAdapter())
        {
        }

        public LineDividedRecyclerView(Context context, IAttributeSet attrs, int defStyle) : this(context, attrs, defStyle, new MvxRecyclerAdapter())
        {
        }

        public LineDividedRecyclerView(Context context, IAttributeSet attrs, int defStyle, IMvxRecyclerAdapter adapter) : base(context, attrs, defStyle, adapter)
        {
            Setup(context);
        }

        private void SetDivider(Context context, Drawable drawable)
        {
            LinearLayoutManager linearLayoutManager = new LinearLayoutManager(context);
            this.SetLayoutManager(linearLayoutManager);
            this.AddItemDecoration(new DividerItemDecoration(drawable, false, false));
        }

        private void Setup(Context context)
        {
            LinearLayoutManager linearLayoutManager = new LinearLayoutManager(context);
            this.SetLayoutManager(linearLayoutManager);

            SetDivider(context, AppCompatExtensions.GetDrawable(Resource.Drawable.line_divider_shape).Mutate());
        }
    }
}