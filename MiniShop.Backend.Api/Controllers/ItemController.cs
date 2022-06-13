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
using Weick.CommonTools.Code.Helper;
using MiniShop.Backend.Api.Config;

namespace MiniShop.Backend.Api.Controllers
{
    [Description("商品信息")]
    public class ItemController : ControllerAbstract
    {
        private readonly Lazy<IItemService> _itemService;
        private readonly Lazy<ICreateItemService> _createItemService;
        private readonly Lazy<IUpdateItemService> _updateItemService;
        public ItemController(ILogger<ItemController> logger, Lazy<IMapper> mapper,
            Lazy<IItemService> itemService,
            Lazy<ICreateItemService> createItemService,
            Lazy<IUpdateItemService> updateItemService) : base(logger, mapper)
        {
            _itemService = itemService;
            _createItemService = createItemService;
            _updateItemService = updateItemService;
        }

        [Description("上传对象存储商品图片")]
        [Parameters(name = "key", param = "图片 key")]
        [Parameters(name = "base64", param = "图片链接 base64")]
        [HttpPost("UploadImgAsync")]
        public async Task<IResultModel> UploadImgAsync([Required] string key, [FromBody] string base64)
        {
            _logger.LogDebug($"上传商品图片 key：{key} base64：{base64}");
            var cosClient = new CosBuilder()
                .SetAccount(BasicSetting.Setting.CosAppId, BasicSetting.Setting.CosRegion)
                .SetCosXmlServer()
                .SetSecret(BasicSetting.Setting.CosSecretId, BasicSetting.Setting.CosSecretKey)
                .Builder();

            var bucketClient = new BucketClient(cosClient, BasicSetting.Setting.CosItemImgBuketName, BasicSetting.Setting.CosAppId);
            var res = await bucketClient.UpFile(key, base64);
            if(res.Code == 200)
            {
                return ResultModel.Success(res.Data);
            }
            else
            {
                return ResultModel.Failed(res.Message, res.Code);
            }
        }

        [Description("删除对象存储商品图片")]
        [Parameters(name = "key", param = "图片 key")]
        [HttpPost("DeleteImgAsync")]
        public async Task<IResultModel> DeleteImgAsync([Required] string key)
        {
            _logger.LogDebug($"删除商品图片 key：{key}");
            var cosClient = new CosBuilder()
                .SetAccount(BasicSetting.Setting.CosAppId, BasicSetting.Setting.CosRegion)
                .SetCosXmlServer()
                .SetSecret(BasicSetting.Setting.CosSecretId, BasicSetting.Setting.CosSecretKey)
                .Builder();

            var bucketClient = new BucketClient(cosClient, BasicSetting.Setting.CosItemImgBuketName, BasicSetting.Setting.CosAppId);
            var res = await bucketClient.DeleteObject(key);
            if(res.Code == 200)
            {
                return ResultModel.Success(res.Data);
            }
            else
            {
                return ResultModel.Failed(res.Message, res.Code);
            }
        }

        [Description("批量删除对象存储商品图片")]
        [Parameters(name = "keys", param = "图片 key 集合")]
        [HttpPost("BatchDeleteImgAsync")]
        public async Task<IResultModel> BatchDeleteImgAsync([FromBody] List<string> keys)
        {
            _logger.LogDebug($"批量删除商品图片");
            var cosClient = new CosBuilder()
                .SetAccount(BasicSetting.Setting.CosAppId, BasicSetting.Setting.CosRegion)
                .SetCosXmlServer()
                .SetSecret(BasicSetting.Setting.CosSecretId, BasicSetting.Setting.CosSecretKey)
                .Builder();

            var bucketClient = new BucketClient(cosClient, BasicSetting.Setting.CosItemImgBuketName, BasicSetting.Setting.CosAppId);
            var res = await bucketClient.BatchDeleteObject(keys);
            if(res.Code == 200)
            {
                return ResultModel.Success(res.Data);
            }
            else
            {
                return ResultModel.Failed(res.Message, res.Code);
            }
        }

        [Description("根据 ID 获取商品")]
        [ResponseCache(Duration = 0)]
        [Parameters(name = "id", param = "商品ID")]
        [HttpGet("GetByIdAsync")]
        public async Task<IResultModel> GetByIdAsync([Required] int id)
        {
            _logger.LogDebug($"根据商品 ID：{id} 获取商品");
            return await _itemService.Value.GetByIdAsync(id);
        }

