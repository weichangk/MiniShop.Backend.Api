using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MiniShop.Backend.Model.Dto;
using MiniShop.Backend.Api.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Weick.Orm.Core.Result;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using System.Linq;
using System.Net;

namespace MiniShop.Backend.Api.Controllers
{
    [Description("门店信息")]
    public class StoreController : ControllerAbstract
    {
        private readonly Lazy<IStoreService> _storeService;
        private readonly Lazy<ICreateStoreService> _createStoreService;
        private readonly Lazy<IUpdateStoreService> _updateStoreService;
        private readonly Lazy<UserManager<IdentityUser>> _userManager;

        public StoreController(ILogger<StoreController> logger, Lazy<IMapper> mapper, Lazy<IStoreService> storeService,
            Lazy<ICreateStoreService> createStoreService,
            Lazy<IUpdateStoreService> updateStoreService,
            Lazy<UserManager<IdentityUser>> userManager) : base(logger, mapper)
        {
            _storeService = storeService;
            _updateStoreService = updateStoreService;
            _createStoreService = createStoreService;
            _userManager = userManager;
        }

        [Description("根据 ID 获取门店")]
        [ResponseCache(Duration = 0)]
        [Parameters(name = "id", param = "门店 ID")]
        [HttpGet("GetByIdAsync")]
        public async Task<IResultModel> GetByIdAsync([Required] int id)
        {
            _logger.LogDebug($"根据 ID：{id} 获取门店");
            return await _storeService.Value.GetByIdAsync(id);
        }

        [Description("根据 shopId 获取门店")]
        [ResponseCache(Duration = 0)]
        [Parameters(name = "storeId", param = "shopId")]
        [HttpGet("GetByStoreIdAsync")]
        public async Task<IResultModel> GetByStoreIdAsync([Required] Guid storeId)
        {
            _logger.LogDebug($"根据 StoreId：{storeId} 获取门店");
            return await _storeService.Value.GetByStoreIdAsync(storeId);
        }

        [Description("根据 shopId 获取门店")]
        [ResponseCache(Duration = 0)]
        [Parameters(name = "shopId", param = "shopId")]
        [HttpGet("GetByShopIdAsync")]
        public async Task<IResultModel> GetByShopIdAsync([Required] Guid shopId)
        {
            _logger.LogDebug($"根据 ShopId：{shopId} 获取门店");
            return await _storeService.Value.GetByShopIdAsync(shopId);
        }

        [Description("根据 shopId 获取门店分页列表")]
        [ResponseCache(Duration = 0)]
        [Parameters(name = "pageIndex", param = "索引页")]
        [Parameters(name = "pageSize", param = "单页条数")]
        [Parameters(name = "shopId", param = "shopId")]
        [HttpGet("GetPageByShopIdAsync")]
        public async Task<IResultModel> GetPageByShopIdAsync([Required] int pageIndex, int pageSize, Guid shopId)
        {
            _logger.LogDebug($"根据 shopId：{shopId} 分页条件：索引页{pageIndex} 单页条数{pageSize} 获取门店分页列表");
            return await _storeService.Value.GetPageByShopIdAsync(pageIndex, pageSize, shopId);
        }

        [Description("根据 shopId 附加查询条件获取门店分页列表")]
        [ResponseCache(Duration = 0)]
        [Parameters(name = "pageIndex", param = "索引页")]
        [Parameters(name = "pageSize", param = "单页条数")]
        [Parameters(name = "shopId", param = "shopId")]
        [Parameters(name = "name", param = "用户名")]
        [HttpGet("GetPageByShopIdWhereQueryAsync")]
        public async Task<IResultModel> GetPageByShopIdWhereQueryAsync([Required] int pageIndex, int pageSize, Guid shopId, string name)
        {
            _logger.LogDebug($"根据 shopId：{shopId} 分页条件：索引页 {pageIndex} 单页条数 {pageSize} 查询条件：门店名称 {name} 获取分页列表");
            return await _storeService.Value.GetPageByShopIdWhereQueryAsync(pageIndex, pageSize, shopId, name);
        }

