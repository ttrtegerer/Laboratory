using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LaboratoryAnalizatorTest
{
    public class LaboratoryAnalizator
    {
        public string Analizator(String Analizator, string Analiz)
        {
            string ZanatiyAnalizator = "Вновь Прибывший";
            String AnalizatorZakaz = "Улучшеный";
            String FirstAnalizator = "Стандарный";
            String SecondAnalizator = "Упрощённый";
            String NotYslugiAnalizator = "Устаревший";
            string FirstAnaliz = "Спид";
            string SecondAnaliz = "Вич";
            string ThirdAnaliz = "Гепатит";
            string FourthtAnaliz = "Альбумин";
            if (Analizator==null|| Analizator==ZanatiyAnalizator) 
            {
                return "(400, $Analyzer with name "+Analizator+" not found)";
            }
            if (Analizator == AnalizatorZakaz)
            {
                return "(400, Analyzer is busy)";
            }
            if(Analizator==FirstAnalizator)
                {
               if(Analiz == SecondAnaliz || Analiz == ThirdAnaliz)
                {
                    return "True";
                }
                else
                {
                    return "(400,Analyzer can not do this order.May be order contains services which analyzer does not support.)";
                }
            }
            if (Analizator == SecondAnalizator)
            {
                if (Analiz == FirstAnaliz || Analiz == ThirdAnaliz)
                {
                    return "True";
                }
                else
                {
                    return "(400,Analyzer can not do this order.May be order contains services which analyzer does not support.)";
                }
            }
            if (Analizator == NotYslugiAnalizator)
            {
                return "(400,Analyzer is not working.)";
            }


            return "False";
        }
    }
}
