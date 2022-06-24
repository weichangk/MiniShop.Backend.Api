using System.Runtime.InteropServices;
using System.Runtime.InteropServices.ComTypes;
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
using System.Threading.Tasks;
using Weick.Orm.Core;
using System.Linq;

namespace MiniShop.Backend.Api.Controllers
{
    [Description("销售订单信息")]
    public class BillController : ControllerAbstract
    {
        private readonly Lazy<IBillInfoService> _billInfoService;
        private readonly Lazy<ICreateBillInfoService> _createBillInfoService;
        private readonly Lazy<IPayFlowService> _payFlowService;
        private readonly Lazy<ICreatePayFlowService> _createPayFlowService;
        private readonly Lazy<ISaleFlowService> _saleFlowService;
        private readonly Lazy<ICreateSaleFlowService> _createSaleFlowService;
        public BillController(ILogger<BillController> logger, Lazy<IMapper> mapper,
            Lazy<IBillInfoService> billInfoService,
            Lazy<ICreateBillInfoService> createBillInfoService,
            Lazy<IPayFlowService> payFlowService,
            Lazy<ICreatePayFlowService> createPayFlowService,
            Lazy<ISaleFlowService> saleFlowService,
            Lazy<ICreateSaleFlowService> createSaleFlowService) : base(logger, mapper)
        {
            _billInfoService = billInfoService;
            _createBillInfoService = createBillInfoService;
            _payFlowService = payFlowService;
            _createPayFlowService = createPayFlowService;
            _saleFlowService = saleFlowService;
            _createSaleFlowService = createSaleFlowService;
        }

        #region BillInfo 销售订单
        [Description("根据 ID 获取销售订单")]
        [ResponseCache(Duration = 0)]
        [Parameters(name = "id", param = "ID")]
        [HttpGet("GetBillInfoByIdAsync")]
        public async Task<IResultModel> GetBillInfoByIdAsync([Required] int id)
        {
            _logger.LogDebug($"根据 ID：{id} 获取销售订单");
            return await _billInfoService.Value.GetByIdAsync(id);
        }

        [Description("根据 shopId、单号 获取销售订单")]
        [ResponseCache(Duration = 0)]
        [Parameters(name = "shopId", param = "shopId")]
        [Parameters(name = "billNo", param = "订单号")]
        [HttpGet("GetBillInfoByShopIdAndBillNoAsync")]
        public async Task<IResultModel> GetBillInfoByShopIdAndBillNoAsync([Required] Guid shopId, string billNo)
        {
            _logger.LogDebug($"根据 shopId{shopId}订单号{billNo} 获取销售订单");
            return await _billInfoService.Value.GetByShopIdAndBillNoAsync(shopId, billNo);
        }  
        [Description("根据 shopId 获取销售订单分页列表")]
        [ResponseCache(Duration = 0)]
        [Parameters(name = "pageIndex", param = "索引页")]
        [Parameters(name = "pageSize", param = "单页条数")]
        [Parameters(name = "shopId", param = "shopId")]
        [HttpGet("GetBillInfoPageByShopIdAsync")]
        public async Task<IResultModel> GetBillInfoPageByShopIdAsync([Required] int pageIndex, int pageSize, Guid shopId)
        {
            _logger.LogDebug($"根据 shopId：{shopId} 分页条件：索引页{pageIndex} 单页条数{pageSize} 获取销售订单分页列表");
            return await _billInfoService.Value.GetPageByShopIdAsync(pageIndex, pageSize, shopId);
        }

        [Description("根据 shopId 获取付款流水销售订单分页列表")]
        [ResponseCache(Duration = 0)]
        [Parameters(name = "pageIndex", param = "索引页")]
        [Parameters(name = "pageSize", param = "单页条数")]
        [Parameters(name = "shopId", param = "shopId")]
        [HttpGet("GetPayFlowBillInfoByShopIdAsync")]
        public async Task<IResultModel> GetPayFlowBillInfoByShopIdAsync([Required] int pageIndex, int pageSize, Guid shopId)
        {
            _logger.LogDebug($"根据 shopId：{shopId} 分页条件：索引页{pageIndex} 单页条数{pageSize} 获取付款流水销售订单分页列表");
            List<PayFlowBillInfoDto> list = new List<PayFlowBillInfoDto>();
            var getPayFlow = (ResultModel<PagedList<PayFlowDto>>)(await _payFlowService.Value.GetPageByShopIdAsync(pageIndex, pageSize, shopId));
            if(getPayFlow.Success)
            {
                foreach(var item in getPayFlow.Data.Item)
                {
                    PayFlowBillInfoDto model = _mapper.Value.Map<PayFlowBillInfoDto>(item);
                    var getBillInfo = (ResultModel<BillInfoDto>)(await _billInfoService.Value.GetByShopIdAndBillNoAsync(shopId, item.BillNo));
                    if(getBillInfo.Success)
                    {
                        model.OperatorName = getBillInfo.Data.OperatorName;
                        model.SaleWay = getBillInfo.Data.SaleWay;
                        model.SaleMoney = getBillInfo.Data.SaleMoney;
                        model.MemberId = getBillInfo.Data.MemberId;

                        list.Add(model);
                    }
                    else
                    {
                        return ResultModel.Failed("error：GetPayFlowBillInfoageByShopIdAsync failed", 500); 
                    }         
                }
                var pageList = list.AsQueryable<PayFlowBillInfoDto>().ToPagedList(pageIndex, pageSize);
                return ResultModel.Success(pageList);
            }
            else
            {
                return ResultModel.Failed("error：GetPayFlowBillInfoageByShopIdAsync failed", 500); 
            }
        }

