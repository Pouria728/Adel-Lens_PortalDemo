using System;

namespace HamrahanSystem.Domain.Entity
{
    public partial class TblLnsCustomSph
    {
        public TblLnsCustomSph()
        {
            this.OrderId = 1;
            this.IsActive = 1;
            OnCreated();
        }

        public int CustomSphId { get; set; }

        public string? Name { get; set; }

        public string? Code { get; set; }

        public string? Description { get; set; }

        public int OrderId { get; set; }

        public short? IsActive { get; set; }

        partial void OnCreated();
    }
}
