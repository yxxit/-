using System;


//快捷菜单类 建立人：刘朋 2010/9/17
namespace XBase.Model.Common
{
    public class QukMenuModel
    {
        private string _QukM_ID;
        private string _CompanyCD;
        private string _UserID;
        private DateTime _MenuAddTime;

        public string QukM_ID
        {
            get { return _QukM_ID; }
            set { _QukM_ID = value; }
        }

        public string CompanyCD
        {
            get { return _CompanyCD; }
            set { _CompanyCD = value; }
        }

        public string UserID
        {
            set { _UserID=value;}
            get { return _UserID; }
        }
        public DateTime MenuAddTime
        {
            set { _MenuAddTime = value; }
            get { return _MenuAddTime; }
        }
    }
}
