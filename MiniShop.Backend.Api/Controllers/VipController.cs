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
    [Description("会员信息")]
    public class VipController : ControllerAbstract
    {
        private readonly Lazy<IVipService> _vipService;
        private readonly Lazy<ICreateVipService> _createVipService;
        private readonly Lazy<IUpdateVipService> _updateVipService;
        public VipController(ILogger<VipController> logger, Lazy<IMapper> mapper,
            Lazy<IVipService> vipService,
            Lazy<ICreateVipService> createVipService,
            Lazy<IUpdateVipService> updateVipService) : base(logger, mapper)
        {
            _vipService = vipService;
            _createVipService = createVipService;
            _updateVipService = updateVipService;
        }

        [Description("根据 ID 获取会员")]
        [ResponseCache(Duration = 0)]
        [Parameters(name = "id", param = "会员 ID")]
        [HttpGet("GetByIdAsync")]
        public async Task<IResultModel> GetByIdAsync([Required] int id)
        {
            _logger.LogDebug($"根据会员 ID：{id} 获取会员");
            return await _vipService.Value.GetByIdAsync(id);
        }

        [Description("根据 shopId、会员卡号获取会员")]
        [ResponseCache(Duration = 0)]
        [Parameters(name = "shopId", param = "shopId")]
        [Parameters(name = "code", param = "会员卡号")]
        [HttpGet("GetByShopIdCodeAsync")]
        public async Task<IResultModel> GetByShopIdCodeAsync([Required] Guid shopId, string code)
        {
            _logger.LogDebug($"根据 shopId：{shopId} 会员卡号：{code} 获取会员");
            return await _vipService.Value.GetByShopIdCodeAsync(shopId, code);
        }


        [Description("根据 shopId 获取会员分页列表")]
        [ResponseCache(Duration = 0)]
        [Parameters(name = "pageIndex", param = "索引页")]
        [Parameters(name = "pageSize", param = "单页条数")]
        [Parameters(name = "shopId", param = "shopId")]
        [HttpGet("GetPageByShopIdAsync")]
        public async Task<IResultModel> GetPageByShopIdAsync([Required] int pageIndex, int pageSize, Guid shopId)
        {
            _logger.LogDebug($"根据 shopId：{shopId} 分页条件：索引页{pageIndex} 单页条数{pageSize} 获取会员分页列表");
            return await _vipService.Value.GetPageByShopIdAsync(pageIndex, pageSize, shopId);
        }

        [Description("根据 shopId 附加查询条件获取会员分页列表")]
        [ResponseCache(Duration = 0)]
        [Parameters(name = "pageIndex", param = "索引页")]
        [Parameters(name = "pageSize", param = "单页条数")]
        [Parameters(name = "shopId", param = "shopId")]
        [Parameters(name = "code", param = "会员卡号")]
        [Parameters(name = "name", param = "会员名称")]
        [Parameters(name = "phone", param = "手机号")]
        [HttpGet("GetPageByShopIdWhereQueryAsync")]
        public async Task<IResultModel> GetPageByShopIdWhereQueryAsync([Required] int pageIndex, int pageSize, Guid shopId, string code, string name, string phone)
        {
            _logger.LogDebug($"根据 shopId：{shopId} 分页条件：索引页 {pageIndex} 单页条数 {pageSize} 查询条件：会员编码 {code} 会员名称 {name} 手机号 {phone} 获取会员分页列表");
            return await _vipService.Value.GetPageByShopIdWhereQueryAsync(pageIndex, pageSize, shopId, code, name, phone);
        }

        [Description("根据 ID 删除会员")]
        [Parameters(name = "id", param = "会员 ID")]
        [HttpDelete("DeleteAsync")]
        [Authorize(Roles = "ShopManager, ShopAssistant")]
        public async Task<IResultModel> DeleteAsync([Required] int id)
        {
            _logger.LogDebug("删除会员");
            return await _vipService.Value.RemoveAsync(id);
        }

        [Description("根据 ID 集合批量删除会员")]
        [Parameters(name = "ids", param = "会员 ID 集合")]
        [HttpDelete("BatchDeleteAsync")]
        [Authorize(Roles = "ShopManager, ShopAssistant")]
        public async Task<IResultModel> BatchDeleteAsync([FromBody] List<int> ids)
        {
            _logger.LogDebug("批量删除会员");
            return await _vipService.Value.RemoveAsync(ids);
        }

        [Description("添加会员")]
        [HttpPost("InsertAsync")]
        public async Task<IResultModel> InsertAsync([FromBody] VipCreateDto model)
        {
            _logger.LogDebug("添加会员");
            return await _createVipService.Value.InsertAsync(model);
        }

        [Description("Put 修改会员")]
        [HttpPut("UpdateAsync")]
        [Authorize(Roles = "ShopManager, ShopAssistant")]
        public async Task<IResultModel> UpdateAsync([FromBody] VipUpdateDto model)
        {
            _logger.LogDebug("修改会员");
            return await _updateVipService.Value.UpdateAsync(model);
        }

        [Description("Patch 修改会员")]
        [HttpPatch("PatchAsync")]
        [Authorize(Roles = "ShopManager, ShopAssistant")]
        public async Task<IResultModel> PatchAsync([FromRoute] int id, [FromBody] JsonPatchDocument<VipUpdateDto> patchDocument)
        {
            _logger.LogDebug("使用JsonPatch修改会员");
            return await _updateVipService.Value.PatchAsync(id, patchDocument);
        }

    }
}
