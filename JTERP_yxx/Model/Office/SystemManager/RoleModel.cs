/**********************************************
 * 类作用：   RoleInfo表数据模板
 * 作者：     钱锋锋
 * 创建时间： 2010/08/16 
 ***********************************************/
using System;

namespace XBase.Model.Office.SystemManager
{
    /// <summary>
    /// 类名：RoleModel
    /// 描述：RoleInfo表数据模板
    /// 
    /// 作者：钱锋锋
    /// 创建时间：2010/08/16    
    /// </summary>
    ///
    public class RoleModel
    {
        #region Model
        private string _roleid;
        private string _companycd;      
        private string _superroleid;
        private string _rolename;
        private string _remark;
        private string _modifieddate;
        private string _modifieduserid;
        private string _superroleid1;
        private string _editflag;
        /// <summary>
        /// 
        /// </summary>
        public string EditFlag
        {
            set { _editflag = value; }
            get { return _editflag; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string RoleID
        {
            set { _roleid = value; }
            get { return _roleid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string CompanyCD
        {
            set { _companycd = value; }
            get { return _companycd; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string SuperRoleID
        {
            set { _superroleid = value; }
            get { return _superroleid; }
        }       
        /// <summary>
        /// 
        /// </summary>
        public string RoleName
        {
            set { _rolename = value; }
            get { return _rolename; }
        }
        
        /// <summary>
        /// 
        /// </summary>
        public string remark
        {
            set { _remark = value; }
            get { return _remark; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string ModifiedDate
        {
            set { _modifieddate = value; }
            get { return _modifieddate; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string ModifiedUserID
        {
            set { _modifieduserid = value; }
            get { return _modifieduserid; }
        }
        public string SuperRoleID1
        {
            set { _superroleid1 = value; }
            get { return _superroleid1; }
        }
        #endregion Model
    }
}
