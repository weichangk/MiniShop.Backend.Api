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
    [Description("类别信息")]
    public class CategorieController : ControllerAbstract
    {
        private readonly Lazy<ICategorieService> _categorieService;
        private readonly Lazy<ICreateCategorieService> _createCategorieService;
        private readonly Lazy<IUpdateCategorieService> _updateCategorieService;
        public CategorieController(ILogger<CategorieController> logger, Lazy<IMapper> mapper,
            Lazy<ICategorieService> categorieService,
            Lazy<ICreateCategorieService> createCategorieService,
            Lazy<IUpdateCategorieService> updateCategorieService) : base(logger, mapper)
        {
            _categorieService = categorieService;
            _createCategorieService = createCategorieService;
            _updateCategorieService = updateCategorieService;
        }

        [Description("根据 ID 获取类别")]
        [ResponseCache(Duration = 0)]
        [Parameters(name = "id", param = "类别 ID")]
        [HttpGet("GetByIdAsync")]
        public async Task<IResultModel> GetByIdAsync([Required] int id)
        {
            _logger.LogDebug($"根据类别 ID：{id} 获取类别");
            return await _categorieService.Value.GetByIdAsync(id);
        }

        [Description("根据 shopId、类别编码获取类别")]
        [ResponseCache(Duration = 0)]
        [Parameters(name = "shopId", param = "shopId")]
        [Parameters(name = "code", param = "类别编码")]
        [HttpGet("GetByShopIdCodeAsync")]
        public async Task<IResultModel> GetByShopIdCodeAsync([Required] Guid shopId, int code)
        {
            _logger.LogDebug($"根据 shopId：{shopId} 类别编码：{code} 获取类别");
            return await _categorieService.Value.GetByShopIdCodeAsync(shopId, code);
        }

        [Description("根据 shopId、类别等级获取最大类别编码")]
        [ResponseCache(Duration = 0)]
        [Parameters(name = "shopId", param = "shopId")]
        [Parameters(name = "level", param = "类别等级")]
        [HttpGet("GetMaxCodeByShopIdLevelAsync")]
        public async Task<IResultModel> GetMaxCodeByShopIdLevelAsync([Required] Guid shopId, int level)
        {
            _logger.LogDebug($"根据 shopId：{shopId} 类别等级：{level} 获取最大类别编码");
            return await _categorieService.Value.GetMaxCodeByShopIdLevelAsync(shopId, level);
        }

        [Description("根据 shopId 获取类别分页列表")]
        [ResponseCache(Duration = 0)]
        [Parameters(name = "pageIndex", param = "索引页")]
        [Parameters(name = "pageSize", param = "单页条数")]
        [Parameters(name = "shopId", param = "shopId")]
        [HttpGet("GetPageByShopIdAsync")]
        public async Task<IResultModel> GetPageByShopIdAsync([Required] int pageIndex, int pageSize, Guid shopId)
        {
            _logger.LogDebug($"根据 shopId：{shopId} 分页条件：索引页{pageIndex} 单页条数{pageSize} 获取类别分页列表");
            return await _categorieService.Value.GetPageByShopIdAsync(pageIndex, pageSize, shopId);
        }

        [Description("根据 shopId 附加查询条件获取类别分页列表")]
        [ResponseCache(Duration = 0)]
        [Parameters(name = "pageIndex", param = "索引页")]
        [Parameters(name = "pageSize", param = "单页条数")]
        [Parameters(name = "shopId", param = "shopId")]
        [Parameters(name = "code", param = "类别编码")]
        [Parameters(name = "name", param = "类别名称")]
        [HttpGet("GetPageByShopIdWhereQueryAsync")]
        public async Task<IResultModel> GetPageByShopIdWhereQueryAsync([Required] int pageIndex, int pageSize, Guid shopId, string code, string name)
        {
            _logger.LogDebug($"根据 shopId：{shopId} 分页条件：索引页 {pageIndex} 单页条数 {pageSize} 查询条件：类别编码 {code} 类别名称 {name} 获取类别分页列表");
            return await _categorieService.Value.GetPageByShopIdWhereQueryAsync(pageIndex, pageSize, shopId, code, name);
        }

        [Description("根据 shopId、是否排序条件获取类别列表")]
        [ResponseCache(Duration = 0)]
        [Parameters(name = "shopId", param = "shopId")]
        [Parameters(name = "isDescending", param = "是否倒序")]
        [HttpGet("GetListAllByShopIdAsync")]
        public async Task<IResultModel> GetListAllByShopIdAsync(Guid shopId, bool isDescending = false)
        {
            _logger.LogDebug($"根据 shopId：{shopId} 是否倒序：{isDescending}条件获取类别列表");
            return await _categorieService.Value.GetListAllByShopIdAsync(shopId, isDescending);
        }

        [Description("根据 ID 删除类别")]
        [Parameters(name = "id", param = "类别 ID")]
        [HttpDelete("DeleteAsync")]
        [Authorize(Roles = "ShopManager, ShopAssistant")]
        public async Task<IResultModel> DeleteAsync([Required] int id)
        {
            _logger.LogDebug("删除类别");
            return await _categorieService.Value.RemoveAsync(id);
        }

        [Description("根据 ID 集合批量删除类别")]
        [Parameters(name = "ids", param = "类别 ID 集合")]
        [HttpDelete("BatchDeleteAsync")]
        [Authorize(Roles = "ShopManager, ShopAssistant")]
        public async Task<IResultModel> BatchDeleteAsync([FromBody] List<int> ids)
        {
            _logger.LogDebug("批量删除类别");
            return await _categorieService.Value.RemoveAsync(ids);
        }

        [Description("添加类别")]
        [HttpPost("InsertAsync")]
        [Authorize(Roles = "ShopManager, ShopAssistant")]
        public async Task<IResultModel> InsertAsync([FromBody] CategorieCreateDto model)
        {
            _logger.LogDebug("添加类别");
            return await _createCategorieService.Value.InsertAsync(model);
        }

        [Description("Put 修改类别")]
        [HttpPut("UpdateAsync")]
        [Authorize(Roles = "ShopManager, ShopAssistant")]
        public async Task<IResultModel> UpdateAsync([FromBody] CategorieUpdateDto model)
        {
            _logger.LogDebug("修改类别");
            return await _updateCategorieService.Value.UpdateAsync(model);
        }

        [Description("Patch 修改类别")]
        [HttpPatch("PatchAsync")]
        [Authorize(Roles = "ShopManager, ShopAssistant")]
        public async Task<IResultModel> PatchAsync([FromRoute] int id, [FromBody] JsonPatchDocument<CategorieUpdateDto> patchDocument)
        {
            _logger.LogDebug("使用JsonPatch修改类别");
            return await _updateCategorieService.Value.PatchAsync(id, patchDocument);
        }

    }
}
