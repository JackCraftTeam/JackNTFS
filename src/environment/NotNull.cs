using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;

namespace JackNTFS.src.environment
{
    internal class NotNull
    {
        private Object obj;

        public NotNull(Object obj)
        {
            this.obj = obj;
        }

        /* 抛出： 论句为空异常 */
        public void RequireNotNull()
        {
            if (this.obj == null)
            {
                obj = new Object();
                throw new ArgumentNullException($"Object {obj.ToString} should not be nulled");
            }
        }
    }
}