        [Description("根据 shopId 附加查询条件获取付款流水销售订单分页列表")]
        [ResponseCache(Duration = 0)]
        [Parameters(name = "pageIndex", param = "索引页")]
        [Parameters(name = "pageSize", param = "单页条数")]
        [Parameters(name = "shopId", param = "shopId")]
        [Parameters(name = "startTime", param = "查询起始时间")]
        [Parameters(name = "endTime", param = "查询结束时间")]
        [HttpGet("GetPayFlowBillInfoByShopIdWhereQueryAsync")]
        public async Task<IResultModel> GetPayFlowBillInfoByShopIdWhereQueryAsync([Required] int pageIndex, int pageSize, Guid shopId, DateTime? startTime, DateTime? endTime)
        {
            _logger.LogDebug($"根据 shopId：{shopId} 分页条件：索引页{pageIndex} 单页条数{pageSize} 查询起始时间{startTime} 查询结束时间{endTime} 获取付款流水销售订单分页列表");
            List<PayFlowBillInfoDto> list = new List<PayFlowBillInfoDto>();
            var getPayFlow = (ResultModel<PagedList<PayFlowDto>>)(await _payFlowService.Value.GetPageByShopIdWhereQueryAsync(pageIndex, pageSize, shopId, startTime, endTime));
            if(getPayFlow.Success)
            {
                foreach(var item in getPayFlow.Data.Item)
                {
                    PayFlowBillInfoDto model = _mapper.Value.Map<PayFlowBillInfoDto>(item);
                    var getBillInfo = (ResultModel<BillInfoDto>)(await _billInfoService.Value.GetByShopIdAndBillNoAsync(shopId, item.BillNo));
                    if(getBillInfo.Success)
                    {
                        model.OperatorName = getBillInfo.Data.OperatorName;
                        model.SaleWay = getBillInfo.Data.SaleWay;
                        model.SaleMoney = getBillInfo.Data.SaleMoney;
                        model.MemberId = getBillInfo.Data.MemberId;

                        list.Add(model);
                    }
                    else
                    {
                        return ResultModel.Failed("error：GetPayFlowBillInfoageByShopIdWhereQueryAsync failed", 500); 
                    }         
                }
                var pageList = list.AsQueryable<PayFlowBillInfoDto>().ToPagedList(pageIndex, pageSize);
                return ResultModel.Success(pageList);
            }
            else
            {
                return ResultModel.Failed("error：GetPayFlowBillInfoageByShopIdWhereQueryAsync failed", 500); 
            }
        }

        [Description("根据 shopId 获取销售流水销售订单分页列表")]
        [ResponseCache(Duration = 0)]
        [Parameters(name = "pageIndex", param = "索引页")]
        [Parameters(name = "pageSize", param = "单页条数")]
        [Parameters(name = "shopId", param = "shopId")]
        [HttpGet("GetSaleFlowBillInfoByShopIdAsync")]
        public async Task<IResultModel> GetSaleFlowBillInfoByShopIdAsync([Required] int pageIndex, int pageSize, Guid shopId)
        {
            _logger.LogDebug($"根据 shopId：{shopId} 分页条件：索引页{pageIndex} 单页条数{pageSize} 获取销售流水销售订单分页列表");
            List<SaleFlowBillInfoDto> list = new List<SaleFlowBillInfoDto>();
            var getSaleFlow = (ResultModel<PagedList<SaleFlowDto>>)(await _saleFlowService.Value.GetPageByShopIdAsync(pageIndex, pageSize, shopId));
            if(getSaleFlow.Success)
            {
                foreach(var item in getSaleFlow.Data.Item)
                {
                    SaleFlowBillInfoDto model = _mapper.Value.Map<SaleFlowBillInfoDto>(item);
                    var getBillInfo = (ResultModel<BillInfoDto>)(await _billInfoService.Value.GetByShopIdAndBillNoAsync(shopId, item.BillNo));
                    if(getBillInfo.Success)
                    {
                        model.OperatorName = getBillInfo.Data.OperatorName;
                        model.SaleWay = getBillInfo.Data.SaleWay;

                        list.Add(model);
                    }
                    else
                    {
                        return ResultModel.Failed("error：GetSaleFlowBillInfoageByShopIdAsync failed", 500); 
                    }         
                }
                var pageList = list.AsQueryable<SaleFlowBillInfoDto>().ToPagedList(pageIndex, pageSize);
                return ResultModel.Success(pageList);
            }
            else
            {
                return ResultModel.Failed("error：GetSaleFlowBillInfoageByShopIdAsync failed", 500); 
            }
        }

