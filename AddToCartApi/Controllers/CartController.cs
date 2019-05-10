using AddToCartBusiness.Data;
using AddToCartBusiness.Dto;
using AddToCartBusiness.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace AddToCartApi.Controllers
{
    [Produces("application/json")]
    [Route("api/cart")]
    public class CartController : Controller
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private Guid _userGuid;
        private ICartOperation _cartOperation;

        public CartController(IHttpContextAccessor httpContextAccessor, ICartOperation cardOperation)
        {
            _httpContextAccessor = httpContextAccessor;
            var user = _httpContextAccessor.HttpContext.Request.Cookies[Constants.USER_COOKIE];
            if (string.IsNullOrEmpty(user))
                SetCookie(Constants.USER_COOKIE, Guid.NewGuid(), Constants.USER_COOKIE_EXPIRETIME);            
            else
                _userGuid = Guid.Parse(user);
            _cartOperation = cardOperation;
        }

        [HttpPost]
        [Route("add")]
        public async Task<JsonResult> AddToCart([FromBody]AddToCartRequestDto request)
        {
            if (request.Quantity == 0 || request.ProductId == 0)
                return Json((new GeneralResponse<string>() { Data = string.Empty, ErrorMsg = "ProductId or Quantity can not be 0", IsSuccess = false }));

            if (!(await _cartOperation.IsProductEnoughInStockAsync(request.ProductId.ToString(), request.Quantity)))
                return Json(new GeneralResponse<string>() { Data = string.Empty, ErrorMsg = "There are not enough products in the warehouse", IsSuccess = false });

            var result = await _cartOperation.AddToCartAsync(request.ProductId, _userGuid.ToString());

            return Json(new GeneralResponse<Cart> { Data = result, IsSuccess = true, ErrorMsg = string.Empty });
        }

        #region Private Methods

        private void SetCookie(string key, Guid guid, double expireDate)
        {
            _userGuid = guid;
            CookieOptions option = new CookieOptions();
            option.Expires = DateTime.Now.AddSeconds(expireDate);
            _httpContextAccessor.HttpContext.Response.Cookies.Append(key, guid.ToString(), option);
        }

        #endregion
    }
}