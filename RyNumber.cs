using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RahimyGroup
{
    public class RyNumber
    {
        /***** Persian Version *****/
        private static string R1_FA(int iVal)
        {
            switch (iVal)
            {
                case 1:
                    return "یک ";
                    break;
                case 2:
                    return "دو ";
                    break;
                case 3:
                    return "سه ";
                    break;
                case 4:
                    return "چهار ";
                    break;
                case 5:
                    return "پنج ";
                    break;
                case 6:
                    return "شش ";
                    break;
                case 7:
                    return "هفت ";
                    break;
                case 8:
                    return "هشت ";
                    break;
                case 9:
                    return "نه ";
                    break;
                default:
                    return "";
            }
        }

        private static string R2_FA(int iVal)
        {
            int N1 = 0;
            int N10 = 0;
            if (iVal != 10)
            {
                N1 = iVal % 10;
                N10 = iVal - N1;
            }
            switch (iVal)
            {
                case 10:
                    return "ده ";
                    break;
                case 11:
                    return "یازده ";
                    break;
                case 12:
                    return "دوازده ";
                    break;
                case 13:
                    return "سیزده ";
                    break;
                case 14:
                    return "چهارده ";
                    break;
                case 15:
                    return "پانزده ";
                    break;
                case 16:
                    return "شانزده ";
                    break;
                case 17:
                    return "هفده ";
                    break;
                case 18:
                    return "هجده ";
                    break;
                case 19:
                    return "نوزده ";
                    break;
                case 20:
                    return "بیست ";
                    break;
                case 30:
                    return "سی ";
                    break;
                case 40:
                    return "چهل ";
                    break;
                case 50:
                    return "پنجاه ";
                    break;
                case 60:
                    return "شصت ";
                    break;
                case 70:
                    return "هفتاد ";
                    break;
                case 80:
                    return "هشتاد ";
                    break;
                case 90:
                    return "نود ";
                    break;
                default:
                    return (R2_FA(N10) + " و " + R1_FA(N1));
            }
        }

        private static string R3_FA(int iVal)
        {
            int N10 = 0;
            int N100 = 0;
            N10 = iVal % 100;
            N100 = iVal - N10;
            switch (iVal)
            {
                case 100:
                    return "یکصد ";
                    break;
                case 200:
                    return "دویست ";
                    break;
                case 300:
                    return "سیصد ";
                    break;
                case 400:
                    return "چهارصد ";
                    break;
                case 500:
                    return "پانصد ";
                    break;
                case 600:
                    return "ششصد ";
                    break;
                case 700:
                    return "هفتصد ";
                    break;
                case 800:
                    return "هشتصد ";
                    break;
                case 900:
                    return "نهصد ";
                    break;
                default:
                    if (N10 >= 10)
                        return (R3_FA(N100) + " و " + R2_FA(N10));
                    else
                        return (R3_FA(N100) + " و " + R1_FA(N10));
            }
        }

        public static string N2L_FA(Int64 iVal)
        {
            int[] P3D = new int[5];
            string N2L = "";
            int TN = 0;
            int Temp;
            int TL = 0;
            string sTemp = "";
            string Number = iVal.ToString();
            int L = Number.Length;

            int I = 0;
            while (I < L)
            {
                I += 3;
                Temp = 0;
                sTemp = "000" + Number;
                sTemp = sTemp.Substring(sTemp.Length - I, I);
                Temp = int.Parse(sTemp.Substring(0, 3));
                P3D[I / 3] = Temp;
            }

            I = 4;
            while (I >= 0)
            {
                TN = P3D[I];
                TL = TN.ToString().Length;
                switch (TL)
                {
                    case 1:
                        N2L += R1_FA(TN);
                        break;
                    case 2:
                        N2L += R2_FA(TN);
                        break;
                    case 3:
                        N2L += R3_FA(TN);
                        break;
                }

                if (TN != 0)
                {
                    switch (I)
                    {
                        case 1:
                            N2L += "";
                            break;
                        case 2:
                            if (P3D[I - 1] != 0)
                                N2L += "هزار و ";
                            else
                                N2L += "هزار ";
                            break;
                        case 3:
                            if (P3D[I - 1] != 0)
                                N2L += "میلیون و ";
                            else
                            {
                                if (P3D[I - 2] != 0)
                                    N2L += "میلیون و ";
                                else
                                    N2L += "میلیون ";
                            }
                            break;
                        case 4:
                            if (P3D[I - 1] != 0)
                                N2L += "میلیارد و ";
                            else
                            {
                                if (P3D[I - 2] != 0)
                                    N2L += "میلیارد و ";
                                else
                                    N2L += "میلیارد ";
                            }
                            break;
                        case 5:
                            if (P3D[I - 1] != 0)
                                N2L += "تریلیون و ";
                            else
                            {
                                if (P3D[I - 2] != 0)
                                    N2L += "تریلیون و ";
                                else
                                {
                                    if (P3D[I - 3] != 0)
                                        N2L += "تریلیون و ";
                                    else
                                        N2L += "تریلیون ";
                                }
                            }
                            break;
                    }
                }
                I--;
            }
            return N2L;
        }

        /***** English Version *****/
        private static string R1_EN(int iVal)
        {
            switch (iVal)
            {
                case 1:
                    return "one ";
                    break;
                case 2:
                    return "two ";
                    break;
                case 3:
                    return "three ";
                    break;
                case 4:
                    return "four ";
                    break;
                case 5:
                    return "five ";
                    break;
                case 6:
                    return "six ";
                    break;
                case 7:
                    return "seven ";
                    break;
                case 8:
                    return "eight ";
                    break;
                case 9:
                    return "nine ";
                    break;
                default:
                    return "";
                    break;
            }
        }

        private static string R2_EN(int iVal)
        {
            int N1 = 0;
            int N10 = 0;
            if (iVal != 10)
            {
                N1 = iVal % 10;
                N10 = iVal - N1;
            }
            switch (iVal)
            {
                case 10:
                    return "ten ";
                    break;
                case 11:
                    return "eleven ";
                    break;
                case 12:
                    return "twelve ";
                    break;
                case 13:
                    return "thirteen ";
                    break;
                case 14:
                    return "fourteen ";
                    break;
                case 15:
                    return "fifteen ";
                    break;
                case 16:
                    return "sixteen ";
                    break;
                case 17:
                    return "seventeen ";
                    break;
                case 18:
                    return "eighteen ";
                    break;
                case 19:
                    return "nineteen ";
                    break;
                case 20:
                    return "twenty";
                    break;
                case 30:
                    return "thirty";
                    break;
                case 40:
                    return "fourty";
                    break;
                case 50:
                    return "fifty";
                    break;
                case 60:
                    return "sixty";
                    break;
                case 70:
                    return "seventy";
                    break;
                case 80:
                    return "eighty";
                    break;
                case 90:
                    return "ninty";
                    break;
                default:
                    return (R2_EN(N10) + "-" + R1_EN(N1));
            }
        }

        private static string R3_EN(int iVal)
        {
            int N10 = 0;
            int N100 = 0;
            N10 = iVal % 100;
            N100 = iVal - N10;
            switch (iVal)
            {
                case 100:
                    return "one hundred ";
                    break;
                case 200:
                    return "two hundred ";
                    break;
                case 300:
                    return "three hundred ";
                    break;
                case 400:
                    return "four hundred ";
                    break;
                case 500:
                    return "five hundred ";
                    break;
                case 600:
                    return "six hundred ";
                    break;
                case 700:
                    return "seven hundred ";
                    break;
                case 800:
                    return "eight hundred ";
                    break;
                case 900:
                    return "nine hundred ";
                    break;
                default:
                    if (N10 >= 10)
                        return (R3_EN(N100) + "" + R2_EN(N10));
                    else
                        return (R3_EN(N100) + "" + R1_EN(N10));
            }
        }

        public static string N2L_EN(Int64 iVal)
        {
            int[] P3D = new int[5];
            string N2L = "";
            int TN = 0;
            int Temp;
            int TL = 0;
            string sTemp = "";
            string Number = iVal.ToString();
            int L = Number.Length;

            int I = 0;
            while (I < L)
            {
                I += 3;
                Temp = 0;
                sTemp = "000" + Number;
                sTemp = sTemp.Substring(sTemp.Length - I, I);
                Temp = int.Parse(sTemp.Substring(0, 3));
                P3D[I / 3] = Temp;
            }

            I = 4;
            while (I >= 0)
            {
                TN = P3D[I];
                TL = TN.ToString().Length;
                switch (TL)
                {
                    case 1:
                        N2L += R1_EN(TN);
                        break;
                    case 2:
                        N2L += R2_EN(TN);
                        break;
                    case 3:
                        N2L += R3_EN(TN);
                        break;
                }

                if (TN != 0)
                {
                    switch (I)
                    {
                        case 1:
                            N2L += "";
                            break;
                        case 2:
                            if (P3D[I - 1] != 0)
                                N2L += "thousand ";
                            else
                                N2L += "thousand ";
                            break;
                        case 3:
                            if (P3D[I - 1] != 0)
                                N2L += "million ";
                            else
                            {
                                if (P3D[I - 2] != 0)
                                    N2L += "million ";
                                else
                                    N2L += "million ";
                            }
                            break;
                        case 4:
                            if (P3D[I - 1] != 0)
                                N2L += "billion ";
                            else
                            {
                                if (P3D[I - 2] != 0)
                                    N2L += "billion ";
                                else
                                    N2L += "billion ";
                            }
                            break;
                        case 5:
                            if (P3D[I - 1] != 0)
                                N2L += "trillion ";
                            else
                            {
                                if (P3D[I - 2] != 0)
                                    N2L += "trillion ";
                                else
                                {
                                    if (P3D[I - 3] != 0)
                                        N2L += "trillion ";
                                    else
                                        N2L += "trillion ";
                                }
                            }
                            break;
                    }
                }
                I--;
            }
            return N2L;
        }
    }
}