namespace HamrahanSystem.Presntation.Models
{
    public class CustomLensStackedViewModel
    {
        public List<CustomLensStackedRowViewModel> Rows { get; set; } = new();
    }

    public class CustomLensStackedRowViewModel
    {
        public int BrandId { get; set; }
        public string BrandName { get; set; } = string.Empty;

        public int LensTypeId { get; set; }
        public string LensTypeName { get; set; } = string.Empty;

        public int DesignTypeId { get; set; }
        public string DesignTypeName { get; set; } = string.Empty;

        public int LensIndexId { get; set; }
        public string LensIndexName { get; set; } = string.Empty;

        public int MaterialId { get; set; }
        public string MaterialName { get; set; } = string.Empty;

        public string Coatings { get; set; } = "-";
        public string Additions { get; set; } = "-";

        public bool IsCorridor { get; set; }
        public bool HasColoringType { get; set; }
        public int? DefineObjectId { get; set; }

        public int BrandOrder { get; set; }
        public int LensTypeOrder { get; set; }
        public int DesignTypeOrder { get; set; }
        public int LensIndexOrder { get; set; }
        public int MaterialOrder { get; set; }
    }
}
