namespace AddToCartBusiness.Dto
{
    public class GeneralResponse<T>
    {
        public GeneralResponse()
        {
            IsSuccess = true;
            ErrorMsg = "";
        }

        public bool IsSuccess { get; set; }

        public string ErrorMsg { get; set; }

        public T Data { get; set; }
    }
}