

using System.Collections.Generic;

namespace HamrahanSystem.Application.DTOs
{

    public partial class TblMainDefineUserDto
    {
        #region Constructors

        public TblMainDefineUserDto() {
        }

        public TblMainDefineUserDto(bool? active, string codePersona, byte company, int id, bool isDisableTriggers, string passWord, long? passWordCheck, string pictureNo, int? rNPersona, string sQLPassword, string sQLUserName, int userID, string userName, string webPassword, string webServer, string webUserName) {

          this.Active = active;
          this.CodePersona = codePersona;
          this.Company = company;
          this.Id = id;
          this.IsDisableTriggers = isDisableTriggers;
          this.PassWord = passWord;
          this.PassWordCheck = passWordCheck;
          this.PictureNo = pictureNo;
          this.RNPersona = rNPersona;
          this.SQLPassword = sQLPassword;
          this.SQLUserName = sQLUserName;
          this.UserID = userID;
          this.UserName = userName;
          this.WebPassword = webPassword;
          this.WebServer = webServer;
          this.WebUserName = webUserName;
        }

        #endregion

        #region Properties

        public bool? Active { get; set; }

        public string CodePersona { get; set; }

        public byte Company { get; set; }

        public int Id { get; set; }

        public bool IsDisableTriggers { get; set; }

        public string PassWord { get; set; }

        public long? PassWordCheck { get; set; }

        public string PictureNo { get; set; }

        public int? RNPersona { get; set; }

        public string SQLPassword { get; set; }

        public string SQLUserName { get; set; }

        public int UserID { get; set; }

        public string UserName { get; set; }

        public string WebPassword { get; set; }

        public string WebServer { get; set; }

        public string WebUserName { get; set; }

        #endregion
    }

}
