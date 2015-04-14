using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using XBase.Data.DBHelper;
using System.Data.SqlClient;
using System.Data;
using XBase.Common;
namespace XBase.Data.Personal.Culture
{
    /// <summary>
    /// 数据访问类CultureDocs。
    /// </summary>
    public class CultureDocs
    {
        public CultureDocs()
        { }
        #region  成员方法


        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public int Exists(string where)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select @count=count(*) from [officedba].CultureDocs");
            strSql.Append(" where " + where);
            SqlParameter[] parameters = {
					new SqlParameter("@count", SqlDbType.Int,4)};

            parameters[0].Direction = ParameterDirection.Output;

            DBHelper.SqlHelper.ExecuteSql(strSql.ToString(), parameters);

            return int.Parse(parameters[0].Value.ToString());

        }

        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(int ID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from [officedba].CultureDocs");
            strSql.Append(" where ID=@ID ");
            SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.Int,4)};
            parameters[0].Value = ID;

            return DBHelper.SqlHelper.ExecuteSql(strSql.ToString(), parameters).Rows.Count > 0;
        }

        /// <summary>
        /// 增加一条数据
        /// </summary>
        /// 20121013 修改 添加custno 字段
        /// 20121025 添加CanViewUserName 字段
        public int AddByCust(XBase.Model.Personal.Culture.CultureDocs model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into [officedba].CultureDocs(");
            strSql.Append("CompanyCD,CultureTypeID,Title,Culturetent,Attachment,CreateDeptID,Creator,CreateDate,ModifiedDate,ModifiedUserID,OrderID,UserCanViewUserName)");
            strSql.Append(" values (");
            strSql.Append("@CompanyCD,@CultureTypeID,@Title,@Culturetent,@Attachment,@CreateDeptID,@Creator,@CreateDate,@ModifiedDate,@ModifiedUserID,@OrderID,@UserCanViewUserName)");
            strSql.Append(";select @ID=@@IDENTITY");
            SqlParameter[] parameters = {
					new SqlParameter("@CompanyCD", SqlDbType.VarChar,8),
					new SqlParameter("@CultureTypeID", SqlDbType.Int,4),
					new SqlParameter("@Title", SqlDbType.VarChar,100),
					new SqlParameter("@Culturetent", SqlDbType.NText),
					new SqlParameter("@Attachment", SqlDbType.VarChar,200),
                    new SqlParameter("@CreateDeptID", SqlDbType.Int,4),
					new SqlParameter("@Creator", SqlDbType.Int,4),
					new SqlParameter("@CreateDate", SqlDbType.DateTime),
					new SqlParameter("@ModifiedDate", SqlDbType.DateTime),
					new SqlParameter("@ModifiedUserID", SqlDbType.VarChar,20),
                   
                    new SqlParameter("@ID", SqlDbType.Int,0) ,
                    new SqlParameter("@OrderID", SqlDbType.VarChar, 50),
                    new SqlParameter("@UserCanViewUserName", SqlDbType.VarChar, 1024)};//20121025 添加CanViewUserName 字段
            parameters[0].Value = model.CompanyCD;
            parameters[1].Value = model.CultureTypeID;
            parameters[2].Value = model.Title;
            parameters[3].Value = model.Culturetent;
            parameters[4].Value = model.Attachment;
            parameters[5].Value = model.CreateDeptID;
            parameters[6].Value = model.Creator;
            parameters[7].Value = model.CreateDate;
            parameters[8].Value = model.ModifiedDate;
            parameters[9].Value = model.ModifiedUserID;

            parameters[10].Direction = ParameterDirection.Output;
            parameters[11].Value = model.Custno.ToString();// 20121013 修改 添加OrderID 字段
            parameters[12].Value = model.UserCanViewUserName.ToString();//20121025 添加CanViewUserName 字段

            SqlHelper.ExecuteTransSql(strSql.ToString(), parameters);
            return int.Parse(parameters[10].Value.ToString());
        }

        /// <summary>
        /// 增加一条数据
        /// </summary>
        /// 20121013 修改 添加custno 字段
        public int Add(XBase.Model.Personal.Culture.CultureDocs model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into [officedba].CultureDocs(");
            strSql.Append("CompanyCD,CultureTypeID,Title,Culturetent,Attachment,CreateDeptID,Creator,CreateDate,ModifiedDate,ModifiedUserID,UserCanViewUserName,iFileName,iFileAddr)");
            strSql.Append(" values (");
            strSql.Append("@CompanyCD,@CultureTypeID,@Title,@Culturetent,@Attachment,@CreateDeptID,@Creator,@CreateDate,@ModifiedDate,@ModifiedUserID,@UserCanViewUserName,'"+model.IFileName+"','"+model.IFileAddr.Replace('\\','/')+"')");
            strSql.Append(";select @ID=@@IDENTITY");
            SqlParameter[] parameters = {
					new SqlParameter("@CompanyCD", SqlDbType.VarChar,8),
					new SqlParameter("@CultureTypeID", SqlDbType.Int,4),
					new SqlParameter("@Title", SqlDbType.VarChar,100),
					new SqlParameter("@Culturetent", SqlDbType.NText),
					new SqlParameter("@Attachment", SqlDbType.VarChar,200),
                    new SqlParameter("@CreateDeptID", SqlDbType.Int,4),
					new SqlParameter("@Creator", SqlDbType.Int,4),
					new SqlParameter("@CreateDate", SqlDbType.DateTime),
					new SqlParameter("@ModifiedDate", SqlDbType.DateTime),
					new SqlParameter("@ModifiedUserID", SqlDbType.VarChar,20),
                   
                   
                    new SqlParameter("@ID", SqlDbType.Int,0),
                    new SqlParameter("@UserCanViewUserName",SqlDbType.VarChar,1024)//2012-10-26 添加可查看人员变量
                                        };
            // new SqlParameter("@OrderID", SqlDbType.VarChar, 50)// 20121013 修改 添加OrderID 字段
            parameters[0].Value = model.CompanyCD;
            parameters[1].Value = model.CultureTypeID;
            parameters[2].Value = model.Title;
            parameters[3].Value = model.Culturetent;
            parameters[4].Value = model.Attachment;
            parameters[5].Value = model.CreateDeptID;
            parameters[6].Value = model.Creator;
            parameters[7].Value = model.CreateDate;
            parameters[8].Value = model.ModifiedDate;
            parameters[9].Value = model.ModifiedUserID;

            parameters[10].Direction = ParameterDirection.Output;
            parameters[11].Value = model.UserCanViewUserName;//2012-10-26 添加 可查看人员变量

            SqlHelper.ExecuteTransSql(strSql.ToString(), parameters);
            return int.Parse(parameters[10].Value.ToString());
        }
        /// <summary>
        /// 更新一条数据
        /// </summary>
        public void Update(XBase.Model.Personal.Culture.CultureDocs model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update [officedba].CultureDocs set ");
            strSql.Append("CompanyCD=@CompanyCD,");
            strSql.Append("CultureTypeID=@CultureTypeID,");
            strSql.Append("Title=@Title,");
            strSql.Append("Culturetent=@Culturetent,");
            strSql.Append("Attachment=@Attachment,");
            strSql.Append("CreateDeptID=@CreateDeptID,");
            strSql.Append("Creator=@Creator,");
            strSql.Append("CreateDate=@CreateDate,");
            strSql.Append("ModifiedDate=@ModifiedDate,");
            strSql.Append("ModifiedUserID=@ModifiedUserID");
            strSql.Append(",UserCanViewUserName=@UserCanViewUserName");//2012-10-26 添加 可查看人员变量
            strSql.Append(",iFileName='"+model.IFileName+"'"); 
            strSql.Append(",iFileAddr='"+model.IFileAddr.Replace('\\','/')+"'"); 

            strSql.Append(" where ID=@ID ");
            SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.Int,4),
					new SqlParameter("@CompanyCD", SqlDbType.VarChar,8),
					new SqlParameter("@CultureTypeID", SqlDbType.Int,4),
					new SqlParameter("@Title", SqlDbType.VarChar,100),
					new SqlParameter("@Culturetent", SqlDbType.NText),
					new SqlParameter("@Attachment", SqlDbType.VarChar,200),
                    new SqlParameter("@CreateDeptID", SqlDbType.Int,4),
					new SqlParameter("@Creator", SqlDbType.Int,4),
					new SqlParameter("@CreateDate", SqlDbType.DateTime),
					new SqlParameter("@ModifiedDate", SqlDbType.DateTime),
					new SqlParameter("@ModifiedUserID", SqlDbType.VarChar,20),
                    new SqlParameter("@UserCanViewUserName",SqlDbType.VarChar,1024)//2012-10-26 添加 可查看人员变量
                                        };
            parameters[0].Value = model.ID;
            parameters[1].Value = model.CompanyCD;
            parameters[2].Value = model.CultureTypeID;
            parameters[3].Value = model.Title;
            parameters[4].Value = model.Culturetent;
            parameters[5].Value = model.Attachment;
            parameters[6].Value = model.CreateDeptID;
            parameters[7].Value = model.Creator;
            parameters[8].Value = model.CreateDate;
            parameters[9].Value = model.ModifiedDate;
            parameters[10].Value = model.ModifiedUserID;
            parameters[11].Value = model.UserCanViewUserName;//2012-10-26 添加 可查看人员变量

            SqlHelper.ExecuteTransSql(strSql.ToString(), parameters);
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public void Delete(int ID)
        {
            StringBuilder strSql1 = new StringBuilder();
            strSql1.Append("delete from officedba.CultureDocsFile where CultureDocsID=@ID ");
            SqlParameter[] param = {
					new SqlParameter("@ID", SqlDbType.Int,4)};
            param[0].Value = ID;
            SqlHelper.ExecuteTransSql(strSql1.ToString(), param);

            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete [officedba].CultureDocs ");
            strSql.Append(" where ID=@ID ");
            SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.Int,4)};
            parameters[0].Value = ID;

            SqlHelper.ExecuteTransSql(strSql.ToString(), parameters);
        }


        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        /// 2012-10-26 添加 可查看人员变量
        public XBase.Model.Personal.Culture.CultureDocs GetModel(int ID)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 a.ID,a.CompanyCD,a.CultureTypeID,a.Title,a.Culturetent,a.Attachment,a.CreateDeptID,a.Creator,a.iFileName,a.iFileAddr, ");
            strSql.Append(" a.CreateDate,a.ModifiedDate,a.ModifiedUserID,a.UserCanViewUserName,b.employeename creatorname,d.employeename modifier,di.deptname ");//2012-10-26 添加 可查看人员变量
            strSql.Append(" from [officedba].CultureDocs  a");
            strSql.Append(" left join officedba.employeeinfo  b on b.id=a.creator and b.companycd=a.companycd ");
            strSql.Append(" left join officedba.userinfo c on c.userid=a.ModifiedUserId and c.companycd=a.companycd ");
            strSql.Append(" left join officedba.employeeinfo  d on d.id=c.employeeID and c.companycd=d.companycd ");
            strSql.Append(" left join officedba.deptinfo di on di.id=a.createDeptID and di.companycd=a.companycd ");
            strSql.Append(" where a.ID=@ID ");
            SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.Int,4)};
            parameters[0].Value = ID;

            XBase.Model.Personal.Culture.CultureDocs model = new XBase.Model.Personal.Culture.CultureDocs();
            DataTable dt = SqlHelper.ExecuteSql(strSql.ToString(), parameters);
            DataSet ds = new DataSet();
            ds.Tables.Add(dt);
            if (ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["ID"].ToString() != "")
                {
                    model.ID = int.Parse(ds.Tables[0].Rows[0]["ID"].ToString());
                }
                model.CompanyCD = ds.Tables[0].Rows[0]["CompanyCD"].ToString();
                if (ds.Tables[0].Rows[0]["CultureTypeID"].ToString() != "")
                {
                    model.CultureTypeID = int.Parse(ds.Tables[0].Rows[0]["CultureTypeID"].ToString());
                }
                if (ds.Tables[0].Rows[0]["CreateDeptID"].ToString() != "")
                {
                    model.CreateDeptID = int.Parse(ds.Tables[0].Rows[0]["CreateDeptID"].ToString());
                }

                model.Title = ds.Tables[0].Rows[0]["Title"].ToString();
                model.Culturetent = ds.Tables[0].Rows[0]["Culturetent"].ToString();
                model.Attachment = ds.Tables[0].Rows[0]["Attachment"].ToString();

                if (ds.Tables[0].Rows[0]["Creator"].ToString() != "")
                {
                    model.Creator = int.Parse(ds.Tables[0].Rows[0]["Creator"].ToString());
                }

                if (ds.Tables[0].Rows[0]["CreateDate"].ToString() != "")
                {
                    model.CreateDate = DateTime.Parse(ds.Tables[0].Rows[0]["CreateDate"].ToString());
                }
                if (ds.Tables[0].Rows[0]["ModifiedDate"].ToString() != "")
                {
                    model.ModifiedDate = DateTime.Parse(ds.Tables[0].Rows[0]["ModifiedDate"].ToString());
                }
                model.ModifiedUserID = ds.Tables[0].Rows[0]["ModifiedUserID"].ToString();
                model.CreateName = ds.Tables[0].Rows[0]["creatorname"].ToString();
                model.Modifier = ds.Tables[0].Rows[0]["modifier"].ToString();
                model.DeptName = ds.Tables[0].Rows[0]["deptname"].ToString();
                model.UserCanViewUserName = ds.Tables[0].Rows[0]["UserCanViewUserName"].ToString();//2012-10-26 添加 可查看人员变量
                model.IFileName = ds.Tables[0].Rows[0]["iFileName"].ToString();
                model.IFileAddr = ds.Tables[0].Rows[0]["iFileAddr"].ToString();
                return model;
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataTable GetList(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select ID,CompanyCD,CultureTypeID,Title,Culturetent,Attachment,CreateDeptID,Creator,CreateDate,ModifiedDate,ModifiedUserID,iFileName,iFileAddr ");
            strSql.Append(" FROM [officedba].CultureDocs ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            return DBHelper.SqlHelper.ExecuteSql(strSql.ToString());
        }

        /// <summary>
        /// GetPageData
        /// </summary>    
        /// <param name="where"></param>
        /// <param name="fields"></param>
        /// <param name="orderExp"></param>
        /// <param name="pageindex"></param>
        /// <param name="pagesize"></param>
        /// <returns></returns>
        public int GetPageData(out DataTable dt, string where, string fields, string orderExp, int pageindex, int pagesize)
        {
            /*          
            set @where = '1=1'
            set @fields = '*'
            set @OrderExp = '[ID] ASC'
            set @pageIndex=1
            set @pageSize=10
             */
            if (where.Trim() + "" == "")
            {
                where = "1=1";
            }

            SqlParameter[] prams = {                                      
									SqlParameterHelper.MakeInParam("@OrderExp",SqlDbType.NVarChar,0,orderExp),
									SqlParameterHelper.MakeInParam("@fields",SqlDbType.NVarChar,0,fields),
									SqlParameterHelper.MakeInParam("@where",SqlDbType.NVarChar,0,where),

									SqlParameterHelper.MakeInParam("@pageSize",SqlDbType.Int,0,pagesize),
									SqlParameterHelper.MakeInParam("@pageIndex",SqlDbType.Int,0,pageindex),
									SqlParameterHelper.MakeParam("@RecsCount",SqlDbType.Int,0,ParameterDirection.Output,null)
									
								   };
            DataSet ds = SqlHelper.ExecuteDataset("", "[officedba].[CultureDocs_GetPageData]", prams);
            dt = ds.Tables[0];
            return Convert.ToInt32(prams[prams.Length - 1].Value);
        }
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataTable GetCultureList(int pageIndex, int pageCount, string ord, ref int TotalCount, string time1, string time2, string strTitle, string content, string createname, string culturetype)
        {

            string strSql = string.Empty;
            strSql += "select  a.[ID] ,a.[CultureTypeID] ,a.[Title] ,a.[Culturetent] ,a.[Creator] , a.Attachment,a.CreateDate,b.TypeName,c.EmployeeName,d.DeptName,a.ModifiedDate,a.iFileName,a.iFileAddr ";
            strSql += "  From [officedba].[CultureDocs] as a  ";
            strSql += " left join [officedba].CultureType as b on a.CultureTypeID = b.ID";
            strSql += " left join [officedba].EmployeeInfo as c on a.Creator = c.ID ";
            strSql += " left join officedba.DeptInfo as d on a.CreateDeptID = d.ID  ";
            strSql += " where a.companycd=@CompanyCD";

            XBase.Common.UserInfoUtil UserInfo = (XBase.Common.UserInfoUtil)XBase.Common.SessionUtil.Session["UserInfo"];
            DataTable dt = XBase.Data.Office.SystemManager.RoleInfoDBHelper.GetRoleInfoByUserID(UserInfo.UserID);
            if (dt != null && dt.Rows.Count > 0)
            {
                //strSql += " and ( ";
                //if (dt.Rows[0]["RoleRange"].ToString() == "0")
                //{
                //    strSql += "(a.Creator IN  ";
                //    strSql += "( SELECT ID FROM  officedba.EmployeeInfo ";
                //    strSql += "  WHERE DeptID IN (SELECT a.ID  ";
                //    strSql += " FROM officedba.DeptInfo a,DepartInfo_Children(" + UserInfo.DeptID.ToString() + ") b  ";
                //    strSql += "  WHERE a.ID=b.ID) AND DeptID not in (select ID from DepartInfo_Children(" + UserInfo.DeptID.ToString() + ") ))) or ";
                //}
                //if (dt.Rows[0]["RoleRange"].ToString() == "1")
                //{

                //    strSql += "(a.Creator IN  ";
                //    strSql += " (SELECT ID FROM  officedba.EmployeeInfo ";
                //    strSql += "  WHERE DeptID IN (SELECT a.ID  ";
                //    strSql += " FROM officedba.DeptInfo a,DepartInfo_Children(" + UserInfo.DeptID.ToString() + ") b  ";
                //    strSql += "  WHERE a.ID=b.ID) )) or ";
                //}
                //if (dt.Rows[0]["RoleRange"].ToString() == "2")
                //{

                //    strSql += "(a.Creator IN  ";
                //    strSql += " (SELECT ID FROM  officedba.EmployeeInfo ";
                //    strSql += "  WHERE DeptID='" + UserInfo.DeptID.ToString() + "' )) or ";
                //}
                //if (dt.Rows[0]["RoleRange"].ToString() == "3")
                //{
                //    strSql += "(a.Creator IN  ";
                //    strSql += "( SELECT ID FROM  officedba.EmployeeInfo ";
                //    strSql += "  WHERE DeptID IN (SELECT a.ID  ";
                //    strSql += " FROM officedba.DeptInfo a,DepartInfo_Children(" + UserInfo.BranchID.ToString() + ") b  ";
                //    strSql += "  WHERE a.ID=b.ID))) or ";
                //}

                strSql += " and ( ";
                if (dt.Rows[0]["RoleRange"].ToString() == "0")
                {
                    strSql += "(a.Creator IN  ";
                    strSql += "( SELECT ID FROM  officedba.EmployeeInfo ";
                    strSql += "  WHERE DeptID IN (SELECT a.ID  ";
                    strSql += " FROM officedba.DeptInfo a,DepartInfo_Children(" + UserInfo.DeptID.ToString() + ") b  ";
                    strSql += "  WHERE a.ID=b.ID) AND DeptID not in (select ID from DepartInfo_Children(" + UserInfo.DeptID.ToString() + ") ))) or ";
                }
                if (dt.Rows[0]["RoleRange"].ToString() == "1")
                {

                    strSql += "(a.Creator IN  ";
                    strSql += " (SELECT ID FROM  officedba.EmployeeInfo ";
                    strSql += "  WHERE DeptID IN (SELECT a.ID  ";
                    strSql += " FROM officedba.DeptInfo a,DepartInfo_Children(" + UserInfo.DeptID.ToString() + ") b  ";
                    strSql += "  WHERE a.ID=b.ID) )) or ";
                }
                if (dt.Rows[0]["RoleRange"].ToString() == "2")
                {

                    strSql += "(a.Creator IN  ";
                    strSql += " (SELECT ID FROM  officedba.EmployeeInfo ";
                    strSql += "  WHERE DeptID='" + UserInfo.DeptID.ToString() + "' )) or ";
                }
                if (dt.Rows[0]["RoleRange"].ToString() == "3")
                {
                    strSql += "(a.Creator IN  ";
                    strSql += "( SELECT ID FROM  officedba.EmployeeInfo ";
                    strSql += "  WHERE DeptID IN (SELECT a.ID  ";
                    strSql += " FROM officedba.DeptInfo a,DepartInfo_Children(" + UserInfo.BranchID.ToString() + ") b  ";
                    strSql += "  WHERE a.ID=b.ID))) or ";
                }
            }
            strSql += "(a.[Creator]=" + UserInfo.EmployeeID + ") ";
            strSql += " OR a.CultureTypeID in (select ID from officedba.CultureType where CanViewUser like '%" + UserInfo.EmployeeID + "%'))";


            string strCompanyCD = string.Empty;//单位编号
            strCompanyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;
            ArrayList arr = new ArrayList();
            arr.Add(new SqlParameter("@CompanyCD", strCompanyCD));
            if (time1 != "" && time2 != "")
            {
                strSql += " and a.CreateDate between '" + time1 + " 0:00:00' and '" + time2 + " 23:59:59' ";
            }
            else if (time1 == "" && time2 != "")
            {
                strSql += " and a.CreateDate <='" + time2 + " 23:59:59' ";
            }
            else if (time1 != "" && time2 == "")
            {
                strSql += " and a.CreateDate >='" + time1 + " 0:00:00' ";
            }

            if (strTitle != "")
            {
                strSql += " and a.Title like @title ";
                arr.Add(new SqlParameter("@Title", "%" + strTitle + "%"));
            }
            if (content != "")
            {
                strSql += " and a.Culturetent like @content ";
                arr.Add(new SqlParameter("@content", "%" + content + "%"));
            }
            if (createname != "")
            {
                strSql += " and c.EmployeeName like @name ";
                arr.Add(new SqlParameter("@name", "%" + createname + "%"));
            }
            if (culturetype != "0")
            {
                //strSql += " and a.CultureTypeID = @culturetype ";
                strSql += " and a.CultureTypeID IN (SELECT ID FROM CultureType_Children(@culturetype))";
                arr.Add(new SqlParameter("@culturetype", culturetype));
            }

            return SqlHelper.CreateSqlByPageExcuteSqlArr(strSql.ToString(), pageIndex, pageCount, ord, arr, ref TotalCount);
        }

        ///<summary>
        ///保存页面文档内容到数据库
        ///</summary>
        public int addDoctent(string DocsID, string docType, byte[] docContent)
        {
            StringBuilder strSql = new StringBuilder();
            XBase.Common.UserInfoUtil UserInfo = (XBase.Common.UserInfoUtil)XBase.Common.SessionUtil.Session["UserInfo"];
            strSql.Append("insert into [officedba].CultureDocsFile(");
            strSql.Append("CompanyCD,CultureDocsID,DocType,DocContent,ModifiedDate,ModifiedUserID)");
            strSql.Append(" values (");
            strSql.Append("@CompanyCD,@CultureDocsID,@DocType,@DocContent,@ModifiedDate,@ModifiedUserID)");
            strSql.Append(";select @ID=@@IDENTITY");
            SqlParameter[] parameters = {
					new SqlParameter("@CompanyCD", SqlDbType.VarChar,8),
					new SqlParameter("@CultureDocsID", SqlDbType.Int,4),
					new SqlParameter("@DocType", SqlDbType.VarChar,8),
					new SqlParameter("@DocContent", SqlDbType.Binary),
					new SqlParameter("@ModifiedDate", SqlDbType.DateTime),
					new SqlParameter("@ModifiedUserID", SqlDbType.VarChar,10),
                     new SqlParameter("@ID", SqlDbType.Int,0) };
            parameters[0].Value = UserInfo.CompanyCD;
            parameters[1].Value = Convert.ToInt32(DocsID);
            parameters[2].Value = docType;
            parameters[3].Value = docContent;
            parameters[4].Value = DateTime.Now;
            parameters[5].Value = UserInfo.UserID;

            parameters[6].Direction = ParameterDirection.Output;

            SqlHelper.ExecuteTransSql(strSql.ToString(), parameters);
            return int.Parse(parameters[6].Value.ToString());
        }

        ///<summary>
        ///新建时修改主ID
        ///</summary>
        public void UpdateMainID(int DocsID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("UPDATE [officedba].[CultureDocsFile]   SET [CultureDocsID] = @DocsID  ");
            strSql.Append(" WHERE ID=(select max(id) ID FROM [officedba].[CultureDocsFile] WHERE 1=1) ");
            SqlParameter[] parameters = {
					new SqlParameter("@DocsID", SqlDbType.Int,4)};
            parameters[0].Value = DocsID;

            SqlHelper.ExecuteTransSql(strSql.ToString(), parameters);
        }

        ///<summary>
        ///获取文档表中某条企业文化关联的最大ID的一条记录（即最新的数据）
        ///</summary>
        public DataTable GetLastUpdate(int DocsID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(" select ID,DocType  from [officedba].[CultureDocsFile] where ID =  ");
            strSql.Append(" (select max(id) ID FROM [officedba].[CultureDocsFile] WHERE CultureDocsID='" + DocsID + "' group by cultureDocsID) ");
            return SqlHelper.ExecuteSql(strSql.ToString());
        }
        ///<summary>
        ///获取文档表某条记录，根据ID
        ///</summary>
        public DataTable GetCultureDocsFile(int DocsID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(" select ID,CompanyCD,CultureDocsID,DocType,DocContent,ModifiedUserID,ModifiedDate  from [officedba].[CultureDocsFile] where ID ='" + DocsID + "'  ");
            return SqlHelper.ExecuteSql(strSql.ToString());
        }

        /// <summary>
        /// 得到一个企业文化关联的文档的修改次数
        /// </summary>
        public int GetModCount(int ID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(" select count(*) modcount  from [officedba].[CultureDocsFile] where cultureDocsID ='" + ID + "'  ");
            DataTable dt = SqlHelper.ExecuteSql(strSql.ToString());
            return int.Parse(dt.Rows[0]["modcount"].ToString());
        }
        /// <summary>
        /// 获取某条企业文化记录的修改历史
        /// </summary>
        public DataTable GetCultureDocsFileList(string strDocsID, int pageIndex, int pageCount, string ord, ref int totalCount)
        {
            string strSql = string.Empty;
            strSql += " select a.ID,a.CompanyCD,a.CultureDocsID,a.doctype,a.doccontent,a.modifiedUserID,a.ModifiedDate,CONVERT(varchar(100), a.ModifiedDate, 23) AS s_ModifiedDate,e.employeename modifier";
            strSql += "  From [officedba].[CultureDocsFile] as a  ";
            strSql += " left join officedba.userinfo b on b.userid=a.modifieduserid and b.companycd=a.companycd ";
            strSql += " left join officedba.employeeinfo e on e.id=b.employeeid and e.companycd=b.companycd  ";
            strSql += "  where a.cultureDocsID=@ID ";
            ArrayList arr = new ArrayList();
            arr.Add(new SqlParameter("@ID", strDocsID));
            return SqlHelper.CreateSqlByPageExcuteSqlArr(strSql.ToString(), pageIndex, pageCount, ord, arr, ref totalCount);
        }

        /// <summary>
        /// 删除一条子项数据
        /// </summary>
        public void DelSubItem(int ID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from officedba.CultureDocsFile where ID=@ID ");
            SqlParameter[] param = {
					new SqlParameter("@ID", SqlDbType.Int,4)};
            param[0].Value = ID;
            SqlHelper.ExecuteTransSql(strSql.ToString(), param);
        }
        /// <summary>
        /// 根据客户查询相关文档
        /// </summary>
        /// 2012-10-17 添加 根据客户ID检索文档
        public DataTable getDocByCustNo(string custID)
        {
            string strSql = string.Empty;
            XBase.Common.UserInfoUtil UserInfo = (XBase.Common.UserInfoUtil)XBase.Common.SessionUtil.Session["UserInfo"];
            strSql += "SELECT  a.ID,a.companycd,t1.TypeName,a.CultureTypeID,a.ModifiedUserID,a.Title,a.Creator,a.Attachment,a.CreateDate,a.Culturetent,a.ModifiedDate,e1.EmployeeName,b.doctype FROM   officedba.CultureDocs as a";
            strSql += " left join officedba.CultureDocsFile  b on b.id =a.id left join officedba.EmployeeInfo e1 on e1.id=a.creator  left join officedba.CultureType  t1 on t1.ID =a.CultureTypeID";
            strSql += " WHERE a.CompanyCD =@CompanyCD ";
            if (custID != "")
                strSql += " and a.OrderID =@custID";
            strSql += "  order by a.CreateDate desc";//2012-10-17 添加 根据时间排序
            SqlParameter[] parameters = {
                    new SqlParameter("@CompanyCD", SqlDbType.VarChar,8),
					new SqlParameter("@custID", SqlDbType.VarChar,50)
                                   };
            parameters[0].Value = UserInfo.CompanyCD;
            parameters[1].Value = custID;
            return SqlHelper.ExecuteSql(strSql, parameters);
        }

        /// <summary>
        /// 删除CultureDocs表中一条数据
        /// </summary>
        /// 2012-10-20 by dyg 
        /// 返回参数: rs  类型:int  作用:记录对数据表影响的行数
        public int DeleteByID(int ID)
        {
            int rs = 0;
            StringBuilder strSql1 = new StringBuilder();
            strSql1.Append("delete from officedba.CultureDocs where ID=@ID ");
            SqlParameter[] param = {
					new SqlParameter("@ID", SqlDbType.Int,4)};
            param[0].Value = ID;

            rs = SqlHelper.ExecuteTransSql(strSql1.ToString(), param);
            return rs;
        }

        /// <summary>
        ///  删除已选择的数据
        /// </summary>
        /// <param name="fID"></param>
        /// <returns></returns>
        public bool DelFileByCheck(string fID)
        {
            bool isSucc = false;
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
             string[] strFileID = null;
             strFileID = fID.Split(',');
             for (int i = 0; i < strFileID.Length; i++)
             {
                 strFileID[i] = "'" + strFileID[i] + "'";
                 sb.Append(strFileID[i]);
             }

             //strFileID = sb.ToString().Replace("''", "','"); 

             TransactionManager tran = new TransactionManager();
             tran.BeginTransaction();
             try
             {
                 SqlHelper.ExecuteNonQuery(tran.Trans, CommandType.Text, "DELETE FROM officedba.CultureDocs where ID IN ( " + strFileID + " ) ", null);
                 tran.Commit();
                 isSucc = true;
             }
             catch (Exception ex)
             {
                 tran.Rollback();
                 isSucc = false;
                 throw ex;
             }
             return isSucc;
        }


        #endregion  成员方法
    }
}

