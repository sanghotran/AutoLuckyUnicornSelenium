using KAutoHelper;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AutoLuckyUnicornSelenium
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        public struct Pet
        {
            public int id;
            public String element;
            public String priority;
            public String status;
            public bool flag_check_easy;
            public bool flag_check_medium;
        }
        public Pet pet;
        public enum State
        {
            Play,
            Idle,
            Easy,
            Check,
            CheckRefresh,
            CheckPet,
            Battle,
            Roll,
            Medium,
            Confirm,
            Recive,
            Back

        }
        State state = State.Play;

        public ChromeDriver chromeDriver;
        public ChromeOptions chromeOptions;
        public Action action;

        public bool time_finish = false;
        public bool flag_finish = false;
        public bool flag_battle = false;

        public int TimeCount;
        public int count;

        private void btn_Open_Click(object sender, EventArgs e)
        {
            ChromeOptions option = new ChromeOptions();

            option.AddArgument("user-data-dir=" + @"E:\Project\Program\C_Sharp\AutoLuckyUnicornSelenium\bin\Debug\profile\UserData");
            ChromeDriver chromeDriver = new ChromeDriver(option);

            chromeDriver.Url = "https://app.luckyunicorn.io/";
            chromeDriver.Navigate();
            chromeDriver.FindElement(By.XPath("/html/body/div[17]/div/div/section/div/button")).Click();
            DialogResult result = MessageBox.Show("Vui lòng nhập pass metamask.\nNhập xong rồi hãy nhấn Yes", "Warning", MessageBoxButtons.YesNo);
            switch(result)
            {
                case DialogResult.Yes:
                    while(true)
                    {
                        switch(state)
                        {
                            case State.Idle:
                                for (int i = 0; i < TimeCount; i++)
                                {
                                    Thread.Sleep(60000);
                                    time.Text = (TimeCount - i).ToString();                                    
                                }
                                state = State.Play;
                                _ResetStatus();
                                count = 15;
                                break;
                            case State.Play:
                                chromeDriver.Url = "https://app.luckyunicorn.io/play-to-earn";
                                chromeDriver.Navigate();
                                chromeDriver.FindElement(By.XPath("/html/body/div[17]/div/div/section/div/button")).Click();
                                state = State.Easy;
                                Thread.Sleep(5000);
                                break;
                            case State.Roll:
                                try
                                {
                                    var Shuffle = chromeDriver.FindElement(By.XPath("//*[@id=\"root\"]/main/section/div[1]/ul/li[1]/div/div[1]/p/button"));
                                    Shuffle.Click();
                                    state = State.Confirm;
                                }
                                catch (Exception ex)
                                { 
                                }
                                break;
                            case State.Recive:
                                try
                                {
                                    var Recive = chromeDriver.FindElement(By.XPath("/html/body/div[13]/div/div/section/div/button[2]"));
                                    Recive.Click();
                                    count -= 1;
                                    if(count == 0)
                                        state = State.Idle;
                                    else
                                        state = State.Play;
                                }
                                catch (Exception ex)
                                {
                                }
                                break;
                            case State.Confirm:
                                try
                                {
                                    Thread.Sleep(20000);
                                    //var image = chromeDriver.FindElement(By.XPath(""));
                                    var hWnd = IntPtr.Zero;
                                    hWnd = AutoControl.FindWindowHandle(null, "MetaMask Notification");
                                    for (int i = 0; i < 8; i++)
                                    {
                                        AutoControl.SendKeyBoardPress(hWnd, VKeys.VK_TAB);
                                        Thread.Sleep(200);
                                    }                                       
                                    AutoControl.SendKeyBoardPress(hWnd, VKeys.VK_RETURN);
                                    if (flag_battle == true)
                                    {
                                        state = State.Recive;
                                    }
                                    else
                                    {
                                        Thread.Sleep(20000);
                                        state = State.Play;
                                    }
                                }
                                catch(Exception ex)
                                {

                                }
                                break;
                            case State.Easy:
                                try
                                {
                                    var image = chromeDriver.FindElement(By.XPath("//*[@id=\"root\"]/main/section/div[1]/ul/li[1]/div/h3/img"));
                                    //var battle = chromeDriver.FindElement(By.XPath("//*[@id=\"root\"]/main/section/div[1]/ul/li[1]/div/div[2]/button[1]"));
                                    var swap = chromeDriver.FindElement(By.XPath("//*[@id=\"root\"]/main/section/div[2]/div[1]/div[2]/div[1]/div[2]/button[2]/img"));
                                    switch(image.GetAttribute("src"))
                                    {
                                        case "https://app.luckyunicorn.io/assets/elements/Wood.svg":
                                            _CheckStatus(1);
                                            if(pet.status == "Yes")
                                            {
                                                status1.Text = "No";
                                                Thread.Sleep(1000);
                                                var battle = chromeDriver.FindElement(By.XPath("//*[@id=\"root\"]/main/section/div[1]/ul/li[1]/div/div[2]/button[1]"));
                                                battle.Click();
                                                state = State.Confirm;
                                                flag_battle = true;
                                                break;
                                            }
                                            state = State.Medium;
                                            break;
                                        case "https://app.luckyunicorn.io/assets/elements/Metal.svg":
                                            _CheckStatus(9);
                                            if(pet.status == "Yes")
                                            {
                                                for (int j = 0; j < 8; j++)
                                                    swap.Click();
                                                status9.Text = "No";
                                                Thread.Sleep(1000);
                                                var battle = chromeDriver.FindElement(By.XPath("//*[@id=\"root\"]/main/section/div[1]/ul/li[1]/div/div[2]/button[1]"));
                                                battle.Click();
                                                state = State.Confirm;
                                                flag_battle = true;
                                                break;
                                            }
                                            state = State.Medium;
                                            break;
                                        case "https://app.luckyunicorn.io/assets/elements/Light.svg":
                                            _CheckStatus(13);
                                            if(pet.status == "Yes")
                                            {
                                                for (int j = 0; j < 12; j++)
                                                    swap.Click();
                                                status13.Text = "No";
                                                Thread.Sleep(1000);
                                                var battle = chromeDriver.FindElement(By.XPath("//*[@id=\"root\"]/main/section/div[1]/ul/li[1]/div/div[2]/button[1]"));
                                                battle.Click();
                                                state = State.Confirm;
                                                flag_battle = true;
                                                break;
                                            }
                                            state = State.Medium;
                                            break;
                                        case "https://app.luckyunicorn.io/assets/elements/Fire.svg":
                                            state = State.Medium;
                                            break;
                                        case "https://app.luckyunicorn.io/assets/elements/Earth.svg":
                                            /*_CheckStatus(14);
                                            if (pet.status == "Yes")
                                            {
                                                for (int j = 0; j < 13; j++)
                                                    swap.Click();
                                                status14.Text = "No";
                                                Thread.Sleep(1000);
                                                var battle = chromeDriver.FindElement(By.XPath("//*[@id=\"root\"]/main/section/div[1]/ul/li[1]/div/div[2]/button[1]"));
                                                battle.Click();
                                                state = State.Confirm;
                                                flag_battle = true;
                                                break;
                                            }*/
                                            state = State.Medium;
                                            break;
                                        case "https://app.luckyunicorn.io/assets/elements/Water.svg":
                                            _CheckStatus(2);
                                            if (pet.status == "Yes")
                                            {
                                                swap.Click();
                                                status2.Text = "No";
                                                Thread.Sleep(1000);
                                                var battle = chromeDriver.FindElement(By.XPath("//*[@id=\"root\"]/main/section/div[1]/ul/li[1]/div/div[2]/button[1]"));
                                                battle.Click();
                                                state = State.Confirm;
                                                flag_battle = true;
                                                break;
                                            }
                                            _CheckStatus(8);
                                            if(pet.status == "Yes")
                                            {
                                                for (int j = 0; j < 7; j++)
                                                    swap.Click();
                                                status7.Text = "No";
                                                Thread.Sleep(1000);
                                                var battle = chromeDriver.FindElement(By.XPath("//*[@id=\"root\"]/main/section/div[1]/ul/li[1]/div/div[2]/button[1]"));
                                                battle.Click();
                                                state = State.Confirm;
                                                flag_battle = true;
                                                break;
                                            }
                                            _CheckStatus(12);
                                            if (pet.status == "Yes")
                                            {
                                                for (int j = 0; j < 11; j++)
                                                    swap.Click();
                                                status12.Text = "No";
                                                Thread.Sleep(1000);
                                                var battle = chromeDriver.FindElement(By.XPath("//*[@id=\"root\"]/main/section/div[1]/ul/li[1]/div/div[2]/button[1]"));
                                                battle.Click();
                                                state = State.Confirm;
                                                flag_battle = true;
                                                break;
                                            }
                                            _CheckStatus(15);
                                            if (pet.status == "Yes")
                                            {
                                                for (int j = 0; j < 14; j++)
                                                    swap.Click();
                                                status15.Text = "No";
                                                Thread.Sleep(1000);
                                                var battle = chromeDriver.FindElement(By.XPath("//*[@id=\"root\"]/main/section/div[1]/ul/li[1]/div/div[2]/button[1]"));
                                                battle.Click();
                                                state = State.Confirm;
                                                flag_battle = true;
                                                break;
                                            }
                                            _CheckStatus(4);
                                            if (pet.status == "Yes")
                                            {
                                                for (int j = 0; j < 3; j++)
                                                    swap.Click();
                                                status4.Text = "No";
                                                Thread.Sleep(1000);
                                                var battle = chromeDriver.FindElement(By.XPath("//*[@id=\"root\"]/main/section/div[1]/ul/li[1]/div/div[2]/button[1]"));
                                                battle.Click();
                                                state = State.Confirm;
                                                flag_battle = true;
                                                break;
                                            }
                                            _CheckStatus(5);
                                            if (pet.status == "Yes")
                                            {
                                                for (int j = 0; j < 4; j++)
                                                    swap.Click();
                                                status5.Text = "No";
                                                Thread.Sleep(1000);
                                                var battle = chromeDriver.FindElement(By.XPath("//*[@id=\"root\"]/main/section/div[1]/ul/li[1]/div/div[2]/button[1]"));
                                                battle.Click();
                                                state = State.Confirm;
                                                flag_battle = true;
                                                break;
                                            }
                                            state = State.Medium;
                                            break;
                                        case "https://app.luckyunicorn.io/assets/elements/Dark.svg":
                                            _CheckStatus(6);
                                            if (pet.status == "Yes")
                                            {
                                                for (int j = 0; j < 5; j++)
                                                    swap.Click();
                                                status6.Text = "No";
                                                Thread.Sleep(1000);
                                                var battle = chromeDriver.FindElement(By.XPath("//*[@id=\"root\"]/main/section/div[1]/ul/li[1]/div/div[2]/button[1]"));
                                                battle.Click();
                                                state = State.Confirm;
                                                flag_battle = true;
                                                break;
                                            }
                                            _CheckStatus(10);
                                            if (pet.status == "Yes")
                                            {
                                                for (int j = 0; j < 9; j++)
                                                    swap.Click();
                                                status10.Text = "No";
                                                Thread.Sleep(1000);
                                                var battle = chromeDriver.FindElement(By.XPath("//*[@id=\"root\"]/main/section/div[1]/ul/li[1]/div/div[2]/button[1]"));
                                                battle.Click();
                                                state = State.Confirm;
                                                flag_battle = true;
                                                break;
                                            }
                                            _CheckStatus(3);
                                            if (pet.status == "Yes")
                                            {
                                                for (int j = 0; j < 2; j++)
                                                    swap.Click();
                                                status3.Text = "No";
                                                Thread.Sleep(1000);
                                                var battle = chromeDriver.FindElement(By.XPath("//*[@id=\"root\"]/main/section/div[1]/ul/li[1]/div/div[2]/button[1]"));
                                                battle.Click();
                                                state = State.Confirm;
                                                flag_battle = true;
                                                break;
                                            }
                                            _CheckStatus(7);
                                            if (pet.status == "Yes")
                                            {
                                                for (int j = 0; j < 6; j++)
                                                    swap.Click();
                                                status7.Text = "No";
                                                Thread.Sleep(1000);
                                                var battle = chromeDriver.FindElement(By.XPath("//*[@id=\"root\"]/main/section/div[1]/ul/li[1]/div/div[2]/button[1]"));
                                                battle.Click();
                                                state = State.Confirm;
                                                flag_battle = true;
                                                break;
                                            }
                                            _CheckStatus(11);
                                            if (pet.status == "Yes")
                                            {
                                                for (int j = 0; j < 10; j++)
                                                    swap.Click();
                                                status11.Text = "No";
                                                Thread.Sleep(1000);
                                                var battle = chromeDriver.FindElement(By.XPath("//*[@id=\"root\"]/main/section/div[1]/ul/li[1]/div/div[2]/button[1]"));
                                                battle.Click();
                                                state = State.Confirm;
                                                flag_battle = true;
                                                break;
                                            }
                                            state = State.Medium;
                                            break;
                                    }
                                }
                                catch(Exception ex)
                                {

                                }
                                break;
                            case State.Medium:
                                try
                                {
                                    var image = chromeDriver.FindElement(By.XPath("//*[@id=\"root\"]/main/section/div[1]/ul/li[2]/div/h3/img"));
                                    //var battle = chromeDriver.FindElement(By.XPath("//*[@id=\"root\"]/main/section/div[1]/ul/li[2]/div/div[2]/button[1]"));
                                    var swap = chromeDriver.FindElement(By.XPath("//*[@id=\"root\"]/main/section/div[2]/div[1]/div[2]/div[1]/div[2]/button[2]/img"));
                                    switch (image.GetAttribute("src"))
                                    {
                                        case "https://app.luckyunicorn.io/assets/elements/Wood.svg":
                                            _CheckStatus(1);
                                            if (pet.status == "Yes")
                                            {
                                                status1.Text = "No";
                                                Thread.Sleep(1000);
                                                var battle = chromeDriver.FindElement(By.XPath("//*[@id=\"root\"]/main/section/div[1]/ul/li[2]/div/div[2]/button[1]"));
                                                battle.Click();
                                                state = State.Confirm;
                                                flag_battle = true;
                                                break;
                                            }
                                            state = State.Roll;
                                            break;
                                        case "https://app.luckyunicorn.io/assets/elements/Metal.svg":
                                            state = State.Roll;
                                            break;
                                        case "https://app.luckyunicorn.io/assets/elements/Light.svg":
                                            _CheckStatus(13);
                                            if (pet.status == "Yes")
                                            {
                                                for (int j = 0; j < 12; j++)
                                                    swap.Click();
                                                status13.Text = "No";
                                                Thread.Sleep(1000);
                                                var battle = chromeDriver.FindElement(By.XPath("//*[@id=\"root\"]/main/section/div[1]/ul/li[2]/div/div[2]/button[1]"));
                                                battle.Click();
                                                state = State.Confirm;
                                                flag_battle = true;
                                                break;
                                            }
                                            state = State.Roll;
                                            break;
                                        case "https://app.luckyunicorn.io/assets/elements/Fire.svg":
                                            state = State.Roll;
                                            break;
                                        case "https://app.luckyunicorn.io/assets/elements/Earth.svg":
                                            state = State.Roll;
                                            break;
                                        case "https://app.luckyunicorn.io/assets/elements/Water.svg":
                                            _CheckStatus(4);
                                            if (pet.status == "Yes")
                                            {
                                                for (int j = 0; j < 3; j++)
                                                    swap.Click();
                                                status4.Text = "No";
                                                Thread.Sleep(1000);
                                                var battle = chromeDriver.FindElement(By.XPath("//*[@id=\"root\"]/main/section/div[1]/ul/li[2]/div/div[2]/button[1]"));
                                                battle.Click();
                                                state = State.Confirm;
                                                flag_battle = true;
                                                break;
                                            }
                                            _CheckStatus(5);
                                            if (pet.status == "Yes")
                                            {
                                                for (int j = 0; j < 4; j++)
                                                    swap.Click();
                                                status5.Text = "No";
                                                Thread.Sleep(1000);
                                                var battle = chromeDriver.FindElement(By.XPath("//*[@id=\"root\"]/main/section/div[1]/ul/li[2]/div/div[2]/button[1]"));
                                                battle.Click();
                                                state = State.Confirm;
                                                flag_battle = true;
                                                break;
                                            }
                                            state = State.Roll;
                                            break;
                                        case "https://app.luckyunicorn.io/assets/elements/Dark.svg":
                                            _CheckStatus(3);
                                            if (pet.status == "Yes")
                                            {
                                                for (int j = 0; j < 2; j++)
                                                    swap.Click();
                                                status3.Text = "No";
                                                Thread.Sleep(1000);
                                                var battle = chromeDriver.FindElement(By.XPath("//*[@id=\"root\"]/main/section/div[1]/ul/li[2]/div/div[2]/button[1]"));
                                                battle.Click();
                                                state = State.Confirm;
                                                flag_battle = true;
                                                break;
                                            }
                                            _CheckStatus(7);
                                            if (pet.status == "Yes")
                                            {
                                                for (int j = 0; j < 6; j++)
                                                    swap.Click();
                                                status7.Text = "No";
                                                Thread.Sleep(1000);
                                                var battle = chromeDriver.FindElement(By.XPath("//*[@id=\"root\"]/main/section/div[1]/ul/li[2]/div/div[2]/button[1]"));
                                                battle.Click();
                                                state = State.Confirm;
                                                flag_battle = true;
                                                break;
                                            }
                                            _CheckStatus(11);
                                            if (pet.status == "Yes")
                                            {
                                                for (int j = 0; j < 10; j++)
                                                    swap.Click();
                                                status11.Text = "No";
                                                Thread.Sleep(1000);
                                                var battle = chromeDriver.FindElement(By.XPath("//*[@id=\"root\"]/main/section/div[1]/ul/li[2]/div/div[2]/button[1]"));
                                                battle.Click();
                                                state = State.Confirm;
                                                flag_battle = true;
                                                break;
                                            }
                                            state = State.Roll;
                                            break;
                                    }
                                }
                                catch(Exception ex)
                                {

                                }
                                break;
                        }                     
                    }
                    break;
                case DialogResult.No:

                    break;
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            state = State.Idle;
            count = 15;
        }

        private void btn_SetTime_Click(object sender, EventArgs e)
        {
            TimeCount = Int32.Parse(txt_TimeCount.Text);
            time.Text = TimeCount.ToString();
        }

        public void _ResetStatus()
        {
            status1.Text = "Yes";
            status2.Text = "Yes";
            status3.Text = "Yes";
            status4.Text = "Yes";
            status5.Text = "Yes";
            status6.Text = "Yes";
            status7.Text = "Yes";
            status8.Text = "Yes";
            status9.Text = "Yes";
            status10.Text = "Yes";
            status11.Text = "Yes";
            status12.Text = "Yes";
            status13.Text = "Yes";
            status14.Text = "Yes";
            status15.Text = "Yes";
        }
        public void _CheckStatus(int i)
        {
            switch (i)
            {
                case 1:
                    pet.status = status1.Text;
                    break;
                case 2:
                    pet.status = status2.Text;
                    break;
                case 3:
                    pet.status = status3.Text;
                    break;
                case 4:
                    pet.status = status4.Text;
                    break;
                case 5:
                    pet.status = status5.Text;
                    break;
                case 6:
                    pet.status = status6.Text;
                    break;
                case 7:
                    pet.status = status7.Text;
                    break;
                case 8:
                    pet.status = status8.Text;
                    break;
                case 9:
                    pet.status = status9.Text;
                    break;
                case 10:
                    pet.status = status10.Text;
                    break;
                case 11:
                    pet.status = status11.Text;
                    break;
                case 12:
                    pet.status = status12.Text;
                    break;
                case 13:
                    pet.status = status13.Text;
                    break;
                case 14:
                    pet.status = status14.Text;
                    break;
                case 15:
                    pet.status = status15.Text;
                    break;
            }
        }
    }
    
}
