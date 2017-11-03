using MusicStoreMobile.Core.Controls;
using Nito.AsyncEx;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Linq.Expressions;

namespace MusicStoreMobile.Core.Helpers.Interfaces
{
    public interface IValidationHelper
    {
        bool ResetValidation();
        bool Validate<T>(Expression<Func<T>> propertyExpression);
        bool Validate();
    }
}
