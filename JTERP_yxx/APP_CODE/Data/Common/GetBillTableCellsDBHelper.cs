using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using XBase.Common;
using XBase.Data.DBHelper;
using System.Collections;
using System.Data.SqlClient;

namespace XBase.Data.Common
{
    public class GetBillTableCellsDBHelper
    {
        //获取扩展项
        public static DataTable GetAllCustom(string TabName)
        {
            string sql = "select ID,FunctionModule,ModelNo,TabName,EFIndex,EFDesc,EFType from officedba.TableCells where companycd='" + ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD + "' and TabName='" + TabName + "' order by EFIndex";
            return SqlHelper.ExecuteSql(sql);
        }
        
        //获取单据明细扩展项对应物品扩展项的索引
        public static string GetSingleCustom(string TabName)
        {
            string str = "";
            string sql = "select ID,FunctionModule,ModelNo,TabName,EFIndex,EFDesc,EFType,PEFIndex from officedba.TableCells where companycd='" + ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD + "' and TabName='" + TabName + "' and PEFIndex>0";
            DataTable dt= SqlHelper.ExecuteSql(sql);
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    if (i == dt.Rows.Count - 1)
                        str += dt.Rows[i]["EFIndex"].ToString() + "|" + dt.Rows[i]["PEFIndex"].ToString() ;
                    else
                        str += dt.Rows[i]["EFIndex"].ToString() + "|" + dt.Rows[i]["PEFIndex"].ToString() + ",";
                }
            }
            return str;
        }
        //获取用户自定义显示数据
        public static DataTable GetAllField(string moduleid)
        {
            string sql = "select moduleid,fieldnum,modulartype,isdisplay,isnull(isenable,0)IsEnable from officedba.Pagealignment where moduleid="+moduleid+" and companycd='" + ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD + "'";
            return SqlHelper.ExecuteSql(sql);
        }
        //添加字段
        public static string insertCustomFilder(string[] strCustomdetail,int rows)
        {
            if (int.Parse(strCustomdetail[0]) == 0)
            {
                return "";
            }
            else
            {
                string all = strCustomdetail[1];
                string[] stronline = all.Split('|');
                string[] str=stronline[rows].Split(',');
                string Customs = "";
                for (int i = 0; i < str.Length; i++)
			    {
                    Customs += ",Custom" + (i + 1).ToString();
			    }
                return Customs;
            }
        }
        //添加字段值
        public static string insertCustomvalue(string[] strCustomdetail,int rows)
        {
            if (int.Parse(strCustomdetail[0]) == 0)
            {
                return "";
            }
            else
            {
                string all = strCustomdetail[1];
                string[] stronline = all.Split('|');
                string[] str = stronline[rows].Split(',');
                string Customs = "";
                for (int i = 0; i < str.Length; i++)
                {
                    //if(str[i]=="")
                    //{
                    //    str[i]="''";
                    //}
                    Customs += ",'" + str[i]+"'";
                }
                return Customs;
            }
        }
        //修改sql语句
        //public static string updateCustomvalue(string[] strCustomdetail, int rows)
        //{
        //    if (int.Parse(strCustomdetail[0]) == 0)
        //    {
        //        return "";
        //    }
        //    else
        //    {
        //        string all = strCustomdetail[1];
        //        string[] stronline = all.Split('|');
        //        string[] str = stronline[rows].Split(',');
        //        string Customs = "";
        //        for (int i = 0; i < str.Length; i++)
        //        {
        //            //if(str[i]=="")
        //            //{
        //            //    str[i]="''";
        //            //}
        //            Customs += ",'" + str[i] + "'";
        //        }
        //        return Customs;
        //    }
        //}
        //保存页面设置
        public static bool InsertPageSetUp(string jinbenxinxi, string dingdandetial, string feiyongxinxi, string hejixinxi, string beizuxinxi, string danjuzhuangtai, string isdis, string moduleid,string isenable)
        {
            ArrayList list=new ArrayList();
            SqlCommand del=new SqlCommand();
            //先删除
            del.CommandText="delete from officedba.Pagealignment where companycd='"+((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD +"' and ModuleId="+moduleid;
            list.Add(del);
            string[] dispaly = isdis.Split(',');
            string[] isable = isenable.Split(',');
            //基本信息
            if (jinbenxinxi != "")
            {
                SqlCommand jiben = new SqlCommand();
                jiben.CommandText = "insert into officedba.Pagealignment (CompanyCD,ModuleId,FieldNum,ModularType,IsDisplay,IsEnable) values('" + ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD + "'," + moduleid + ",'" + jinbenxinxi + "',0," + dispaly[0] + ","+isable[0]+")";
                list.Add(jiben);
            }
            //明细信息
            if (dingdandetial != "")
            {
                SqlCommand mingxi = new SqlCommand();
                mingxi.CommandText = "insert into officedba.Pagealignment (CompanyCD,ModuleId,FieldNum,ModularType,IsDisplay,IsEnable) values('" + ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD + "'," + moduleid + ",'" + dingdandetial + "',1," + dispaly[1] + ","+isable[1]+")";
                list.Add(mingxi);
            }
            //费用明细
            if (feiyongxinxi != "")
            {
                SqlCommand feiyong = new SqlCommand();
                feiyong.CommandText = "insert into officedba.Pagealignment (CompanyCD,ModuleId,FieldNum,ModularType,IsDisplay,IsEnable) values('" + ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD + "'," + moduleid + ",'" + feiyongxinxi + "',2," + dispaly[2] + "," + isable[2] + ")";
                list.Add(feiyong);
            }
            //合计信息
            if (hejixinxi != "")
            {
                SqlCommand heji = new SqlCommand();
                heji.CommandText = "insert into officedba.Pagealignment (CompanyCD,ModuleId,FieldNum,ModularType,IsDisplay,IsEnable) values('" + ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD + "'," + moduleid + ",'" + hejixinxi + "',3," + dispaly[3] + "," + isable[3] + ")";
                list.Add(heji);
            }
            //备注信息
            if (beizuxinxi != "")
            {
                SqlCommand remark = new SqlCommand();
                remark.CommandText = "insert into officedba.Pagealignment (CompanyCD,ModuleId,FieldNum,ModularType,IsDisplay,IsEnable) values('" + ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD + "'," + moduleid + ",'" + beizuxinxi + "',4," + dispaly[4] + "," + isable[4] + ")";
                list.Add(remark);
            }
            //单据状态
            if (danjuzhuangtai != "")
            {
                SqlCommand type = new SqlCommand();
                type.CommandText = "insert into officedba.Pagealignment (CompanyCD,ModuleId,FieldNum,ModularType,IsDisplay,IsEnable) values('" + ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD + "'," + moduleid + ",'" + danjuzhuangtai + "',5," + dispaly[5] + "," + isable[5] + ")";
                list.Add(type);
            }
           
            return SqlHelper.ExecuteTransWithArrayList(list);
        }
        public static bool ClearPageSetUp(string moduleid)
        {
            ArrayList list = new ArrayList();
            SqlCommand del = new SqlCommand();
            del.CommandText = "delete from officedba.Pagealignment where companycd='" + ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD + "' and ModuleId=" + moduleid;
            list.Add(del);
            return SqlHelper.ExecuteTransWithArrayList(list);
        }
        //datatable 类型转换:System.Decimal,System.Double>>String
        public static DataTable TypeChange(DataTable data)
        {
            DataTable dt = data.Clone();
            //string[] list=str.Split(',');
            //修改类型
            foreach (DataColumn col in dt.Columns)
            {
                //for (int i = 0; i < list.Length; i++)
                //{
                if (col.DataType.ToString() == "System.Decimal" || col.DataType.ToString() == "System.Double")
                {
                    col.DataType = typeof(String);
                }
                //}
            }
            for (int i = 0; i < data.Rows.Count; i++)
            {
                DataRow row = dt.NewRow();
                for (int j = 0; j < data.Columns.Count; j++)
                {
                    if (data.Columns[j].DataType.ToString() == "System.Decimal" || data.Columns[j].DataType.ToString() == "System.Double")
                    {
                        if (data.Rows[i][j].ToString().LastIndexOf('0') == -1)
                        { 
                            row[j] = data.Rows[i][j];
                        }
                        else
                        {
                             string[] str = data.Rows[i][j].ToString().Split('0');
                             int count = -1;
                             for (int k = str.Length-1; k >= 0; k--)
                             {
                                 if (str[k].Length>0)
                                 {
                                     count = k;
                                 }
                                 if (count > -1)
                                 {
                                     break;
                                 }
                             }
                             string val = "";
                             for (int k = 0; k <= count; k++)
                             {
                                 if (k == count)
                                 {
                                     val += str[k];
                                 }
                                 else
                                 {
                                     val += str[k] + "0";
                                 }
                             }
                             if (val.LastIndexOf('.') != -1 && val.LastIndexOf('.')==val.Length-1)
                             {
                                 val = val.Substring(0, val.Length - 1);
                             }
                             row[j] = val;
                        }
                    }
                    else
                    {
                        row[j] = data.Rows[i][j];
                    }
                }
                dt.Rows.Add(row);
            }
            return dt;

        }
    }
    
}
