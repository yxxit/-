using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using XBase.Common;
using System.Data;
using XBase.Data.DBHelper;
using XBase.Model.Office.StorageManager;
using System.Data.SqlClient;
using System.Collections;

namespace XBase.Data.Office.StorageManager
{
    public class DisassemblingDBHelper
    {
       public static UserInfoUtil userinfo = ((UserInfoUtil)SessionUtil.Session["UserInfo"]);
        #region 获取套件信息
        public static DataTable GetBOM(string ID)
        {
            string sql = @"select '套件' typename,0 typeid,b.id,b.unitid,b.prodno,b.productname,b.specification,c.codename,b.storageid,'1' quota,isnull(b.standardsell,0)standardsell from (select companycd,productid,unitid from officedba.Bom where id=" + ID + @") a left join  
            officedba.ProductInfo b on a.companycd=b.companycd and a.productid=b.id left join officedba.CodeUnitType c on a.unitid=c.id and a.companycd=c.companycd where a.companycd='" + userinfo.CompanyCD + "'";
            return SqlHelper.ExecuteSql(sql.ToString());
        }
        #endregion 

        #region 获取子健ID
        public static DataTable GetBOMID(string ID)
        {
            string sql = @"select id from officedba.bom where parentno="+ID+" and companycd='"+userinfo.CompanyCD+"'";
            return SqlHelper.ExecuteSql(sql.ToString());
        }
        #endregion 

        #region 获取子健信息
        public static DataTable GetBOMinfo(string ID)
        {
            string sql = @"select '散件' typename,1 typeid,b.id,b.unitid,b.prodno,b.productname,b.specification,c.codename,b.storageid,a.quota,isnull(b.standardsell,0)standardsell from (select boms.quota,bom.companycd,boms.productid,boms.unitid from officedba.Bom bom left join officedba.Bomdetail boms  on bom.bomno=boms.bomno and bom.companycd=boms.companycd where bom.id=" + ID + @") a left join  
            officedba.ProductInfo b on a.companycd=b.companycd and a.productid=b.id left join officedba.CodeUnitType c on a.unitid=c.id and a.companycd=c.companycd where a.companycd='" +userinfo.CompanyCD+"'";
            return SqlHelper.ExecuteSql(sql.ToString());
        }
        #endregion 

        #region 检查唯一性
        public static bool exsist(string billno)
        {
            string sql = @"select count(1) from officedba.Disassembling where companycd='" + userinfo.CompanyCD + "' and billno='"+billno+"'";
            DataTable dt= SqlHelper.ExecuteSql(sql.ToString());
            if (int.Parse(dt.Rows[0][0].ToString()) > 0)
                return false;
            else
                return true;
        }
        #endregion 


