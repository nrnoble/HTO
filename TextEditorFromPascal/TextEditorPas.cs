// Decompiled with JetBrains decompiler
// Type: TextEditor.Program1
// Assembly: TextEditor, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 57E5AD5E-F941-495C-A816-CBA0B11D49FA
// Assembly location: C:\PABCWork.NET\Samples\Applications\TextEditor\TextEditor.exe

using System;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace TextEditorFromPascal
{
    public class Program1
    {
        public const string TextFileExt = "txt";
        public const string TextFileFilter = "Текстовые файлы (*.txt)|*.txt";
        public static Form myForm;
        public static TextBox TextBox1;
        public static ToolStripItem[] intarr2;
    public static ToolStripItem[] intarr3;
    public static ToolStripItem[] intarr4;
    public static ToolStripItem[] intarr5;
    public static bool is_init;

    [STAThread]
        public static void Main()
        {
            Program1._Init_();
            Program1._InitVariables_();
            Program1.Main();
            //PABCSystem_implementation______.PABCSystem_implementation______.Finalization();
            PABCSystem.PABCSystem.__FinalizeModule__();
        }

        public static void SaveFile(string FileName)
        {
            StreamWriter streamWriter = new StreamWriter(FileName, false, Encoding.Default);
            streamWriter.Write(Program1.TextBox1.Text);
            streamWriter.Close();
        }

        public static void OpenFile(string FileName)
        {
            StreamReader streamReader = new StreamReader(FileName, Encoding.Default);
            Program1.TextBox1.Text = streamReader.ReadToEnd();
            streamReader.Close();
        }

        public static void FormClose(object sender, EventArgs args)
        {
            Program1.myForm.Close();
        }

        public static void MenuSaveClick(object sender, EventArgs args)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.DefaultExt = "txt";
            saveFileDialog.Filter = "Текстовые файлы (*.txt)|*.txt";
            if (saveFileDialog.ShowDialog() != DialogResult.OK)
                return;
            Program1.SaveFile(saveFileDialog.FileName);
        }

        public static void MenuOpenClick(object sender, EventArgs args)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.DefaultExt = "txt";
            openFileDialog.Filter = "Текстовые файлы (*.txt)|*.txt";
            if (openFileDialog.ShowDialog() != DialogResult.OK)
                return;
            Program1.OpenFile(openFileDialog.FileName);
        }

        public static void Main1()
        {
            Program1.myForm = new Form();
            Program1.myForm.Text = "Простой текстовый редактор";
            Program1.TextBox1 = new TextBox();
            Program1.TextBox1.Multiline = true;
            Program1.TextBox1.Height = 100;
            Program1.TextBox1.Dock = DockStyle.Fill;
            Program1.TextBox1.ScrollBars = ScrollBars.Both;
            Program1.TextBox1.Font = new Font("Courier New", 10f);
            Program1.myForm.Controls.Add((Control)Program1.TextBox1);
            ToolStrip toolStrip = new ToolStrip();
            toolStrip.GripStyle = ToolStripGripStyle.Hidden;
            ToolStripMenuItem toolStripMenuItem = new ToolStripMenuItem("Файл");
            toolStripMenuItem.DropDownItems.Add((ToolStripItem)new ToolStripMenuItem("Открыть", (Image)new Bitmap(PABCSystem.PABCSystem.GetResourceStream("Open.png")), new EventHandler(Program1.MenuOpenClick)));
            toolStripMenuItem.DropDownItems.Add((ToolStripItem)new ToolStripMenuItem("Сохранить как...", (Image)new Bitmap(PABCSystem.PABCSystem.GetResourceStream("Save.png")), new EventHandler(Program1.MenuSaveClick)));
            toolStripMenuItem.DropDownItems.Add((ToolStripItem)new ToolStripMenuItem("Выход", (Image)null, new EventHandler(Program1.FormClose)));
            toolStrip.Items.Add((ToolStripItem)toolStripMenuItem);
            Program1.myForm.Controls.Add((Control)toolStrip);
            if (PABCSystem.PABCSystem.CommandLineArgs.Length == 1)
                Program1.OpenFile(PABCSystem.PABCSystem.CommandLineArgs[0]);
            Application.Run(Program1.myForm);
        }

        public static void _Init_()
        {
            if (Program1.is_init)
        return;
            Program1.is_init = true;
            PABCSystem.PABCSystem.IsConsoleApplication = false;
            PABCSystem.PABCSystem.__CONFIG__.Add("locale", (object)"ru");
            PABCSystem.PABCSystem.__CONFIG__.Add("full_locale", (object)"ru-RU");
            PABCSystem_implementation______.PABCSystem_implementation______.Initialization();
            PABCExtensions_implementation______.PABCExtensions_implementation______.Initialization();
        }

        public static void _InitVariables_()
        {
        }
    }
}
