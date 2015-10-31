using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vz.MegaHack.Engines {
    public static class StatisticsHelper {
        public static double GetStandardDeviation(List<double> doubleList) {
            double average = doubleList.Average();
            double sumOfDerivation = 0;
            foreach (double value in doubleList) {
                sumOfDerivation += (value) * (value);
            }
            double sumOfDerivationAverage = sumOfDerivation / doubleList.Count;
            return Math.Sqrt(sumOfDerivationAverage - (average * average));
        }
    }
}