        /// <summary>
        /// 增加一条数据
        /// </summary>
        public static bool Add(DisassemblingModel model,List<DisassemblingListModel> list,out int  IndexIDentity)
        {
            string[] sql = new string[1 + list.Count];
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into officedba.Disassembling(");
            strSql.Append("creater,status,Updater,UpdateDate,BillType,Remark,companyCD,BillNo,BomID,CreatDate,TotalPrice,departmentID,HandsManID");
            strSql.Append(")");
            strSql.Append(" values (");
            strSql.Append("" + model.creater + ",");
           
            strSql.Append("" + '1' + ",");
            strSql.Append("'" + userinfo.UserID+ "',");
            strSql.Append("getdate(),");
            strSql.Append("" + model.BillType + ",");
            strSql.Append("'" + model.Remark + "',");
            strSql.Append("'" + model.companyCD + "',");
            strSql.Append("'" + model.BillNo + "',");
            strSql.Append("" + model.BomID + ",");
            strSql.Append("'" + model.CreatDate + "',");
            strSql.Append("" + model.TotalPrice + ",");
            strSql.Append("" + model.departmentID + ",");
            strSql.Append("" + model.HandsManID + "");
            strSql.Append(")");
            strSql.Append(";");
            sql[0] = strSql.ToString();
            for (int i = 0; i < list.Count; i++)
            {
                DisassemblingListModel model1=list[i];
                StringBuilder strSql1=new StringBuilder();
			    strSql1.Append("insert into officedba.DisassemblingDetails(");
			    strSql1.Append("Quota,Batch,UsedCount,companyCD,BillsNo,Types,Storageid,Productid,UnitID,Price,Amount");
			    strSql1.Append(")");
			    strSql1.Append(" values (");
			    strSql1.Append(""+model1.Quota+",");
			    strSql1.Append("'"+model1.Batch+"',");
			    strSql1.Append(""+model1.UsedCount+",");
			    strSql1.Append("'"+model1.companyCD+"',");
			    strSql1.Append("'"+model.BillNo+"',");
			    strSql1.Append(""+model1.Types+",");
			    strSql1.Append(""+model1.Storageid+",");
			    strSql1.Append(""+model1.Productid+",");
			    strSql1.Append(""+model1.UnitID+",");
			    strSql1.Append(""+model1.Price+",");
			    strSql1.Append(""+model1.Amount+"");
			    strSql1.Append(")");
			    strSql1.Append(";");
                sql[i + 1] = strSql1.ToString();
            }
            bool result = SqlHelper.ExecuteTransForListWithSQL(sql);
            if (result)
            {
                string getid = "select isnull(max(id),0) from officedba.Disassembling ";
                DataTable dt = SqlHelper.ExecuteSql(getid);
                IndexIDentity = int.Parse(dt.Rows[0][0].ToString());
            }
            else
            {
                IndexIDentity = 0;
            }
            return result;
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public static bool Update(DisassemblingModel model,List<DisassemblingListModel> list)
        {
            string[] sql = new string[2 + list.Count];
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update officedba.Disassembling set ");
            strSql.Append("creater=" + model.creater + ",");
            strSql.Append("Updater='" + userinfo.UserID + "',");
            strSql.Append("UpdateDate=getdate(),");
            strSql.Append("BillType=" + model.BillType + ",");
            strSql.Append("Remark='" + model.Remark + "',");
            strSql.Append("companyCD='" + model.companyCD + "',");
            strSql.Append("BillNo='" + model.BillNo + "',");
            strSql.Append("BomID=" + model.BomID + ",");
            strSql.Append("CreatDate='" + model.CreatDate + "',");
            strSql.Append("TotalPrice=" + model.TotalPrice + ",");
            strSql.Append("departmentID=" + model.departmentID + ",");
            strSql.Append("HandsManID=" + model.HandsManID + "");
            strSql.Append(" where ID=" + model.ID + " ");
            sql[0] = strSql.ToString();
            //删除明细
            sql[1] = "delete from officedba.DisassemblingDetails where billsno='"+model.BillNo+"' and companycd='"+userinfo.CompanyCD+"'";
            for (int i = 0; i < list.Count; i++)
            {
                DisassemblingListModel model1 = list[i];
                StringBuilder strSql1 = new StringBuilder();
                strSql1.Append("insert into officedba.DisassemblingDetails(");
                strSql1.Append("Quota,Batch,UsedCount,companyCD,BillsNo,Types,Storageid,Productid,UnitID,Price,Amount");
                strSql1.Append(")");
                strSql1.Append(" values (");
                strSql1.Append("" + model1.Quota + ",");
                strSql1.Append("'" + model1.Batch + "',");
                strSql1.Append("" + model1.UsedCount + ",");
                strSql1.Append("'" + model1.companyCD + "',");
                strSql1.Append("'" + model1.BillsNo + "',");
                strSql1.Append("" + model1.Types + ",");
                strSql1.Append("" + model1.Storageid + ",");
                strSql1.Append("" + model1.Productid + ",");
                strSql1.Append("" + model1.UnitID + ",");
                strSql1.Append("" + model1.Price + ",");
                strSql1.Append("" + model1.Amount + "");
                strSql1.Append(")");
                strSql1.Append(";");
                sql[i + 2] = strSql1.ToString();
            }
            bool result = SqlHelper.ExecuteTransForListWithSQL(sql);
            return result;
        }
        #region 判断物品库存
        public static bool ISConfirmBill(DisassemblingModel model,bool type, out string proname)
        {
            string billtype = "";
            if (type)
            {
                billtype = (model.BillType - 1).ToString();
            }
            else
            {
                if ((model.BillType - 1) == 0)
                {
                    billtype = "1";
                }
                else
                {
                    billtype = "0";
                }
            }
            string sql = @"select c.productcount-d.usedcount Pcount,d.productname from officedba.StorageProduct c right join 
(select a.companycd,b.storageid,b.productid,b.batch,b.UsedCount,e.productname from officedba.Disassembling a left join officedba.DisassemblingDetails b on
a.billno=b.billsno and a.companycd=b.companycd left join officedba.ProductInfo e on e.companycd=b.companycd and e.id=b.productid where a.id=" + model.ID + @" and types=" + billtype + @" and a.companycd='" + model.companyCD + @"') d on 
c.companycd=d.companycd and c.storageid=d.storageid and c.productid=d.productid and isnull(c.batchno,'')=isnull(d.batch,'') ";
            DataTable dt = SqlHelper.ExecuteSql(sql);
            bool result=true;
            proname = "";
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    if (decimal.Parse(dt.Rows[i]["Pcount"].ToString()) <= 0)
                    {
                        proname += "," + dt.Rows[i]["productname"].ToString();
                        if (result)
                        {
                            result = false;
                        }
                    }
                }
            }
            else
            {
                result = false;
            }
            if (proname.Length > 0)
            {
                proname = proname.Substring(1, proname.Length-1);
            }
            return result;
        }
        #endregion