        [Description("根据 ID 删除门店")]
        [Parameters(name = "id", param = "门店 ID")]
        [HttpDelete("DeleteAsync")]
        [Authorize(Roles = "ShopManager, ShopAssistant")]
        public async Task<IResultModel> DeleteAsync([Required] int id)
        {
            _logger.LogDebug("删除门店");
            var store = (ResultModel<StoreDto>) (await _storeService.Value.GetByIdAsync(id));
            if (store == null || store.Data == null)
            {
                _logger.LogError($"门店删除错误：{id} 不存在");
                return ResultModel.Failed($"用户删除错误：{id} 不存在", (int)HttpStatusCode.NotFound);
            }
            else
            {
                if (store.Data.ShopId == store.Data.StoreId)
                {
                    return ResultModel.Failed("不能删除系统默认门店", (int)HttpStatusCode.Forbidden);
                }
                var users = await _userManager.Value.GetUsersForClaimAsync(new Claim("storeid", store.Data.StoreId.ToString()));
                foreach (var u in users)
                {
                    var result = await _userManager.Value.DeleteAsync(u);
                    if (!result.Succeeded)
                    {
                        _logger.LogError($"用户删除错误：{result.Errors.FirstOrDefault().Description}");
                        return ResultModel.Failed("用户删除错误：{name} 删除失败", (int)HttpStatusCode.InternalServerError);
                    }
                }
                return await _storeService.Value.RemoveAsync(id);
            }
        }

        [Description("根据 ID 集合批量删除门店")]
        [Parameters(name = "ids", param = "门店 ID 集合")]
        [HttpDelete("BatchDeleteAsync")]
        [Authorize(Roles = "ShopManager, ShopAssistant")]
        public async Task<IResultModel> BatchDeleteAsync([FromBody] List<int> ids)
        {
            _logger.LogDebug("批量删除门店");
            foreach (var id in ids)
            {
                var store = (ResultModel<StoreDto>)(await _storeService.Value.GetByIdAsync(id));
                if (store == null || store.Data == null)
                {
                    _logger.LogError($"门店删除错误：{id} 不存在");
                    return ResultModel.Failed($"用户删除错误：{id} 不存在", (int)HttpStatusCode.NotFound);
                }
                else
                {
                    if (store.Data.ShopId == store.Data.StoreId)
                    {
                        return ResultModel.Failed("不能删除系统默认门店", (int)HttpStatusCode.Forbidden);
                    }
                }
            }

            foreach (var id in ids)
            {
                var store = (ResultModel<StoreDto>)(await _storeService.Value.GetByIdAsync(id));
                var users = await _userManager.Value.GetUsersForClaimAsync(new Claim("storeid", store.Data.StoreId.ToString()));
                foreach (var u in users)
                {
                    var result = await _userManager.Value.DeleteAsync(u);
                    if (!result.Succeeded)
                    {
                        _logger.LogError($"用户删除错误：{result.Errors.FirstOrDefault().Description}");
                        return ResultModel.Failed("用户删除错误：{name} 删除失败", (int)HttpStatusCode.InternalServerError);
                    }
                }
            }
            return await _storeService.Value.RemoveAsync(ids);
        }

        [Description("添加门店")]
        [HttpPost("InsertAsync")]
        [Authorize(Roles = "ShopManager, ShopAssistant")]
        public async Task<IResultModel> InsertAsync([FromBody] StoreCreateDto model)
        {
            _logger.LogDebug("添加门店");
            return await _createStoreService.Value.InsertAsync(model);
        }

        [Description("Put 修改门店")]
        [HttpPut("UpdateAsync")]
        [Authorize(Roles = "ShopManager, ShopAssistant")]
        public async Task<IResultModel> UpdateAsync([FromBody] StoreUpdateDto model)
        {
            _logger.LogDebug("修改门店");
            return await _updateStoreService.Value.UpdateAsync(model);
        }

        [Description("Patch 修改门店")]
        [HttpPatch("PatchAsync")]
        [Authorize(Roles = "ShopManager, ShopAssistant")]
        public async Task<IResultModel> PatchAsync([FromRoute] int id, [FromBody] JsonPatchDocument<StoreUpdateDto> patchDocument)
        {
            _logger.LogDebug("使用 JsonPatch 修改门店");
            return await _updateStoreService.Value.PatchAsync(id, patchDocument);
        }
    }
}
