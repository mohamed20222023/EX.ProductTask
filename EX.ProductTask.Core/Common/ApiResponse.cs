namespace Core.Common
{
	public class ApiResponse
	{
		public int StatusCode { get; set; } 
		public int TotalItems { get; set; } 
		public bool Success { get; set; }
		public string Message { get; set; }
		public object Data { get; set; }

		public ApiResponse()
		{
		}

		public ApiResponse(object data,int totalItems, int statusCode = 200)
		{
			StatusCode = statusCode;
			Success = true;
			TotalItems = totalItems;
			Data = data;
		}

		public ApiResponse(string message, int statusCode = 400)
		{
			StatusCode = statusCode;
			Success = false;
			Message = message;
		}
	}
}
