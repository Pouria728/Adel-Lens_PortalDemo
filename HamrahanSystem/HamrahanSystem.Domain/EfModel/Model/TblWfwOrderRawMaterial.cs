using System;

namespace HamrahanSystem.Domain.Entity
{
    public partial class TblWfwOrderRawMaterial
    {
        public TblWfwOrderRawMaterial()
        {
            this.Quantity = 1;
            this.DateCreate = DateTime.Now;
            this.DateUpdate = DateTime.Now;
            OnCreated();
        }

        public long OrderRawMaterialId { get; set; }

        public long OrderId { get; set; }

        public long OrderProcessId { get; set; }

        public int ProcessStepId { get; set; }

        public int DefineObjectId { get; set; }

        public int? DefineObjectRecNo { get; set; }

        public string? Barcode { get; set; }

        public string? MaterialName { get; set; }

        public int Quantity { get; set; }

        public DateTime DateCreate { get; set; }

        public DateTime DateUpdate { get; set; }

        public int? CreatedBy { get; set; }

        public int? ModifiedBy { get; set; }

        partial void OnCreated();
    }
}
