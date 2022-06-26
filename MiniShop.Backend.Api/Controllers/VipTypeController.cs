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
    [Description("会员类别信息")]
    public class VipTypeController : ControllerAbstract
    {
        private readonly Lazy<IVipTypeService> _vipTypeService;
        private readonly Lazy<ICreateVipTypeService> _createVipTypeService;
        private readonly Lazy<IUpdateVipTypeService> _updateVipTypeService;
        public VipTypeController(ILogger<VipTypeController> logger, Lazy<IMapper> mapper,
            Lazy<IVipTypeService> vipTypeService,
            Lazy<ICreateVipTypeService> createVipTypeService,
            Lazy<IUpdateVipTypeService> updateVipTypeService) : base(logger, mapper)
        {
            _vipTypeService = vipTypeService;
            _createVipTypeService = createVipTypeService;
            _updateVipTypeService = updateVipTypeService;
        }

        [Description("根据 ID 获取会员类别")]
        [ResponseCache(Duration = 0)]
        [Parameters(name = "id", param = "会员类别 ID")]
        [HttpGet("GetByIdAsync")]
        public async Task<IResultModel> GetByIdAsync([Required] int id)
        {
            _logger.LogDebug($"根据会员类别 ID：{id} 获取会员类别");
            return await _vipTypeService.Value.GetByIdAsync(id);
        }

        [Description("根据 shopId、会员类别编码获取会员类别")]
        [ResponseCache(Duration = 0)]
        [Parameters(name = "shopId", param = "shopId")]
        [Parameters(name = "code", param = "会员类别编码")]
        [HttpGet("GetByShopIdCodeAsync")]
        public async Task<IResultModel> GetByShopIdCodeAsync([Required] Guid shopId, int code)
        {
            _logger.LogDebug($"根据 shopId：{shopId} 会员类别编码：{code} 获取会员类别");
            return await _vipTypeService.Value.GetByShopIdCodeAsync(shopId, code);
        }

        [Description("根据 shopId、会员类别等级获取最大会员类别编码")]
        [ResponseCache(Duration = 0)]
        [Parameters(name = "shopId", param = "shopId")]
        [HttpGet("GetMaxCodeByShopIdAsync")]
        public async Task<IResultModel> GetMaxCodeByShopIdAsync([Required] Guid shopId)
        {
            _logger.LogDebug($"根据 shopId：{shopId} 获取最大会员类别编码");
            return await _vipTypeService.Value.GetMaxCodeByShopIdAsync(shopId);
        }

        [Description("根据 shopId 获取供会员类别")]
        [ResponseCache(Duration = 0)]
        [Parameters(name = "shopId", param = "ShopId")]
        [HttpGet("GetByShopIdAsync")]
        public async Task<IResultModel> GetByShopIdAsync([Required] Guid shopId)
        {
            _logger.LogDebug($"根据 ShopId：{shopId} 获取会员类别");
            return await _vipTypeService.Value.GetByShopIdAsync(shopId);
        }

        [Description("根据 shopId 获取会员类别分页列表")]
        [ResponseCache(Duration = 0)]
        [Parameters(name = "pageIndex", param = "索引页")]
        [Parameters(name = "pageSize", param = "单页条数")]
        [Parameters(name = "shopId", param = "shopId")]
        [HttpGet("GetPageByShopIdAsync")]
        public async Task<IResultModel> GetPageByShopIdAsync([Required] int pageIndex, int pageSize, Guid shopId)
        {
            _logger.LogDebug($"根据 shopId：{shopId} 分页条件：索引页{pageIndex} 单页条数{pageSize} 获取会员类别分页列表");
            return await _vipTypeService.Value.GetPageByShopIdAsync(pageIndex, pageSize, shopId);
        }

        [Description("根据 shopId 附加查询条件获取会员类别分页列表")]
        [ResponseCache(Duration = 0)]
        [Parameters(name = "pageIndex", param = "索引页")]
        [Parameters(name = "pageSize", param = "单页条数")]
        [Parameters(name = "shopId", param = "shopId")]
        [Parameters(name = "code", param = "会员类别编码")]
        [Parameters(name = "name", param = "会员类别名称")]
        [HttpGet("GetPageByShopIdWhereQueryAsync")]
        public async Task<IResultModel> GetPageByShopIdWhereQueryAsync([Required] int pageIndex, int pageSize, Guid shopId, string code, string name)
        {
            _logger.LogDebug($"根据 shopId：{shopId} 分页条件：索引页 {pageIndex} 单页条数 {pageSize} 查询条件：会员类别编码 {code} 会员类别名称 {name} 获取会员类别分页列表");
            return await _vipTypeService.Value.GetPageByShopIdWhereQueryAsync(pageIndex, pageSize, shopId, code, name);
        }

        [Description("根据 shopId、是否排序条件获取会员类别列表")]
        [ResponseCache(Duration = 0)]
        [Parameters(name = "shopId", param = "shopId")]
        [Parameters(name = "isDescending", param = "是否倒序")]
        [HttpGet("GetListAllByShopIdAsync")]
        public async Task<IResultModel> GetListAllByShopIdAsync(Guid shopId, bool isDescending = false)
        {
            _logger.LogDebug($"根据 shopId：{shopId} 是否倒序：{isDescending}条件获取会员类别列表");
            return await _vipTypeService.Value.GetListAllByShopIdAsync(shopId, isDescending);
        }

        [Description("根据 ID 删除会员类别")]
        [Parameters(name = "id", param = "会员类别 ID")]
        [HttpDelete("DeleteAsync")]
        [Authorize(Roles = "ShopManager, ShopAssistant")]
        public async Task<IResultModel> DeleteAsync([Required] int id)
        {
            _logger.LogDebug("删除会员类别");
            return await _vipTypeService.Value.RemoveAsync(id);
        }

        [Description("根据 ID 集合批量删除会员类别")]
        [Parameters(name = "ids", param = "会员类别 ID 集合")]
        [HttpDelete("BatchDeleteAsync")]
        [Authorize(Roles = "ShopManager, ShopAssistant")]
        public async Task<IResultModel> BatchDeleteAsync([FromBody] List<int> ids)
        {
            _logger.LogDebug("批量删除会员类别");
            return await _vipTypeService.Value.RemoveAsync(ids);
        }

        [Description("添加会员类别")]
        [HttpPost("InsertAsync")]
        [Authorize(Roles = "ShopManager, ShopAssistant")]
        public async Task<IResultModel> InsertAsync([FromBody] VipTypeCreateDto model)
        {
            _logger.LogDebug("添加会员类别");
            return await _createVipTypeService.Value.InsertAsync(model);
        }

        [Description("Put 修改会员类别")]
        [HttpPut("UpdateAsync")]
        [Authorize(Roles = "ShopManager, ShopAssistant")]
        public async Task<IResultModel> UpdateAsync([FromBody] VipTypeUpdateDto model)
        {
            _logger.LogDebug("修改会员类别");
            return await _updateVipTypeService.Value.UpdateAsync(model);
        }

        [Description("Patch 修改会员类别")]
        [HttpPatch("PatchAsync")]
        [Authorize(Roles = "ShopManager, ShopAssistant")]
        public async Task<IResultModel> PatchAsync([FromRoute] int id, [FromBody] JsonPatchDocument<VipTypeUpdateDto> patchDocument)
        {
            _logger.LogDebug("使用JsonPatch修改会员类别");
            return await _updateVipTypeService.Value.PatchAsync(id, patchDocument);
        }

    }
}
