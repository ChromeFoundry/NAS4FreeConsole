using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml;
using System.Xml.XPath;
using System.Globalization;


namespace NAS4FreeConsole
{
    public class ServerInfo : IDisposable
    {
        #region Global Variables
        private Client _client;
        private int _refreshTimer;
        private bool _staticUpdated = false;
        private DateTime _lastupdate = DateTime.MinValue;
        private List<DiskUsage> _diskUsages;
        private Dictionary<string, object> attributes = new Dictionary<string, object>();
        public Boolean _isUpdating = false;
        private bool disposed = false;

        public delegate void UpdateCompleteHandler();
        public event UpdateCompleteHandler UpdateCompleted;
        #endregion

        #region Initialize
        private void Initialize()
        {
            attributes.Clear();
            attributes.Add("HostName", String.Empty);
            attributes.Add("ProductVersion", String.Empty);
            attributes.Add("BuildTime", String.Empty);
            attributes.Add("OSVersion", String.Empty);
            attributes.Add("Platform", String.Empty);
            attributes.Add("SystemDate", String.Empty);
            attributes.Add("Uptime", String.Empty);
            attributes.Add("CPUCount", 0);
            attributes.Add("CPUFrequency", 0);
            attributes.Add("CPUTemperature", 0d);
            attributes.Add("CPUUsage", 0);
            attributes.Add("RAMReal", 0d);
            attributes.Add("RAMPhysical", 0d);
            attributes.Add("RAMTotal", 0d);
            attributes.Add("RAMFree", 0d);
            attributes.Add("ConfigXml", String.Empty);
            _diskUsages = new List<DiskUsage>();
        }
        #endregion

        #region Constructors
        public ServerInfo(Client client) : this(client, 5) { }
        public ServerInfo(Client client, int seconds)
        {
            Initialize();
            _client = client;
            _refreshTimer = seconds;
        }
        #endregion

        #region Properties
        #region IsUpdating
        /// <summary>
        /// Returns the state of the object.
        /// </summary>
        public Boolean IsUpdating
        {
            get { return _isUpdating; }
        }
        #endregion

        #region Refresh Timer
        /// <summary>
        /// Gets or sets the number of seconds between updates.
        /// </summary>
        public int RefreshTimer
        {
            get { return _refreshTimer; }
            set { _refreshTimer = value; }
        }
        #endregion

        #region Hostname
        public string HostName
        {
            get
            {
                return (string)attributes["HostName"];
            }
        }
        private void Refresh_HostName()
        {
            if (!_staticUpdated && !disposed)
            {
                Logger.LogMessage("Begin HostName Refresh...", LogType.Debug);
                attributes["HostName"] = _client.GetCommandResponse("hostname");
            }
        }
        #endregion

        #region Product Version
        public string ProductVersion
        {
            get
            {
                return (string)attributes["ProductVersion"];
            }
        }
        private void Refresh_ProductVersion()
        {
            if (!_staticUpdated && !disposed)
            {
                Logger.LogMessage("Begin ProductVersion Refresh...", LogType.Debug);
                attributes["ProductVersion"] = string.Format("{0} {1} (revision {2})", _client.GetCommandResponse("cat /etc/prd.version"),
                                                                                      _client.GetCommandResponse("cat /etc/prd.version.name"),
                                                                                      _client.GetCommandResponse("cat /etc/prd.revision"));
            }
        }
        #endregion

        #region Build Time
        public string BuildTime
        {
            get
            {
                return (string)attributes["BuildTime"];
            }
        }
        private void Refresh_BuildTime()
        {
            if (!_staticUpdated && !disposed)
            {
                Logger.LogMessage("Begin BuildTime Refresh...", LogType.Debug);
                attributes["BuildTime"] = _client.GetCommandResponse("cat /etc/prd.version.buildtime");
            }
        }
        #endregion

        #region OS Version
        public string OSVersion
        {
            get
            {
                return (string)attributes["OSVersion"];
            }
        }
        private void Refresh_OSVersion()
        {
            if (!_staticUpdated && !disposed)
            {
                Logger.LogMessage("Begin OSVersion Refresh...", LogType.Debug);
                attributes["OSVersion"] = string.Format("{0} {1} (revision {2})", _client.GetCommandResponse("/sbin/sysctl -n kern.ostype"),
                                                                                _client.GetCommandResponse("/sbin/sysctl -n kern.osrelease"),
                                                                                _client.GetCommandResponse("/sbin/sysctl -n kern.osrevision"));
            }
        }
        #endregion

