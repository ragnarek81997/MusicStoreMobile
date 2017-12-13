using System;
using MvvmCross.Plugins.Validation;
using MvvmCross.Plugins.Validation.ForFieldBinding;

namespace MusicStoreMobile.Core.Validators
{
    public class NCFieldObservableCollectionRequiredAttribute : NCFieldValidationAttribute
	{
		private NCFieldObservableCollectionRequiredAttribute(string message = null) : base(message) { }

		public NCFieldObservableCollectionRequiredAttribute(int maximumCount, string message = null)
			: base(message)
		{
            MaximumCount = maximumCount;
		}

        public int MinimumCount { get; set; }
        public int MaximumCount { get; private set; }

		public override IValidation CreateValidation(Type valueType)
		{
			if (!valueType.FullName.Contains("MvvmCross.FieldBinding"))
				throw new NotSupportedException("NCFieldObservableCollectionRequired Validator for type " + valueType.Name + " is not supported.");

			return new NCFieldObservableCollectionRequiredValidation(collection =>
			{
				if (collection == null)
					return true;

				return collection.Count >= MinimumCount && collection.Count <= MaximumCount;
			}, MinimumCount, MaximumCount, Message);
		}
	}
}
