using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace FunctionsApp
{
    /// <summary>
    /// Class which stores row data 
    /// </summary>
    public class RowFunction
    {
        public double ValueX { get; set; }
        public double ValueY { get; set; }
        public double Result { get; set; }
    }

    /// <summary>
    /// Class stores information about funtion. Calculates the results of function evaluations
    /// </summary>
    public class FunctionData
    {
        /// <summary>
        /// Variable used in <see cref="RecalculateFuncsResult"/>
        /// </summary>
        public int FunctionPower;
        public double ValueA;
        public double ValueB;
        public int[] ArrayC;
        public int ValueC;
        /// <summary>
        /// Variable stores rows with data
        /// </summary>
        public BindingList<RowFunction> RowFunctions { get; set; }

        public FunctionData(int power) 
        {
            RowFunctions = new BindingList<RowFunction>() { new RowFunction() };
            FunctionPower = power;
            ArrayC = GetArrayCCalculatedByPower(power);
            ValueC = ArrayC[0];
        }

        /// <summary>
        /// Calculates <see cref="ArrayC"/> with actual values for function
        /// </summary>
        /// <param name="power"> Current power of function </param>
        /// <returns></returns>
        public int[] GetArrayCCalculatedByPower(int power)
        {
            var _arrayC = new int[Consts.NumberOfFuncs];
            for (int i = 0; i < Consts.NumberOfFuncs; i++)
                _arrayC[i] = (i + 1) * (int)Math.Pow(10, power - 1);
            return _arrayC;
        }

        /// <summary>
        /// Recalculates function results in each row
        /// </summary>
        public void RecalculateFuncsResult()
        {
            foreach (RowFunction row in RowFunctions)
                row.Result = ValueA * Math.Pow(row.ValueX, FunctionPower) + 
                    ValueB * Math.Pow(row.ValueY, FunctionPower - 1) + ValueC;
        }
    }
}
