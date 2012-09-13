﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ThomsonReuters.Languages.TypesLanguage.Syntax
{
    public interface IUsingsScope : ISyntaxNode
    {
        IEnumerable<QualifiedIdentifier> Usings { get; }
    }
}
