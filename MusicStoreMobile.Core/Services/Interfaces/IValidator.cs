using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using MvvmCross.Plugins.Validation;

namespace MusicStoreMobile.Core.Services.Interfaces
{
	public interface IValidator
	{
        ISet<string> InitializeSet(object subject);
		IErrorCollection Validate(object subject, string group = null);
        IErrorCollection Validate<T>(Expression<Func<T>> propertyExpression, string group = null);
		bool IsRequired(object subject, string memberName);
	}
}
