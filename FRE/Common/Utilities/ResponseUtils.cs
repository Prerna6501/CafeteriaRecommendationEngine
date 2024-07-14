using Common.Models;
using Newtonsoft.Json;

namespace Common.Utilities
{
    public static class ResponseUtils
    {
        public static string CreateSuccessJsonResponse(string response)
        {
            var responseModel = new ResponseModel()
            {
                Response = response,
                IsSuccesful = true,
            };
            return JsonConvert.SerializeObject(responseModel, Formatting.Indented);
        }

        public static string CreateExceptionJsonResponse(string response)
        {
            var responseModel = new ResponseModel()
            {
                Response = response,
                IsSuccesful = false,
            };
            return JsonConvert.SerializeObject(responseModel, Formatting.Indented);
        }

        public static bool HandleResponse(string response, out string message)
        {
            var responseModel = JsonConvert.DeserializeObject<ResponseModel>(response);
            message = responseModel.Response;
            if (responseModel.IsSuccesful)
            {
                Console.WriteLine("Operation successful.");
                return true;
            }
            else
            {
                Console.WriteLine($"Exception: {responseModel.Response}");
                return false;
            }
        }
    }
}
