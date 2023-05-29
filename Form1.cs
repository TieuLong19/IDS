using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using GroupDocs.Comparison;
using GroupDocs.Comparison.Options;
using GroupDocs.Comparison.Result;
using System.Diagnostics;
using System.Reflection.Emit;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;
using IDS_DLL_01;

namespace ProjectIDS
{
    public partial class Form1 : Form
    {
        private Timer timer;
        myFunction x = new myFunction();
        public Form1()
        {
            InitializeComponent();
        }
        int countMessage = 0;
        string filename = "";
        string pathFolder = "";
        string fileNameCompare = "";
        string pathFolderCompare = "";
        List<Tuple<string, string>> myListDataInFile = new List<Tuple<string, string>>();
        List<Tuple<string, string>> myListDataOnComputer = new List<Tuple<string, string>>();
        List<Tuple<string, string>> captureFileChangeContent = new List<Tuple<string, string>>();
        List<Tuple<string, string>> captureFileNew = new List<Tuple<string, string>>();
        List<Tuple<string, string>> captureFileChangeName = new List<Tuple<string, string>>();
        List<Tuple<string, string>> captureFileDel = new List<Tuple<string, string>>();
        List<Tuple<string, string>> NameChanged = new List<Tuple<string, string>>();

        List<Tuple<string, string>> captureFileChangeContentSaveToCompare = new List<Tuple<string, string>>();
        public void ClearTuple()
        {
            myListDataInFile.Clear();
            myListDataOnComputer.Clear();
            captureFileChangeContent.Clear();
            captureFileNew.Clear();
            captureFileChangeName.Clear();
            captureFileDel.Clear();
            NameChanged.Clear();
        }
        //-----------------------------------------------------------Lưu đường dẫn đến file excel và thư mục cần kiểm tra -------------------------------------------------
        private void blogin_Click(object sender, EventArgs e)
        {
            filename = LocateFileSaved();
            pathFolder = LocateFolderSaved();
        }
        //----------------------------------------------------------- So sánh nội dung bị thay đổi khi file bị đổi nội dung (Sử dụng thư viện Comparison)-------------------------------------------------
        public void CompareTwoFile(string c, string d)
        {
            ComparerSettings a = new ComparerSettings();
            Comparer b = new Comparer(c);
            b.Add(d);
            b.Compare();
            ChangeInfo[] changes = b.GetChanges();
            foreach (var change in changes)
            {
                if (change.Type == ChangeType.Deleted)
                {
                    listBox1.Items.Add($"{c} -- Deleted content: {change.Text} ");
                }
                else if (change.Type == ChangeType.Inserted)
                {
                    listBox1.Items.Add($"{c} -- Inserted content: {change.Text} ");
                }
                else if (change.Type == ChangeType.StyleChanged)
                {
                    listBox1.Items.Add(c);
                    listBox1.Items.Add("Changed content:");
                    listBox1.Items.Add("Source content:");
                    listBox1.Items.Add(change.SourceText);
                    listBox1.Items.Add("Target content:");
                    listBox1.Items.Add(change.TargetText);
                }
            }
        }
        private void bexit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        public string EncryptString(string plainText, byte[] key, byte[] iv)
        {
            Aes encryptor = Aes.Create();
            encryptor.Mode = CipherMode.CBC;
            encryptor.Key = key;
            encryptor.IV = iv;
            MemoryStream memoryStream = new MemoryStream();
            ICryptoTransform aesEncrypt = encryptor.CreateEncryptor();
            CryptoStream cryptoStream = new CryptoStream(memoryStream, aesEncrypt, CryptoStreamMode.Write);
            byte[] plainBytes = Encoding.ASCII.GetBytes(plainText);
            cryptoStream.Write(plainBytes, 0, plainBytes.Length);
            cryptoStream.FlushFinalBlock();
            byte[] cipherBytes = memoryStream.ToArray();
            memoryStream.Close();
            cryptoStream.Close();
            string cipherText = Convert.ToBase64String(cipherBytes, 0, cipherBytes.Length);
            return cipherText;

        }
        //----------------------------------- Duyệt toàn bộ thư mục để check mã hash và lưu vào file excel, Hàm hash file nằm trong DLL ---------------------
        public void TraverseDirectory(string directoryPath)
        {
            try
            {
                foreach (string subdirectory in Directory.GetDirectories(directoryPath))
                {
                    TraverseDirectory(subdirectory);
                }
                ltest.Items.Add("Processing diretory: " + directoryPath);

                foreach (string file in Directory.GetFiles(directoryPath))
                {
                    ltest.Items.Add("Processing file: " + file);
                    string hashfile = x.HashAFile(file);
                    ltest.Items.Add("Processing MD5: " + hashfile);

                    SaveFile(file, hashfile);
                }
            }
            catch (Exception ex)
            {
                terror.Text = "Nhập đúng đường dẫn thư mục lưu trữ các file";
            }
        }
        /*        public string HashAFile(string path)
                {
                    Stream x_stream = new FileStream(path, FileMode.Open);
                    HashAlgorithm x_hash_alg = HashAlgorithm.Create("MD5");
                    byte[] x_hash_code = x_hash_alg.ComputeHash(x_stream);
                    string result = null;
                    foreach (byte b in x_hash_code)
                    {
                        result += b.ToString("X2");
                    }
                    x_stream.Close();
                    return result;
                }*/
        //----------------------------------------------------------- Tạo đường dẫn tới file excel đã lưu -------------------------------------------------
        public string LocateFileSaved()
        {
            SaveFileDialog s1 = new SaveFileDialog();
            s1.Filter = "File anh (*.jpg;*jpeg)|*.jpg;*jpeg|CSV Macintosh|*.csv|All files|*.*";
            s1.FilterIndex = 1; // file anh dau tien
            s1.Title = "Save a user file";
            s1.ShowDialog();
            return s1.FileName;
        }
        public string LocateFolderSaved()
        {
            FolderBrowserDialog folderBrowserDialog1 = new FolderBrowserDialog();
            folderBrowserDialog1.ShowDialog();
            string a = folderBrowserDialog1.SelectedPath;
            return a;
        }
        //----------------------------------------------------------- Lưu dữ liệu vào file excel -------------------------------------------------
        public void SaveFile(string a, string b)
        {
            FileStream fs;
            if (filename == "")
            {
                terror.Text = "Vui lòng nhập đúng đường dẫn file excel để lưu";
                return;
            }
            if (File.Exists(filename))
            {
                fs = new FileStream(filename, FileMode.Append, FileAccess.Write, FileShare.Write);
            }
            else
            {
                fs = new FileStream(filename, FileMode.Create, FileAccess.Write, FileShare.Write);
            }
            Encoding encoding = Encoding.UTF8;
            string chuoi = a.Trim() + "|" + b.Trim() + "\n";
            byte[] buffer = encoding.GetBytes(chuoi);
            fs.Write(buffer, 0, buffer.Length);
            fs.Close();
        }
        //----------------------------------------------------------- Đọc dữ liệu từ file excel -------------------------------------------------
        public void GetData()
        {
            string s = "";
            if (File.Exists(filename))
            {
                FileStream fs = new FileStream(filename, FileMode.Open, FileAccess.ReadWrite, FileShare.Read);
                int SIZEBUFFER = (int)fs.Length;
                byte[] buffer = new byte[SIZEBUFFER];
                int numberRead = fs.Read(buffer, 0, SIZEBUFFER);
                fs.Close();
                s = Encoding.UTF8.GetString(buffer, 0, numberRead);
                string[] data = s.Split('\n');
                for (int i = 0; i < data.Length; i++)
                {
                    if (data[i].Trim() == "") { continue; }
                    try
                    {
                        string[] data2 = data[i].Split('|');
                        Tuple<string, string> a = new Tuple<string, string>(data2[0], data2[1]);
                        myListDataInFile.Add(a);
                    }
                    catch (Exception ex)
                    {
                        terror.Text = $"Vui lòng nhập đúng đường dẫn";
                        return;
                    }
                }
            }
        }
        public void GetDataOfComputer(string directoryPath)
        {
            try
            {
                foreach (string subdirectory in Directory.GetDirectories(directoryPath))
                {
                    GetDataOfComputer(subdirectory);
                }

                foreach (string file in Directory.GetFiles(directoryPath))
                {
                    string hashfile = x.HashAFile(file);
                    Tuple<string, string> a = new Tuple<string, string>(file, hashfile);
                    myListDataOnComputer.Add(a);
                }
            }
            catch (Exception ex)
            {
                terror.Text = "Vui lòng tạo đường dẫn trước khi thực hiện";
            }
        }
        //----------------------------------------------------------- Bắt đầu kiểm tra file khi đã ấn nút bắt đầu -------------------------------------------------
        public void CheckFile()
        {
            terror.Text = "";
            GetData();
            GetDataOfComputer(pathFolder);
            foreach (Tuple<string, string> item in myListDataOnComputer)
            {
                bool flag = false, flag2 = false, flag3 = false;

                foreach (Tuple<string, string> item2 in myListDataInFile)
                {
                    if (item.Item1 == item2.Item1 && item.Item2 == item2.Item2)
                    {
                        myListDataInFile.Remove(item2);
                        flag = true;
                        break;
                    }
                    else if (item.Item1 != item2.Item1 && item.Item2 == item2.Item2)
                    {
                        NameChanged.Add(item2);
                        myListDataInFile.Remove(item2);
                        flag = true;
                        flag2 = true;

                        break;
                    }
                    else if (item.Item1 == item2.Item1 && item.Item2 != item2.Item2)
                    {
                        flag = true;
                        flag3 = true;
                        myListDataInFile.Remove(item2);
                        break;
                    }
                }
                if (!flag) captureFileNew.Add(item);
                if (flag2) captureFileChangeName.Add(item);
                if (flag3)
                {
                    captureFileChangeContent.Add(item);
                    bool checkdup = false;
                    foreach (Tuple<string, string> check in captureFileChangeContentSaveToCompare)
                    {
                        if (check.Item1 == item.Item1)
                        {
                            checkdup = true;
                            break;
                        }
                    }
                    if (!checkdup)
                        captureFileChangeContentSaveToCompare.Add(item);
                }
            }
            foreach (Tuple<string, string> item in myListDataInFile)
            {
                captureFileDel.Add(item);
            }
            Print(captureFileNew, "File new");
            Print(captureFileChangeContent, "File changed content");
            Print(captureFileDel, "File Del");
            for (int i = 0; i < captureFileChangeName.Count; i++)
            {
                string a = "Name is changed from " + NameChanged[i].Item1 + " to " + captureFileChangeName[i].Item1;
                ltest.Items.Add(a);
            }

        }
        public void Print(List<Tuple<string, string>> a, string b)
        {
            foreach (Tuple<string, string> item in a)
            {
                ltest.Items.Add(b + "  " + item.Item1 + "  " + item.Item2);
                if (countMessage == 0)
                {
                    countMessage++;
                    DialogResult result = MessageBox.Show($"Phát hiện xâm nhập {b} {item.Item1} {item.Item2}", "Cảnh báo", MessageBoxButtons.YesNo);
                    if(result == DialogResult.Yes)
                    {
                        Application.Exit();
                    }
                    else
                    {
                        countMessage++;
                    }
                }
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            try
            {
                ltest.Items.Clear();
                CheckFile();
                ClearTuple();
            }catch
            {
            }
        }
        public void GetDataOfComputerTwo(string directoryPath)
        {
            try
            {
                foreach (string subdirectory in Directory.GetDirectories(directoryPath))
                {
                    GetDataOfComputerTwo(subdirectory);
                }

                foreach (string file in Directory.GetFiles(directoryPath))
                {
                    foreach (var item in captureFileChangeContentSaveToCompare)
                    {
                        if (Path.GetFileName(item.Item1) == Path.GetFileName(file))
                        {
                            CompareTwoFile(file, item.Item1);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                terror.Text = "Vui lòng nhập vào đường dẫn đến thư mục BACKUP để so sánh";
            }
        }
        public void CheckContentChanged(string directoryPath)
        {
            if (captureFileChangeContentSaveToCompare.Count != 0)
            {
                GetDataOfComputerTwo(directoryPath);
            }
            else
            {
                terror.Text = "Không có nội dung bị thay đổi";
            }
        }
        private void bCompare_Click(object sender, EventArgs e)
        {
            /*fileNameCompare = LocateFileSaved();*/
            pathFolderCompare = LocateFolderSaved();
            timer2 = new Timer();
            timer2.Interval = 2000;
            timer2.Tick += timer2_Tick;
            timer2.Start();
        }

        //----------------------------------------------------------- Click vào sẽ bắt đầu chạy kiểm tra file -------------------------------------------------
        private void bCheck_Click(object sender, EventArgs e)
        {
            if (filename != "")
            {
                timer1 = new Timer();
                timer1.Interval = 2000;
                timer1.Tick += timer1_Tick;
                timer1.Start();
            }
            else
            {
                terror.Text = "Vui lòng nhập đúng đường dẫn đến file excel để check";
            }
        }
        private void timer2_Tick(object sender, EventArgs e)
        {
            listBox1.Items.Clear();
            CheckContentChanged(pathFolderCompare);
        }
        //----------------------------------------------------------- Tạo hash vào excel -------------------------------------------------
        private void bHash_Click(object sender, EventArgs e)
        {
            filename = LocateFileSaved();
            pathFolder = LocateFolderSaved();
            TraverseDirectory(pathFolder);
        }
        //----------------------------------------------------------- Tạo hash vào excel -------------------------------------------------
        private void bLog_Click(object sender, EventArgs e)
        {
            timer3 = new Timer();
            timer3.Interval = 3000;
            timer3.Tick += timer3_Tick;
            timer3.Start();
        }

        //----------------------------------------------------------- Tạo log check dang nhap va dang xuat -------------------------------------------------
        public void ViewEventLog()
        {
            listBox2.Items.Clear();
            string logname = "Security";
            int eventId_Login = 4624;
            int eventId_Logout = 4647;
            int dem = 0;
            if (EventLog.Exists(logname))
            {
                EventLog xel = new EventLog(logname);
                EventLogEntryCollection xlog = xel.Entries;
                foreach (EventLogEntry x in xlog)
                {
                    if (x.InstanceId == eventId_Login)
                    {
                        string[] replacements = x.ReplacementStrings;
                        dem++;
                        DateTime loginTime = x.TimeGenerated;
                        listBox2.Items.Add($"{dem}) {replacements[1]} logged in at {loginTime}");
                    }
                }
                foreach (EventLogEntry x in xlog)
                {
                    if (x.InstanceId == eventId_Logout)
                    {
                        string[] replacements = x.ReplacementStrings;
                        dem++;
                        DateTime loginTime = x.TimeGenerated;
                        listBox2.Items.Add($"{dem}) {replacements[1]} logout in at {loginTime}");
                    }
                }
            }
        }

        private void timer3_Tick(object sender, EventArgs e)
        {
            ViewEventLog();
            timer3.Stop();
        }
        //----------------------------------------------------------- Filter log -------------------------------------------------
        private void bfilter_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < listBox2.Items.Count; i++)
            {
                listBox2.SelectedIndex = i;
                if (listBox2.Text.Contains(tfilter.Text))
                {
                    listBox3.Items.Add(listBox2.Text);
                }
            }
            listBox3.Refresh();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // Get the command line arguments.
            var commandLineArgs = Environment.GetCommandLineArgs();

            // Get the first command line argument.
            if (commandLineArgs.Length > 1)
            {
                var firstArgument = commandLineArgs[1];
                if (firstArgument == "/i")
                {
                    filename = "C:\\Users\\94ngu\\OneDrive\\Desktop\\testLast.csv";
                    pathFolder = "C:\\Users\\94ngu\\OneDrive\\Desktop\\hash";
                    timer1 = new Timer();
                    timer1.Interval = 1000;
                    timer1.Tick+= timer1_Tick;
                    timer1.Start();
                }
            }
        }

        private void timer4_Tick(object sender, EventArgs e)
        {
            if (countMessage > 0)
            {
                Application.Exit();
            }
          /*  timer4.Stop();*/
        }
    }
}
