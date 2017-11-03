using System;
namespace MusicStoreMobile.Core.Enums
{
	public enum ErrorStatusCode
	{
		Empty = 0,

		// 1 - 199 gloabal
		Recaptcha = 1,
		InvalidToken,

		//200
		NoContent = 200,
		// 300
		NotModified = 304,
		
		// 400
		BudRequest = 400,
		//EmailConfirmed = 400, // коли відкриваються лінк для підтвредження пошти другий раз
		//EmailSendError = 401,
		AccessDenied = 403,
		ContentNotFound = 404,
		Conflict = 405,// ця дія вже виконанна
		InvalidData,
		InProgress,

		// 500
		InternalServerError = 500,
		DatabaseError = 501,
		SearchError,

		// 600 - register
		InvalidSignUp = 600,

		// 701 - 
		UploadImageFiled = 701,

	}
}
