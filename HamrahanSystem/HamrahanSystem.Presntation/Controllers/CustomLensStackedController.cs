using HamrahanSystem.Application.DTOs;
using HamrahanSystem.Application.UseCaseInterface;
using HamrahanSystem.Presntation.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HamrahanSystem.Presntation.Controllers
{
    [Authorize]
    public class CustomLensStackedController(
        ITblLnsBrandService tblLnsBrandService,
        ITblLnsLensTypeService tblLnsLensTypeService,
        ITblLnsDesignTypeService tblLnsDesignTypeService,
        ITblLnsCustomLensTypeCoatingService tblLnsCustomLensTypeCoatingService,
        ITblLnsCustomLensIndexService tblLnsCustomLensIndexService,
        ITblLnsCustomLensTypeMaterialService tblLnsCustomLensTypeMaterialService,
        ITblLnsCustomDesignTypeAdditionService tblLnsCustomDesignTypeAdditionService
    ) : Controller
    {
        [Authorize(Roles = "admin,CustomLensBrand_Index,CustomLensType_Index,CustomLensDesignType_Index,CustomLensTypeCoating_Index,CustomLensTypeMaterial_Index,CustomLensIndex_Index,CustomLensAddition_Index")]
        public IActionResult Index()
        {
            var brands = (tblLnsBrandService.GetAll().Result ?? Enumerable.Empty<TblLnsBrandDto>())
                .Where(x => x.IsSpecial && x.IsActive)
                .OrderBy(x => x.OrderId)
                .ToList();

            var brandById = brands.ToDictionary(x => x.BrandId, x => x);
            var brandIds = brandById.Keys.ToHashSet();

            var lensTypes = (tblLnsLensTypeService.GetAll().Result ?? Enumerable.Empty<TblLnsLensTypeDto>())
                .Where(x => x.IsSpecial && x.IsActive && x.BrandId.HasValue && brandIds.Contains(x.BrandId.Value))
                .OrderBy(x => x.OrderId)
                .ToList();
            var lensTypeById = lensTypes.ToDictionary(x => x.LensTypeId, x => x);
            var lensTypeIds = lensTypeById.Keys.ToHashSet();

            var designTypes = (tblLnsDesignTypeService.GetAll().Result ?? Enumerable.Empty<TblLnsDesignTypeDto>())
                .Where(x => x.IsSpecial && x.IsActive && x.LensTypeId.HasValue && lensTypeIds.Contains(x.LensTypeId.Value))
                .OrderBy(x => x.OrderId)
                .ToList();
            var designById = designTypes.ToDictionary(x => x.DesignTypeId, x => x);
            var designIds = designById.Keys.ToHashSet();

            var lensIndexes = (tblLnsCustomLensIndexService.GetAll().Result ?? Enumerable.Empty<TblLnsCustomLensIndexDto>())
                .Where(x => x.IsActive && x.DesignTypeId.HasValue && designIds.Contains(x.DesignTypeId.Value))
                .OrderBy(x => x.OrderId)
                .ToList();

            var materials = (tblLnsCustomLensTypeMaterialService.GetAll().Result ?? Enumerable.Empty<TblLnsCustomLensTypeMaterialDto>())
                .Where(x => x.IsActive && x.LensIndexId.HasValue)
                .OrderBy(x => x.OrderId)
                .ToList();

            var coatingsByDesignId = (tblLnsCustomLensTypeCoatingService.GetAll().Result ?? Enumerable.Empty<TblLnsCustomLensTypeCoatingDto>())
                .Where(x => x.IsActive && x.DesignTypeId.HasValue && designIds.Contains(x.DesignTypeId.Value))
                .GroupBy(x => x.DesignTypeId!.Value)
                .ToDictionary(
                    g => g.Key,
                    g => string.Join(" | ",
                        g.OrderBy(x => x.OrderId)
                         .Select(x => x.CoatingName?.Trim())
                         .Where(x => !string.IsNullOrWhiteSpace(x))
                         .Distinct())
                );

            var additionsByDesignId = (tblLnsCustomDesignTypeAdditionService.GetAll().Result ?? Enumerable.Empty<TblLnsCustomDesignTypeAdditionDto>())
                .Where(x => x.IsActive && x.DesignTypeId.HasValue && designIds.Contains(x.DesignTypeId.Value))
                .GroupBy(x => x.DesignTypeId!.Value)
                .ToDictionary(
                    g => g.Key,
                    g => string.Join(" | ",
                        g.OrderBy(x => x.OrderId)
                         .Select(x => x.AdditionValue.HasValue ? x.AdditionValue.Value.ToString("0.00") : string.Empty)
                         .Where(x => !string.IsNullOrWhiteSpace(x))
                         .Distinct())
                );

            var materialsByLensIndex = materials
                .GroupBy(x => x.LensIndexId!.Value)
                .ToDictionary(g => g.Key, g => g.OrderBy(x => x.OrderId).ToList());

            var rows = new List<CustomLensStackedRowViewModel>();

            foreach (var lensIndex in lensIndexes)
            {
                if (!lensIndex.DesignTypeId.HasValue || !designById.TryGetValue(lensIndex.DesignTypeId.Value, out var designType))
                {
                    continue;
                }

                if (!designType.LensTypeId.HasValue || !lensTypeById.TryGetValue(designType.LensTypeId.Value, out var lensType))
                {
                    continue;
                }

                if (!lensType.BrandId.HasValue || !brandById.TryGetValue(lensType.BrandId.Value, out var brand))
                {
                    continue;
                }

                var coatingText = coatingsByDesignId.GetValueOrDefault(designType.DesignTypeId, "-");
                var additionText = additionsByDesignId.GetValueOrDefault(designType.DesignTypeId, "-");

                if (materialsByLensIndex.TryGetValue(lensIndex.CustomLensIndexId, out var indexMaterials) && indexMaterials.Count > 0)
                {
                    foreach (var material in indexMaterials)
                    {
                        rows.Add(new CustomLensStackedRowViewModel
                        {
                            BrandId = brand.BrandId,
                            BrandName = brand.Name,
                            LensTypeId = lensType.LensTypeId,
                            LensTypeName = lensType.Name,
                            DesignTypeId = designType.DesignTypeId,
                            DesignTypeName = designType.Name,
                            LensIndexId = lensIndex.CustomLensIndexId,
                            LensIndexName = lensIndex.LensIndexName,
                            MaterialId = material.CustomLensTypeMaterialId,
                            MaterialName = material.MaterialName,
                            Coatings = string.IsNullOrWhiteSpace(coatingText) ? "-" : coatingText,
                            Additions = string.IsNullOrWhiteSpace(additionText) ? "-" : additionText,
                            IsCorridor = lensType.IsCorridor,
                            HasColoringType = lensIndex.HasColoringType,
                            DefineObjectId = material.DefineObjectId,
                            BrandOrder = brand.OrderId,
                            LensTypeOrder = lensType.OrderId,
                            DesignTypeOrder = designType.OrderId,
                            LensIndexOrder = lensIndex.OrderId,
                            MaterialOrder = material.OrderId
                        });
                    }
                }
                else
                {
                    rows.Add(new CustomLensStackedRowViewModel
                    {
                        BrandId = brand.BrandId,
                        BrandName = brand.Name,
                        LensTypeId = lensType.LensTypeId,
                        LensTypeName = lensType.Name,
                        DesignTypeId = designType.DesignTypeId,
                        DesignTypeName = designType.Name,
                        LensIndexId = lensIndex.CustomLensIndexId,
                        LensIndexName = lensIndex.LensIndexName,
                        MaterialId = 0,
                        MaterialName = "-",
                        Coatings = string.IsNullOrWhiteSpace(coatingText) ? "-" : coatingText,
                        Additions = string.IsNullOrWhiteSpace(additionText) ? "-" : additionText,
                        IsCorridor = lensType.IsCorridor,
                        HasColoringType = lensIndex.HasColoringType,
                        DefineObjectId = null,
                        BrandOrder = brand.OrderId,
                        LensTypeOrder = lensType.OrderId,
                        DesignTypeOrder = designType.OrderId,
                        LensIndexOrder = lensIndex.OrderId,
                        MaterialOrder = int.MaxValue
                    });
                }
            }

            var model = new CustomLensStackedViewModel
            {
                Rows = rows
                    .OrderBy(x => x.BrandOrder)
                    .ThenBy(x => x.LensTypeOrder)
                    .ThenBy(x => x.DesignTypeOrder)
                    .ThenBy(x => x.LensIndexOrder)
                    .ThenBy(x => x.MaterialOrder)
                    .ToList()
            };

            return View(model);
        }
    }
}
