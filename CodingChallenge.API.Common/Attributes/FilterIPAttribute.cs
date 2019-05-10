using System;
using System.Collections;
using System.Configuration;
using System.Text;
using System.Web;
using System.Web.Http;
using System.Web.Http.Controllers;

namespace CodingChallenge.API.Common.Attributes
{
    public class FilterIpAttribute : AuthorizeAttribute
    {
        protected override bool IsAuthorized(HttpActionContext actionContext)
        {
            if (actionContext == null)
                throw new ArgumentNullException("actionContext");

            var userIpAddress = ((HttpContextWrapper) actionContext.Request.Properties["MS_HttpContext"]).Request
                .UserHostName;

            try
            {
                // Check that the IP is allowed to access
                var ipAllowed = CheckAllowedIPs(userIpAddress);

                // Check that the IP is not denied to access
                var ipDenied = CheckDeniedIPs(userIpAddress);

                // Only allowed if allowed and not denied
                var finallyAllowed = ipAllowed && !ipDenied;

                return finallyAllowed;
            }
            catch (Exception)
            {
                // Log the exception, probably something wrong with the configuration
            }

            return true; // if there was an exception, then we return true
        }

        /// <summary>
        ///     Checks the allowed IPs.
        /// </summary>
        /// <param name="userIpAddress">The user ip address.</param>
        /// <returns></returns>
        private bool CheckAllowedIPs(string userIpAddress)
        {
            // Populate the IPList with the Single IPs
            if (!string.IsNullOrEmpty(AllowedSingleIPs))
                SplitAndAddSingleIPs(AllowedSingleIPs, _allowedIpListToCheck);

            // Populate the IPList with the Masked IPs
            if (!string.IsNullOrEmpty(AllowedMaskedIPs))
                SplitAndAddMaskedIPs(AllowedMaskedIPs, _allowedIpListToCheck);

            // Check if there are more settings from the configuration (Web.config)
            if (!string.IsNullOrEmpty(ConfigurationKeyAllowedSingleIPs))
            {
                var configurationAllowedAdminSingleIPs =
                    ConfigurationManager.AppSettings[ConfigurationKeyAllowedSingleIPs];
                if (!string.IsNullOrEmpty(configurationAllowedAdminSingleIPs))
                    SplitAndAddSingleIPs(configurationAllowedAdminSingleIPs, _allowedIpListToCheck);
            }

            if (!string.IsNullOrEmpty(ConfigurationKeyAllowedMaskedIPs))
            {
                var configurationAllowedAdminMaskedIPs =
                    ConfigurationManager.AppSettings[ConfigurationKeyAllowedMaskedIPs];
                if (!string.IsNullOrEmpty(configurationAllowedAdminMaskedIPs))
                    SplitAndAddMaskedIPs(configurationAllowedAdminMaskedIPs, _allowedIpListToCheck);
            }

            return _allowedIpListToCheck.CheckNumber(userIpAddress);
        }

        /// <summary>
        ///     Checks the denied IPs.
        /// </summary>
        /// <param name="userIpAddress">The user ip address.</param>
        /// <returns></returns>
        private bool CheckDeniedIPs(string userIpAddress)
        {
            // Populate the IPList with the Single IPs
            if (!string.IsNullOrEmpty(DeniedSingleIPs))
                SplitAndAddSingleIPs(DeniedSingleIPs, _deniedIpListToCheck);

            // Populate the IPList with the Masked IPs
            if (!string.IsNullOrEmpty(DeniedMaskedIPs))
                SplitAndAddMaskedIPs(DeniedMaskedIPs, _deniedIpListToCheck);

            // Check if there are more settings from the configuration (Web.config)
            if (!string.IsNullOrEmpty(ConfigurationKeyDeniedSingleIPs))
            {
                var configurationDeniedAdminSingleIPs =
                    ConfigurationManager.AppSettings[ConfigurationKeyDeniedSingleIPs];
                if (!string.IsNullOrEmpty(configurationDeniedAdminSingleIPs))
                    SplitAndAddSingleIPs(configurationDeniedAdminSingleIPs, _deniedIpListToCheck);
            }

            if (!string.IsNullOrEmpty(ConfigurationKeyDeniedMaskedIPs))
            {
                var configurationDeniedAdminMaskedIPs =
                    ConfigurationManager.AppSettings[ConfigurationKeyDeniedMaskedIPs];
                if (!string.IsNullOrEmpty(configurationDeniedAdminMaskedIPs))
                    SplitAndAddMaskedIPs(configurationDeniedAdminMaskedIPs, _deniedIpListToCheck);
            }

            return _deniedIpListToCheck.CheckNumber(userIpAddress);
        }

