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

       public static void Bitwise()
        {          
            
            for(int i=0;i<Dulieu.diachi.Count;i++)
            {
                if(Dulieu.diachi[i]!=Dulieu.diachi[i+1])
                {
                    break;
                }
                else
                {
                    for(int k=0;k<Dulieu.onho.Count;k++)
                    {
                        a[Convert.ToInt16(Dulieu.onho[k])] = true;
                    }                   
                }
            }

            for(int j=0;j<Dulieu.diachiNull.Count;j++)
            {
                if (Dulieu.diachiNull[j] != Dulieu.diachiNull[j + 1])
                {
                    break;
                }
                else
                {
                    for(int m=0;m<Dulieu.onhoNull.Count;m++)
                    {
                        a[Convert.ToInt16(Dulieu.onhoNull[m])] = false;
                    }
                }
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
