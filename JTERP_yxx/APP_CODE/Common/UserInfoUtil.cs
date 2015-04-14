/**********************************************
 * 作    者： 吴志强
 * 创建日期： 2008.12.29
 * 描    述： 登陆用户信息类
 * 修改日期： 2009.01.10
 * 版    本： 0.5.0
 ***********************************************/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Collections;

namespace XBase.Common
{
    /// <summary>
    /// 类名：UserInfoUtil
    /// 描述：提供与用户相关的一些属性
    /// 
    /// 作者：吴志强
    /// 创建时间：2008/12/29
    /// 最后修改时间：2009/04/14
    /// 修改人：游德春 
    /// 修改内容：增加 Serializable 属性
    /// </summary>
    ///
    [Serializable]
    public class UserInfoUtil
    {
        //用户ID
        private string _UserID = string.Empty;
        //用户名
        private string _UserName = string.Empty;
        //用户公司代码
        private string _CompanyCD = string.Empty;
        //用户公司名称
        private string _CompanyName = string.Empty;
        //用户部门ID
        private int _DepartmentID;
        private string _DepartmentName;
        //角色列表
        private int[] _Role;
        //角色权限范围
        private string _RoleRange;
        //用户人员编号
        private int _employeeID;
        //用户员工名称
        private string _employeename;
        //用户工号
        private string _employeeNum;
        //用户菜单信息
        private DataTable _MenuInfo = null;
        //用户页面操作权限信息
        private DataTable _AuthorityInfo = null;
        //分店ID 若为总店则为0
        private int _branchid;
        // 是否查询总店库存
        private bool _iscxstore;
        //是否为分店
        private bool _issubstore;
        //下属分店或分公司的ID列表
        private ArrayList _substoreList;
        //分店名称
        private string _substorename;
        //用户是否超级管理员
        private string _IsRoot;
        //是否启用条码
        private bool _isbarcode;
        //出入库是否显示价格
        private bool _isdisplayprcie;
        //是否多计量单位
        private bool _ismoreunit;
        //是否超订单发货
        private bool _isoverorder;
        //允许出入库价格为零
        private bool _iszero;
        //是否启用批次规则设置
        private bool _isbatch;
        //是否启用自动生成凭证
        private bool _isvoucher;
        //是否启用自动审核登帐
        private bool _isapply;
        //小数位数
        private string _selpoint;
        //版本
        private string _version;
        //是否启用定制打印
        private string _isprint;
        //定制打印编码
        private string _printno;
        //是否启用零库存出库
        private bool _StorageZero;
        //是否超生产订单入库
        private string _printwidth;
        //定制打印模板宽度
        private bool _StorageOver;
        //是否超任务单领料
        private bool _isovertake;
        //是否客户账号登陆
        private string _isCust;


        //2014-03-29 bao add 加入单据业务类型、火车调运表状态、审批状态
        //单据业务类型
        private string _BillType_CGHTD;//采购合同