        /// <summary>
        ///     Splits the incoming ip string of the format "IP,IP" example "10.2.0.0,10.3.0.0" and adds the result to the IPList
        /// </summary>
        /// <param name="ips">The ips.</param>
        /// <param name="list">The list.</param>
        private void SplitAndAddSingleIPs(string ips, IpList list)
        {
            var splitSingleIPs = ips.Split(',');
            foreach (var ip in splitSingleIPs)
                list.Add(ip);
        }

        /// <summary>
        ///     Splits the incoming ip string of the format "IP;MASK,IP;MASK" example "10.2.0.0;255.255.0.0,10.3.0.0;255.255.0.0"
        ///     and adds the result to the IPList
        /// </summary>
        /// <param name="ips">The ips.</param>
        /// <param name="list">The list.</param>
        private void SplitAndAddMaskedIPs(string ips, IpList list)
        {
            var splitMaskedIPs = ips.Split(',');
            foreach (var maskedIp in splitMaskedIPs)
            {
                var ipAndMask = maskedIp.Split(';');
                list.Add(ipAndMask[0], ipAndMask[1]); // IP;MASK
            }
        }

        public override void OnAuthorization(HttpActionContext filterContext)
        {
            base.OnAuthorization(filterContext);
        }

        #region Allowed

        /// <summary>
        ///     Comma seperated string of allowable IPs. Example "10.2.5.41,192.168.0.22"
        /// </summary>
        /// <value></value>
        public string AllowedSingleIPs { get; set; }

        /// <summary>
        ///     Comma seperated string of allowable IPs with masks. Example "10.2.0.0;255.255.0.0,10.3.0.0;255.255.0.0"
        /// </summary>
        /// <value>The masked I ps.</value>
        public string AllowedMaskedIPs { get; set; }

        /// <summary>
        ///     Gets or sets the configuration key for allowed single IPs
        /// </summary>
        /// <value>The configuration key single I ps.</value>
        public string ConfigurationKeyAllowedSingleIPs { get; set; }

        /// <summary>
        ///     Gets or sets the configuration key allowed mmasked IPs
        /// </summary>
        /// <value>The configuration key masked I ps.</value>
        public string ConfigurationKeyAllowedMaskedIPs { get; set; }

        /// <summary>
        ///     List of allowed IPs
        /// </summary>
        private readonly IpList _allowedIpListToCheck = new IpList();

        #endregion

        #region Denied

        /// <summary>
        ///     Comma seperated string of denied IPs. Example "10.2.5.41,192.168.0.22"
        /// </summary>
        /// <value></value>
        public string DeniedSingleIPs { get; set; }

        /// <summary>
        ///     Comma seperated string of denied IPs with masks. Example "10.2.0.0;255.255.0.0,10.3.0.0;255.255.0.0"
        /// </summary>
        /// <value>The masked I ps.</value>
        public string DeniedMaskedIPs { get; set; }


        /// <summary>
        ///     Gets or sets the configuration key for denied single IPs
        /// </summary>
        /// <value>The configuration key single I ps.</value>
        public string ConfigurationKeyDeniedSingleIPs { get; set; }

        /// <summary>
        ///     Gets or sets the configuration key for denied masked IPs
        /// </summary>
        /// <value>The configuration key masked I ps.</value>
        public string ConfigurationKeyDeniedMaskedIPs { get; set; }

        /// <summary>
        ///     List of denied IPs
        /// </summary>
        private readonly IpList _deniedIpListToCheck = new IpList();

        #endregion
    }

