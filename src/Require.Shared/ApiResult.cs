using System.Text.Json.Serialization;

namespace Require.Shared
{
    public enum ResultEnum { None, Success, Fault }
    public class ApiResult<TType>
    {
        public const int SUCCESS = 0;
        public const int UNSPECIFIED = -1;

        public ResultEnum Result { get; set; } = ResultEnum.None;
        public TType? Value { get; set; } = default(TType);
        public int ErrorCode { get; set; } = SUCCESS;
        public string Error { get; set; } = String.Empty;
        public string[] AdditionalInformation { get; set; } = Array.Empty<string>();

        [JsonIgnore]
        public bool IsSuccessful { get { return this.Result == ResultEnum.Success; } }
        [JsonIgnore]
        public bool IsFaulted { get { return this.Result == ResultEnum.Fault; } }

        public ApiResult()
        {

        }

        public ApiResult(TType value)
        {
            this.Result = ResultEnum.Success;
            this.Value = value;
        }

        public ApiResult(int errorCode, string error, params string[] additionalInformation)
        {
            this.Result = ResultEnum.Fault;
            this.ErrorCode = errorCode;
            this.Error = error;
            this.AdditionalInformation = additionalInformation;
        }

        public ApiResult(string error, params string[] additionalInformation)
        {
            this.Result = ResultEnum.Fault;
            this.ErrorCode = UNSPECIFIED;
            this.Error = error;
            this.AdditionalInformation = additionalInformation;
        }

        public static implicit operator ApiResult<TType>(TType value) { return new ApiResult<TType>(value); }
        public static implicit operator TType(ApiResult<TType> value) {
            if (value.IsSuccessful)
                return value.Value!;
            else
                throw new ResultFaultedException();
        }



    }
}