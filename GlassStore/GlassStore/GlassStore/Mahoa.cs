using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GlassStore
{
    class Mahoa
    {

        public static bool[] a = new bool[48];
        public static bool[] b = new bool[8];
        public static int count = 0;

        public static Int64 BiaWia;

        public static bool IsNumber(string pValue)
        {
            foreach (Char c in pValue)
            {
                if (!Char.IsDigit(c))
                    return false;
            }
            return true;
        }

        public static void addbit()
        {
            for (int i = 0; i < Dulieu.zDataAll.Rows.Count; i++)
            {
                if (string.IsNullOrEmpty(Convert.ToString(Dulieu.zDataAll.Rows[i][1])))
                {
                    BiaWia |= 0 << i;
                }
                else
                {
                    BiaWia |= 1 << i;
                }
            }

        }

        public static void tachdiachi_onho()
        {
            for(int a=0; a<Dulieu.zDataAll.Rows.Count;a++)
            {
              // Dulieu.zDataAll.Rows[i][2]= 
            }
        }


       

        public static bool[] Phanhoa()
        {
            while(count<a.Length)
            {
                if (a.Length - count < b.Length)
                {
                    b = new bool[a.Length - count];
                }
                for(int i = 0; i < b.Length; count++, i++)
                {
                    b[i] = a[count];
                }
            }
            return b;
        }


        

        public static int Ma_hoa_8bit(bool[] cum)
        {
                        
            int result1 = 2 ^ 2;

            for (int i = 0; i < 8; i++)
            {
                result1 = result1 + (int)Math.Pow(2, i)*Convert.ToInt32(cum[i]);
            }


            return result1;
        }

        

       

        public static void Search_led_location(string address, string led_local)
        {
            if(IsNumber(led_local)==true)
            {

            }
        }

    }
}
