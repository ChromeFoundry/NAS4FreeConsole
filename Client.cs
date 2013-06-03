using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Renci.SshNet;
using Renci.SshNet.Common;

namespace NAS4FreeConsole
{
    public class Client : SshClient, IDisposable
    {
        private bool disposed = false;
        private object _lock = new Object();

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="server">Server Name or IP Address</param>
        /// <param name="port">Port number to connect to server</param>
        /// <param name="user">username</param>
        /// <param name="pass">password</param>
        public Client(string server, int port, string user, string pass) : base(server, port, user, pass)
        {
            try
            {
                base.ConnectionInfo.Timeout = new TimeSpan(0, 0, 15);
                base.ConnectionInfo.RetryAttempts = 2;
                base.Connect();
            }
            catch (System.Net.Sockets.SocketException sEx) { string s = sEx.Message; }
            catch (Exception ex) { string s = ex.Message; }
        }
 
        /// <summary>
        /// Execute a command and return the raw output.
        /// </summary>
        /// <param name="cmd">command to issue to connected server.</param>
        /// <returns></returns>
        public string GetCommandRawResponse(string cmd)
        {
            try
            {
                if (!disposed)
                {
                    if (!base.IsConnected)
                    {
                        base.Connect();
                    }
                    SshCommand command = base.CreateCommand(cmd);
                    lock (_lock)
                    {
                        return command.Execute();
                    }
                }
                return String.Empty;
            }
            catch { return String.Empty; }
        }
 
        /// <summary>
        /// Execute a command and return a single line result string.
        /// </summary>
        /// <param name="cmd">command to issue to connected server.</param>
        /// <returns></returns>
        public string GetCommandResponse(string cmd)
        {
            return GetCommandResponse(cmd, " ");
        }
        public string GetCommandResponse(string cmd, string seperator)
        {
            try
            {
                if (!disposed)
                {
                    if (!base.IsConnected)
                    {
                        base.Connect();
                    }
                    SshCommand command = base.CreateCommand(cmd);
                    command.CommandTimeout = new TimeSpan(0, 0, 10);
                    lock (_lock)
                    {
                        return TrimNewLines(command.Execute(), seperator);
                    }
                }
                return String.Empty;
            }
            catch { return String.Empty;  }
        }

        /// <summary>
        /// Upload a file to the connected server.
        /// </summary>
        /// <param name="fileinfo">Local file</param>
        /// <param name="newfile">Server upload full-qualified filename</param>
        public bool UploadFile(FileInfo fileinfo, string newfile)
        {
            bool retVal = false;
            if (!disposed && base.IsConnected)
            {
                using (ScpClient transfer = new ScpClient(base.ConnectionInfo))
                {
                    try
                    {
                        transfer.Connect();
                        transfer.OperationTimeout = new TimeSpan(0, 1, 0);
                        transfer.Upload(fileinfo, newfile);
                        retVal = true;
                    }
                    catch
                    {
                        retVal = false;
                    }
                    finally
                    {
                        transfer.Disconnect();
                    }
                }
            }
            return retVal;
        }

        /// <summary>
        /// Trim newline characters from string and replace with seperator.
        /// This command is useful in turning multiline string into delimited string.
        /// </summary>
        /// <param name="str">String to process</param>
        /// <param name="seperator">replacement delimiter string</param>
        /// <returns></returns>
        private string TrimNewLines(string str, string seperator)
        {
            string retVal = str.Replace(Environment.NewLine, seperator).Replace("\n", seperator).Trim();
            while (retVal.EndsWith(seperator))
            {
                retVal = retVal.Substring(0, retVal.LastIndexOf(seperator) - 1);
            }
            return retVal;
        }

        /// <summary>
        /// Issue Shutdown with Restart command to connected server.
        /// </summary>
        public void RebootServer()
        {
            if (!disposed)
            {
                SshCommand command = base.CreateCommand("/sbin/shutdown -r now");
                command.Execute();
            }
        }

        /// <summary>
        /// Issue Shutdown command to connected server.
        /// </summary>
        public void ShutdownServer()
        {
            if (!disposed)
            {
                SshCommand command = base.CreateCommand("/sbin/shutdown -p now");
                command.Execute();
            }
        }

        /// <summary>
        /// Close open server connection and dispose object.
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
                    if (base.IsConnected)
                    {
                        base.Disconnect();
                    }
                    base.Dispose();
                }
                disposed = true;
            }
        }

    }
}
