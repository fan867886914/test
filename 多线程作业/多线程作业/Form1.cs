using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace 多线程作业
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        private static readonly object Form_Obj = new object();
        List<Task> taskList = new List<Task>();
        private void button1_Click(object sender, EventArgs e)
        {
            taskList.Clear();
            i = 0;
            Stopwatch sw = new Stopwatch();
            sw.Start();
            //for (int i=0;i<3;i++)
            //{
                //if (i == 0)
                //{
                    taskList.Add( Task.Run(() =>
                    {
                       

                            GetString(qiaoFeng);

                        
                    }));
                  
                //}
                //else if (i == 1)
                //{
                    taskList.Add(Task.Run(() =>
                    {
                      
                            GetString(duanYu);
                        

                       
                    }));
                //}
                //else
                //{
                    taskList.Add(Task.Run(() =>
                    {
                      
                            GetString(xuZhu);
                        
                    }));
                //}

                

               
            //}
            Task.Run(()=> 
            {
                while (true) 
                {
                    int time = this.GetRandomNumber(0, 2021);
                    Console.WriteLine(time);
                    // Thread.Sleep(time);//随机休息一下
                    if (time == DateTime.Now.Year)
                    {
                        this.IsOK = false;
                        this.Invoke(new Action(() =>
                        {
                            this.textBox1.AppendText("天降雷霆灭世，天龙八部的故事就此结束...." + "\r\n");
                            Console.WriteLine("天降雷霆灭世，天龙八部的故事就此结束....");
                          
                        }));
                        break;
                    }
                }
              
               

            });

            Task.Factory.ContinueWhenAny(taskList.ToArray(), t =>
            {
                this.Invoke(new Action(() =>
                {
                    this.textBox1.AppendText("某某已经做好准备啦。。。。" + "\r\n");
                    Console.WriteLine("某某已经做好准备啦。。。。");
                }));
               
              
            });

            Task.Factory.ContinueWhenAll(taskList.ToArray(), t =>
            {
                this.Invoke(new Action(() =>
                {
                    this.textBox1.AppendText("中原群雄大战辽兵，忠义两难一死谢天" + "\r\n");
                    Console.WriteLine("中原群雄大战辽兵，忠义两难一死谢天");
                    sw.Stop();
                    TimeSpan ts2 = sw.Elapsed;
                    Console.WriteLine("系统统计出来整个天龙八部的故事花了{0}ms.", ts2.TotalMilliseconds);
                    this.textBox1.AppendText($"系统统计出来整个天龙八部的故事花了{ts2.TotalMilliseconds}ms." + "\r\n");

                }));
              

            });
        }
        List<string> qiaoFeng = new List<string>();
        List<string> duanYu = new List<string>();
        List<string> xuZhu = new List<string>();

        private void Form1_Load(object sender, EventArgs e)
        {
            //初始化数据
            qiaoFeng.Add("丐帮帮主");
            qiaoFeng.Add("契丹人");
            qiaoFeng.Add("南院大王");
            qiaoFeng.Add("挂印离开");

            xuZhu.Add("小和尚");
            xuZhu.Add("逍遥掌门");
            xuZhu.Add("灵鹫宫宫主");
            xuZhu.Add(" 西夏驸马");

            duanYu.Add("钟灵儿");
            duanYu.Add("木婉清");
            duanYu.Add("王语嫣");
            duanYu.Add("大理国王");

        }
        bool istrue = false;
        int i = 0;
        private bool IsOK = true;
        private void GetString(List<string> list) 
        {
          
                foreach (string str in list)
                {
             

                    Thread.Sleep(this.GetRandomNumber(500, 1000));//随机休息一下
                    Console.WriteLine(str);
                    this.Invoke(new Action(() =>
                    {
                        this.textBox1.AppendText(str + "\r\n");

                        lock (Form_Obj)
                        {
                            i++;
                            if (i == 1)
                            {
                                istrue = true;
                            }


                            if (istrue == true)
                            {
                                Console.WriteLine("天龙八部就此拉开序幕。。。。");
                                this.textBox1.AppendText("天龙八部就此拉开序幕。。。。" + "\r\n");

                                istrue = false;

                            }
                        }
                    }));




                
            }
               
            

               
                

              
            
        }

        public int GetRandomNumberDelay(int min, int max)
        {
            Thread.Sleep(this.GetRandomNumber(500, 1000));//随机休息一下
            return this.GetRandomNumber(min, max);
        }


        /// <summary>
        /// 获取随机数，解决重复问题
        /// </summary>
        /// <param name="min"></param>
        /// <param name="max"></param>
        /// <returns></returns>
        public int GetRandomNumber(int min, int max)
        {
            Guid guid = Guid.NewGuid();//每次都是全新的ID  全球唯一Id
            string sGuid = guid.ToString();
            int seed = DateTime.Now.Millisecond;
            //保证seed 在同一时刻 是不相同的
            for (int i = 0; i < sGuid.Length; i++)
            {
                switch (sGuid[i])
                {
                    case 'a':
                    case 'b':
                    case 'c':
                    case 'd':
                    case 'e':
                    case 'f':
                    case 'g':
                        seed = seed + 1;
                        break;
                    case 'h':
                    case 'i':
                    case 'j':
                    case 'k':
                    case 'l':
                    case 'm':
                    case 'n':
                        seed = seed + 2;
                        break;
                    case 'o':
                    case 'p':
                    case 'q':
                    case 'r':
                    case 's':
                    case 't':
                        seed = seed + 3;
                        break;
                    case 'u':
                    case 'v':
                    case 'w':
                    case 'x':
                    case 'y':
                    case 'z':
                        seed = seed + 3;
                        break;
                    default:
                        seed = seed + 4;
                        break;
                }
            }
            Random random = new Random(seed);
            return random.Next(min, max);
        }

    }
}
