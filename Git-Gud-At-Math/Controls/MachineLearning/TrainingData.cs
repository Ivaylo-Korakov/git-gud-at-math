﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Git_Gud_At_Math.Controls.MachineLearning
{
    /// <summary>
    /// All the data about the machine learning is here
    /// </summary>
    public static class TrainingData
    {
        public static double[,] TestDataTs = new double[,]
        {
            { 1,0},
            { 2,3},
            { 3,4},
            { 4,3},
            { 5,5},
            { 6,7},
            { 7,4},
            { 8,5},
        };

        public static double[,] CourseraTs = new double[,]
        {
            {6.1101, 17.592},
            {5.5277, 9.1302},
            {8.5186, 13.662},
            {7.0032, 11.854},
            {5.8598, 6.8233},
            {8.3829, 11.886},
            {7.4764, 4.3483},
            {8.5781, 12},
            {6.4862, 6.5987},
            {5.0546, 3.8166},
            {5.7107, 3.2522},
            {14.164, 15.505},
            {5.734, 3.1551},
            {8.4084, 7.2258},
            {5.6407, 0.71618},
            {5.3794, 3.5129},
            {6.3654, 5.3048},
            {5.1301, 0.56077},
            {6.4296, 3.6518},
            {7.0708, 5.3893},
            {6.1891, 3.1386},
            {20.27, 21.767},
            {5.4901, 4.263},
            {6.3261, 5.1875},
            {5.5649, 3.0825},
            {18.945, 22.638},
            {12.828, 13.501},
            {10.957, 7.0467},
            {13.176, 14.692},
            {22.203, 24.147},
            {5.2524, -1.22},
            {6.5894, 5.9966},
            {9.2482, 12.134},
            {5.8918, 1.8495},
            {8.2111, 6.5426},
            {7.9334, 4.5623},
            {8.0959, 4.1164},
            {5.6063, 3.3928},
            {12.836, 10.117},
            {6.3534, 5.4974},
            {5.4069, 0.55657},
            {6.8825, 3.9115},
            {11.708, 5.3854},
            {5.7737, 2.4406},
            {7.8247, 6.7318},
            {7.0931, 1.0463},
            {5.0702, 5.1337},
            {5.8014, 1.844},
            {11.7, 8.0043},
            {5.5416, 1.0179},
            {7.5402, 6.7504},
            {5.3077, 1.8396},
            {7.4239, 4.2885},
            {7.6031, 4.9981},
            {6.3328, 1.4233},
            {6.3589, -1.4211},
            {6.2742, 2.4756},
            {5.6397, 4.6042},
            {9.3102, 3.9624},
            {9.4536, 5.4141},
            {8.8254, 5.1694},
            {5.1793, -0.74279},
            {21.279, 17.929},
            {14.908, 12.054},
            {18.959, 17.054},
            {7.2182, 4.8852},
            {8.2951, 5.7442},
            {10.236, 7.7754},
            {5.4994, 1.0173},
            {20.341, 20.992},
            {10.136, 6.6799},
            {7.3345, 4.0259},
            {6.0062, 1.2784},
            {7.2259, 3.3411},
            {5.0269, -2.6807},
            {6.5479, 0.29678},
            {7.5386, 3.8845},
            {5.0365, 5.7014},
            {10.274, 6.7526},
            {5.1077, 2.0576},
            {5.7292, 0.47953},
            {5.1884, 0.20421},
            {6.3557, 0.67861},
            {9.7687, 7.5435},
            {6.5159, 5.3436},
            {8.5172, 4.2415},
            {9.1802, 6.7981},
            {6.002, 0.92695},
            {5.5204, 0.152},
            {5.0594, 2.8214},
            {5.7077, 1.8451},
            {7.6366, 4.2959},
            {5.8707, 7.2029},
            {5.3054, 1.9869},
            {8.2934, 0.14454},
            {13.394, 9.0551},
            {5.4369, 0.61705},
        };

        public static double[,] ExcelTs = new double[,]
        {
            {8.45, 10.75},
            {7.75, 7.75},
            {9.5, 6.25},
            {9.85, 4.25},
            {11.95, 2.00},
            {11.6, 2.00},
            {9.15, 5.00},
            {7.4, 2.75},
            {10.55, 5.50},
            {8.8, 4.00},
            {7.4, 9.75},
            {13, 0.00},
            {6.35, 14.00},
            {8.8, 4.50},
            {11.25, 2.00},
            {6.35, 10.75},
            {12.3, 1.75},
            {8.45, 6.50},
            {8.45, 5.00},
            {13, 0.00},
            {8.1, 5.00},
            {9.15, 6.25},
            {12.3, 0.75},
            {12.3, 0.75},
            {10.55, 3.25},
            {11.25, 2.50},
            {6, 11.75},
            {6.35, 9.50},
        };

        public static double[,] OpenClassRoomTs = new double[,]
        {
            {2.07, 0.78},
            {2.37, 0.92},
            {2.54, 0.91},
            {2.54, 0.91},
            {2.55, 0.94},
            {2.79, 0.97},
            {2.91, 0.96},
            {3.04, 0.91},
            {3.11, 0.94},
            {3.16, 0.96},
            {3.33, 0.90},
            {3.38, 0.91},
            {3.41, 0.94},
            {3.42, 0.97},
            {3.53, 1.05},
            {3.64, 1.01},
            {3.67, 0.96},
            {3.93, 0.97},
            {4.05, 1.08},
            {4.25, 1.15},
            {4.34, 1.03},
            {4.38, 1.01},
            {4.42, 0.97},
            {4.61, 1.09},
            {4.69, 1.06},
            {4.98, 1.12},
            {5.04, 1.03},
            {5.07, 1.09},
            {5.42, 1.07},
            {5.44, 1.16},
            {5.46, 1.08},
            {5.57, 1.11},
            {5.60, 1.10},
            {5.69, 1.16},
            {5.72, 1.14},
            {5.85, 1.08},
            {6.20, 1.13},
            {6.35, 1.12},
            {6.48, 1.20},
            {6.74, 1.21},
            {6.86, 1.13},
            {7.02, 1.12},
            {7.08, 1.21},
            {7.15, 1.25},
            {7.47, 1.25},
            {7.60, 1.18},
            {7.74, 1.19},
            {7.77, 1.30},
            {7.83, 1.26},
            {7.93, 1.26},
        };
    }
}
