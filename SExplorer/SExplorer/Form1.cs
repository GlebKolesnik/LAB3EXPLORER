using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SExplorer
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        private string filePath = "C: ";
        private bool isFile = false;
        private string currentlySelectedItemName = "";
        private void Form1_load(object sender, EventArgs e)
        {
            textBox1.Text = filePath;
            loadFilesAndDirectories();
        }
        public void loadFilesAndDirectories()
        {
            DirectoryInfo fileList;
            string tempFilePath = "";
            FileAttributes fileAttr;

            try
            {
                if (isFile)
                {
                    tempFilePath = filePath + "/" + currentlySelectedItemName;
                    FileInfo fileDetails = new FileInfo(tempFilePath);
                    label2.Text = fileDetails.Name;
                    label4.Text = fileDetails.Extension;
                    fileAttr = File.GetAttributes(tempFilePath);
                    Process.Start(tempFilePath);

                }
                else
                {
                    fileAttr = File.GetAttributes(filePath);

                }
                if((fileAttr & FileAttributes.Directory ) == FileAttributes.Directory)
                {
                    fileList = new DirectoryInfo(filePath);
                    FileInfo[] files = fileList.GetFiles(); //Get all of the files 
                    DirectoryInfo[] dirs = fileList.GetDirectories(); //Get DIRS

                    listView1.Items.Clear();

                    for (int i = 0; i < files.Length; i++)
                    {
                        listView1.Items.Add(files[i].Name, 2);
                    }
                    for (int i = 0; i < dirs.Length; i++)
                    {
                        listView1.Items.Add(dirs[i].Name, 1);
                    }
                }
                else
                {
                    label2.Text = this.currentlySelectedItemName;
                }
            }
            catch (Exception e)
            {

            }
        }
        public void removeBackSlash()
        {
            string path = textBox1.Text;
            if (path.LastIndexOf("/") == path.Length - 1)
            {
                textBox1.Text = path.Substring(0, path.Length - 1);
            }
        }
        public void goBack()
        {
            try
            {
                removeBackSlash();
                string path = textBox1.Text;
                path = path.Substring(0, path.LastIndexOf("/"));
                this.isFile = false;
                textBox1.Text = path;
                removeBackSlash();
            }
            catch (Exception e)
            {

            }
        }
        //label4 = fileTypeLable
        private void fileTypeLable(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }
        private void fileNameLable(object sender, EventArgs e)
        //label2 = fileNameLable
        {

        }
        
        private void label1_Click(object sender, EventArgs e)
        {

        }
        // button1 = backButton
        private void backButton(object sender, EventArgs e)
        {
            goBack();
            loadButtonAction();
        }
        // button2 = goButton 
        public void loadButtonAction()
        {
            //removeBackSlash(); 
            filePath = textBox1.Text;
            loadFilesAndDirectories();
            isFile = false;

        }
        private void goButton(object sender, EventArgs e)
        {
            loadButtonAction();
        }
        // textBox1 = filePathTextBox
        private void filePathTextBox(object sender, EventArgs e)
        {

        }
        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void listView1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            loadButtonAction();
        }

        private void listView1_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
        {
            currentlySelectedItemName = e.Item.Text;

            FileAttributes fileAttr = File.GetAttributes(filePath + "/" + currentlySelectedItemName);
            if ((fileAttr & FileAttributes.Directory) == FileAttributes.Directory)
            {
                isFile = false;
                textBox1.Text = filePath + "/" + currentlySelectedItemName;
            }
            else
            {
                isFile = true;
            }
        }
    }
}
