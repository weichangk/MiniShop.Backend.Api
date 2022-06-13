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
    [Description("单位信息")]
    public class UnitController : ControllerAbstract
    {
        private readonly Lazy<IUnitService> _unitService;
        private readonly Lazy<ICreateUnitService> _createUnitService;
        private readonly Lazy<IUpdateUnitService> _updateUnitService;
        public UnitController(ILogger<UnitController> logger, Lazy<IMapper> mapper,
            Lazy<IUnitService> unitService,
            Lazy<ICreateUnitService> createUnitService,
            Lazy<IUpdateUnitService> updateUnitService) : base(logger, mapper)
        {
            _unitService = unitService;
            _createUnitService = createUnitService;
            _updateUnitService = updateUnitService;
        }

        [Description("根据 ID 获取单位")]
        [ResponseCache(Duration = 0)]
        [Parameters(name = "id", param = "单位ID")]
        [HttpGet("GetByIdAsync")]
        public async Task<IResultModel> GetByIdAsync([Required] int id)
        {
            _logger.LogDebug($"根据单位 ID：{id} 查询单位");
            return await _unitService.Value.GetByIdAsync(id);
        }

        [Description("根据 shopId、单位编码获取单位")]
        [ResponseCache(Duration = 0)]
        [Parameters(name = "shopId", param = "shopId")]
        [Parameters(name = "code", param = "单位编码")]
        [HttpGet("GetByShopIdCodeAsync")]
        public async Task<IResultModel> GetByShopIdCodeAsync([Required] Guid shopId, int code)
        {
            _logger.LogDebug($"根据 shopId：{shopId} 单位编码：{code} 获取单位");
            return await _unitService.Value.GetByShopIdCodeAsync(shopId, code);
        }

        [Description("根据 shopId 获取最大单位编码")]
        [ResponseCache(Duration = 0)]
        [Parameters(name = "shopId", param = "shopId")]
        [HttpGet("GetMaxCodeByShopIdAsync")]
        public async Task<IResultModel> GetMaxCodeByShopIdAsync([Required] Guid shopId)
        {
            _logger.LogDebug($"根据 shopId：{shopId} 获取最大单位编码");
            return await _unitService.Value.GetMaxCodeByShopIdAsync(shopId);
        }

        [Description("根据 shopId 获取单位分页列表")]
        [ResponseCache(Duration = 0)]
        [Parameters(name = "pageIndex", param = "索引页")]
        [Parameters(name = "pageSize", param = "单页条数")]
        [Parameters(name = "shopId", param = "shopId")]
        [HttpGet("GetPageByShopIdAsync")]
        public async Task<IResultModel> GetPageByShopIdAsync([Required] int pageIndex, int pageSize, Guid shopId)
        {
            _logger.LogDebug($"根据 ShopId：{shopId} 分页条件：索引页{pageIndex} 单页条数{pageSize} 获取单位分页列表");
            return await _unitService.Value.GetPageByShopIdAsync(pageIndex, pageSize, shopId);
        }

        [Description("根据 shopId 附加查询条件获取单位分页列表")]
        [ResponseCache(Duration = 0)]
        [Parameters(name = "pageIndex", param = "索引页")]
        [Parameters(name = "pageSize", param = "单页条数")]
        [Parameters(name = "shopId", param = "shopId")]
        [Parameters(name = "code", param = "单位编码")]
        [Parameters(name = "name", param = "单位名称")]
        [HttpGet("GetPageByShopIdWhereQueryAsync")]
        public async Task<IResultModel> GetPageByShopIdWhereQueryAsync([Required] int pageIndex, int pageSize, Guid shopId, string code, string name)
        {
            _logger.LogDebug($"根据 shopId：{shopId} 分页条件：索引页 {pageIndex} 单页条数 {pageSize} 查询条件：单位编码 {code} 单位名称 {name} 获取单位分页列表");
            return await _unitService.Value.GetPageByShopIdWhereQueryAsync(pageIndex, pageSize, shopId, code, name);
        }

        [Description("根据 shopId、是否排序条件获取单位列表")]
        [ResponseCache(Duration = 0)]
        [Parameters(name = "shopId", param = "shopId")]
        [Parameters(name = "isDescending", param = "是否倒序")]
        [HttpGet("GetListAllByShopIdAsync")]
        public async Task<IResultModel> GetListAllByShopIdAsync(Guid shopId, bool isDescending = false)
        {
            _logger.LogDebug($"根据 shopId：{shopId} 是否倒序：{isDescending}条件获取单位列表");
            return await _unitService.Value.GetListAllByShopIdAsync(shopId, isDescending);
        }

        [Description("根据 ID 删除单位")]
        [Parameters(name = "id", param = "单位 ID")]
        [HttpDelete("DeleteAsync")]
        [Authorize(Roles = "ShopManager, ShopAssistant")]
        public async Task<IResultModel> DeleteAsync([Required] int id)
        {
            _logger.LogDebug("删除单位");
            return await _unitService.Value.RemoveAsync(id);
        }

        [Description("根据 ID 集合批量删除单位")]
        [Parameters(name = "ids", param = "单位 ID 集合")]
        [HttpDelete("BatchDeleteAsync")]
        [Authorize(Roles = "ShopManager, ShopAssistant")]
        public async Task<IResultModel> BatchDeleteAsync([FromBody] List<int> ids)
        {
            _logger.LogDebug("批量删除单位");
            return await _unitService.Value.RemoveAsync(ids);
        }

        [Description("添加单位")]
        [HttpPost("InsertAsync")]
        [Authorize(Roles = "ShopManager, ShopAssistant")]
        public async Task<IResultModel> InsertAsync([FromBody] UnitCreateDto model)
        {
            _logger.LogDebug("添加单位");
            return await _createUnitService.Value.InsertAsync(model);
        }

        [Description("Put 修改单位")]
        [HttpPut("UpdateAsync")]
        [Authorize(Roles = "ShopManager, ShopAssistant")]
        public async Task<IResultModel> UpdateAsync([FromBody] UnitUpdateDto model)
        {
            _logger.LogDebug("修改单位");
            return await _updateUnitService.Value.UpdateAsync(model);
        }

        [Description("Patch 修改单位")]
        [HttpPatch("PatchAsync")]
        [Authorize(Roles = "ShopManager, ShopAssistant")]
        public async Task<IResultModel> PatchAsync([FromRoute] int id, [FromBody] JsonPatchDocument<UnitUpdateDto> patchDocument)
        {
            _logger.LogDebug("使用JsonPatch修改单位");
            return await _updateUnitService.Value.PatchAsync(id, patchDocument);
        }

    }
}
