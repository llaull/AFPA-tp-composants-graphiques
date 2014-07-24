using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Text.RegularExpressions;

namespace composants_graphiques
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            string txt = @"c:\texte.txt";
            chercher.Enabled = false;


            if (!File.Exists(txt)) // Verification de la presence du fichier dans le chemin filename
            {
                label_erreur.ForeColor = System.Drawing.Color.Red;
                label_erreur.Text = "Erreur - Le Fichier est inexistant !";

            }
            else
            {
                label_erreur.ForeColor = System.Drawing.Color.Green;
                label_erreur.Text = "Fichier lu : " + txt;

                StreamReader mySr = new StreamReader(txt, Encoding.Default);
                richTextBox1.Text = mySr.ReadToEnd();

                StreamReader sr = new StreamReader(txt, Encoding.Default);
                string line = sr.ReadToEnd();

                string[] badWordsLine = line.Split(new string[] { " ", "\t", "\r\n" }, StringSplitOptions.RemoveEmptyEntries);

                Array.Sort(badWordsLine);

                for (int i = 0; i < badWordsLine.Length; i++)
                {
                    badWordsLine[i] = nettoie(badWordsLine[i]);
                }
                IEnumerable<string> listeTrie = badWordsLine.Distinct();


                foreach (string mot in listeTrie)
                {
                    liste.Items.Add(mot);
                    if (mot == "")
                    {
                        liste.Items.Remove(mot);
                    }
                }



                int test = badWordsLine.Length;

               
            }
        }

        public static string nettoie(string chaine)
        {
            return Regex.Replace(chaine, "[^A-Za-z0-9-'áàâäãåçéèêëíìîïñóòôöõúùûüýÿæœÁÀÂÄÃÅÇÉÈÊËÍÌÎÏÑÓÒÔÖÕÚÙÛÜÝŸÆŒ]", "");
        }


        private void liste_Click(object sender, EventArgs e)
        {
            chercher.Enabled = true;
        }

        private void chercher_Click(object sender, EventArgs e)
        {

            richTextBox1.SelectAll();
            richTextBox1.SelectionColor = Color.Black;
            richTextBox1.Select(0,0);
            int index = 0;
            while (index != -1)
            {
                index = Colore(index, liste.Text);
            }
           
        }


        private void liste_DoubleClick(object sender, EventArgs e)
        {
            int index = richTextBox1.Text.IndexOf(liste.Text, 0);
            richTextBox1.Focus();
            if (index != -1)
            {
                richTextBox1.SelectionStart = index;
                richTextBox1.SelectionLength = liste.Text.Length;
            }

        }
    
        private int Colore(int StartIndex, string mots)
        {
            

            int index = richTextBox1.Text.IndexOf(mots, StartIndex);
            if (index != -1)
            {
                richTextBox1.Select(index, mots.Length);
                richTextBox1.SelectionColor = Color.Red;
                return index + mots.Length;
               
            }
            return index;

        }

        
    }
}