        #region 确认单据
        public static bool ConfirmBill(DisassemblingModel model, out string Msg)
        {
            ArrayList lstConfirm = new ArrayList();
            SqlCommand commSA1 = new SqlCommand();
            string sql1 = @"select d.productid,d.batch,d.billsno,d.price,d.StorageID,d.usedcount Pcount,d.types,c.id from officedba.StorageProduct c right join 
(select b.billsno,b.price,b.types,a.companycd,b.storageid,b.productid,b.batch,b.UsedCount,e.productname from officedba.Disassembling a left join officedba.DisassemblingDetails b on
a.billno=b.billsno and a.companycd=b.companycd left join officedba.ProductInfo e on e.companycd=b.companycd and e.id=b.productid where a.id=" + model.ID + @"  and a.companycd='" + model.companyCD + @"') d on 
c.companycd=d.companycd and c.storageid=d.storageid and c.productid=d.productid and isnull(c.batchno,'')=d.batch ";
            DataTable dt = SqlHelper.ExecuteSql(sql1);

            commSA1.CommandText = "update officedba.Disassembling set confirmer=" + userinfo.EmployeeID + ", confirmdate=getdate(),status=2 where id=" + model.ID;
            lstConfirm.Add(commSA1);
            bool result = false;
            
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {

                  
                    StorageAccountModel StorageAccountM = new StorageAccountModel();
                    StorageAccountM.CompanyCD = userinfo.CompanyCD;
                    
                    if (dt.Rows[i]["batch"].ToString() != "")
                    {
                      
                        StorageAccountM.BatchNo = dt.Rows[i]["batch"].ToString();
                    }
                    StorageAccountM.BillNo = dt.Rows[i]["billsno"].ToString();
                    StorageAccountM.Price = Convert.ToDecimal(dt.Rows[i]["price"].ToString());
                    StorageAccountM.HappenDate = Convert.ToDateTime(DateTime.Now);
                    StorageAccountM.Creator = userinfo.EmployeeID;
                    StorageAccountM.PageUrl = "../Office/StorageManager/Disassembling.aspx";
                    StorageAccountM.ReMark ="";

                    if (dt.Rows[i]["productid"].ToString() != "")
                    {

                        StorageAccountM.ProductID = Convert.ToInt32(dt.Rows[i]["productid"].ToString());
                    }
                    if (dt.Rows[i]["StorageID"].ToString() != "")
                    {
                        
                        StorageAccountM.StorageID = Convert.ToInt32(dt.Rows[i]["StorageID"].ToString());
                    }
                    if (dt.Rows[i]["Pcount"].ToString() != "")
                    {
                        
                        StorageAccountM.HappenCount = Convert.ToDecimal(dt.Rows[i]["Pcount"].ToString());
                        StorageAccountM.ProductCount = Convert.ToDecimal(dt.Rows[i]["Pcount"].ToString());
                    }
                    if(model.BillType==1)
                    {
                        if (dt.Rows[i]["types"].ToString() == "0")
                        {
                            StorageAccountM.BillType = 24;
                            SqlCommand commSA = new SqlCommand();
                            commSA = StorageAccountDBHelper.InsertStorageAccountCommand(StorageAccountM, "1");
                            lstConfirm.Add(commSA);
                            SqlCommand commSA2 = new SqlCommand();
                            commSA2.CommandText = "update officedba.StorageProduct set productcount=isnull(productcount,0)-" + dt.Rows[i]["Pcount"].ToString() + " where id=" + dt.Rows[i]["id"].ToString();
                            lstConfirm.Add(commSA2);
                        }
                        else
                        {
                            StorageAccountM.BillType = 23;
                            SqlCommand commSA = new SqlCommand();
                            commSA = StorageAccountDBHelper.InsertStorageAccountCommand(StorageAccountM, "0");
                            lstConfirm.Add(commSA);
                            SqlCommand commSA2 = new SqlCommand();
                            commSA2.CommandText = "update officedba.StorageProduct set productcount=isnull(productcount,0)+" + dt.Rows[i]["Pcount"].ToString() + " where id=" + dt.Rows[i]["id"].ToString();
                            lstConfirm.Add(commSA2);
                        }
                    }else
                    {
                        if (dt.Rows[i]["types"].ToString() == "0")
                        {
                            StorageAccountM.BillType = 23;
                            SqlCommand commSA = new SqlCommand();
                            commSA = StorageAccountDBHelper.InsertStorageAccountCommand(StorageAccountM, "0");
                            lstConfirm.Add(commSA);
                            SqlCommand commSA2 = new SqlCommand();
                            commSA2.CommandText = "update officedba.StorageProduct set productcount=isnull(productcount,0)+" + dt.Rows[i]["Pcount"].ToString() + " where id=" + dt.Rows[i]["id"].ToString();
                            lstConfirm.Add(commSA2);
                        }
                        else
                        {
                            StorageAccountM.BillType = 24;
                            SqlCommand commSA = new SqlCommand();
                            commSA = StorageAccountDBHelper.InsertStorageAccountCommand(StorageAccountM, "1");
                            lstConfirm.Add(commSA);
                            SqlCommand commSA2 = new SqlCommand();
                            commSA2.CommandText = "update officedba.StorageProduct set productcount=isnull(productcount,0)-" + dt.Rows[i]["Pcount"].ToString() + " where id=" + dt.Rows[i]["id"].ToString();
                            lstConfirm.Add(commSA2);
                        }
                    }
                }
            }
            //更新分仓量表
            result = SqlHelper.ExecuteTransWithArrayList(lstConfirm);
            if (result)
            {
                Msg = "确认成功";
            }
            else
            {
                Msg = "确认失败";
            }
            return result;
        }
        #endregion

