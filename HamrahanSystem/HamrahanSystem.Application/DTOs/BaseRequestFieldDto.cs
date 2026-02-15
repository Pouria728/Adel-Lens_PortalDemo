
using System.Collections.Generic;

namespace HamrahanSystem.Application.DTOs
{

    public partial class BaseRequestFieldDto
    {
        #region Constructors

        public BaseRequestFieldDto()
        {
        }

        public BaseRequestFieldDto(bool allowNull, int baseRequestFieldId, int baseRequestId, int? baseRequestParentFieldId, int dataTypeId, int fieldType, int maxRecord, string title, List<BaseRequestFieldDto> baseRequestFieldes_BaseRequestParentFieldId, BaseRequestFieldDto baseRequestFielde_BaseRequestParentFieldId, BaseRequesteDto baseRequeste, List<RequestFieldeDto> requestFieldes)
        {

            this.AllowNull = allowNull;
            this.BaseRequestFieldId = baseRequestFieldId;
            this.BaseRequestId = baseRequestId;
            this.BaseRequestParentFieldId = baseRequestParentFieldId;
            this.DataTypeId = dataTypeId;
            this.FieldType = fieldType;
            this.MaxRecord = maxRecord;
            this.Title = title;
            this.BaseRequestFieldes_BaseRequestParentFieldId = baseRequestFieldes_BaseRequestParentFieldId;
            this.BaseRequestFielde_BaseRequestParentFieldId = baseRequestFielde_BaseRequestParentFieldId;
            this.BaseRequeste = baseRequeste;
            this.RequestFieldes = requestFieldes;
        }

        #endregion

        #region Properties

        public bool AllowNull { get; set; }

        public int BaseRequestFieldId { get; set; }

        public int BaseRequestId { get; set; }

        public int? BaseRequestParentFieldId { get; set; }

        public int DataTypeId { get; set; }

        public int FieldType { get; set; }

        public int MaxRecord { get; set; }

        public string Title { get; set; }

        #endregion

        #region Navigation Properties

        public List<BaseRequestFieldDto> BaseRequestFieldes_BaseRequestParentFieldId { get; set; }

        public BaseRequestFieldDto BaseRequestFielde_BaseRequestParentFieldId { get; set; }

        public BaseRequesteDto BaseRequeste { get; set; }

        public List<RequestFieldeDto> RequestFieldes { get; set; }

        #endregion
    }

}
