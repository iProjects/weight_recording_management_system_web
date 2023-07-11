using System;
using System.Collections;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Threading;
using System.Windows.Forms;
using System.Xml;
using Microsoft.Win32;
using weight_recording_ui.reports;

namespace weight_recording_ui
{
    /// <summary>
    /// Summary description for SplashScreen.
    /// </summary>
    public class SplashScreen : System.Windows.Forms.Form
    {
        // Threading
        static SplashScreen ms_frmSplash = null;
        static Thread ms_oThread = null;

        // Fade in and out.
        private double m_dblOpacityIncrement = .05;
        private double m_dblOpacityDecrement = .08;
        private const int TIMER_INTERVAL = 50;

        // Status and progress bar
        static string ms_sStatus;
        private double m_dblCompletionFraction = 0;
        private Rectangle m_rProgress;

        // Progress smoothing
        private double m_dblLastCompletionFraction = 0.0;
        private double m_dblPBIncrementPerTimerInterval = .015;

        // Self-calibration support
        private bool m_bFirstLaunch = false;
        private DateTime m_dtStart;
        private bool m_bDTSet = false;
        private int m_iIndex = 1;
        private int m_iActualTicks = 0;
        private ArrayList m_alPreviousCompletionFraction;
        private ArrayList m_alActualTimes = new ArrayList();
        private const string REG_KEY_INITIALIZATION = "Initialization";
        private const string REGVALUE_PB_MILISECOND_INCREMENT = "Increment";
        private const string REGVALUE_PB_PERCENTS = "Percents";
        private System.Windows.Forms.Timer timer1;
        private NotifyIcon appNotifyIcon;
        private Panel pnlStatus;
        private Label lblTimeRemaining;
        private Label lblStatus;
        private Label lblbuildversion;
        private Label lblappname;
        private System.ComponentModel.IContainer components;
        public static string app_name = "weight recording app";

