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
using System.Threading.Tasks;
using MiniShop.Backend.Model.Enums;

namespace MiniShop.Backend.Api.Controllers
{
    [Description("采购订单")]
    public class PurchaseOderController : ControllerAbstract
    {
        private readonly Lazy<IPurchaseOderService> _purchaseOderService;
        private readonly Lazy<ICreatePurchaseOderService> _createPurchaseOderService;
        private readonly Lazy<IUpdatePurchaseOderService> _updatePurchaseOderService;
        private readonly Lazy<IAuditPurchaseOderService> _auditPurchaseOderService;

        public PurchaseOderController(ILogger<PurchaseOderController> logger, Lazy<IMapper> mapper,
            Lazy<IPurchaseOderService> purchaseOderService,
            Lazy<ICreatePurchaseOderService> createPurchaseOderService,
            Lazy<IUpdatePurchaseOderService> updatePurchaseOderService,
            Lazy<IAuditPurchaseOderService> auditPurchaseOderService) : base(logger, mapper)
        {
            _purchaseOderService = purchaseOderService;
            _createPurchaseOderService = createPurchaseOderService;
            _updatePurchaseOderService = updatePurchaseOderService;
            _auditPurchaseOderService = auditPurchaseOderService;
        }

        [Description("根据采购订单 ID 获取采购订单")]
        [ResponseCache(Duration = 0)]
        [Parameters(name = "id", param = "采购订单 ID")]
        [HttpGet("GetByIdAsync")]
        public async Task<IResultModel> GetByIdAsync([Required] int id)
        {
            _logger.LogDebug($"根据采购订单 ID：{id} 获取采购订单");
            return await _purchaseOderService.Value.GetByIdAsync(id);
        }

        [Description("根据 shopId、采购订单号获取采购订单")]
        [ResponseCache(Duration = 0)]
        [Parameters(name = "shopId", param = "shopId")]
        [Parameters(name = "oderNo", param = "订单号")]
        [HttpGet("GetByShopIdOderNoAsync")]
        public async Task<IResultModel> GetByShopIdOderNoAsync([Required] Guid shopId, string oderNo)
        {
            _logger.LogDebug($"根据 shopId：{shopId} 采购订单号：{oderNo} 获取采购订单");
            return await _purchaseOderService.Value.GetByShopIdOderNoAsync(shopId, oderNo);
        }

        [Description("根据 shopId 获取采购订单分页列表")]
        [ResponseCache(Duration = 0)]
        [Parameters(name = "pageIndex", param = "索引页")]
        [Parameters(name = "pageSize", param = "单页条数")]
        [Parameters(name = "shopId", param = "shopId")]
        [HttpGet("GetPageByShopIdAsync")]
        public async Task<IResultModel> GetPageByShopIdAsync([Required] int pageIndex, int pageSize, Guid shopId)
        {
            _logger.LogDebug($"根据 shopId：{shopId} 分页条件：索引页{pageIndex} 单页条数{pageSize} 获取采购订单分页列表");
            return await _purchaseOderService.Value.GetPageByShopIdAsync(pageIndex, pageSize, shopId);
        }

        [Description("根据 shopId 获取已审核未收货采购订单分页列表")]
        [ResponseCache(Duration = 0)]
        [Parameters(name = "pageIndex", param = "索引页")]
        [Parameters(name = "pageSize", param = "单页条数")]
        [Parameters(name = "shopId", param = "shopId")]
        [HttpGet("GetAuditedUnReceivedPageByShopIdAsync")]
        public async Task<IResultModel> GetAuditedUnReceivedPageByShopIdAsync([Required] int pageIndex, int pageSize, Guid shopId)
        {
            _logger.LogDebug($"根据 shopId：{shopId} 分页条件：索引页{pageIndex} 单页条数{pageSize} 获取已审核未收货采购订单分页列表");
            return await _purchaseOderService.Value.GetAuditedUnReceivedPageByShopIdAsync(pageIndex, pageSize, shopId);
        }

        [Description("根据 shopId 获取已审核未退货采购订单分页列表")]
        [ResponseCache(Duration = 0)]
        [Parameters(name = "pageIndex", param = "索引页")]
        [Parameters(name = "pageSize", param = "单页条数")]
        [Parameters(name = "shopId", param = "shopId")]
        [HttpGet("GetAuditedUnReturnPageByShopIdAsync")]
        public async Task<IResultModel> GetAuditedUnReturnPageByShopIdAsync([Required] int pageIndex, int pageSize, Guid shopId)
        {
            _logger.LogDebug($"根据 shopId：{shopId} 分页条件：索引页{pageIndex} 单页条数{pageSize} 获取已审核未退货采购订单分页列表");
            return await _purchaseOderService.Value.GetAuditedUnReturnPageByShopIdAsync(pageIndex, pageSize, shopId);
        }

        [Description("根据 shopId 附加查询条件获取采购订单分页列表")]
        [ResponseCache(Duration = 0)]
        [Parameters(name = "pageIndex", param = "索引页")]
        [Parameters(name = "pageSize", param = "单页条数")]
        [Parameters(name = "shopId", param = "shopId")]
        [Parameters(name = "oderNo", param = "采购订单号")]
        [HttpGet("GetPageByShopIdWhereQueryAsync")]
        public async Task<IResultModel> GetPageByShopIdWhereQueryAsync([Required] int pageIndex, int pageSize, Guid shopId, string oderNo)
        {
            _logger.LogDebug($"根据 shopId：{shopId} 分页条件：索引页 {pageIndex} 单页条数 {pageSize} 查询条件：采购订单号 {oderNo} 获取采购订单分页列表");
            return await _purchaseOderService.Value.GetPageByShopIdWhereQueryAsync(pageIndex, pageSize, shopId, oderNo);
        }

