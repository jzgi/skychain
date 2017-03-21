﻿using System;

namespace Greatbone.Core
{
    ///
    /// An access check filter before the target nodule is invoked.
    ///
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = false, Inherited = false)]
    public abstract class AuthorizeAttribute : Attribute
    {
        public Nodule Nodule { get; internal set; }

        public bool Start { get; set; }

        public bool End { get; set; }

        public abstract void Or(AuthorizeAttribute another);

        public abstract bool Check(ActionContext ac);
    }
}