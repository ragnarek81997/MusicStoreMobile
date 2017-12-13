using MvvmCross.FieldBinding;
using System;
using System.Linq;
using System.Reflection;
using MvvmCross.Plugins.Validation;
using System.Collections.ObjectModel;
using System.Collections;
using System.Linq;

namespace MusicStoreMobile.Core.Validators
{
    public class NCFieldObservableCollectionRequiredValidation : IValidation
	{
		private readonly Func<IList, bool> _predicate;
		private string _message;
		private int _minimumCount;
        private int _maximumCount;

        public NCFieldObservableCollectionRequiredValidation(Func<IList, bool> predicate, int minimumCount, int maximumCount, string message)
		{
			_predicate = predicate;
            _minimumCount = minimumCount;
            _maximumCount = maximumCount;
            _message = message;
		}

		public IErrorInfo Validate(string fieldName, object value, object subject)
		{
			if (value == null)
				return null;

			var incValue = value.GetType().GetRuntimeProperties().FirstOrDefault(x => x.Name == "Value").GetValue(value);
			if (incValue == null)
				return null;

			return Validate(fieldName, incValue as IList, subject);
		}

		public IErrorInfo Validate(string fieldName, IList value, object subject)
		{
			if (!_predicate(value))
			{
				var fieldTitle = System.Text.RegularExpressions.Regex.Replace(fieldName, "(?<=.)([A-Z])", " $0", System.Text.RegularExpressions.RegexOptions.None);
				return new ErrorInfo(fieldName, _message == null ? string.Format("The Count of {0} must between {1} and {2}", fieldTitle, _minimumCount, _maximumCount) : string.Format(_message, fieldTitle, _minimumCount, _maximumCount) );
			}
			return null;
		}
	}
}
