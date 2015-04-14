/**********************************************
 * 类名：UserRoleSelectDBHelper
 * 描述：处理角色选择页面的业务处理
 * 作者：钱锋锋
 * 创建时间：2010/08/19
 ***********************************************/
using System.Data;
using System;
using XBase.Data.Common;
using XBase.Common;
namespace XBase.Business.Common
{
    public class UserRoleSelectBus
    {
        #region 常量定义
        const string SHOWTYEP_CODE_SELECT_Role = "1";//部门
        const string SHOWTYPE_CODE_USERS = "2";//人员

        const string OPRT_CODE_SELECT = "1";//单选
        const string OPRT_CODE_SELECTS = "2";//多选
        #endregion

     

    

        #region 获取角色信息
        public static DataTable GetRoleInfoByCompanyCD(string ShowType, string OprtType)
        {
            try
            {
                string companyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;
                // string CompanyCD = "1001";
                DataTable dt = UserRoleSelectDBHelper.GetRoleInfo(companyCD);
                if (Convert.ToInt32(ShowType) > 1)
                {
                    ShowType = string.Empty;
                    OprtType = string.Empty;
                }
                if (!string.IsNullOrEmpty(ShowType) &&
                    !string.IsNullOrEmpty(OprtType))
                {
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        //单选部门 
                        if (ShowType == SHOWTYEP_CODE_SELECT_Role && OprtType == OPRT_CODE_SELECT)
                        {
                            foreach (DataRow rows in dt.Rows)
                            {
                                rows["RoleName"] = "<input type='radio' name='select'  id='chk_" + rows["RoleID"] + "' value='" + rows["RoleID"] + "|" + rows["RoleName"].ToString() + "'   >" + rows["RoleName"].ToString();
                            }

                        }//多选部门
                        //else if (ShowType == SHOWTYEP_CODE_SELECT_Role && OprtType == OPRT_CODE_SELECTS)
                        //{
                        //    foreach (DataRow rows in dt.Rows)
                        //    {
                        //        rows["RoleName"] = "<input type='checkbox' name='select'  id='chk_" + rows["ID"] + "' value='" + rows["ID"] + "|" + rows["RoleName"].ToString() + "'   >" + rows["RoleName"].ToString();
                        //    }
                        //}
                    }
                }
                return dt;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region 获取部门信息
        /// <summary>
        /// 获取部门信息
        /// </summary>
        /// <returns>DataTable 部门信息</returns>
        public static DataTable GetRoleInfo(string TypeID)
        {
            string companyCD = string.Empty;
            //获取公司代码
            try
            {
                companyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;
            }
            catch
            {
                companyCD = "AAAAAA";
            }
            //查询部门信息
            DataTable dtRole = UserRoleSelectDBHelper.GetRoleInfo(companyCD);
            //部门信息不存在时，返回
            if (dtRole == null || dtRole.Rows.Count < 1) return dtRole;

            //定义返回的部门信息变量
            DataTable dtReturn = new DataTable();
            //复制部门信息表结构
            dtReturn = dtRole.Clone();
            #region 部门信息排序处理
            //获取第一级部门信息
            DataRow[] drSuperRole = dtRole.Select("SuperRoleID IS NULL");
            //遍历第一级部门
            for (int i = 0; i < drSuperRole.Length; i++)
            {
                DataRow drFirstRole = (DataRow)drSuperRole[i];
                //获取部门ID
                int RoleID = (int)drFirstRole["RoleID"];
                //替换部门名称内容
                if (TypeID == ConstUtil.TYPE_DANX_CODE)
                {
                    drFirstRole["RoleName"] = "<input type='radio' name='select'  id='chk_" + RoleID.ToString() + "' value='" + ConstUtil.Role_EMPLOY_SELECT_Role
                  + RoleID.ToString() + "|" + drFirstRole["RoleName"].ToString() + "'>" + drFirstRole["RoleName"].ToString();
                }
                else if (TypeID == ConstUtil.TYPE_DUOX_CODE)
                {
                    drFirstRole["RoleName"] = "<input type='checkbox' name='select'  id='chk_" + RoleID.ToString() + "' value='" + ConstUtil.Role_EMPLOY_SELECT_Role
                                  + RoleID.ToString() + "|" + drFirstRole["RoleName"].ToString() + "'>" + drFirstRole["RoleName"].ToString();
                }
                //导入第一级部门
                dtReturn.ImportRow(drFirstRole);
                //设定子部门
                dtReturn = ReorderRoleRow(dtReturn, RoleID, dtRole, 1, TypeID);
            }
            #endregion
            return dtReturn;
        }
        #endregion

        #region 部门信息排序处理
        /// <summary>
        /// 部门信息排序处理
        /// 获取部门的子部门信息，支持无限级的子部门
        /// </summary>
        /// <param name="dtReturn">返回的数据集</param>
        /// <param name="RoleID">部门ID</param>
        /// <param name="dtRole">部门信息</param>
        /// <param name="align">对齐位置</param>
        /// <returns></returns>
        private static DataTable ReorderRoleRow(DataTable dtReturn, int RoleID,
            DataTable dtRole, int align, string TypeID)
        {
            //获取部门的子部门
            DataRow[] drSubRole = dtRole.Select("SuperRoleID = " + RoleID);
            //遍历所有子部门
            for (int i = 0; i < drSubRole.Length; i++)
            {
                //通过对齐位置，来控制该部门前空格数
                string alignPosition = string.Empty;
                for (int j = 0; j < align; j++)
                {
                    alignPosition += "&nbsp;&nbsp;";
                }
                //获取子部门数据
                DataRow drSubRoleTemp = (DataRow)drSubRole[i];
                //获取子部门ID
                int subRoleID = (int)drSubRoleTemp["RoleID"];
                if (TypeID == ConstUtil.TYPE_DANX_CODE)
                {
                    drSubRoleTemp["RoleName"] = alignPosition + "<input type='radio' name='select'  id='chk_" + subRoleID.ToString() + "' value='" + ConstUtil.Role_EMPLOY_SELECT_Role
                                   + subRoleID.ToString() + "|" + drSubRoleTemp["RoleName"].ToString() + "'>" + drSubRoleTemp["RoleName"].ToString();
                }
                else if (TypeID == ConstUtil.TYPE_DUOX_CODE)
                {
                    drSubRoleTemp["RoleName"] = alignPosition + "<input type='checkbox'   id='chk_" + subRoleID.ToString() + "' value='" + ConstUtil.Role_EMPLOY_SELECT_Role
                                                    + subRoleID.ToString() + "|" + drSubRoleTemp["RoleName"].ToString() + "'>" + drSubRoleTemp["RoleName"].ToString();
                }

                //导入子部门
                dtReturn.ImportRow(drSubRoleTemp);
                //生成子部门的子部门信息
                dtReturn = ReorderRoleRow(dtReturn, subRoleID, dtRole, align + 1, TypeID);
            }
            return dtReturn;
        }
        #endregion

     
        #region 获取部门信息(单选)
        /// <summary>
        /// 获取部门信息
        /// </summary>
        /// <returns>DataTable 部门信息</returns>
        public static DataTable GetDepartmentInfo()
        {
            string companyCD = string.Empty;
            //获取公司代码
            try
            {
                companyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;
            }
            catch
            {
                companyCD = "AAAAAA";
            }
            //string companyCD = "AAAAAA";
            //查询部门信息
            DataTable dtRole = UserRoleSelectDBHelper.GetRoleInfo(companyCD);
            //部门信息不存在时，返回
            if (dtRole == null || dtRole.Rows.Count < 1) return dtRole;

            //定义返回的部门信息变量
            DataTable dtReturn = new DataTable();
            //复制部门信息表结构
            dtReturn = dtRole.Clone();

            #region 部门信息排序处理

            //获取第一级部门信息
            DataRow[] drSuperRole = dtRole.Select("SuperRoleID IS NULL");
            //遍历第一级部门
            for (int i = 0; i < drSuperRole.Length; i++)
            {
                DataRow drFirstRole = (DataRow)drSuperRole[i];
                //获取部门ID
                int RoleID = (int)drFirstRole["RoleID"];
                //替换部门名称内容
                drFirstRole["RoleName"] = "<input type='radio' name=\"radipRole\"  id='chk_" + RoleID.ToString() + "' value='" + drFirstRole["ID"].ToString() + "' onclick=\"popRoleObj.FillRoleValue(this,'" + drFirstRole["RoleName"].ToString() + "');\">" + drFirstRole["RoleName"].ToString();
                //导入第一级部门
                dtReturn.ImportRow(drFirstRole);
                //设定子部门
                dtReturn = ReorderDepartmentRow(dtReturn, RoleID, dtRole, 1);
            }

            #endregion

            return dtReturn;
        }
        #endregion

        #region 部门信息排序处理
        /// <summary>
        /// 部门信息排序处理
        /// 获取部门的子部门信息，支持无限级的子部门
        /// </summary>
        /// <param name="dtReturn">返回的数据集</param>
        /// <param name="RoleID">部门ID</param>
        /// <param name="dtRole">部门信息</param>
        /// <param name="align">对齐位置</param>
        /// <returns></returns>
        private static DataTable ReorderDepartmentRow(DataTable dtReturn, int RoleID, DataTable dtRole, int align)
        {
            //获取部门的子部门
            DataRow[] drSubRole = dtRole.Select("SuperRoleID = " + RoleID);

            //遍历所有子部门
            for (int i = 0; i < drSubRole.Length; i++)
            {
                //通过对齐位置，来控制该部门前空格数
                string alignPosition = string.Empty;
                for (int j = 0; j < align; j++)
                {
                    alignPosition += "&nbsp;&nbsp;";
                }
                //获取子部门数据
                DataRow drSubRoleTemp = (DataRow)drSubRole[i];
                //获取子部门ID
                int subRoleID = (int)drSubRoleTemp["RoleID"];
                drSubRoleTemp["RoleName"] = alignPosition + "<input type='radio' name=\"radipRole\" id='chk_" + subRoleID.ToString() + "' value='" + drSubRoleTemp["RoleID"].ToString() + "' onclick=\"popRoleObj.FillRoleValue(this,'" + drSubRoleTemp["RoleName"].ToString() + "');\">" + drSubRoleTemp["RoleName"].ToString();
                //导入子部门
                dtReturn.ImportRow(drSubRoleTemp);
                //生成子部门的子部门信息
                dtReturn = ReorderDepartmentRow(dtReturn, subRoleID, dtRole, align + 1);
            }
            return dtReturn;
        }
        #endregion
    }
}
