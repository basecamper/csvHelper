﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace csvklasse
{
    public partial class Form1 : Form
    {

        Dictionary<string, int> dict = new Dictionary<string, int>();
        Dictionary<string, int> topWords = new Dictionary<string, int>();
        List<string> top = new List<string>();
        List<string> linesWithThe = new List<string>();
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            csvhelper helper = new csvhelper();
            helper.openCsv();
            helper.getContent();
            helper.cleanUp();
            helper.splitToLines();
            helper.getLstLines();
            helper.splitToWords();

            progressBar.Maximum = helper.getLstWords().Count();
            progressBar.Value = 1;
            progressBar.Step = 1;

            int value;

            //linesWithThe = helper.findWord(helper.getLstLines(), "employed");

            //for (int i = 0; i < 20; i++)
            //{
            //    lstTop.Items.Add(linesWithThe[i]);
            //}

            foreach (var word in helper.getLstWords())
            {
                dict[word] = dict.TryGetValue(word, out value) ? ++value : 1;
                progressBar.PerformStep();
            }
            foreach (KeyValuePair<string, int> word in dict.OrderByDescending(key => key.Value))
            {
                top.Add(word.Key + " " + word.Value);
            }

            for (int i = 0; i < 20; i++)
            {
                lstTop.Items.Add(top[i]);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            csvhelper write = new csvhelper();
            write.saveCSV(top, "F:\\csv-klasse\\test.txt");
        }
    }
}