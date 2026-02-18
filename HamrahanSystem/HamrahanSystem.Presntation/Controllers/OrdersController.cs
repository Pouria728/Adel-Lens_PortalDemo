using HamrahanSystem.Application.DTOs;
using HamrahanSystem.Application.UseCaseImplementation;
using HamrahanSystem.Application.UseCaseInterface;
using HamrahanSystem.Domain.Repository;
using HamrahanSystem.Domain.Entity;
using HamrahanSystem.Infrastructure.Repository;
using HamrahanSystem.Presntation.Models;
using HamrahanSystem.Presntation.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.AspNetCore.SignalR;
using NetTopologySuite.Index.HPRtree;
using Newtonsoft.Json;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.Text;
using System.Text.RegularExpressions;
using static FastReport.Fonts.FastGraphicsPath;

namespace HamrahanSystem.Presntation.Controllers
{
	[Authorize]
	public class OrdersController(ITblLnsOrderService tblLnsOrderService, ITblLnsOrderItemService tblLnsOrderItemService, ITblLnsBrandService tblLnsBrandService, ITblLnsBrandLensTypeRService tblLnsBrandLensTypeRService,
		ITblLnsLensTypeRLensIndexRService tblLnsLensTypeRLensIndexRService, ITblLnsLensIndexRSphService tblLnsLensIndexRSphService,
		ITblLnsSphCylService tblLnsSphCylService, ICacheService cacheService, IViewSalListCustomerService viewSalListCustomerService,
		ITblWfwOrderProcessStepService tblWfwOrderProcessStepService, ITblWfwProcessStepService tblWfwProcessStepService, IUserService userService, IConfiguration configuration,
		ITblLnsLensTypeService tblLnsLensTypeService, ITblLnsDesignTypeService tblLnsDesignTypeService,
		ITblLnsCustomLensIndexService tblLnsCustomLensIndexService, ITblLnsCustomLensTypeMaterialService tblLnsCustomLensTypeMaterialService,
		ITblLnsCustomLensTypeCoatingService tblLnsCustomLensTypeCoatingService, ITblClrDefineObjectService tblClrDefineObjectService,
		ITblLnsCustomSphService tblLnsCustomSphService, ITblLnsCustomCylService tblLnsCustomCylService,
		ITblLnsCustomDesignTypeAdditionService tblLnsCustomDesignTypeAdditionService,
		ITblWfwRelationStepRepository tblWfwRelationStepRepository,
		ITblWfwResultStepRepository tblWfwResultStepRepository,
		ICustomLensAutoPrintService customLensAutoPrintService,
		ICustomLensPrintSettingsService customLensPrintSettingsService,
		AdelModel dbContext
		) : Controller
	{
		private static readonly Regex CustomLensPrintPlaceholderRegex = new(@"\{(orderId|factorNo|printer)\}", RegexOptions.IgnoreCase | RegexOptions.Compiled);

		[Authorize(Roles = "admin,OrderStock_Index")]
		public IActionResult OrderStock()
		{

			ViewBag.ListBrand = tblLnsBrandService.GetAllActive().Result.Where(x=>x.IsStock).Select(x => new { id = x.BrandId, parentid = 0, text = "\u200E" + x.Name, });
			ViewBag.ListLensType = tblLnsBrandLensTypeRService.GetAll().Result
				.Select(x => new { id = x.BrandLensTypeRId, parentid = x.BrandId, text = "\u200E" + (x.TblLnsLensTypeR?.Name ?? "") })
				.ToList();
			ViewBag.ListLensIndex = tblLnsLensTypeRLensIndexRService.GetAll().Result
				.Select(x => new { id = x.LensTypeRLensIndexRId, parentid = x.BrandLensTypeRId, text = "\u200E" + (x.TblLnsLensIndexR?.Name ?? "") })
				.ToList();
			ViewBag.ListSph = tblLnsLensIndexRSphService.GetAll().Result
				.OrderBy(x => x.TblLnsSph.OrderId)
				.ThenBy(x => x.LensIndexRSphId)
				.Select(x => new { id = x.LensIndexRSphId, parentid = x.LensTypeRLensIndexRId, text = "\u200E" + (x.TblLnsSph?.Name ?? "") }).ToList();
			ViewBag.ListCyl = (tblLnsSphCylService.GetAll().Result ?? Enumerable.Empty<TblLnsSphCylDto>())
				.Where(x => x != null)
				.OrderBy(x => x.LensIndexRSphId)
				.ThenBy(x => x.TblLnsCyl != null ? x.TblLnsCyl.OrderId : int.MaxValue)
				.ThenBy(x => x.SphCylId)
				.Select(x => new
				{
					id = x.SphCylId,
					parentid = x.LensIndexRSphId,
					text = "\u200E" + (x.TblLnsCyl?.Name ?? ""),
					defineobjectid = x.DefineObjectId,
					nameobject = "\u200E" + (x.TblClrDefineObject?.NameObject ?? ""),
					cylid = x.CylId
				}).ToList();

			var customerId = HttpContext.Session.Get<int?>("CustomerId");
			var isAdmin = HttpContext.Session.Get<bool?>("IsAdmin");
            if (customerId.HasValue && isAdmin.HasValue && !isAdmin.Value)
            {
                var itemgg = new List<ViewSalListCustomerDto>();
                var selectedCustomer = viewSalListCustomerService.GetById(customerId.Value).Result;
                itemgg.Add(selectedCustomer);
                ViewBag.ListCustomer = itemgg;
                ViewBag.DefaultDefineCustomerId = selectedCustomer?.DefineCustomerId;
                ViewBag.DefaultDefineCustomerName = selectedCustomer?.NameFormal;
				ViewBag.IsCutomer = true;
            }
			else
            {
                ViewBag.ListCustomer = viewSalListCustomerService.GetAll().Result;
                ViewBag.DefaultDefineCustomerId = null;
                ViewBag.DefaultDefineCustomerName = "";
                ViewBag.IsCutomer = false;
            }



            return View();
		}
		[Authorize(Roles = "admin,OrderStock_Index")]
		[HttpPost]
		public IActionResult OrderStock(TblLnsOrderDto item)
		{
			item.Company = 3;
			item.CreateDate = (DateTime.Now.ToString());
			item.CreatedBy = HttpContext.Session.Get<int>("UserId");
			item.IndexDocument = (int)IndexDocument.LnsReadyOrder;
			
			tblLnsOrderService.Add(item, false);
			return Json(new
			{
				success = true,
				message = ""
			});

		}
		[Authorize(Roles = "admin,OrderStock_Index")]
		[HttpPost]
		public IActionResult OrderStockSend(TblLnsOrderDto item)
		{
			item.Company = 3;
			item.CreateDate = (DateTime.Now.ToString());
			item.CreatedBy = HttpContext.Session.Get<int>("UserId");
			item.IndexDocument = (int)IndexDocument.LnsReadyOrder;
			tblLnsOrderService.Add(item, true);
			return Json(new
			{
				success = true,
				message = ""
			});

		}
		[Authorize(Roles = "admin,OrderStockGranty_Index")]
		public IActionResult OrderStockGranty()
		{
			ViewBag.ListBrand = tblLnsBrandService.GetAllActive().Result.Where(x => x.IsStockGranty).Select(x => new { id = x.BrandId, parentid = 0, text = "\u200E" + x.Name, });
			ViewBag.ListLensType = tblLnsBrandLensTypeRService.GetAll().Result
				.Select(x => new { id = x.BrandLensTypeRId, parentid = x.BrandId, text = "\u200E" + (x.TblLnsLensTypeR?.Name ?? "") })
				.ToList();
			ViewBag.ListLensIndex = tblLnsLensTypeRLensIndexRService.GetAll().Result
				.Select(x => new { id = x.LensTypeRLensIndexRId, parentid = x.BrandLensTypeRId, text = "\u200E" + (x.TblLnsLensIndexR?.Name ?? "") })
				.ToList();
			ViewBag.ListSph = tblLnsLensIndexRSphService.GetAll().Result
				.OrderBy(x => x.TblLnsSph.OrderId)
				.ThenBy(x => x.LensIndexRSphId)
				.Select(x => new { id = x.LensIndexRSphId, parentid = x.LensTypeRLensIndexRId, text = "\u200E" + (x.TblLnsSph?.Name ?? "") }).ToList();
			ViewBag.ListCyl = (tblLnsSphCylService.GetAll().Result ?? Enumerable.Empty<TblLnsSphCylDto>())
				.Where(x => x != null)
				.OrderBy(x => x.LensIndexRSphId)
				.ThenBy(x => x.TblLnsCyl != null ? x.TblLnsCyl.OrderId : int.MaxValue)
				.ThenBy(x => x.SphCylId)
				.Select(x => new
				{
					id = x.SphCylId,
					parentid = x.LensIndexRSphId,
					text = "\u200E" + (x.TblLnsCyl?.Name ?? ""),
					defineobjectid = x.DefineObjectId,
					nameobject = "\u200E" + (x.TblClrDefineObject?.NameObject ?? "")
				}).ToList();
            var customerId = HttpContext.Session.Get<int?>("CustomerId");
            var isAdmin = HttpContext.Session.Get<bool?>("IsAdmin");
            if (customerId.HasValue && isAdmin.HasValue && !isAdmin.Value)
            {
                var itemgg = new List<ViewSalListCustomerDto>();
                itemgg.Add(viewSalListCustomerService.GetById(customerId.Value).Result);
                ViewBag.ListCustomer = itemgg;
                ViewBag.IsCutomer = true;
            }
            else
                ViewBag.IsCutomer = false;

            return View();
		}
		[Authorize(Roles = "admin,OrderStockGranty_Index")]
		[HttpPost]
		public IActionResult OrderStockGranty(TblLnsOrderDto item)
		{

			item.Company = 3;
			item.CreateDate = (DateTime.Now.ToString());
			item.CreatedBy = HttpContext.Session.Get<int>("UserId");
			item.IndexDocument = (int)IndexDocument.LnsOrderGranty;
			item.TblLnsOrderItems.ForEach(x => x.Quantity = 1);
			
			tblLnsOrderService.Add(item, false);
			return Json(new
			{
				success = true,
				message = ""
			});

		}
		[Authorize(Roles = "admin,OrderStockGranty_Index")]
		[HttpPost]
		public IActionResult OrderStockGrantySend(TblLnsOrderDto item)
		{

			item.Company = 3;
			item.CreateDate = (DateTime.Now.ToString());
			item.CreatedBy = HttpContext.Session.Get<int>("UserId");
			item.IndexDocument = (int)IndexDocument.LnsOrderGranty;
			item.TblLnsOrderItems.ForEach(x => x.Quantity = 1);
			
			tblLnsOrderService.Add(item, true);
			return Json(new
			{
				success = true,
				message = ""
			});

		}
		[Authorize(Roles = "admin,ListOrder_Index")]
		public IActionResult ShowOrder(int id )
		{
			int? customerId = HttpContext.Session.Get<int?>("CustomerId").HasValue ? HttpContext.Session.Get<int>("CustomerId") : null;
			var item=tblLnsOrderService.GetById(id);
			if ((customerId==null)||(customerId != null && item.Result.DefineCustomerId == customerId)) {
				return View(item.Result);
			}
			return View();
		}
		[Authorize(Roles = "admin,ListOrder_Index")]
		[HttpPost]
		public IActionResult OrderSend(int Id)
		{


			tblLnsOrderService.UpdateStatus(Id,(int)StatusOrder.Inprogress);

			return Json(new
			{
				success = true,
				message = ""
			});

		}
        [Authorize(Roles = "admin,ListOrder_Index")]
        [HttpPost]
        public JsonResult DeleteOrder([FromBody] dynamic data)
        {
            int orderId = data.id;
            tblLnsOrderService.Delete(orderId);
            return Json(new { success = true });
        }
        [Authorize(Roles = "admin,ListOrder_Index")]
		[HttpPost]
		public IActionResult OrderRemove(int Id)
		{
			
			tblLnsOrderService.UpdateStatus(Id, (int)StatusOrder.Cancel);
			return Json(new
			{
				success = true,
				message = ""
			});

		}
		[Authorize(Roles = "admin,ListOrder_Index")]
		public IActionResult ListOrders()
		{
			ViewBag.ListBrand = tblLnsBrandService.GetAllActive().Result.Select(x => new { id = x.BrandId, parentid = 0, text = x.Name, });
			ViewBag.ListLensType = tblLnsBrandLensTypeRService.GetAll().Result
				.Select(x => new { id = x.BrandLensTypeRId, parentid = x.BrandId, text = "\u200E" + (x.TblLnsLensTypeR?.Name ?? "") })
				.ToList();
			ViewBag.ListLensIndex = tblLnsLensTypeRLensIndexRService.GetAll().Result
				.Select(x => new { id = x.LensTypeRLensIndexRId, parentid = x.BrandLensTypeRId, text = "\u200E" + (x.TblLnsLensIndexR?.Name ?? "") })
				.ToList();
			ViewBag.ListSph = tblLnsLensIndexRSphService.GetAll().Result
				.OrderBy(x => x.TblLnsSph.OrderId)
				.ThenBy(x => x.LensIndexRSphId)
				.Select(x => new { id = x.LensIndexRSphId, parentid = x.LensTypeRLensIndexRId, text = "\u200E" + (x.TblLnsSph?.Name ?? "") }).ToList();
			ViewBag.ListCyl = tblLnsSphCylService.GetAll().Result
				.OrderBy(x => x.LensIndexRSphId)
				.ThenBy(x => x.TblLnsCyl != null ? x.TblLnsCyl.OrderId : int.MaxValue)
				.ThenBy(x => x.SphCylId)
				.Select(x => new
				{
					id = x.SphCylId,
					parentid = x.LensIndexRSphId,
					text = "\u200E" + (x.TblLnsCyl?.Name ?? ""),
					defineobjectid = x.DefineObjectId,
					nameobject = "\u200E" + (x.TblClrDefineObject?.NameObject ?? "")
				}).ToList();

			List<SelectListItem> indexDocuments = Enum.GetValues(typeof(IndexDocument)).Cast<IndexDocument>().Select(o => new SelectListItem() { Text = ((DescriptionAttribute[])(o.GetType().GetField(o.ToString()).GetCustomAttributes(typeof(DescriptionAttribute), false)))[0].Description, Value = ((int)o).ToString() }).ToList();
			ViewBag.ListDocument = indexDocuments;

			List<SelectListItem> statusOrder = Enum.GetValues(typeof(StatusOrder)).Cast<StatusOrder>().Select(o => new SelectListItem() { Text = ((DescriptionAttribute[])(o.GetType().GetField(o.ToString()).GetCustomAttributes(typeof(DescriptionAttribute), false)))[0].Description, Value = ((int)o).ToString() }).ToList();
			ViewBag.ListstatusOrder = statusOrder;


			return View();

		}
		[Authorize(Roles = "admin,OrderReq_Index")]
		public IActionResult ListReqOrders()
		{
			ViewBag.ListBrand = tblLnsBrandService.GetAllActive().Result.Select(x => new { id = x.BrandId, parentid = 0, text = "\u200E" + x.Name, });
			ViewBag.ListLensType = tblLnsBrandLensTypeRService.GetAll().Result
				.Select(x => new { id = x.BrandLensTypeRId, parentid = x.BrandId, text = "\u200E" + (x.TblLnsLensTypeR?.Name ?? "") })
				.ToList();
			ViewBag.ListLensIndex = tblLnsLensTypeRLensIndexRService.GetAll().Result
				.Select(x => new { id = x.LensTypeRLensIndexRId, parentid = x.BrandLensTypeRId, text = "\u200E" + (x.TblLnsLensIndexR?.Name ?? "") })
				.ToList();
			ViewBag.ListSph = tblLnsLensIndexRSphService.GetAll().Result
				.OrderBy(x => x.TblLnsSph.OrderId)
				.ThenBy(x => x.LensIndexRSphId)
				.Select(x => new { id = x.LensIndexRSphId, parentid = x.LensTypeRLensIndexRId, text = "\u200E" + (x.TblLnsSph?.Name ?? "") }).ToList();
			ViewBag.ListCyl = tblLnsSphCylService.GetAll().Result
				.OrderBy(x => x.LensIndexRSphId)
				.ThenBy(x => x.TblLnsCyl != null ? x.TblLnsCyl.OrderId : int.MaxValue)
				.ThenBy(x => x.SphCylId)
				.Select(x => new
				{
					id = x.SphCylId,
					parentid = x.LensIndexRSphId,
					text = "\u200E" + (x.TblLnsCyl?.Name ?? ""),
					defineobjectid = x.DefineObjectId,
					nameobject = "\u200E" + (x.TblClrDefineObject?.NameObject ?? "")
				}).ToList();
			
			List<SelectListItem> indexDocuments = Enum.GetValues(typeof(IndexDocument)).Cast<IndexDocument>().Select(o => new SelectListItem() { Text = ((DescriptionAttribute[])(o.GetType().GetField(o.ToString()).GetCustomAttributes(typeof(DescriptionAttribute), false)))[0].Description, Value = ((int)o).ToString() }).ToList();
			ViewBag.ListDocument = indexDocuments;

			List<SelectListItem> statusOrder = Enum.GetValues(typeof(StatusOrder)).Cast<StatusOrder>().Select(o => new SelectListItem() { Text = ((DescriptionAttribute[])(o.GetType().GetField(o.ToString()).GetCustomAttributes(typeof(DescriptionAttribute), false)))[0].Description, Value = ((int)o).ToString() }).ToList();
			ViewBag.ListStatusOrder = statusOrder;
			List<SelectListItem> statusRequest = Enum.GetValues(typeof(StatusRequest)).Cast<StatusRequest>().Select(o => new SelectListItem() { Text = ((DescriptionAttribute[])(o.GetType().GetField(o.ToString()).GetCustomAttributes(typeof(DescriptionAttribute), false)))[0].Description, Value = ((int)o).ToString() }).ToList();
			ViewBag.ListStatusRequest = statusRequest;


			return View();

		}
		[Authorize(Roles = "admin,OrderReq_Index")]
		public IActionResult CustomLensOrder()
		{
            var customerId = HttpContext.Session.Get<int?>("CustomerId");
            var isAdmin = HttpContext.Session.Get<bool?>("IsAdmin");
            if (customerId.HasValue && isAdmin.HasValue && !isAdmin.Value)
            {
                var itemgg = new List<ViewSalListCustomerDto>();
                itemgg.Add(viewSalListCustomerService.GetById(customerId.Value).Result);
                ViewBag.ListCustomer = itemgg;
                ViewBag.IsCutomer = true;
            }
            else
            {
                ViewBag.IsCutomer = false;
            }

            var customBrands = (tblLnsBrandService.GetAll().Result ?? Enumerable.Empty<TblLnsBrandDto>())
                .Where(x => x.IsSpecial == true && x.IsActive == true)
                .OrderBy(x => x.OrderId)
                .Select(x => new { id = x.BrandId, text = x.Name ?? string.Empty })
                .ToList();
            var activeCustomBrandIds = customBrands.Select(x => x.id).ToHashSet();

            var customLensTypes = (tblLnsLensTypeService.GetAll().Result ?? Enumerable.Empty<TblLnsLensTypeDto>())
                .Where(x => x.IsSpecial == true && x.IsActive == true && x.BrandId.HasValue && activeCustomBrandIds.Contains(x.BrandId.Value))
                .OrderBy(x => x.OrderId)
                .Select(x => new { id = x.LensTypeId, parentid = x.BrandId ?? 0, text = x.Name ?? string.Empty, corridor = x.IsCorridor })
                .ToList();
            var activeCustomLensTypeIds = customLensTypes.Select(x => x.id).ToHashSet();

            var customDesignTypes = (tblLnsDesignTypeService.GetAll().Result ?? Enumerable.Empty<TblLnsDesignTypeDto>())
                .Where(x => x.IsSpecial == true && x.IsActive == true && x.LensTypeId.HasValue && activeCustomLensTypeIds.Contains(x.LensTypeId.Value))
                .OrderBy(x => x.OrderId)
                .Select(x => new
                {
                    id = x.DesignTypeId,
                    parentid = x.LensTypeId ?? 0,
                    text = x.Name ?? string.Empty,
                    sphPlus = x.SphPlus,
                    sphMinus = x.SphMinus,
                    addition = x.Addition
                })
                .ToList();
            var activeCustomDesignTypeIds = customDesignTypes.Select(x => x.id).ToHashSet();

            var customLensIndexes = (tblLnsCustomLensIndexService.GetAll().Result ?? Enumerable.Empty<TblLnsCustomLensIndexDto>())
                .Where(x => x.IsActive == true && x.DesignTypeId.HasValue && activeCustomDesignTypeIds.Contains(x.DesignTypeId.Value))
                .OrderBy(x => x.OrderId)
                .Select(x => new { id = x.CustomLensIndexId, parentid = x.DesignTypeId ?? 0, text = x.LensIndexName ?? string.Empty })
                .ToList();
            var activeCustomLensIndexIds = customLensIndexes.Select(x => x.id).ToHashSet();

            var customMaterials = (tblLnsCustomLensTypeMaterialService.GetAll().Result ?? Enumerable.Empty<TblLnsCustomLensTypeMaterialDto>())
                .Where(x => x.IsActive == true && x.LensIndexId.HasValue && x.LensIndexId.Value > 0 && activeCustomLensIndexIds.Contains(x.LensIndexId.Value))
                .OrderBy(x => x.OrderId)
                .Select(x => new { id = x.CustomLensTypeMaterialId, parentid = x.LensIndexId ?? 0, text = x.MaterialName ?? string.Empty, defineObjectId = x.DefineObjectId ?? 0 })
                .ToList();

            var customCoatings = (tblLnsCustomLensTypeCoatingService.GetAll().Result ?? Enumerable.Empty<TblLnsCustomLensTypeCoatingDto>())
                .Where(x => x.IsActive == true && x.DesignTypeId.HasValue && activeCustomDesignTypeIds.Contains(x.DesignTypeId.Value))
                .OrderBy(x => x.OrderId)
                .Select(x => new { id = x.CustomLensTypeCoatingId, parentid = x.DesignTypeId ?? 0, text = x.CoatingName ?? string.Empty })
                .ToList();

            string FormatCustomNumber(string? value)
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    return string.Empty;
                }

                var normalized = value.Trim().Replace(',', '.');
                if (decimal.TryParse(normalized, NumberStyles.Float, CultureInfo.InvariantCulture, out var parsed))
                {
                    return Math.Abs(parsed).ToString("0.00", CultureInfo.InvariantCulture);
                }

                return value.Trim();
            }

