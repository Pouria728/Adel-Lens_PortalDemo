

using System.Collections.Generic;

namespace HamrahanSystem.Application.DTOs
{

    public partial class TblFeeBehoofMasterDto
    {
        #region Constructors

        public TblFeeBehoofMasterDto() {
        }

        public TblFeeBehoofMasterDto(byte? agentTypeIndex, int behoofCode, byte? behoofTypeIndex, byte company, string englishBehoof, string farsiBehoof, short? iNTypeIndex, string latinBehoof, int? linkedCodingTable, byte? listType, string param, List<TblFeeBehoofDetailDto> tblFeeBehoofDetails) {

          this.AgentTypeIndex = agentTypeIndex;
          this.BehoofCode = behoofCode;
          this.BehoofTypeIndex = behoofTypeIndex;
          this.Company = company;
          this.EnglishBehoof = englishBehoof;
          this.FarsiBehoof = farsiBehoof;
          this.INTypeIndex = iNTypeIndex;
          this.LatinBehoof = latinBehoof;
          this.LinkedCodingTable = linkedCodingTable;
          this.ListType = listType;
          this.Param = param;
          this.TblFeeBehoofDetails = tblFeeBehoofDetails;
        }

        #endregion

        #region Properties

        public byte? AgentTypeIndex { get; set; }

        public int BehoofCode { get; set; }

        public byte? BehoofTypeIndex { get; set; }

        public byte Company { get; set; }

        public string EnglishBehoof { get; set; }

        public string FarsiBehoof { get; set; }

        public short? INTypeIndex { get; set; }

        public string LatinBehoof { get; set; }

        public int? LinkedCodingTable { get; set; }

        public byte? ListType { get; set; }

        public string Param { get; set; }

        #endregion

        #region Navigation Properties

        public List<TblFeeBehoofDetailDto> TblFeeBehoofDetails { get; set; }

        #endregion
    }

}
