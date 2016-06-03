using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using nepumuk;
using System.Globalization;

namespace EinlesenVonStep
{
    public partial class Form1 : Form
    {

        String[] stpDat;
        String din;
        String FileDescritpion;
        String FileName;
        String FileShema;
        String FileCreationDate;
       
     //   int[] Data;


        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            stpDat = textBox1.Text.Split('\n');
           // textBox1.Text = "";
            din = stpDat[0].Substring(0,stpDat[0].Length-3);
            foreach (string a in stpDat)
            {
                interpreteString(a);
                //textBox1.Text += a + " new line \n";
            }
            writeLabels();
        }

        private void interpreteString(string s)
        {
            int currentTask;
            string instruktion;
            string rest;

            if (s.Contains("FILE_DESCRIPTION"))
            {
                FileDescritpion = s.Substring(s.IndexOf("(('") + 3, s.IndexOf("'),") - s.IndexOf("(('")-3);
            }
            if (s.Contains("FILE_NAME"))
            {
                FileName = s.Substring(s.IndexOf("('")+2, s.IndexOf("','") - s.IndexOf("('")-2);
                FileCreationDate = s.Substring(s.IndexOf("','") + 3, s.IndexOf("',('") - s.IndexOf("','")-3);

            }
            if (s.Contains("FILE_SCHEMA"))
            {   
                FileShema = s.Substring(s.IndexOf("(('") + 3, s.IndexOf("'))") - s.IndexOf("(('")-3);
            }
            if (s.Contains("#"))
            {
               currentTask= Convert.ToInt32( s.Substring(1, s.IndexOf('=')-1));
               instruktion = s.Substring(s.IndexOf('=') + 1, s.IndexOf('(') - 1 - s.IndexOf('='));
               rest = s.Substring(s.IndexOf('(') + 1, s.Length - 1 - s.IndexOf('('));

               switch (instruktion)
               {
                   case "CARTESIAN_POINT":
                       rest = rest.Substring(rest.IndexOf(',')+1);
                       double x, y, z;
                       x = Convert.ToDouble(rest.Substring(rest.IndexOf("(") + 1, rest.IndexOf(',') - rest.IndexOf("(")-1), CultureInfo.InvariantCulture.NumberFormat);
                       rest = rest.Substring(rest.IndexOf(',') + 1);
                       y = Convert.ToDouble(rest.Substring(0, rest.IndexOf(',')), CultureInfo.InvariantCulture.NumberFormat);
                       rest = rest.Substring(rest.IndexOf(',') + 1);
                       z = Convert.ToDouble(rest.Substring(0, rest.IndexOf(')')), CultureInfo.InvariantCulture.NumberFormat);
                       vectorDraw1.DrawPoint(new Vector(x,y,z));
                      
                       break;

                   default:


                       break;
               }




            }

            


            
        }

        private void writeLabels()
        {
            label1.Text = ("Name: " + FileName);
            label2.Text = ("Date of Creation: " + FileCreationDate);
            label3.Text = ("Descritpion: " + FileDescritpion);
            label4.Text = ("Shema: " + FileShema);
            label5.Text = ("" + din);



        }

       

        


    }
}