            string FormatAdditionNumber(decimal value)
            {
                return value.ToString("0.00", CultureInfo.InvariantCulture).Replace('.', ',');
            }

            var customSphList = (tblLnsCustomSphService.GetAll().Result ?? Enumerable.Empty<TblLnsCustomSphDto>())
                .Where(x => x.IsActive)
                .OrderBy(x => x.OrderId)
                .ThenBy(x => x.CustomSphId)
                .Select(x => new { id = x.CustomSphId, text = FormatCustomNumber(x.Name) })
                .Where(x => !string.IsNullOrWhiteSpace(x.text))
                .ToList();

            var customCylList = (tblLnsCustomCylService.GetAll().Result ?? Enumerable.Empty<TblLnsCustomCylDto>())
                .Where(x => x.IsActive)
                .OrderBy(x => x.OrderId)
                .ThenBy(x => x.CustomCylId)
                .Select(x => new { id = x.CustomCylId, text = FormatCustomNumber(x.Name) })
                .Where(x => !string.IsNullOrWhiteSpace(x.text))
                .ToList();

            var customAdditionList = (tblLnsCustomDesignTypeAdditionService.GetAll().Result ?? Enumerable.Empty<TblLnsCustomDesignTypeAdditionDto>())
                .Where(x => x.IsActive && x.DesignTypeId.HasValue && x.AdditionValue.HasValue)
                .OrderBy(x => x.OrderId)
                .ThenBy(x => x.CustomDesignTypeAdditionId)
                .Select(x => new
                {
                    id = x.CustomDesignTypeAdditionId,
                    parentid = x.DesignTypeId ?? 0,
                    text = FormatAdditionNumber(x.AdditionValue!.Value)
                })
                .ToList();

