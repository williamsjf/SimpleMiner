﻿using SimpleMiner.Parsing.Html.Results;
using System;
using System.Collections.Generic;

namespace SimpleMiner.Parsing.Html.Components
{
    public class HtmlFormComponent : HtmlParseResult, IComponent
    {
        public HtmlFormComponent()
        {
            Values = new Dictionary<string, string>();
            SelectValues = new List<HtmlFormValues>();
        }

        public string Action { get; set; }

        public string Name { get; set; }

        public string Id { get; set; }

        public Dictionary<string, string> Values { get; set; }

        public string Method { get; set; }

        public List<HtmlFormValues> SelectValues { get; set; }

        public HtmlFormComponent Clone()
        {
            var instance = new HtmlFormComponent();
            instance = (HtmlFormComponent)this.MemberwiseClone();

            return instance;
        }
    }

    public class HtmlFormValues
    {
        public HtmlFormValues()
        {
            this.SelectName = string.Empty;
            this.SelectValues = new Dictionary<string, string>();
        }

        public string SelectName { get; set; }

        public Dictionary<string, string> SelectValues { get; set; }
    }
}
