namespace HamrahanSystem.Application.DTOs
{
    public partial class TblLnsCustomSphDto
    {
        public TblLnsCustomSphDto()
        {
        }

        public int CustomSphId { get; set; }

        public string? Name { get; set; }

        public string? Code { get; set; }

        public string? Description { get; set; }

        public int OrderId { get; set; }

        public bool IsActive { get; set; }
    }
}