        [Description("根据 shopId 附加查询条件获取销售流水销售订单分页列表")]
        [ResponseCache(Duration = 0)]
        [Parameters(name = "pageIndex", param = "索引页")]
        [Parameters(name = "pageSize", param = "单页条数")]
        [Parameters(name = "shopId", param = "shopId")]
        [Parameters(name = "startTime", param = "查询起始时间")]
        [Parameters(name = "endTime", param = "查询结束时间")]
        [HttpGet("GetSaleFlowBillInfoByShopIdWhereQueryAsync")]
        public async Task<IResultModel> GetSaleFlowBillInfoByShopIdWhereQueryAsync([Required] int pageIndex, int pageSize, Guid shopId, DateTime? startTime, DateTime? endTime)
        {
            _logger.LogDebug($"根据 shopId：{shopId} 分页条件：索引页{pageIndex} 单页条数{pageSize} 查询起始时间{startTime} 查询结束时间{endTime} 获取销售流水销售订单分页列表");
            List<SaleFlowBillInfoDto> list = new List<SaleFlowBillInfoDto>();
            var getSaleFlow = (ResultModel<PagedList<SaleFlowDto>>)(await _saleFlowService.Value.GetPageByShopIdWhereQueryAsync(pageIndex, pageSize, shopId, startTime, endTime));
            if(getSaleFlow.Success)
            {
                foreach(var item in getSaleFlow.Data.Item)
                {
                    SaleFlowBillInfoDto model = _mapper.Value.Map<SaleFlowBillInfoDto>(item);
                    var getBillInfo = (ResultModel<BillInfoDto>)(await _billInfoService.Value.GetByShopIdAndBillNoAsync(shopId, item.BillNo));
                    if(getBillInfo.Success)
                    {
                        model.OperatorName = getBillInfo.Data.OperatorName;
                        model.SaleWay = getBillInfo.Data.SaleWay;

                        list.Add(model);
                    }
                    else
                    {
                        return ResultModel.Failed("error：GetSaleFlowBillInfoageByShopIdWhereQueryAsync failed", 500); 
                    }         
                }
                var pageList = list.AsQueryable<SaleFlowBillInfoDto>().ToPagedList(pageIndex, pageSize);
                return ResultModel.Success(pageList);
            }
            else
            {
                return ResultModel.Failed("error：GetSaleFlowBillInfoageByShopIdWhereQueryAsync failed", 500); 
            }
        }

        [Description("添加销售订单")]
        [HttpPost("InsertBillInfoAsync")]
        public async Task<IResultModel> InsertBillInfoAsync([FromBody] BillInfoCreateDto model)
        {
            _logger.LogDebug("添加销售订单");
            return await _createBillInfoService.Value.InsertAsync(model);
        }
        #endregion

        #region PayFlow 付款流水订单
        [Description("根据 ID 获取付款流水订单")]
        [ResponseCache(Duration = 0)]
        [Parameters(name = "id", param = "ID")]
        [HttpGet("GetPayFlowByIdAsync")]
        public async Task<IResultModel> GetPayFlowByIdAsync([Required] int id)
        {
            _logger.LogDebug($"根据 ID：{id} 获取付款流水订单");
            return await _payFlowService.Value.GetByIdAsync(id);
        }

        [Description("根据 shopId 获取付款流水订单分页列表")]
        [ResponseCache(Duration = 0)]
        [Parameters(name = "pageIndex", param = "索引页")]
        [Parameters(name = "pageSize", param = "单页条数")]
        [Parameters(name = "shopId", param = "shopId")]
        [HttpGet("GetPayFlowPageByShopIdAsync")]
        public async Task<IResultModel> GetPayFlowPageByShopIdAsync([Required] int pageIndex, int pageSize, Guid shopId)
        {
            _logger.LogDebug($"根据 shopId：{shopId} 分页条件：索引页{pageIndex} 单页条数{pageSize} 获取付款流水订单分页列表");
            return await _payFlowService.Value.GetPageByShopIdAsync(pageIndex, pageSize, shopId);
        }