        public string BillType_CGHTD
        {
            get { return _BillType_CGHTD; }
            set { _BillType_CGHTD = value; }
        }
        private string _BillType_DYD;//调运单
        /// <summary>
        /// 调运单
        /// </summary>
        public string BillType_DYD
        {
            get { return _BillType_DYD; }
            set { _BillType_DYD = value; }
        }
        private string _BillType_CGDHD;//采购到货单
        /// <summary>
        /// 采购到货单
        /// </summary>
        public string BillType_CGDHD
        {
            get { return _BillType_CGDHD; }
            set { _BillType_CGDHD = value; }
        }
        private string _BillType_CGZJD;//质检单
        /// <summary>
        /// 质检单
        /// </summary>
        public string BillType_CGZJD
        {
            get { return _BillType_CGZJD; }
            set { _BillType_CGZJD = value; }
        }
        private string _BillType_CGRKD;//采购入库单
        /// <summary>
        /// 采购入库单
        /// </summary>
        public string BillType_CGRKD
        {
            get { return _BillType_CGRKD; }
            set { _BillType_CGRKD = value; }
        }
        private string _BillType_XSHTD;//销售合同
        /// <summary>
        /// 销售合同
        /// </summary>
        public string BillType_XSHTD
        {
            get { return _BillType_XSHTD; }
            set { _BillType_XSHTD = value; }
        }
        private string _BillType_CGZXD;//采购直销单
        /// <summary>
        /// 采购直销单
        /// </summary>
        public string BillType_CGZXD
        {
            get { return _BillType_CGZXD; }
            set { _BillType_CGZXD = value; }
        }
        private string _BillType_XSFHD;//销售发货单
        /// <summary>
        /// 销售发货单
        /// </summary>
        public string BillType_XSFHD
        {
            get { return _BillType_XSFHD; }
            set { _BillType_XSFHD = value; }
        }
        private string _BillType_XSCKD;//销售出库单
        /// <summary>
        /// 销售出库单
        /// </summary>
        public string BillType_XSCKD
        {
            get { return _BillType_XSCKD; }
            set { _BillType_XSCKD = value; }
        }
        //火车调运表状态
        private string _DiaoyunType_NoEffec;//未生效
        /// <summary>
        /// 未生效
        /// </summary>
        public string DiaoyunType_NoEffec
        {
            get { return _DiaoyunType_NoEffec; }
            set { _DiaoyunType_NoEffec = value; }
        }
        private string _DiaoyunType_InCar;//装车
        /// <summary>
        /// 装车
        /// </summary>
        public string DiaoyunType_InCar
        {
            get { return _DiaoyunType_InCar; }
            set { _DiaoyunType_InCar = value; }
        }
        private string _DiaoyunType_Send;//发货 
        /// <summary>
        /// 发货
        /// </summary>
        public string DiaoyunType_Send
        {
            get { return _DiaoyunType_Send; }
            set { _DiaoyunType_Send = value; }
        }
        private string _DiaoyunType_InLoad;//在途
        /// <summary>
        /// 在途
        /// </summary>
        public string DiaoyunType_InLoad
        {
            get { return _DiaoyunType_InLoad; }
            set { _DiaoyunType_InLoad = value; }
        }
        private string _DiaoyunType_Arrive;//到货
        /// <summary>
        /// 到货
        /// </summary>
        public string DiaoyunType_Arrive
        {
            get { return _DiaoyunType_Arrive; }
            set { _DiaoyunType_Arrive = value; }
        }
        //审批状态值
        private string _ShenpiType_Wait;//待审核
        /// <summary>
        /// 待审核
        /// </summary>
        public string ShenpiType_Wait
        {
            get { return _ShenpiType_Wait; }
            set { _ShenpiType_Wait = value; }
        }
        private string _ShenpiType_ApproveComp;//审核通过
        /// <summary>
        /// 审核通过
        /// </summary>
        public string ShenpiType_ApproveComp
        {
            get { return _ShenpiType_ApproveComp; }
            set { _ShenpiType_ApproveComp = value; }
        }
        private string _ShenpiType_Back;//审核退回
        /// <summary>
        /// 审核退回
        /// </summary>
        public string ShenpiType_Back
        {
            get { return _ShenpiType_Back; }
            set { _ShenpiType_Back = value; }
        }

        //审批状态
        /// <summary>
        /// 是否启用零库存出库
        /// </summary>
        public bool IsStorageZero
        {
            get { return _StorageZero; }
            set { _StorageZero = value; }
        }
        /// <summary>
        /// 是否超生产订单入库
        /// </summary>
        public bool IsStorageOver
        {
            get { return _StorageOver; }
            set { _StorageOver = value; }
        }
        public string IsPrint
        {
            get { return _isprint; }
            set { _isprint = value; }
        }

        public string PrintNo
        {
            get { return _printno; }
            set { _printno = value; }
        }
        public string PrintWidth
        {
            get { return _printwidth; }
            set { _printwidth = value; }
        }
        public string Version
        {
            get { return _version; }
            set { _version = value; }
        }

        /// <summary>
        /// 分店名称
        /// </summary>
        public string SubStoreName
        {
            get { return _substorename; }
            set { _substorename = value; }
        }

        /// <summary>
        /// 下属分店或分公司的ID列表
        /// </summary>
        public ArrayList SubStoreList
        {
            get { return _substoreList; }
            set { _substoreList = value; }
        }
        /// <summary>
        /// 是否分店 是 true 否 false 
        /// </summary>
        public bool IsSubStore
        {
            get { return _issubstore; }
            set { _issubstore = value; }
        }

        /// <summary>
        /// 分店ID 
        /// </summary>
        public int BranchID
        {
            get { return _branchid; }
            set { _branchid = value; }
        }

