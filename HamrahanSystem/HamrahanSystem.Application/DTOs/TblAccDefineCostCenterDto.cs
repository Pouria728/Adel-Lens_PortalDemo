

using System.Collections.Generic;

namespace HamrahanSystem.Application.DTOs
{

    public partial class TblAccDefineCostCenterDto
    {
        #region Constructors

        public TblAccDefineCostCenterDto() {
        }

        public TblAccDefineCostCenterDto(int accDefineCostCenterId, bool? active, string codeCostCenter, byte company, byte? indexTypeCostCenter, byte? indexTypeTask, short? isActive, string nameCostCenter, string nameCostCenterEN, int recNo, int? refMaster, byte? refMasterCompany, int? refMasterKey, int? registryKey, int? rNGroupFormal, byte? stateFormal, List<TblAccDefineCostCenterDto> tblAccDefineCostCenters_RefMasterKey_RefMasterCompany, TblAccDefineCostCenterDto tblAccDefineCostCenter_RefMasterKey_RefMasterCompany) {

          this.AccDefineCostCenterId = accDefineCostCenterId;
          this.Active = active;
          this.CodeCostCenter = codeCostCenter;
          this.Company = company;
          this.IndexTypeCostCenter = indexTypeCostCenter;
          this.IndexTypeTask = indexTypeTask;
          this.IsActive = isActive;
          this.NameCostCenter = nameCostCenter;
          this.NameCostCenterEN = nameCostCenterEN;
          this.RecNo = recNo;
          this.RefMaster = refMaster;
          this.RefMasterCompany = refMasterCompany;
          this.RefMasterKey = refMasterKey;
          this.RegistryKey = registryKey;
          this.RNGroupFormal = rNGroupFormal;
          this.StateFormal = stateFormal;
          this.TblAccDefineCostCenters_RefMasterKey_RefMasterCompany = tblAccDefineCostCenters_RefMasterKey_RefMasterCompany;
          this.TblAccDefineCostCenter_RefMasterKey_RefMasterCompany = tblAccDefineCostCenter_RefMasterKey_RefMasterCompany;
        }

        #endregion

        #region Properties

        public int AccDefineCostCenterId { get; set; }

        public bool? Active { get; set; }

        public string CodeCostCenter { get; set; }

        public byte Company { get; set; }

        public byte? IndexTypeCostCenter { get; set; }

        public byte? IndexTypeTask { get; set; }

        public short? IsActive { get; set; }

        public string NameCostCenter { get; set; }

        public string NameCostCenterEN { get; set; }

        public int RecNo { get; set; }

        public int? RefMaster { get; set; }

        public byte? RefMasterCompany { get; set; }

        public int? RefMasterKey { get; set; }

        public int? RegistryKey { get; set; }

        public int? RNGroupFormal { get; set; }

        public byte? StateFormal { get; set; }

        #endregion

        #region Navigation Properties

        public List<TblAccDefineCostCenterDto> TblAccDefineCostCenters_RefMasterKey_RefMasterCompany { get; set; }

        public TblAccDefineCostCenterDto TblAccDefineCostCenter_RefMasterKey_RefMasterCompany { get; set; }

        #endregion
    }

}
