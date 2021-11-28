using J2RXEK_HFT_2021221.Models;
using System;
using System.Collections.Generic;

namespace J2RXEK_HFT_2021221.Client
{
    class Program
    {
        static void Setup()
        {
            Menu menu=new Menu();
            menu.clear += Clear;
            menu.input += Input;
            menu.write += Write;
            menu.writeline += WriteLine;
            menu.Start();
            
        }
        static void Main(string[] args)
        {
            Setup();
        }
        static void Clear()
        {
            Console.Clear();
        }
        static string Input()
        {
            return Console.ReadLine();
        }
        static void Write(string word)
        {
            Console.Write(word);
        }
        static void WriteLine(string word)
        {
            Console.WriteLine(word);
        }
    }
}
