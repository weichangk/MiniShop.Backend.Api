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
    [Description("供应商信息")]
    public class SupplierController : ControllerAbstract
    {
        private readonly Lazy<ISupplierService> _supplierService;
        private readonly Lazy<ICreateSupplierService> _createSupplierService;
        private readonly Lazy<IUpdateSupplierService> _updateSupplierService;
        public SupplierController(ILogger<SupplierController> logger, Lazy<IMapper> mapper,
            Lazy<ISupplierService> supplierService,
            Lazy<ICreateSupplierService> createSupplierService,
            Lazy<IUpdateSupplierService> updateSupplierService) : base(logger, mapper)
        {
            _supplierService = supplierService;
            _createSupplierService = createSupplierService;
            _updateSupplierService = updateSupplierService;
        }

        [Description("根据供应商 ID 获取供应商")]
        [ResponseCache(Duration = 0)]
        [Parameters(name = "id", param = "供应商 ID")]
        [HttpGet("GetByIdAsync")]
        public async Task<IResultModel> GetByIdAsync([Required] int id)
        {
            _logger.LogDebug($"根据供应商 ID：{id} 获取供应商");
            return await _supplierService.Value.GetByIdAsync(id);
        }

        [Description("根据 shopId、供应商编码获取供应商")]
        [ResponseCache(Duration = 0)]
        [Parameters(name = "shopId", param = "shopId")]
        [Parameters(name = "code", param = "供应商编码")]
        [HttpGet("GetByShopIdCodeAsync")]
        public async Task<IResultModel> GetByShopIdCodeAsync([Required] Guid shopId, int code)
        {
            _logger.LogDebug($"根据 shopId：{shopId} 供应商编码：{code} 查询供应商");
            return await _supplierService.Value.GetByShopIdCodeAsync(shopId, code);
        }

        [Description("根据 shopId 获取供应商")]
        [ResponseCache(Duration = 0)]
        [Parameters(name = "shopId", param = "ShopId")]
        [HttpGet("GetByShopIdAsync")]
        public async Task<IResultModel> GetByShopIdAsync([Required] Guid shopId)
        {
            _logger.LogDebug($"根据 ShopId：{shopId} 获取供应商");
            return await _supplierService.Value.GetByShopIdAsync(shopId);
        }

        [Description("根据 shopId 获取最大供应商编码")]
        [ResponseCache(Duration = 0)]
        [Parameters(name = "shopId", param = "shopId")]
        [HttpGet("GetMaxCodeByShopIdAsync")]
        public async Task<IResultModel> GetMaxCodeByShopIdAsync([Required] Guid shopId)
        {
            _logger.LogDebug($"根据 shopId：{shopId} 获取最大供应商编码");
            return await _supplierService.Value.GetMaxCodeByShopIdAsync(shopId);
        }

        [Description("根据 shopId 获取供应商分页列表")]
        [ResponseCache(Duration = 0)]
        [Parameters(name = "pageIndex", param = "索引页")]
        [Parameters(name = "pageSize", param = "单页条数")]
        [Parameters(name = "shopId", param = "shopId")]
        [HttpGet("GetPageByShopIdAsync")]
        public async Task<IResultModel> GetPageByShopIdAsync([Required] int pageIndex, int pageSize, Guid shopId)
        {
            _logger.LogDebug($"根据 shopId：{shopId} 分页条件：索引页{pageIndex} 单页条数{pageSize} 获取供应商分页列表");
            return await _supplierService.Value.GetPageByShopIdAsync(pageIndex, pageSize, shopId);
        }

        [Description("根据 shopId 附加查询条件获取供应商分页列表")]
        [ResponseCache(Duration = 0)]
        [Parameters(name = "pageIndex", param = "索引页")]
        [Parameters(name = "pageSize", param = "单页条数")]
        [Parameters(name = "shopId", param = "shopId")]
        [Parameters(name = "code", param = "供应商编码")]
        [Parameters(name = "name", param = "供应商名称")]
        [HttpGet("GetPageByShopIdWhereQueryAsync")]
        public async Task<IResultModel> GetPageByShopIdWhereQueryAsync([Required] int pageIndex, int pageSize, Guid shopId, string code, string name)
        {
            _logger.LogDebug($"根据 shopId：{shopId} 分页条件：索引页 {pageIndex} 单页条数 {pageSize} 查询条件：供应商编码 {code} 供应商名称 {name} 获取供应商");
            return await _supplierService.Value.GetPageByShopIdWhereQueryAsync(pageIndex, pageSize, shopId, code, name);
        }

        [Description("根据 ID 删除供应商")]
        [Parameters(name = "id", param = "供应商 ID")]
        [HttpDelete("DeleteAsync")]
        [Authorize(Roles = "ShopManager, ShopAssistant")]
        public async Task<IResultModel> DeleteAsync([Required] int id)
        {
            _logger.LogDebug("删除供应商");
            return await _supplierService.Value.RemoveAsync(id);
        }

        [Description("根据 ID 集合批量删除供应商")]
        [Parameters(name = "ids", param = "供应商 ID 集合")]
        [HttpDelete("BatchDeleteAsync")]
        [Authorize(Roles = "ShopManager, ShopAssistant")]
        public async Task<IResultModel> BatchDeleteAsync([FromBody] List<int> ids)
        {
            _logger.LogDebug("批量删除供应商");
            return await _supplierService.Value.RemoveAsync(ids);
        }

        [Description("添加供应商")]
        [HttpPost("InsertAsync")]
        [Authorize(Roles = "ShopManager, ShopAssistant")]
        public async Task<IResultModel> InsertAsync([FromBody] SupplierCreateDto model)
        {
            _logger.LogDebug("添加供应商");
            return await _createSupplierService.Value.InsertAsync(model);
        }

        [Description("Put 修改供应商")]
        [HttpPut("UpdateAsync")]
        [Authorize(Roles = "ShopManager, ShopAssistant")]
        public async Task<IResultModel> UpdateAsync([FromBody] SupplierUpdateDto model)
        {
            _logger.LogDebug("修改供应商");
            return await _updateSupplierService.Value.UpdateAsync(model);
        }

        [Description("Patch 修改供应商")]
        [HttpPatch("PatchAsync")]
        [Authorize(Roles = "ShopManager, ShopAssistant")]
        public async Task<IResultModel> PatchAsync([FromRoute] int id, [FromBody] JsonPatchDocument<SupplierUpdateDto> patchDocument)
        {
            _logger.LogDebug("使用 JsonPatch 修改供应商");
            return await _updateSupplierService.Value.PatchAsync(id, patchDocument);
        }

    }
}
