using MusicStoreMobile.Core.Controls;
using MusicStoreMobile.Core.Helpers.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using System.Linq.Expressions;
using MvvmCross.FieldBinding;
using MusicStoreMobile.Core.Services.Interfaces;
using MvvmCross.Plugins.Validation;

namespace MusicStoreMobile.Core.Helpers.Implementations
{
    class ValidationHelper : IValidationHelper
    {
        private readonly Services.Interfaces.IValidator _validator;

        private object _subject { get; set; }
        private ObservableDictionary<string, string> _errorsDictionary { get; set; }
        private Action<string> _requestFocusAction { get; set; }

        public ValidationHelper(Services.Interfaces.IValidator validator, object subject, ObservableDictionary<string, string> errorsDictionary, Action<string> requestFocusAction = null)
        {
            _validator = validator;
            _subject = subject;
            _errorsDictionary = errorsDictionary;
            _requestFocusAction = requestFocusAction;

            ResetValidation();
        }



        #region reset validation

        public bool ResetValidation()
        {
            if (_subject == null || _errorsDictionary == null) return false;

            var validationPropertysSet = _validator.InitializeSet(_subject);
            if(validationPropertysSet == null) return false;

            foreach(var item in validationPropertysSet)
            {
                _errorsDictionary[item] = "";
            }
            return true;
        }

		#endregion reset validation

		#region validate model

		public bool Validate()
		{
			//inits
			if (_subject == null || _errorsDictionary == null)
			{
				return false;
			}
			var focusProperty = "";

			var isValid = true;

			//create set from clear dictionary in end
			var deleteSet = new HashSet<string>();
			foreach (var item in _errorsDictionary)
			{
				deleteSet.Add(item.Key);
			}

			//get validation collection
			var validatorResult = _validator.Validate(_subject);

			foreach (var item in validatorResult)
			{
				if (deleteSet.Contains(item.MemberName) || !_errorsDictionary.ContainsKey(item.MemberName))
				{
                    //update error message
                    _errorsDictionary[item.MemberName] = item.Message;

                    //not delete this error in end
                    deleteSet.Remove(item.MemberName);

                    //is validate all request focus in first error property
                    if (isValid)
					{
						focusProperty = item.MemberName;
					}
					isValid = false;
				}
			}

			foreach (var item in deleteSet)
			{
				_errorsDictionary.Remove(item);
			}

			if (isValid)
			{
				focusProperty = "";
			}
			_requestFocusAction?.Invoke(focusProperty);

			return isValid;
		}

		public bool Validate<T>(Expression<Func<T>> propertyExpression)
		{
			//inits
			if (_subject == null || _errorsDictionary == null || propertyExpression == null)
			{
				return false;
			}
			var propertyName = "";
			if (propertyExpression != null)
			{
				propertyName = (propertyExpression.Body as MemberExpression).Member.Name;
			}
			if (string.IsNullOrWhiteSpace(propertyName))
			{
				return false;
			}
			var isValid = true;

			//create set from clear dictionary in end
			var deleteSet = new HashSet<string>();
			deleteSet.Add(propertyName);

			//get validation collection
			var validatorResult = _validator.Validate(propertyExpression);

			foreach (var item in validatorResult)
			{
				if (deleteSet.Contains(item.MemberName) || !_errorsDictionary.ContainsKey(item.MemberName))
				{
                    //update error message
                    _errorsDictionary[item.MemberName] = item.Message;

                    //not delete this error in end
                    deleteSet.Remove(item.MemberName);

                    isValid = false;
					break;
				}
			}

			foreach (var item in deleteSet)
			{
				_errorsDictionary.Remove(item);
			}

			return isValid;
		}

		#endregion validate model
	}
}
