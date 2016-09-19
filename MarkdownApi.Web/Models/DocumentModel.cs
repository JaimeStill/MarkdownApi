﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MarkdownApi.Web.Models
{
    public class DocumentModel
    {
        public int id { get; set; }
        public string title { get; set; }
        public string markdown { get; set; }
        public string html { get; set; }
        public ProjectModel project { get; set; }
    }
}