        [Description("根据 shopId、商品编码获取商品")]
        [ResponseCache(Duration = 0)]
        [Parameters(name = "shopId", param = "shopId")]
        [Parameters(name = "code", param = "商品编码")]
        [HttpGet("GetByShopIdCodeAsync")]
        public async Task<IResultModel> GetByShopIdCodeAsync([Required] Guid shopId, string code)
        {
            _logger.LogDebug($"根据 shopId：{shopId} 商品编码：{code} 获取商品");
            return await _itemService.Value.GetByShopIdCodeAsync(shopId, code);
        }

        [Description("根据 shopId 获取商品分页列表")]
        [ResponseCache(Duration = 0)]
        [Parameters(name = "pageIndex", param = "索引页")]
        [Parameters(name = "pageSize", param = "单页条数")]
        [Parameters(name = "shopId", param = "shopId")]
        [HttpGet("GetPageByShopIdAsync")]
        public async Task<IResultModel> GetPageByShopIdAsync([Required] int pageIndex, int pageSize, Guid shopId)
        {
            _logger.LogDebug($"根据 shopId：{shopId} 分页条件：索引页{pageIndex} 单页条数{pageSize} 获取商品分页列表");
            return await _itemService.Value.GetPageByShopIdAsync(pageIndex, pageSize, shopId);
        }

        [Description("根据 shopId 附加查询条件获取商品分页列表")]
        [ResponseCache(Duration = 0)]
        [Parameters(name = "pageIndex", param = "索引页")]
        [Parameters(name = "pageSize", param = "单页条数")]
        [Parameters(name = "shopId", param = "shopId")]
        [Parameters(name = "code", param = "商品编码")]
        [Parameters(name = "name", param = "商品名称")]
        [HttpGet("GetPageByShopIdWhereQueryAsync")]
        public async Task<IResultModel> GetPageByShopIdWhereQueryAsync([Required] int pageIndex, int pageSize, Guid shopId, string code, string name)
        {
            _logger.LogDebug($"根据 shopId：{shopId} 分页条件：索引页 {pageIndex} 单页条数 {pageSize} 查询条件：商品编码 {code} 商品名称 {name} 获取商品分页列表");
            return await _itemService.Value.GetPageByShopIdWhereQueryAsync(pageIndex, pageSize, shopId, code, name);
        }

        [Description("根据 shopId、是否排序条件获取商品列表")]
        [ResponseCache(Duration = 0)]
        [Parameters(name = "shopId", param = "shopId")]
        [Parameters(name = "isDescending", param = "是否倒序")]
        [HttpGet("GetListAllByShopIdAsync")]
        public async Task<IResultModel> GetListAllByShopIdAsync(Guid shopId, bool isDescending = false)
        {
            _logger.LogDebug($"根据 shopId：{shopId} 是否倒序：{isDescending}条件获取商品列表");
            return await _itemService.Value.GetListAllByShopIdAsync(shopId, isDescending);
        }

        [Description("根据 ID 删除商品")]
        [Parameters(name = "id", param = "商品 ID")]
        [HttpDelete("DeleteAsync")]
        [Authorize(Roles = "ShopManager, ShopAssistant")]
        public async Task<IResultModel> DeleteAsync([Required] int id)
        {
            _logger.LogDebug("删除商品");
            return await _itemService.Value.RemoveAsync(id);
        }

        [Description("根据 ID 集合批量删除商品")]
        [Parameters(name = "ids", param = "商品 ID 集合")]
        [HttpDelete("BatchDeleteAsync")]
        [Authorize(Roles = "ShopManager, ShopAssistant")]
        public async Task<IResultModel> BatchDeleteAsync([FromBody] List<int> ids)
        {
            _logger.LogDebug("批量删除商品");
            return await _itemService.Value.RemoveAsync(ids);
        }

        [Description("添加商品")]
        [HttpPost("InsertAsync")]
        [Authorize(Roles = "ShopManager, ShopAssistant")]
        public async Task<IResultModel> InsertAsync([FromBody] ItemCreateDto model)
        {
            _logger.LogDebug("添加商品");
            return await _createItemService.Value.InsertAsync(model);
        }

        [Description("Put 修改商品")]
        [HttpPut("UpdateAsync")]
        [Authorize(Roles = "ShopManager, ShopAssistant")]
        public async Task<IResultModel> UpdateAsync([FromBody] ItemUpdateDto model)
        {
            _logger.LogDebug("修改商品");
            return await _updateItemService.Value.UpdateAsync(model);
        }

        [Description("Patch 修改商品")]
        [HttpPatch("PatchAsync")]
        [Authorize(Roles = "ShopManager, ShopAssistant")]
        public async Task<IResultModel> PatchAsync([FromRoute] int id, [FromBody] JsonPatchDocument<ItemUpdateDto> patchDocument)
        {
            _logger.LogDebug("使用JsonPatch修改商品");
            return await _updateItemService.Value.PatchAsync(id, patchDocument);
        }

    }
}
