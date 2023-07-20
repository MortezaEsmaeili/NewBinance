using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BinanceApp.Helper
{
    public class ArrayPoint
    {
        public ArrayPoint(int _index, decimal _value)
        {
            index = _index;
            value = _value;
        }
        public int index { get;  }
        public decimal value { get;  }
    }
    public class PersonalMath
    {
        private List<decimal> _dataArr;
        public PersonalMath(List<decimal> dataArr )
        {
            _dataArr = dataArr;
        }
        public void  GetLocalMinMax(decimal percent, out List<ArrayPoint>localMins, out List<ArrayPoint> localMaxes)
        {
            var shiftedData = _dataArr.Skip(1).ToList();
            shiftedData.Add(shiftedData.Last());
            decimal[] diff = new decimal[_dataArr.Count];
            localMins = new List<ArrayPoint>();
            localMaxes = new List<ArrayPoint>();
           
            for (int i = 0; i < _dataArr.Count; i++)
            {
                diff[i] = _dataArr[i] - shiftedData[i];
                if (i == 0) continue;
                if (Math.Sign(diff[i - 1]) > Math.Sign(diff[i]))
                    localMins.Add(new ArrayPoint(i, _dataArr[i]));
                else if (Math.Sign(diff[i - 1]) < Math.Sign(diff[i]))
                    localMaxes.Add(new ArrayPoint(i, _dataArr[i]));
            }
            FilterLocalMins(percent, localMins, localMaxes);
            FilterLocalMaxes(percent, localMins, localMaxes);
        }

        /*private static void FilterLocalMaxes(decimal percent, List<Point> localMaxes)
        {
            if (localMaxes.Count > 1)
            {
                var tolerance = percent * (localMaxes.Max(m => m.value) - localMaxes.Min(m => m.value)) / 100;
                int k = 1;
                while (k < localMaxes.Count)
                {
                    if (Math.Abs(localMaxes[k - 1].value - localMaxes[k].value) < tolerance)
                    {
                        if (localMaxes[k - 1].value > localMaxes[k].value)
                            localMaxes.RemoveAt(k);
                        else
                            localMaxes.RemoveAt(k - 1);
                    }
                    else
                        k++;
                }
            }
        }*/

        private static void FilterLocalMaxes(decimal percent, List<ArrayPoint> localMins, List<ArrayPoint> localMaxes)
        {
            List<ArrayPoint> localMinMax = new List<ArrayPoint>();
            localMinMax.AddRange(localMins);
            localMinMax.AddRange(localMaxes);
            localMinMax = localMinMax.OrderBy(x => x.index).ToList();
            decimal max = localMinMax.Max(m=>m.value);
            decimal min = localMinMax.Min(m=>m.value);
            decimal targetPercentValue = (percent * (max + min) / 2) / 10000;

            if (localMaxes.Count > 1)
            {
                decimal tolerance = 0;
                int k = 0;
                while (k < localMaxes.Count)
                {
                    int localmaxIdx = localMinMax.IndexOf(localMaxes[k]);
                    if (localmaxIdx > 0)
                    {
                        if ((localmaxIdx + 1) < localMinMax.Count)
                            tolerance = localMinMax[localmaxIdx].value -
                                Math.Min(localMinMax[localmaxIdx - 1].value, localMinMax[localmaxIdx + 1].value);
                        else
                            tolerance = localMinMax[localmaxIdx].value - localMinMax[localmaxIdx - 1].value ;
                    }
                    else if ((localmaxIdx + 1) < localMinMax.Count)
                    {
                        tolerance = localMinMax[localmaxIdx].value - localMinMax[localmaxIdx + 1].value;
                    }
                    if (tolerance < targetPercentValue)
                        localMaxes.RemoveAt(k);
                    else
                        k++;
                }
            }
        }

        private static void FilterLocalMins(decimal percent, List<ArrayPoint> localMins, List<ArrayPoint>localMaxes)
        {
            List<ArrayPoint> localMinMax = new List<ArrayPoint>();
            localMinMax.AddRange( localMins);
            localMinMax.AddRange(localMaxes);
            localMinMax = localMinMax.OrderBy(x => x.index).ToList();
            decimal max = localMinMax.Max(x=>x.value);
            decimal min = localMinMax.Min(x=>x.value);
            decimal targetPercentValue = (percent * (max+min) / 2)/ 10000;

            if (localMins.Count > 1)
            {
                decimal tolerance = 0;
                int k = 0;
                while (k < localMins.Count)
                {
                    int localminIdx = localMinMax.IndexOf(localMins[k]);
                    if (localminIdx > 0)
                    {
                        if ((localminIdx + 1) < localMinMax.Count)
                            tolerance = Math.Max(localMinMax[localminIdx - 1].value, localMinMax[localminIdx + 1].value)
                                - localMinMax[localminIdx].value;
                        else
                            tolerance = localMinMax[localminIdx - 1].value - localMinMax[localminIdx].value;
                    }
                    else if ((localminIdx + 1) < localMinMax.Count)
                    {
                        tolerance = localMinMax[localminIdx + 1].value - localMinMax[localminIdx].value;
                    }
                    if (tolerance < targetPercentValue)
                        localMins.RemoveAt(k);
                    else
                        k++;                    
                }
            }
        }

        List<ArrayPoint> GetLocalMax(int degree=1)
        {
            if (_dataArr.Count < degree + 2)
                return null;
            List<ArrayPoint> localMaxes = new List<ArrayPoint>();
            decimal maxValue = _dataArr[0];
            int localDegree = 0;
            bool isAdditive = true;
            for (int i=1; i< _dataArr.Count; i++)
            {
                if (maxValue < _dataArr[i])
                {
                    maxValue = _dataArr[i];
                    localDegree++;
                    isAdditive = true;
                }
                else
                {
                    if (localDegree > degree)
                    {
                        localMaxes.Add(new ArrayPoint(i, maxValue));
                        localDegree = 0;
                    }


                    isAdditive = false;
                }
            }




            return localMaxes;
        }

        List<decimal> getLocalMin(int degree=1)
        {
            if (_dataArr.Count < degree + 2)
                return null;
            List<decimal> minLocal = new List<decimal>();


            return minLocal;
        }
    }
}
