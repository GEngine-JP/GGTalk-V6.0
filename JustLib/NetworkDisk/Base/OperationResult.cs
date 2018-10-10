using System;
using System.Collections.Generic;
using System.Text;

namespace JustLib.NetworkDisk
{
    /// <summary>
    /// 针对Delete/Move/Copy操作的回复。
    /// </summary>
    [Serializable]
    public class OperationResult
    {
        public OperationResult() { }
        public OperationResult(string error)
        {
            this.succeed = false;
            this.errorMessage = error;
        }

        #region Succeed
        private bool succeed = true;
        public bool Succeed
        {
            get { return succeed; }
            set { succeed = value; }
        }
        #endregion

        #region ErrorMessage
        private string errorMessage = null;
        /// <summary>
        /// 如果Succeed为false，则该属性说明失败的原因。
        /// </summary>
        public string ErrorMessage
        {
            get { return errorMessage; }
            set { errorMessage = value; }
        }
        #endregion
    }
}
