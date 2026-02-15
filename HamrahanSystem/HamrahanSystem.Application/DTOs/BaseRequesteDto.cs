
using System.Collections.Generic;

namespace HamrahanSystem.Application.DTOs
{

    public partial class BaseRequesteDto
    {
        #region Constructors

        public BaseRequesteDto()
        {
        }

        public BaseRequesteDto(int baseRequestId, DateTime? dateCreate, bool isActive, string title, int typeRequest, List<BaseRequestStepDto> baseRequestSteps, List<RequestDto> requests, List<BaseRequestFieldDto> baseRequestFieldes)
        {

            this.BaseRequestId = baseRequestId;
            this.DateCreate = dateCreate;
            this.IsActive = isActive;
            this.Title = title;
            this.TypeRequest = typeRequest;
            this.BaseRequestSteps = baseRequestSteps;
            this.Requests = requests;
            this.BaseRequestFieldes = baseRequestFieldes;
        }

        #endregion

        #region Properties

        public int BaseRequestId { get; set; }

        public DateTime? DateCreate { get; set; }

        public bool IsActive { get; set; }

        public string Title { get; set; }

        public int TypeRequest { get; set; }

        #endregion

        #region Navigation Properties

        public List<BaseRequestStepDto> BaseRequestSteps { get; set; }

        public List<RequestDto> Requests { get; set; }

        public List<BaseRequestFieldDto> BaseRequestFieldes { get; set; }

        #endregion
    }

}