using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace XBase.Model.Office.BasicData
{
    [Serializable]
   public class SteelTypeModel
    {
       private int _id;
       private string _companyCD;
       private string _typeName;
       private int _parentID;
       private string _usedStatus;
       private string _remark;
       private DateTime _ModifiedDate;

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

       public string TypeName
       {
           get { return _typeName; }
           set { _typeName = value; }
       }

       public int ParentID
       {
           get { return _parentID; }
           set { _parentID = value; }
       }

       public string UsedStatus
       {
           get
           {
               return _usedStatus;
           }
           set
           {
               _usedStatus = value;
           }
       }
       public string Remark
       {
           get { return _remark; }
           set { _remark = value; }
       }

       public DateTime ModifiedDate
       {
           get { return _ModifiedDate; }
           set { _ModifiedDate = value; }
       }

    }
}
