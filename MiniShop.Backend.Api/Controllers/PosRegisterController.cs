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
    [Description("POS登记信息")]
    public class PosRegisterController : ControllerAbstract
    {
        private readonly Lazy<IPosRegisterService> _posRegisterService;
        private readonly Lazy<ICreatePosRegisterService> _createPosRegisterService;
        private readonly Lazy<IUpdatePosRegisterService> _updatePosRegisterService;
        public PosRegisterController(ILogger<PosRegisterController> logger, Lazy<IMapper> mapper,
            Lazy<IPosRegisterService> posRegisterService,
            Lazy<ICreatePosRegisterService> createPosRegisterService,
            Lazy<IUpdatePosRegisterService> updatePosRegisterService) : base(logger, mapper)
        {
            _posRegisterService = posRegisterService;
            _createPosRegisterService = createPosRegisterService;
            _updatePosRegisterService = updatePosRegisterService;
        }

        [Description("根据 ID 获取POS登记")]
        [ResponseCache(Duration = 0)]
        [Parameters(name = "id", param = "POS登记ID")]
        [HttpGet("GetByIdAsync")]
        public async Task<IResultModel> GetByIdAsync([Required] int id)
        {
            _logger.LogDebug($"根据POS登记 ID：{id} 查询POS登记");
            return await _posRegisterService.Value.GetByIdAsync(id);
        }

        [Description("根据POS登记编码获取POS登记")]
        [ResponseCache(Duration = 0)]
        [Parameters(name = "code", param = "POS登记编码")]
        [HttpGet("GetByCodeAsync")]
        public async Task<IResultModel> GetByCodeAsync([Required]string code)
        {
            _logger.LogDebug($"根据POS登记编码：{code} 获取POS登记");
            return await _posRegisterService.Value.GetByCodeAsync(code);
        }

        [Description("获取POS登记分页列表")]
        [ResponseCache(Duration = 0)]
        [Parameters(name = "pageIndex", param = "索引页")]
        [Parameters(name = "pageSize", param = "单页条数")]
        [HttpGet("GetPageAsync")]
        public async Task<IResultModel> GetPageAsync([Required] int pageIndex, int pageSize, Guid shopId)
        {
            _logger.LogDebug($"根据分页条件：索引页{pageIndex} 单页条数{pageSize} 获取POS登记分页列表");
            return await _posRegisterService.Value.GetPageAsync(pageIndex, pageSize);
        }

        [Description("根据查询条件获取POS登记分页列表")]
        [ResponseCache(Duration = 0)]
        [Parameters(name = "pageIndex", param = "索引页")]
        [Parameters(name = "pageSize", param = "单页条数")]
        [Parameters(name = "code", param = "POS登记编码")]
        [Parameters(name = "name", param = "POS登记名称")]
        [HttpGet("GetPageWhereQueryAsync")]
        public async Task<IResultModel> GetPageWhereQueryAsync([Required] int pageIndex, int pageSize, Guid shopId, string code, string name)
        {
            _logger.LogDebug($"根据分页条件：索引页 {pageIndex} 单页条数 {pageSize} 查询条件：POS登记编码 {code} POS登记名称 {name} 获取POS登记分页列表");
            return await _posRegisterService.Value.GetPageWhereQueryAsync(pageIndex, pageSize, code, name);
        }

        [Description("根据 ID 删除POS登记")]
        [Parameters(name = "id", param = "POS登记 ID")]
        [HttpDelete("DeleteAsync")]
        [Authorize(Roles = "ShopManager, ShopAssistant")]
        public async Task<IResultModel> DeleteAsync([Required] int id)
        {
            _logger.LogDebug("删除POS登记");
            return await _posRegisterService.Value.RemoveAsync(id);
        }

        [Description("根据 ID 集合批量删除POS登记")]
        [Parameters(name = "ids", param = "POS登记 ID 集合")]
        [HttpDelete("BatchDeleteAsync")]
        [Authorize(Roles = "ShopManager, ShopAssistant")]
        public async Task<IResultModel> BatchDeleteAsync([FromBody] List<int> ids)
        {
            _logger.LogDebug("批量删除POS登记");
            return await _posRegisterService.Value.RemoveAsync(ids);
        }

        [Description("添加POS登记")]
        [HttpPost("InsertAsync")]
        [Authorize(Roles = "ShopManager, ShopAssistant")]
        public async Task<IResultModel> InsertAsync([FromBody] PosRegisterCreateDto model)
        {
            _logger.LogDebug("添加POS登记");
            return await _createPosRegisterService.Value.InsertAsync(model);
        }

        [Description("Put 修改POS登记")]
        [HttpPut("UpdateAsync")]
        [Authorize(Roles = "ShopManager, ShopAssistant")]
        public async Task<IResultModel> UpdateAsync([FromBody] PosRegisterUpdateDto model)
        {
            _logger.LogDebug("修改POS登记");
            return await _updatePosRegisterService.Value.UpdateAsync(model);
        }

        [Description("Patch 修改POS登记")]
        [HttpPatch("PatchAsync")]
        [Authorize(Roles = "ShopManager, ShopAssistant")]
        public async Task<IResultModel> PatchAsync([FromRoute] int id, [FromBody] JsonPatchDocument<PosRegisterUpdateDto> patchDocument)
        {
            _logger.LogDebug("使用JsonPatch修改POS登记");
            return await _updatePosRegisterService.Value.PatchAsync(id, patchDocument);
        }

    }
}
