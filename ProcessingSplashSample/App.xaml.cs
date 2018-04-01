using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace ProcessingSplashSample
{
    /// <summary>
    /// App.xaml の相互作用ロジック
    /// https://qiita.com/dhq_boiler/items/930ff2dc960fd950d7a3
    /// </summary>
    public partial class App : Application
    {
        public App()
        {
            var lst = new[] {"aaaaa.txt", "bbbbb.txt", "ccccc.txt", "ddddd.txt", "eeeee.txt"};
            ProcessingSplash ps = new ProcessingSplash("Initializing", (path) =>
            {
                //ここで重い処理を処理させる
                System.Threading.Thread.Sleep(1000);
            },lst.ToList());

            // バックグラウンド処理が終わるまで表示して待つ
            ps.ShowDialog();

            if (ps.IsComplete)
            {
                Console.WriteLine("処理が成功した");
            }
            else
            {
                Console.WriteLine("処理が失敗した");
            }
        }
    }
}