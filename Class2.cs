using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace Com_Methods
{
    class Class2
    {
        //static void f(int i)
        //{
        //    Console.WriteLine(i);
        //}


        // анонимный метод
        //delegate (параметр) {инструкция}
        //нужен для распараллеливания, типо как метод, который будем вызывать несколько раз, чтобы параллелить

        //delegate void Solver (int i);
        //class A
        //{
        //    public void func(int i)
        //    {
        //        //Аннонимный или безымянный метод
        //        Solver Func = delegate (int I)
        //        {
        //            Console.WriteLine(I);
        //        };

        //        Func(i);
        //    }
        //}


        // лямбда функция
        delegate void Solver(int i);
        //class A
        //{
        //    public void func(int i)
        //    {
        //        //(параметры) => {инструкции}
        //        Solver Func = (int I) =>
        //        {
        //            Math.Pow(I, 3);
        //        };

        //        Func(i);
        //    }
        //}

        public static void Main()
        {
            //Сами создаем потоки
            Thread t1 = new Thread(/*Solver Func =*/ () => { Math.Pow(5, 3); });
            t1.Start();
            //Даем распределить компьютеру потоки
            ThreadPool.QueueUserWorkItem((Par) => { Math.Pow(5, 3); });
            Stopwatch sw = new Stopwatch();

            sw.Start();
           // Parallel.For()
            for (int i = 0; i < 100000; i++)
            {

               
            }
            sw.Stop();
           Console.WriteLine( sw.Elapsed.TotalSeconds);
          }
    }
}