        [Description("根据 shopId 附加查询条件获取付款流水订单分页列表")]
        [ResponseCache(Duration = 0)]
        [Parameters(name = "pageIndex", param = "索引页")]
        [Parameters(name = "pageSize", param = "单页条数")]
        [Parameters(name = "shopId", param = "shopId")]
        [Parameters(name = "startTime", param = "订单查询开始时间")]
        [Parameters(name = "endTime", param = "订单查询结束时间")]
        [HttpGet("GetPayFlowPageByShopIdWhereQueryAsync")]
        public async Task<IResultModel> GetPayFlowPageByShopIdWhereQueryAsync([Required] int pageIndex, int pageSize, Guid shopId, DateTime? startTime, DateTime? endTime)
        {
            _logger.LogDebug($"根据 shopId：{shopId} 分页条件：索引页 {pageIndex} 单页条数 {pageSize} 查询条件：订单查询开始时间 {startTime} 订单查询结束时间 {endTime} 获取付款流水订单分页列表");
            return await _payFlowService.Value.GetPageByShopIdWhereQueryAsync(pageIndex, pageSize, shopId, startTime, endTime);
        }

        [Description("添加付款流水订单")]
        [HttpPost("InsertPayFlowAsync")]
        public async Task<IResultModel> InsertPayFlowAsync([FromBody] PayFlowCreateDto model)
        {
            _logger.LogDebug("添加付款流水订单");
            return await _createPayFlowService.Value.InsertAsync(model);
        }
        #endregion

        #region SaleFlow 销售流水订单
        [Description("根据 ID 获取销售流水订单")]
        [ResponseCache(Duration = 0)]
        [Parameters(name = "id", param = "ID")]
        [HttpGet("GetSaleFlowByIdAsync")]
        public async Task<IResultModel> GetSaleFlowByIdAsync([Required] int id)
        {
            _logger.LogDebug($"根据 ID：{id} 获取销售流水订单");
            return await _saleFlowService.Value.GetByIdAsync(id);
        }

        [Description("根据 shopId 获取销售流水订单分页列表")]
        [ResponseCache(Duration = 0)]
        [Parameters(name = "pageIndex", param = "索引页")]
        [Parameters(name = "pageSize", param = "单页条数")]
        [Parameters(name = "shopId", param = "shopId")]
        [HttpGet("GetSaleFlowPageByShopIdAsync")]
        public async Task<IResultModel> GetSaleFlowPageByShopIdAsync([Required] int pageIndex, int pageSize, Guid shopId)
        {
            _logger.LogDebug($"根据 shopId：{shopId} 分页条件：索引页{pageIndex} 单页条数{pageSize} 获取销售流水订单分页列表");
            return await _saleFlowService.Value.GetPageByShopIdAsync(pageIndex, pageSize, shopId);
        }

        [Description("根据 shopId 附加查询条件获取销售流水订单分页列表")]
        [ResponseCache(Duration = 0)]
        [Parameters(name = "pageIndex", param = "索引页")]
        [Parameters(name = "pageSize", param = "单页条数")]
        [Parameters(name = "shopId", param = "shopId")]
        [Parameters(name = "startTime", param = "订单查询开始时间")]
        [Parameters(name = "endTime", param = "订单查询结束时间")]
        [HttpGet("GetSaleFlowPageByShopIdWhereQueryAsync")]
        public async Task<IResultModel> GetSaleFlowPageByShopIdWhereQueryAsync([Required] int pageIndex, int pageSize, Guid shopId, DateTime? startTime, DateTime? endTime)
        {
            _logger.LogDebug($"根据 shopId：{shopId} 分页条件：索引页 {pageIndex} 单页条数 {pageSize} 查询条件：订单查询开始时间 {startTime} 订单查询结束时间 {endTime} 获取销售流水订单分页列表");
            return await _saleFlowService.Value.GetPageByShopIdWhereQueryAsync(pageIndex, pageSize, shopId, startTime, endTime);
        }

        [Description("添加销流水订单")]
        [HttpPost("InsertSaleFlowAsync")]
        public async Task<IResultModel> InsertSaleFlowAsync([FromBody] SaleFlowCreateDto model)
        {
            _logger.LogDebug("添加销流水订单");
            return await _createSaleFlowService.Value.InsertAsync(model);
        }
        #endregion
    }
}