        /// <summary>
        /// Constructor
        /// </summary>
        public SplashScreen()
        {
            InitializeComponent();
            this.Opacity = .00;
            timer1.Interval = TIMER_INTERVAL;
            timer1.Start();
            if (this.BackgroundImage != null)
                this.ClientSize = this.BackgroundImage.Size;
        }

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (components != null)
                {
                    components.Dispose();
                }
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code
        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SplashScreen));
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.appNotifyIcon = new System.Windows.Forms.NotifyIcon(this.components);
            this.pnlStatus = new System.Windows.Forms.Panel();
            this.lblTimeRemaining = new System.Windows.Forms.Label();
            this.lblStatus = new System.Windows.Forms.Label();
            this.lblbuildversion = new System.Windows.Forms.Label();
            this.lblappname = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // timer1
            // 
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // appNotifyIcon
            // 
            this.appNotifyIcon.Text = "notifyIcon1";
            this.appNotifyIcon.Visible = true;
            // 
            // pnlStatus
            // 
            this.pnlStatus.BackColor = System.Drawing.Color.Black;
            this.pnlStatus.Location = new System.Drawing.Point(108, 52);
            this.pnlStatus.Name = "pnlStatus";
            this.pnlStatus.Size = new System.Drawing.Size(279, 24);
            this.pnlStatus.TabIndex = 1;
            this.pnlStatus.Paint += new System.Windows.Forms.PaintEventHandler(this.pnlStatus_Paint);
            // 
            // lblTimeRemaining
            // 
            this.lblTimeRemaining.BackColor = System.Drawing.Color.Transparent;
            this.lblTimeRemaining.Location = new System.Drawing.Point(108, 83);
            this.lblTimeRemaining.Name = "lblTimeRemaining";
            this.lblTimeRemaining.Size = new System.Drawing.Size(279, 16);
            this.lblTimeRemaining.TabIndex = 2;
            this.lblTimeRemaining.Text = "Time remaining";
            // 
            // lblStatus
            // 
            this.lblStatus.BackColor = System.Drawing.Color.Transparent;
            this.lblStatus.ForeColor = System.Drawing.Color.Black;
            this.lblStatus.Location = new System.Drawing.Point(108, 30);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(279, 14);
            this.lblStatus.TabIndex = 0;
            this.lblStatus.Text = "progress";
            // 
            // lblbuildversion
            // 
            this.lblbuildversion.BackColor = System.Drawing.Color.Transparent;
            this.lblbuildversion.Location = new System.Drawing.Point(108, 151);
            this.lblbuildversion.Name = "lblbuildversion";
            this.lblbuildversion.Size = new System.Drawing.Size(279, 16);
            this.lblbuildversion.TabIndex = 3;
            this.lblbuildversion.Text = "build version";
            // 
            // lblappname
            // 
            this.lblappname.BackColor = System.Drawing.Color.Transparent;
            this.lblappname.Location = new System.Drawing.Point(108, 112);
            this.lblappname.Name = "lblappname";
            this.lblappname.Size = new System.Drawing.Size(279, 16);
            this.lblappname.TabIndex = 4;
            this.lblappname.Text = "app name";
            // 
            // SplashScreen
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(576, 362);
            this.Controls.Add(this.lblappname);
            this.Controls.Add(this.lblbuildversion);
            this.Controls.Add(this.lblTimeRemaining);
            this.Controls.Add(this.pnlStatus);
            this.Controls.Add(this.lblStatus);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "SplashScreen";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = " Loading..........";
            this.TopMost = true;
            this.Load += new System.EventHandler(this.SplashScreen_Load);
            this.ResumeLayout(false);

        }
        #endregion

        // ************* Static Methods *************** //

        // A static method to create the thread and 
        // launch the SplashScreen.
        static public void ShowSplashScreen()
        {
            // Make sure it's only launched once.
            if (ms_frmSplash != null)
                return;
            ms_oThread = new Thread(new ThreadStart(SplashScreen.ShowForm));
            ms_oThread.IsBackground = true;
            ms_oThread.SetApartmentState(ApartmentState.STA);
            //Start  the Thread.
            ms_oThread.Start();
            while (ms_frmSplash == null || ms_frmSplash.IsHandleCreated == false)
            {
                //put the Main thread to sleep to allow oThread to do some work.
                System.Threading.Thread.Sleep(TIMER_INTERVAL);
            }
        }

        // A property returning the splash screen instance
        static public SplashScreen SplashForm
        {
            get
            {
                return ms_frmSplash;
            }
        }

        // A private entry point for the thread.
        static private void ShowForm()
        {
            ms_frmSplash = new SplashScreen();
            Application.Run(ms_frmSplash);
        }

        // A static method to close the SplashScreen
        // UPDATE : http://www.codeproject.com/KB/cs/prettygoodsplashscreen.aspx?df=100&forumid=26207&mpp=50&noise=1&fr=51&select=2073339&fid=26207#xx2073339xx
        //A static method to close the SplashScreen
        static public void CloseForm(Form in_parentForm)
        {
            // Update the owner a thread safe fashion!
            MethodInvoker SetOwner = delegate
            {
                ms_frmSplash.Owner = in_parentForm;
            };

            if (ms_frmSplash != null)
            {
                // Make it start going away.
                ms_frmSplash.m_dblOpacityIncrement = -ms_frmSplash.m_dblOpacityDecrement;

                if (ms_frmSplash.InvokeRequired)
                    ms_frmSplash.Invoke(SetOwner);
                else
                    SetOwner();
            }
            ms_oThread.Abort();
            ms_oThread = null; // we do not need these any more.
            ms_frmSplash = null;
        }

        static public void CloseForm()
        {
            if (ms_frmSplash != null && ms_frmSplash.IsDisposed == false)
            {
                // Make it start going away.
                ms_frmSplash.m_dblOpacityIncrement = -ms_frmSplash.m_dblOpacityDecrement;
            }
            ms_oThread.Abort();
            ms_oThread = null;	// we don't need these any more.
            ms_frmSplash = null;
        }

        // A static method to set the status and update the reference.
        static public void SetStatus(string newStatus)
        {
            SetStatus(newStatus, true);
        }

        // A static method to set the status and optionally update the reference.
        // This is useful if you are in a section of code that has a variable
        // set of status string updates.  In that case, don't set the reference.
        static public void SetStatus(string newStatus, bool setReference)
        {
            ms_sStatus = newStatus;
            if (ms_frmSplash == null)
                return;
            if (setReference)
                ms_frmSplash.SetReferenceInternal();
        }

        // Static method called from the initializing application to 
        // give the splash screen reference points.  Not needed if
        // you are using a lot of status strings.
        static public void SetReferencePoint()
        {
            if (ms_frmSplash == null)
                return;
            ms_frmSplash.SetReferenceInternal();

        }

        // ************ Private methods ************

        // Internal method for setting reference points.
        private void SetReferenceInternal()
        {
            if (m_bDTSet == false)
            {
                m_bDTSet = true;
                m_dtStart = DateTime.Now;
                ReadIncrements();
            }
            double dblMilliseconds = ElapsedMilliSeconds();
            m_alActualTimes.Add(dblMilliseconds);
            m_dblLastCompletionFraction = m_dblCompletionFraction;
            if (m_alPreviousCompletionFraction != null && m_iIndex < m_alPreviousCompletionFraction.Count)
                m_dblCompletionFraction = (double)m_alPreviousCompletionFraction[m_iIndex++];
            else
                m_dblCompletionFraction = (m_iIndex > 0) ? 1 : 0;
        }

        // Utility function to return elapsed Milliseconds since the 
        // SplashScreen was launched.
        private double ElapsedMilliSeconds()
        {
            TimeSpan ts = DateTime.Now - m_dtStart;
            return ts.TotalMilliseconds;
        }

        // Function to read the checkpoint intervals from the previous invocation of the
        // splashscreen from the registry.
        private void ReadIncrements()
        {
            //UPDATE : http://www.codeproject.com/KB/cs/prettygoodsplashscreen.aspx?df=100&forumid=26207&mpp=50&noise=1&fr=1&select=2603941&fid=26207#xx2603941xx
            string sPBIncrementPerTimerInterval = TimeStampObserverDataLayer.GetStringValue(REGVALUE_PB_MILISECOND_INCREMENT, "0.0015");
            double dblResult;

            if (Double.TryParse(sPBIncrementPerTimerInterval, System.Globalization.NumberStyles.Float, System.Globalization.NumberFormatInfo.InvariantInfo, out dblResult) == true)
                m_dblPBIncrementPerTimerInterval = dblResult;
            else
                m_dblPBIncrementPerTimerInterval = .0015;

            //UPDATE : http://www.codeproject.com/KB/cs/prettygoodsplashscreen.aspx?df=100&forumid=26207&mpp=50&noise=1&fr=1&select=2603941&fid=26207#xx2603941xx
            string sPBPreviousPctComplete = TimeStampObserverDataLayer.GetStringValue(REGVALUE_PB_PERCENTS, "");

            if (sPBPreviousPctComplete != "")
            {
                string[] aTimes = sPBPreviousPctComplete.Split(null);
                m_alPreviousCompletionFraction = new ArrayList();

                for (int i = 0; i < aTimes.Length; i++)
                {
                    double dblVal;
                    if (Double.TryParse(aTimes[i], System.Globalization.NumberStyles.Float, System.Globalization.NumberFormatInfo.InvariantInfo, out dblVal))
                        m_alPreviousCompletionFraction.Add(dblVal);
                    else
                        m_alPreviousCompletionFraction.Add(1.0);
                }
            }
            else
            {
                m_bFirstLaunch = true;
                // UPDATE :http://www.codeproject.com/KB/cs/prettygoodsplashscreen.aspx?df=100&forumid=26207&mpp=50&noise=1&fr=1&select=2771540&fid=26207#xx2771540xx
                SetRemainintTimeCrossThread("");
            }
        }

        // Method to store the intervals (in percent complete) from the current invocation of
        // the splash screen to the registry.
        private void StoreIncrements()
        {
            string sPercent = "";
            double dblElapsedMilliseconds = ElapsedMilliSeconds();
            for (int i = 0; i < m_alActualTimes.Count; i++)
                sPercent += ((double)m_alActualTimes[i] / dblElapsedMilliseconds).ToString("0.####", System.Globalization.NumberFormatInfo.InvariantInfo) + " ";

            //http://www.codeproject.com/KB/cs/prettygoodsplashscreen.aspx?df=100&forumid=26207&mpp=50&noise=1&fr=1&select=2603941&fid=26207#xx2603941xx
            TimeStampObserverDataLayer.SetStringValue(REGVALUE_PB_PERCENTS, sPercent);

            m_dblPBIncrementPerTimerInterval = 1.0 / (double)m_iActualTicks;
            //http://www.codeproject.com/KB/cs/prettygoodsplashscreen.aspx?df=100&forumid=26207&mpp=50&noise=1&fr=1&select=2603941&fid=26207#xx2603941xx
            TimeStampObserverDataLayer.SetStringValue(REGVALUE_PB_MILISECOND_INCREMENT, m_dblPBIncrementPerTimerInterval.ToString("#.000000", System.Globalization.NumberFormatInfo.InvariantInfo));
        }

        //********* Event Handlers ************

        // Tick Event handler for the Timer control.  Handle fade in and fade out.  Also
        // handle the smoothed progress bar.
        private void timer1_Tick(object sender, System.EventArgs e)
        {
            lblStatus.Text = ms_sStatus;

            if (m_dblOpacityIncrement > 0)
            {
                m_iActualTicks++;
                if (this.Opacity < 1)
                    this.Opacity += m_dblOpacityIncrement;
            }
            else
            {
                if (this.Opacity > 0)
                    this.Opacity += m_dblOpacityIncrement;
                else
                {
                    StoreIncrements();
                    // UPDATE : http://www.codeproject.com/KB/cs/prettygoodsplashscreen.aspx?df=100&forumid=26207&noise=2&mpp=50&select=1364609&fid=26207&fr=101#xx1364609xx
                    timer1.Stop();
                    this.Close();
                    Debug.WriteLine("Called this.Close()");
                }
            }

            if (m_bFirstLaunch == false && m_dblLastCompletionFraction < m_dblCompletionFraction)
            {
                m_dblLastCompletionFraction += m_dblPBIncrementPerTimerInterval;
                int width = (int)Math.Floor(pnlStatus.ClientRectangle.Width * m_dblLastCompletionFraction);
                int height = pnlStatus.ClientRectangle.Height;
                int x = pnlStatus.ClientRectangle.X;
                int y = pnlStatus.ClientRectangle.Y;
                if (width > 0 && height > 0)
                {
                    m_rProgress = new Rectangle(x, y, width, height);
                    // UPDATE : http://www.codeproject.com/KB/cs/prettygoodsplashscreen.aspx?df=100&forumid=26207&noise=2&mpp=50&select=1392388&fid=26207&fr=101#xx1392388xx
                    //pnlStatus.Invalidate(m_rProgress);
                    if (!pnlStatus.IsDisposed)
                    {
                        Graphics g = pnlStatus.CreateGraphics();
                        LinearGradientBrush brBackground = new LinearGradientBrush(m_rProgress, Color.FromArgb(58, 96, 151), Color.FromArgb(181, 237, 254), LinearGradientMode.Horizontal);
                        g.FillRectangle(brBackground, m_rProgress);
                        g.Dispose();
                    }
                    int iSecondsLeft = 1 + (int)(TIMER_INTERVAL * ((1.0 - m_dblLastCompletionFraction) / m_dblPBIncrementPerTimerInterval)) / 1000;

                    // UPDATE :http://www.codeproject.com/KB/cs/prettygoodsplashscreen.aspx?df=100&forumid=26207&mpp=50&noise=1&fr=1&select=2771540&fid=26207#xx2771540xx
                    if (iSecondsLeft == 1)
                    {
                        SetRemainintTimeCrossThread(string.Format("1 second remaining"));
                    }
                    else
                    {
                        SetRemainintTimeCrossThread(string.Format("{0} seconds remaining", iSecondsLeft));
                    }

                }
            }
        }

        // UPDATE :http://www.codeproject.com/KB/cs/prettygoodsplashscreen.aspx?df=100&forumid=26207&mpp=50&noise=1&fr=1&select=2771540&fid=26207#xx2771540xx
        private void SetRemainintTimeCrossThread(string myParameter)
        {
            if (this.InvokeRequired)
            {
                Invoke(new MethodInvoker(
                   delegate()
                   {
                       SetRemainintTimeCrossThread(myParameter);
                   }));
            }
            else
            {
                // This is where your original corss thread exception generating code goes.
                lblTimeRemaining.Text = myParameter;
            }
        }

        public void SetOwnerCrossThread(Form frmParent)
        {
            if (this.InvokeRequired)
            {
                Invoke(new MethodInvoker(
                   delegate()
                   {
                       SetOwnerCrossThread(frmParent);
                   }));
            }
            else
            {
                // This is where your original corss thread exception generating code goes.
                SplashForm.Owner = frmParent;
            }
        }

        // Paint the portion of the panel invalidated during the tick event.
        private void pnlStatus_Paint(object sender, System.Windows.Forms.PaintEventArgs e)
        {
            if (m_rProgress.Width == 0 && m_rProgress.Height == 0) return;
            if (m_bFirstLaunch == false && e.ClipRectangle.Width > 0 && m_iActualTicks > 1)
            {
                LinearGradientBrush brBackground = new LinearGradientBrush(m_rProgress, Color.FromArgb(58, 96, 151), Color.FromArgb(181, 237, 254), LinearGradientMode.Horizontal);
                e.Graphics.FillRectangle(brBackground, m_rProgress);
            }
        }

        // Close the form if they double click on it.
        private void SplashScreen_DoubleClick(object sender, System.EventArgs e)
        {
            //CloseForm();
        }
        public bool NotifyMessage(string _Title, string _Text)
        {
            try
            {
                appNotifyIcon.Text = app_name;
                appNotifyIcon.Icon = new Icon("resources/Dollar.ico");
                appNotifyIcon.BalloonTipIcon = ToolTipIcon.Info;
                appNotifyIcon.BalloonTipTitle = _Title;
                appNotifyIcon.BalloonTipText = _Text;
                appNotifyIcon.Visible = true;
                appNotifyIcon.ShowBalloonTip(900000);

                return true;
            }
            catch (Exception ex)
            {
                Utils.LogEventViewer(ex);
                return false;
            }
        }
        private void SplashScreen_Load(object sender, EventArgs e)
        {
            try
            {
                NotifyMessage(app_name, "System Launching...");
                lblTimeRemaining.Visible = false;
                pnlStatus.Visible = false;
                lblStatus.Visible = false;

                //app version
                var _buid_version = Application.ProductVersion;
                lblbuildversion.Text = "build version - " + _buid_version;

                lblappname.Text = app_name;

                this.Text = app_name + " Loading..........";

            }
            catch (Exception ex)
            {
                Utils.LogEventViewer(ex);
            }
        }
    }

    /// <summary>
    /// A local class for managing xml access.
    /// </summary>
    internal class TimeStampObserverDataLayer
    {
        private static string file = "TimeStampObserver.xml";

        /// <summary>
        /// get value of the specified key
        /// Note that if specified file did not exist, defaultValue will be return
        /// </summary>
        /// <param name="key">key to find</param>
        /// <param name="defaultValue">value to return if not found</param>
        /// <returns></returns>
        static public string GetStringValue(string key,
            string defaultValue)
        {

            try
            {
                XmlDocument xml = new XmlDocument();
                xml.Load(file);
                XmlElement value = xml.GetElementById(key);
                if (value == null) return defaultValue;
                else return value.InnerText;
            }
            catch
            {
                return defaultValue;
            }
        }


        /// <summary>
        /// Method for storing a string Value to xml file
        /// Note that this function replace an existing value, and throw an Exception if not found, nor non existing file.
        /// </summary>
        /// <param name="key">key</param>
        /// <param name="stringValue">value to replace</param>
        static public void SetStringValue(string key,
             string stringValue)
        {
            XmlDocument xml = new XmlDocument();
            if (!File.Exists(file))
            {
                //create it
            }
            else
            {
                xml.Load(file);
            }

            XmlElement value = xml.GetElementById(key);
            value.InnerText = stringValue;
            xml.Save(file);
        }


    }

    /// <summary>
    /// A class for managing registry access.
    /// </summary>
    public class RegistryAccess
    {
        private const string SOFTWARE_KEY = "Software";
        private const string COMPANY_NAME = "weight recording app";
        private const string APPLICATION_NAME = "weight recording app";

        // Method for retrieving a Registry Value.
        static public string GetStringRegistryValue(string key, string defaultValue)
        {
            RegistryKey rkCompany;
            RegistryKey rkApplication;

            rkCompany = Registry.CurrentUser.OpenSubKey(SOFTWARE_KEY, false).OpenSubKey(COMPANY_NAME, false);
            if (rkCompany != null)
            {
                rkApplication = rkCompany.OpenSubKey(APPLICATION_NAME, true);
                if (rkApplication != null)
                {
                    foreach (string sKey in rkApplication.GetValueNames())
                    {
                        if (sKey == key)
                        {
                            return (string)rkApplication.GetValue(sKey);
                        }
                    }
                }
            }
            return defaultValue;
        }

        // Method for storing a Registry Value.
        static public void SetStringRegistryValue(string key, string stringValue)
        {
            RegistryKey rkSoftware;
            RegistryKey rkCompany;
            RegistryKey rkApplication;

            rkSoftware = Registry.CurrentUser.OpenSubKey(SOFTWARE_KEY, true);
            rkCompany = rkSoftware.CreateSubKey(COMPANY_NAME);
            if (rkCompany != null)
            {
                rkApplication = rkCompany.CreateSubKey(APPLICATION_NAME);
                if (rkApplication != null)
                {
                    rkApplication.SetValue(key, stringValue);
                }
            }
        }
    }
}
