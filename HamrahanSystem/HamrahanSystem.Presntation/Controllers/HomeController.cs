using System.Diagnostics;
using System.Globalization;
using FastReport;
using FastReport.Export.Pdf;
using FastReport.Utils;
using HamrahanSystem.Application.DTOs;
using HamrahanSystem.Application.UseCaseInterface;
using HamrahanSystem.Presntation.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HamrahanSystem.Presntation.Controllers
{
    [Authorize]
    public class HomeController(
        ILogger<HomeController> logger,
        ITblLnsOrderService tblLnsOrderService,
        ITblClrDefineObjectService tblClrDefineObjectService,
        IViewSalListCustomerService viewSalListCustomerService,
        ITblDefineServiceService tblDefineServiceService,
        IWebHostEnvironment webHostEnvironment) : Controller
    {
        private const string CustomLensTemplateFileName = "CustomLensRxOrder.frx";
        private readonly ILogger<HomeController> _logger = logger;

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> PDFReportPage2(string? reportname, string? selectedValue, string? hash, string? printer)
        {
            _ = hash;
            _ = printer;

            if (!string.Equals(reportname, "RXOrder", StringComparison.OrdinalIgnoreCase))
            {
                return BadRequest("Unsupported report name.");
            }

            var key = (selectedValue ?? string.Empty).Trim();
            if (string.IsNullOrWhiteSpace(key))
            {
                return BadRequest("SelectedValue is required.");
            }

            var order = await ResolveOrderAsync(key);
            if (order == null)
            {
                return NotFound("Order was not found.");
            }

            var templatePath = Path.Combine(webHostEnvironment.ContentRootPath, "Reports", CustomLensTemplateFileName);
            if (!System.IO.File.Exists(templatePath))
            {
                _logger.LogError("Custom lens report template was not found. Path={Path}", templatePath);
                return NotFound("Custom lens report template was not found.");
            }

            var reportData = await BuildCustomLensReportDataAsync(order);
            var pdfBytes = BuildCustomLensPdf(templatePath, reportData.HeaderRows, reportData.DetailRows);
            return File(pdfBytes, "application/pdf");
        }

        private async Task<TblLnsOrderDto?> ResolveOrderAsync(string selectedValue)
        {
            if (int.TryParse(selectedValue, NumberStyles.Integer, CultureInfo.InvariantCulture, out var orderId) && orderId > 0)
            {
                var byId = await tblLnsOrderService.GetById(orderId);
                if (byId?.OrderId > 0)
                {
                    return byId;
                }
            }

            var factor = selectedValue.Trim().ToUpperInvariant();
            var list = await tblLnsOrderService.GetAllByFilter(
                null,
                (int)IndexDocument.LnsOrder,
                null,
                null,
                200,
                1,
                200,
                "desc",
                "OrderId",
                factor);

            var found = (list.Item1 ?? new List<TblLnsOrderDto>())
                .FirstOrDefault(x => !string.IsNullOrWhiteSpace(x.FactorNo) &&
                                     string.Equals(x.FactorNo.Trim(), factor, StringComparison.OrdinalIgnoreCase));

            if (found?.OrderId > 0 && found.OrderId <= int.MaxValue)
            {
                return await tblLnsOrderService.GetById((int)found.OrderId);
            }

            return null;
        }

        private async Task<CustomLensReportData> BuildCustomLensReportDataAsync(TblLnsOrderDto order)
        {
            var reportData = new CustomLensReportData();

            var orderMeta = ParseMeta(order.Description);
            var rightEye = order.TblLnsOrderItems?.FirstOrDefault(i => i.IsRight == 1) ?? order.TblLnsOrderItems?.FirstOrDefault();
            var leftEye = order.TblLnsOrderItems?.FirstOrDefault(i => i.IsRight == 0) ?? order.TblLnsOrderItems?.Skip(1).FirstOrDefault();
            var rightEyeMeta = ParseMeta(rightEye?.Description);
            var leftEyeMeta = ParseMeta(leftEye?.Description);

            var factorNo = ResolveFactorNo(order);
            var customerName = await ResolveCustomerNameAsync(order);
            var productName = await ResolveProductNameAsync(order, orderMeta);
            var services = await ResolveServicesAsync(order, orderMeta);

            reportData.HeaderRows.Add(new LnsOrdePrivateRow
            {
                OrderId = order.OrderId,
                Company = customerName,
                CompanyId = string.Empty,
                CreateDate = Safe(order.CreateDate),
                OrderNo = factorNo,
                HBox = ToText(order.HBox),
                VBox = ToText(order.VBox),
                DBL = ToText(order.Dbl),
                ED = ToText(order.EffectiveDiameter),
                PA = ToText(order.Panto),
                FFA = ToText(order.Ffa),
                VD = ToText(order.Vd),
                Base = ResolveOrderBase(order, orderMeta),
                Frame = ResolveFrame(order, orderMeta),
                Color = ResolveOrderColor(order, orderMeta),
                Comment = ResolveComment(order, orderMeta),
                Mobile = ResolveContact(orderMeta, "MOBILE", "CELLPHONE"),
                Tel = ResolveContact(orderMeta, "TEL", "PHONE"),
                Masrafkonande = Safe(order.Consumer),
                StoreName = Safe(order.StoreName),
                Corridor = ResolveCorridor(order.Corridor, orderMeta),
                NameKala = productName,
                Services = services,
                Coating = ResolveCoating(order, orderMeta)
            });

            reportData.DetailRows.Add(BuildEyeRow(order, rightEye, rightEyeMeta, orderMeta, factorNo, true));
            reportData.DetailRows.Add(BuildEyeRow(order, leftEye, leftEyeMeta, orderMeta, factorNo, false));

            return reportData;
        }

        private async Task<string> ResolveCustomerNameAsync(TblLnsOrderDto order)
        {
            if (!order.DefineCustomerId.HasValue || order.DefineCustomerId.Value <= 0)
            {
                return "-";
            }

            try
            {
                var customer = await viewSalListCustomerService.GetById(order.DefineCustomerId.Value);
                return Safe(customer?.NameFormal);
            }
            catch (Exception ex)
            {
                _logger.LogWarning(ex, "Could not resolve customer for id {CustomerId}", order.DefineCustomerId.Value);
                return "-";
            }
        }

        private async Task<string> ResolveProductNameAsync(TblLnsOrderDto order, Dictionary<string, string> orderMeta)
        {
            var fromMeta = FirstNonEmpty(
                MetaValue(orderMeta, "NAMEKALA"),
                MetaValue(orderMeta, "PRODUCT"),
                MetaValue(orderMeta, "PRODUCTNAME"));
            if (!string.IsNullOrWhiteSpace(fromMeta))
            {
                return fromMeta;
            }

            if (order.DefineObjectId.HasValue && order.DefineObjectId.Value > 0)
            {
                try
                {
                    var defineObject = await tblClrDefineObjectService.GetById(order.DefineObjectId.Value);
                    if (!string.IsNullOrWhiteSpace(defineObject?.NameObject))
                    {
                        return defineObject.NameObject.Trim();
                    }
                }
                catch (Exception ex)
                {
                    _logger.LogWarning(ex, "Could not resolve product name for define object {DefineObjectId}", order.DefineObjectId.Value);
                }
            }

            var fromItem = order.TblLnsOrderItems?.FirstOrDefault()?.TblClrDefineObject?.NameObject;
            if (!string.IsNullOrWhiteSpace(fromItem))
            {
                return fromItem.Trim();
            }

            var fromDesign = FirstNonEmpty(order.TblLnsDesignType?.Name, order.TblLnsLensType?.Name);
            return string.IsNullOrWhiteSpace(fromDesign) ? "-" : fromDesign;
        }

        private async Task<string> ResolveServicesAsync(TblLnsOrderDto order, Dictionary<string, string> orderMeta)
        {
            var fromMeta = FirstNonEmpty(
                MetaValue(orderMeta, "SERVICES"),
                MetaValue(orderMeta, "SERVICE"),
                MetaValue(orderMeta, "EXTRASERVICES"),
                MetaValue(orderMeta, "EXTRA SERVICES"));
            if (!string.IsNullOrWhiteSpace(fromMeta))
            {
                return fromMeta;
            }

            var ids = (order.TblLnsOrderservices ?? new List<TblLnsOrderserviceDto>())
                .Where(x => x?.DefineServiceId.HasValue == true && x.DefineServiceId.Value > 0)
                .Select(x => x!.DefineServiceId!.Value)
                .Distinct()
                .ToList();

            if (ids.Count == 0)
            {
                return "-";
            }

            var names = new List<string>();
            foreach (var id in ids)
            {
                try
                {
                    var service = await tblDefineServiceService.GetById(id);
                    if (!string.IsNullOrWhiteSpace(service?.ServiceName))
                    {
                        names.Add(service.ServiceName.Trim());
                    }
                }
                catch (Exception ex)
                {
                    _logger.LogWarning(ex, "Could not resolve service name for id {ServiceId}", id);
                }
            }

            if (names.Count > 0)
            {
                return string.Join(" - ", names.Distinct(StringComparer.OrdinalIgnoreCase));
            }

            return string.Join(" - ", ids);
        }

        private static LnsOrderDetailPrivateRow BuildEyeRow(
            TblLnsOrderDto order,
            TblLnsOrderItemDto? eye,
            Dictionary<string, string> eyeMeta,
            Dictionary<string, string> orderMeta,
            string factorNo,
            bool isRight)
        {
            var sideLabel = isRight ? "RIGHT" : "LEFT";
            var sideId = isRight ? 1 : 0;

            return new LnsOrderDetailPrivateRow
            {
                OrderId = order.OrderId,
                OrderDetailId = eye?.OrderItemId ?? 0,
                CodeKala = factorNo,
                NameKala = string.Empty,
                Axis = ResolveAxis(eye, eyeMeta),
                SPH = ResolveSph(eye, eyeMeta),
                CYL = ResolveCyl(eye, eyeMeta),
                ADD = ResolveAdd(eye, eyeMeta),
                PRISM = ResolvePrism(eye, eyeMeta),
                BPRISM = ResolveBasePrism(eye, eyeMeta),
                Fitting = ResolveFitting(order, eye, eyeMeta, orderMeta, isRight),
                DIA = ResolveDia(eye, eyeMeta),
                PD = ResolvePd(order, eye, isRight),
                DC = ResolveDc(eye, eyeMeta),
                NO = sideLabel,
                Masrafkonande = Safe(order.Consumer),
                StoreName = Safe(order.StoreName),
                SideType = sideLabel,
                SideTypeId = sideId
            };
        }

        private static byte[] BuildCustomLensPdf(string templatePath, List<LnsOrdePrivateRow> headerRows, List<LnsOrderDetailPrivateRow> detailRows)
        {
            using var report = new Report();
            Config.WebMode = true;
            report.Load(templatePath);
            report.RegisterData(headerRows, "LnsOrdePrivate");
            report.RegisterData(detailRows, "LnsOrderDetailPrivate");
            report.RegisterData(headerRows, "LnsOrder");
            report.RegisterData(detailRows, "LnsOrderDetail");
            var orderSource = report.GetDataSource("LnsOrdePrivate");
            if (orderSource != null)
            {
                orderSource.Enabled = true;
            }

            var detailSource = report.GetDataSource("LnsOrderDetailPrivate");
            if (detailSource != null)
            {
                detailSource.Enabled = true;
            }

            var legacyOrderSource = report.GetDataSource("LnsOrder");
            if (legacyOrderSource != null)
            {
                legacyOrderSource.Enabled = true;
            }

            var legacyDetailSource = report.GetDataSource("LnsOrderDetail");
            if (legacyDetailSource != null)
            {
                legacyDetailSource.Enabled = true;
            }
            report.Prepare();

            using var stream = new MemoryStream();
            using var pdf = new PDFExport { ShowProgress = false };
            report.Export(pdf, stream);
            return stream.ToArray();
        }

        private static string ResolveFactorNo(TblLnsOrderDto order)
        {
            if (!string.IsNullOrWhiteSpace(order.FactorNo))
            {
                return order.FactorNo.Trim();
            }

            return $"RX{order.OrderId.ToString().PadLeft(8, '0')}";
        }

        private static string ResolveOrderBase(TblLnsOrderDto order, Dictionary<string, string> orderMeta)
        {
            var fromMeta = FirstNonEmpty(MetaValue(orderMeta, "BASECURVE"), MetaValue(orderMeta, "BASE"));
            if (!string.IsNullOrWhiteSpace(fromMeta))
            {
                return fromMeta;
            }

            return ToText(order.ItemBase);
        }

        private static string ResolveFrame(TblLnsOrderDto order, Dictionary<string, string> orderMeta)
        {
            var fromMeta = FirstNonEmpty(MetaValue(orderMeta, "FRAME"), MetaValue(orderMeta, "FRAMETYPE"));
            if (!string.IsNullOrWhiteSpace(fromMeta))
            {
                return fromMeta;
            }

            return FirstNonEmpty(order.TblLnsFrameType?.Name, "-");
        }

        private static string ResolveOrderColor(TblLnsOrderDto order, Dictionary<string, string> orderMeta)
        {
            var fromMeta = FirstNonEmpty(MetaValue(orderMeta, "COLOR"), MetaValue(orderMeta, "COLORING"));
            if (!string.IsNullOrWhiteSpace(fromMeta))
            {
                return fromMeta;
            }

            return Safe(order.Color);
        }

        private static string ResolveCoating(TblLnsOrderDto order, Dictionary<string, string> orderMeta)
        {
            var fromMeta = MetaValue(orderMeta, "COATING");
            if (!string.IsNullOrWhiteSpace(fromMeta))
            {
                return fromMeta;
            }

            return FirstNonEmpty(order.TblLnsCoating?.Name, "-");
        }

        private static string ResolveCorridor(int? corridor, Dictionary<string, string> orderMeta)
        {
            var fromMeta = MetaValue(orderMeta, "CORRIDOR");
            if (!string.IsNullOrWhiteSpace(fromMeta))
            {
                return fromMeta;
            }

            return corridor switch
            {
                null => "-",
                0 => "AUTO",
                14 => "14 mm",
                16 => "16 mm",
                18 => "18 mm",
                _ => $"{corridor} mm"
            };
        }

        private static string ResolveComment(TblLnsOrderDto order, Dictionary<string, string> orderMeta)
        {
            var fromMeta = FirstNonEmpty(
                MetaValue(orderMeta, "COMMENT"),
                MetaValue(orderMeta, "DESCRIPTION"),
                MetaValue(orderMeta, "DESC"));
            if (!string.IsNullOrWhiteSpace(fromMeta))
            {
                return fromMeta;
            }

            var fromDescription = ExtractDescriptionComment(order.Description);
            if (!string.IsNullOrWhiteSpace(fromDescription) && fromDescription != "-")
            {
                return fromDescription;
            }

            return Safe(order.BeforeLensSpec);
        }

        private static string ExtractDescriptionComment(string? description)
        {
            if (string.IsNullOrWhiteSpace(description))
            {
                return "-";
            }

            var chunks = description
                .Split(';', StringSplitOptions.RemoveEmptyEntries)
                .Select(x => x.Trim())
                .Where(x => !string.IsNullOrWhiteSpace(x) && !x.Contains('='))
                .ToList();

            return chunks.Count > 0 ? string.Join(" - ", chunks) : "-";
        }

        private static string ResolveContact(Dictionary<string, string> orderMeta, params string[] keys)
        {
            foreach (var key in keys)
            {
                var value = MetaValue(orderMeta, key);
                if (!string.IsNullOrWhiteSpace(value))
                {
                    return value;
                }
            }

            return "-";
        }

        private static string ResolveSph(TblLnsOrderItemDto? eye, Dictionary<string, string> eyeMeta)
        {
            var fromMeta = MetaValue(eyeMeta, "SPH");
            if (!string.IsNullOrWhiteSpace(fromMeta))
            {
                return fromMeta;
            }

            return FirstNonEmpty(
                eye?.TblLnsSph?.Name,
                eye?.TblLnsLensIndexRSph?.TblLnsSph?.Name,
                eye?.SphId?.ToString(CultureInfo.InvariantCulture),
                "-");
        }

        private static string ResolveCyl(TblLnsOrderItemDto? eye, Dictionary<string, string> eyeMeta)
        {
            var fromMeta = MetaValue(eyeMeta, "CYL");
            if (!string.IsNullOrWhiteSpace(fromMeta))
            {
                return fromMeta;
            }

            return FirstNonEmpty(
                eye?.TblLnsCyl?.Name,
                eye?.TblLnsSphCyl?.TblLnsCyl?.Name,
                eye?.CylId?.ToString(CultureInfo.InvariantCulture),
                "-");
        }

        private static string ResolveAxis(TblLnsOrderItemDto? eye, Dictionary<string, string> eyeMeta)
        {
            var fromMeta = MetaValue(eyeMeta, "AXIS");
            if (!string.IsNullOrWhiteSpace(fromMeta))
            {
                return fromMeta;
            }

            return ToText(eye?.Axis);
        }

        private static string ResolveAdd(TblLnsOrderItemDto? eye, Dictionary<string, string> eyeMeta)
        {
            var fromMeta = MetaValue(eyeMeta, "ADD");
            if (!string.IsNullOrWhiteSpace(fromMeta))
            {
                return fromMeta;
            }

            if (eye?.ItemAdd.HasValue != true)
            {
                return "-";
            }

            var raw = eye.ItemAdd.Value;
            if (Math.Abs(raw) >= 20)
            {
                return (raw / 100m).ToString("0.##", CultureInfo.InvariantCulture);
            }

            return raw.ToString(CultureInfo.InvariantCulture);
        }

        private static string ResolvePrism(TblLnsOrderItemDto? eye, Dictionary<string, string> eyeMeta)
        {
            var fromMeta = MetaValue(eyeMeta, "PRISM");
            if (!string.IsNullOrWhiteSpace(fromMeta))
            {
                return fromMeta;
            }

            if (eye?.Prism.HasValue != true)
            {
                return "-";
            }

            var raw = eye.Prism.Value;
            if (Math.Abs(raw) >= 20)
            {
                return (raw / 100m).ToString("0.##", CultureInfo.InvariantCulture);
            }

            return raw.ToString(CultureInfo.InvariantCulture);
        }

        private static string ResolveBasePrism(TblLnsOrderItemDto? eye, Dictionary<string, string> eyeMeta)
        {
            var fromMeta = FirstNonEmpty(
                MetaValue(eyeMeta, "BPRISM"),
                MetaValue(eyeMeta, "BASEOFPRISM"),
                MetaValue(eyeMeta, "BP"));
            if (!string.IsNullOrWhiteSpace(fromMeta))
            {
                return fromMeta;
            }

            return eye?.BaseOfPrism switch
            {
                1 => "UP",
                2 => "DOWN",
                3 => "IN",
                4 => "OUT",
                5 => "IN/UP",
                6 => "IN/DOWN",
                7 => "OUT/UP",
                8 => "OUT/DOWN",
                _ => ToText(eye?.BaseOfPrism)
            };
        }

        private static string ResolveFitting(
            TblLnsOrderDto order,
            TblLnsOrderItemDto? eye,
            Dictionary<string, string> eyeMeta,
            Dictionary<string, string> orderMeta,
            bool isRight)
        {
            var fromMeta = FirstNonEmpty(
                MetaValue(eyeMeta, "FITTING"),
                MetaValue(eyeMeta, "SEG"),
                isRight ? MetaValue(orderMeta, "FITTINGR") : MetaValue(orderMeta, "FITTINGL"),
                isRight ? MetaValue(orderMeta, "FITTING-R") : MetaValue(orderMeta, "FITTING-L"),
                isRight ? MetaValue(orderMeta, "SEGR") : MetaValue(orderMeta, "SEGL"));
            if (!string.IsNullOrWhiteSpace(fromMeta))
            {
                return FormatTwoDecimal(fromMeta);
            }

            if (eye?.Fitting.HasValue == true)
            {
                return FormatTwoDecimal(eye.Fitting.Value.ToString("0.##", CultureInfo.InvariantCulture));
            }

            return FormatTwoDecimal((isRight ? order.FittingR : order.FittingL)?.ToString(CultureInfo.InvariantCulture));
        }

        private static string ResolveDia(TblLnsOrderItemDto? eye, Dictionary<string, string> eyeMeta)
        {
            var fromMeta = MetaValue(eyeMeta, "DIA");
            if (!string.IsNullOrWhiteSpace(fromMeta))
            {
                return fromMeta;
            }

            return ToText(eye?.Dia);
        }

        private static string ResolvePd(TblLnsOrderDto order, TblLnsOrderItemDto? eye, bool isRight)
        {
            if (eye?.Ipd.HasValue == true)
            {
                return ToText(eye.Ipd);
            }

            return ToText(isRight ? order.IpdR : order.IpdL);
        }

        private static string ResolveDc(TblLnsOrderItemDto? eye, Dictionary<string, string> eyeMeta)
        {
            var fromMeta = MetaValue(eyeMeta, "DC");
            if (!string.IsNullOrWhiteSpace(fromMeta))
            {
                return fromMeta;
            }

            return ToText(eye?.Dc);
        }

        private static string FormatTwoDecimal(string? value)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                return "-";
            }

            var normalized = value.Trim().Replace("?", ".").Replace(",", ".");
            if (decimal.TryParse(normalized, NumberStyles.Any, CultureInfo.InvariantCulture, out var parsed))
            {
                return parsed.ToString("0.##", CultureInfo.InvariantCulture);
            }

            return value.Trim();
        }

        private static Dictionary<string, string> ParseMeta(string? raw)
        {
            var result = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
            if (string.IsNullOrWhiteSpace(raw))
            {
                return result;
            }

            foreach (var part in raw.Split(';', StringSplitOptions.RemoveEmptyEntries))
            {
                var index = part.IndexOf('=');
                if (index <= 0)
                {
                    continue;
                }

                var key = part[..index].Trim();
                var value = part[(index + 1)..].Trim();
                if (!string.IsNullOrWhiteSpace(key) && !string.IsNullOrWhiteSpace(value))
                {
                    result[key] = value;
                }
            }

            return result;
        }

        private static string MetaValue(Dictionary<string, string> meta, string key)
        {
            if (meta.TryGetValue(key, out var value) && !string.IsNullOrWhiteSpace(value))
            {
                return value.Trim();
            }

            return string.Empty;
        }

        private static string FirstNonEmpty(params string?[] values)
        {
            foreach (var value in values)
            {
                if (!string.IsNullOrWhiteSpace(value))
                {
                    return value.Trim();
                }
            }

            return string.Empty;
        }

        private static string ToText(object? value)
        {
            if (value == null)
            {
                return "-";
            }

            return value switch
            {
                decimal d => d.ToString("0.##", CultureInfo.InvariantCulture),
                double d => d.ToString("0.##", CultureInfo.InvariantCulture),
                float f => f.ToString("0.##", CultureInfo.InvariantCulture),
                _ => Safe(value.ToString())
            };
        }

        private static string Safe(string? value)
        {
            return string.IsNullOrWhiteSpace(value) ? "-" : value.Trim();
        }

        private sealed class CustomLensReportData
        {
            public List<LnsOrdePrivateRow> HeaderRows { get; } = new();
            public List<LnsOrderDetailPrivateRow> DetailRows { get; } = new();
        }

        private sealed class LnsOrdePrivateRow
        {
            public long OrderId { get; set; }
            public string Company { get; set; } = "-";
            public string CompanyId { get; set; } = string.Empty;
            public string CreateDate { get; set; } = "-";
            public string OrderNo { get; set; } = "-";
            public string HBox { get; set; } = "-";
            public string VBox { get; set; } = "-";
            public string DBL { get; set; } = "-";
            public string ED { get; set; } = "-";
            public string PA { get; set; } = "-";
            public string FFA { get; set; } = "-";
            public string VD { get; set; } = "-";
            public string Base { get; set; } = "-";
            public string Frame { get; set; } = "-";
            public string Color { get; set; } = "-";
            public string Comment { get; set; } = "-";
            public string Mobile { get; set; } = "-";
            public string Tel { get; set; } = "-";
            public string Masrafkonande { get; set; } = "-";
            public string StoreName { get; set; } = "-";
            public string Corridor { get; set; } = "-";
            public string NameKala { get; set; } = "-";
            public string Services { get; set; } = "-";
            public string Coating { get; set; } = "-";
        }

        private sealed class LnsOrderDetailPrivateRow
        {
            public long OrderId { get; set; }
            public long OrderDetailId { get; set; }
            public string CodeKala { get; set; } = "-";
            public string NameKala { get; set; } = "-";
            public string Axis { get; set; } = "-";
            public string SPH { get; set; } = "-";
            public string CYL { get; set; } = "-";
            public string ADD { get; set; } = "-";
            public string PRISM { get; set; } = "-";
            public string BPRISM { get; set; } = "-";
            public string Fitting { get; set; } = "-";
            public string DIA { get; set; } = "-";
            public string PD { get; set; } = "-";
            public string DC { get; set; } = "-";
            public string NO { get; set; } = "-";
            public string Masrafkonande { get; set; } = "-";
            public string StoreName { get; set; } = "-";
            public string SideType { get; set; } = "-";
            public int? SideTypeId { get; set; }
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
