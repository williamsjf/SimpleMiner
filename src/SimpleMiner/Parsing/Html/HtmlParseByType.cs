﻿using System.ComponentModel;

namespace SimpleMiner.Parsing.Html
{
    public enum HtmlParseByType
    {
        [Description("Id")]
        Id = 1,

        [Description("Class")]
        Class = 2,

        [Description("Xpath")]
        Xpath = 3
    }
}
