using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace strata
{
    public class GameData
    {
        public int DivsCounter;
        public division[] divs;
        public string mapp;
        public string gamemode;
        public Form1 form;
        

        public GameData(Form1 form) 
        {
            this.form = form;
            this.divs = form.divs;
            DivsCounter = form.counter;
            this.gamemode = form.gamemode;
            mapp = form.mappath;
        }

        public void Save()
        {
            using (StreamWriter sw = File.CreateText("save.txt"))
            {
                sw.WriteLine(DivsCounter);
                sw.WriteLine(mapp);
                //sw.WriteLine(gamemode);
                int i = 0;
                while (divs[i].number > -1)
                {
                    sw.WriteLine(divs[i].Location.X);
                    sw.WriteLine(divs[i].Location.Y);
                    sw.WriteLine(divs[i].number);
                    sw.WriteLine(divs[i].alpha);
                    sw.WriteLine(divs[i].clas);
                    sw.WriteLine(divs[i].side);
                    sw.WriteLine(divs[i].name);
                    i++;

                }

                sw.WriteLine("end");
                sw.Close();
            }
        }

        public void Load(string filename, Form1 form)
        {
            using (StreamReader sr = File.OpenText(filename))
            {
                int c = Convert.ToInt32(sr.ReadLine());
                mapp = sr.ReadLine();
                gamemode = form.gamemode;
                string X = " ";
                while (X!="end")
                {
                    for (int i = 0; i < c; i++)
                    {
                        X = sr.ReadLine();
                        if (X == "end") { break; }
                        int x = Convert.ToInt32(X);//
                        int y = Convert.ToInt32(sr.ReadLine());//
                        int n = Convert.ToInt32(sr.ReadLine());//
                        double a = Convert.ToDouble(sr.ReadLine());//
                        string cl = sr.ReadLine();//
                        string s = sr.ReadLine();//
                        string na = sr.ReadLine();//
                        divs[i] = new division(cl,s,new Point(x,y), a, na,n,gamemode);
                    }
                }
                sr.Close();
            }
            form.divs = divs;
            form.map.Image = Image.FromFile(mapp);
        }

        public GameData() { }
    }
}