        #region Platform
        public string Platform
        {
            get
            {
                return (string)attributes["Platform"];
            }
        }
        private void Refresh_Platform()
        {
            if (!_staticUpdated && !disposed)
            {
                Logger.LogMessage("Begin Platform Refresh...", LogType.Debug);
                attributes["Platform"] = string.Format("{0} on {1}", _client.GetCommandResponse("cat /etc/platform"),
                                                        _client.GetCommandResponse("/sbin/sysctl -n hw.model"));
            }
        }
        #endregion

        #region System Date
        public string SystemDate
        {
            get
            {
                return (string)attributes["SystemDate"];
            }
        }
        private void Refresh_SystemDate()
        {
            if (RequiresUpdate && !disposed)
            {
                Logger.LogMessage("Begin SystemDate Refresh...", LogType.Debug);
                attributes["SystemDate"] = _client.GetCommandResponse("date");
            }
        }
        #endregion

        #region Uptime
        public string Uptime
        {
            get
            {
                return (string)attributes["Uptime"];
            }
        }
        private void Refresh_Uptime()
        {
            if (RequiresUpdate && !disposed)
            {
                Logger.LogMessage("Begin Uptime Refresh...", LogType.Debug);
                string temp = _client.GetCommandResponse("/sbin/sysctl -n kern.boottime");
                Match match = Regex.Match(temp, @"sec = (\d+)", RegexOptions.IgnoreCase);
                if (match.Success)
                {
                    double sec = double.Parse(match.Groups[1].Value);
                    if (sec > 60) { sec -= 30; }
                    DateTime dtNow = DateTime.ParseExact(_client.GetCommandResponse("date -j +\"%Y%m%d %H:%M:%S %z\""), "yyyyMMdd HH:mm:ss zzz", System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.AdjustToUniversal);
                    DateTime dtBoot = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc).AddTicks(TimeSpan.FromSeconds(sec).Ticks);
                    TimeSpan span = dtNow.Subtract(dtBoot);
                    if (span.Days > 0)
                        attributes["Uptime"] = string.Format("{0} day(s) {1} hour(s) {2} minute(s) {3} second(s)", span.Days,
                                                                                                                  span.Hours,
                                                                                                                  span.Minutes,
                                                                                                                  span.Seconds);
                    else if (span.Hours > 0)
                        attributes["Uptime"] = string.Format("{0} hour(s) {1} minute(s) {2} second(s)", span.Hours,
                                                                                                        span.Minutes,
                                                                                                        span.Seconds);
                    else if (span.Minutes > 0)
                        attributes["Uptime"] = string.Format("{0} minute(s) {1} second(s)", span.Minutes,
                                                                                             span.Seconds);
                    else if (span.Minutes > 0)
                        attributes["Uptime"] = string.Format("{0} second(s)", span.Seconds);
                }
            }
        }
        #endregion

        #region CPU Count
        public int CPUCount
        {
            get
            {
                return (int)attributes["CPUCount"];
            }
        }
        private void Refresh_CPUCount()
        {
            if (!_staticUpdated && !disposed)
            {
                Logger.LogMessage("Begin CPUCount Refresh...", LogType.Debug);
                string temp = _client.GetCommandResponse("/sbin/sysctl -q -n kern.smp.cpus");
                Match match = Regex.Match(temp, @"(\d+)", RegexOptions.IgnoreCase);
                if (match.Success)
                {
                    try
                    {
                        int c = int.Parse(match.Groups[1].Value);
                        if (c < 1) c = 1;
                        attributes["CPUCount"] = c;
                    }
                    catch
                    {
                        attributes["CPUCount"] = 1;
                    }
                }
                else
                {
                    attributes["CPUCount"] = 1;
                }
            }
        }
        #endregion

