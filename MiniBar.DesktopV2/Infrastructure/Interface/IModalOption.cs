﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Infrastructure.Interface
{
    public interface IModalOption : IOption
    {
        WindowStartupLocation WindowStartupLocation { get; }

        Size Size { get; }

        bool ShowIcon { get; }

        ResizeMode ResizeMode { get; }

        bool IsDialog { get; }
    }
}