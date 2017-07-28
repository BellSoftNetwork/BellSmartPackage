using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BellLib.Class
{
    public class NumberManager
    {
        public enum Unit
        {
            만 = 1,
            억 = 2,
            조 = 3
        }

        public class NumberUnit
        {
            public double number { get; set; }
            public Unit? unit { get; set; }
        }

        public NumberUnit CalculateNumber(double num)
        {
            NumberUnit result = new NumberUnit();

            const double CUT = 10000;
            double curNum = num;
            int div = 0;
            double decPoint = 1;

            while (curNum >= CUT)
            {
                curNum /= CUT;
                div++;
            }

            if ((int)curNum >= 1000)
                decPoint = 1;
            else if ((int)curNum >= 100)
                decPoint = 10;
            else if ((int)curNum >= 10)
                decPoint = 100;
            else
                decPoint = 1000;

            result.number = (Convert.ToInt32((curNum * decPoint)) / decPoint);
            result.unit = (Unit)div;

            if (result.unit == 0)
                result.unit = null;

            return result;
        }
    }
}