        /// <summary>
        /// 是否启用批次  true:启用 false:不启用
        /// </summary>
        public bool IsBarCode
        {
            get { return _isbarcode; }
            set { _isbarcode = value; }
        }

        /// <summary>
        /// 出入库是否显示价格与金额   true:显示 false:不显示
        /// </summary>
        public bool IsDisplayPrice
        {
            get { return _isdisplayprcie; }
            set
            {
                _isdisplayprcie = value;
            }
        }
        /// <summary>
        /// 是否启用多计量单位   true:启用 false:不启用
        /// </summary>
        public bool IsMoreUnit
        {
            get
            {
                return _ismoreunit;
            }
            set
            {
                _ismoreunit = value;
            }
        }
        /// <summary>
        /// 是否启用超订单发货   true:启用 false:不启用
        /// </summary>
        public bool IsOverOrder
        {
            get
            {
                return _isoverorder;
            }
            set
            {
                _isoverorder = value;
            }
        }
        /// <summary>
        /// 是否启用超任务单领料   true:启用 false:不启用
        /// </summary>
        public bool IsOverTake
        {
            get
            {
                return _isovertake;
            }
            set
            {
                _isovertake = value;
            }
        }
        /// <summary>
        /// 允许出入库价格为零   true:启用  false:停用
        /// </summary>
        public bool IsZero
        {
            get
            {
                return _iszero;
            }
            set
            {
                _iszero = value;
            }
        }

        /// <summary>
        /// 是否启用批次规则设置   true:启用 false:不启用
        /// </summary>
        public bool IsBatch
        {
            get
            {
                return _isbatch;
            }
            set
            {
                _isbatch = value;
            }
        }
        /// <summary>
        /// 是否启用自动生成凭证   true:启用 false:不启用
        /// </summary>
        public bool IsVoucher
        {
            get
            {
                return _isvoucher;
            }
            set
            {
                _isvoucher = value;
            }
        }
        /// <summary>
        /// 是否启用自动审核登帐   true:启用 false:不启用
        /// </summary>
        public bool IsApply
        {
            get
            {
                return _isapply;
            }
            set
            {
                _isapply = value;
            }
        }
        /// <summary>
        /// 小数位数 默认为2
        /// </summary>
        public string SelPoint
        {
            get
            {
                return _selpoint;
            }
            set
            {
                _selpoint = value;
            }
        }

        public string EmployeeNum
        {
            get
            {
                return _employeeNum;
            }
            set
            {
                _employeeNum = value;
            }
        }

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

        public int EmployeeID
        {
            get
            {
                return _employeeID;
            }
            set
            {
                _employeeID = value;
            }
        }

        public int[] Role
        {
            get
            {
                return _Role;
            }
            set
            {
                _Role = value;
            }
        }

        public string RoleRange
        {
            get { return _RoleRange; }
            set { _RoleRange = value; }
        }


        public string UserID
        {
            get
            {
                return _UserID;
            }
            set
            {
                _UserID = value;
            }
        }

        public string UserName
        {
            get
            {
                return _UserName;
            }
            set
            {
                _UserName = value;
            }
        }

        public string CompanyCD
        {
            get
            {
                return _CompanyCD;
            }
            set
            {
                _CompanyCD = value;
            }
        }

        public string CompanyName
        {
            get
            {
                return _CompanyName;
            }
            set
            {
                _CompanyName = value;
            }
        }

        public int DeptID
        {
            get
            {
                return _DepartmentID;
            }
            set
            {
                _DepartmentID = value;
            }
        }

        public string DeptName
        {
            get
            {
                return _DepartmentName;
            }
            set
            {
                _DepartmentName = value;
            }
        }

        public DataTable MenuInfo
        {
            get
            {
                return _MenuInfo;
            }
            set
            {
                _MenuInfo = value;
            }
        }

        public DataTable AuthorityInfo
        {
            get
            {
                return _AuthorityInfo;
            }
            set
            {
                _AuthorityInfo = value;
            }
        }

        public string IsRoot
        {
            get
            {
                return _IsRoot;
            }
            set
            {
                _IsRoot = value;
            }
        }
        //是否客户登陆
        public string IsCust
        {
            get { return _isCust; }
            set { _isCust = value; }
        }

    }
}
