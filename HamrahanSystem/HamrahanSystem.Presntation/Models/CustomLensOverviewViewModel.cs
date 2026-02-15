using HamrahanSystem.Application.DTOs;
using System.Collections.Generic;

namespace HamrahanSystem.Presntation.Models
{
    public class CustomLensOverviewViewModel
    {
        public List<TblLnsBrandDto> Brands { get; set; } = new();
        public List<TblLnsLensTypeDto> LensTypes { get; set; } = new();
        public List<TblLnsDesignTypeDto> DesignTypes { get; set; } = new();
        public List<TblLnsCustomLensTypeCoatingDto> TypeCoatings { get; set; } = new();
        public List<TblLnsCustomLensIndexDto> LensIndexes { get; set; } = new();
        public List<TblLnsCustomLensTypeMaterialDto> Materials { get; set; } = new();
        public List<TblLnsCustomDesignTypeAdditionDto> Additions { get; set; } = new();
    }
}
