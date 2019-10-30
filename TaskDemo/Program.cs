using System;
using System.Threading;
using System.Threading.Tasks;

namespace TaskDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            //Task task = new Task(() => Console.WriteLine("异步编程"));
            //task.Start();

            //创建并执行了该任务
            //Task.Run(() => Console.WriteLine("异步编程"));

            Task task = Task.Run(() =>
            {
                Thread.Sleep(5000);//延迟五秒
                Console.WriteLine("异步编程");
                Thread.Sleep(5000);
            });
            Console.WriteLine(task.IsCompleted);//IsCompleted判断是否执行完成
            //wait，等待任务执行完成
            task.Wait();//wait放在第一个IsCompleted后面，会先返回fales然后开始执行Run方法
            Console.WriteLine(task.IsCompleted);
            Console.WriteLine("ok");
            Console.ReadKey();
        }
    }
}
