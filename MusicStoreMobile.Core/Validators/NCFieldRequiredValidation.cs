using MvvmCross.FieldBinding;
using MvvmCross.Plugins.Validation;
using System;
using System.Collections.ObjectModel;
using System.Reflection;

namespace MusicStoreMobile.Core.Validators
{
    public class NCFieldRequiredValidation<T> : IValidation<T>
    {
        private readonly Func<T, bool> _predicate;
        private string _message;

        public NCFieldRequiredValidation(Func<T, bool> predicate, string message)
        {
            _predicate = predicate;
            _message = message;
        }

        public IErrorInfo Validate(string fieldName, object value, object subject)
        {
            if (value != null && value.GetType().GenericTypeArguments?[0] != null && value.GetType().GenericTypeArguments[0].GetTypeInfo().IsGenericType)
            {
                var genericType = value.GetType().GenericTypeArguments[0];

                var a = genericType.GetGenericTypeDefinition().Equals(typeof(Nullable<>));
                if (a)
                {
                    return Validate(fieldName, ((INC<T>)value).Value, subject);
                }
            }

            return Validate(fieldName, (T)value, subject);
        }

        public IErrorInfo Validate(string fieldName, T value, object subject)
        {
            if (!_predicate(value))
            {
                var fieldTitle = System.Text.RegularExpressions.Regex.Replace(fieldName, "(?<=.)([A-Z])", " $0", System.Text.RegularExpressions.RegexOptions.None);
                return new ErrorInfo(fieldName, _message == null ? string.Format("{0} is Required", fieldTitle) : string.Format(_message, fieldTitle));
            }
            return null;
        }
    }
}
