using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Api_ProjectManagement.Common.Exceptions
{
    public static class HandleException
    {
        public static ObjectResult GetExceptionResult(Exception ex)
        {
            ObjectResult objRes = new ObjectResult(ex.Message);
            objRes.StatusCode = StatusCodes.Status500InternalServerError;

            if (ex is HttpStatusException httpStatusEx)
            {
                objRes.StatusCode = (int)httpStatusEx.StatusCode;
                objRes.Value = httpStatusEx.Message;
            }
            else if (ex is NotImplementedException)
            {
                objRes.StatusCode = StatusCodes.Status501NotImplemented;
            }
            else if (ex is DbUpdateException)
            {
                objRes.StatusCode = StatusCodes.Status400BadRequest;
                objRes.Value = $"Ocurrió un error al intentar guardar en la DB. {ex.Message} + {ex.InnerException} + {ex.StackTrace}";
            }
            //objRes.Value = new { Mensaje = objRes.Value };
            objRes.Value = new { statusCode = objRes.StatusCode, mensaje = objRes.Value };
            return objRes;
        }
    }
}
