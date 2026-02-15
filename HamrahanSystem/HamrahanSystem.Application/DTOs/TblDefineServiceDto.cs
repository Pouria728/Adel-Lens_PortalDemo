

using System.Collections.Generic;

namespace HamrahanSystem.Application.DTOs
{

    public partial class TblDefineServiceDto
    {
        #region Constructors

        public TblDefineServiceDto() {
        }

        public TblDefineServiceDto(bool active, byte company, int defineServiceId, short? isActive, int kindService, int recNo, int? rNScruple, string serviceCode, string serviceName, string serviceNameEN) {

          this.Active = active;
          this.Company = company;
          this.DefineServiceId = defineServiceId;
          this.IsActive = isActive;
          this.KindService = kindService;
          this.RecNo = recNo;
          this.RNScruple = rNScruple;
          this.ServiceCode = serviceCode;
          this.ServiceName = serviceName;
          this.ServiceNameEN = serviceNameEN;
        }

        #endregion

        #region Properties

        public bool Active { get; set; }

        public byte Company { get; set; }

        public int DefineServiceId { get; set; }

        public short? IsActive { get; set; }

        public int KindService { get; set; }

        public int RecNo { get; set; }

        public int? RNScruple { get; set; }

        public string ServiceCode { get; set; }

        public string ServiceName { get; set; }

        public string ServiceNameEN { get; set; }

        #endregion
    }

}