        #region CPU Frequency
        public int CPUFrequency
        {
            get { return (int)attributes["CPUFrequency"]; }
        }
        public string CPUFrequencyString
        {
            get { return CPUFrequency.ToString() + "MHz"; }
        }
        private void Refresh_CPUFrequency()
        {
            if (RequiresUpdate && !disposed)
            {
                Logger.LogMessage("Begin CPUFrequency Refresh...", LogType.Debug);
                string temp = _client.GetCommandResponse("/sbin/sysctl -n dev.cpu.0.freq");
                Match match = Regex.Match(temp, @"(\d+)", RegexOptions.IgnoreCase);
                if (match.Success)
                {
                    attributes["CPUFrequency"] = int.Parse(match.Groups[1].Value);
                }
                else
                {
                    attributes["CPUFrequency"] = 0;
                }
            }
        }
        #endregion

        #region CPU Temperature
        public double CPUTemperature
        {
            get { return (double)attributes["CPUTemperature"]; }
        }
        public string CPUTemperatureString
        {
            get
            {
                if (CPUTemperature == double.Parse ("0.0"))
                {

                    return "Temperature function not supported";
                }
                else
                {
                    return CPUTemperature.ToString("##0.0") + " °C  (" + ((CPUTemperature * (9d / 5d)) + 32d).ToString("##0.0") + " °F)";
                }
            }
        }
        private void Refresh_CPUTemperature()
        {
            if (RequiresUpdate && !disposed)
            {
                Logger.LogMessage("Begin CPUTemp Refresh...", LogType.Debug);
                double avgTemp = 0;
                double cnt = 0;
                this.Refresh_CPUCount();
                for (int i = 0; i < CPUCount; i++)
                {
                    string temp = _client.GetCommandResponse(String.Format("/sbin/sysctl -n dev.cpu.{0}.temperature", i));
                    try
                    {
                        avgTemp += double.Parse(DigitOnly(temp), new CultureInfo("en-US", false));
                        cnt++;
                    }
                    catch (FormatException) { }
                }
                if (cnt == 0d)
                    attributes["CPUTemperature"] = 0d;
                else
                    attributes["CPUTemperature"] = avgTemp / cnt;
            }
        }
        #endregion

        #region CPU Usage
        public int CPUUsage
        {
            get { return (int)attributes["CPUUsage"]; }
        }
        public string CPUUsageString
        {
            get { return attributes["CPUUsage"].ToString() + "%"; }
        }
        private void Refresh_CPUUsage()
        {
            if (RequiresUpdate && !disposed)
            {
                Logger.LogMessage("Begin CPUUsage Refresh...", LogType.Debug);
                int cpuUsage = 0;
                string cmd = "/sbin/sysctl -n kern.cp_times";

                string[] temp1 = _client.GetCommandResponse(cmd, ";").Replace(";", " ").Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                System.Threading.Thread.Sleep(1000);
                string[] temp2 = _client.GetCommandResponse(cmd, ";").Replace(";", " ").Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
       
                if (temp1.Length > 4 && temp1.Length == temp2.Length)
                {
                    double totStart = 0;
                    double tIdleStart = 0;
                    double totEnd = 0;
                    double tIdleEnd = 0;
                    for (int i = 0; i < (temp1.Length/5); i++)
                    {
                        try
                        {
                            double userStart = double.Parse(temp1[(5 * i) + 0]);
                            double niceStart = double.Parse(temp1[(5 * i) + 1]);
                            double sysStart = double.Parse(temp1[(5 * i) + 2]);
                            double intrStart = double.Parse(temp1[(5 * i) + 3]);
                            double idleStart = double.Parse(temp1[(5 * i) + 4]);

                            double userEnd = double.Parse(temp2[(5 * i) + 0]);
                            double niceEnd = double.Parse(temp2[(5 * i) + 1]);
                            double sysEnd = double.Parse(temp2[(5 * i) + 2]);
                            double intrEnd = double.Parse(temp2[(5 * i) + 3]);
                            double idleEnd = double.Parse(temp2[(5 * i) + 4]);

                            totStart += userStart + niceStart + sysStart + intrStart + idleStart;
                            tIdleStart += idleStart;
                            totEnd += userEnd + niceEnd + sysEnd + intrEnd + idleEnd;
                            tIdleEnd += idleEnd;
                        }
                        catch { }
                    }
                    if (totEnd > totStart)
                    {
                        double totalUsed = (totEnd - totStart) - (tIdleEnd - tIdleStart);
                        cpuUsage = (int)(Math.Floor(100 * (totalUsed / (totEnd - totStart))));
                    }
                    attributes["CPUUsage"] = cpuUsage;
                }
            }
        }
        #endregion

