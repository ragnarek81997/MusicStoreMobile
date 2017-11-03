using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using MvvmCross.Plugins.Validation;

namespace MusicStoreMobile.Core.Services.Implementations
{
    public class Validator : MusicStoreMobile.Core.Services.Interfaces.IValidator
	{
		static readonly object SyncLock = new object();
		static readonly Dictionary<Type, Tuple<IValidationCollection, ISet<string>>> ValidationCache = new Dictionary<Type, Tuple<IValidationCollection, ISet<string>>>();

		private Tuple<IValidationCollection, ISet<string>> InitializeTuple(object subject)
		{
			Tuple<IValidationCollection, ISet<string>> tuple;
			var type = subject.GetType();
			lock (SyncLock)
			{
				if (!ValidationCache.TryGetValue(type, out tuple))
				{
					tuple = BuildTupleFor(type);
					ValidationCache[type] = tuple;
				}
			}
			return tuple;
		}

		public IValidationCollection InitializeCollection(object subject)
		{
            var result = InitializeTuple(subject);
            return result?.Item1;
		}

		public ISet<string> InitializeSet(object subject)
		{
			var result = InitializeTuple(subject);
			return result?.Item2;
		}

		private Tuple<IValidationCollection, ISet<string>> BuildTupleFor(Type type)
		{
			var validationCollection = new ValidationCollection();
            var validationSet = new HashSet<string>();

			var properties = type.GetRuntimeProperties();
			foreach (var propertyInfo in properties)
			{
				var attributes = propertyInfo.GetCustomAttributes(true).OfType<ValidationAttribute>().ToArray();
				foreach (var validationAttribute in attributes)
				{
					validationCollection.Add(new ValidationInfo(propertyInfo, validationAttribute.CreateValidation(propertyInfo.PropertyType), validationAttribute.Groups));
                    validationSet.Add(propertyInfo.Name);
				}
			}
			var fields = type.GetRuntimeFields();
			foreach (var fieldInfo in fields)
			{
				var attributes = fieldInfo.GetCustomAttributes(true).OfType<ValidationAttribute>().ToArray();
				foreach (var validationAttribute in attributes)
				{
					validationCollection.Add(new ValidationInfo(fieldInfo, validationAttribute.CreateValidation(fieldInfo.FieldType), validationAttribute.Groups));
                    validationSet.Add(fieldInfo.Name);
				}
			}
			return new Tuple<IValidationCollection, ISet<string>>(validationCollection, validationSet);
		}

		public IErrorCollection Validate(object subject, string group = null)
		{
			var collection = InitializeCollection(subject);
			var result = collection
				.Where(c => c.Groups.IsNullOrEmpty() || c.Groups.Contains(group))
				.Select(c => c.Validation.Validate(c.Member.Name, (c.Member as PropertyInfo)?.GetValue(subject, null) ?? (c.Member as FieldInfo)?.GetValue(subject), subject))
				.Where(t => t != null);
			return new ErrorCollection(result.ToList());
		}

		public IErrorCollection Validate<T>(Expression<Func<T>> propertyExpression, string group = null)
		{
            var subject = ((propertyExpression.Body as MemberExpression).Expression as ConstantExpression).Value;

			var collection = InitializeCollection(subject);
			
            var result = collection
                .Where(c => (c.Groups.IsNullOrEmpty() || c.Groups.Contains(group)) && c.Member.Name == (propertyExpression.Body as MemberExpression).Member.Name )
				.Select(c => c.Validation.Validate(c.Member.Name, (c.Member as PropertyInfo)?.GetValue(subject, null) ?? (c.Member as FieldInfo)?.GetValue(subject), subject))
				.Where(t => t != null);
			return new ErrorCollection(result.ToList());
		}

		public bool IsRequired(object subject, string memberName)
		{
			return InitializeCollection(subject).Any(v => v.Member.Name == memberName && HasRequiredValidator(v.Validation.GetType()));
		}

		private bool HasRequiredValidator(Type validationType)
		{
			var isGenericType = validationType.IsConstructedGenericType;
			if (isGenericType)
				return validationType.GetGenericTypeDefinition() == typeof(RequiredValidation<>);
			return false;
		}
	}
}
