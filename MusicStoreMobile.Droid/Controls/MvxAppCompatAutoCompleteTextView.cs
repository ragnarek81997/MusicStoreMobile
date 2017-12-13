﻿using System;
using System.Collections;
using Android.Content;
using Android.Runtime;
using Android.Util;
using MvvmCross.Binding.Attributes;
using MvvmCross.Binding.Droid.Views;
using Android.Support.V7.Widget;
using Android.Widget;

namespace MusicStoreMobile.Droid.Controls
{
    [Register("mvvmcross.binding.droid.views.MvxAppCompatAutoCompleteTextView")]
    public class MvxAppCompatAutoCompleteTextView : AppCompatAutoCompleteTextView
    {
        public MvxAppCompatAutoCompleteTextView(Context context, IAttributeSet attrs)
            : this(context, attrs, new MvxFilteringAdapter(context))
        {
        }

        public MvxAppCompatAutoCompleteTextView(Context context, IAttributeSet attrs,
                                       MvxFilteringAdapter adapter)
            : base(context, attrs)
        {
            var itemTemplateId = MvxAttributeHelpers.ReadListItemTemplateId(context, attrs);
            if (itemTemplateId > 0)
                adapter.ItemTemplateId = itemTemplateId;

            Adapter = adapter;

            // note - we shouldn't realy need both of these... but we do
            ItemClick += OnItemClick;
            ItemSelected += OnItemSelected;
        }

        protected MvxAppCompatAutoCompleteTextView(IntPtr javaReference, JniHandleOwnership transfer)
            : base(javaReference, transfer)
        {
        }

        private void OnItemClick(object sender, AdapterView.ItemClickEventArgs itemClickEventArgs)
        {
            OnItemClick(itemClickEventArgs.Position);
        }

        private void OnItemSelected(object sender, AdapterView.ItemSelectedEventArgs itemSelectedEventArgs)
        {
            OnItemSelected(itemSelectedEventArgs.Position);
        }

        protected virtual void OnItemClick(int position)
        {
            var selectedObject = Adapter.GetRawItem(position);
            SelectedObject = selectedObject;
        }

        protected virtual void OnItemSelected(int position)
        {
            var selectedObject = Adapter.GetRawItem(position);
            SelectedObject = selectedObject;
        }

        public new MvxFilteringAdapter Adapter
        {
            get
            {
                return base.Adapter as MvxFilteringAdapter;
            }
            set
            {
                var existing = Adapter;
                if (existing == value)
                    return;

                if (existing != null)
                    existing.PartialTextChanged -= AdapterOnPartialTextChanged;

                if (existing != null && value != null)
                {
                    value.ItemsSource = existing.ItemsSource;
                    value.ItemTemplateId = existing.ItemTemplateId;
                }

                if (value != null)
                    value.PartialTextChanged += AdapterOnPartialTextChanged;

                base.Adapter = value;

                if (existing != null)
                    existing.ItemsSource = null;
            }
        }

        private void AdapterOnPartialTextChanged(object sender, EventArgs eventArgs)
        {
            FireChanged(PartialTextChanged);
        }

        [MvxSetToNullAfterBinding]
        public IEnumerable ItemsSource
        {
            get { return Adapter.ItemsSource; }
            set { Adapter.ItemsSource = value; }
        }

        public int ItemTemplateId
        {
            get { return Adapter.ItemTemplateId; }
            set { Adapter.ItemTemplateId = value; }
        }

        public string PartialText => Adapter.PartialText;

        private object _selectedObject;

        public object SelectedObject
        {
            get
            {
                return _selectedObject;
            }
            private set
            {
                if (_selectedObject == value)
                    return;

                _selectedObject = value;
                FireChanged(SelectedObjectChanged);
            }
        }

        public event EventHandler SelectedObjectChanged;

        public event EventHandler PartialTextChanged;

        private void FireChanged(EventHandler eventHandler)
        {
            eventHandler?.Invoke(this, EventArgs.Empty);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                ItemClick -= OnItemClick;
                ItemSelected -= OnItemSelected;

                if (Adapter != null)
                {
                    Adapter.PartialTextChanged -= AdapterOnPartialTextChanged;
                }
            }
            base.Dispose(disposing);
        }
    }
}