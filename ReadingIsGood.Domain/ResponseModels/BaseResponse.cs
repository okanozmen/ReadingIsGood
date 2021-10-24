namespace ReadingIsGood.Domain.ResponseModels
{
    public class BaseResponse
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; }

        public BaseResponse(bool isSuccess, string message)
        {
            this.IsSuccess = isSuccess;
            this.Message = message;
        }
    }
}
