using System;
using Android.Content;
using Android.Content.Res;
using Android.Graphics;
using Android.Graphics.Drawables;
using Android.Support.V7.Widget;
using Android.Text;
using Android.Util;
using Android.Views;
using Android.Widget;
using Java.Lang;

namespace MusicStoreMobile.Droid.Controls
{
    public class DividerItemDecoration : RecyclerView.ItemDecoration
    {
        private Drawable mDivider;
        private bool mShowFirstDivider = false;
        private bool mShowLastDivider = false;


        public DividerItemDecoration(Context context, IAttributeSet attrs)
        {
            TypedArray a = context.ObtainStyledAttributes(attrs, new int[] { Android.Resource.Attribute.ListDivider });
            mDivider = a.GetDrawable(0);
            a.Recycle();
        }

        public DividerItemDecoration(Context context, IAttributeSet attrs, bool showFirstDivider, bool showLastDivider)
        {
            TypedArray a = context.ObtainStyledAttributes(attrs, new int[] { Android.Resource.Attribute.ListDivider });
            mDivider = a.GetDrawable(0);
            a.Recycle();

            mShowFirstDivider = showFirstDivider;
            mShowLastDivider = showLastDivider;
        }

        public DividerItemDecoration(Drawable divider)
        {
            mDivider = divider;
        }

        public DividerItemDecoration(Drawable divider, bool showFirstDivider, bool showLastDivider)
        {
            mDivider = divider;

            mShowFirstDivider = showFirstDivider;
            mShowLastDivider = showLastDivider;
        }

        public override void GetItemOffsets(Rect outRect, View view, RecyclerView parent, RecyclerView.State state)
        {
            base.GetItemOffsets(outRect, view, parent, state);

            if (mDivider == null)
            {
                return;
            }
            if (parent.GetChildPosition(view) < 1)
            {
                return;
            }

            if (getOrientation(parent) == LinearLayoutManager.Vertical)
            {
                outRect.Top = mDivider.IntrinsicHeight;
            }
            else
            {
                outRect.Left = mDivider.IntrinsicHeight;
            }
        }

        public override void OnDrawOver(Canvas c, RecyclerView parent, RecyclerView.State state)
        {
            if (mDivider == null)
            {
                base.OnDrawOver(c, parent, state);
                return;
            }

            // Initialization needed to avoid compiler warning
            int left = 0, right = 0, top = 0, bottom = 0, size;
            int orientation = getOrientation(parent);
            int childCount = parent.ChildCount;

            if (orientation == LinearLayoutManager.Vertical)
            {
                size = mDivider.IntrinsicHeight;
                left = parent.PaddingLeft;
                right = parent.Width - parent.PaddingRight;
            }
            else
            { //horizontal
                size = mDivider.IntrinsicWidth;
                top = parent.PaddingTop;
                bottom = parent.Height - parent.PaddingBottom;
            }

            for (int i = mShowFirstDivider ? 0 : 1; i < childCount; i++)
            {
                View child = parent.GetChildAt(i);
                RecyclerView.LayoutParams parametrs = (RecyclerView.LayoutParams)child.LayoutParameters;

                if (orientation == LinearLayoutManager.Vertical)
                {
                    top = child.Top - parametrs.TopMargin;
                    bottom = top + size;
                }
                else
                { //horizontal
                    left = child.Left - parametrs.LeftMargin;
                    right = left + size;
                }
                mDivider.SetBounds(left, top, right, bottom);
                mDivider.Draw(c);
            }

            // show last divider
            if (mShowLastDivider && childCount > 0)
            {
                View child = parent.GetChildAt(childCount - 1);
                RecyclerView.LayoutParams parametrs = (RecyclerView.LayoutParams)child.LayoutParameters;
                if (orientation == LinearLayoutManager.Vertical)
                {
                    top = child.Bottom + parametrs.BottomMargin;
                    bottom = top + size;
                }
                else
                { // horizontal
                    left = child.Right + parametrs.RightMargin;
                    right = left + size;
                }
                mDivider.SetBounds(left, top, right, bottom);
                mDivider.Draw(c);
            }
        }

        private int getOrientation(RecyclerView parent)
        {
            if (parent.GetLayoutManager() is LinearLayoutManager)
            {
                LinearLayoutManager layoutManager = (LinearLayoutManager)parent.GetLayoutManager();
                return layoutManager.Orientation;
            }
            else
            {
                throw new IllegalStateException("DividerItemDecoration can only be used with a LinearLayoutManager.");
            }
        }
    }
}