        #region Memory Usage / Total
        public double RAMReal
        {
            get { return (double)attributes["RAMReal"]; }
        }
        public double RAMPhysical
        {
            get { return (double)attributes["RAMPhysical"]; }
        }
        public double RAMTotal
        {
            get { return (double)attributes["RAMTotal"]; }
        }
        public double RAMFree
        {
            get { return (double)attributes["RAMFree"]; }
        }
        public double RAMUsed
        {
            get { return RAMTotal - RAMFree; }
        }
        public int RAMUsage
        {
            get { return RAMTotal > 0d ? (int)Math.Round((RAMUsed * 100d / RAMTotal), 0) : 0; }
        }
        public string RAMUsageString
        {
            get { return String.Format("{0}% of {1}MB", RAMUsage, Math.Round(RAMPhysical/1024d/1024d)); }
        }
        private void Refresh_RAMUsage()
        {
            if (RequiresUpdate && !disposed)
            {
                Logger.LogMessage("Begin RAMUsage Refresh...", LogType.Debug);
                try
                {
                    //int hwPageSz = int.Parse(_client.GetCommandResponse("/sbin/sysctl -n hw.pagesize"));

                    double memReal = double.Parse(_client.GetCommandResponse("/sbin/sysctl -n hw.realmem"));
                    String temp = _client.GetCommandResponse("/sbin/sysctl -n vm.stats.vm.v_active_count vm.stats.vm.v_inactive_count vm.stats.vm.v_wire_count vm.stats.vm.v_cache_count vm.stats.vm.v_free_count hw.physmem");
                    string[] split = temp.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                    if (split.Length == 6)
                    {
                        double memActive = double.Parse(split[0]);
                        double memInActive = double.Parse(split[1]);
                        double memWire = double.Parse(split[2]);
                        double memCache = double.Parse(split[3]);
                        double memFree = double.Parse(split[4]);
                        double memPhys = double.Parse(split[5]);

                        attributes["RAMReal"] = memReal;
                        attributes["RAMPhysical"] = memPhys;
                        attributes["RAMTotal"] = memActive + memInActive + memWire + memCache + memFree;
                        attributes["RAMFree"] = memFree + memInActive;
                    }
                }
                catch (FormatException)
                {
                    attributes["RAMReal"] = 0d;
                    attributes["RAMPhysical"] = 0d;
                    attributes["RAMTotal"] = 0d;
                    attributes["RAMFree"] = 0d;
                }
            }
        }
        #endregion

