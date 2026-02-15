namespace HamrahanSystem.Application.DTOs
{
    public partial class TblLnsCustomCylDto
    {
        public TblLnsCustomCylDto()
        {
        }

        public int CustomCylId { get; set; }

        public string? Name { get; set; }

        public string? Code { get; set; }

        public string? Description { get; set; }

        public int OrderId { get; set; }

        public bool IsActive { get; set; }
    }
}
