﻿using System;
using System.Collections.Generic;
using System.Text;

namespace DataServices.Providers
{
    public class DbAccessOptions
    {
        private readonly string DbAccessSettings = "DbAccessSettings";

        public int DbNRetrys { get; set; }
        public int DbWaitTimeInSeconds { get; set; }
    }
}
