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
using Android.Content.Res;
using MvvmCross.Binding.Droid.Target;
using Android.Support.V7.Widget;

namespace MusicStoreMobile.Droid.MvxBindings
{
    public class ValidationEditTextViewTargetBinding : MvxAndroidTargetBinding
    {
        public ValidationEditTextViewTargetBinding(EditText target) : base(target)
        {

        }

        public override Type TargetType => typeof(ColorStateList);

        protected override void SetValueImpl(object target, object value)
        {
            ((AppCompatEditText)target).SupportBackgroundTintList = (ColorStateList)value;
        }

    }
}