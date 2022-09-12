using FunctionsApp;
using System.ComponentModel;

namespace FunctionsTests
{
    [TestClass]
    public class FunctionDataTest
    {
        [TestMethod]
        public void GetArrayCCalculatedByPower_EveryPowerFunc_Check()
        {
            List<int[]> _results = new List<int[]>();

            for (int curPower = 1; curPower <= Consts.NumberOfFuncs; curPower++)
            {
                FunctionData _functiondata = new FunctionData(curPower);
                _results.Add(_functiondata.ArrayC);
            }

            List<int[]> _expectedResults = new List<int[]>()
            {
                new int[]{ 1, 2, 3, 4, 5 },
                new int[]{ 10, 20, 30, 40, 50 },
                new int[]{ 100, 200, 300, 400, 500 },
                new int[]{ 1000, 2000, 3000, 4000, 5000 },
                new int[]{ 10000, 20000, 30000, 40000, 50000 },
            };

            for (int i = 0; i < Consts.NumberOfFuncs; i++)
                for (int j = 0; j < 5; j++)
                    Assert.AreEqual(_expectedResults[i][j], _results[i][j]);
        }


        [TestMethod]
        public void RecalculateFuncsResult_EveryPowerFunc_Rows3_Check()
        {
            List<double[]> _results = new List<double[]>();

            for (int curPower = 1; curPower <= Consts.NumberOfFuncs; curPower++)
            {
                FunctionData _functiondata = new FunctionData(curPower);
                _functiondata.ValueA = 7.2;
                _functiondata.ValueB = 2.1;
                _functiondata.ValueC = _functiondata.ArrayC[3];
                _functiondata.RowFunctions = new BindingList<RowFunction>()
                {
                    new RowFunction()
                    {
                        ValueX = 1,
                        ValueY = 1
                    },

                     new RowFunction()
                    {
                        ValueX = 5.5,
                        ValueY = 1.7
                    },

                      new RowFunction()
                    {
                        ValueX = -5,
                        ValueY = 0
                    }

                };

                _functiondata.RecalculateFuncsResult();
                double[] answers = new double[3]
                {
                    _functiondata.RowFunctions[0].Result,
                    _functiondata.RowFunctions[1].Result,
                    _functiondata.RowFunctions[2].Result
                };
                _results.Add(answers);
            }

            List<double[]> _expectedResults = new List<double[]>()
            {
                new double[]{ 13.3, 45.7, -29.9 },
                new double[]{ 49.3, 261.37, 220 },
                new double[]{ 409.3, 1603.969, -500 },
                new double[]{ 4009.3, 10598.7673, 8500 },
                new double[]{ 40009.3, 76254.01441, 17500 }
            };

            for (int i = 0; i < Consts.NumberOfFuncs; i++)
                for (int j = 0; j < 3; j++)
                    Assert.AreEqual(_expectedResults[i][j], _results[i][j], 0.0001);
        }
    }
}