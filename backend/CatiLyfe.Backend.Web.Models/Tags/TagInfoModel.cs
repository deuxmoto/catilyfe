using System;
using System.Collections.Generic;
using System.Text;

namespace CatiLyfe.Backend.Web.Models.Tags
{
    public class TagInfoModel
    {
        public TagInfoModel(string tag, int weight)
        {
            this.Tag = tag;
            this.Weight = weight;
        }

        public string Tag { get; private set; }

        public int Weight { get; private set; }
    }
}