        #region 取消确认单据
        public static bool UnConfirmBill(DisassemblingModel model, out string Msg)
        {
            ArrayList lstConfirm = new ArrayList();
            SqlCommand commSA1 = new SqlCommand();
            string sql1 = @"select d.productid,d.batch,d.billsno,d.price,d.StorageID,d.usedcount Pcount,d.types,c.id from officedba.StorageProduct c right join 
(select b.billsno,b.price,b.types,a.companycd,b.storageid,b.productid,b.batch,b.UsedCount,e.productname from officedba.Disassembling a left join officedba.DisassemblingDetails b on
a.billno=b.billsno and a.companycd=b.companycd left join officedba.ProductInfo e on e.companycd=b.companycd and e.id=b.productid where a.id=" + model.ID + @"  and a.companycd='" + model.companyCD + @"') d on 
c.companycd=d.companycd and c.storageid=d.storageid and c.productid=d.productid and isnull(c.batchno,'')=d.batch ";
            DataTable dt = SqlHelper.ExecuteSql(sql1);

            commSA1.CommandText = "update officedba.Disassembling set confirmer=null, confirmdate=null,status=1 where id=" + model.ID;
            lstConfirm.Add(commSA1);
            bool result = false;

            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {


                   
                    if (model.BillType == 1)
                    {
                        if (dt.Rows[i]["types"].ToString() == "0")
                        {
                            //StorageAccountM.BillType = 24;
                            SqlCommand commSA = new SqlCommand();
                            commSA.CommandText = "delete  officedba.StorageAccount where companycd='" + ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD + "' and billtype=24 and billno='" + dt.Rows[i]["billsno"].ToString() +"'";
                            lstConfirm.Add(commSA);
                            SqlCommand commSA2 = new SqlCommand();
                            commSA2.CommandText = "update officedba.StorageProduct set productcount=isnull(productcount,0)+" + dt.Rows[i]["Pcount"].ToString() + " where id=" + dt.Rows[i]["id"].ToString();
                            lstConfirm.Add(commSA2);
                        }
                        else
                        {
                            //StorageAccountM.BillType = 23;
                            SqlCommand commSA = new SqlCommand();
                            commSA.CommandText = "delete  officedba.StorageAccount where companycd='" + ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD + "' and billtype=23 and billno='" + dt.Rows[i]["billsno"].ToString() + "'";
                            lstConfirm.Add(commSA);
                            SqlCommand commSA2 = new SqlCommand();
                            commSA2.CommandText = "update officedba.StorageProduct set productcount=isnull(productcount,0)-" + dt.Rows[i]["Pcount"].ToString() + " where id=" + dt.Rows[i]["id"].ToString();
                            lstConfirm.Add(commSA2);
                        }
                    }
                    else
                    {
                        if (dt.Rows[i]["types"].ToString() == "0")
                        {
                            //StorageAccountM.BillType = 23;
                            SqlCommand commSA = new SqlCommand();
                            commSA.CommandText = "delete  officedba.StorageAccount where companycd='" + ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD + "' and billtype=23 and billno='" + dt.Rows[i]["billsno"].ToString() + "'";
                            lstConfirm.Add(commSA);
                            SqlCommand commSA2 = new SqlCommand();
                            commSA2.CommandText = "update officedba.StorageProduct set productcount=isnull(productcount,0)-" + dt.Rows[i]["Pcount"].ToString() + " where id=" + dt.Rows[i]["id"].ToString();
                            lstConfirm.Add(commSA2);
                        }
                        else
                        {
                            //StorageAccountM.BillType = 24;
                            SqlCommand commSA = new SqlCommand();
                            commSA.CommandText = "delete  officedba.StorageAccount where companycd='" + ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD + "' and billtype=24 and billno='" + dt.Rows[i]["billsno"].ToString() + "'";
                            lstConfirm.Add(commSA);
                            SqlCommand commSA2 = new SqlCommand();
                            commSA2.CommandText = "update officedba.StorageProduct set productcount=isnull(productcount,0)+" + dt.Rows[i]["Pcount"].ToString() + " where id=" + dt.Rows[i]["id"].ToString();
                            lstConfirm.Add(commSA2);
                        }
                    }
                }
            }
            //更新分仓量表
            result = SqlHelper.ExecuteTransWithArrayList(lstConfirm);
            if (result)
            {
                Msg = "取消确认成功";
            }
            else
            {
                Msg = "取消确认失败";
            }
            return result;
        }
        #endregion

        #region 根据ID获取单据
        public static DataTable GetBillByID(string ID)
        {
            string sql = @"select e.specification,e.prodno,isnull(convert(varchar(10),a.CloseDate,120),'')CloseDate,isnull(dbo.getEmployeeName(a.Confirmer),'')Confirmer,a.status,a.id,a.companycd,a.billno,a.bomid,g.productname Tproductname,a.departmentid,c.deptname,isnull(convert(numeric(12," + userinfo.SelPoint + @"),a.totalprice),0)totalprice,isnull(dbo.getEmployeeName(a.handsmanid),'') handsman,a.handsmanid,
a.remark,isnull(a.creater,0)creater,isnull(dbo.getEmployeeName(a.creater),'')creatname,isnull(convert(varchar(10),a.ConfirmDate,120),'')ConfirmDate,isnull(dbo.getEmployeeName(a.Closer),'')Closername,isnull(a.Closer,0)Closer,
isnull(convert(varchar(10),a.UpdateDate,120),'')UpdateDate,b.Types,case when b.types=0 then '套件' else '散件' end typesname,b.Storageid,e.productname,b.productid,b.unitid,h.codename,isnull(convert(varchar(10),a.CreatDate,120),'')CreatDate,
isnull(convert(numeric(12," +userinfo.SelPoint+@"),b.price),0)price,isnull(convert(numeric(12,"+userinfo.SelPoint+@"),b.amount),0)amount,isnull(convert(numeric(12,"+userinfo.SelPoint+@"),b.quota),0)quota,isnull(convert(numeric(12,"+userinfo.SelPoint+@"),b.usedcount),0)usedcount,b.batch,a.Updater
from officedba.Disassembling a left join officedba.DisassemblingDetails b on
a.billno=b.billsno and a.companycd=b.companycd left join officedba.ProductInfo e on e.companycd=b.companycd and e.id=b.productid 
left join officedba.bom f on f.companycd=a.companycd and f.id=a.bomid left join officedba.ProductInfo g on g.companycd=f.companycd and g.id=f.productid 
left join officedba.DeptInfo c on a.departmentid=c.id left join officedba.CodeUnitType h on h.id=b.unitid and h.companycd=b.companycd where a.id="+ID;
            return SqlHelper.ExecuteSql(sql.ToString());
        }
        #endregion 

        
        #region 结单
        public static bool CloseBill(string id)
        {
            string sql = @"update officedba.Disassembling set status=3,Closer=" + userinfo.EmployeeID + ",CloseDate=getdate(),Updater='" + userinfo.UserID + "',UpdateDate=getdate() where id="+id;
            return SqlHelper.ExecuteWithSQL(sql);
        }
        #endregion

        #region 取消结单
        public static bool CancelCloseBill(string id)
        {
            string sql = @"update officedba.Disassembling set status=2,Closer=null,CloseDate=null,Updater='" + userinfo.UserID + "',UpdateDate=getdate() where id="+id;
            return SqlHelper.ExecuteWithSQL(sql);
        }
        #endregion


        #region 是否能被删除
        public static bool isdel(string id)
        {
            string sql = @"select count(1) from officedba.Disassembling where status>1 and id="+id;
            DataTable dt = SqlHelper.ExecuteSql(sql.ToString());
            if (int.Parse(dt.Rows[0][0].ToString()) > 0)
                return false;
            else
                return true;
        }
        #endregion 

        #region 删除：
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="ID"></param>
        /// <param name="CompanyCD"></param>
        /// <returns></returns>
        public static bool DeleteIt(string ID)
        {
            string[] sql = new string[2];
            sql[0] = "delete from officedba.DisassemblingDetails where billsno=(select billno from officedba.Disassembling where ID=" + ID + ") and CompanyCD='" + userinfo.CompanyCD + "'";
            sql[1] = "delete from  officedba.Disassembling where   ID=" + ID;

            return SqlHelper.ExecuteTransForListWithSQL(sql);
        }
        #endregion

        #region 根据单据编号获取单据信息
        public static DataTable GetBillByNo(string No)
        {
            string companycd = userinfo.CompanyCD;
            string Sql="select a.id,a.companycd,a.billno,a.bomid,g.productname Tproductname,a.departmentid,c.deptname,isnull(convert(varchar(10),a.CloseDate,120),'')CloseDate," ;
            Sql+=" isnull(dbo.getEmployeeName(a.Confirmer),'')Confirmer,";
            Sql+=" a.status,isnull(convert(numeric(12," + userinfo.SelPoint + "),a.totalprice),0)totalprice,";
            Sql+=" isnull(dbo.getEmployeeName(a.handsmanid),'') handsman,a.handsmanid,";
            Sql+=" a.remark,isnull(a.creater,0)creater,isnull(dbo.getEmployeeName(a.creater),'')creatname,";
            Sql+=" isnull(convert(varchar(10),a.CreatDate,120),'')CreatDate,";
            Sql+=" isnull(convert(varchar(10),a.ConfirmDate,120),'')ConfirmDate,";
            Sql+=" isnull(dbo.getEmployeeName(a.Closer),'')Closername,isnull(a.Closer,0)Closer,";
            Sql+=" isnull(convert(varchar(10),a.UpdateDate,120),'')UpdateDate,";
            Sql+="  b.Types,case when b.types=0 then '套件' else '散件' end typesname,";
            Sql+=" b.Storageid,si.StorageName,e.prodno,e.productname, e.specification,b.productid,b.unitid,h.codename,";
            Sql+=" isnull(convert(numeric(12," + userinfo.SelPoint + "),b.price),0)price,";
            Sql+=" isnull(convert(numeric(12," + userinfo.SelPoint + "),b.amount),0)amount,";
            Sql+=" isnull(convert(numeric(12," + userinfo.SelPoint + "),b.quota),0)quota,isnull(convert(numeric(12," + userinfo.SelPoint + "),b.usedcount),0)usedcount,b.batch,a.Updater, ";
            Sql += " isnull(convert(numeric(12," + userinfo.SelPoint + "),(select sum(amount) as totalprice from officedba.DisassemblingDetails where CompanyCD='" + companycd + "' and billsno='" + No + "')),0) as b_totalPrice,";
            Sql += " isnull(convert(numeric(12," + userinfo.SelPoint + "),(select sum(usedcount) as totalcount from officedba.DisassemblingDetails where CompanyCD='" + companycd + "' and billsno='" + No + "')),0)  as b_totalCount";
            Sql+=" from officedba.Disassembling a ";
            Sql+=" left join officedba.DisassemblingDetails b on a.billno=b.billsno and a.companycd=b.companycd ";
            Sql+=" left join officedba.ProductInfo e on e.companycd=b.companycd and e.id=b.productid ";
            Sql+=" left join officedba.bom f on f.companycd=a.companycd and f.id=a.bomid ";
            Sql+=" left join officedba.ProductInfo g on g.companycd=f.companycd and g.id=f.productid ";
            Sql+=" left join officedba.DeptInfo c on a.departmentid=c.id ";
            Sql+=" left join officedba.CodeUnitType h on h.id=b.unitid and h.companycd=b.companycd ";
            Sql+=" left join officedba.StorageInfo si on si.ID=b.storageid";
            Sql += " where a.CompanyCD='" + companycd + "' and a.BillNo='" + No + "'";

            return SqlHelper.ExecuteSql(Sql.ToString());
        }
        #endregion

    }
}
