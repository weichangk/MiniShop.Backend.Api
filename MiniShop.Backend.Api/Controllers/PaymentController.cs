using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MiniShop.Backend.Model.Dto;
using MiniShop.Backend.Api.Services;
using Weick.Orm.Core.Result;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Net;
using System.Threading.Tasks;

namespace MiniShop.Backend.Api.Controllers
{
    [Description("支付方式信息")]
    public class PaymentController : ControllerAbstract
    {
        private readonly Lazy<IPaymentService> _paymentService;
        private readonly Lazy<ICreatePaymentService> _createPaymentService;
        private readonly Lazy<IUpdatePaymentService> _updatePaymentService;
        public PaymentController(ILogger<PaymentController> logger, Lazy<IMapper> mapper,
            Lazy<IPaymentService> paymentService,
            Lazy<ICreatePaymentService> createPaymentService,
            Lazy<IUpdatePaymentService> updatePaymentService) : base(logger, mapper)
        {
            _paymentService = paymentService;
            _createPaymentService = createPaymentService;
            _updatePaymentService = updatePaymentService;
        }

        [Description("根据 ID 获取支付方式")]
        [ResponseCache(Duration = 0)]
        [Parameters(name = "id", param = "支付方式ID")]
        [HttpGet("GetByIdAsync")]
        public async Task<IResultModel> GetByIdAsync([Required] int id)
        {
            _logger.LogDebug($"根据支付方式 ID：{id} 查询支付方式");
            return await _paymentService.Value.GetByIdAsync(id);
        }

        [Description("根据 shopId、支付方式编码获取支付方式")]
        [ResponseCache(Duration = 0)]
        [Parameters(name = "shopId", param = "shopId")]
        [Parameters(name = "code", param = "支付方式编码")]
        [HttpGet("GetByShopIdCodeAsync")]
        public async Task<IResultModel> GetByShopIdCodeAsync([Required] Guid shopId, string code)
        {
            _logger.LogDebug($"根据 shopId：{shopId} 支付方式编码：{code} 获取支付方式");
            return await _paymentService.Value.GetByShopIdCodeAsync(shopId, code);
        }

        [Description("根据 shopId 获取支付方式分页列表")]
        [ResponseCache(Duration = 0)]
        [Parameters(name = "pageIndex", param = "索引页")]
        [Parameters(name = "pageSize", param = "单页条数")]
        [Parameters(name = "shopId", param = "shopId")]
        [HttpGet("GetPageByShopIdAsync")]
        public async Task<IResultModel> GetPageByShopIdAsync([Required] int pageIndex, int pageSize, Guid shopId)
        {
            _logger.LogDebug($"根据 ShopId：{shopId} 分页条件：索引页{pageIndex} 单页条数{pageSize} 获取支付方式分页列表");
            return await _paymentService.Value.GetPageByShopIdAsync(pageIndex, pageSize, shopId);
        }

        [Description("根据 shopId 附加查询条件获取支付方式分页列表")]
        [ResponseCache(Duration = 0)]
        [Parameters(name = "pageIndex", param = "索引页")]
        [Parameters(name = "pageSize", param = "单页条数")]
        [Parameters(name = "shopId", param = "shopId")]
        [Parameters(name = "code", param = "支付方式编码")]
        [Parameters(name = "name", param = "支付方式名称")]
        [HttpGet("GetPageByShopIdWhereQueryAsync")]
        public async Task<IResultModel> GetPageByShopIdWhereQueryAsync([Required] int pageIndex, int pageSize, Guid shopId, string code, string name)
        {
            _logger.LogDebug($"根据 shopId：{shopId} 分页条件：索引页 {pageIndex} 单页条数 {pageSize} 查询条件：支付方式编码 {code} 支付方式名称 {name} 获取支付方式分页列表");
            return await _paymentService.Value.GetPageByShopIdWhereQueryAsync(pageIndex, pageSize, shopId, code, name);
        }

        [Description("根据 shopId、是否排序条件获取支付方式列表")]
        [ResponseCache(Duration = 0)]
        [Parameters(name = "shopId", param = "shopId")]
        [Parameters(name = "isDescending", param = "是否倒序")]
        [HttpGet("GetListAllByShopIdAsync")]
        public async Task<IResultModel> GetListAllByShopIdAsync(Guid shopId, bool isDescending = false)
        {
            _logger.LogDebug($"根据 shopId：{shopId} 是否倒序：{isDescending}条件获取支付方式列表");
            return await _paymentService.Value.GetListAllByShopIdAsync(shopId, isDescending);
        }

        [Description("根据 ID 删除支付方式")]
        [Parameters(name = "id", param = "支付方式 ID")]
        [HttpDelete("DeleteAsync")]
        [Authorize(Roles = "ShopManager, ShopAssistant")]
        public async Task<IResultModel> DeleteAsync([Required] int id)
        {
            _logger.LogDebug("删除支付方式");
            return await _paymentService.Value.RemoveAsync(id);
        }

        [Description("根据 ID 集合批量删除支付方式")]
        [Parameters(name = "ids", param = "支付方式 ID 集合")]
        [HttpDelete("BatchDeleteAsync")]
        [Authorize(Roles = "ShopManager, ShopAssistant")]
        public async Task<IResultModel> BatchDeleteAsync([FromBody] List<int> ids)
        {
            _logger.LogDebug("批量删除支付方式");
            return await _paymentService.Value.RemoveAsync(ids);
        }

        [Description("添加支付方式")]
        [HttpPost("InsertAsync")]
        [Authorize(Roles = "ShopManager, ShopAssistant")]
        public async Task<IResultModel> InsertAsync([FromBody] PaymentCreateDto model)
        {
            _logger.LogDebug("添加支付方式");
            return await _createPaymentService.Value.InsertAsync(model);
        }

        [Description("Put 修改支付方式")]
        [HttpPut("UpdateAsync")]
        [Authorize(Roles = "ShopManager, ShopAssistant")]
        public async Task<IResultModel> UpdateAsync([FromBody] PaymentUpdateDto model)
        {
            _logger.LogDebug("修改支付方式");
            return await _updatePaymentService.Value.UpdateAsync(model);
        }

        [Description("Patch 修改支付方式")]
        [HttpPatch("PatchAsync")]
        [Authorize(Roles = "ShopManager, ShopAssistant")]
        public async Task<IResultModel> PatchAsync([FromRoute] int id, [FromBody] JsonPatchDocument<PaymentUpdateDto> patchDocument)
        {
            _logger.LogDebug("使用JsonPatch修改支付方式");
            return await _updatePaymentService.Value.PatchAsync(id, patchDocument);
        }

    }
}
