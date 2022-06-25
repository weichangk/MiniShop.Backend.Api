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
using MiniShop.Backend.Model.Enums;

namespace MiniShop.Backend.Api.Controllers
{
    [Description("会员积分设置信息")]
    public class VipScoreSettingController : ControllerAbstract
    {
        private readonly Lazy<IVipScoreSettingService> _vipScoreSettingService;
        private readonly Lazy<ICreateVipScoreSettingService> _createVipScoreSettingService;
        private readonly Lazy<IUpdateVipScoreSettingService> _updateVipScoreSettingService;
        public VipScoreSettingController(ILogger<VipScoreSettingController> logger, Lazy<IMapper> mapper,
            Lazy<IVipScoreSettingService> vipScoreSettingService,
            Lazy<ICreateVipScoreSettingService> createVipScoreSettingService,
            Lazy<IUpdateVipScoreSettingService> updateVipScoreSettingService) : base(logger, mapper)
        {
            _vipScoreSettingService = vipScoreSettingService;
            _createVipScoreSettingService = createVipScoreSettingService;
            _updateVipScoreSettingService = updateVipScoreSettingService;
        }

        [Description("根据 ID 获取会员积分设置")]
        [ResponseCache(Duration = 0)]
        [Parameters(name = "id", param = "会员积分设置 ID")]
        [HttpGet("GetByIdAsync")]
        public async Task<IResultModel> GetByIdAsync([Required] int id)
        {
            _logger.LogDebug($"根据会员积分设置 ID：{id} 获取会员积分设置");
            return await _vipScoreSettingService.Value.GetByIdAsync(id);
        }

        [Description("根据 shopId、会员类别ID、会员积分方式 获取会员积分设置")]
        [ResponseCache(Duration = 0)]
        [Parameters(name = "shopId", param = "shopId")]
        [Parameters(name = "vipTypeId", param = "会员类别ID")]
        [Parameters(name = "vipScoreWay", param = "会员积分方式")]
        [HttpGet("GetByShopIdVipTypeIdVipScoreWayAsync")]
        public async Task<IResultModel> GetByShopIdVipTypeIdVipScoreWayAsync([Required] Guid shopId, int vipTypeId, EnumVipScoreWay vipScoreWay)
        {
            _logger.LogDebug($"根据 shopId：{shopId} 会员类别ID:{vipTypeId} 会员积分方式:{vipScoreWay} 获取会员积分设置");
            return await _vipScoreSettingService.Value.GetByShopIdVipTypeIdVipScoreWayAsync(shopId, vipTypeId, vipScoreWay);
        }

        [Description("根据 shopId 获取会员积分设置分页列表")]
        [ResponseCache(Duration = 0)]
        [Parameters(name = "pageIndex", param = "索引页")]
        [Parameters(name = "pageSize", param = "单页条数")]
        [Parameters(name = "shopId", param = "shopId")]
        [HttpGet("GetPageByShopIdAsync")]
        public async Task<IResultModel> GetPageByShopIdAsync([Required] int pageIndex, int pageSize, Guid shopId)
        {
            _logger.LogDebug($"根据 shopId：{shopId} 分页条件：索引页{pageIndex} 单页条数{pageSize} 获取会员积分设置分页列表");
            return await _vipScoreSettingService.Value.GetPageByShopIdAsync(pageIndex, pageSize, shopId);
        }

        [Description("根据 ID 删除会员积分设置")]
        [Parameters(name = "id", param = "会员积分设置 ID")]
        [HttpDelete("DeleteAsync")]
        [Authorize(Roles = "ShopManager, ShopAssistant")]
        public async Task<IResultModel> DeleteAsync([Required] int id)
        {
            _logger.LogDebug("删除会员积分设置");
            return await _vipScoreSettingService.Value.RemoveAsync(id);
        }

        [Description("根据 ID 集合批量删除会员积分设置")]
        [Parameters(name = "ids", param = "会员积分设置 ID 集合")]
        [HttpDelete("BatchDeleteAsync")]
        [Authorize(Roles = "ShopManager, ShopAssistant")]
        public async Task<IResultModel> BatchDeleteAsync([FromBody] List<int> ids)
        {
            _logger.LogDebug("批量删除会员积分设置");
            return await _vipScoreSettingService.Value.RemoveAsync(ids);
        }

        [Description("添加会员积分设置")]
        [HttpPost("InsertAsync")]
        [Authorize(Roles = "ShopManager, ShopAssistant")]
        public async Task<IResultModel> InsertAsync([FromBody] VipScoreSettingCreateDto model)
        {
            _logger.LogDebug("添加会员积分设置");
            return await _createVipScoreSettingService.Value.InsertAsync(model);
        }

        [Description("Put 修改会员积分设置")]
        [HttpPut("UpdateAsync")]
        [Authorize(Roles = "ShopManager, ShopAssistant")]
        public async Task<IResultModel> UpdateAsync([FromBody] VipScoreSettingUpdateDto model)
        {
            _logger.LogDebug("修改会员积分设置");
            return await _updateVipScoreSettingService.Value.UpdateAsync(model);
        }

        [Description("Patch 修改会员积分设置")]
        [HttpPatch("PatchAsync")]
        [Authorize(Roles = "ShopManager, ShopAssistant")]
        public async Task<IResultModel> PatchAsync([FromRoute] int id, [FromBody] JsonPatchDocument<VipScoreSettingUpdateDto> patchDocument)
        {
            _logger.LogDebug("使用JsonPatch修改会员积分设置");
            return await _updateVipScoreSettingService.Value.PatchAsync(id, patchDocument);
        }

    }
}
