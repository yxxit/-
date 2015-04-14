/**********************************************
 * 类作用：   初始化数据模板
 * 建立人：   宋凯歌
 * 建立时间： 2010/11/12
 ***********************************************/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace XBase.Model.Office.SystemManager
{
    public class InitSystemDataModel
    {
        private string _companycd;
        private string _deptinfo;
        private string _userid;
        private string _deptname;
        private string _hiddeptname;
        private string _employeeinfo;
        private string _employeename;
        private string _hidemployeename;
        private string _storagename;
        private string _custname;
        private string _hidcustname;
        private string _linkmanname;
        private string _worktel;
        private string _typename;
        private string _proname;
        private string _user;
        private string _password;



        /// <summary>
        /// 公司代码
        /// </summary>
        public string CompanyCD
        {
            get
            {
                return _companycd;
            }
            set
            {
                _companycd = value;
            }
        }

        /// <summary>
        /// 组织机构
        /// </summary>
        public string DeptInfo
        {
            get
            {
                return _deptinfo;
            }
            set
            {
                _deptinfo = value;
            }
        }

        /// <summary>
        /// 用户
        /// </summary>
        public string UserID
        {
            get
            {
                return _userid;
            }
            set
            {
                _userid = value;
            }
        }
        /// <summary>
        /// 组织机构名称
        /// </summary>
        public string DeptName
        {
            get
            {
                return _deptname;
            }
            set
            {
                _deptname = value;
            }
        }
        /// <summary>
        /// 组织机构拼音缩写
        /// </summary>
        public string HidDeptName
        {
            get
            {
                return _hiddeptname;
            }
            set
            {
                _hiddeptname = value;
            }
        }
        /// <summary>
        /// 人员档案
        /// </summary>
        public string EmployeeInfo
        {
            get
            {
                return _employeeinfo;
            }
            set
            {
                _employeeinfo = value;
            }
        }
        /// <summary>
        /// 人员档案名称
        /// </summary>
        public string EmployeeName
        {
            get
            {
                return _employeename;
            }
            set
            {
                _employeename = value;
            }
        }
        /// <summary>
        /// 人员档案名称拼音缩写
        /// </summary>
        public string HidEmployeeName
        {
            get
            {
                return _hidemployeename;
            }
            set
            {
                _hidemployeename = value;
            }
        }
        /// <summary>
        /// 仓库名称
        /// </summary>
        public string StorageName
        {
            get
            {
                return _storagename;
            }
            set
            {
                _storagename = value;
            }
        }
        /// <summary>
        /// 客户名称
        /// </summary>
        public string CustName
        {
            get
            {
                return _custname;
            }
            set
            {
                _custname = value;
            }
        }
        /// <summary>
        /// 客户名称拼音缩写
        /// </summary>
        public string HidCustName
        {
            get
            {
                return _hidcustname;
            }
            set
            {
                _hidcustname = value;
            }
        }
        /// <summary>
        /// 联系人
        /// </summary>
        public string LinkManName
        {
            get
            {
                return _linkmanname;
            }
            set
            {
                _linkmanname = value;
            }
        }
        /// <summary>
        /// 联系人电话
        /// </summary>
        public string WorkTel
        {
            get
            {
                return _worktel;
            }
            set
            {
                _worktel = value;
            }
        }
        /// <summary>
        /// 供应商类别
        /// </summary>
        public string TypeName
        {
            get
            {
                return _typename;
            }
            set
            {
                _typename = value;
            }
        }
        /// <summary>
        /// 供应商名称
        /// </summary>
        public string ProName
        {
            get
            {
                return _proname;
            }
            set
            {
                _proname = value;
            }
        }
        /// <summary>
        /// 用户名
        /// </summary>
        public string User
        {
            get
            {
                return _user;
            }
            set
            {
                _user = value;
            }
        }
        /// <summary>
        /// 密码
        /// </summary>
        public string PassWord
        {
            get
            {
                return _password;
            }
            set
            {
                _password = value;
            }
        }


    }
}
