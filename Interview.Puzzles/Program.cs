using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Collections;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;
using System.Text.RegularExpressions;
using System.Text;
using System;
using Interview.Puzzles;

class Solution
{
    public static void Main(string[] args)
    {
        int b = 0b0101101101;
        var r = FlippingBit.CountFlip(b);
        //Console.WriteLine(Convert.ToString(r, toBase: 2));
        Console.WriteLine(r);
        Console.ReadLine();
    }
}