    internal class IpArrayList
    {
        private readonly ArrayList _ipNumList = new ArrayList();
        private bool _isSorted;

        /// <summary>
        ///     Constructor that sets the mask for the list
        /// </summary>
        public IpArrayList(uint mask)
        {
            Mask = mask;
        }

        /// <summary>
        ///     The IP mask for this list of IP numbers
        /// </summary>
        public uint Mask { get; }

        /// <summary>
        ///     Add a new IP numer (range) to the list
        /// </summary>
        public void Add(uint ipNum)
        {
            _isSorted = false;
            _ipNumList.Add(ipNum & Mask);
        }

        /// <summary>
        ///     Checks if an IP number is within the ranges included by the list
        /// </summary>
        public bool Check(uint ipNum)
        {
            var found = false;
            if (_ipNumList.Count > 0)
            {
                if (!_isSorted)
                {
                    _ipNumList.Sort();
                    _isSorted = true;
                }
                ipNum = ipNum & Mask;
                if (_ipNumList.BinarySearch(ipNum) >= 0) found = true;
            }
            return found;
        }

        /// <summary>
        ///     Clears the list
        /// </summary>
        public void Clear()
        {
            _ipNumList.Clear();
            _isSorted = false;
        }

        /// <summary>
        ///     The ToString is overriden to generate a list of the IP numbers
        /// </summary>
        public override string ToString()
        {
            var buf = new StringBuilder();
            foreach (uint ipnum in _ipNumList)
            {
                if (buf.Length > 0) buf.Append("\r\n");
                buf.Append(((int) ipnum & 0xFF000000) >> 24).Append('.');
                buf.Append(((int) ipnum & 0x00FF0000) >> 16).Append('.');
                buf.Append(((int) ipnum & 0x0000FF00) >> 8).Append('.');
                buf.Append((int) ipnum & 0x000000FF);
            }
            return buf.ToString();
        }
    }

    /// <summary>
    ///     Summary description for Class1.
    /// </summary>
    public class IpList
    {
        private readonly ArrayList _ipRangeList = new ArrayList();
        private readonly SortedList _maskList = new SortedList();
        private readonly ArrayList _usedList = new ArrayList();

        public IpList()
        {
            // Initialize IP mask list and create IPArrayList into the ipRangeList
            uint mask = 0x00000000;
            for (var level = 1; level < 33; level++)
            {
                mask = (mask >> 1) | 0x80000000;
                _maskList.Add(mask, level);
                _ipRangeList.Add(new IpArrayList(mask));
            }
        }

        // Parse a String IP address to a 32 bit unsigned integer
        // We can't use System.Net.IPAddress as it will not parse
        // our masks correctly eg. 255.255.0.0 is pased as 65535 !
        private uint ParseIp(string ipNumber)
        {
            uint res = 0;
            var elements = ipNumber.Split('.');
            if (elements.Length == 4)
            {
                res = (uint) Convert.ToInt32(elements[0]) << 24;
                res += (uint) Convert.ToInt32(elements[1]) << 16;
                res += (uint) Convert.ToInt32(elements[2]) << 8;
                res += (uint) Convert.ToInt32(elements[3]);
            }
            return res;
        }

        /// <summary>
        ///     Add a single IP number to the list as a string, ex. 10.1.1.1
        /// </summary>
        public void Add(string ipNumber)
        {
            Add(ParseIp(ipNumber));
        }

        /// <summary>
        ///     Add a single IP number to the list as a unsigned integer, ex. 0x0A010101
        /// </summary>
        public void Add(uint ip)
        {
            ((IpArrayList) _ipRangeList[31]).Add(ip);
            if (!_usedList.Contains(31))
            {
                _usedList.Add(31);
                _usedList.Sort();
            }
        }

        /// <summary>
        ///     Adds IP numbers using a mask for range where the mask specifies the number of
        ///     fixed bits, ex. 172.16.0.0 255.255.0.0 will add 172.16.0.0 - 172.16.255.255
        /// </summary>
        public void Add(string ipNumber, string mask)
        {
            Add(ParseIp(ipNumber), ParseIp(mask));
        }

