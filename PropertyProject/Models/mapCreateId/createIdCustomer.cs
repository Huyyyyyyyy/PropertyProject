using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PropertyProject.Models.mapCreateId
{
    public class createIdCustomer
    {
        public string createIdForCustomer()
        {
            using (PropertyProjectEntities db = new PropertyProjectEntities())
            {
                string ma1 = "NN";
                string s = "";

                string maxId = db.Customers.Max(c => c.idCustomer);

                if (string.IsNullOrEmpty(maxId))
                {
                    s = ma1 + "001";
                }
                else
                {
                    int k = Convert.ToInt32(maxId.Substring(2)) + 1;
                    s = $"{ma1}{k:D3}";
                }

                return s;
            }
        }


        public string createIntroduceIdForCustomer()
        {
            using (PropertyProjectEntities db = new PropertyProjectEntities())
            {
                string ma1 = "NN";
                string s = "";

                string maxId = db.Customers.Max(c => c.idCustomer);

                if (string.IsNullOrEmpty(maxId))
                {
                    s = ma1 + "0000001";
                }
                else
                {
                    int k = Convert.ToInt32(maxId.Substring(2)) + 1;
                    s = $"{ma1}{k:D7}";
                }

                return s;
            }
        }
    }
}