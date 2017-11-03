using System;
namespace MusicStoreMobile.Core.ViewModelResults
{
    public class ServiceResult
	{
		public bool Success { get; set; }
		public Error Error { get; set; } = new Error();
	}
	public class ServiceResult<T> : ServiceResult
	{
		public ServiceResult()
		{
		}

		public ServiceResult(ServiceResult serviceResult)
		{
			Success = serviceResult.Success;
			Error = serviceResult.Error;
		}

		public T Result { get; set; }
	}
}