        /// <summary>
        ///     Adds IP numbers using a mask for range where the mask specifies the number of
        ///     fixed bits, ex. 0xAC1000 0xFFFF0000 will add 172.16.0.0 - 172.16.255.255
        /// </summary>
        public void Add(uint ip, uint umask)
        {
            var level = _maskList[umask];
            if (level != null)
            {
                ip = ip & umask;
                ((IpArrayList) _ipRangeList[(int) level - 1]).Add(ip);
                if (!_usedList.Contains((int) level - 1))
                {
                    _usedList.Add((int) level - 1);
                    _usedList.Sort();
                }
            }
        }

        /// <summary>
        ///     Adds IP numbers using a mask for range where the mask specifies the number of
        ///     fixed bits, ex. 192.168.1.0/24 which will add 192.168.1.0 - 192.168.1.255
        /// </summary>
        public void Add(string ipNumber, int maskLevel)
        {
            Add(ParseIp(ipNumber), (uint) _maskList.GetKey(_maskList.IndexOfValue(maskLevel)));
        }

        /// <summary>
        ///     Adds IP numbers using a from and to IP number. The method checks the range and
        ///     splits it into normal ip/mask blocks.
        /// </summary>
        public void AddRange(string fromIp, string toIp)
        {
            AddRange(ParseIp(fromIp), ParseIp(toIp));
        }

        /// <summary>
        ///     Adds IP numbers using a from and to IP number. The method checks the range and
        ///     splits it into normal ip/mask blocks.
        /// </summary>
        public void AddRange(uint fromIp, uint toIp)
        {
            // If the order is not asending, switch the IP numbers.
            if (fromIp > toIp)
            {
                var tempIp = fromIp;
                fromIp = toIp;
                toIp = tempIp;
            }
            if (fromIp == toIp)
            {
                Add(fromIp);
            }
            else
            {
                var diff = toIp - fromIp;
                var diffLevel = 1;
                var range = 0x80000000;
                if (diff < 256)
                {
                    diffLevel = 24;
                    range = 0x00000100;
                }
                while (range > diff)
                {
                    range = range >> 1;
                    diffLevel++;
                }
                var mask = (uint) _maskList.GetKey(_maskList.IndexOfValue(diffLevel));
                var minIp = fromIp & mask;
                if (minIp < fromIp) minIp += range;
                if (minIp > fromIp)
                {
                    AddRange(fromIp, minIp - 1);
                    fromIp = minIp;
                }
                if (fromIp == toIp)
                {
                    Add(fromIp);
                }
                else
                {
                    if (minIp + (range - 1) <= toIp)
                    {
                        Add(minIp, mask);
                        fromIp = minIp + range;
                    }
                    if (fromIp == toIp)
                    {
                        Add(toIp);
                    }
                    else
                    {
                        if (fromIp < toIp) AddRange(fromIp, toIp);
                    }
                }
            }
        }

        /// <summary>
        ///     Checks if an IP number is contained in the lists, ex. 10.0.0.1
        /// </summary>
        public bool CheckNumber(string ipNumber)
        {
            return CheckNumber(ParseIp(ipNumber));
        }

        /// <summary>
        ///     Checks if an IP number is contained in the lists, ex. 0x0A000001
        /// </summary>
        public bool CheckNumber(uint ip)
        {
            var found = false;
            var i = 0;
            while (!found && i < _usedList.Count)
            {
                found = ((IpArrayList) _ipRangeList[(int) _usedList[i]]).Check(ip);
                i++;
            }
            return found;
        }

        /// <summary>
        ///     Clears all lists of IP numbers
        /// </summary>
        public void Clear()
        {
            foreach (int i in _usedList)
                ((IpArrayList) _ipRangeList[i]).Clear();
            _usedList.Clear();
        }

        /// <summary>
        ///     Generates a list of all IP ranges in printable format
        /// </summary>
        public override string ToString()
        {
            var buffer = new StringBuilder();
            foreach (int i in _usedList)
            {
                buffer.Append("\r\nRange with mask of ").Append(i + 1).Append("\r\n");
                buffer.Append((IpArrayList) _ipRangeList[i]);
            }
            return buffer.ToString();
        }
    }
}