            var defineObjectIds = customMaterials.Select(x => x.defineObjectId).Where(x => x > 0).Distinct().ToList();
            var defineObjects = (tblClrDefineObjectService.GetAll().Result ?? Enumerable.Empty<TblClrDefineObjectDto>())
                .Where(x => defineObjectIds.Contains(x.DefineObjectId))
                .ToDictionary(x => x.DefineObjectId, x => x);

            string BuildDefineObjectText(TblClrDefineObjectDto item)
            {
                var parts = new List<string>();
                if (!string.IsNullOrWhiteSpace(item?.CodeObject))
                {
                    parts.Add(item.CodeObject.Trim());
                }
                if (!string.IsNullOrWhiteSpace(item?.NameObject))
                {
                    parts.Add(item.NameObject.Trim());
                }
                if (!string.IsNullOrWhiteSpace(item?.TechnicalSpecs))
                {
                    parts.Add(item.TechnicalSpecs.Trim());
                }
                return string.Join(" - ", parts);
            }

            var customProducts = customMaterials
                .Where(x => x.defineObjectId > 0 && defineObjects.ContainsKey(x.defineObjectId))
                .Select(x => new { id = x.defineObjectId, parentid = x.parentid, text = BuildDefineObjectText(defineObjects[x.defineObjectId]) })
                .Distinct()
                .ToList();

            ViewBag.CustomBrandList = customBrands;
            ViewBag.CustomLensTypeList = customLensTypes;
            ViewBag.CustomDesignTypeList = customDesignTypes;
            ViewBag.CustomLensIndexList = customLensIndexes;
            ViewBag.CustomMaterialList = customMaterials;
            ViewBag.CustomCoatingList = customCoatings;
            ViewBag.CustomProductList = customProducts;
            ViewBag.CustomSphList = customSphList;
            ViewBag.CustomCylList = customCylList;
            ViewBag.CustomAdditionList = customAdditionList;