        #region Disk Drive Usage / Size
        public List<DiskUsage> DiskUsages
        {
            get { return _diskUsages; }
        }
        private void Refresh_DiskUsages()
        {
            if (RequiresUpdate && !disposed)
            {
                Logger.LogMessage("Begin DiskUsage Refresh...", LogType.Debug);

                List<DiskUsage> temp = new List<DiskUsage>();
                String raw = _client.GetCommandRawResponse("/bin/df -h");
                String[] lines = raw.Split(new string[] { Environment.NewLine, "\n" }, StringSplitOptions.RemoveEmptyEntries);
                foreach (string line in lines)
                {
                    Match match = Regex.Match(line, @"(\S+)\s+(\S+)\s+(\S+)\s+(\S+)\s+(\d+)%\s+(.+)");
                    if (match.Success)
                    {
                        try
                        {
                            String fileSystem = match.Groups[1].Value;
                            Logger.LogMessage(" - Disk: '" + fileSystem + "'", LogType.Debug);
                            XmlNode node = ParseXMLConfig("nas4free/mounts/mount[devicespecialfile=\"" + fileSystem + "\"]");
                            if (node != null)
                            {
                                Logger.LogMessage("   - Found mount config", LogType.Debug);
                                String sSize = match.Groups[2].Value;
                                String sUsed = match.Groups[3].Value;
                                String sFree = match.Groups[4].Value;
                                String sUsedPcnt = match.Groups[5].Value;
                                XmlNode innerNode = node.SelectSingleNode("sharename");
                                if (innerNode != null)
                                {
                                    Logger.LogMessage("   - Found sharename node", LogType.Debug);
                                    String sMount = innerNode.InnerText;
                                    if (!string.IsNullOrEmpty(sMount))
                                    {
                                        DiskUsage du = new DiskUsage(sMount);
                                        du.FileSystem = fileSystem;
                                        du.TotalSpace = sSize;
                                        du.UsedSpace = sUsed;
                                        du.FreeSpace = sFree;
                                        int iTemp = 0;
                                        int.TryParse(sUsedPcnt, out iTemp);
                                        du.PercentUsed = iTemp;
                                        du.PoolStatus = String.Empty;
                                        Logger.LogMessage("   - Adding disk" + fileSystem, LogType.Debug);
                                        temp.Add(du);
                                    }
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            Logger.LogMessage("   - Error: " + ex.Message, LogType.Debug);
                        }
                    }
                }

                String zfsRaw = _client.GetCommandRawResponse("/sbin/zpool list -H");
                lines = zfsRaw.Split(new String[] { Environment.NewLine, "\n" }, StringSplitOptions.RemoveEmptyEntries);
                foreach (string line in lines)
                {
                    Match match = Regex.Match(line, @"(\S+)\s+(\S+)\s+(\S+)\s+(\S+)\s+(\d+)%\s+(\S+)\s+(\S+)\s+(.+)");
                    if (match.Success)
                    {
                        String poolName = match.Groups[1].Value;
                        Logger.LogMessage(" - Pool: '" + poolName + "'", LogType.Debug);
                        XmlNode node = ParseXMLConfig("nas4free/zfs/pools/pool[name=\"" + poolName + "\"]");
                        if (node != null)
                        {
                            try
                            {
                                Logger.LogMessage("   - Found pool config", LogType.Debug);
                                String sSize = match.Groups[2].Value;
                                String sAlloc = match.Groups[3].Value;
                                String sFree = match.Groups[4].Value;
                                String sCap = match.Groups[5].Value;
                                String sDedup = match.Groups[6].Value;
                                String sHealth = match.Groups[7].Value;
                                String sAltRoot = match.Groups[8].Value;
                                String zfsRaw2 = _client.GetCommandRawResponse("/sbin/zfs list -H -o used,available " + poolName);
                                Match match2 = Regex.Match(zfsRaw2, @"(\S+)\s+(\S+)");
                                if (match2.Success)
                                {
                                    sAlloc = match2.Groups[1].Value;
                                    sFree = match2.Groups[2].Value;
                                }
                                DiskUsage du = new DiskUsage(poolName);
                                du.FileSystem = poolName;
                                du.TotalSpace = sSize;
                                du.UsedSpace = sAlloc;
                                du.FreeSpace = sFree;
                                int iTemp = 0;
                                int.TryParse(sCap, out iTemp);
                                du.PercentUsed = iTemp;
                                du.PoolStatus = sHealth;
                                Logger.LogMessage("   - Adding pool '" + poolName + "'", LogType.Debug);
                                temp.Add(du);
                            }
                            catch (Exception ex)
                            {
                                Logger.LogMessage("   - Error: " + ex.Message, LogType.Debug);
                            }
                        }
                    }
                    temp = temp.OrderBy(t => t.Name).ToList();
                }
                lock (_diskUsages)
                {
                    _diskUsages = temp;
                    Logger.LogMessage(" - Updated DiskUsage", LogType.Debug);
                }
            }
        }
        #endregion

        #region Config XML
        public string ConfigXML
        {
            get { return (string)attributes["ConfigXML"]; }
        }
        public void Refresh_ConfigXML()
        {
            if (!_staticUpdated && !disposed)
            {
                Logger.LogMessage("Begin ConfigXML Refresh...", LogType.Debug); 
                attributes["ConfigXML"] = _client.GetCommandRawResponse("cat /conf/config.xml");
                Logger.LogMessage(" - config.xml: " + System.Text.ASCIIEncoding.Unicode.GetByteCount(ConfigXML).ToString() + " bytes", LogType.Debug);
            }
        }
        public bool Update_ConfigXML(FileInfo fileinfo)
        {
            bool retVal = false;
            if (!disposed)
            {
                string newfile = "/conf/temp.xml";
                if (_client.UploadFile(fileinfo, newfile))
                {
                    string sSize = _client.GetCommandResponse(String.Format("du -k {0} | awk '{{print $1}}'", newfile));
                    try
                    {
                        double dsize = double.Parse(sSize);
                        if (dsize > 0d)
                        {
                            _client.GetCommandRawResponse(String.Format("cp -f {0} /conf/config.xml", newfile));
                            _client.GetCommandRawResponse(String.Format("rm -f {0}", newfile));
                            retVal = true;
                        }
                    }
                    catch { }
                }
            }
            return retVal;
        }
        #endregion
        #endregion

        #region Private Methods
        private bool RequiresUpdate
        {
            get 
            { 
                if (disposed)
                {
                    return false;
                }
                return CheckRequiresUpdate(_refreshTimer); 
            }
        }

        private bool CheckRequiresUpdate(double expire)
        {
            return (DateTime.Now.Subtract(_lastupdate).TotalSeconds > expire);
        }
        private XmlNode ParseXMLConfig(string xpath)
        {
            if (_staticUpdated)
            {
                try
                {
                    XmlDocument doc = new XmlDocument();
                    doc.LoadXml(ConfigXML);
                    XmlNode nodes = doc.SelectSingleNode(xpath);
                    return nodes;
                }
                catch (Exception ex)
                {
                    Logger.LogMessage("ParseXMLConfig: " + ex.Message, LogType.Error);
                    return null;
                }
            }
            else
            {
                return null;
            }
        }

        public static string DigitOnly(string str)
        {
            String retVal = String.Empty;
            if (!String.IsNullOrEmpty(str))
            {
                StringBuilder sb = new StringBuilder();
                foreach (char ch in str)
                {
                    if (char.IsDigit(ch) || ch.Equals('-') || ch.Equals('.') || ch.Equals(','))
                    {
                        sb.Append(ch);
                    }
                }
                retVal = sb.ToString();

                if (CharOccurrences(retVal, ',') == 1)
                {
                    if (CharOccurrences(retVal, '.') == 1)
                    {
                        retVal = retVal.Replace(",", "");
                    }
                    else if (CharOccurrences(retVal, '.') > 1)
                    {
                        retVal = retVal.Replace(".", "");
                        retVal = retVal.Replace(',', '.');
                    }
                    else
                    {
                        retVal = retVal.Replace(',', '.');
                    }
                }
                else if (CharOccurrences(retVal, ',') > 1)
                {
                    retVal = retVal.Replace(",", "");
                }
            }
            return retVal;
        }
        public static int CharOccurrences(string str, char ch)
        {
            return str.Count(x => x.Equals(ch));
        }
        #endregion

        #region Public Methods
        /// <summary>
        /// Update property attributes in need of updating.
        /// </summary>
        public void Refresh()
        {
            if (RequiresUpdate && !_isUpdating && !disposed)
            {
                Logger.LogMessage("Begin server info thread", LogType.Debug);
                _isUpdating = true;
                Refresh_ConfigXML();
                Refresh_HostName();
                Refresh_ProductVersion();
                Refresh_BuildTime();
                Refresh_OSVersion();
                Refresh_Platform();
                Refresh_SystemDate();
                Refresh_Uptime();
                Refresh_CPUCount();
                Refresh_CPUFrequency();
                Refresh_CPUTemperature();
                Refresh_CPUUsage();
                Refresh_RAMUsage();
                _staticUpdated = true;
                Refresh_DiskUsages();
                _lastupdate = DateTime.Now;
                if (UpdateCompleted != null)
                    UpdateCompleted();
            }
            _isUpdating = false;
            Logger.LogMessage("End server info thread", LogType.Debug);
        }

        /// <summary>
        /// Wait for Refresh to complete and dispose.
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
        }
        protected virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    int waitCount = 0;
                    while (_isUpdating)
                    {
                        if (waitCount > 5) break;
                        System.Threading.Thread.Sleep(100);
                        waitCount++;
                    }
                }
                disposed = true;
            }
        }
        #endregion
    }

    /// <summary>
    /// Structure for holding mounted disk usage stats.
    /// </summary>
    public struct DiskUsage
    {
        public DiskUsage(string name)
        {
            Name = name;
            FileSystem = String.Empty;
            TotalSpace = String.Empty;
            UsedSpace = String.Empty;
            FreeSpace = String.Empty;
            PercentUsed = 0;
            PoolStatus = "UNKNOWN";
        }
        public string Name;
        public string FileSystem;
        public string TotalSpace;
        public string UsedSpace;
        public string FreeSpace;
        public int PercentUsed;
        public string PoolStatus;
    }
}
