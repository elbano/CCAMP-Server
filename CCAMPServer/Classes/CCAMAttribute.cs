using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CCAMPServer.Classes
{
    [AttributeUsage(AttributeTargets.Property)]
    public class CCAMAttribute : Attribute
    {
        #region Variables
        private string Name;
        private bool IsOptional;

        #endregion

        #region Constructors
        public CCAMAttribute(string name, bool isOptional = true)
        {
            this.Name = name;
            this.IsOptional = isOptional;
        }
        #endregion

        #region Methods

        public string GetName()
        {
            return this.Name;
        }

        public bool IsOPtional()
        {
            return this.IsOptional;
        }

        #endregion
    }
}
