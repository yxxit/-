using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace XBase.Model.Office.BasicData
{
    [Serializable]
   public class BatchInfoModel
    {
       private int _id;
       private string _companyCD;
       private string _batchNo;
       private string _usedStatus;
       private string _remark;
       private DateTime _modifiedDate;


       public int ID
       {
           get { return _id; }
           set { _id = value; }
       }
       public string CompanyCD
       {
           get { return _companyCD; }
           set { _companyCD = value; }
       }
       public string UsedStatus
       {
           get { return _usedStatus; }
           set { _usedStatus = value; }
       }
       public string BatchNO
       {
           get { return _batchNo; }
           set { _batchNo = value; }
       }
       public string Remark
       {
           get { return _remark; }
           set { _remark = value; }
       }
       public DateTime ModifiedDate       
       {
           get { return _modifiedDate; }
           set { _modifiedDate = value; }
       }
    }
}
