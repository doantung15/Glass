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
        public static bool[] b = new bool[16];
        public static int count = 0;

        public static List<string> Position = new List<string>();
        public static List<string> diachi = new List<string>();
        public static List<string> onho = new List<string>();
        public static StringBuilder ads = new StringBuilder();
        public static int m;
        public static List<string> thongtinkho = new List<string>();
        public static List<string> chiatungkho = new List<string>();

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
                    ads.Append("0");
                }
                else
                {
                    ads.Append("1");
                }
            }
            
            for (int j=0;j<=ads.Length;j+=45)
            {
                m++;
            }

            

        }

        
        

        public static void tachdiachi_onho()
        {

            for (int i = 0; i < Dulieu.zDataAll.Rows.Count; i++)
            {
                Position.Add(Dulieu.zDataAll.Rows[i][2].ToString());
            }

            for (int j = 0; j < Position.Count; j++)
            {
                diachi.Add(Convert.ToString(Position[j].Substring(1, 2)));
                onho.Add(Convert.ToString(Position[j].Substring(4, 2)));
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


        

        public static int Ma_hoa_16bit(bool[] cum)
        {
                        
            int result1 = 2 ^ 2;

            for (int i = 0; i < 16; i++)
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