        [Description("根据 shopId 附加查询条件获取已审核未收货采购订单分页列表")]
        [ResponseCache(Duration = 0)]
        [Parameters(name = "pageIndex", param = "索引页")]
        [Parameters(name = "pageSize", param = "单页条数")]
        [Parameters(name = "shopId", param = "shopId")]
        [Parameters(name = "oderNo", param = "采购订单号")]
        [HttpGet("GetAuditedUnReceivedPageByShopIdWhereQueryAsync")]
        public async Task<IResultModel> GetAuditedUnReceivedPageByShopIdWhereQueryAsync([Required] int pageIndex, int pageSize, Guid shopId, string oderNo)
        {
            _logger.LogDebug($"根据 shopId：{shopId} 分页条件：索引页 {pageIndex} 单页条数 {pageSize} 查询条件：采购订单号 {oderNo} 获取已审核未收货采购订单分页列表");
            return await _purchaseOderService.Value.GetAuditedUnReceivedPageByShopIdWhereQueryAsync(pageIndex, pageSize, shopId, oderNo);
        }

        [Description("根据 shopId 附加查询条件获取已审核未退货采购订单分页列表")]
        [ResponseCache(Duration = 0)]
        [Parameters(name = "pageIndex", param = "索引页")]
        [Parameters(name = "pageSize", param = "单页条数")]
        [Parameters(name = "shopId", param = "shopId")]
        [Parameters(name = "oderNo", param = "采购订单号")]
        [HttpGet("GetAuditedUnReturnPageByShopIdWhereQueryAsync")]
        public async Task<IResultModel> GetAuditedUnReturnPageByShopIdWhereQueryAsync([Required] int pageIndex, int pageSize, Guid shopId, string oderNo)
        {
            _logger.LogDebug($"根据 shopId：{shopId} 分页条件：索引页 {pageIndex} 单页条数 {pageSize} 查询条件：采购订单号 {oderNo} 获取已审核未退货采购订单分页列表");
            return await _purchaseOderService.Value.GetAuditedUnReturnPageByShopIdWhereQueryAsync(pageIndex, pageSize, shopId, oderNo);
        }

        [Description("根据 ID 删除采购订单")]
        [Parameters(name = "id", param = "采购订单ID")]
        [HttpDelete("DeleteAsync")]
        [Authorize(Roles = "ShopManager, ShopAssistant")]
        public async Task<IResultModel> DeleteAsync([Required] int id)
        {
            _logger.LogDebug("删除采购订单");
            return await _purchaseOderService.Value.RemoveAsync(id);
        }

        [Description("根据 ID 集合批量删除采购订单")]
        [Parameters(name = "ids", param = "采购订单 ID 集合")]
        [HttpDelete("BatchDeleteAsync")]
        [Authorize(Roles = "ShopManager, ShopAssistant")]
        public async Task<IResultModel> BatchDeleteAsync([FromBody] List<int> ids)
        {
            _logger.LogDebug("批量删除采购订单");
            return await _purchaseOderService.Value.RemoveAsync(ids);
        }

        [Description("添加采购订单")]
        [HttpPost("InsertAsync")]
        [Authorize(Roles = "ShopManager, ShopAssistant")]
        public async Task<IResultModel> InsertAsync([FromBody] PurchaseOderCreateDto model)
        {
            _logger.LogDebug("添加采购订单");
            return await _createPurchaseOderService.Value.InsertAsync(model);
        }

        [Description("Put 修改采购订单")]
        [HttpPut("UpdateAsync")]
        [Authorize(Roles = "ShopManager, ShopAssistant")]
        public async Task<IResultModel> UpdateAsync([FromBody] PurchaseOderUpdateDto model)
        {
            _logger.LogDebug("修改采购订单");
            return await _updatePurchaseOderService.Value.UpdateAsync(model);
        }

        [Description("审核采购订单")]
        [HttpPut("AuditAsync")]
        [Authorize(Roles = "ShopManager, ShopAssistant")]
        public async Task<IResultModel> AuditAsync([FromBody] PurchaseOderAuditDto model)
        {
            _logger.LogDebug("审核采购订单");
            return await _auditPurchaseOderService.Value.UpdateAsync(model);
        }

        [Description("Patch 修改采购订单")]
        [HttpPatch("PatchAsync")]
        [Authorize(Roles = "ShopManager, ShopAssistant")]
        public async Task<IResultModel> PatchAsync([FromRoute] int id, [FromBody] JsonPatchDocument<PurchaseOderUpdateDto> patchDocument)
        {
            _logger.LogDebug("使用JsonPatch修改采购订单");
            return await _updatePurchaseOderService.Value.PatchAsync(id, patchDocument);
        }

        [Description("修改采购订单金额")]
        [HttpPut("UpdateOderAmountAsync")]
        [Authorize(Roles = "ShopManager, ShopAssistant")]
        public async Task<IResultModel> UpdateOderAmountAsync([FromForm]int id, [FromForm]decimal oderAmount)
        {
            _logger.LogDebug("修改采购订单金额");
            return await _updatePurchaseOderService.Value.UpdateOderAmountAsync(id, oderAmount);
        }

        [Description("修改采购订单状态")]
        [HttpPut("UpdatePurchaseOderStatusAsync")]
        [Authorize(Roles = "ShopManager, ShopAssistant")]
        public async Task<IResultModel> UpdatePurchaseOderStatusAsync([FromForm]int id, [FromForm]EnumPurchaseOrderStatus state)
        {
            _logger.LogDebug("修改采购订单金额");
            return await _updatePurchaseOderService.Value.UpdatePurchaseOderStatusAsync(id, state);
        }
    }
}