            return View(new TblLnsOrderDto());
		}

        [Authorize(Roles = "admin,OrderReq_Index")]
        [HttpPost]
        public IActionResult CustomLensOrder(TblLnsOrderDto item)
        {
            item.Company = 3;
            item.CreateDate = DateTime.Now.ToString();
            item.CreatedBy = HttpContext.Session.Get<int>("UserId");
            item.IndexDocument = (int)IndexDocument.LnsOrder;
            item.TblLnsOrderItems ??= new List<TblLnsOrderItemDto>();
            NormalizeCustomOrderForDatabase(item);
            // Custom Lens Order UI uses custom definition tables for these fields.
            // Tbl_Lns_Order foreign keys point to base tables, so we keep them null to avoid FK errors.
            item.LensIndexId = null;
            item.MaterialTypeId = null;
            item.CoatingId = null;

            try
            {
                tblLnsOrderService.Add(item, false);
                return Json(new
                {
                    success = true,
                    message = ""
                });
            }
            catch (Exception ex)
            {
                return Json(new
                {
                    success = false,
                    message = ex.InnerException?.Message ?? ex.Message
                });
            }
        }

        [Authorize(Roles = "admin,OrderReq_Index")]
        [HttpPost]
        public async Task<IActionResult> CustomLensOrderSend(TblLnsOrderDto item)
        {
            item.Company = 3;
            item.CreateDate = DateTime.Now.ToString();
            item.CreatedBy = HttpContext.Session.Get<int>("UserId");
            item.IndexDocument = (int)IndexDocument.LnsOrder;
            item.TblLnsOrderItems ??= new List<TblLnsOrderItemDto>();
            NormalizeCustomOrderForDatabase(item);
            // Custom Lens Order UI uses custom definition tables for these fields.
            // Tbl_Lns_Order foreign keys point to base tables, so we keep them null to avoid FK errors.
            item.LensIndexId = null;
            item.MaterialTypeId = null;
            item.CoatingId = null;

            try
            {
                await tblLnsOrderService.Add(item, true);
                if (item.StatusId == (int)StatusOrder.Inprogress && item.OrderId > 0)
                {
                    await customLensAutoPrintService.DispatchAsync(item.OrderId, item.FactorNo);
                }

                return Json(new
                {
                    success = true,
                    message = ""
                });
            }
            catch (Exception ex)
            {
                return Json(new
                {
                    success = false,
                    message = ex.InnerException?.Message ?? ex.Message
                });
            }
        }

        private static void NormalizeCustomOrderForDatabase(TblLnsOrderDto item)
        {
            item.BeforeLensSpec = TrimToLength(item.BeforeLensSpec, 20);
            item.Consumer = TrimToLength(item.Consumer, 254);
            item.StoreName = TrimToLength(item.StoreName, 254);
            item.Color = TrimToLength(item.Color, 254);
            item.Description = TrimMetadataToLength(item.Description, 254);

            if (item.TblLnsOrderItems == null)
            {
                return;
            }

            foreach (var orderItem in item.TblLnsOrderItems.Where(x => x != null))
            {
                orderItem.ConsumerTitle = TrimToLength(orderItem.ConsumerTitle, 20);
                orderItem.Optician = TrimToLength(orderItem.Optician, 254);
                orderItem.Description = TrimMetadataToLength(orderItem.Description, 254);
            }
        }

        private static string? TrimToLength(string? value, int maxLength)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                return value;
            }

            var normalized = value.Trim();
            if (normalized.Length <= maxLength)
            {
                return normalized;
            }

            return normalized[..maxLength];
        }

        private static string? TrimMetadataToLength(string? value, int maxLength)
        {
            var normalized = TrimToLength(value, maxLength);
            if (string.IsNullOrWhiteSpace(normalized))
            {
                return normalized;
            }

            if (normalized.Length < maxLength)
            {
                return normalized;
            }

            var parts = normalized.Split(';', StringSplitOptions.RemoveEmptyEntries);
            if (parts.Length <= 1)
            {
                return normalized;
            }

            var builder = new StringBuilder();
            foreach (var rawPart in parts)
            {
                var part = rawPart.Trim();
                if (string.IsNullOrWhiteSpace(part))
                {
                    continue;
                }
                if (!part.Contains('='))
                {
                    continue;
                }

                var candidateLength = builder.Length == 0
                    ? part.Length
                    : builder.Length + 1 + part.Length;

                if (candidateLength > maxLength)
                {
                    break;
                }

                if (builder.Length > 0)
                {
                    builder.Append(';');
                }

                builder.Append(part);
            }

            return builder.Length > 0 ? builder.ToString() : normalized;
        }

		[Authorize(Roles = "admin,OrderReq_Index")]
		public IActionResult ShowOrderReq(long id)
		{
			int? RoleId = HttpContext.Session.Get<int?>("RoleId") ;
			bool IsAdmin = HttpContext.Session.Get<bool>("IsAdmin");
			var item = tblWfwOrderProcessStepService.GetById(id);
			if ((IsAdmin == true) || (RoleId!=null&& item.Result.TblWfwProcessStep.TblWfwRoleStepes.Any(x=>x.RoleId==RoleId)))
			{
                var customerName = string.Empty;
                var defineCustomerId = item.Result?.TblWfwOrderProcess?.TblLnsOrder?.DefineCustomerId;
                if (defineCustomerId.HasValue)
                {
                    var customer = viewSalListCustomerService.GetById(defineCustomerId.Value).Result;
                    customerName = customer?.NameFormal ?? string.Empty;
                }
                ViewBag.CustomerName = customerName;

				var order = item.Result?.TblWfwOrderProcess?.TblLnsOrder;
				if (order != null && order.IndexDocument == (int)IndexDocument.LnsOrder)
				{
					var printSettings = customLensPrintSettingsService.GetAsync().Result;
					var firstPrinter = printSettings.Printers?.FirstOrDefault() ?? string.Empty;
					var factorNo = !string.IsNullOrWhiteSpace(order.FactorNo)
						? order.FactorNo.Trim()
						: "RX" + order.OrderId.ToString().PadLeft(8, '0');
					ViewBag.CustomLensPrintPreviewUrl = BuildCustomLensPrintUrl(
						printSettings.ReportUrlTemplate,
						order.OrderId,
						factorNo,
						firstPrinter);

					if (order.DefineObjectId.HasValue)
					{
						var defineObject = tblClrDefineObjectService.GetById(order.DefineObjectId.Value).Result;
						ViewBag.ProductName = defineObject?.NameObject ?? string.Empty;
					}

					ViewBag.CustomSphLookup = (tblLnsCustomSphService.GetAll().Result ?? Enumerable.Empty<TblLnsCustomSphDto>())
						.Where(x => x.IsActive)
						.GroupBy(x => x.CustomSphId)
						.ToDictionary(x => x.Key, x => x.First().Name ?? string.Empty);

					ViewBag.CustomCylLookup = (tblLnsCustomCylService.GetAll().Result ?? Enumerable.Empty<TblLnsCustomCylDto>())
						.Where(x => x.IsActive)
						.GroupBy(x => x.CustomCylId)
						.ToDictionary(x => x.Key, x => x.First().Name ?? string.Empty);

					ViewBag.CustomBrandLookup = (tblLnsBrandService.GetAll().Result ?? Enumerable.Empty<TblLnsBrandDto>())
						.Where(x => x.IsSpecial == true && x.IsActive == true)
						.GroupBy(x => x.BrandId)
						.ToDictionary(x => x.Key, x => x.First().Name ?? string.Empty);

					ViewBag.CustomLensTypeLookup = (tblLnsLensTypeService.GetAll().Result ?? Enumerable.Empty<TblLnsLensTypeDto>())
						.Where(x => x.IsSpecial == true && x.IsActive == true)
						.GroupBy(x => x.LensTypeId)
						.ToDictionary(x => x.Key, x => x.First().Name ?? string.Empty);

					ViewBag.CustomDesignTypeLookup = (tblLnsDesignTypeService.GetAll().Result ?? Enumerable.Empty<TblLnsDesignTypeDto>())
						.Where(x => x.IsSpecial == true && x.IsActive == true)
						.GroupBy(x => x.DesignTypeId)
						.ToDictionary(x => x.Key, x => x.First().Name ?? string.Empty);

					ViewBag.CustomLensIndexLookup = (tblLnsCustomLensIndexService.GetAll().Result ?? Enumerable.Empty<TblLnsCustomLensIndexDto>())
						.Where(x => x.IsActive)
						.GroupBy(x => x.CustomLensIndexId)
						.ToDictionary(x => x.Key, x => x.First().LensIndexName ?? string.Empty);

					ViewBag.CustomMaterialLookup = (tblLnsCustomLensTypeMaterialService.GetAll().Result ?? Enumerable.Empty<TblLnsCustomLensTypeMaterialDto>())
						.Where(x => x.IsActive)
						.GroupBy(x => x.CustomLensTypeMaterialId)
						.ToDictionary(x => x.Key, x => x.First().MaterialName ?? string.Empty);

					ViewBag.CustomCoatingLookup = (tblLnsCustomLensTypeCoatingService.GetAll().Result ?? Enumerable.Empty<TblLnsCustomLensTypeCoatingDto>())
						.Where(x => x.IsActive)
						.GroupBy(x => x.CustomLensTypeCoatingId)
						.ToDictionary(x => x.Key, x => x.First().CoatingName ?? string.Empty);
				}
				return View(item.Result);
			}
			return View();
		}

		[Authorize(Roles = "admin,OrderReq_Index")]
		[HttpPost]
		public IActionResult OrderReqSend(TblWfwOrderProcessStepDto item)
		{
			return Json(new
			{
				success = false,
				message = "ط«ط¨طھ ظˆ ط§ط±ط³ط§ظ„ ط¯ط³طھغŒ ط؛غŒط±ظپط¹ط§ظ„ ط§ط³طھ. ظ„ط·ظپط§ ظپظ‚ط· ط§ط² ط·ط±غŒظ‚ ط§ط³ع©ظ† ع¯ط§ظ† ط§ظ‚ط¯ط§ظ… ع©ظ†غŒط¯."
			});
		}

		[Authorize(Roles = "admin,OrderReq_Index")]
		[HttpPost]
		public IActionResult OrderReqSendByCode(string orderCode, string? selectedResultOption, int? selectedResultStepId, string? materialBarcode)
		{
			var normalizedCode = NormalizeOrderCode(orderCode);
			if (string.IsNullOrWhiteSpace(normalizedCode))
			{
				return Json(new
				{
					success = false,
					message = "ط´ظ…ط§ط±ظ‡ ط³ظپط§ط±ط´ ظˆط§ط±ط¯ ظ†ط´ط¯ظ‡ ط§ط³طھ."
				});
			}

			bool isAdmin = HttpContext.Session.Get<bool>("IsAdmin");
			int? roleId = isAdmin ? null : HttpContext.Session.Get<int?>("RoleId");
			int? customerId = HttpContext.Session.Get<int?>("CustomerId");

			List<TblWfwOrderProcessStepDto> inprogressSteps = LoadInprogressSteps(roleId, customerId, normalizedCode);

			var currentStep = inprogressSteps
				.Where(x => x?.TblWfwOrderProcess?.TblLnsOrder != null)
				.Where(HasStepAccess)
				.Where(x => IsOrderCodeMatch(normalizedCode, x.TblWfwOrderProcess.TblLnsOrder))
				.OrderByDescending(x => x.DateCreate)
				.FirstOrDefault();

			if (currentStep == null)
			{
				inprogressSteps = LoadInprogressSteps(roleId, customerId, null);
				currentStep = inprogressSteps
					.Where(x => x?.TblWfwOrderProcess?.TblLnsOrder != null)
					.Where(HasStepAccess)
					.Where(x => IsOrderCodeMatch(normalizedCode, x.TblWfwOrderProcess.TblLnsOrder))
					.OrderByDescending(x => x.DateCreate)
					.FirstOrDefault();
			}

			if (currentStep == null)
			{
				return Json(new
				{
					success = false,
					message = "ط³ظپط§ط±ط´ ط¯ط± ظ…ط±ط§ط­ظ„ ط¯ط± ط¬ط±غŒط§ظ†ظگ ع©ط§ط±طھط§ط¨ظ„ ط´ظ…ط§ ظ¾غŒط¯ط§ ظ†ط´ط¯."
				});
			}

			if (currentStep.StatusId != (int)StatusRequest.Inprogress)
			{
				return Json(new
				{
					success = false,
					message = "ط§غŒظ† ظ…ط±ط­ظ„ظ‡ ظ‚ط¨ظ„ط§ ط®ط§طھظ…ظ‡ غŒط§ظپطھظ‡ ظˆ ظ‚ط§ط¨ظ„ طھط؛غŒغŒط± ظ†غŒط³طھ."
				});
			}

			var transitionRequirement = ResolveTransitionRequirement(currentStep);
			var requiresMaterialScan = currentStep.TblWfwProcessStep?.IsBarcode == true;
			int? selectedRelationStepId = null;
			if (transitionRequirement.RequiresChoice)
			{
				var normalizedChoice = NormalizeTransitionChoice(selectedResultOption);
				var matchedChoice = transitionRequirement.Options
					.FirstOrDefault(x => selectedResultStepId.HasValue && selectedResultStepId.Value > 0
						? x.ResultStepId == selectedResultStepId.Value
						: string.Equals(x.Name, normalizedChoice, StringComparison.OrdinalIgnoreCase));

				if (matchedChoice == null)
				{
					return Json(new
					{
						success = false,
						requiresResultChoice = true,
						requiresMaterialScan,
						message = "ط¨ط±ط§غŒ ط§ظ†طھظ‚ط§ظ„ ط§غŒظ† ظ…ط±ط­ظ„ظ‡ ط¨ط§غŒط¯ غŒع© ع¯ط²غŒظ†ظ‡ ط§ظ†طھط®ط§ط¨ ط´ظˆط¯.",
						options = transitionRequirement.Options.Select(x => new
						{
							id = x.ResultStepId,
							text = x.Name
						}).ToList()
					});
				}

				selectedRelationStepId = matchedChoice.RelationStepId;
			}

			if (requiresMaterialScan)
			{
				var normalizedMaterialBarcode = NormalizeMaterialBarcode(materialBarcode);
				if (string.IsNullOrWhiteSpace(normalizedMaterialBarcode))
				{
					return Json(new
					{
						success = false,
						requiresResultChoice = transitionRequirement.RequiresChoice,
						requiresMaterialScan = true,
						message = "اسکن بارکد ماده مصرفی الزامی است.",
						options = transitionRequirement.Options.Select(x => new
						{
							id = x.ResultStepId,
							text = x.Name
						}).ToList()
					});
				}

				if (!TryRegisterConsumedMaterial(currentStep, normalizedMaterialBarcode, out var registerError))
				{
					return Json(new
					{
						success = false,
						requiresResultChoice = transitionRequirement.RequiresChoice,
						requiresMaterialScan = true,
						message = registerError,
						options = transitionRequirement.Options.Select(x => new
						{
							id = x.ResultStepId,
							text = x.Name
						}).ToList()
					});
				}
			}

			currentStep.StatusId = (int)StatusRequest.Complete;
			currentStep.DateComplete = DateTime.Now;
			currentStep.UserId = HttpContext.Session.Get<int>("UserId");
			tblWfwOrderProcessStepService.UpdateStatus(currentStep, selectedRelationStepId);

			return Json(new
			{
				success = true,
				requiresResultChoice = false,
				message = ""
			});
		}

		[Authorize(Roles = "admin,OrderReq_Index")]
		[HttpPost]
		[ValidateAntiForgeryToken]
		public IActionResult OrderReqSendByScan(TblWfwOrderProcessStepDto item, string scannedCode, int? scanDurationMs, int? scanLength)
		{
			if (!IsLikelyScannerInput(scanDurationMs, scanLength))
			{
				return Json(new
				{
					success = false,
					message = "ظˆط±ظˆط¯ ط¯ط³طھغŒ ظ…ط¬ط§ط² ظ†غŒط³طھ. ظ„ط·ظپط§ ظپظ‚ط· ط¨ط§ ع¯ط§ظ† ط§ط³ع©ظ† ع©ظ†غŒط¯."
				});
			}

			var currentStep = tblWfwOrderProcessStepService.GetById(item.OrderProcessStepId).Result;
			if (currentStep == null)
			{
				return Json(new
				{
					success = false,
					message = "ط±ع©ظˆط±ط¯ ظ…ط±ط­ظ„ظ‡ ط³ظپط§ط±ط´ غŒط§ظپطھ ظ†ط´ط¯."
				});
			}

			if (!HasStepAccess(currentStep))
			{
				return Json(new
				{
					success = false,
					message = "ط´ظ…ط§ ط¯ط³طھط±ط³غŒ ط§ظ†ط¬ط§ظ… ط§غŒظ† ظ…ط±ط­ظ„ظ‡ ط±ط§ ظ†ط¯ط§ط±غŒط¯."
				});
			}

			if (currentStep.StatusId != (int)StatusRequest.Inprogress)
			{
				return Json(new
				{
					success = false,
					message = "ط§غŒظ† ظ…ط±ط­ظ„ظ‡ ظ‚ط¨ظ„ط§ ط®ط§طھظ…ظ‡ غŒط§ظپطھظ‡ ظˆ ظ‚ط§ط¨ظ„ طھط؛غŒغŒط± ظ†غŒط³طھ."
				});
			}

			var order = currentStep.TblWfwOrderProcess?.TblLnsOrder;
			if (!IsOrderCodeMatch(scannedCode, order))
			{
				return Json(new
				{
					success = false,
					message = "ط´ظ…ط§ط±ظ‡ ط³ظپط§ط±ط´ ط§ط³ع©ظ†â€Œط´ط¯ظ‡ ظ…ط¹طھط¨ط± ظ†غŒط³طھ غŒط§ ط¨ط§ ط§غŒظ† ط³ظپط§ط±ط´ ظ…ط·ط§ط¨ظ‚طھ ظ†ط¯ط§ط±ط¯."
				});
			}

			if (currentStep.TblWfwProcessStep?.IsBarcode == true)
			{
				List<TblLnsOrderItemDto> tblLnsOrderItemDto =
					item?.TblWfwOrderProcess?.TblLnsOrder?.TblLnsOrderItems ?? new List<TblLnsOrderItemDto>();

				if (tblLnsOrderItemDto.Any())
				{
					tblLnsOrderItemService.UpdateProvidedQuantity(tblLnsOrderItemDto);
				}
			}

			currentStep.StatusId = (int)StatusRequest.Complete;
			currentStep.DateComplete = DateTime.Now;
			currentStep.UserId = HttpContext.Session.Get<int>("UserId");
			tblWfwOrderProcessStepService.UpdateStatus(currentStep);

			return Json(new
			{
				success = true,
				message = ""
			});
		}

		private bool HasStepAccess(TblWfwOrderProcessStepDto currentStep)
		{
			bool isAdmin = HttpContext.Session.Get<bool>("IsAdmin");
			int? roleId = HttpContext.Session.Get<int?>("RoleId");
			bool hasRoleAccess = roleId.HasValue &&
								 currentStep.TblWfwProcessStep?.TblWfwRoleStepes?.Any(x => x.RoleId == roleId.Value) == true;
			return isAdmin || hasRoleAccess;
		}

		private sealed class TransitionResultOption
		{
			public int ResultStepId { get; init; }
			public int RelationStepId { get; init; }
			public int ToProcessStepId { get; init; }
			public string Name { get; init; } = string.Empty;
		}

		private sealed class TransitionResultRequirement
		{
			public List<TransitionResultOption> Options { get; init; } = new();
			public bool RequiresChoice => Options.Count > 0;
		}

		private TransitionResultRequirement ResolveTransitionRequirement(TblWfwOrderProcessStepDto currentStep)
		{
			if (currentStep?.TblWfwOrderProcess == null)
			{
				return new TransitionResultRequirement();
			}

			var relationRows = (tblWfwRelationStepRepository.GetByFromProcessStepId(currentStep.ProcessStepId) ?? new List<TblWfwRelationStep>())
				.Where(x => x.ToProcessStepId.HasValue)
				.ToList();

			if (!relationRows.Any())
			{
				return new TransitionResultRequirement();
			}

			var processSteps = tblWfwProcessStepService
				.GetAll(currentStep.TblWfwOrderProcess.ProcessId, null, null, null, null, null)
				.Result
				.Item1 ?? new List<TblWfwProcessStepDto>();

			var activeProcessSteps = processSteps
				.Where(x => x.IsActive)
				.OrderBy(x => x.OrderId)
				.ToList();

			var activeStepIds = new HashSet<int>(activeProcessSteps.Select(x => x.ProcessStepId));
			var relationRowsWithTarget = relationRows
				.Where(x => x.ToProcessStepId.HasValue && activeStepIds.Contains(x.ToProcessStepId.Value))
				.ToList();

			if (!relationRowsWithTarget.Any())
			{
				return new TransitionResultRequirement();
			}

			var relationToStepMap = relationRowsWithTarget
				.GroupBy(x => x.RelationStepId)
				.ToDictionary(x => x.Key, x => x.First().ToProcessStepId!.Value);
			var stepOrderMap = activeProcessSteps
				.GroupBy(x => x.ProcessStepId)
				.ToDictionary(x => x.Key, x => x.First().OrderId);

			var options = tblWfwResultStepRepository
				.GetByRelationStepIds(relationRowsWithTarget.Select(x => x.RelationStepId))
				.Where(x => x.RelationStepId.HasValue && relationToStepMap.ContainsKey(x.RelationStepId.Value))
				.Where(x => x.IsActive == null || x.IsActive == 1)
				.Where(x => !string.IsNullOrWhiteSpace(x.Name))
				.OrderBy(x => stepOrderMap[relationToStepMap[x.RelationStepId!.Value]])
				.ThenBy(x => x.ResultStepId)
				.Select(x => new TransitionResultOption
				{
					ResultStepId = x.ResultStepId,
					RelationStepId = x.RelationStepId!.Value,
					ToProcessStepId = relationToStepMap[x.RelationStepId!.Value],
					Name = x.Name!.Trim()
				})
				.ToList();

			return new TransitionResultRequirement
			{
				Options = options
			};
		}

		private static string NormalizeTransitionChoice(string? value)
		{
			return (value ?? string.Empty).Trim();
		}

		private static string NormalizeMaterialBarcode(string? value)
		{
			return (value ?? string.Empty)
				.Trim()
				.Replace(" ", string.Empty)
				.Replace("\u200C", string.Empty)
				.Replace("\u200F", string.Empty)
				.Replace("[", string.Empty)
				.Replace("]", string.Empty)
				.Replace("(", string.Empty)
				.Replace(")", string.Empty)
				.Replace("{", string.Empty)
				.Replace("}", string.Empty)
				.Replace("۰", "0")
				.Replace("۱", "1")
				.Replace("۲", "2")
				.Replace("۳", "3")
				.Replace("۴", "4")
				.Replace("۵", "5")
				.Replace("۶", "6")
				.Replace("۷", "7")
				.Replace("۸", "8")
				.Replace("۹", "9")
				.Replace("٠", "0")
				.Replace("١", "1")
				.Replace("٢", "2")
				.Replace("٣", "3")
				.Replace("٤", "4")
				.Replace("٥", "5")
				.Replace("٦", "6")
				.Replace("٧", "7")
				.Replace("٨", "8")
				.Replace("٩", "9");
		}

		private bool TryRegisterConsumedMaterial(TblWfwOrderProcessStepDto currentStep, string normalizedMaterialBarcode, out string registerError)
		{
			registerError = string.Empty;

			var order = currentStep?.TblWfwOrderProcess?.TblLnsOrder;
			if (order == null)
			{
				registerError = "اطلاعات سفارش برای ثبت ماده مصرفی یافت نشد.";
				return false;
			}

			var allowedDefineObjectIds = (tblLnsCustomLensTypeMaterialService.GetAll().Result ?? Enumerable.Empty<TblLnsCustomLensTypeMaterialDto>())
				.Where(x => x.IsActive)
				.Where(x => x.DefineObjectId.HasValue && x.DefineObjectId.Value > 0)
				.Select(x => x.DefineObjectId!.Value)
				.Distinct()
				.ToHashSet();

			if (!allowedDefineObjectIds.Any())
			{
				registerError = "لیست مواد خام تعریف نشده است.";
				return false;
			}

			var barcodeRecNo = int.TryParse(normalizedMaterialBarcode, out var parsedRecNo) ? parsedRecNo : (int?)null;
			var matchedDefineObjects = (tblClrDefineObjectService.Search(normalizedMaterialBarcode).Result ?? new List<TblClrDefineObjectDto>())
				.Where(x => allowedDefineObjectIds.Contains(x.DefineObjectId))
				.Where(x => x.IsActive == null || x.IsActive == 1)
				.Where(x =>
					string.Equals(NormalizeMaterialBarcode(x.TechnicalSpecs), normalizedMaterialBarcode, StringComparison.OrdinalIgnoreCase) ||
					string.Equals(NormalizeMaterialBarcode(x.CodeObject), normalizedMaterialBarcode, StringComparison.OrdinalIgnoreCase) ||
					(barcodeRecNo.HasValue && x.RecNo == barcodeRecNo.Value))
				.GroupBy(x => x.DefineObjectId)
				.Select(x => x.First())
				.ToList();

			if (!matchedDefineObjects.Any())
			{
				registerError = "بارکد ماده خام معتبر نیست یا در مواد تعریف‌شده موجود نیست.";
				return false;
			}

			if (matchedDefineObjects.Count > 1)
			{
				registerError = "این بارکد بین چند ماده مشترک است. لطفا بارکد دقیق‌تری اسکن کنید.";
				return false;
			}

			var defineObject = matchedDefineObjects[0];
			var now = DateTime.Now;
			var userId = HttpContext.Session.Get<int?>("UserId");

			var existingRow = dbContext.TblWfwOrderRawMaterials.FirstOrDefault(x =>
				x.OrderId == order.OrderId &&
				x.ProcessStepId == currentStep.ProcessStepId &&
				x.DefineObjectId == defineObject.DefineObjectId);

			if (existingRow == null)
			{
				dbContext.TblWfwOrderRawMaterials.Add(new TblWfwOrderRawMaterial
				{
					OrderId = order.OrderId,
					OrderProcessId = currentStep.OrderProcessId,
					ProcessStepId = currentStep.ProcessStepId,
					DefineObjectId = defineObject.DefineObjectId,
					DefineObjectRecNo = defineObject.RecNo,
					Barcode = normalizedMaterialBarcode,
					MaterialName = defineObject.NameObject,
					Quantity = 1,
					DateCreate = now,
					DateUpdate = now,
					CreatedBy = userId,
					ModifiedBy = userId
				});
			}
			else
			{
				existingRow.Quantity += 1;
				existingRow.DateUpdate = now;
				existingRow.ModifiedBy = userId;
				existingRow.Barcode = normalizedMaterialBarcode;
				existingRow.DefineObjectRecNo ??= defineObject.RecNo;
				if (string.IsNullOrWhiteSpace(existingRow.MaterialName))
				{
					existingRow.MaterialName = defineObject.NameObject;
				}
			}

			dbContext.SaveChanges();
			return true;
		}

		private static bool IsLikelyScannerInput(int? scanDurationMs, int? scanLength)
		{
			if (!scanDurationMs.HasValue || !scanLength.HasValue)
			{
				return false;
			}

			if (scanLength.Value < 4 || scanLength.Value <= 1)
			{
				return false;
			}

			if (scanDurationMs.Value <= 0 || scanDurationMs.Value > 1500)
			{
				return false;
			}

			var avgGap = (double)scanDurationMs.Value / (scanLength.Value - 1);
			return avgGap <= 120;
		}

		private List<TblWfwOrderProcessStepDto> LoadInprogressSteps(int? roleId, int? customerId, string? factorNo)
		{
			var result = tblWfwOrderProcessStepService
				.GetAllByFilter((int)StatusRequest.Inprogress, null, null, customerId, roleId, null, null, null, null, "desc", "dateCreate", factorNo ?? string.Empty, null, null)
				.Result;

			return result.Item1 ?? new List<TblWfwOrderProcessStepDto>();
		}

		private static bool IsOrderCodeMatch(string? scannedCode, TblLnsOrderDto? order)
		{
			if (order == null)
			{
				return false;
			}

			var normalizedScanned = NormalizeOrderCode(scannedCode);
			if (string.IsNullOrWhiteSpace(normalizedScanned))
			{
				return false;
			}

			var expectedCode = NormalizeOrderCode(ResolveOrderCode(order));
			if (string.Equals(normalizedScanned, expectedCode, StringComparison.OrdinalIgnoreCase))
			{
				return true;
			}

			return long.TryParse(normalizedScanned, out var scannedOrderId) &&
				   scannedOrderId == order.OrderId;
		}

		private static string ResolveOrderCode(TblLnsOrderDto order)
		{
			if (!string.IsNullOrWhiteSpace(order.FactorNo))
			{
				return order.FactorNo.Trim();
			}

			var prefix = order.IndexDocument == (int)IndexDocument.LnsOrder ? "RX" : "ST";
			return prefix + order.OrderId.ToString().PadLeft(8, '0');
		}

		private static string NormalizeOrderCode(string? value)
		{
			if (string.IsNullOrWhiteSpace(value))
			{
				return string.Empty;
			}

			var normalized = value.Trim().Replace(" ", string.Empty).Replace("-", string.Empty);
			var chars = normalized.ToCharArray();
			for (var i = 0; i < chars.Length; i++)
			{
				// Convert Persian/Arabic digits to ASCII digits for stable order-code parsing.
				if (chars[i] >= '\u06F0' && chars[i] <= '\u06F9')
				{
					chars[i] = (char)('0' + (chars[i] - '\u06F0'));
				}
				else if (chars[i] >= '\u0660' && chars[i] <= '\u0669')
				{
					chars[i] = (char)('0' + (chars[i] - '\u0660'));
				}
			}

			return new string(chars).ToUpperInvariant();
		}

		[Authorize(Roles = "admin,OrderReq_Index")]
		[HttpPost]
		public IActionResult OrderReqRemove(int Id)
		{

			//tblLnsOrderService.UpdateStatus(Id, (int)StatusOrder.Cancel);
			return Json(new
			{
				success = true,
				message = ""
			});

		}


        [HttpPost]
        public JsonResult ListDefineCustomerSearch(string search)
        {
            List<ViewSalListCustomerDto> productes = new List<ViewSalListCustomerDto>();
            if (HttpContext.Session.Get<int?>("CustomerId") == null)
			{
				productes = viewSalListCustomerService.GetByName(search).Result;
			}
			else
			{
				productes.Add(viewSalListCustomerService.GetById(HttpContext.Session.Get<int>("CustomerId")).Result);

			}

			var items = new
			{
				results = (from item in productes
						   select new
						   {
							   id = item.DefineCustomerId,
							   text = item.NameFormal
						   })
			};
			return Json(items);
		}
        [HttpPost]
        public JsonResult ListUserSearch(string search)
        {
            var term = (search ?? string.Empty).Trim();
            var users = userService.GetAll().Result;
            if (!string.IsNullOrWhiteSpace(term))
            {
                users = users.Where(x =>
                    (!string.IsNullOrWhiteSpace(x.Username) && x.Username.Contains(term, StringComparison.OrdinalIgnoreCase)) ||
                    (!string.IsNullOrWhiteSpace(x.DisplayName) && x.DisplayName.Contains(term, StringComparison.OrdinalIgnoreCase)) ||
                    (!string.IsNullOrWhiteSpace(x.FirstName) && x.FirstName.Contains(term, StringComparison.OrdinalIgnoreCase)) ||
                    (!string.IsNullOrWhiteSpace(x.LastName) && x.LastName.Contains(term, StringComparison.OrdinalIgnoreCase)));
            }

            string BuildUserLabel(UserDto user)
            {
                if (!string.IsNullOrWhiteSpace(user.Username))
                    return user.Username;
                if (!string.IsNullOrWhiteSpace(user.DisplayName))
                    return user.DisplayName;
                var fullName = $"{user.FirstName} {user.LastName}".Trim();
                return fullName;
            }

            var items = new
            {
                results = (from item in users.Take(50)
                           select new
                           {
                               id = item.UserId,
                               text = BuildUserLabel(item)
                           })
            };
            return Json(items);
        }
        [HttpPost]
        [Authorize(Roles = "admin,ListOrder_Index")]
        public JsonResult DetailNew([FromBody] GridDto itemGrid)
        {
			itemGrid.Page = 1;
            int pageIndex = Convert.ToInt32(itemGrid.Page)-1 ;
            int pageSize = itemGrid.Rows=20;
            int totalRecords = 33;
            int? userId = itemGrid.createdById;
            int? roleId = HttpContext.Session.Get<bool>("IsAdmin") ? null : HttpContext.Session.Get<int>("RoleId");

            int? customerId = HttpContext.Session.Get<int?>("CustomerId").HasValue
                              ? HttpContext.Session.Get<int>("CustomerId")
                              : itemGrid.customerid;
  
            var item = tblLnsOrderService.GetAllByFilter(
                itemGrid.orderstatusid, itemGrid.indexdocumetid, customerId, userId, null,
                itemGrid.Page, itemGrid.Rows, itemGrid.Sord, itemGrid.Sidx, search: "", fromDate: itemGrid.fromDate, toDate: itemGrid.toDate
            );

            totalRecords = item.Result.Item2;
            if (totalRecords <= 0)
                return Json(new { });

            var listStatusOrder = Enum.GetValues(typeof(StatusOrder)).Cast<StatusOrder>()
                .Select(o => new {
                    Text = ((DescriptionAttribute[])(o.GetType()
                            .GetField(o.ToString())
                            .GetCustomAttributes(typeof(DescriptionAttribute), false)))[0].Description,
                    Value = (int)o
                }).ToList();

            var indexDocuments = Enum.GetValues(typeof(IndexDocument)).Cast<IndexDocument>()
                .Select(o => new {
                    Text = ((DescriptionAttribute[])(o.GetType()
                            .GetField(o.ToString())
                            .GetCustomAttributes(typeof(DescriptionAttribute), false)))[0].Description,
                    Value = (int)o
                }).ToList();

            var listCustomer = viewSalListCustomerService.GetAll().Result;
            var users = userService.GetAll().Result.ToList();

            string BuildUserLabel(UserDto user)
            {
                if (!string.IsNullOrWhiteSpace(user.Username))
                    return user.Username;
                if (!string.IsNullOrWhiteSpace(user.DisplayName))
                    return user.DisplayName;
                var fullName = $"{user.FirstName} {user.LastName}".Trim();
                return fullName;
            }

            var userLookup = users
                .Where(x => x != null)
                .GroupBy(x => x.UserId)
                .Select(x => x.First())
                .ToDictionary(x => x.UserId, BuildUserLabel);

            string ResolveUserName(int? userId)
            {
                if (!userId.HasValue)
                    return string.Empty;
                return userLookup.TryGetValue(userId.Value, out var userName) ? userName : string.Empty;
            }

            int totalPages = (int)Math.Ceiling((float)totalRecords / (float)pageSize);
            var jsonData = new
            {
                total = totalPages,
                page = itemGrid.Page,
                records = totalRecords,
                rows = from p in item.Result.Item1
                       join m in listCustomer on p.DefineCustomerId equals m.DefineCustomerId
                       join n in indexDocuments on p.IndexDocument equals n.Value
                       join o in listStatusOrder on p.StatusId equals o.Value
                       select new
                       {
                           id = p.OrderId,
                           factorNo = !string.IsNullOrWhiteSpace(p.FactorNo)
                               ? p.FactorNo
                               : (n.Value != (int)IndexDocument.LnsOrder ? "ST" : "RX") + p.OrderId.ToString().PadLeft(8, '0'),
                           statusOrder = o.Text,
                           customer = m.NameFormal,
                           createdByUser = ResolveUserName(p.CreatedBy),
                           indexDocuments = n.Text,
                           createDate = Convert.ToDateTime(p.CreateDate).ToPersianDateTime()
                       }
            };
            return Json(jsonData);
        }

        [Authorize(Roles = "admin,ListOrder_Index")]
		public JsonResult Detail()
		{
			GridDto itemGrid = JsonConvert.DeserializeObject<GridDto>(Request.Form.First().Key);
			int pageIndex = Convert.ToInt32(itemGrid.Page) - 1;
			int pageSize = itemGrid.Rows;
			int totalRecords = 0;
            int? userId = itemGrid.createdById;
            int? roleId = HttpContext.Session.Get<bool>("IsAdmin") ? null : HttpContext.Session.Get<int>("RoleId");

            int? customerId = HttpContext.Session.Get<int?>("CustomerId").HasValue ? HttpContext.Session.Get<int>("CustomerId") : itemGrid.customerid;
            string? factorNo = !string.IsNullOrWhiteSpace(HttpContext.Session.Get<string?>("FactorNo")) ? HttpContext.Session.Get<string>("FactorNo") : itemGrid.factorNo;

            //int? userId = HttpContext.Session.GetInt32("UserId");
            //int? roleId = HttpContext.Session.GetString("IsAdmin") == "True" ? null : HttpContext.Session.GetInt32("RoleId");

            //int? customerId = HttpContext.Session.GetInt32("CustomerId") ?? itemGrid.customerid;

            var item = tblLnsOrderService.GetAllByFilter(itemGrid.orderstatusid, itemGrid.indexdocumetid, customerId, userId, null, itemGrid.Page, itemGrid.Rows, itemGrid.Sord, itemGrid.Sidx, factorNo, itemGrid.fromDate, itemGrid.toDate);// GetByTaxPayerId(taxPayerId,null,page,rows); // roh
            //var item = tblWfwOrderProcessStepService.GetAllByFilter(itemGrid.statusrequestid, itemGrid.orderstatusid, itemGrid.indexdocumetid, customerId, roleId, null, itemGrid.Page, itemGrid.Rows, itemGrid.Sord, itemGrid.Sidx);// GetByTaxPayerId(taxPayerId,null,page,rows);

            totalRecords = item.Result.Item2;
			if (totalRecords <= 0)
			{
				return Json(new { });
			}

			var listStatusOrder = Enum.GetValues(typeof(StatusOrder)).Cast<StatusOrder>().Select(o => new { Text = ((DescriptionAttribute[])(o.GetType().GetField(o.ToString()).GetCustomAttributes(typeof(DescriptionAttribute), false)))[0].Description, Value = (int)o }).ToList();
			var indexDocuments = Enum.GetValues(typeof(IndexDocument)).Cast<IndexDocument>().Select(o => new { Text = ((DescriptionAttribute[])(o.GetType().GetField(o.ToString()).GetCustomAttributes(typeof(DescriptionAttribute), false)))[0].Description, Value = (int)o }).ToList();
			var listCustomer = viewSalListCustomerService.GetAll().Result;
            var users = userService.GetAll().Result.ToList();

            string BuildUserLabel(UserDto user)
            {
                if (!string.IsNullOrWhiteSpace(user.Username))
                    return user.Username;
                if (!string.IsNullOrWhiteSpace(user.DisplayName))
                    return user.DisplayName;
                var fullName = $"{user.FirstName} {user.LastName}".Trim();
                return fullName;
            }

            var userLookup = users
                .Where(x => x != null)
                .GroupBy(x => x.UserId)
                .Select(x => x.First())
                .ToDictionary(x => x.UserId, BuildUserLabel);

            string ResolveUserName(int? userId)
            {
                if (!userId.HasValue)
                    return string.Empty;
                return userLookup.TryGetValue(userId.Value, out var userName) ? userName : string.Empty;
            }

			int totalPages = (int)Math.Ceiling((float)totalRecords / (float)pageSize);
			var jsonData = new
			{
				total = totalPages,
				page = itemGrid.Page,
				records = totalRecords,
				rows = from p in item.Result.Item1
					   join m in listCustomer on p.DefineCustomerId equals m.DefineCustomerId
					   join n in indexDocuments on p.IndexDocument equals n.Value
					   join o in listStatusOrder on p.StatusId equals o.Value
					   select new
					   {
						   id = p.OrderId,
                           factorNo1 = !string.IsNullOrWhiteSpace(p.FactorNo)
                               ? p.FactorNo
                               : (n.Value != (int)IndexDocument.LnsOrder ? "ST" : "RX") + p.OrderId.ToString().PadLeft(8, '0'),
                           factorNo = !string.IsNullOrWhiteSpace(p.FactorNo)
                               ? p.FactorNo
                               : (n.Value != (int)IndexDocument.LnsOrder ? "ST" : "RX") + p.OrderId.ToString().PadLeft(8, '0'),
                           statusOrder = o.Text,
						   customer = m.NameFormal,
						   createdByUser = ResolveUserName(p.CreatedBy),
						   indexDocuments = n.Text,
						   //createDate = (Convert.ToDateTime(p.CreateDate)).ToPersianDateTime(), // roh 

                           createDate = Convert.ToDateTime(p.CreateDate).ToPersianDateTime()
                       }
			};
			return Json(jsonData); 

		}
		[Authorize(Roles = "admin,OrderReq_Index")]
		public JsonResult DetailReq()
		{
			GridDto itemGrid = JsonConvert.DeserializeObject<GridDto>(Request.Form.First().Key);
			int pageIndex = Convert.ToInt32(itemGrid.Page) - 1;
			int pageSize = itemGrid.Rows;
			int? roleId = HttpContext.Session.Get<bool>("IsAdmin") ? null : HttpContext.Session.Get<int>("RoleId");

			static int? NormalizeFilterValue(int? value) =>
				(value.HasValue && value.Value > 0) ? value : null;

			var orderStatusId = NormalizeFilterValue(itemGrid.orderstatusid);
			var indexDocumentId = NormalizeFilterValue(itemGrid.indexdocumetid);
			var requestStatusId = NormalizeFilterValue(itemGrid.statusrequestid);
			var createdById = NormalizeFilterValue(itemGrid.createdById);
			int? customerId = HttpContext.Session.Get<int?>("CustomerId").HasValue
				? HttpContext.Session.Get<int>("CustomerId")
				: NormalizeFilterValue(itemGrid.customerid);
			string factorNo = !string.IsNullOrWhiteSpace(HttpContext.Session.Get<string?>("FactorNo"))
				? HttpContext.Session.Get<string>("FactorNo")
				: itemGrid.factorNo;


            var allSteps = tblWfwOrderProcessStepService
                .GetAllByFilter(null, orderStatusId, indexDocumentId, customerId, roleId, createdById, null, null, null, "desc", "dateCreate", factorNo, itemGrid.fromDate, itemGrid.toDate)
                .Result.Item1 ?? new List<TblWfwOrderProcessStepDto>();

			var listStatusOrder = Enum.GetValues(typeof(StatusOrder)).Cast<StatusOrder>().Select(o => new { Text = ((DescriptionAttribute[])(o.GetType().GetField(o.ToString()).GetCustomAttributes(typeof(DescriptionAttribute), false)))[0].Description, Value = (int)o }).ToList();
			var listStatusRequest = Enum.GetValues(typeof(StatusRequest)).Cast<StatusRequest>().Select(o => new { Text = ((DescriptionAttribute[])(o.GetType().GetField(o.ToString()).GetCustomAttributes(typeof(DescriptionAttribute), false)))[0].Description, Value = (int)o }).ToList();
			var indexDocuments = Enum.GetValues(typeof(IndexDocument)).Cast<IndexDocument>().Select(o => new { Text = ((DescriptionAttribute[])(o.GetType().GetField(o.ToString()).GetCustomAttributes(typeof(DescriptionAttribute), false)))[0].Description, Value = (int)o }).ToList();
			var listCustomer = viewSalListCustomerService.GetAll().Result.ToList();
            var users = userService.GetAll().Result.ToList();

            string BuildUserLabel(UserDto user)
            {
                if (!string.IsNullOrWhiteSpace(user.Username))
                    return user.Username;
                if (!string.IsNullOrWhiteSpace(user.DisplayName))
                    return user.DisplayName;
                var fullName = $"{user.FirstName} {user.LastName}".Trim();
                return fullName;
            }

            var userLookup = users
                .Where(x => x != null)
                .GroupBy(x => x.UserId)
                .Select(x => x.First())
                .ToDictionary(x => x.UserId, BuildUserLabel);

            string ResolveUserName(int? userId)
            {
                if (!userId.HasValue)
                    return string.Empty;
                return userLookup.TryGetValue(userId.Value, out var userName) ? userName : string.Empty;
            }

            var statusOrderLookup = listStatusOrder.ToDictionary(x => x.Value, x => x.Text);
            var statusRequestLookup = listStatusRequest.ToDictionary(x => x.Value, x => x.Text);
            var indexDocumentLookup = indexDocuments.ToDictionary(x => x.Value, x => x.Text);
            var customerLookup = listCustomer
                .Where(x => x.DefineCustomerId.HasValue)
                .GroupBy(x => x.DefineCustomerId!.Value)
                .ToDictionary(x => x.Key, x => x.First().NameFormal ?? string.Empty);
			var relationStepByFromTo = (tblWfwRelationStepRepository.GetAll() ?? Array.Empty<TblWfwRelationStep>())
				.Where(x => x.FromProcessStepId > 0 && x.ToProcessStepId.HasValue)
				.GroupBy(x => (From: x.FromProcessStepId, To: x.ToProcessStepId!.Value))
				.ToDictionary(
					x => x.Key,
					x => x.OrderBy(y => y.RelationStepId).First().RelationStepId);
			var resultNamesByRelationId = (tblWfwResultStepRepository.GetAll() ?? Array.Empty<TblWfwResultStep>())
				.Where(x => x.RelationStepId.HasValue && x.RelationStepId.Value > 0)
				.Where(x => x.IsActive == null || x.IsActive == 1)
				.Where(x => !string.IsNullOrWhiteSpace(x.Name))
				.GroupBy(x => x.RelationStepId!.Value)
				.ToDictionary(
					x => x.Key,
					x => x.Select(y => y.Name!.Trim())
						.Where(y => !string.IsNullOrWhiteSpace(y))
						.Distinct(StringComparer.OrdinalIgnoreCase)
						.ToList());

			string ResolveTransitionResult(int? fromProcessStepId, int? toProcessStepId)
			{
				if (!fromProcessStepId.HasValue || !toProcessStepId.HasValue)
					return string.Empty;

				if (!relationStepByFromTo.TryGetValue((fromProcessStepId.Value, toProcessStepId.Value), out var relationStepId))
					return string.Empty;

				if (!resultNamesByRelationId.TryGetValue(relationStepId, out var resultNames) || resultNames.Count == 0)
					return string.Empty;

				return resultNames.Count == 1 ? resultNames[0] : string.Join(" / ", resultNames);
			}

			List<OrderReqHistoryRow> BuildStepHistoryRows(List<TblWfwOrderProcessStepDto> orderedSteps)
			{
				var historyRows = new List<OrderReqHistoryRow>();
				for (var index = 0; index < orderedSteps.Count; index++)
				{
					var step = orderedSteps[index];
					statusRequestLookup.TryGetValue(step.StatusId, out var stepStatusName);

					string transitionResult = string.Empty;
					if (index < orderedSteps.Count - 1)
					{
						var nextStep = orderedSteps[index + 1];
						transitionResult = ResolveTransitionResult(step.ProcessStepId, nextStep.ProcessStepId);
					}

					historyRows.Add(new OrderReqHistoryRow
					{
						StepName = step.TblWfwProcessStep?.Name ?? string.Empty,
						TransitionResult = transitionResult,
						UserName = ResolveUserName(step.UserId),
						DateCreate = step.DateCreate.ToPersianDateTime(),
						DateComplete = step.DateComplete.HasValue ? step.DateComplete.Value.ToPersianDateTime() : string.Empty,
						StatusRequest = stepStatusName ?? string.Empty
					});
				}

				return historyRows;
			}

            var orderRows = allSteps
                .Where(x => x?.TblWfwOrderProcess?.TblLnsOrder != null)
                .GroupBy(x => x.TblWfwOrderProcess.TblLnsOrder.OrderId)
                .Select(group =>
                {
                    var orderedSteps = group.OrderBy(x => x.DateCreate).ToList();
                    var currentStep = orderedSteps
                        .Where(x => x.StatusId == (int)StatusRequest.Inprogress)
                        .OrderByDescending(x => x.DateCreate)
                        .FirstOrDefault()
                        ?? orderedSteps.LastOrDefault();

                    var order = currentStep?.TblWfwOrderProcess?.TblLnsOrder ?? orderedSteps.Last().TblWfwOrderProcess.TblLnsOrder;
                    var factorNoValue = !string.IsNullOrWhiteSpace(order.FactorNo)
                        ? order.FactorNo
                        : (order.IndexDocument != (int)IndexDocument.LnsOrder ? "ST" : "RX") + order.OrderId.ToString().PadLeft(8, '0');

                    var customerName = string.Empty;
                    if (order.DefineCustomerId.HasValue)
                    {
                        customerLookup.TryGetValue(order.DefineCustomerId.Value, out customerName);
                    }
                    indexDocumentLookup.TryGetValue(order.IndexDocument ?? 0, out var indexDocumentName);
                    statusOrderLookup.TryGetValue(order.StatusId, out var statusOrderName);
                    statusRequestLookup.TryGetValue(currentStep?.StatusId ?? 0, out var statusRequestName);

                    return new OrderReqGridRow
                    {
                        Id = order.OrderId,
                        CurrentStepId = currentStep?.OrderProcessStepId ?? 0,
                        FactorNo = factorNoValue,
                        Customer = customerName ?? string.Empty,
                        CreatedByUser = ResolveUserName(order.CreatedBy),
                        IndexDocuments = indexDocumentName ?? string.Empty,
                        StatusOrder = statusOrderName ?? string.Empty,
                        StatusRequest = statusRequestName ?? string.Empty,
                        StepName = currentStep?.TblWfwProcessStep?.Name ?? string.Empty,
                        StepCode = currentStep?.TblWfwProcessStep?.Code ?? string.Empty,
                        DateCreate = currentStep?.DateCreate.ToPersianDateTime() ?? string.Empty,
                        DateComplete = currentStep?.DateComplete.HasValue == true ? currentStep.DateComplete.Value.ToPersianDateTime() : string.Empty,
                        CurrentStatusId = currentStep?.StatusId ?? 0,
                        StepHistory = BuildStepHistoryRows(orderedSteps)
                    };
                })
                .Where(x => !requestStatusId.HasValue || x.CurrentStatusId == requestStatusId.Value)
                .ToList();

            var inprogressCount = orderRows.Count(x => x.CurrentStatusId == (int)StatusRequest.Inprogress);
            var completeCount = orderRows.Count(x => x.CurrentStatusId == (int)StatusRequest.Complete);

            var sortedRows = ApplyOrderReqSort(orderRows, itemGrid.Sidx, itemGrid.Sord);
            var totalRecords = sortedRows.Count;
            var pagedRows = pageSize > 0
                ? sortedRows.Skip(Math.Max(pageIndex, 0) * pageSize).Take(pageSize).ToList()
                : sortedRows.ToList();

			int totalPages = totalRecords > 0 && pageSize > 0 ? (int)Math.Ceiling((float)totalRecords / (float)pageSize) : 0;
			var jsonData = new
			{
				total = totalPages,
				page = itemGrid.Page,
				records = totalRecords,
				inprogressCount = inprogressCount,
				completeCount = completeCount,
				rows = pagedRows.Select(x => new
                {
                    id = x.Id,
                    currentStepId = x.CurrentStepId,
                    factorNo1 = x.FactorNo,
                    factorNo = x.FactorNo,
                    stepName = x.StepName,
                    stepCode = x.StepCode,
                    statusOrder = x.StatusOrder,
                    statusRequest = x.StatusRequest,
                    customer = x.Customer,
                    createdByUser = x.CreatedByUser,
                    indexDocuments = x.IndexDocuments,
                    dateCreate = x.DateCreate,
                    dateComplete = x.DateComplete,
                    stepHistory = x.StepHistory
                })
			};
			return Json(jsonData); 

		}

        private static List<OrderReqGridRow> ApplyOrderReqSort(List<OrderReqGridRow> rows, string? sortField, string? sortOrder)
        {
            var sortAsc = string.Equals(sortOrder, "asc", StringComparison.OrdinalIgnoreCase);
            var key = (sortField ?? string.Empty).Trim().ToLowerInvariant();

            return key switch
            {
                "customer" => sortAsc ? rows.OrderBy(x => x.Customer).ToList() : rows.OrderByDescending(x => x.Customer).ToList(),
                "createdbyuser" => sortAsc ? rows.OrderBy(x => x.CreatedByUser).ToList() : rows.OrderByDescending(x => x.CreatedByUser).ToList(),
                "factorno" or "factorno1" => sortAsc ? rows.OrderBy(x => x.FactorNo).ToList() : rows.OrderByDescending(x => x.FactorNo).ToList(),
                "indexdocuments" => sortAsc ? rows.OrderBy(x => x.IndexDocuments).ToList() : rows.OrderByDescending(x => x.IndexDocuments).ToList(),
                "stepname" => sortAsc ? rows.OrderBy(x => x.StepName).ToList() : rows.OrderByDescending(x => x.StepName).ToList(),
                "stepcode" => sortAsc ? rows.OrderBy(x => x.StepCode).ToList() : rows.OrderByDescending(x => x.StepCode).ToList(),
                "statusorder" => sortAsc ? rows.OrderBy(x => x.StatusOrder).ToList() : rows.OrderByDescending(x => x.StatusOrder).ToList(),
                "statusrequest" => sortAsc ? rows.OrderBy(x => x.StatusRequest).ToList() : rows.OrderByDescending(x => x.StatusRequest).ToList(),
                "datecreate" => sortAsc ? rows.OrderBy(x => x.DateCreate).ToList() : rows.OrderByDescending(x => x.DateCreate).ToList(),
                "datecomplete" => sortAsc ? rows.OrderBy(x => x.DateComplete).ToList() : rows.OrderByDescending(x => x.DateComplete).ToList(),
                _ => sortAsc ? rows.OrderBy(x => x.FactorNo).ToList() : rows.OrderByDescending(x => x.FactorNo).ToList()
            };
        }

        private sealed class OrderReqGridRow
        {
            public long Id { get; set; }
            public long CurrentStepId { get; set; }
            public string FactorNo { get; set; } = string.Empty;
            public string Customer { get; set; } = string.Empty;
            public string CreatedByUser { get; set; } = string.Empty;
            public string IndexDocuments { get; set; } = string.Empty;
            public string StatusOrder { get; set; } = string.Empty;
            public string StatusRequest { get; set; } = string.Empty;
            public string StepName { get; set; } = string.Empty;
            public string StepCode { get; set; } = string.Empty;
            public string DateCreate { get; set; } = string.Empty;
            public string DateComplete { get; set; } = string.Empty;
            public int CurrentStatusId { get; set; }
            public List<OrderReqHistoryRow> StepHistory { get; set; } = new();
        }

        private sealed class OrderReqHistoryRow
        {
            public string StepName { get; set; } = string.Empty;
            public string TransitionResult { get; set; } = string.Empty;
            public string UserName { get; set; } = string.Empty;
            public string DateCreate { get; set; } = string.Empty;
            public string DateComplete { get; set; } = string.Empty;
            public string StatusRequest { get; set; } = string.Empty;
        }

        private static string BuildCustomLensPrintUrl(string? template, long orderId, string? factorNo, string? printer)
        {
            if (string.IsNullOrWhiteSpace(template))
            {
                return string.Empty;
            }

            var replaced = CustomLensPrintPlaceholderRegex.Replace(template, match =>
            {
                var key = match.Groups[1].Value.ToLowerInvariant();
                return key switch
                {
                    "orderid" => Uri.EscapeDataString(orderId.ToString()),
                    "factorno" => Uri.EscapeDataString(factorNo ?? string.Empty),
                    "printer" => Uri.EscapeDataString(printer ?? string.Empty),
                    _ => string.Empty
                };
            }).Trim();

            if (string.IsNullOrWhiteSpace(replaced))
            {
                return string.Empty;
            }

            var hasPrinterPlaceholder = template.Contains("{printer}", StringComparison.OrdinalIgnoreCase);
            if (!hasPrinterPlaceholder && !string.IsNullOrWhiteSpace(printer))
            {
                replaced = QueryHelpers.AddQueryString(replaced, "printer", printer);
            }

            return replaced;
        }

        [Authorize]
        public IActionResult Print(string orderId,int printType)
        {

            ViewBag.PrintType=printType;
            ViewBag.PrintUrl= configuration.GetConnectionString("PrintUrl");
            ViewBag.Id = orderId;

            return View();
        }



    }
}



