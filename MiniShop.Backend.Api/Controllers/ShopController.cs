using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MiniShop.Backend.Model.Dto;
using MiniShop.Backend.Api.Services;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Weick.Orm.Core.Result;
using Microsoft.AspNetCore.JsonPatch;
using MiniShop.Backend.Model.Enums;

namespace MiniShop.Backend.Api.Controllers
{
    [Description("商店信息")]
    public class ShopController : ControllerAbstract
    {
        private readonly Lazy<IShopService> _shopService;
        private readonly Lazy<IShopCreateService> _shopCreateService;
        private readonly Lazy<IShopUpdateService> _shopUpdateService;

        public ShopController(ILogger<ShopController> logger, Lazy<IMapper> mapper,
            Lazy<IShopService> shopService,
            Lazy<IShopCreateService> shopCreateService,
            Lazy<IShopUpdateService> shopUpdateService) : base(logger, mapper)
        {
            _shopService = shopService;
            _shopCreateService = shopCreateService;
            _shopUpdateService = shopUpdateService;
        }

        [Description("根据 ShopId 获取商店")]
        [ResponseCache(Duration = 0)]
        [Parameters(name = "shopId", param = "ShopId")]
        [HttpGet("GetByShopIdAsync")]
        public async Task<IResultModel> GetByShopIdAsync([Required] Guid shopId)
        {
            _logger.LogDebug($"根据 ShopId：{shopId } 获取商店");
            return await _shopService.Value.GetByShopIdAsync(shopId);
        }

        [Description("新增商店")]
        [Authorize(Roles = nameof(EnumRole.ShopManager))]
        [HttpPost("InsertAsync")]
        public async Task<IResultModel> InsertAsync([FromBody] ShopCreateDto model)
        {
            _logger.LogDebug("新增商店");
            return await _shopCreateService.Value.InsertAsync(model);
        }

        [Description("Put 修改商店")]
        [Authorize(Roles = nameof(EnumRole.ShopManager))]
        [HttpPut("UpdateAsync")]
        public async Task<IResultModel> UpdateAsync([FromBody] ShopUpdateDto model)
        {
            _logger.LogDebug("Put 修改商店");
            return await _shopUpdateService.Value.UpdateAsync(model);
        }

        [Description("Patch 修改商店")]
        [Parameters(name = "id", param = "商店ID")]
        [Authorize(Roles = nameof(EnumRole.ShopManager))]
        [HttpPatch("PatchAsync")]
        public async Task<IResultModel> PatchAsync([Required] int id, [FromBody] JsonPatchDocument<ShopUpdateDto> patchDocument)
        {
            _logger.LogDebug("Patch 修改商店");
            return await _shopUpdateService.Value.PatchAsync(id, patchDocument);
        }

    }
}
