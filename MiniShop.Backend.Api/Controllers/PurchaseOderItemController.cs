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

namespace MiniShop.Backend.Api.Controllers
{
    [Description("采购订单商品")]
    public class PurchaseOderItemController : ControllerAbstract
    {
        private readonly Lazy<IPurchaseOderItemService> _purchaseOderItemService;
        private readonly Lazy<ICreatePurchaseOderItemService> _createPurchaseOderItemService;
        private readonly Lazy<IUpdatePurchaseOderItemService> _updatePurchaseOderItemService;
        public PurchaseOderItemController(ILogger<PurchaseOderController> logger, Lazy<IMapper> mapper,
            Lazy<IPurchaseOderItemService> purchaseOderItemService,
            Lazy<ICreatePurchaseOderItemService> createPurchaseOderItemService,
            Lazy<IUpdatePurchaseOderItemService> updatePurchaseOderItemService) : base(logger, mapper)
        {
            _purchaseOderItemService = purchaseOderItemService;
            _createPurchaseOderItemService = createPurchaseOderItemService;
            _updatePurchaseOderItemService = updatePurchaseOderItemService;
        }

        [Description("根据ID查询采购订单商品")]
        [OperationId("根据ID查询采购订单商品")]
        [ResponseCache(Duration = 0)]
        [Parameters(name = "id", param = "采购订单商品ID")]
        [HttpGet]
        public async Task<IResultModel> GetById([Required] int id)
        {
            _logger.LogDebug($"根据采购订单商品ID：{id} 采购订单商品");
            return await _purchaseOderItemService.Value.GetByIdAsync(id);
        }

        [Description("根据商店ID和采购订单商品ID查询采购订单商品")]
        [OperationId("根据商店ID和采购订单商品ID查询采购订单商品")]
        [ResponseCache(Duration = 0)]
        [Parameters(name = "shopId", param = "商店ID")]
        [Parameters(name = "id", param = "采购订单商品ID")]
        [HttpGet("GetByIdOnShop")]
        public async Task<IResultModel> GetByIdOnShop([Required] Guid shopId, int id)
        {
            _logger.LogDebug($"根据商店ID：{shopId} 采购订单商品ID：{id} 查询采购订单商品");
            return await _purchaseOderItemService.Value.GetByIdOnShopAsync(shopId, id);
        }

        [Description("根据分页条件获取采购订单商品")]
        [OperationId("根据分页条件获取采购订单商品")]
        [ResponseCache(Duration = 0)]
        [Parameters(name = "pageIndex", param = "索引页")]
        [Parameters(name = "pageSize", param = "单页条数")]
        [Parameters(name = "shopId", param = "商店ID")]
        [HttpGet("GetPageOnShop")]
        public async Task<IResultModel> GetPageOnShop([Required] int pageIndex, int pageSize, Guid shopId)
        {
            _logger.LogDebug($"根据商店ID：{shopId} 分页条件：索引页{pageIndex} 单页条数{pageSize} 获取采购订单商品");
            return await _purchaseOderItemService.Value.GetPageByShopIdAsync(pageIndex, pageSize, shopId);
        }

        [Description("根据商店ID、订单号获取采购订单商品列表")]
        [OperationId("根据商店ID、订单号获取采购订单商品列表")]
        [ResponseCache(Duration = 0)]
        [Parameters(name = "pageIndex", param = "索引页")]
        [Parameters(name = "pageSize", param = "单页条数")]
        [Parameters(name = "shopId", param = "商店ID")]
        [Parameters(name = "oderNo", param = "采购订单编码")]
        [HttpGet("GetPageByShopIdOderNoAsync")]
        public async Task<IResultModel> GetPageByShopIdOderNoAsync([Required] int pageIndex, int pageSize,  Guid shopId, string oderNo)
        {
            _logger.LogDebug($"根据商店ID：{shopId} 采购订单编码：{oderNo} 分页条件：索引页{pageIndex} 单页条数{pageSize} 获取采购订单商品列表");
            return await _purchaseOderItemService.Value.GetPageByShopIdOderNoAsync(pageIndex, pageSize, shopId, oderNo);
        }

        [Description("通过指定采购订单ID删除采购订单商品")]
        [OperationId("通过指定采购订单ID删除采购订单商品")]
        [Parameters(name = "id", param = "采购订单商品ID")]
        [HttpDelete]
        [Authorize(Roles = "ShopManager, ShopAssistant")]
        public async Task<IResultModel> Delete([Required] int id)
        {
            _logger.LogDebug("删除采购订单商品");
            return await _purchaseOderItemService.Value.RemoveAsync(id);
        }

        [Description("通过指定采购订单商品ID集合批量删除采购订单商品")]
        [OperationId("批量删除采购订单商品")]
        [Parameters(name = "ids", param = "采购订单商品ID集合")]
        [HttpDelete("BatchDelete")]
        [Authorize(Roles = "ShopManager, ShopAssistant")]
        public async Task<IResultModel> BatchDelete([FromBody] List<int> ids)
        {
            _logger.LogDebug("批量删除采购订单");
            return await _purchaseOderItemService.Value.RemoveAsync(ids);
        }

        [Description("添加采购订单商品，成功后返回当前采购订单商品")]
        [OperationId("添加采购订单商品")]
        [HttpPost]
        [Authorize(Roles = "ShopManager, ShopAssistant")]
        public async Task<IResultModel> Add([FromBody] PurchaseOderItemCreateDto model)
        {
            _logger.LogDebug("添加采购订单商品");
            return await _createPurchaseOderItemService.Value.InsertAsync(model);
        }

        [Description("Put修改采购订单，成功返回采购订单商品")]
        [OperationId("修改采购订单商品")]
        [HttpPut]
        [Authorize(Roles = "ShopManager, ShopAssistant")]
        public async Task<IResultModel> Update([FromBody] PurchaseOderItemUpdateDto model)
        {
            _logger.LogDebug("修改采购订单商品");
            return await _updatePurchaseOderItemService.Value.UpdateAsync(model);
        }

        [Description("Patch使用修改采购订单，成功返回采购订单商品")]
        [OperationId("修改采购订单商品")]
        [HttpPatch]
        [Authorize(Roles = "ShopManager, ShopAssistant")]
        public async Task<IResultModel> PatchUpdate([FromRoute] int id, [FromBody] JsonPatchDocument<PurchaseOderItemUpdateDto> patchDocument)
        {
            _logger.LogDebug("使用JsonPatch修改采购商品");
            return await _updatePurchaseOderItemService.Value.PatchAsync(id, patchDocument);
        }



    }
}
