using System;
using System.Collections.Generic;
using System.Text;

namespace JustLib.NetworkDisk
{
    /// <summary>
    /// 用户使用的网络硬盘的状态。
    /// </summary>
    public class NetworkDiskState
    {
        public NetworkDiskState() { }
        public NetworkDiskState(ulong total,ulong used)
        {
            this.totalSize = total;
            this.sizeUsed = used;
        }
    
        #region TotalSize
        private ulong totalSize = 0;
        /// <summary>
        /// TotalSize 网络硬盘总大小。
        /// </summary>
        public ulong TotalSize
        {
            get { return totalSize; }
            set { totalSize = value; }
        } 
        #endregion

        #region SizeUsed
        private ulong sizeUsed = 0;
        /// <summary>
        /// SizeUsed 已经使用的网络硬盘大小。
        /// </summary>
        public ulong SizeUsed
        {
            get { return sizeUsed; }
            set { sizeUsed = value; }
        } 
        #endregion
    }
}
